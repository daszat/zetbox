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
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            Kistl.Server.Program.StopServer();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            Kistl.Server.Program.StartServer();
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
