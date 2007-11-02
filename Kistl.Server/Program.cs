using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace Kistl.Server
{
    /// <summary>
    /// Hauptprogramm
    /// </summary>
    public class Program
    {
        static Server server = new Server();

        static void Main(string[] args)
        {
            server.StartServer();
            Console.WriteLine("Server started, press the anykey");
            Console.ReadLine();

            server.StopServer();
        }
    }

    /// <summary>
    /// Serversteuerung
    /// </summary>
    public class Server : MarshalByRefObject
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
        /// Server starten, Methode blockiert bis zum Serverstart. 
        /// Nach 20 sec. wird der start jedoch beendet.
        /// </summary>
        public void StartServer()
        {
            API.ObjectBrokerFactory.Init(new ObjectBrokerServer());

            serviceThread = new Thread(new ThreadStart(this.RunWCFServer));
            serviceThread.Start();

            if (!serverStarted.WaitOne(20 * 1000, false))
            {
                throw new ApplicationException("Server did not started within 20 sec.");
            }
        }

        /// <summary>
        /// Stopped den Server
        /// </summary>
        public void StopServer()
        {
            host.Close();
        }

        /// <summary>
        /// Führt den eigentlichen WCF Host start asynchron durch und 
        /// wartet bis er wieder gestopped wird.
        /// </summary>
        private void RunWCFServer()
        {
            Console.WriteLine("Starting Server...");

            Uri uri = new Uri("http://localhost:6666/KistlService");

            host = new ServiceHost(typeof(Kistl.Server.KistlService), uri);
            host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
            host.Faulted += new EventHandler(host_Faulted);

            host.Open();

            serverStarted.Set();

            Console.WriteLine("Server started");

            while (host.State == CommunicationState.Opened)
            {
                Thread.Sleep(100);
            }
        }

        private void host_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("Host faulted");
        }

        private void host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Console.WriteLine("UnknownMessageReceived: {0}", e.Message.ToString());
        }
    }
}
