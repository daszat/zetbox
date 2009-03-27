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
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.WPF;

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
                HandleCommandline(e.Args);

                //var debugger = AppContext.Factory.CreateSpecificModel<KistlDebuggerAsModel>(KistlContext.GetContext());
                //AppContext.Factory.ShowModel(debugger, true);

                FixupDatabase();

                var initialWorkspace = AppContext.Factory.CreateSpecificModel<WorkspaceModel>(KistlContext.GetContext());
                AppContext.Factory.ShowModel(initialWorkspace, true);
            }
        }

        private void FixupDatabase()
        {
            //FixNotNullableConstraints();
            //CreateTypeRefs();
        }

        private void CreateTypeRefs()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Creating TypeRefs for GUI"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    object muh;
                    muh = (typeof(NullableValuePropertyModel<Boolean>).ToRef(ctx));
                    muh = (typeof(NullableValuePropertyModel<DateTime>).ToRef(ctx));
                    muh = (typeof(NullableValuePropertyModel<Double>).ToRef(ctx));
                    muh = (typeof(NullableValuePropertyModel<int>).ToRef(ctx));
                    muh = (typeof(ChooseReferencePropertyModel<string>).ToRef(ctx));
                    muh = (typeof(SimpleReferenceListPropertyModel<string>).ToRef(ctx));
                    muh = (typeof(ReferencePropertyModel<string>).ToRef(ctx));
                    muh = (typeof(ObjectReferenceModel).ToRef(ctx));
                    muh = (typeof(ObjectListModel).ToRef(ctx));

                    foreach (Enumeration e in ctx.GetQuery<Enumeration>())
                    {
                        var enumRef = e.GetDataType().ToRef(ctx);
                        var genericRef = typeof(EnumerationPropertyModel<int>).GetGenericTypeDefinition().ToRef(ctx);
                        var concreteRef = ctx.GetQuery<Kistl.App.Base.TypeRef>()
                            .Where(r => r.FullName == genericRef.FullName
                                && r.Assembly.ID == genericRef.Assembly.ID
                                && r.GenericArguments.Count == 1)
                            .ToList()
                            .Where(r => r.GenericArguments.Single().ID == enumRef.ID)
                            .SingleOrDefault();

                        if (concreteRef == null)
                        {
                            concreteRef = ctx.Create<TypeRef>();
                            concreteRef.FullName = genericRef.FullName;
                            concreteRef.Assembly = genericRef.Assembly;
                            concreteRef.GenericArguments.Add(enumRef);
                        }
                    }

                    ctx.SubmitChanges();
                }
            }

        }

        private static void FixNotNullableConstraints()
        {
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

                        currentStringRangeConstraint.MaxLength = prop.Length;
                    }
                    ctx.SubmitChanges();
                }
            }
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
