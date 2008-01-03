using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Diagnostics;

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
            if (args.Length > 0)
            {
                try
                {
                    // Start Server
                    API.CustomActionsManagerFactory.Init(new CustomActionsManagerServer());

                    bool actiondone = false;
                    if (!string.IsNullOrEmpty(args.FirstOrDefault(a => a.Contains("generate"))))
                    {
                        actiondone = true;
                        server.GenerateCode();
                    }
                    if (!string.IsNullOrEmpty(args.FirstOrDefault(a => a.Contains("database"))))
                    {
                        actiondone = true;
                        server.GenerateDatabase();
                    }

                    if(!actiondone)
                    {
                        Console.WriteLine("Usage: Kistl.Server [options]");
                        Console.WriteLine("  With no options: Starts the server");
                        Console.WriteLine("  -generate: Generates Objects & EF-Model");
                        Console.WriteLine("  -database: Updates the Database");
                        Console.WriteLine("  -?: Displays this help");
                    }
                }
                catch (Exception ex)
                {
                    Helper.HandleError(ex);
                }

                Console.WriteLine("Hit the anykey to exit");
                Console.ReadKey();
            }
            else
            {
                server.StartServer();
                Console.WriteLine("Server started, press the anykey to exit");
                Console.ReadKey();

                server.StopServer();
            }
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
        /// Constructor
        /// </summary>
        public Server()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        #region AssemblyResolve
        /// <summary>
        /// TODO: Das muss geändert werden. 
        /// Siehe http://blogs.msdn.com/suzcook/archive/2003/05/29/57143.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // XML Serializers -> just ignore it...
            if (args.Name.ToLower().Contains(".xmlserializers")) return null;

            Trace.TraceInformation("Resolving Assembly {0}", args.Name);
            System.Reflection.Assembly result = null;

            try
            {
                result = System.Reflection.Assembly.LoadFrom(Helper.CodeGenPath + @"\bin\" + args.Name);
            }
            catch { }

            if (result == null)
            {
                // Try to load a .dll
                try
                {
                    result = System.Reflection.Assembly.LoadFrom(Helper.CodeGenPath + @"\bin\" + args.Name + ".dll");
                }
                catch { }
            }
            if (result == null)
            {
                Trace.TraceWarning("Unable to load Assembly {0}", args.Name);
            }

            return result;
        }
        #endregion

        /// <summary>
        /// Server starten, Methode blockiert bis zum Serverstart. 
        /// Nach 20 sec. wird der start jedoch beendet.
        /// </summary>
        public void StartServer()
        {
            Trace.TraceInformation("Starting Server");
            API.CustomActionsManagerFactory.Init(new CustomActionsManagerServer());

            // Preload Kistl.Objects.Server.dll so the Mapping Resources will be loaded
            Console.WriteLine(typeof(Kistl.App.Base.ObjectClass).FullName);
            
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
        public void StopServer()
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
