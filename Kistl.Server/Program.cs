using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace Kistl.Server
{
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

    public class Server : MarshalByRefObject
    {
        private ServiceHost host = null;
        private Thread serviceThread = null;
        private AutoResetEvent serverStarted = new AutoResetEvent(false);

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

        public void StopServer()
        {
            host.Close();
        }

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
