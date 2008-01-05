using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace Kistl.Server
{
    /// <summary>
    /// Serversteuerung
    /// </summary>
    public class Server : MarshalByRefObject, Kistl.API.IKistlAppDomain
    {
        /// <summary>
        /// WCF Service Host
        /// </summary>
        private ServiceHost host = null;

        /// <summary>
        /// WCF Service Thread
        /// </summary>
        private Thread serviceThread = null;

        /// <summary>
        /// Nur zum signalisieren des Serverstarts.
        /// </summary>
        private AutoResetEvent serverStarted = new AutoResetEvent(false);

        /// <summary>
        /// Constructor
        /// </summary>
        public Server()
        {
            API.CustomActionsManagerFactory.Init(new CustomActionsManagerServer());

            // Preload Kistl.Objects.Server.dll so the Mapping Resources will be loaded
            // Console.WriteLine(typeof(Kistl.App.Base.ObjectClass).FullName);
            Kistl.API.AssemblyLoader.Load("Kistl.Objects.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            Kistl.API.AssemblyLoader.ReflectionOnlyLoadFrom("Kistl.Objects.Server");
        }

        /// <summary>
        /// Server starten, Methode blockiert bis zum Serverstart. 
        /// Nach 20 sec. wird der start jedoch beendet.
        /// </summary>
        public void Start()
        {
            Trace.TraceInformation("Starting Server");

            serviceThread = new Thread(new ThreadStart(this.RunWCFServer));
            serviceThread.Start();

            if (!serverStarted.WaitOne(20 * 1000, false))
            {
                throw new ApplicationException("Server did not started within 20 sec.");
            }
            Trace.TraceInformation("Server started");
        }

        /// <summary>
        /// Stopped den Server
        /// </summary>
        public void Stop()
        {
            Trace.TraceInformation("Stopping Server");
            host.Close();
            Trace.TraceInformation("Server stopped");
        }

        /// <summary>
        /// Führt den eigentlichen WCF Host start asynchron durch und 
        /// wartet bis er wieder gestopped wird.
        /// </summary>
        private void RunWCFServer()
        {
            Trace.TraceInformation("Starting WCF Server...");

            Uri uri = new Uri("http://localhost:6666/KistlService");

            host = new ServiceHost(typeof(Kistl.Server.KistlService), uri);
            host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
            host.Faulted += new EventHandler(host_Faulted);

            host.Open();

            serverStarted.Set();

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
            Trace.TraceInformation("Generating Code...");
            Generators.Generator.GenerateCode();
            Trace.TraceInformation("Generating Code finished!");
        }

        internal void GenerateDatabase()
        {
            Trace.TraceInformation("Generating Database...");
            Generators.Generator.GenerateDatabase();
            Trace.TraceInformation("Generating Database finished!");
        }

        internal void GenerateAll()
        {
            GenerateCode();
            GenerateDatabase();
        }
    }
}
