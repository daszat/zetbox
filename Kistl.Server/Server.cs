using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;

using Kistl.API.Configuration;
using Kistl.API.Server;

namespace Kistl.Server
{
    /// <summary>
    /// Serversteuerung
    /// </summary>
    public class Server : MarshalByRefObject, Kistl.API.IKistlAppDomain, IDisposable
    {
        /// <summary>
        /// WCF Service Host
        /// </summary>
        private ServiceHost host = null;
        private ServiceHost hostStreams = null;

        /// <summary>
        /// WCF Service Thread
        /// </summary>
        private Thread serviceThread = null;

        /// <summary>
        /// Nur zum signalisieren des Serverstarts.
        /// </summary>
        private AutoResetEvent serverStarted = new AutoResetEvent(false);

        private ServerApplicationContext appCtx;
        /// <summary>
        /// Server starten, Methode blockiert bis zum Serverstart. 
        /// Nach 20 sec. wird der start jedoch beendet.
        /// </summary>
        public void Start(KistlConfig config)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Starting Server"))
            {
                // re-use application context if available
                appCtx = ServerApplicationContext.Current ?? new ServerApplicationContext(config);

                serviceThread = new Thread(new ThreadStart(this.RunWCFServer));
                serviceThread.Start();

                if (!serverStarted.WaitOne(40 * 1000, false))
                {
                    throw new InvalidOperationException("Server did not started within 40 sec.");
                }
            }
        }

        /// <summary>
        /// Stops the Server
        /// </summary>
        public void Stop()
        {
            Trace.TraceInformation("Stopping Server");
            
            host.Close();
            hostStreams.Close();

            if (!serviceThread.Join(5000))
            {
                Trace.TraceInformation("Server did not stopped, aborting");
                serviceThread.Abort();
            }
            serviceThread = null;
            serverStarted.Close();

            Trace.TraceInformation("Server stopped");
        }

        public const string DefaultServiceUrl = "http://localhost:6666/KistlService";
        public const string DefaultStreamsUrl = "http://localhost:6666/KistlServiceStreams";
        /// <summary>
        /// Führt den eigentlichen WCF Host start asynchron durch und 
        /// wartet bis er wieder gestopped wird.
        /// </summary>
        private void RunWCFServer()
        {
            try
            {
                using (TraceClient.TraceHelper.TraceMethodCall("Starting WCF Server"))
                {
                    host = new ServiceHost(typeof(Kistl.Server.KistlService),
                        new Uri(String.IsNullOrEmpty(appCtx.Configuration.ServiceUrl) ? DefaultServiceUrl : appCtx.Configuration.ServiceUrl));
                    host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
                    host.Faulted += new EventHandler(host_Faulted);

                    host.Open();

                    hostStreams = new ServiceHost(typeof(Kistl.Server.KistlServiceStreams),
                        new Uri(String.IsNullOrEmpty(appCtx.Configuration.StreamsUrl) ? DefaultStreamsUrl : appCtx.Configuration.StreamsUrl));
                    hostStreams.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
                    hostStreams.Faulted += new EventHandler(host_Faulted);

                    hostStreams.Open();

                    serverStarted.Set();
                }

                Trace.TraceInformation("WCF Server started");

                while (host.State == CommunicationState.Opened)
                {
                    Thread.Sleep(100);
                }

                Trace.TraceInformation("WCF Server: Hosts closed, exiting WCF thread");
            }
            catch (Exception error)
            {
                Trace.TraceError("Unhandled exception ({0}) while running the WCF Server: {1}", error.GetType().Name, error.Message);
                Trace.TraceError(error.ToString());
                Trace.TraceError(error.StackTrace);

                throw error;
            }
        }

        private void host_Faulted(object sender, EventArgs e)
        {
            Trace.TraceWarning("Host faulted");
        }

        private void host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Trace.TraceWarning("UnknownMessageReceived: {0}", e.Message.ToString());
        }

        internal void GenerateCode()
        {
            Generators.Generator.GenerateCode();
        }

        internal void GenerateDatabase()
        {
            Generators.Generator.GenerateDatabase();
        }

        internal void GenerateAll()
        {
            Generators.Generator.GenerateAll();
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
