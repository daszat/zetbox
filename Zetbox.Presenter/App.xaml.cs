using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Zetbox.Client;

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
            try
            {
                client.Stop();
            }
            catch
            {
                // Pech gehabt
            }
        }

        public static bool ZetboxStarted = false;

        private void StartZetbox(object state)
        {
            try
            {
                Zetbox.API.APIInit init = new Zetbox.API.APIInit();
                init.Init();

                client = new Client();
                client.Start();

                ZetboxStarted = true;
            }
            catch
            {
                // Pech gehabt
            }
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(StartZetbox));
        }
    }
}
