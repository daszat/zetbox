
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using ZBox.App.SchemaMigration;

    public class MigrationTasksBase : IMigrationTasks
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Migration");

        private readonly ISchemaProvider _src;
        private readonly ISchemaProvider _dst;
        private readonly IKistlContext _logCtx;

        public MigrationTasksBase(IKistlContext logCtx, ISchemaProvider src, ISchemaProvider dst)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (dst == null) throw new ArgumentNullException("dst");
            if (logCtx == null) throw new ArgumentNullException("logCtx");

            _src = src;
            _dst = dst;
            _logCtx = logCtx;
        }

        public InputStream ExecuteQueryStreaming(string sql)
        {
            return new InputStream(_src.ReadTableData(sql));
        }

        public OutputStream WriteTableStreaming(string destTable)
        {
            return new OutputStream(_dst.GetQualifiedTableName(destTable), _dst);
        }

        public void CleanDestination(SourceTable tbl)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");
            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping cleaning of unmapped table [{0}]", tbl.Name);
                return;
            }
            CleanDestination(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName));
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
        private static string GetColName(Kistl.App.Base.Property prop)
        {
            if (prop is Kistl.App.Base.ObjectReferenceProperty)
            {
                var orp = (Kistl.App.Base.ObjectReferenceProperty)prop;
                return "fk_" + orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).RoleName;
            }
            else
            {
                return prop.Name;
            }
        }

        public void TableBaseMigration(SourceTable tbl)
        {
            TableBaseMigration(tbl, null, null);
        }

        public void TableBaseMigration(SourceTable tbl, NullConverter[] nullConverter)
        {
            TableBaseMigration(tbl, nullConverter, null);
        }

        public void TableBaseMigration(SourceTable tbl, NullConverter[] nullConverter, Join[] additional_joins)
        {
            // ------------------- Argument checks ------------------- 
            if (tbl == null) throw new ArgumentNullException("tbl");
            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping base migration of unmapped table [{0}]", tbl.Name);
                return;
            }

            Log.InfoFormat("Migrating {0} to {1}", tbl.Name, tbl.DestinationObjectClass.Name);

            // ------------------- Build columns ------------------- 
            var mappedColumns = tbl.SourceColumn
                .Where(c => c.DestinationProperty != null)
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

        private static List<string> GetDestinationColumnNames(SourceTable tbl, List<SourceColumn> srcColumns)
        {
            var dstColumnNames = srcColumns.Select(c => GetColName(c.DestinationProperty)).ToList();
            // Error Col
            if (typeof(IMigrationInfo).IsAssignableFrom(tbl.DestinationObjectClass.GetDataType()))
            {
                dstColumnNames.Add("MigrationErrors");
            }
            return dstColumnNames;
        }

        private void TableBaseComplexMigration(SourceTable tbl, NullConverter[] nullConverter, List<SourceColumn> mappedColumns, List<SourceColumn> referringCols, Join[] additional_joins)
        {
            if (additional_joins != null
                && additional_joins.Length > 0
                && additional_joins.All(j => referringCols
                    .Select(c => c.DestinationProperty)
                    .OfType<ObjectReferenceProperty>()
                    .Any(orp => j.JoinTableName == _dst.GetQualifiedTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName))))
            {
                throw new InvalidOperationException("Unmapped additional joins found");
            }

            // could automatically create needed indices
            var all_joins = new Dictionary<SourceColumn, Join>();
            var root_joins = referringCols
                .GroupBy(k => k.DestinationProperty)
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
                                        .GroupBy(k => k.DestinationProperty)
                                        .Select(g => g.First(p => p.References.References == null))
                                ).ToList();
            var srcColumnNames = srcColumns.Select(c =>
            {
                var orp = c.DestinationProperty as ObjectReferenceProperty;
                if (c.References != null)
                {
                    return new ProjectionColumn()
                    {
                        ColumnName = "ID",
                        Alias = c.DestinationProperty.Name,
                        Source = all_joins[c]
                    };
                }
                else if (c.References == null
                    && c.DestinationProperty is ObjectReferenceProperty)
                {
                    if (additional_joins != null
                        && additional_joins.Count(i => i.JoinTableName == _dst.GetQualifiedTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName)) > 0)
                    {
                        return new ProjectionColumn()
                        {
                            ColumnName = "ID",
                            Alias = c.DestinationProperty.Name,
                            Source = additional_joins.Single(j => j.JoinTableName == _dst.GetQualifiedTableName(orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd).Type.TableName))
                        };
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("No join found for {0}", c));
                    }
                }
                else
                {
                    return new ProjectionColumn()
                    {
                        ColumnName = c.Name,
                        Source = null
                    };
                }
            }).ToList();

            var dstColumnNames = GetDestinationColumnNames(tbl, srcColumns);
            long processedRows;

            using (var srcReader = _src.ReadJoin(_src.GetQualifiedTableName(tbl.Name), srcColumnNames, joins))
            using (var translator = new Translator(tbl, srcReader, srcColumns, nullConverter))
            {
                _dst.WriteTableData(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName), translator, dstColumnNames);
                processedRows = translator.ProcessedRows;
            }

            // count rows in original table, joins should not add or remove rows
            WriteLog(
                tbl.Name, _src.CountRows(_src.GetQualifiedTableName(tbl.Name)),
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

        private IEnumerable<Join> CreateReferenceJoin(IGrouping<Property, KeyValuePair<SourceColumn, SourceColumn>> referenceGroup, Dictionary<SourceColumn, Join> all_joins)
        {
            var x = referenceGroup
                .Select(r => new KeyValuePair<SourceColumn, SourceColumn>(r.Key, r.Value.References)) // go to referenced SourceColumns
                .Where(r => r.Value.References != null && r.Value.DestinationProperty != null) // find secondary references
                .GroupBy(r => r.Value.DestinationProperty)
                .Select(g => new { Property = g.Key, Result = CreateReferenceJoin(g, all_joins) })
                .ToArray();

            if (x.Length > 0)
            {
                var result = new List<Join>();
                foreach (var i in x)
                {
                    foreach (var j in i.Result)
                    {
                        var join = CreateJoinComponent(referenceGroup, all_joins);
                        join.JoinColumnName = join.JoinColumnName.Concat(new[] { new ColumnRef("ID", j) }).ToArray();
                        join.FKColumnName = join.FKColumnName.Concat(new[] { new ColumnRef(GetColName(i.Property), ColumnRef.Local) }).ToArray();
                        j.Joins.Add(join);
                        result.Add(j);
                    }
                }
                return result;
            }
            else
            {
                var result = CreateJoinComponent(referenceGroup, all_joins);
                return new[] { result };
            }
        }

        private Join CreateJoinComponent(IGrouping<Property, KeyValuePair<SourceColumn, SourceColumn>> referenceGroup, Dictionary<SourceColumn, Join> all_joins)
        {
            // Get SrcTbl & check for uniqueness.
            var srcTbl = referenceGroup.Select(r => r.Value.References.SourceTable).Distinct().Single();
            var directRefs = referenceGroup.Where(r => r.Value.References.References == null);

            var result = new Join()
            {
                Type = JoinType.Left,
                JoinTableName = _dst.GetQualifiedTableName(srcTbl.DestinationObjectClass.TableName),
                JoinColumnName = directRefs.Select(reference => new ColumnRef(reference.Value.References.DestinationProperty.Name, ColumnRef.Local)).ToArray(),
                FKColumnName = directRefs.Select(reference => new ColumnRef(reference.Value.Name, ColumnRef.PrimaryTable)).ToArray()
            };
            directRefs.ForEach(dr => all_joins[dr.Key] = result);
            return result;
        }

        private void TableBaseSimpleMigration(SourceTable tbl, NullConverter[] nullConverter, List<SourceColumn> mappedColumns)
        {
            var dstColumnNames = GetDestinationColumnNames(tbl, mappedColumns);
            var srcColumnNames = mappedColumns.Select(c => c.Name).ToArray();

            // no fk mapping required
            using (var srcReader = _src.ReadTableData(_src.GetQualifiedTableName(tbl.Name), srcColumnNames))
            using (var translator = new Translator(tbl, srcReader, mappedColumns, nullConverter))
            {
                _dst.WriteTableData(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName), translator, dstColumnNames);

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
