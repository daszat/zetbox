using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Kistl.Client
{
    public class Client : MarshalByRefObject, Kistl.API.IKistlAppDomain
    {
        public Client()
        {
        }

        Application app = new Application();
        
        #region IKistlAppDomain Members

        public void Start()
        {
            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 3);
            API.CustomActionsManagerFactory.Init(new CustomActionsManagerClient());

            MainWindow wnd = new MainWindow();
            wnd.Closed += new EventHandler(wnd_Closed);
            
            app.Run(wnd);
        }

        void wnd_Closed(object sender, EventArgs e)
        {
            app.Shutdown();
        }

        public void Stop()
        {
        }

        #endregion
    }
}
