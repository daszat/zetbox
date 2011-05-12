
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

            throw new Exception("test");

            //return builder.Build();
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

                    RunFixes(container.Resolve<IKistlContext>());

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

        private void RunFixes(IKistlContext ctx)
        {
            //var vmd = ctx.FindPersistenceObject<ViewModelDescriptor>(new Guid("67A49C49-B890-4D35-A8DB-1F8E43BFC7DF"));
            //foreach (var p in ctx.GetQuery<Kistl.App.Base.ObjectReferenceProperty>())
            //{
            //    if (p.GetIsList())
            //    {
            //        if (!p.RelationEnd.Parent.GetOtherEnd(p.RelationEnd).HasPersistentOrder)
            //        {
            //            Logging.Log.InfoFormat("Changing VMD for {0}", p.Name);
            //            p.ValueModelDescriptor = vmd;
            //        }
            //        else
            //        {
            //            Logging.Log.InfoFormat("Leaving VMD for {0}", p.Name);
            //        }
            //    }
            //}

            //ctx.SubmitChanges();
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
                var mdl = vmf.CreateViewModel<ExceptionReporterViewModel.Factory>().Invoke(container.Resolve<IKistlContext>(), ex, container.Resolve<IScreenshotTool>().GetScreenshot());
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
