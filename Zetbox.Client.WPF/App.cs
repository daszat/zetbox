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
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Autofac;
    using Autofac.Features.Metadata;
    using Microsoft.Samples.KMoore.WPFSamples.InfoTextBox;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.WPF.Converter;
    using Zetbox.Client.WPF.Toolkit;

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

        public App()
        {
            this.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            this.DispatcherUnhandledException += Application_DispatcherUnhandledException;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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

                    var config = ZetboxConfig.FromFile(configFilePath, GetConfigFileName());
                    AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                    InitCulture(config);
                    InitializeClient(args, config);
                }
            }
            catch (Exception ex)
            {
                ShowExceptionReporter(ex);

                // unable to start, exit
                System.Environment.Exit(1);
            }

            EventManager.RegisterClassHandler(typeof(DatePicker),
                DatePicker.LoadedEvent,
                new RoutedEventHandler(DatePicker_Loaded));
        }

        void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null) return;

            var tb = WPFHelper.FindVisualChild<DatePickerTextBox>(dp);
            if (tb == null) return;

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;

            wm.Content = WpfToolkitResources.DatePicker_WatermarkText;
        }

        protected virtual string GetConfigFileName()
        {
            return "Zetbox.WPF.xml";
        }

        // Move to another method to avoid loading Zetbox.Objects
        private void InitializeClient(string[] args, ZetboxConfig config)
        {
            InitializeSplashScreenImageResource();

            StartupScreen.ShowSplashScreen(Zetbox.Client.Properties.Resources.Startup_Message, Zetbox.Client.Properties.Resources.Startup_InitApp, 6);
            if (config.Server != null && config.Server.StartServer)
            {
                StartupScreen.SetInfo(Zetbox.Client.Properties.Resources.Startup_Server);
                serverDomain = new ServerDomainManager();
                serverDomain.Start(config);
            }

            container = CreateMasterContainer(config);

            StartupScreen.SetInfo(Zetbox.Client.Properties.Resources.Startup_Launcher);

            //SplashScreen.DispatchAndWait(() => LoadStyles(SplashScreen.Resources));

            var iconConverter = LoadStyles(this.Resources);

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

        private IconConverter LoadStyles(ResourceDictionary targetResources)
        {
            targetResources.BeginInit();
            targetResources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Zetbox.Client.WPF;component/AppResources.xaml", UriKind.Relative) });

            // Create icon converter
            var iconConverter = new IconConverter(container.Resolve<IFrozenContext>(), container.Resolve<Func<IZetboxContext>>());
            targetResources["IconConverter"] = iconConverter;
            targetResources["ImageCtrlConverter"] = new ImageCtrlConverter(iconConverter);

            // Init all Converter that are not using a Context
            var templateSelectorFactory = container.Resolve<Zetbox.Client.WPF.Toolkit.VisualTypeTemplateSelector.Factory>();
            targetResources["defaultTemplateSelector"] = templateSelectorFactory(null);
            targetResources["listItemTemplateSelector"] = templateSelectorFactory("Zetbox.App.GUI.SingleLineKind");
            targetResources["dashBoardTemplateSelector"] = templateSelectorFactory("Zetbox.App.GUI.DashboardKind");

            // Manually add DefaultStyles and DefaultViews
            // Otherwise converter are unknown
            targetResources.MergedDictionaries.Add(Freeze(new ResourceDictionary() { Source = new Uri("/Zetbox.Client.WPF;component/Styles/DefaultStyles.xaml", UriKind.Relative) }));
            targetResources.MergedDictionaries.Add(Freeze(new ResourceDictionary() { Source = new Uri("/Zetbox.Client.WPF;component/Styles/DefaultHighlightColorDefinitions.xaml", UriKind.Relative) }));

            // Load registrated dictionaries from autofac
            foreach (var dict in container.Resolve<IEnumerable<Meta<ResourceDictionary>>>().Where(m => WPFHelper.RESOURCE_DICTIONARY_STYLE.Equals(m.Metadata[WPFHelper.RESOURCE_DICTIONARY_KIND])).Select(m => m.Value))
            {
                targetResources.MergedDictionaries.Add(Freeze(dict));
            }

            targetResources.MergedDictionaries.Add(Freeze(new ResourceDictionary() { Source = new Uri("/Zetbox.Client.WPF;component/Styles/DefaultViews.xaml", UriKind.Relative) }));
            // Load registrated dictionaries from autofac
            foreach (var dict in container.Resolve<IEnumerable<Meta<ResourceDictionary>>>().Where(m => WPFHelper.RESOURCE_DICTIONARY_VIEW.Equals(m.Metadata[WPFHelper.RESOURCE_DICTIONARY_KIND])).Select(m => m.Value))
            {
                targetResources.MergedDictionaries.Add(Freeze(dict));
            }

            targetResources.EndInit();
            return iconConverter;
        }

        private static ResourceDictionary Freeze(ResourceDictionary dict)
        {
            foreach (var i in dict.OfType<Freezable>())
                i.Freeze();
            return dict;
        }

        protected virtual void InitializeSplashScreenImageResource()
        {
            if (!this.Resources.Contains("SplashScreenImage"))
            {
                this.Resources["SplashScreenImage"] = new ImageBrush() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Zetbox.Client.WPF;component/SplashScreenImage.png")) };
            }
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

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

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
