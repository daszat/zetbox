using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;

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

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        #region AssemblyResolve
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // XML Serializers -> just ignore it...
            if (args.Name.ToLower().Contains(".xmlserializers")) return null;

            Trace.TraceInformation("Resolving Assembly {0}", args.Name);
            System.Reflection.Assembly result = null;

            try
            {
                result = System.Reflection.Assembly.LoadFrom(Helper.AssemblyPath + args.Name);
            }
            catch { }

            if (result == null)
            {
                // Try to load a .dll
                try
                {
                    result = System.Reflection.Assembly.LoadFrom(Helper.AssemblyPath + args.Name + ".dll");
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
