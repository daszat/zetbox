using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Globalization;
using Kistl.GUI.DB;
using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;

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
            Debugger.KistlContextDebuggerWPF.ShowDebugger();

            SplashScreen.ShowSplashScreen("Kistl is starting...", "Init application", 5);

            using (TraceClient.TraceHelper.TraceMethodCall("Starting Client"))
            {
                Manager.Create(e.Args, Kistl.App.GUI.Toolkit.WPF);

#if false
                using (TraceClient.TraceHelper.TraceMethodCall("Uploading GUI Objects"))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        // The various handles for adding all the new data
                        Module guiModule = ctx.GetQuery<Module>().Where(m => m.ModuleName == "GUI").Single();

                        // delete old data
                        ctx.GetQuery<Kistl.App.GUI.PresenterInfo>().ForEach(pi => ctx.Delete(pi));

                        foreach (LocalPresenterInfo info in LocalPresenterInfo.Implementations)
                        {
                            string aName = info.AssemblyName;
                            Assembly presenterAssembly = FetchOrCreateAssembly(ctx, guiModule, info.AssemblyName);
                            Assembly dataAssembly = null;
                            if (info.SourceType.Assembly.FullName != typeof(object).Assembly.FullName)
                                dataAssembly = FetchOrCreateAssembly(ctx, guiModule, info.SourceType.Assembly.FullName);

                            Kistl.App.GUI.PresenterInfo upload = ctx.Create<Kistl.App.GUI.PresenterInfo>();
                            upload.PresenterAssembly = presenterAssembly;
                            upload.PresenterTypeName = info.ClassName;

                            upload.DataAssembly = dataAssembly;
                            upload.DataTypeName = info.SourceType.FullName;
                            
                            upload.ControlType = info.Control;
                        }

                        ctx.SubmitChanges();
                    }
                }
#endif
            }
        }

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

    }
}
