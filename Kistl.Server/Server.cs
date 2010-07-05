
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
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
    using System.Data;

    /// <summary>
    /// Serversteuerung
    /// </summary>
    public class Server
        : IDisposable
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

        public void GenerateCode()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.BeginLifetimeScope())
            {
                var generator = subContainer.Resolve<Generators.Generator>();
                generator.GenerateCode();
            }
        }

        public void Export(string file, string[] namespaces)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            using (var subContainer = container.BeginLifetimeScope())
            {
                Exporter.ExportFromContext(subContainer.Resolve<IKistlContext>(), file, namespaces);
            }
        }

        public void Import(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
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
            using (Log.InfoTraceMethodCallFormat("file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            using (var subContainer = container.BeginLifetimeScope())
            {
                Exporter.PublishFromContext(subContainer.Resolve<IKistlContext>(), file, namespaces);
            }
        }

        public void Deploy(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            using (var subContainer = container.BeginLifetimeScope())
            using (FileStream fs = File.OpenRead(file))
            {
                var ctx = subContainer.Resolve<IKistlServerContext>();
                Importer.Deploy(ctx, fs);
                Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        public void CheckSchemaFromCurrentMetaData(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("withRepair=[{0}]", withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IKistlContext>();
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("withRepair=[{0}]", withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<BaseMemoryContext>();
                KistlConfig cfg = subContainer.Resolve<KistlConfig>();
                ISchemaProvider schemaProvider = subContainer.Resolve<ISchemaProvider>(cfg.Server.SchemaProvider);
                schemaProvider.Open(cfg.Server.ConnectionString);
                SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(string file, bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],withRepair=[{1}]", file, withRepair))
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
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<BaseMemoryContext>();
                IKistlContext dbctx = subContainer.Resolve<IKistlContext>();

                // load database contents into local cache
                // to be independent of the database when managing
                // the schema
                using (MemoryStream ms = new MemoryStream())
                {
                    Exporter.PublishFromContext(dbctx, ms, new string[] { "*" });
                    ms.Seek(0, SeekOrigin.Begin);
                    Importer.LoadFromXml(ctx, ms);
                }

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void UpdateSchema(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<BaseMemoryContext>();
                Importer.LoadFromXml(ctx, file);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void CopyDatabase(ISchemaProvider src, ISchemaProvider dest)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (dest == null) throw new ArgumentNullException("dest");

            Log.InfoFormat("Copy scr Database to staging database");

            Log.Info("Dropping all Tables and Objects");
            dest.DropAllObjects();

            // foreach table in src
            foreach (var tbl in src.GetTableNames())
            {
                Log.InfoFormat("Migrating table {0}", tbl);
                var cols = src.GetTableColumns(tbl);
                dest.CreateTable(tbl, cols);

                var colNames = cols.Select(i => i.Name).ToArray();

                using (IDataReader rd = src.ReadTableData(tbl, colNames))
                {
                    object[] values = new object[colNames.Length];
                    while (rd.Read())
                    {
                        rd.GetValues(values);
                        dest.WriteTableData(tbl, colNames, values);
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        // TODO: Replace this when NamedInstances are introduced
        public static readonly Guid Groups_Everyone = new Guid("76D43CF2-4DDF-4A3A-9AD6-28CABFDDDFF1");

        public void SyncIdentities()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.BeginLifetimeScope())
            {
                IKistlContext ctx = subContainer.Resolve<IKistlContext>();
                var userList = new Dictionary<string, string>();
                ReadUsers(Environment.UserDomainName, userList);
                ReadUsers(Environment.MachineName, userList);

                var identities = ctx.GetQuery<Kistl.App.Base.Identity>().ToLookup(k => k.UserName.ToUpper());
                var everyone = ctx.FindPersistenceObject<Kistl.App.Base.Group>(Groups_Everyone);

                foreach (var user in userList)
                {
                    if (!identities.Contains(user.Key.ToUpper()))
                    {
                        var id = ctx.Create<Kistl.App.Base.Identity>();
                        id.UserName = user.Key;
                        id.DisplayName = user.Value;
                        id.Groups.Add(everyone);
                        Log.InfoFormat("Adding Identity {0} ({1})", id.DisplayName, id.UserName);
                    }
                }

                ctx.SubmitChanges();
            }
        }

        private void ReadUsers(string machine, Dictionary<string, string> userList)
        {
            try
            {
                using (DirectoryEntry root = new DirectoryEntry("WinNT://" + machine))
                {
                    root.Children.SchemaFilter.Add("User");
                    foreach (DirectoryEntry d in root.Children)
                    {
                        var login = machine + "\\" + d.Name;
                        userList[login] = (d.Properties["FullName"].Value ?? login).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error reading users from " + machine, ex);
            }
        }

        public void RunFixes()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IKistlServerContext>();

                foreach (var prj in ctx.GetQuery<ZBox.App.SchemaMigration.MigrationProject>())
                {
                    prj.UpdateFromSourceSchema();
                }

                //Log.Info("Currently no fixes to do");

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
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IKistlServerContext>();
                var list = ctx.GetQuery<Kistl.App.Base.Module>().ToList();
                ctx.SubmitChanges();
                Log.InfoFormat("Loaded [{0}] objects", ctx.AttachedObjects.Count());
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
