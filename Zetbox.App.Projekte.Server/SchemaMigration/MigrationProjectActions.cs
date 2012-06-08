
namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using Zetbox.App.SchemaMigration;
    using Zetbox.API.Configuration;

    [Implementor]
    public class MigrationProjectActions
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.SchemaMigration");

        // TODO: fix this, as it is currently only working by accident
        private static ILifetimeScope _scope;
        private static ZetboxConfig _cfg;

        public MigrationProjectActions(ILifetimeScope scope, ZetboxConfig cfg)
        {
            _scope = scope;
            _cfg = cfg;
        }

        [Invocation]
        public static void UpdateFromSourceSchema(Zetbox.App.SchemaMigration.MigrationProject obj)
        {
            foreach (var s in obj.StagingDatabases)
            {
                using (Log.InfoTraceMethodCall("UpdateFromSourceSchema", s.Description))
                {
                    var connectionString = _cfg.Server.GetConnectionString(s.ConnectionStringKey);
                    if (string.IsNullOrEmpty(connectionString.ConnectionString)) throw new InvalidOperationException("Source ConnectionString is empty");
                    if (string.IsNullOrEmpty(connectionString.SchemaProvider)) throw new InvalidOperationException("Source Provider is empty");
                    ISchemaProvider src = _scope.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                    src.Open(connectionString.ConnectionString);

                    var destTbls = s.SourceTables
                        .ToDictionary(k => k.Name);

                    // foreach table in src
                    // TODO: And views!!
                    foreach (var tbl in src.GetTableNames().ToList().Union(src.GetViewNames().ToList()))
                    {
                        if (tbl.Schema != s.Schema) continue;

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

            // TODO: For now, submit changes
            // Later, implement InvokeOnServer correctly
            obj.Context.SubmitChanges();
        }
    }
}
