using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Markup;
using System.Globalization;

namespace Kistl.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Client client;

        /// <summary>
        /// The magic lines
        /// </summary>
        static App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                        CultureInfo.CurrentCulture.IetfLanguageTag)));
        }


        /// <summary>
        /// Hochfahren der Applikation
        /// </summary>
        public App()
        {
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
            client.Stop();
        }

        /// <summary>
        /// Applikation hochfahren, KistServer instanzieren
        /// TODO: Je nach konfiguration (Single, Server/Client) wird das Service gestartet 
        /// oder eben nicht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 6);

            Kistl.API.APIInit init = new Kistl.API.APIInit();
            if (e.Args.Length > 0 && !e.Args[0].StartsWith("-"))
            {
                init.Init(e.Args[0]);
            }
            else
            {
                init.Init();
            }

            client = new Client();
            client.Start();
        }
    }
}
