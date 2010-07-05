using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server;
using Kistl.App.Extensions;
using Kistl.API.Utils;
using Kistl.API;
using ZBox.App.SchemaMigration;
using Autofac;

namespace ZBox.App.SchemaMigration
{
    public class CustomServerActions_SchemaMigration
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("ZBox.SchemaMigration");

        // TODO: fix this, as it is currently only working by accident
        private static ILifetimeScope _scope;

        public CustomServerActions_SchemaMigration(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public static void OnUpdateFromSourceSchema_MigrationProject(ZBox.App.SchemaMigration.MigrationProject obj)
        {
            if (string.IsNullOrEmpty(obj.SrcConnectionString)) throw new InvalidOperationException("Source ConnectionString is empty");
            if (string.IsNullOrEmpty(obj.SrcProvider)) throw new InvalidOperationException("Source Provider is empty");
            ISchemaProvider src = _scope.Resolve<ISchemaProvider>(obj.SrcProvider);
            src.Open(obj.SrcConnectionString);

            var destTbls = obj.Context.GetQuery<SourceTable>()
                .Where(i => i.MigrationProject == obj)
                .ToDictionary(k => k.Name);

            // foreach table in src
            foreach (var tbl in src.GetTableNames())
            {
                Log.InfoFormat("reading table {0}", tbl);
                SourceTable destTbl;
                if (!destTbls.ContainsKey(tbl))
                {
                    destTbl = obj.Context.Create<SourceTable>();
                    destTbl.Name = tbl;
                    obj.SourceTables.Add(destTbl);
                }
                else
                {
                    destTbl = destTbls[tbl];
                }

                var cols = src.GetTableColumns(tbl);
                var destCols = obj.Context.GetQuery<SourceColumn>()
                    .Where(i => i.SourceTable == destTbl)
                    .ToDictionary(k => k.Name);

                foreach (var col in cols)
                {
                    SourceColumn destCol;
                    if (!destCols.ContainsKey(col.Name))
                    {
                        destCol = obj.Context.Create<SourceColumn>();
                        destCol.Name = col.Name;
                        destTbl.SourceColumn.Add(destCol);
                    }
                    else
                    {
                        destCol = destCols[col.Name];
                    }
                    destCol.DbType = (ColumnType)col.Type;
                    destCol.IsNullable = col.IsNullable;
                    destCol.Size = col.Size;
                }
            }
        }
    }
}
