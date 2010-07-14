
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using ZBox.App.SchemaMigration;

    public class MigrationTasksBase : IMigrationTasks
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Migration");

        private readonly ISchemaProvider _src;
        private readonly ISchemaProvider _dst;
        public MigrationTasksBase(ISchemaProvider src, ISchemaProvider dst)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (dst == null) throw new ArgumentNullException("dst");

            _src = src;
            _dst = dst;
        }

        public void CleanDestination(SourceTable tbl)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");

            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping cleaning of unmapped table [{0}]", tbl.Name);
                return;
            }

            _dst.TruncateTable(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName));
        }

        public void TableBaseMigration(SourceTable tbl)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");

            if (tbl.DestinationObjectClass == null)
            {
                Log.InfoFormat("Skipping base migration of unmapped table [{0}]", tbl.Name);
                return;
            }

            var srcColumns = tbl.SourceColumn
                .Where(c => c.DestinationProperty != null && c.DestinationProperty is Kistl.App.Base.ValueTypeProperty)
                .OrderBy(c => c.Name)
                .ToList();
            var srcColumnNames = srcColumns.Select(c => c.Name).ToArray();
            var dstColumnNames = srcColumns.Select(c => c.DestinationProperty.Name).ToArray();

            var referringCols = srcColumns.Where(c => c.References != null).ToList();
            if (referringCols.Count == 0)
            {
                // no fk mapping required
                using (var sourceTbl = _src.ReadTableData(_dst.GetQualifiedTableName(tbl.Name), srcColumnNames))
                using (var translator = new Translator(tbl, sourceTbl, srcColumns))
                {
                    _dst.WriteTableData(_dst.GetQualifiedTableName(tbl.DestinationObjectClass.TableName), translator, dstColumnNames);
                }
            }
            else
            {
                // could automatically create needed indices

                var joins = referringCols.Select(reference =>
                {
                    var referencedTable = _dst.GetQualifiedTableName(reference.References.SourceTable.DestinationObjectClass.TableName);
                    return new Join()
                    {
                        JoinTableName = referencedTable,
                        Type = JoinType.Left,
                        JoinColumnName = reference.References.DestinationProperty.Name,
                        FKColumnName = reference.Name
                    };
                });

                var srcReader = _src.ReadJoin(_src.GetQualifiedTableName(tbl.Name), joins);
                // SELECT src.*, ref1.ID, ... FROM src LEFT JOIN referencedTableDestination ref1 ON (src.col = ref1.col) ...
                // if (ref1.ID == null) { error += "ref1 could not be resolved"; set default }
            }
        }
    }
}
