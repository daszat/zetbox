using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.GUI.DB;
using Kistl.Client.WPF.View;
using Kistl.Client.PresenterModel;
using Kistl.Client.PresenterModel.WPF;

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

        private static ServerDomainManager serverDomain;

        private static string[] Init(string[] args)
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

            var appCtx = new GuiApplicationContext(config, Toolkit.WPF);

            return result;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Debugger.KistlContextDebuggerWPF.ShowDebugger();

            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 5);

            using (TraceClient.TraceHelper.TraceMethodCall("Starting Client"))
            {
                Init(e.Args);
            }

            using (TraceClient.TraceHelper.TraceMethodCall("Fixing NotNullableConstraints"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    // Apply a NotNullableConstraint as appropriate
                    foreach (var prop in ctx.GetQuery<Property>())
                    {
                        var currentNotNullableConstraint = prop.Constraints.OfType<NotNullableConstraint>().SingleOrDefault();
                        bool hasNullableConstraint = (currentNotNullableConstraint != null);
                        if (prop.IsNullable && hasNullableConstraint)
                        {
                            prop.Constraints.Remove(currentNotNullableConstraint);
                            System.Console.Out.WriteLine("Removed obsolete NotNullableConstraint");
                        }
                        else if (!prop.IsNullable && !hasNullableConstraint)
                        {
                            prop.Constraints.Add(ctx.Create<NotNullableConstraint>());
                            System.Console.Out.WriteLine("Added missing NotNullableConstraint");
                        }
                    }

                    // synchronize Stringproperty's Length
                    foreach (var prop in ctx.GetQuery<StringProperty>())
                    {
                        var currentStringRangeConstraint = prop.Constraints.OfType<StringRangeConstraint>().SingleOrDefault();

                        if (currentStringRangeConstraint == null)
                        {
                            currentStringRangeConstraint = ctx.Create<StringRangeConstraint>();
                            currentStringRangeConstraint.MinLength = 0;
                            prop.Constraints.Add(currentStringRangeConstraint);
                        }

                        currentStringRangeConstraint.MaxLength = prop.Length.Value;
                    }
                    ctx.SubmitChanges();
                }
            }

            var workspace = new WorkspaceView();
            // workspace.DataContext = new WorkspaceModel(new UiThreadManager(), new AsyncThreadManager(), FrozenContext.Single);
            workspace.DataContext = new WorkspaceModel(new SynchronousThreadManager(), new SynchronousThreadManager(), FrozenContext.Single);
            workspace.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (serverDomain != null)
                serverDomain.Stop();
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
