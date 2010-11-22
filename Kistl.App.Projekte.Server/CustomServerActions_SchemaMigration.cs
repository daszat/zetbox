
namespace ZBox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Extensions;
    using ZBox.App.SchemaMigration;

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
            foreach (var s in obj.StagingDatabases)
            {
                if (string.IsNullOrEmpty(s.ConnectionString)) throw new InvalidOperationException("Source ConnectionString is empty");
                if (string.IsNullOrEmpty(s.Provider)) throw new InvalidOperationException("Source Provider is empty");
                ISchemaProvider src = _scope.ResolveNamed<ISchemaProvider>(s.Provider);
                src.Open(s.ConnectionString);

                var destTbls = s.SourceTables
                    .ToDictionary(k => k.Name);

                // foreach table in src
                // TODO: And views!!
                foreach (var tbl in src.GetTableNames().ToList().Union(src.GetViewNames().ToList()))
                {
                    Log.InfoFormat("reading table {0}", tbl);
                    SourceTable destTbl;
                    if (!destTbls.ContainsKey(tbl.Name))
                    {
                        destTbl = obj.Context.Create<SourceTable>();
                        destTbl.Name = tbl.Name;
                        s.SourceTables.Add(destTbl);
                    }
                    else
                    {
                        destTbl = destTbls[tbl.Name];
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
}
