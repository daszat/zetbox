
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Text;
    using System.Threading;
    using Autofac.Integration.Wcf;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using System.ServiceModel.Web;

    public class WcfServer
        : MarshalByRefObject, IKistlAppDomain, IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Wcf");

        /// <summary>
        /// WCF Service Host
        /// </summary>
        private readonly ServiceHostBase _mainHost = null;

        /// <summary>
        /// Bootstrapper WCF Service Host
        /// </summary>
        private readonly ServiceHostBase _bootstrapperHost = null;

        /// <summary>
        /// WCF Service Thread
        /// </summary>
        private Thread serviceThread = null;

        /// <summary>
        /// Only to signal the server start
        /// </summary>
        private AutoResetEvent serverStarted = new AutoResetEvent(false);

        public WcfServer(AutofacServiceHostFactory factory, AutofacWebServiceHostFactory webFactory, KistlConfig config)
        {
            if (factory == null) { throw new ArgumentNullException("factory"); }
            if (webFactory == null) { throw new ArgumentNullException("webFactory"); }
            if (config == null) throw new ArgumentNullException("config");

            _mainHost = factory.CreateServiceHost(typeof(KistlService).AssemblyQualifiedName, new Uri[] { });
            _mainHost.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(host_UnknownMessageReceived);
            _mainHost.Faulted += host_Faulted;
            _mainHost.Closed += host_Closed;
            _mainHost.Opened += host_Opened;

            //_bootstrapperHost = webFactory.CreateServiceHost(typeof(BootstrapperService).AssemblyQualifiedName, new Uri[] { });
            _bootstrapperHost = new WebServiceHost(new BootstrapperService(config));
        }

        void host_Opened(object sender, EventArgs e)
        {
            Log.Info("Host opened");
        }

        void host_Closed(object sender, EventArgs e)
        {
            Log.Info("Host closed");
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
                serviceThread = new Thread(new ThreadStart(this.RunWCFServer));
                serviceThread.Start();

                if (!serverStarted.WaitOne(40 * 1000, false))
                {
                    throw new InvalidOperationException("Server did not start within 40 sec.");
                }
            }
        }

        /// <summary>
        /// Stops the WCF Server.
        /// </summary>
        public void Stop()
        {
            Log.Info("Stopping Server");

            _mainHost.Close();
            _bootstrapperHost.Close();

            if (!serviceThread.Join(5000))
            {
                Log.Info("Server did not stop after 5s, aborting");
                serviceThread.Abort();
            }
            serviceThread = null;

            // Work around AppDomainUnloadedException due to Race with WCF's _host.Close()
            // See https://bugs.launchpad.net/nunitv2/+bug/423611
            Log.Info("Waiting for WCF to disappear");
            Thread.Sleep(3000);

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
                    _mainHost.Open();
                    _bootstrapperHost.Open();
                    serverStarted.Set();
                }

                Log.Info("WCF Server started");

                while (_mainHost.State == CommunicationState.Opened)
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

            if (_mainHost != null && _mainHost.State != CommunicationState.Closed)
            {
                _mainHost.Close();
                ((IDisposable)_mainHost).Dispose();
            }

            if (_bootstrapperHost != null && _bootstrapperHost.State != CommunicationState.Closed)
            {
                _bootstrapperHost.Close();
                ((IDisposable)_bootstrapperHost).Dispose();
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
