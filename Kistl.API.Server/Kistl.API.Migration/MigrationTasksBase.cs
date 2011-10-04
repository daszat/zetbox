
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using ZBox.App.SchemaMigration;

    public class MigrationTasksBase
        : IMigrationTasks
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Migration");

        private readonly ISchemaProvider _src;
        private readonly ISchemaProvider _dst;
        private readonly IKistlContext _logCtx;

        public MigrationTasksBase(IKistlContext logCtx, ISchemaProvider src, ISchemaProvider dst)
        {
            if (src == null)
                throw new ArgumentNullException("src");
            if (dst == null)
                throw new ArgumentNullException("dst");
            if (logCtx == null)
                throw new ArgumentNullException("logCtx");

            _src = src;
            _dst = dst;
            _logCtx = logCtx;
        }

        public InputStream ExecuteQueryStreaming(string sql)
        {
            return new InputStream(_src.ReadTableData(sql));
        }

        public OutputStream WriteTableStreaming(TableRef destTable)
        {
            return new OutputStream(destTable, _dst);
        }

        public void CleanDestination(SourceTable tbl)
        {
            if (tbl == null)
                throw new ArgumentNullException("tbl");
            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping cleaning of unmapped table [{0}]", tbl.Name);
                return;
            }
            CleanDestination(_dst.GetTableName(tbl.DestinationObjectClass.Module.SchemaName, tbl.DestinationObjectClass.TableName));
        }

        public void CleanDestination(TableRef tbl)
        {
            _dst.TruncateTable(tbl);
        }

        /// <summary>
        /// TODO: Use Construct from Generator
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private static string GetColName(Property prop)
        {
            if (prop is ObjectReferenceProperty)
            {
                var orp = (ObjectReferenceProperty)prop;
                return "fk_" + orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).RoleName;
            }
            else
            {
                return prop.Name;
            }
        }

        /// <summary>
        /// TODO: Use Construct from Generator
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        private static string GetColName(IEnumerable<Property> props)
        {
            // TODO: Move Consruct to Server.API so we can use it here
            if (props.First() is CompoundObjectProperty)
            {
                return string.Join("_", props.Select(p => p.Name).ToArray());
            }
            else if (props.Count() == 1)
            {
                return GetColName(props.Single());
            }
            else
            {
                throw new ArgumentException("Property must only contain exact one DestinationProperty or first must be a CompoundObjectProperty", "props");
            }
        }

        public void TableBaseMigration(SourceTable tbl)
        {
            TableBaseMigration(tbl, (Converter[])null, (Join[])null);
        }

        public void TableBaseMigration(SourceTable tbl, params Converter[] nullConverter)
        {
            TableBaseMigration(tbl, nullConverter, null);
        }

        public void TableBaseMigration(SourceTable tbl, Converter[] nullConverter, Join[] additional_joins)
        {
            // ------------------- Argument checks ------------------- 
            if (tbl == null)
                throw new ArgumentNullException("tbl");
            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping base migration of unmapped table [{0}]", tbl.Name);
                return;
            }

            using (Log.InfoTraceMethodCallFormat("TableBaseMigration", "{0} to {1}", tbl.Name, tbl.DestinationObjectClass.Name))
            {
                // ------------------- Build columns ------------------- 
                var mappedColumns = tbl.SourceColumn
                    .Where(c => c.DestinationProperty.Count > 0)
                    .OrderBy(c => c.Name)
                    .ToList();
                // Ref Cols
                var referringCols = mappedColumns.Where(c => c.References != null).ToList();

                // ------------------- Migrate ------------------- 
                if (referringCols.Count == 0 && (additional_joins == null || additional_joins.Length == 0))
                {
                    TableBaseSimpleMigration(tbl, nullConverter, mappedColumns);
                }
                else
                {
                    TableBaseComplexMigration(tbl, nullConverter, mappedColumns, referringCols, additional_joins);
                }
            }
        }

        private static List<string> GetDestinationColumnNames(SourceTable tbl, List<SourceColumn> srcColumns)
        {
            var dstColumnNames = srcColumns.Select(c => GetColName(c.DestinationProperty)).ToList();
            // Error Col
            if (typeof(IMigrationInfo).IsAssignableFrom(tbl.DestinationObjectClass.GetDataType()))
            {
                dstColumnNames.Add("MigrationErrors");
            }
            dstColumnNames.AddRange(srcColumns
                .Where(c => c.DestinationProperty.First() is CompoundObjectProperty)
                .GroupBy(c => c.DestinationProperty.First())
                .Select(grp => GetColName(grp.Key)));

            return dstColumnNames;
        }

        private void TableBaseComplexMigration(SourceTable tbl, Converter[] nullConverter, List<SourceColumn> mappedColumns, List<SourceColumn> referringCols, Join[] additional_joins)
        {
            if (additional_joins != null
                && additional_joins.Length > 0
                && additional_joins.All(j => referringCols
                    .Select(c => c.DestinationProperty.Single())
                    .OfType<ObjectReferenceProperty>()
                    .Any(orp => j.JoinTableName == _dst.GetTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.Module.SchemaName, orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName))))
            {
                throw new InvalidOperationException("Unmapped additional joins found");
            }

            // could automatically create needed indices
            var all_joins = new Dictionary<SourceColumn, Join>();
            var root_joins = referringCols
                .GroupBy(k => k.DestinationProperty.Single())
                .SelectMany(referenceGroup => CreateReferenceJoin(referenceGroup, all_joins))
                .ToArray();

            // Add manual joins
            IEnumerable<Join> joins;
            if (additional_joins != null)
            {
                joins = root_joins.Union(additional_joins);
            }
            else
            {
                joins = root_joins;
            }

            var srcColumns = mappedColumns
                                .Where(c => c.References == null)
                                .Union(
                                    referringCols
                                        .GroupBy(k => k.DestinationProperty.Single()) // referring columns cannot be mapped remotely
                                        .Select(g => g.First(p => p.References.References == null))
                                ).ToList();
            var srcColumnNames = srcColumns.Select(c =>
            {
                var orp = c.DestinationProperty.FirstOrDefault() as ObjectReferenceProperty;
                if (c.References != null)
                {
                    return new ProjectionColumn("ID", all_joins[c], System.Data.DbType.Int32, c.DestinationProperty.Single().Name);
                }
                else if (c.References == null
                    && orp != null)
                {
                    if (additional_joins != null
                        && additional_joins.Count(i => i.JoinTableName == _dst.GetTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.Module.SchemaName, orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName)) > 0)
                    {
                        return new ProjectionColumn(
                            "ID",
                            additional_joins.Single(j => j.JoinTableName == _dst.GetTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.Module.SchemaName, orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName)),
                            System.Data.DbType.Int32,
                            orp.Name);
                    }
                    else if (nullConverter.Any(converter => converter.Column.Name == c.Name))
                    {
                        return new ProjectionColumn(c.Name, ColumnRef.PrimaryTable, c.DestinationProperty.Single().Name);
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("No join found for {0}", c));
                    }
                }
                else
                {
                    return new ProjectionColumn(c.Name, ColumnRef.PrimaryTable, (System.Data.DbType)c.DbType, null);
                }
            }).ToList();

            var dstColumnNames = GetDestinationColumnNames(tbl, srcColumns);
            long processedRows;

            using (var srcReader = _src.ReadJoin(_src.GetTableName(tbl.StagingDatabase.Schema, tbl.Name), srcColumnNames, joins))
            using (var translator = new Translator(tbl, srcReader, srcColumns, nullConverter))
            {
                _dst.WriteTableData(_dst.GetTableName(tbl.DestinationObjectClass.Module.SchemaName, tbl.DestinationObjectClass.TableName), translator, dstColumnNames);
                processedRows = translator.ProcessedRows;
            }

            // count rows in original table, joins should not add or remove rows
            WriteLog(
                tbl.Name, _src.CountRows(_src.GetTableName(tbl.StagingDatabase.Schema, tbl.Name)),
                tbl.DestinationObjectClass.TableName, processedRows);
        }

        private IEnumerable<Join> CreateReferenceJoin(IGrouping<Property, SourceColumn> referenceGroup, Dictionary<SourceColumn, Join> all_joins)
        {
            // Create a "fake" intermediate IGrouping of the right type
            return CreateReferenceJoin(referenceGroup
                .Select(g => new KeyValuePair<SourceColumn, SourceColumn>(g, g))
                .GroupBy(g => referenceGroup.Key)
                .Single(),
                all_joins);
        }

        // recursively create necessary joins to resolve references
        private IEnumerable<Join> CreateReferenceJoin(IGrouping<Property, KeyValuePair<SourceColumn, SourceColumn>> referenceGroup, Dictionary<SourceColumn, Join> all_joins)
        {
            var secondaryReferences = referenceGroup
                .Select(r => new KeyValuePair<SourceColumn, SourceColumn>(r.Key, r.Value.References)) // go to referenced SourceColumns
                .Where(r => r.Value.References != null && r.Value.DestinationProperty.Count > 0); // find secondary references

            // Create join for this referenceGroup
            var join = CreateJoinComponent(referenceGroup, all_joins);

            // add secondary joins for referenced tables
            foreach (var subJoin in secondaryReferences
                .GroupBy(r => r.Value.DestinationProperty.Single())
                .Select(g => new { Property = g.Key, Joins = CreateReferenceJoin(g, all_joins) })
                .ToArray())
            {
                foreach (var j in subJoin.Joins)
                {
                    // Append join columns to primary join
                    join.JoinColumnName = join.JoinColumnName.Concat(new[] { new ColumnRef("ID", j, System.Data.DbType.Int32) }).ToArray();
                    join.FKColumnName = join.FKColumnName.Concat(new[] { new ColumnRef(GetColName(subJoin.Property), ColumnRef.Local, System.Data.DbType.Int32) }).ToArray();
                    join.Joins.Add(j);
                }
            }

            return new[] { join };
        }

        private Join CreateJoinComponent(IGrouping<Property, KeyValuePair<SourceColumn, SourceColumn>> referenceGroup, Dictionary<SourceColumn, Join> all_joins)
        {
            // Get SrcTbl & check for uniqueness.
            var srcTbl = referenceGroup.Select(r => r.Value.References.SourceTable).Distinct().Single();
            var directRefs = referenceGroup.Where(r => r.Value.References.References == null);

            var result = new Join()
            {
                Type = JoinType.Left,
                JoinTableName = _dst.GetTableName(srcTbl.DestinationObjectClass.Module.SchemaName, srcTbl.DestinationObjectClass.TableName),
                JoinColumnName = directRefs.Select(reference => new ColumnRef(reference.Value.References.DestinationProperty.Single().Name, ColumnRef.Local, reference.Value.References.DestinationProperty.Single().GetDbType())).ToArray(),
                FKColumnName = directRefs.Select(reference => new ColumnRef(reference.Value.Name, ColumnRef.PrimaryTable, (System.Data.DbType)reference.Value.DbType)).ToArray()
            };
            directRefs.ForEach(dr => all_joins[dr.Key] = result);
            return result;
        }

        private void TableBaseSimpleMigration(SourceTable tbl, Converter[] nullConverter, List<SourceColumn> mappedColumns)
        {
            var dstColumnNames = GetDestinationColumnNames(tbl, mappedColumns);
            var srcColumnNames = mappedColumns.Select(c => c.Name).ToArray();
            var tblRef = _src.GetTableName(tbl.StagingDatabase.Schema, tbl.Name);

            // no fk mapping required
            using (var srcReader = _src.ReadTableData(tblRef, srcColumnNames))
            using (var translator = new Translator(tbl, srcReader, mappedColumns, nullConverter))
            {
                _dst.WriteTableData(_dst.GetTableName(tbl.DestinationObjectClass.Module.SchemaName, tbl.DestinationObjectClass.TableName), translator, dstColumnNames);

                WriteLog(
                    tbl.Name, translator.ProcessedRows,
                    tbl.DestinationObjectClass.TableName, translator.ProcessedRows);
            }
        }

        private void WriteLog(string srcTbl, long srcRows, string dstTbl, long dstRows)
        {
            var log = _logCtx.Create<MigrationLog>();
            log.Timestamp = DateTime.Now;
            log.Source = srcTbl;
            log.SourceRows = (int)srcRows;
            log.Destination = dstTbl;
            log.DestinationRows = (int)dstRows;
            _logCtx.SubmitChanges();
        }
    }
}
