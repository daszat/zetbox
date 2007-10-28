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
        static ServiceHost host = null;
        static Thread serviceThread = null;

        static void Main(string[] args)
        {
            StartServer();
            Console.WriteLine("Server started, press the anykey");
            Console.ReadLine();

            host.Close();
        }

        public static void StartServer()
        {
            serviceThread = new Thread(new ThreadStart(RunWCFServer));
            serviceThread.Start();
        }

        public static void StopServer()
        {
            host.Close();
        }

        public static void RunWCFServer()
        {
            Console.WriteLine("Starting Server...");

            Uri uri = new Uri("http://localhost:6666/KistlService");

            host = new ServiceHost(typeof(Kistl.Server.KistlService), uri);
            host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
            host.Faulted += new EventHandler(host_Faulted);

            host.Open();

            while (host.State == CommunicationState.Opened)
            {
                Thread.Sleep(100);
            }
        }

        static void host_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("Host faulted");
        }

        static void host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Console.WriteLine("UnknownMessageReceived: {0}", e.Message.ToString());
        }
    }
}
