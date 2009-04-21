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
using Kistl.App.Extensions;
using Kistl.App.GUI;
using Kistl.Client.WPF.View;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.WPF;
using System.Diagnostics;

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

            //CreateViewDescriptors();
            //CreatePresentableModelDescriptorClass();

            //AddDefaultPMDescriptorToObjectClass();

            //CreatePresentableModelDescriptors();
            //CreateMorePresentableModelDescriptors();

            //CreateTypeRefBaseTypeRefRelation();
            //FixupTypeRefParents();

            //CreateDefaultValueModelProperties();

            //CheckAndCleanAllAssemblies();
            //CheckAndCleanAllTypeRefs();
            //CreateTypeRefsForTesting();
        }

        private void CreateTypeRefsForTesting()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CreateTypeRefsForTesting"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                	typeof(ICollection<int>).ToRef(ctx);
                	typeof(ICollection<int?>).ToRef(ctx);
                    ctx.SubmitChanges();
                }
            }
        }
        private void CheckAndCleanAllAssemblies()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CheckAndCleanAllAssemblies"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var assemblies = ctx.GetQuery<Assembly>();
                    foreach (var a in assemblies)
                    {
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(a.AssemblyName);
                        a.AssemblyName = assembly.FullName;
                    }
                    ctx.SubmitChanges();
                }
            }
        }

        // TODO: should go to TempAppHelpers
        public static void CheckAndCleanAllTypeRefs()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CheckAndCleanAllTypeRefs"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var types = ctx.GetQuery<TypeRef>().ToLookup(obj => obj.AsType(false));

                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\temp\update.sql"))
                    {

                        foreach (var duplicates in types.Where(g => g.Key != null))
                        {
                            var systemType = duplicates.Key;
                            var typeRef = systemType.ToRef(ctx);

                            foreach (var type in duplicates)
                            {
                                if (type != typeRef)
                                {
                                    ctx.Delete(type);
                                    sw.WriteLine("UPDATE PresentableModelDescriptors SET fk_PresentableModelRef = {0} WHERE fk_PresentableModelRef = {1}", typeRef.ID, type.ID);
                                    sw.WriteLine("UPDATE TypeRefs SET fk_Parent = {0} WHERE fk_Parent = {1}", typeRef.ID, type.ID);
                                    sw.WriteLine("UPDATE TypeRefs_GenericArgumentsCollection SET fk_TypeRef = {0} WHERE fk_TypeRef = {1}", typeRef.ID, type.ID);
                                    sw.WriteLine("UPDATE TypeRefs_GenericArgumentsCollection SET fk_GenericArguments = {0} WHERE fk_GenericArguments = {1}", typeRef.ID, type.ID);
                                    ctx.SubmitChanges();
                                }
                            }

                            UpdateParent(ctx, typeRef);
                            ctx.SubmitChanges();
                        }
                    }
                    ctx.SubmitChanges();
                    foreach (var type in types[null])
                    {
                        Trace.TraceError("illegal type: {0}", type.ToString());
                        ctx.Delete(type);
                        ctx.SubmitChanges();
                    }
                }
            }
        }

        private void FixupTypeRefParents()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("FixupTypeRefParents"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var typeRefs = ctx.GetQuery<TypeRef>();
                    foreach (var tr in typeRefs)
                    {
                        if (tr.Parent != null)
                            continue;

                        UpdateParent(ctx, tr);
                        ctx.SubmitChanges();
                    }
                }
            }
        }

        private static void UpdateParent(IKistlContext ctx, TypeRef tr)
        {
            var type = tr.AsType(false);
            if (type != null
                && type != typeof(object)
                && !type.IsGenericTypeDefinition
                && type.BaseType != null)
            {
                tr.Parent = type.BaseType.ToRef(ctx);
                UpdateParent(ctx, tr.Parent);
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
