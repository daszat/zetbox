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
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.GUI.DB;

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
                // TODO: Leider muss ich das machen, weil hier an dieser Stelle
                // _darf_ es nichts geben, was eine Referenz auf Kistl.Objects benötigt
                // weil Kistl.Objects dynamisch geladen wird.
                // woher Kistl.Objects geladen wird, entscheide der AssemblyLoader,
                // der gerade initialisiert wird.
                Manager.Create(e.Args, Toolkit.WPF);
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
