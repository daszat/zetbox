using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Kistl.API.Client;

namespace Kistl.Client
{
    public class Client : MarshalByRefObject, Kistl.API.IKistlAppDomain
    {
        public Client()
        {
            Kistl.API.ObjectType.AsClient();
        }

        /// <summary>
        /// Das KistService
        /// </summary>
        private Kistl.API.IKistlAppDomain server = null;

        /// <summary>
        /// AppDomain, in der das KistService rennt.
        /// </summary>
        private AppDomain serverDomain = null;

        #region IKistlAppDomain Members

        public void Start()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Starting Client"))
            {
                if (Kistl.API.Configuration.KistlConfig.Current.Server.StartServer)
                {
                    // Create a new AppDomain for the Server!
                    // Damit trennt man den Server schön brav vom Client & kann 
                    // CustomActionsManagerFactory.Init zwei mal aufrufen :-)
                    serverDomain = AppDomain.CreateDomain("ServerAppDomain");

                    SplashScreen.SetInfo("Setting up Server");
                    Kistl.API.APIInit initServer = (Kistl.API.APIInit)serverDomain.CreateInstanceAndUnwrap("Kistl.API", "Kistl.API.APIInit");
                    initServer.Init(Kistl.API.Configuration.KistlConfig.Current.ConfigFilePath);

                    SplashScreen.SetInfo("Starting Server");
                    server = (Kistl.API.IKistlAppDomain)serverDomain.CreateInstanceAndUnwrap("Kistl.Server", "Kistl.Server.Server");

                    SplashScreen.SetInfo("Starting WCF Server");
                    server.Start();
                }

                SplashScreen.SetInfo("Setting up Client");
                API.CustomActionsManagerFactory.Init(new CustomActionsManagerClient());
            }
        }

        public void Stop()
        {
            if (server != null)
            {
                try
                {
                    server.Stop();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                    // TODO: Bad Hack, Do Something!
                    AppDomain.Unload(serverDomain);
                }
                server = null;
            }
        }

        #endregion
    }
}
