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

            // CreateViewDescriptors();
        }

        private void CreateViewDescriptors()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Creating TypeRefs for GUI"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var guiModule = ctx.Find<Module>(4);
                    var typeRefClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "TypeRef");
                    var vdClass = ctx.Create<ObjectClass>();
                    vdClass.ClassName = "ViewDescriptor";
                    vdClass.TableName = "ViewDescriptors";
                    vdClass.Module = guiModule;
                    vdClass.IsFrozenObject = true;

                    var nameProp = ctx.Create<EnumerationProperty>();
                    nameProp.CategoryTags = "Summary,Main";
                    nameProp.Description = "The visual type of this View";
                    nameProp.Enumeration = ctx.Find<Enumeration>(55);
                    nameProp.Module = guiModule;
                    nameProp.ObjectClass = vdClass;
                    nameProp.PropertyName = "VisualType";

                    var toolkitProp = ctx.Create<EnumerationProperty>();
                    toolkitProp.CategoryTags = "Summary,Main";
                    toolkitProp.Description = "Which toolkit provides this View";
                    toolkitProp.Enumeration = ctx.Find<Enumeration>(53);
                    toolkitProp.Module = guiModule;
                    toolkitProp.ObjectClass = vdClass;
                    toolkitProp.PropertyName = "Toolkit";

                    var controlRel = ctx.Create<Relation>();
                    controlRel.Description = "This relation describes which control implements which view";
                    controlRel.A = ctx.Create<RelationEnd>();
                    controlRel.A.RoleName = "View";
                    controlRel.A.Type = vdClass;
                    controlRel.A.Multiplicity = Multiplicity.ZeroOrMore;
                    controlRel.A.Navigator = ctx.Create<ObjectReferenceProperty>();
                    controlRel.A.Navigator.CategoryTags = "Main";
                    controlRel.A.Navigator.Description = "The control implementing this View";
                    controlRel.A.Navigator.Module = guiModule;
                    controlRel.A.Navigator.ObjectClass = vdClass;
                    controlRel.A.Navigator.PropertyName = "ControlRef";
                    controlRel.A.Navigator.ReferenceObjectClass = typeRefClass;
                    controlRel.A.Role = (int)RelationEndRole.A;

                    controlRel.B = ctx.Create<RelationEnd>();
                    controlRel.B.RoleName = "ControlRef";
                    controlRel.B.Type = typeRefClass;
                    controlRel.B.Multiplicity = Multiplicity.One;
                    controlRel.B.Role = (int)RelationEndRole.B;

                    controlRel.Storage = StorageType.MergeIntoA;

                    var pModelRel = ctx.Create<Relation>();
                    pModelRel.Description = "This relation describes which presentable model can be displayed with this view";
                    pModelRel.A = ctx.Create<RelationEnd>();
                    pModelRel.A.RoleName = "View";
                    pModelRel.A.Type = vdClass;
                    pModelRel.A.Multiplicity = Multiplicity.ZeroOrMore;
                    pModelRel.A.Navigator = ctx.Create<ObjectReferenceProperty>();
                    pModelRel.A.Navigator.CategoryTags = "Main";
                    pModelRel.A.Navigator.Description = "The PresentableModel usable by this View";
                    pModelRel.A.Navigator.Module = guiModule;
                    pModelRel.A.Navigator.ObjectClass = vdClass;
                    pModelRel.A.Navigator.PropertyName = "PresentedModelRef";
                    pModelRel.A.Navigator.ReferenceObjectClass = typeRefClass;
                    pModelRel.A.Role = (int)RelationEndRole.A;

                    pModelRel.B = ctx.Create<RelationEnd>();
                    pModelRel.B.RoleName = "PresentedModelRef";
                    pModelRel.B.Type = typeRefClass;
                    pModelRel.B.Multiplicity = Multiplicity.One;
                    pModelRel.B.Role = (int)RelationEndRole.B;
                    
                    pModelRel.Storage = StorageType.MergeIntoA;

                    ctx.SubmitChanges();

                }
            }
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
