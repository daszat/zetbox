using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Configuration;

namespace Kistl.Client
{
    public sealed class ServerDomainManager : IDisposable
    {

        private IKistlAppDomain serverManager = null;
        private AppDomain serverDomain = null;
        private ClientSponsor clientSponsor;

        private bool unloadAppDomainOnShutDown = true;

        /// <summary>
        /// TODO: This is for NUnit...
        /// </summary>
        public void DisableUnloadAppDomainOnShutdown()
        {
            unloadAppDomainOnShutDown = false;
        }

        public void Start(KistlConfig config)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Starting AppDomain for Server"))
            {
                serverDomain = AppDomain.CreateDomain("ServerAppDomain",
                    AppDomain.CurrentDomain.Evidence,
                    AppDomain.CurrentDomain.SetupInformation);

                AssemblyLoader.Bootstrap(serverDomain, config);
                serverManager = (IKistlAppDomain)serverDomain.CreateInstanceAndUnwrap(
                    "Kistl.Server",
                    "Kistl.Server.Server");
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
            lock (typeof(ServerDomainManager))
            {
                if (serverManager != null)
                {
                    try
                    {
                        clientSponsor.Unregister(serverManager as MarshalByRefObject);
                        serverManager.Stop();
                        AssemblyLoader.Unload(serverDomain);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.TraceError(ex.ToString());
                    }
                }
                serverManager = null;

                if (serverDomain != null && unloadAppDomainOnShutDown)
                {
                    AppDomain.Unload(serverDomain);
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
