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

namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.DirectoryServices;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.App.Packaging;
    using Zetbox.Generator;

    /// <summary>
    /// Central Server Object
    /// </summary>
    internal sealed class Server
        : IDisposable, IServer
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server");

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
                var config = subContainer.Resolve<ZetboxConfig>();
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
                Exporter.ExportFromContext(subContainer.Resolve<IZetboxServerContext>(), file, schemaModules, ownerModules);
            }
        }

        public void Import(params string[] files)
        {
            using (Log.InfoTraceMethodCallFormat("Import", "files=[{0}]", string.Join(", ", files)))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IZetboxServerContext ctx = subContainer.Resolve<IZetboxServerContext>();
                Importer.LoadFromXml(ctx, files);
                Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        public void Publish(string file, string[] namespaces)
        {
            using (Log.InfoTraceMethodCallFormat("Publish", "file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            using (var subContainer = container.BeginLifetimeScope())
            {
                Exporter.PublishFromContext(subContainer.Resolve<IZetboxServerContext>(), file, namespaces);
            }
        }

        public void Deploy()
        {
            using (Log.InfoTraceMethodCall("Deploy", "files=*, update schema"))
            {
                var files = Directory.GetFiles("Modules", "*.xml", SearchOption.TopDirectoryOnly);
                if (files == null || files.Length == 0) throw new InvalidOperationException("No files found to deploy");
                Logging.Server.InfoFormat("Found {0} files to deploy", files.Length);
                // TODO: remove this as it is only a temporary workaround for introducing calculated properties
                CheckSchema(true);
                // TODO: Define a standard migration procedure
                MigrateDatabase();

                UpdateSchema(files);
                Deploy(files);
                CheckSchema(false);
            }
        }

        private void MigrateDatabase()
        {
            using (Log.InfoTraceMethodCall("Migrating Database"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                try
                {
                    var ctx = subContainer.Resolve<IZetboxServerContext>();
                    var module = ctx.GetQuery<Zetbox.App.Base.Module>().Where(i => i.Name == "KistlBase").FirstOrDefault();
                    if (module != null)
                    {
                        module.Name = "ZetboxBase";
                    }
                    ctx.SubmitRestore();
                }
                catch
                {
                    // TODO: For now - ignore any error
                    // TODO: Define a standard migration procedure
                }
            }
        }

        public void Deploy(params string[] files)
        {
            using (Log.InfoTraceMethodCallFormat("Deploy", "files=[{0}]", string.Join(", ", files)))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IZetboxServerContext>();
                Importer.Deploy(ctx, files);
                Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        public void CheckSchemaFromCurrentMetaData(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("CheckSchemaFromCurrentMetaData", "withRepair=[{0}]", withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IZetboxServerContext>();
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("CheckSchema", "withRepair=[{0}]", withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IZetboxContext ctx = subContainer.Resolve<BaseMemoryContext>();
                ZetboxConfig cfg = subContainer.Resolve<ZetboxConfig>();
                var connectionString = cfg.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);
                ISchemaProvider schemaProvider = subContainer.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                schemaProvider.Open(connectionString.ConnectionString);
                SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(string[] files, bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("CheckSchema", "files=[{0}],withRepair=[{1}]", string.Join(", ", files), withRepair))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IZetboxContext ctx = subContainer.Resolve<BaseMemoryContext>();
                Importer.LoadFromXml(ctx, files);
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void UpdateSchema()
        {
            using (Log.InfoTraceMethodCall("UpdateSchema"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IZetboxContext ctx = subContainer.Resolve<BaseMemoryContext>();
                IZetboxContext dbctx = subContainer.Resolve<IZetboxServerContext>();

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

        public void UpdateSchema(params string[] files)
        {
            using (Log.InfoTraceMethodCallFormat("UpdateSchema", "files=[{0}]", string.Join(", ", files)))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IZetboxContext ctx = subContainer.Resolve<BaseMemoryContext>();
                Importer.LoadFromXml(ctx, files);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void SyncIdentities()
        {
            using (Log.InfoTraceMethodCall("SyncIdentities"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                IZetboxContext ctx = subContainer.Resolve<IZetboxContext>();
                var userList = subContainer.Resolve<IIdentitySource>().GetAllIdentities();

                var identities = ctx.GetQuery<Zetbox.App.Base.Identity>().ToLookup(k => k.UserName.ToUpper());
                var everyone = Zetbox.NamedObjects.Base.Groups.Everyone.Find(ctx);

                foreach (var user in userList)
                {
                    if (!identities.Contains(user.UserName.ToUpper()))
                    {
                        var id = ctx.Create<Zetbox.App.Base.Identity>();
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

                var ctx = subContainer.Resolve<IZetboxServerContext>();

                foreach (var prop in ctx.GetQuery<Property>().Where(p => p.CategoryTags != null))
                {
                    prop.CategoryTags = prop.CategoryTags
                        .Replace("Changed", "Meta")
                        .Replace("Export", "Meta")
                        .Replace("Information", "Main")
                        .Trim();
                }

                //foreach (var prj in ctx.GetQuery<Zetbox.App.SchemaMigration.MigrationProject>())
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

                //var tr = typeof(Zetbox.App.Base.ObjectClass).ToRef(ctx);
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
                var config = subContainer.Resolve<ZetboxConfig>();
                var connectionString = config.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);
                var schemaProvider = subContainer.ResolveNamed<ISchemaProvider>(connectionString.SchemaProvider);
                schemaProvider.Open(connectionString.ConnectionString);
                schemaProvider.DropAllObjects();
            }
        }

        public List<IDataObject> GetParcelHack<T>(IZetboxServerContext ctx, int lastID, int count)
            where T : class, IDataObject
        {
            // The query translator cannot properly handle the IDataObject cast:
            // return GetQuery<T>().Cast<IDataObject>();

            var result = new List<IDataObject>();
            foreach (var o in ctx.GetQuery<T>().Where(obj => obj.ID > lastID).OrderBy(obj => obj.ID).Take(count))
            {
                result.Add(o);
            }
            return result;
        }

        private List<IDataObject> GetParcel(Type t, IZetboxServerContext ctx, int lastID, int count)
        {
            var mi = this.GetType().FindGenericMethod("GetParcelHack", new[] { t }, new Type[] { typeof(IZetboxServerContext), typeof(int), typeof(int) });
            return (List<IDataObject>)mi.Invoke(this, new object[] { ctx, lastID, count });
        }

        public void RecalculateProperties(Property[] properties)
        {
            using (Log.InfoTraceMethodCallFormat("RecalculateProperties", "properties.Length=[{0}]", properties == null ? "ALL" : properties.Length.ToString()))
            using (var propertyContainer = container.BeginLifetimeScope())
            using (var propertyCtx = propertyContainer.Resolve<IZetboxServerContext>())
            {
                var subContainer = container.BeginLifetimeScope();
                try
                {
                    var ctx = subContainer.Resolve<IZetboxServerContext>();
                    if (properties == null)
                    {
                        properties = propertyCtx.GetQuery<ValueTypeProperty>().Where(p => p.IsCalculated).ToArray();
                        // TODO: .Concat(propertyCtx.GetQuery<CompoundObjectProperty>().Where(p => p.IsCalculated))
                    }
                    int objCounter = 0;
                    foreach (var clsGroup in properties.GroupBy(p => p.ObjectClass).OrderBy(g => g.Key.Name).ThenBy(g => g.Key.ID))
                    {
                        if (clsGroup.Key is ObjectClass)
                        {
                            Log.InfoFormat("Processing ObjectClass [{0}]", clsGroup.Key.Name);
                            var lastID = 0;
                            var dtType = clsGroup.Key.GetDataType();
                            List<IDataObject> parcel = null;
                            do
                            {
                                parcel = GetParcel(dtType, ctx, lastID, 100);
                                foreach (var obj in parcel)
                                {
                                    foreach (var p in clsGroup)
                                    {
                                        obj.Recalculate(p.Name);
                                    }
                                    objCounter++;
                                    lastID = obj.ID;
                                }
                                Log.InfoFormat("Updated {0} objects", objCounter);
                                ctx.SubmitChanges();
                                subContainer.Dispose();
                                subContainer = container.BeginLifetimeScope();
                                ctx = subContainer.Resolve<IZetboxServerContext>();
                            } while (parcel != null && parcel.Count > 0);
                        }
                        else if (clsGroup.Key is CompoundObject)
                        {
                            Log.WarnFormat("Skipping CompoundObject [{0}]", clsGroup.Key.Name);
                        }
                    }
                    ctx.SubmitChanges();
                }
                finally
                {
                    subContainer.Dispose();
                }
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
        //        var ctx = subContainer.Resolve<IZetboxServerContext>();
        //        var list = ctx.GetQuery<Zetbox.App.Base.ObjectClass>().ToList();
        //        ctx.SubmitChanges();
        //        Log.InfoFormat("Loaded [{0}] objects", ctx.AttachedObjects.Count());
        //    }
        //}

        void FetchModules()
        {
            using (Log.InfoTraceMethodCall("FetchModules"))
            using (var subContainer = container.BeginLifetimeScope())
            {
                var ctx = subContainer.Resolve<IZetboxServerContext>();
                var list = ctx.GetQuery<Zetbox.App.Base.Module>().ToList();
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
