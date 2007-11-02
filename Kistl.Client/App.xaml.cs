using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Kistl.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            API.ObjectBrokerFactory.Init(new ObjectBrokerClient());

            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            server.StopServer();
        }

        private Kistl.Server.Server server = null;
        private AppDomain domain = null;

        void App_Startup(object sender, StartupEventArgs e)
        {
            // Create a new AppDomain for the Server!
            // Damit trennt man den Server schön brav vom Client & kann 
            // ObjectBrokerFactory.Init zwei mal aufrufen :-)
            domain = AppDomain.CreateDomain("ServerAppDomain");
            server = (Kistl.Server.Server)domain.CreateInstanceAndUnwrap("Kistl.Server", "Kistl.Server.Server");
            server.StartServer();
        }

        private static KistService.KistlServiceClient service = new Kistl.Client.KistService.KistlServiceClient();

        public static KistService.KistlServiceClient Service
        {
            get
            {
                return service;
            }
        }
    }
}
