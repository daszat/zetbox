using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

using Autofac;
using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.API.Utils;
using Kistl.App.GUI;
using Kistl.Client.Presentables;
using Kistl.App.Extensions;
using Kistl.Client.WPF.Converter;

namespace Kistl.Client.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current { get { return (App)(Application.Current); } }

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

        private static ServerDomainManager serverDomain;
        private static IContainer container;

        private string[] HandleCommandline(string[] args, out string configFilePath)
        {
            string[] result;

            if (args.Length == 1 && !args[0].StartsWith("-"))
            {
                configFilePath = args[0];
                result = new string[] { };
            }
            else
            {
                configFilePath = String.Empty;
                result = (string[])args.Clone();
            }
            return result;
        }

        private IContainer CreateMasterContainer(KistlConfig config)
        {
            var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);
            builder.RegisterType<Launcher>().SingleInstance();
            return builder.Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DebugConsole.Show();
            Logging.Configure();
            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 6);

            using (Logging.Log.InfoTraceMethodCall("Starting Client"))
            {
                string configFilePath;
                var args = HandleCommandline(e.Args, out configFilePath);

                var config = KistlConfig.FromFile(configFilePath);

                if (config.Server != null && config.Server.StartServer)
                {
                    SplashScreen.SetInfo("Starting Server");
                    serverDomain = new ServerDomainManager();
                    serverDomain.Start(config);
                }
                else
                {
                    SplashScreen.SetInfo("No server start required");
                }

                SplashScreen.SetInfo("Bootstraping Assembly Resolver");
                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                container = CreateMasterContainer(config);

                // initialise AppContext
                SplashScreen.SetInfo("Initializing Application Context");
                var appCtx = container.Resolve<ApplicationContext>();

                SplashScreen.SetInfo("Initializing Custom Actions Manager");
                // initialise custom actions manager
                var cams = container.Resolve<BaseCustomActionsManager>();

                SplashScreen.SetInfo("Initializing Launcher");

                // Init Resources
                this.Resources["IconConverter"] = new IconConverter(config.Client.DocumentStore);

                // delegate all business logic into another class, which 
                // allows us to load the Kistl.Objects assemblies _before_ 
                // they are needed.
                var launcher = container.Resolve<Launcher>();
                launcher.Show(args);
            }

            SplashScreen.HideSplashScreen();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (serverDomain != null)
                serverDomain.Stop();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
            e.Handled = true;
        }

#if DONOTUSE
        private static Assembly FetchOrCreateAssembly(
            IKistlContext ctx,
            Module guiModule,
            string aName)
        {
            Assembly result = ctx.GetQuery<Assembly>()
                .Where(a => a.Name == aName)
                .ToList()
                .SingleOrDefault();

            if (result == null)
            {
                result = ctx.Create<Assembly>();
                result.Name = aName;
                result.Module = guiModule;
            }

            return result;
        }
#endif
    }
}
