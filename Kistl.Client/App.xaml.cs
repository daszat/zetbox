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
        /// <summary>
        /// Hochfahren der Applikation
        /// </summary>
        public App()
        {
            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 3);
            API.CustomActionsManagerFactory.Init(new CustomActionsManagerClient());

            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        /// <summary>
        /// Programmende -> Server stoppen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Exit(object sender, ExitEventArgs e)
        {
            server.StopServer();
        }

        /// <summary>
        /// Das KistService
        /// </summary>
        private Kistl.Server.Server server = null;

        /// <summary>
        /// AppDomain, in der das KistService rennt.
        /// </summary>
        private AppDomain domain = null;

        /// <summary>
        /// Applikation hochfahren, KistServer instanzieren
        /// TODO: Je nach konfiguration (Single, Server/Client) wird das Service gestartet 
        /// oder eben nicht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen.SetInfo("Starting Server");
            // Create a new AppDomain for the Server!
            // Damit trennt man den Server schön brav vom Client & kann 
            // ObjectBrokerFactory.Init zwei mal aufrufen :-)
            domain = AppDomain.CreateDomain("ServerAppDomain");
            server = (Kistl.Server.Server)domain.CreateInstanceAndUnwrap("Kistl.Server", "Kistl.Server.Server");
            server.StartServer();
        }
    }
}
