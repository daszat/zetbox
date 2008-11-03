using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using Kistl.API.Server;
using Kistl.API.Configuration;

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

                if (!serverStarted.WaitOne(20 * 1000, false))
                {
                    throw new InvalidOperationException("Server did not started within 20 sec.");
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
            GenerateCode();
            GenerateDatabase();
        }

        #region IDisposable Members
        // implement Dispose Pattern after 
        // http://msdn2.microsoft.com/en-us/library/ms244737.aspx
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (host != null)
                {
                    host.Close();
                    host = null;
                }
                if (hostStreams != null)
                {
                    hostStreams.Close();
                    hostStreams = null;
                }

                if (serverStarted != null)
                {
                    serverStarted.Close();
                    serverStarted = null;
                }
            }
        }

        #endregion
    }
}
