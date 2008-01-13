using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Kistl.Client;

namespace WPFPresenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Client client;

        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            client.Stop();            
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 6);
            Kistl.API.APIInit init = new Kistl.API.APIInit();
            init.Init();

            client = new Client();
            client.Start();

            SplashScreen.HideSplashScreen();
        }
    }
}
