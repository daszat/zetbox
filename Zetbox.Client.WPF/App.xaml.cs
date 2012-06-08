
namespace Zetbox.Client.WPF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Threading;
    using Autofac;
    using Autofac.Features.Metadata;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.WPF.Converter;
    using Zetbox.Client.WPF.Toolkit;
    using Microsoft.Samples.KMoore.WPFSamples.InfoTextBox;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static new App Current { get { return (App)(System.Windows.Application.Current); } }

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
        private static bool wpfResourcesInitialized = false;

        private string[] HandleCommandline(string[] args, out string configFilePath)
        {
            string[] result;

            if (args.Length >= 1)
            {
                configFilePath = args[0];
                result = args.Skip(1).ToArray();
            }
            else
            {
                configFilePath = String.Empty;
                result = (string[])args.Clone();
            }
            return result;
        }

        private IContainer CreateMasterContainer(ZetboxConfig config)
        {
            var builder = Zetbox.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);

            builder
                .RegisterType<Launcher>()
                .SingleInstance();

            builder
                .Register<Zetbox.Client.WPF.Toolkit.VisualTypeTemplateSelector>((c, p) => new Zetbox.Client.WPF.Toolkit.VisualTypeTemplateSelector(
                    p.Named<object>("requestedKind"),
                    c.Resolve<IFrozenContext>()))
                .InstancePerDependency();

            return builder.Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ShowDebugConsole"] == "true")
                {
                    DebugConsole.Show();
                }
                Logging.Configure();

                using (Logging.Log.InfoTraceMethodCall("Starting Client"))
                {
                    string configFilePath;
                    var args = HandleCommandline(e.Args, out configFilePath);

                    var config = ZetboxConfig.FromFile(configFilePath, "Zetbox.Client.WPF.xml");
                    AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                    InitCulture(config);
                    StartupScreen.ShowSplashScreen(Zetbox.Client.Properties.Resources.Startup_Message, Zetbox.Client.Properties.Resources.Startup_InitApp, 6);

                    InitializeClient(args, config);
                }
            }
            catch (Exception ex)
            {
                ShowExceptionReporter(ex);

                // unable to start, exit
                System.Environment.Exit(1);
            }

            // The WPFToolkit library is not translated and does not support changeing the DateTimePickerTextbox.Watermark.
            // Therefore, we have to replace the underlying ResourceManager.
            try
            {
                var srType = typeof(Microsoft.Windows.Controls.DatePicker).Assembly.GetTypes().Single(t => t.Name == "SR");
                var resourceManagerField = srType.GetField("_resourceManager", BindingFlags.Static | BindingFlags.NonPublic);
                resourceManagerField.SetValue(null, WpfToolkitResources.ResourceManager);
            }
            catch (Exception /* ex */)
            {
                // ignore this
                //ShowExceptionReporter(ex);
            }
        }

        // Move to another method to avoid loading Zetbox.Objects
        private void InitializeClient(string[] args, ZetboxConfig config)
        {
            if (config.Server != null && config.Server.StartServer)
            {
                StartupScreen.SetInfo(Zetbox.Client.Properties.Resources.Startup_Server);
                serverDomain = new ServerDomainManager();
                serverDomain.Start(config);
            }
            else
            {
                StartupScreen.SetInfo(Zetbox.Client.Properties.Resources.Startup_NoServerStart);
            }


            container = CreateMasterContainer(config);

            StartupScreen.SetInfo(Zetbox.Client.Properties.Resources.Startup_Launcher);

            // Make Gendarme happy
            var resources = this.Resources;

            resources.BeginInit();

            // Create icon converter
            var iconConverter = new IconConverter(container.Resolve<IFrozenContext>(), container.Resolve<Func<IZetboxContext>>());
            resources["IconConverter"] = iconConverter;
            resources["ImageCtrlConverter"] = new ImageCtrlConverter(iconConverter);

            // Init all Converter that are not using a Context
            var templateSelectorFactory = container.Resolve<Zetbox.Client.WPF.Toolkit.VisualTypeTemplateSelector.Factory>();
            resources["defaultTemplateSelector"] = templateSelectorFactory(null);
            resources["listItemTemplateSelector"] = templateSelectorFactory("Zetbox.App.GUI.SingleLineKind");
            resources["dashBoardTemplateSelector"] = templateSelectorFactory("Zetbox.App.GUI.DashboardKind");

            // Manually add DefaultStyles and DefaultViews
            // Otherwise converter are unknown
            resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Zetbox.Client.WPF;component/Styles/DefaultStyles.xaml", UriKind.Relative) });
            resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Zetbox.Client.WPF;component/Styles/DefaultHighlightColorDefinitions.xaml", UriKind.Relative) });

            // Load registrated dictionaries from autofac
            foreach (var dict in container.Resolve<IEnumerable<Meta<ResourceDictionary>>>().Where(m => WPFHelper.RESOURCE_DICTIONARY_STYLE.Equals(m.Metadata[WPFHelper.RESOURCE_DICTIONARY_KIND])).Select(m => m.Value))
            {
                resources.MergedDictionaries.Add(dict);
            }

            resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Zetbox.Client.WPF;component/Styles/DefaultViews.xaml", UriKind.Relative) });
            // Load registrated dictionaries from autofac
            foreach (var dict in container.Resolve<IEnumerable<Meta<ResourceDictionary>>>().Where(m => WPFHelper.RESOURCE_DICTIONARY_VIEW.Equals(m.Metadata[WPFHelper.RESOURCE_DICTIONARY_KIND])).Select(m => m.Value))
            {
                resources.MergedDictionaries.Add(dict);
            }

            resources.EndInit();

            // Init credentials explicit
            StartupScreen.SetInfo(Zetbox.Client.Properties.Resources.Startup_EnsuringCredentials);
            container.Resolve<ICredentialsResolver>().EnsureCredentials();

            StartupScreen.SetInfo(Zetbox.Client.Properties.Resources.Startup_Launcher);

            // Tell icon converter that everything is initialized
            iconConverter.Initialized();

            // Focus nightmare
            // http://stackoverflow.com/questions/673536/wpf-cant-set-focus-to-a-child-of-usercontrol/4785124#4785124
            EventManager.RegisterClassHandler(typeof(Window), Window.LoadedEvent, new RoutedEventHandler(FocusFixLoaded));
            EventManager.RegisterClassHandler(typeof(Zetbox.Client.WPF.View.ZetboxBase.InstanceCollectionBase), UserControl.LoadedEvent, new RoutedEventHandler(FocusFixLoaded));

            wpfResourcesInitialized = true;

            FixupDatabase(container.Resolve<Func<IZetboxContext>>());

            IServiceControlManager scm = null;
            if (container.TryResolve<IServiceControlManager>(out scm))
            {
                Logging.Log.Info("Starting Zetbox Services");
                scm.Start();
            }
            else
            {
                Logging.Log.Info("Service control manager not registered");
            }

            StartupScreen.CanCloseOnWindowLoaded();
            // delegate all business logic into another class, which 
            // allows us to load the Zetbox.Objects assemblies _before_ 
            // they are needed.
            var launcher = container.Resolve<Launcher>();
            launcher.Show(args);
        }

        // Focus nightmare
        // http://stackoverflow.com/questions/673536/wpf-cant-set-focus-to-a-child-of-usercontrol/4785124#4785124
        void FocusFixLoaded(object sender, RoutedEventArgs e)
        {
            var element = e.Source as FrameworkElement;
            element.Dispatcher.Invoke(new Action(() =>
            {
                var firstTxt = element.FindVisualChild<InfoTextBox>();
                if (firstTxt != null)
                {
                    Keyboard.Focus(firstTxt);
                }
            }), DispatcherPriority.ApplicationIdle);
        }

        private static void InitCulture(ZetboxConfig config)
        {
            if (config.Client == null) return;
            if (!string.IsNullOrEmpty(config.Client.Culture))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(config.Client.Culture);
            }
            if (!string.IsNullOrEmpty(config.Client.UICulture))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(config.Client.UICulture);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Logging.Log.Info("Stopping Zetbox Services");
            IServiceControlManager scm = null;
            if (container.TryResolve<IServiceControlManager>(out scm))
            {
                scm.Stop();
            }
            else
            {
                Logging.Log.Info("Service control manager not registered");
            }

            if (serverDomain != null)
                serverDomain.Stop();

            try
            {
                if (container != null)
                    container.Dispose();
            }
            catch (Exception ex)
            {
                // A WCF Proxy may throw an exception while shutting down when the server is not available - WTF?
                Logging.Log.Error("Application_Exit", ex);
            }
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var inner = e.Exception.GetInnerException();
            if (inner is Zetbox.API.Common.UnresolvableIdentityException)
            {
                Environment.Exit(1);
            }
            else
            {
                ShowExceptionReporter(e.Exception);
                e.Handled = true;
            }
        }

        private static void ShowExceptionReporter(Exception ex)
        {
            var inner = ex.GetInnerException();
            Logging.Client.Error("Unhandled Exception", inner);
            if (inner is InvalidZetboxGeneratedVersionException)
            {
                MessageBox.Show(
                    WpfToolkitResources.InvalidZetboxGeneratedVersionException_Message,
                    WpfToolkitResources.InvalidZetboxGeneratedVersionException_Title,
                    MessageBoxButton.OK,
                    MessageBoxImage.Stop);
            }
            else if (wpfResourcesInitialized && container != null)
            {
                var vmf = container.Resolve<IViewModelFactory>();
                var mdl = vmf.CreateViewModel<ExceptionReporterViewModel.Factory>().Invoke(container.Resolve<IZetboxContext>(), null, ex, container.Resolve<IScreenshotTool>().GetScreenshot());
                vmf.ShowDialog(mdl);
            }
            else
            {
                MessageBox.Show(ex.ToString());
            }
        }

#if DONOTUSE
        private static Assembly FetchOrCreateAssembly(
            IZetboxContext ctx,
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
