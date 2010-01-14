
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;

    /// <summary>
    /// Serversteuerung
    /// </summary>
    public class Server
        : IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server");

        public Server(IContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// The IoC container used by this Server.
        /// </summary>
        private IContainer container;

        public void GenerateCode()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.CreateInnerContainer())
            {
                var generator = subContainer.Resolve<Generators.Generator>();
                generator.GenerateCode();
            }
        }

        public void Export(string file, string[] namespaces)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            {
                Packaging.Exporter.Export(file, namespaces);
            }
        }

        public void Import(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            {
                Packaging.Importer.LoadFromXml(file);
            }
        }

        public void Publish(string file, string[] namespaces)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            {
                Packaging.Exporter.Publish(file, namespaces);
            }
        }

        public void Deploy(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            {
                Packaging.Importer.Deploy(file);
            }
        }

        public void CheckSchemaFromCurrentMetaData(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("withRepair=[{0}]", withRepair))
            using (var subContainer = container.CreateInnerContainer())
            using (IKistlContext ctx = KistlContext.GetContext())
            using (var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx)))
            {
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("withRepair=[{0}]", withRepair))
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                ISchemaProvider schemaProvider = subContainer.Resolve<ISchemaProvider>();
                SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(string file, bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],withRepair=[{1}]", file, withRepair))
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                using (FileStream fs = File.OpenRead(file))
                {
                    Packaging.Importer.LoadFromXml(ctx, fs);
                    var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                    mgr.CheckSchema(withRepair);
                }
            }
        }

        public void UpdateSchema()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                IKistlContext dbctx = subContainer.Resolve<IKistlContext>();

                // load database contents into local cache
                // to be independent of the database when managing 
                // the schema
                using (MemoryStream ms = new MemoryStream())
                {
                    Packaging.Exporter.Publish(dbctx, ms, new string[] { "*" });
                    ms.Seek(0, SeekOrigin.Begin);
                    Packaging.Importer.LoadFromXml(ctx, ms);
                }

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void UpdateSchema(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                using (FileStream fs = File.OpenRead(file))
                {
                    Packaging.Importer.LoadFromXml(ctx, fs);

                    var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                    mgr.UpdateSchema();
                }
            }
        }

        public void RunFixes()
        {
            using (Log.InfoTraceMethodCall())
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    //var baseClass = ctx.GetQuery<ControlKindClass>().Where(cls => cls.ClassName == "ControlKind").ToList().Single();
                    //var guiModule = ctx.GetQuery<Module>().Where(mod => mod.ModuleName == "GUI").ToList().Single();
                    //var trModule = ctx.GetQuery<Module>().Where(mod => mod.ModuleName == "TimeRecords").ToList().Single();

                    //CreateCkc(ctx, baseClass, guiModule, "KistlDebuggerKind", "Displays assorted debugging related informations about the datastore and processes",
                    //    new[] { typeof(Kistl.Client.Presentables.KistlDebuggerAsModel) });

                    //CreateCkc(ctx, baseClass, guiModule, "MenuItemKind", "A command used in a menu",
                    //    new[] { typeof(Kistl.Client.Presentables.ActionModel) });

                    //CreateCkc(ctx, baseClass, trModule, "TimeRecordsDashboardKind", "A dashboard for the TimeRecords module",
                    //    new[] { typeof(Kistl.Client.Presentables.TimeRecords.Dashboard) });

                    //CreateCkc(ctx, baseClass, trModule, "WorkEffortKind", "A specialized control for WorkEfforts",
                    //    new[] { typeof(Kistl.Client.Presentables.TimeRecords.WorkEffortModel) });

                    //CreateCkc(ctx, baseClass, guiModule, "GuiDashboardKind", "A dashboard for the GUI module",
                    //    new[] { typeof(Kistl.Client.Presentables.GUI.DashboardModel) });

                    //CreateCkc(ctx, baseClass, guiModule, "RelationKind", "A specialized control for Relations",
                    //    new[] { typeof(Kistl.Client.Presentables.Relations.RelationModel) });

                    //CreateCkc(ctx, baseClass, guiModule, "KistlDebuggerKind", "Displays assorted debugging related informations about the datastore and processes",
                    //    new[] { typeof(Kistl.Client.Presentables.KistlDebuggerAsModel) });

                    //CreateCkc(ctx, baseClass, guiModule, "KistlDebuggerKind", "Displays assorted debugging related informations about the datastore and processes",
                    //    new[] { typeof(Kistl.Client.Presentables.KistlDebuggerAsModel) });

                    ctx.SubmitChanges();
                }
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
