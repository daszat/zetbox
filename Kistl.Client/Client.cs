using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;

using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client
{
    public class Client : MarshalByRefObject, Kistl.API.IKistlAppDomain
    {
        public Client()
        {
        }

        /// <summary>
        /// Das KistService
        /// </summary>
        private Kistl.API.IKistlAppDomain server = null;

        /// <summary>
        /// AppDomain, in der das KistService rennt.
        /// </summary>
        private AppDomain serverDomain = null;

        private ClientSponsor clientSponsor;

        #region IKistlAppDomain Members

        public void Start()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Starting Client"))
            {
                if (ApplicationContext.Current.Configuration.Server.StartServer)
                {
                /*
                    // Create a new AppDomain for the Server!
                    // Damit trennt man den Server schön brav vom Client & kann 
                    // CustomActionsManagerFactory.Init zwei mal aufrufen :-)
                    serverDomain = AppDomain.CreateDomain("ServerAppDomain", 
                        AppDomain.CurrentDomain.Evidence, 
                        AppDomain.CurrentDomain.SetupInformation);

//                    SplashScreen.SetInfo("Setting up Server");
                    Kistl.API.APIInit initServer = (Kistl.API.APIInit)serverDomain.CreateInstanceAndUnwrap("Kistl.API", "Kistl.API.APIInit");
                    initServer.Init(Kistl.API.HostType.Server, ApplicationContext.Current.Configuration.ConfigFilePath);

                    //                    SplashScreen.SetInfo("Starting Server");
                    server = (Kistl.API.IKistlAppDomain)serverDomain.CreateInstanceAndUnwrap("Kistl.Server", "Kistl.Server.Server");

                    clientSponsor = new ClientSponsor();
                    clientSponsor.RenewalTime = TimeSpan.FromMinutes(2);
                    clientSponsor.Register(server as MarshalByRefObject);

                    //                   SplashScreen.SetInfo("Starting WCF Server");
                    server.Start();
                 */
                    throw new NotImplementedException("TODO: re-enable integrated Server");
                }
            }
        }

        public void Stop()
        {
            if (server != null && !serverDomain.IsFinalizingForUnload())
            {
                try
                {
                    clientSponsor.Unregister(server as MarshalByRefObject);
                    server.Stop();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                }
                server = null;
            }

            if (serverDomain != null && !serverDomain.IsFinalizingForUnload())
            {
                AppDomain.Unload(serverDomain);
            }
        }

        #endregion
    }
}
