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
                Manager.Create(e.Args, Toolkit.WPF);
#if false
                using (IKistlContext ctx = KistlContext.GetContext())
                {

                    Module guiModule = ctx.GetQuery<Module>().Where(m => m.ModuleName == "GUI").ToList().Single();

                    foreach (ControlInfo info in ControlInfo.Implementations)
                    {
                        string aName = info.AssemblyName;
                        Assembly controlAssembly = ctx.GetQuery<Assembly>().Where(a => a.AssemblyName == aName).ToList().SingleOrDefault();
                        if (controlAssembly == null)
                        {
                            controlAssembly = ctx.Create<Assembly>();
                            controlAssembly.AssemblyName = info.AssemblyName;
                            controlAssembly.Module = guiModule;
                        }
                        ctx.SubmitChanges();

                        Kistl.App.GUI.ControlInfo upload = ctx.Create<Kistl.App.GUI.ControlInfo>();
                        upload.Assembly = controlAssembly;
                        upload.ClassName = info.ClassName;
                        upload.ControlType = (int)Enum.Parse(typeof(Kistl.App.GUI.VisualType), info.ControlType.ToString());
                        upload.IsContainer = info.Container;
                        upload.Platform = (int)Enum.Parse(typeof(Kistl.App.GUI.Toolkit), info.Platform.ToString());
                        ctx.SubmitChanges();

                    }
                }
#endif
            }
        }

    }
}
