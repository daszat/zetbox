
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.Converter;
    using System.Windows.Input;
    using System.Windows.Controls;
    using Kistl.Client.WPF.Toolkit;
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

        private IContainer CreateMasterContainer(KistlConfig config)
        {
            var builder = Kistl.API.Utils.AutoFacBuilder.CreateContainerBuilder(config, config.Client.Modules);

            builder
                .RegisterType<Launcher>()
                .SingleInstance();

            builder
                .Register<Kistl.Client.WPF.Toolkit.VisualTypeTemplateSelector>((c, p) => new Kistl.Client.WPF.Toolkit.VisualTypeTemplateSelector(
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

                    var config = KistlConfig.FromFile(configFilePath, "Kistl.Client.WPF.xml");
                    InitCulture(config);

                    if (config.Server != null && config.Server.StartServer)
                    {
                        serverDomain = new ServerDomainManager();
                        serverDomain.Start(config);
                    }
                    else
                    {
                    }

                    AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                    container = CreateMasterContainer(config);

                    // Init Resources
                    var iconConverter = new IconConverter(container.Resolve<IFrozenContext>(), container.Resolve<IKistlContext>());
                    this.Resources["IconConverter"] = iconConverter;
                    this.Resources["ImageCtrlConverter"] = new ImageCtrlConverter(iconConverter);
                    var templateSelectorFactory = container.Resolve<Kistl.Client.WPF.Toolkit.VisualTypeTemplateSelector.Factory>();
                    this.Resources["defaultTemplateSelector"] = templateSelectorFactory(null);
                    this.Resources["listItemTemplateSelector"] = templateSelectorFactory("Kistl.App.GUI.SingleLineKind");
                    this.Resources["dashBoardTemplateSelector"] = templateSelectorFactory("Kistl.App.GUI.DashboardKind");

                    // Manually add DefaultStyles and DefaultViews
                    // Otherwise converter are unknown
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Kistl.Client.WPF;component/View/DefaultStyles.xaml", UriKind.Relative) });
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Kistl.Client.WPF;component/View/DefaultHighlightColorDefinitions.xaml", UriKind.Relative) });
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Kistl.Client.WPF;component/View/DefaultViews.xaml", UriKind.Relative) });

                    // Load registrated dictionaries from autofac
                    foreach (var dict in container.Resolve<IEnumerable<ResourceDictionary>>())
                    {
                        this.Resources.MergedDictionaries.Add(dict);
                    }

                    // Focus nightmare
                    // http://stackoverflow.com/questions/673536/wpf-cant-set-focus-to-a-child-of-usercontrol/4785124#4785124
                    EventManager.RegisterClassHandler(typeof(Window), Window.LoadedEvent, new RoutedEventHandler(FocusFixLoaded));
                    EventManager.RegisterClassHandler(typeof(Kistl.Client.WPF.View.KistlBase.InstanceCollectionBase), UserControl.LoadedEvent, new RoutedEventHandler(FocusFixLoaded));

                    FixupDatabase(container.Resolve<Func<IKistlContext>>());

                    // delegate all business logic into another class, which 
                    // allows us to load the Kistl.Objects assemblies _before_ 
                    // they are needed.
                    var launcher = container.Resolve<Launcher>();
                    launcher.Show(args);
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
            }),DispatcherPriority.ApplicationIdle);
        }

        private static void InitCulture(KistlConfig config)
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
            if (serverDomain != null)
                serverDomain.Stop();

            if (container != null)
                container.Dispose();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.GetInnerException() is Kistl.API.Common.UnresolvableIdentityException)
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
            if (container != null)
            {
                var vmf = container.Resolve<IViewModelFactory>();
                var mdl = vmf.CreateViewModel<ExceptionReporterViewModel.Factory>().Invoke(container.Resolve<IKistlContext>(), null, ex, container.Resolve<IScreenshotTool>().GetScreenshot());
                vmf.ShowDialog(mdl);
            }
            else
            {
                MessageBox.Show(ex.ToString());
            }
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
