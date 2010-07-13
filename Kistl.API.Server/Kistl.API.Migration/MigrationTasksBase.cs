
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

            _dst.TruncateTable(tbl.DestinationObjectClass.TableName);
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
                .OrderBy(c => c.Name).ToList();
            var srcColumnNames = srcColumns.Select(c => c.Name).ToArray();
            var dstColumnNames = srcColumns.Select(c => c.DestinationProperty.Name).ToArray();

            using (var sourceTbl = _src.ReadTableData(tbl.Name, srcColumnNames))
            using (var translator = new Translator(tbl, sourceTbl, srcColumns))
            {
                _dst.WriteTableData(tbl.DestinationObjectClass.TableName, translator, dstColumnNames);
            }
        }
    }
}
