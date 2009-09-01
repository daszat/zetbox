
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
        /// <summary>
        /// WCF Service Host
        /// </summary>
        private ServiceHost host = null;

        /// <summary>
        /// WCF Streams Service Host
        /// </summary>
        private ServiceHost hostStreams = null;

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
            using (Logging.Log.TraceMethodCall("Starting Server"))
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
                using (var ctx = KistlContext.GetContext())
                {
                    appCtx = new ServerApplicationContext(config);
                    appCtx.LoadDefaultActionsManager();
                }
            }
            appCtx = ServerApplicationContext.Current;
        }

        /// <summary>
        /// Stops the WCF Server.
        /// </summary>
        public void Stop()
        {
            Logging.Log.Info("Stopping Server");

            host.Close();
            hostStreams.Close();

            if (!serviceThread.Join(5000))
            {
                Logging.Log.Info("Server did not stopped, aborting");
                serviceThread.Abort();
            }
            serviceThread = null;
            serverStarted.Close();

            Logging.Log.Info("Server stopped");
        }

        private const string DefaultServiceUrl = "http://localhost:6666/KistlService";
        private const string DefaultStreamsUrl = "http://localhost:6666/KistlServiceStreams";

        public static string GetServiceUrl(KistlConfig cfg)
        {
            return String.IsNullOrEmpty(cfg.ServiceUrl) ? DefaultServiceUrl : cfg.ServiceUrl;
        }

        public static string GetStreamsUrl(KistlConfig cfg)
        {
            return String.IsNullOrEmpty(cfg.StreamsUrl) ? DefaultStreamsUrl : cfg.StreamsUrl;
        }

        /// <summary>
        /// Führt den eigentlichen WCF Host start asynchron durch und 
        /// wartet bis er wieder gestopped wird.
        /// </summary>
        private void RunWCFServer()
        {
            try
            {
                using (Logging.Log.TraceMethodCall("Starting WCF Server"))
                {
                    host = new ServiceHost(typeof(KistlService),
                        new Uri(GetServiceUrl(appCtx.Configuration)));
                    host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
                    host.Faulted += new EventHandler(host_Faulted);

                    host.Open();

                    hostStreams = new ServiceHost(typeof(KistlServiceStreams),
                        new Uri(GetStreamsUrl(appCtx.Configuration)));
                    hostStreams.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
                    hostStreams.Faulted += new EventHandler(host_Faulted);

                    hostStreams.Open();

                    serverStarted.Set();
                }

                Logging.Log.Info("WCF Server started");

                while (host.State == CommunicationState.Opened)
                {
                    Thread.Sleep(100);
                }

                Logging.Log.Info("WCF Server: Hosts closed, exiting WCF thread");
            }
            catch (Exception error)
            {
                Logging.Log.Error("Unhandled exception while running WCF Server", error);
                throw error;
            }
        }

        private void host_Faulted(object sender, EventArgs e)
        {
            Logging.Log.Warn("Host faulted: " + e);
        }

        private void host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Logging.Log.WarnFormat("UnknownMessageReceived: {0}", e.Message);
        }

        public void GenerateCode()
        {
            Generators.Generator.GenerateCode();
        }

        internal void Export(string file, string[] namespaces)
        {
            Packaging.Exporter.Export(file, namespaces);
        }

        internal void Import(string file)
        {
            Packaging.Importer.LoadFromXml(file);
        }

        internal void Publish(string file, string[] namespaces)
        {
            Packaging.Exporter.Publish(file, namespaces);
        }

        public void Deploy(string file)
        {
            Packaging.Importer.Deploy(file);
        }

        internal void CheckSchemaFromCurrentMetaData(bool withRepair)
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                using (FileStream report = File.OpenWrite(@"C:\temp\KistlCodeGen\schemareport.log"))
                {
                    report.SetLength(0);
                    using (var mgr = new SchemaManagement.SchemaManager(ctx, report))
                    {
                        mgr.CheckSchema(withRepair);
                    }
                }
            }
        }

        public void CheckSchema(bool withRepair)
        {
            using (IKistlContext ctx = SchemaManagement.SchemaManager.GetSavedSchema())
            {
                using (FileStream report = File.OpenWrite(@"C:\temp\KistlCodeGen\schemareport.log"))
                {
                    report.SetLength(0);
                    using (var mgr = new SchemaManagement.SchemaManager(ctx, report))
                    {
                        mgr.CheckSchema(withRepair);
                    }
                }
            }
        }

        internal void CheckSchema(string file, bool withRepair)
        {
            using (IKistlContext ctx = new MemoryContext())
            {
                using (FileStream fs = File.OpenRead(file))
                {
                    Packaging.Importer.LoadFromXml(ctx, fs);
                    using (FileStream report = File.OpenWrite(@"C:\temp\KistlCodeGen\schemareport.log"))
                    {
                        report.SetLength(0);
                        using (var mgr = new SchemaManagement.SchemaManager(ctx, report))
                        {
                            mgr.CheckSchema(withRepair);
                        }
                    }
                }
            }
        }

        public void UpdateSchema()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                using (FileStream report = File.OpenWrite(@"C:\temp\KistlCodeGen\updateschemareport.log"))
                {
                    report.SetLength(0);
                    using (var mgr = new SchemaManagement.SchemaManager(ctx, report))
                    {
                        mgr.UpdateSchema();
                    }
                }
            }
        }

        public void UpdateSchema(string file)
        {
            using (IKistlContext ctx = new MemoryContext())
            {
                using (FileStream fs = File.OpenRead(file))
                {
                    Packaging.Importer.LoadFromXml(ctx, fs);

                    using (FileStream report = File.OpenWrite(@"C:\temp\KistlCodeGen\updateschemareport.log"))
                    {
                        report.SetLength(0);
                        using (var mgr = new SchemaManagement.SchemaManager(ctx, report))
                        {
                            mgr.UpdateSchema();
                        }
                    }
                }
            }
        }

        internal void RunFixes()
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
