using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public GuiApplicationContext AppContext { get; private set; }

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
        private bool _timeRecorder = false;

        private string[] HandleCommandline(string[] args)
        {
            string configFilePath;
            string[] result;

            if (args.Length == 1 && !args[0].StartsWith("-"))
            {
                configFilePath = args[0];
                result = new string[] { };
            }
            else
            {
                configFilePath = "";
                result = (string[])args.Clone();
            }

            var config = KistlConfig.FromFile(configFilePath);

            if (config.Server.StartServer)
            {
                serverDomain = new ServerDomainManager();
                serverDomain.Start(config);
            }

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, config);
            AppContext = new GuiApplicationContext(config, "WPF");

            return result;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 5);

            using (TraceClient.TraceHelper.TraceMethodCall("Starting Client"))
            {
                var args = HandleCommandline(e.Args);
                _timeRecorder = args.Contains("-timerecorder");

                //var debugger = AppContext.Factory.CreateSpecificModel<KistlDebuggerAsModel>(KistlContext.GetContext());
                //AppContext.Factory.ShowModel(debugger, true);

                PresentableModel initialWorkspace;
                if (_timeRecorder)
                {
                    initialWorkspace = AppContext.Factory.CreateSpecificModel<Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel>(KistlContext.GetContext());
                    AppContext.Factory.ShowModel(initialWorkspace, true);
                }
                else
                {
                    FixupDatabase();

                    initialWorkspace = AppContext.Factory.CreateSpecificModel<WorkspaceModel>(KistlContext.GetContext());
                }
                AppContext.Factory.ShowModel(initialWorkspace, true);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (serverDomain != null)
                serverDomain.Stop();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString() + e.Exception.StackTrace);
            e.Handled = true;
        }

#if DONOTUSE
        private static Assembly FetchOrCreateAssembly(
            IKistlContext ctx,
            Module guiModule,
            string aName)
        {
            Assembly result = ctx.GetQuery<Assembly>()
                .Where(a => a.AssemblyName == aName)
                .ToList()
                .SingleOrDefault();

            if (result == null)
            {
                result = ctx.Create<Assembly>();
                result.AssemblyName = aName;
                result.Module = guiModule;
            }

            return result;
        }
#endif
    }
}
