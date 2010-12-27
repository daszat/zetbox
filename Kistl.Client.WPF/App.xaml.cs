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
                .Register<Kistl.Client.WPF.View.VisualTypeTemplateSelector>((c, p) => new Kistl.Client.WPF.View.VisualTypeTemplateSelector(
                    p.Named<object>("requestedKind"), 
                    c.Resolve<IFrozenContext>()))
                .InstancePerDependency();
            
            return builder.Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DebugConsole.Show();
            Logging.Configure();
            SplashScreen.ShowSplashScreen("ZBox is starting...", "Init application", 5);

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

                if (!string.IsNullOrEmpty(config.Client.Culture))
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(config.Client.Culture);
                }
                if (!string.IsNullOrEmpty(config.Client.UICulture))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(config.Client.UICulture);
                }

                SplashScreen.SetInfo("Bootstrapping Assembly Resolver");
                AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);

                container = CreateMasterContainer(config);

                SplashScreen.SetInfo("Initializing Launcher");

                // Init Resources
                this.Resources["IconConverter"] = new IconConverter(config.Client.DocumentStore, container.Resolve<IFrozenContext>());
                var templateSelectorFactory = container.Resolve<Kistl.Client.WPF.View.VisualTypeTemplateSelector.Factory>();
                this.Resources["defaultTemplateSelector"] = templateSelectorFactory(null);
                this.Resources["listItemTemplateSelector"] = templateSelectorFactory("Kistl.App.GUI.SingleLineKind");
                this.Resources["dashBoardTemplateSelector"] = templateSelectorFactory("Kistl.App.GUI.DashboardKind");

                RunFixes(container.Resolve<IKistlContext>());

                // delegate all business logic into another class, which 
                // allows us to load the Kistl.Objects assemblies _before_ 
                // they are needed.
                var launcher = container.Resolve<Launcher>();
                launcher.Show(args);
            }

            SplashScreen.HideSplashScreen();
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
