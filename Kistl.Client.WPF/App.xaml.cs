using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Globalization;
using Kistl.GUI.DB;

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
            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 5);

            using (TraceClient.TraceHelper.TraceMethodCall("Starting Client"))
            {
                Manager.Create(e.Args, Toolkit.WPF);
            }
        }

    }
}
