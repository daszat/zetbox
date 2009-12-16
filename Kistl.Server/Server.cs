
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.API.Utils;

    /// <summary>
    /// Serversteuerung
    /// </summary>
    public class Server
        : MarshalByRefObject, IKistlAppDomain, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server");

        /// <summary>
        /// WCF Service Host
        /// </summary>
        private ServiceHost host = null;

        /// <summary>
        /// WCF Service Thread
        /// </summary>
        private Thread serviceThread = null;

        /// <summary>
        /// Only to signal the server start
        /// </summary>
        private AutoResetEvent serverStarted = new AutoResetEvent(false);

        /// <summary>
        /// The local Application Context
        /// </summary>
        private ServerApplicationContext appCtx;

        /// <summary>
        /// Starts the WCF Server in the background. If the server hasn't started successfully within 40 seconds, it is aborted and an InvalidOperationException is thrown.
        /// </summary>
        public void Start(KistlConfig config)
        {
            using (Log.InfoTraceMethodCall("Starting Server"))
            {
                Init(config);

                serviceThread = new Thread(new ThreadStart(this.RunWCFServer));
                serviceThread.Start();

                if (!serverStarted.WaitOne(40 * 1000, false))
                {
                    throw new InvalidOperationException("Server did not started within 40 sec.");
                }
            }
        }

        /// <summary>
        /// Initialises the configuration of the server.
        /// </summary>
        /// <param name="config"></param>
        public void Init(KistlConfig config)
        {
            // re-use application context if available
            if (ServerApplicationContext.Current == null)
            {
                appCtx = new ServerApplicationContext(config);
                appCtx.LoadDefaultActionsManager();
            }
            appCtx = ServerApplicationContext.Current;
        }

        /// <summary>
        /// Stops the WCF Server.
        /// </summary>
        public void Stop()
        {
            Log.Info("Stopping Server");

            host.Close();

            if (!serviceThread.Join(5000))
            {
                Log.Info("Server did not stopped, aborting");
                serviceThread.Abort();
            }
            serviceThread = null;
            serverStarted.Close();

            Log.Info("Server stopped");
        }

        /// <summary>
        /// Führt den eigentlichen WCF Host start asynchron durch und 
        /// wartet bis er wieder gestopped wird.
        /// </summary>
        private void RunWCFServer()
        {
            try
            {
                using (Log.DebugTraceMethodCall("Starting WCF Server"))
                {
                    host = new ServiceHost(typeof(KistlService));
                    host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
                    host.Faulted += new EventHandler(host_Faulted);

                    host.Open();
                    serverStarted.Set();
                }

                Log.Info("WCF Server started");

                while (host.State == CommunicationState.Opened)
                {
                    Thread.Sleep(100);
                }

                Log.Info("WCF Server: Hosts closed, exiting WCF thread");
            }
            catch (Exception error)
            {
                Log.Error("Unhandled exception while running WCF Server", error);
                throw error;
            }
        }

        private void host_Faulted(object sender, EventArgs e)
        {
            Log.Warn("Host faulted: " + e);
        }

        private void host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Log.WarnFormat("UnknownMessageReceived: {0}", e.Message);
        }

        public void GenerateCode()
        {
            using (Log.InfoTraceMethodCall())
            {
                Generators.Generator.GenerateCode();
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
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    using (var mgr = new SchemaManagement.SchemaManager(ctx))
                    {
                        mgr.CheckSchema(withRepair);
                    }
                }
            }
        }

        public void CheckSchema(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("withRepair=[{0}]", withRepair))
            {
                using (IKistlContext ctx = SchemaManagement.SchemaManager.GetSavedSchema())
                {
                    using (var mgr = new SchemaManagement.SchemaManager(ctx))
                    {
                        mgr.CheckSchema(withRepair);
                    }
                }
            }
        }

        public void CheckSchema(string file, bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],withRepair=[{1}]", file, withRepair))
            {
                using (IKistlContext ctx = new MemoryContext())
                {
                    using (FileStream fs = File.OpenRead(file))
                    {
                        Packaging.Importer.LoadFromXml(ctx, fs);
                        using (var mgr = new SchemaManagement.SchemaManager(ctx))
                        {
                            mgr.CheckSchema(withRepair);
                        }
                    }
                }
            }
        }

        public void UpdateSchema()
        {
            using (Log.InfoTraceMethodCall())
            {
                using (IKistlContext ctx = new MemoryContext())
                {
                    // decouple database context
                    using (IKistlContext dbctx = KistlContext.GetContext())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            Packaging.Exporter.Publish(dbctx, ms, new string[] { "*" });
                            ms.Seek(0, SeekOrigin.Begin);
                            Packaging.Importer.LoadFromXml(ctx, ms);
                        }
                    }

                    using (var mgr = new SchemaManagement.SchemaManager(ctx))
                    {
                        mgr.UpdateSchema();
                    }
                }
            }
        }

        public void UpdateSchema(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            {
                using (IKistlContext ctx = new MemoryContext())
                {
                    using (FileStream fs = File.OpenRead(file))
                    {
                        Packaging.Importer.LoadFromXml(ctx, fs);

                        using (var mgr = new SchemaManagement.SchemaManager(ctx))
                        {
                            mgr.UpdateSchema();
                        }
                    }
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

        private static void CreateCkc(IKistlContext ctx, ControlKindClass baseClass, Module guiModule, string className, string description, Type[] supportedTypes)
        {
            var roStringKind = ctx.Create<ControlKindClass>();
            roStringKind.BaseObjectClass = baseClass;
            roStringKind.ClassName = className;
            roStringKind.DefaultPresentableModelDescriptor = baseClass.DefaultPresentableModelDescriptor;
            roStringKind.Description = description;
            roStringKind.Module = guiModule;
            roStringKind.ShowIconInLists = baseClass.ShowIconInLists;
            roStringKind.ShowIdInLists = baseClass.ShowIdInLists;
            roStringKind.ShowNameInLists = baseClass.ShowNameInLists;
            foreach (var t in supportedTypes)
            {
                roStringKind.SupportedInterfaces.Add(t.ToRef(ctx));
            }
            roStringKind.TableName = className + "s";
        }

        #region IDisposable Members
        // TODO: implement Dispose Pattern after 
        // http://msdn2.microsoft.com/en-us/library/ms244737.aspx
        public void Dispose()
        {
            Log.Info("Disposing");
            //if (disposing)
            //{
            //    if (host != null)
            //    {
            //        host.Close();
            //        host = null;
            //    }
            //    if (hostStreams != null)
            //    {
            //        hostStreams.Close();
            //        hostStreams = null;
            //    }

            //    if (serverStarted != null)
            //    {
            //        serverStarted.Close();
            //        serverStarted = null;
            //    }
            //}
        }

        #endregion
    }
}
