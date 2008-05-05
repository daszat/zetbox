using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Globalization;

namespace Kistl.Client.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///  See http://dasz.at/index.php/2007/12/wpf-datetime-format/
        /// </summary>
        static App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        private void Application_Startup(object sender, StartupEventArgs e)
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

            using (TraceClient.TraceHelper.TraceMethodCall("Starting Client"))
            {
                SplashScreen.SetInfo("Setting up Client");
                Kistl.API.ObjectType.Init("Kistl.Objects.Client"); 
                API.CustomActionsManagerFactory.Init(new CustomActionsManagerClient());
            }
        }

    }
}
