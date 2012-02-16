
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.DirectoryServices;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.App.Packaging;

    /// <summary>
    /// Central Server Object
    /// </summary>
    internal sealed class Server
        : IDisposable, IServer
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server");

        public Server(ILifetimeScope container)
        {
            this.container = container;
        }

        /// <summary>
        /// The IoC container used by this Server.
        /// </summary>
        private ILifetimeScope container;

        public void AnalyzeDatabase(string connectionName, TextWriter output)
        {
            using (Log.InfoTraceMethodCall("AnalyzeDatabase", connectionName))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var config = subContainer.Resolve<KistlConfig>();
                var connectionString = config.Server.GetConnectionString(connectionName);
                var schemaProvider = subContainer.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                schemaProvider.Open(connectionString.ConnectionString);
                output.WriteLine("# Tables");
                foreach (var table in schemaProvider.GetTableNames().OrderBy(t => t.Name))
                {
                    output.WriteLine(" * {0}", table);
                }
                output.WriteLine("# Views");
                foreach (var view in schemaProvider.GetViewNames().OrderBy(v => v.Name))
                {
                    output.WriteLine(" * {0}", view);
                    foreach (var line in schemaProvider.GetViewDefinition(view).Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        output.WriteLine(" > {0}", line);
                    }
                    output.WriteLine();
                }
                output.WriteLine("# Procedures");
                foreach (var proc in schemaProvider.GetProcedureNames().OrderBy(p => p.Name))
                {
                    output.WriteLine(" * {0}", proc);
                    foreach (var line in schemaProvider.GetProcedureDefinition(proc).Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        output.WriteLine(" > {0}", line);
                    }
                    output.WriteLine();
                }
            }
        }

        public void Export(string file, string[] schemaModules, string[] ownerModules)
        {
            using (Log.InfoTraceMethodCallFormat("Export", "file=[{0}],schemaModules=[{1}],ownerModules=[{2}]", file, string.Join(";", schemaModules ?? new string[] { }), string.Join(";", ownerModules ?? new string[] { })))
            using (var subContainer = container.BeginLifetimeScope())
            {
                Exporter.ExportFromContext(subContainer.Resolve<IKistlServerContext>(), file, schemaModules, ownerModules);
            }
        }

        public void Import(string file)
        {
            using (Log.InfoTraceMethodCallFormat("Import", "file=[{0}]", file))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlServerContext ctx = subContainer.Resolve<IKistlServerContext>();
                Importer.LoadFromXml(ctx, file);
                Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        public void Publish(string file, string[] namespaces)
        {
            using (Log.InfoTraceMethodCallFormat("Publish", "file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            using (var subContainer = container.BeginLifetimeScope())
            {
                Exporter.PublishFromContext(subContainer.Resolve<IKistlServerContext>(), file, namespaces);
            }
        }

        public void Deploy(string file)
        {
            using (Log.InfoTraceMethodCallFormat("Deploy", "file=[{0}]", file))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IKistlServerContext>();
                Importer.Deploy(ctx, file);
                Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        public void CheckBaseSchema(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("CheckSchemaFromCurrentMetaData", "withRepair=[{0}]", withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IFrozenContext>();
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckBaseSchema(withRepair);
            }
        }

        public void CheckSchemaFromCurrentMetaData(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("CheckSchemaFromCurrentMetaData", "withRepair=[{0}]", withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IKistlServerContext>();
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("CheckSchema", "withRepair=[{0}]", withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<BaseMemoryContext>();
                KistlConfig cfg = subContainer.Resolve<KistlConfig>();
                var connectionString = cfg.Server.GetConnectionString(Kistl.API.Helper.KistlConnectionStringKey);
                ISchemaProvider schemaProvider = subContainer.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                schemaProvider.Open(connectionString.ConnectionString);
                SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(string file, bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("CheckSchema", "file=[{0}],withRepair=[{1}]", file, withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<BaseMemoryContext>();
                Importer.LoadFromXml(ctx, file);
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void UpdateSchema()
        {
            using (Log.InfoTraceMethodCall("UpdateSchema"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<BaseMemoryContext>();
                IKistlContext dbctx = subContainer.Resolve<IKistlServerContext>();

                // load database contents into local cache
                // to be independent of the database when managing
                // the schema
                using (MemoryStream ms = new MemoryStream())
                {
                    Exporter.PublishFromContext(dbctx, ms, new string[] { "*" }, "in-memory buffer");
                    ms.Seek(0, SeekOrigin.Begin);
                    Importer.LoadFromXml(ctx, ms, "in-memory buffer");
                }

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void UpdateSchema(string[] files)
        {
            using (Log.InfoTraceMethodCallFormat("UpdateSchema", "file=[{0}]", string.Join(";", files)))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<BaseMemoryContext>();
                foreach (var f in files)
                {
                    Importer.LoadFromXml(ctx, f);
                }

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void SyncIdentities()
        {
            using (Log.InfoTraceMethodCall("SyncIdentities"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<IKistlContext>();
                var userList = subContainer.Resolve<IIdentitySource>().GetAllIdentities();

                var identities = ctx.GetQuery<Kistl.App.Base.Identity>().ToLookup(k => k.UserName.ToUpper());
                var everyone = Kistl.NamedObjects.Base.Groups.Everyone.Find(ctx);

                foreach (var user in userList)
                {
                    if (!identities.Contains(user.UserName.ToUpper()))
                    {
                        var id = ctx.Create<Kistl.App.Base.Identity>();
                        id.UserName = user.UserName;
                        id.DisplayName = user.DisplayName;
                        id.Groups.Add(everyone);
                        Log.InfoFormat("Adding Identity {0} ({1})", id.DisplayName, id.UserName);
                    }
                }

                ctx.SubmitChanges();
            }
        }

        public void RunFixes()
        {
            using (Log.InfoTraceMethodCall("RunFixes"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                //Log.Info("Currently no fixes to do");

                var ctx = subContainer.Resolve<IKistlServerContext>();

                //foreach (var prj in ctx.GetQuery<ZBox.App.SchemaMigration.MigrationProject>())
                //{
                //    prj.UpdateFromSourceSchema();
                //}


                //foreach (var tr in ctx.GetQuery<TypeRef>())
                //{
                //    tr.UpdateToStringCache();
                //}

                //foreach (var ck in ctx.GetQuery<ControlKind>())
                //{
                //    var ckc = ck.GetObjectClass(ctx);
                //    ck.Name = ckc.Module.Namespace + "." + ckc.Name;

                //    //var parent = ckc.BaseObjectClass;
                //    //if (parent != null)
                //    //{
                //    //    ck.Parent = ctx.GetQuery<ControlKind>().FirstOrDefault(c => c.Name == parent.Name);
                //    //}
                //}

                //foreach (var vd in ctx.GetQuery<ViewDescriptor>())
                //{
                //    if (vd.Kind != null)
                //    {
                //        string name = vd.Kind.Name;
                //        vd.ControlKind = ctx.GetQuery<ControlKind>().FirstOrDefault(c => c.Name == name);
                //    }
                //}

                //var usedControlKinds = ctx.GetQuery<ViewModelDescriptor>().Select(vmd => vmd.DefaultKind).ToList();
                //usedControlKinds.AddRange(ctx.GetQuery<ViewModelDescriptor>().Select(vmd => vmd.DefaultGridCellKind).ToList());
                //usedControlKinds.AddRange(ctx.GetQuery<ViewModelDescriptor>().ToList().SelectMany(vmd => vmd.SecondaryControlKinds).ToList());

                //foreach (var ck in ctx.GetQuery<ControlKind>())
                //{
                //    if (!usedControlKinds.Contains(ck))
                //    {
                //        ctx.Delete(ck);
                //    }
                //}

                //var tr = typeof(Kistl.App.Base.ObjectClass).ToRef(ctx);
                //Console.WriteLine(tr.ToString());

                //using (var s = new MemoryStream())
                //{
                //    s.Write(new byte[] { (byte)'h', (byte)'e', (byte)'l', (byte)'l', (byte)'o' }, 0, 5);
                //    var blob = ctx.CreateBlob(s, "test.txt", "text");
                //}

                ctx.SubmitChanges();
            }
        }

        public void WipeDatabase()
        {
            using (var subContainer = container.BeginLifetimeScope())
            {
                var config = subContainer.Resolve<KistlConfig>();
                var connectionString = config.Server.GetConnectionString(Kistl.API.Helper.KistlConnectionStringKey);
                var schemaProvider = subContainer.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                schemaProvider.Open(connectionString.ConnectionString);
                schemaProvider.DropAllObjects();
            }
        }

        public void RunBenchmarks()
        {
            // FetchObjectClasses();        
            Console.WriteLine("Waiting to start benchmark. Press return key to commence.");
            Console.ReadKey();
            FetchModules();
        }

        //void FetchObjectClasses()
        //{
        //    using (Log.InfoTraceMethodCall())
        //        using (var subContainer = container.CreateInnerContainer()) {
        //        var ctx = subContainer.Resolve<IKistlServerContext>();
        //        var list = ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList();
        //        ctx.SubmitChanges();
        //        Log.InfoFormat("Loaded [{0}] objects", ctx.AttachedObjects.Count());
        //    }
        //}

        void FetchModules()
        {
            using (Log.InfoTraceMethodCall("FetchModules"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IKistlServerContext>();
                var list = ctx.GetQuery<Kistl.App.Base.Module>().ToList();
                ctx.SubmitChanges();
                Log.InfoFormat("Fetched [{0}] modules; loaded [{1}] objects", list.Count, ctx.AttachedObjects.Count());
            }
        }

        #region IDisposable Members

        // TODO: implement Dispose Pattern after
        // http://msdn2.microsoft.com/en-us/library/ms244737.aspx
        public void Dispose()
        {
            Log.Info("Disposing");
            {
                if (container != null)
                {
                    container.Dispose();
                    container = null;
                }
            }
        }

        #endregion
    }
}
