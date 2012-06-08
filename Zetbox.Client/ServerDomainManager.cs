
namespace Zetbox.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Lifetime;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;

    /// <summary>
    /// Delegates the <see cref="IZetboxAppDomain"/> interface into a new AppDomain.
    /// </summary>
    public sealed class ServerDomainManager
        : IZetboxAppDomain, IDisposable
    {
        private readonly static object _lock = new object();

        private IZetboxAppDomain serverManager = null;
        private AppDomain serverDomain = null;
        private ClientSponsor clientSponsor;

        public void Start(ZetboxConfig config)
        {
            using (Logging.Log.DebugTraceMethodCall("Start", "Starting AppDomain for Server"))
            {
                serverDomain = AppDomain.CreateDomain("ServerAppDomain",
                    AppDomain.CurrentDomain.Evidence,
                    AppDomain.CurrentDomain.SetupInformation);

                AssemblyLoader.Bootstrap(serverDomain, config);

                serverManager = (IZetboxAppDomain)serverDomain.CreateInstanceAndUnwrap(
                    "Zetbox.Server.Service",
                    "Zetbox.Server.Service.ServerManager");
                serverManager.Start(config);

                if (clientSponsor == null)
                {
                    clientSponsor = new ClientSponsor();
                    clientSponsor.RenewalTime = TimeSpan.FromMinutes(2);
                }

                clientSponsor.Register(serverManager as MarshalByRefObject);
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (serverManager != null)
                {
                    try
                    {
                        clientSponsor.Unregister(serverManager as MarshalByRefObject);
                        Logging.Log.Info("Closing client sponsor");
                        clientSponsor.Close();
                        Logging.Log.Info("Stopping server manager");
                        serverManager.Stop();
                        Logging.Log.Info("Unloading AssemblyLoader from server app domain");
                        AssemblyLoader.Unload(serverDomain);
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Warn("Error during shutdown", ex);
                        throw;
                    }
                }
                else
                {
                    Logging.Log.Error("Tried unloading an already unloaded server manager.");
                }
                
                clientSponsor = null;
                serverManager = null;

                if (serverDomain != null)
                {
                    Logging.Log.Info("Unloading server app domain");
                    AppDomain.Unload(serverDomain);
                }
                else
                {
                    Logging.Log.Warn("Server app domain already vanished.");
                }

                serverDomain = null;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}
