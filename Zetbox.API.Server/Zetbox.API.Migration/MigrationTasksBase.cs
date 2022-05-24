// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.SchemaMigration;

    public class MigrationTasksBase
        : IMigrationTasks
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MigrationTasksBase));

        private readonly ISchemaProvider _src;
        private readonly ISchemaProvider _dst;
        private readonly IZetboxContext _logCtx;

        public MigrationTasksBase(IZetboxContext logCtx, ISchemaProvider src, ISchemaProvider dst)
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
        private static async Task<string> GetColName(Property prop)
        {
            if (prop is ObjectReferenceProperty)
            {
                var orp = (ObjectReferenceProperty)prop;
                return "fk_" + (await orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd)).RoleName;
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
        private static async Task<string> GetColName(IEnumerable<Property> props)
        {
            // TODO: Move Consruct to Server.API so we can use it here
            if (props.First() is CompoundObjectProperty)
            {
                return string.Join("_", props.Select(p => p.Name).ToArray());
            }
            else if (props.Count() == 1)
            {
                return await GetColName(props.Single());
            }
            else
            {
                throw new ArgumentException("Property must only contain exact one DestinationProperty or first must be a CompoundObjectProperty", "props");
            }
        }

        public async Task TableBaseMigration(SourceTable tbl)
        {
            await TableBaseMigration(tbl, (Converter[])null, (Join[])null);
        }

        public async Task TableBaseMigration(SourceTable tbl, params Converter[] converter)
        {
            await TableBaseMigration(tbl, converter, null);
        }

        public async Task TableBaseMigration(SourceTable tbl, Converter[] converter, Join[] additional_joins)
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
                    await TableBaseSimpleMigration(tbl, converter, mappedColumns);
                }
                else
                {
                    await TableBaseComplexMigration(tbl, converter, mappedColumns, referringCols, additional_joins);
                }
            }
        }

        private static async Task<List<string>> GetDestinationColumnNames(SourceTable tbl, List<SourceColumn> srcColumns)
        {
            var dstColumnNames = (await srcColumns.Select(async c => await GetColName(c.DestinationProperty)).WhenAll()).ToList();
            // Error Col
            if (typeof(IMigrationInfo).IsAssignableFrom(await tbl.DestinationObjectClass.GetDataType()))
            {
                dstColumnNames.Add("MigrationErrors");
            }
            dstColumnNames.AddRange(await srcColumns
                .Where(c => c.DestinationProperty.First() is CompoundObjectProperty)
                .GroupBy(c => c.DestinationProperty.First())
                .Select(async grp => await GetColName(grp.Key))
                .WhenAll());

            return dstColumnNames;
        }

        private Task TableBaseComplexMigration(SourceTable tbl, Converter[] converter, List<SourceColumn> mappedColumns, List<SourceColumn> referringCols, Join[] additional_joins)
        {
            throw new NotImplementedException("Handle async/await");
            //// TODO: re-implement this check
            ////if (additional_joins != null
            ////    && additional_joins.Length > 0
            ////    && additional_joins.All(async j => referringCols
            ////        .Select(c => c.DestinationProperty.Single())
            ////        .OfType<ObjectReferenceProperty>()
            ////        .Where(async orp => j.JoinTableName == _dst.GetTableName((await orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd)).Type.Module.SchemaName, (await orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd)).Type.TableName)))
            ////        .Any())
            ////{
            ////    throw new InvalidOperationException("Unmapped additional joins found");
            ////}

            ////// could automatically create needed indices
            ////var all_joins = new Dictionary<SourceColumn, Join>();
            ////var root_joins = (await Task.WhenAll(referringCols
            ////    .GroupBy(k => k.DestinationProperty.Single())
            ////    .SelectMany(referenceGroup => CreateReferenceJoin(referenceGroup, all_joins))))
            ////    .ToArray();

            ////// Add manual joins
            ////IEnumerable<Join> joins;
            ////if (additional_joins != null)
            ////{
            ////    joins = root_joins.Union(additional_joins);
            ////}
            ////else
            ////{
            ////    joins = root_joins;
            ////}

            ////var srcColumns = mappedColumns
            ////                    .Where(c => c.References == null)
            ////                    .Union(
            ////                        referringCols
            ////                            .GroupBy(k => k.DestinationProperty.Single()) // referring columns cannot be mapped remotely
            ////                            .Select(g => g.First(p => p.References.References == null))
            ////                    ).ToList();
            ////var srcColumnNames = (await Task.WhenAll(srcColumns.Select(async c =>
            ////{
            ////    var orp = c.DestinationProperty.FirstOrDefault() as ObjectReferenceProperty;
            ////    if (c.References != null)
            ////    {
            ////        return new ProjectionColumn("ID", all_joins[c], System.Data.DbType.Int32, c.DestinationProperty.Single().Name);
            ////    }
            ////    else if (c.References == null
            ////        && orp != null)
            ////    {
            ////        var otherEndType = await (await orp.RelationEnd.Parent.GetOtherEnd(orp.RelationEnd)).GetProp_Type();
            ////        var otherEndModule = await otherEndType.GetProp_Module();
            ////        if (additional_joins != null
            ////            && additional_joins.Count(i => i.JoinTableName == _dst.GetTableName(otherEndModule.SchemaName, otherEndType.TableName)) > 0)
            ////        {
            ////            return new ProjectionColumn(
            ////                "ID",
            ////                additional_joins.Single(j => j.JoinTableName == _dst.GetTableName(otherEndModule.SchemaName, otherEndType.TableName)),
            ////                System.Data.DbType.Int32,
            ////                orp.Name);
            ////        }
            ////        else if (converter.Any(cnv => cnv.Column.Name == c.Name))
            ////        {
            ////            return new ProjectionColumn(c.Name, ColumnRef.PrimaryTable, c.DestinationProperty.Single().Name);
            ////        }
            ////        else
            ////        {
            ////            throw new InvalidOperationException(string.Format("No join found for {0}", c));
            ////        }
            ////    }
            ////    else
            ////    {
            ////        return new ProjectionColumn(c.Name, ColumnRef.PrimaryTable, (System.Data.DbType)c.DbType, null);
            ////    }
            ////}))).ToList();

            ////var dstColumnNames = await GetDestinationColumnNames(tbl, srcColumns);
            ////long processedRows;

            ////using (var srcReader = _src.ReadJoin(_src.GetTableName(tbl.StagingDatabase.Schema, tbl.Name), srcColumnNames, joins))
            ////using (var translator = new Translator(tbl, srcReader, srcColumns, converter))
            ////{
            ////    _dst.WriteTableData(_dst.GetTableName(tbl.DestinationObjectClass.Module.SchemaName, tbl.DestinationObjectClass.TableName), translator, dstColumnNames);
            ////    processedRows = translator.ProcessedRows;
            ////}

            ////// count rows in original table, joins should not add or remove rows
            ////WriteLog(
            ////    tbl.Name, _src.CountRows(_src.GetTableName(tbl.StagingDatabase.Schema, tbl.Name)),
            ////    tbl.DestinationObjectClass.TableName, processedRows);
        }

        private async Task<IEnumerable<Join>> CreateReferenceJoin(IGrouping<Property, SourceColumn> referenceGroup, Dictionary<SourceColumn, Join> all_joins)
        {
            // Create a "fake" intermediate IGrouping of the right type
            return await CreateReferenceJoin(referenceGroup
                .Select(g => new KeyValuePair<SourceColumn, SourceColumn>(g, g))
                .GroupBy(g => referenceGroup.Key)
                .Single(),
                all_joins);
        }

        // recursively create necessary joins to resolve references
        private async Task<IEnumerable<Join>> CreateReferenceJoin(IGrouping<Property, KeyValuePair<SourceColumn, SourceColumn>> referenceGroup, Dictionary<SourceColumn, Join> all_joins)
        {
            var secondaryReferences = referenceGroup
                .Select(r => new KeyValuePair<SourceColumn, SourceColumn>(r.Key, r.Value.References)) // go to referenced SourceColumns
                .Where(r => r.Value.References != null && r.Value.DestinationProperty.Count > 0); // find secondary references

            // Create join for this referenceGroup
            var join = CreateJoinComponent(referenceGroup, all_joins);

            // add secondary joins for referenced tables
            foreach (var subJoin in await secondaryReferences
                .GroupBy(r => r.Value.DestinationProperty.Single())
                .Select(async g => new { Property = g.Key, Joins = await CreateReferenceJoin(g, all_joins) }).WhenAll())
            {
                foreach (var j in subJoin.Joins)
                {
                    // Append join columns to primary join
                    join.JoinColumnName = join.JoinColumnName.Concat(new[] { new ColumnRef("ID", j, System.Data.DbType.Int32) }).ToArray();
                    join.FKColumnName = join.FKColumnName.Concat(new[] { new ColumnRef(await GetColName(subJoin.Property), ColumnRef.Local, System.Data.DbType.Int32) }).ToArray();
                    join.CompareNullsAsEqual = join.CompareNullsAsEqual.Concat(new[] { j.CompareNullsAsEqual[0] }).ToArray();
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
                FKColumnName = directRefs.Select(reference => new ColumnRef(reference.Value.Name, ColumnRef.PrimaryTable, (System.Data.DbType)reference.Value.DbType)).ToArray(),
                CompareNullsAsEqual = directRefs.Select(reference => reference.Key.CompareNulls).ToArray()
            };
            directRefs.ForEach(dr => all_joins[dr.Key] = result);
            return result;
        }

        private async Task TableBaseSimpleMigration(SourceTable tbl, Converter[] nullConverter, List<SourceColumn> mappedColumns)
        {
            var dstColumnNames = await GetDestinationColumnNames(tbl, mappedColumns);
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
