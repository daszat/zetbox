// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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
