
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading;

    using Autofac.Integration.Wcf;

    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;

    public class WcfServer
        : MarshalByRefObject, IKistlAppDomain, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Wcf");

        /// <summary>
        /// WCF Service Host
        /// </summary>
        private readonly ServiceHostBase _host = null;

        /// <summary>
        /// WCF Service Thread
        /// </summary>
        private Thread serviceThread = null;

        /// <summary>
        /// Only to signal the server start
        /// </summary>
        private AutoResetEvent serverStarted = new AutoResetEvent(false);

        public WcfServer(AutofacServiceHostFactory factory)
        {
            _host = factory.CreateServiceHost(typeof(KistlService).AssemblyQualifiedName, new[] { new Uri("http://localhost:6666/KistlService") });
            _host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
            _host.Faulted += new EventHandler(host_Faulted);
        }

        /// <summary>
        /// Starts the WCF Server in the background. If the server hasn't 
        /// started successfully within 40 seconds, it is aborted and an 
        /// <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <param name="config">the loaded configuration for the Server</param>
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
        /// <param name="config">the loaded configuration for the Server</param>
        public void Init(KistlConfig config)
        {
            ServerApplicationContext appCtx;
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

            _host.Close();

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
        /// Executes the actual WcfHost in a separate thread and waits until shutdown.
        /// </summary>
        private void RunWCFServer()
        {
            try
            {
                using (Log.DebugTraceMethodCall("Starting WCF Server"))
                {
                    _host.Open();
                    serverStarted.Set();
                }

                Log.Info("WCF Server started");

                while (_host.State == CommunicationState.Opened)
                {
                    Thread.Sleep(100);
                }

                Log.Info("WCF Server: Hosts closed, exiting WCF thread");
            }
            catch (Exception error)
            {
                Log.Error("Unhandled exception while running WCF Server", error);
                throw;
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

        #region IDisposable Members

        // TODO: implement Dispose Pattern after 
        // http://msdn2.microsoft.com/en-us/library/ms244737.aspx
        public void Dispose()
        {
            Log.Info("Disposing WcfServer");

            if (_host != null && _host.State != CommunicationState.Closed)
            {
                _host.Close();
                ((IDisposable)_host).Dispose();
            }

            if (serverStarted != null)
            {
                serverStarted.Close();
                ((IDisposable)serverStarted).Dispose();
                serverStarted = null;
            }
        }

        #endregion
    }
}
