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
        }

        private void CreateDefaultValueModelProperties()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CreateDefaultValueModelProperties"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {

                    var guiModule = ctx.Find<Module>(4);
                    //var ocClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "ObjectClass");
                    var propClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "Property");
                    var pmdClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "PresentableModelDescriptor");

                    var valueRel = ctx.Create<Relation>();
                    valueRel.Description = "Connects a Property with the PresentableModel to display her value";
                    valueRel.A = ctx.Create<RelationEnd>();
                    valueRel.A.RoleName = "Property";
                    valueRel.A.Type = propClass;
                    valueRel.A.Multiplicity = Multiplicity.ZeroOrMore;
                    valueRel.A.Navigator = ctx.Create<ObjectReferenceProperty>();
                    valueRel.A.Navigator.CategoryTags = "Main";
                    valueRel.A.Navigator.Description = "The PresentableModel to use for values of this Property";
                    valueRel.A.Navigator.Module = guiModule;
                    valueRel.A.Navigator.ObjectClass = propClass;
                    valueRel.A.Navigator.PropertyName = "ValueModelDescriptor";
                    valueRel.A.Navigator.ReferenceObjectClass = pmdClass;
                    valueRel.A.Role = (int)RelationEndRole.A;

                    valueRel.B = ctx.Create<RelationEnd>();
                    valueRel.B.RoleName = "ValueModelDescriptor";
                    valueRel.B.Type = pmdClass;
                    valueRel.B.Multiplicity = Multiplicity.One;
                    valueRel.B.Role = (int)RelationEndRole.B;

                    valueRel.Storage = StorageType.MergeIntoA;

                    ctx.SubmitChanges();
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
                tr.Parent = FindOrCreateTypeRef(ctx, type.BaseType.FullName, type.BaseType.Assembly.FullName);
                UpdateParent(ctx, tr.Parent);
            }
        }

        private void CreateTypeRefBaseTypeRefRelation()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CreateTypeRefBaseTypeRefRelation"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var typeRefClass = ctx.GetQuery<ObjectClass>().Single(cls => cls.ClassName == "TypeRef");
                    var baseModule = ctx.GetQuery<Module>().Single(m => m.ModuleName == "KistlBase");

                    var controlRel = ctx.Create<Relation>();
                    controlRel.Description = "This relation describes the underlying inheritance relationships";
                    controlRel.A = ctx.Create<RelationEnd>();
                    controlRel.A.RoleName = "Child";
                    controlRel.A.Type = typeRefClass;
                    controlRel.A.Multiplicity = Multiplicity.ZeroOrMore;
                    controlRel.A.Navigator = ctx.Create<ObjectReferenceProperty>();
                    controlRel.A.Navigator.Description = "The TypeRef of the BaseClass of the referenced Type";
                    controlRel.A.Navigator.Module = baseModule;
                    controlRel.A.Navigator.ObjectClass = typeRefClass;
                    controlRel.A.Navigator.PropertyName = "Parent";
                    controlRel.A.Navigator.ReferenceObjectClass = typeRefClass;
                    controlRel.A.Role = (int)RelationEndRole.A;

                    controlRel.B = ctx.Create<RelationEnd>();
                    controlRel.B.RoleName = "Parent";
                    controlRel.B.Type = typeRefClass;
                    controlRel.B.Multiplicity = Multiplicity.One;
                    controlRel.B.Role = (int)RelationEndRole.B;

                    controlRel.Storage = StorageType.MergeIntoA;

                    ctx.SubmitChanges();
                }
            }
        }

        private void CreateMorePresentableModelDescriptors()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CreateMorePresentableModelDescriptors"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    PresentableModelDescriptor pmd;

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ObjectClassModel),
                        VisualType.Object,
                        "DataObjectModel with specific extensions for ObjectClasses");

                    var ocClass = ctx.GetQuery<ObjectClass>().Single(obj => obj.ClassName == "ObjectClass");
                    ocClass.DefaultPresentableModelDescriptor = pmd;


                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ModuleModel),
                        VisualType.Object,
                        "DataObjectModel with specific extensions for Modules");

                    ocClass = ctx.GetQuery<ObjectClass>().Single(obj => obj.ClassName == "Module");
                    ocClass.DefaultPresentableModelDescriptor = pmd;

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(MethodInvocationModel),
                        VisualType.Object,
                        "DataObjectModel with specific extensions for MethodInvocations");

                    ocClass = ctx.GetQuery<ObjectClass>().Single(obj => obj.ClassName == "MethodInvocation");
                    ocClass.DefaultPresentableModelDescriptor = pmd;

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(DataTypeModel),
                        VisualType.Object,
                        "DataObjectModel with specific extensions for DataTypes");

                    ocClass = ctx.GetQuery<ObjectClass>().Single(obj => obj.ClassName == "DataType");
                    ocClass.DefaultPresentableModelDescriptor = pmd;

                    ctx.SubmitChanges();
                }
            }
        }

        private void CreatePresentableModelDescriptors()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CreatePresentableModelDescriptors"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    ctx.GetQuery<PresentableModelDescriptor>().ForEach(obj => ctx.Delete(obj));
                    ctx.GetQuery<ViewDescriptor>().ForEach(obj => ctx.Delete(obj));

                    PresentableModelDescriptor pmd;

                    // Propertyvalues
                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableValuePropertyModel<Boolean>),
                        VisualType.Boolean,
                        "A simple true/false attribute");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.Boolean,
                        "Kistl.Client.WPF.View.NullableBoolValueView", "Kistl.Client.WPF");

                    // no specific boolean control available
                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WinForms, VisualType.String,
                        "Kistl.Client.Forms.View.NullablePropertyTextBoxView", "Kistl.Client.Forms");

                    // no specific boolean control available
                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.ASPNET, VisualType.String,
                        "Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxViewLoader", "Kistl.Client.ASPNET.Toolkit");

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableValuePropertyModel<DateTime>),
                        VisualType.String,
                        "A date and time attribute");

                    CreateAllPropertyViewDescriptors(ctx, pmd);

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableValuePropertyModel<Double>),
                        VisualType.String,
                        "A floating point attribute");

                    CreateAllPropertyViewDescriptors(ctx, pmd);

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableValuePropertyModel<int>),
                        VisualType.String,
                        "An integer attribute");

                    CreateAllPropertyViewDescriptors(ctx, pmd);

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ReferencePropertyModel<String>),
                        VisualType.String,
                        "A string attribute");

                    CreateAllPropertyViewDescriptors(ctx, pmd);

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.StringList,
                        "Kistl.Client.WPF.View.ListValueView", "Kistl.Client.WPF");

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ChooseReferencePropertyModel<String>),
                        VisualType.StringSelection,
                        "Select a string value from a set of values");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.StringSelection,
                        "Kistl.Client.WPF.View.TextValueSelectionView", "Kistl.Client.WPF");



                    foreach (var enumDef in ctx.GetQuery<Enumeration>())
                    {
                        Type enumType = typeof(EnumerationPropertyModel<int>)
                            .GetGenericTypeDefinition()
                            .MakeGenericType(enumDef.GetDataType());

                        pmd = CreatePresentableModelDescriptor(ctx,
                            enumType,
                            VisualType.Enumeration,
                            String.Format("An enumeration value for {0}", enumDef.ClassName));

                        CreateViewDescriptor(ctx, pmd,
                            Toolkit.WPF, VisualType.Enumeration,
                            "Kistl.Client.WPF.View.EnumSelectionView", "Kistl.Client.WPF");

                        // no specific enumeration control available
                        CreateViewDescriptor(ctx, pmd,
                            Toolkit.WinForms, VisualType.String,
                            "Kistl.Client.Forms.View.NullablePropertyTextBoxView", "Kistl.Client.Forms");

                        // no specific enumeration control available
                        CreateViewDescriptor(ctx, pmd,
                            Toolkit.ASPNET, VisualType.String,
                            "Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxViewLoader", "Kistl.Client.ASPNET.Toolkit");

                    }

                    // Method results
                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableResultModel<Boolean>),
                        VisualType.Boolean,
                        "A method returning true or false");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.Boolean,
                        "Kistl.Client.WPF.View.NullableBoolValueView", "Kistl.Client.WPF");

                    // no specific boolean control available
                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WinForms, VisualType.String,
                        "Kistl.Client.Forms.View.NullablePropertyTextBoxView", "Kistl.Client.Forms");

                    // no specific boolean control available
                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.ASPNET, VisualType.String,
                        "Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxViewLoader", "Kistl.Client.ASPNET.Toolkit");


                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableResultModel<DateTime>),
                        VisualType.String,
                        "A method returning a date and time value");

                    CreateAllPropertyViewDescriptors(ctx, pmd);

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableResultModel<Double>),
                        VisualType.String,
                        "A method returning a floating point value");

                    CreateAllPropertyViewDescriptors(ctx, pmd);

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(NullableResultModel<int>),
                        VisualType.String,
                        "A method returning an integer value");

                    CreateAllPropertyViewDescriptors(ctx, pmd);

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ObjectResultModel<String>),
                        VisualType.String,
                        "A method returning a string");

                    CreateAllPropertyViewDescriptors(ctx, pmd);


                    // Various top- and midlevel objects
                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(PropertyGroupModel),
                        VisualType.PropertyGroup,
                        "A group of properties");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.PropertyGroup,
                        "Kistl.Client.WPF.View.PropertyGroupBoxView", "Kistl.Client.WPF");


                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(DataObjectModel),
                        VisualType.Object,
                        "A complete IDataObject");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.Object,
                        "Kistl.Client.WPF.View.DataObjectFullView", "Kistl.Client.WPF");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.ObjectListEntry,
                        "Kistl.Client.WPF.View.DataObjectView", "Kistl.Client.WPF");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WinForms, VisualType.Object,
                        "Kistl.Client.Forms.View.DataObjectFullView", "Kistl.Client.Forms");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.ASPNET, VisualType.Object,
                        "Kistl.Client.ASPNET.Toolkit.View.DataObjectFullViewLoader", "Kistl.Client.ASPNET.Toolkit");


                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ObjectListModel),
                        VisualType.ObjectList,
                        "A list of IDataObjects");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.ObjectList,
                        "Kistl.Client.WPF.View.DataObjectListView", "Kistl.Client.WPF");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WinForms, VisualType.ObjectList,
                        "Kistl.Client.Forms.View.DataObjectListView", "Kistl.Client.Forms");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.ASPNET, VisualType.ObjectList,
                        "Kistl.Client.ASPNET.Toolkit.View.DataObjectListViewLoader", "Kistl.Client.ASPNET.Toolkit");



                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ObjectReferenceModel),
                        VisualType.ObjectReference,
                        "A reference to an IDataObject");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.ObjectReference,
                        "Kistl.Client.WPF.View.ObjectReferenceView", "Kistl.Client.WPF");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WinForms, VisualType.ObjectReference,
                        "Kistl.Client.Forms.View.ObjectReferenceView", "Kistl.Client.Forms");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.ASPNET, VisualType.ObjectReference,
                        "Kistl.Client.ASPNET.Toolkit.View.ObjectReferenceViewLoader", "Kistl.Client.ASPNET.Toolkit");


                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ObjectResultModel<Kistl.API.IDataObject>),
                        VisualType.ObjectReference,
                        "A method returning an IDataObject reference");

                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(ActionModel),
                        VisualType.MenuItem,
                        "An action which can be triggered by the user");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.MenuItem,
                        "Kistl.Client.WPF.View.ActionView", "Kistl.Client.WPF");



                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(DataObjectSelectionTaskModel),
                        VisualType.SelectionTaskDialog,
                        "A task for the user: select an IDataObject from a list");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.SelectionTaskDialog,
                        "Kistl.Client.WPF.View.SelectionDialog", "Kistl.Client.WPF");


                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(WorkspaceModel),
                        VisualType.WorkspaceWindow,
                        "A top-level window containing a Workspace, a visual representation for IKistlContext");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.WorkspaceWindow,
                        "Kistl.Client.WPF.View.WorkspaceView", "Kistl.Client.WPF");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WinForms, VisualType.WorkspaceWindow,
                        "Kistl.Client.Forms.View.WorkspaceView", "Kistl.Client.Forms");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.ASPNET, VisualType.WorkspaceWindow,
                        "Kistl.Client.ASPNET.Toolkit.View.WorkspaceViewLoader", "Kistl.Client.ASPNET.Toolkit");


                    pmd = CreatePresentableModelDescriptor(ctx,
                        typeof(KistlDebuggerAsModel),
                        VisualType.KistlDebugger,
                        "A debugger window showing the used IKistlContexts and their AttachedObjects");

                    CreateViewDescriptor(ctx, pmd,
                        Toolkit.WPF, VisualType.KistlDebugger,
                        "Kistl.Client.WPF.View.KistlDebuggerView", "Kistl.Client.WPF");

                    //CreatePresentableModelDescriptor(ctx,
                    //    typeof(xx),
                    //    VisualType.xx,
                    //    "xx");

                    ctx.SubmitChanges();
                }
            }
        }

        private void CreateAllPropertyViewDescriptors(IKistlContext ctx, PresentableModelDescriptor pmd)
        {
            CreatePropertyViewDescriptor(ctx, pmd, Toolkit.WPF,
                "Kistl.Client.WPF.View.NullablePropertyTextBoxView", "Kistl.Client.WPF");

            CreatePropertyViewDescriptor(ctx, pmd, Toolkit.WinForms,
                "Kistl.Client.Forms.View.NullablePropertyTextBoxView", "Kistl.Client.Forms");

            CreatePropertyViewDescriptor(ctx, pmd, Toolkit.ASPNET,
                "Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxViewLoader", "Kistl.Client.ASPNET.Toolkit");

        }

        private void CreatePropertyViewDescriptor(
            IKistlContext ctx, PresentableModelDescriptor pmd,
            Toolkit tk,
            string typeName, string assemblyName)
        {
            CreateViewDescriptor(ctx, pmd, tk, pmd.DefaultVisualType, typeName, assemblyName);
        }

        private static void CreateViewDescriptor(IKistlContext ctx, PresentableModelDescriptor pmd,
            Toolkit tk, VisualType vt, string typeName, string assemblyName)
        {
            var vd = ctx.Create<ViewDescriptor>();
            vd.ControlRef = FindOrCreateTypeRef(ctx, typeName, assemblyName);
            vd.Toolkit = tk;
            vd.VisualType = vt;
            vd.PresentedModelDescriptor = pmd;
        }

        private static PresentableModelDescriptor CreatePresentableModelDescriptor(IKistlContext ctx, Type mdlType, VisualType defaultVisual, string description)
        {
            var pmd = ctx.Create<PresentableModelDescriptor>();
            pmd.DefaultVisualType = defaultVisual;
            pmd.Description = description;
            pmd.PresentableModelRef = mdlType.ToRef(ctx);
            return pmd;
        }

        private static TypeRef FindOrCreateTypeRef(IKistlContext ctx, string fullname, string assembly)
        {
            // Adapted from ToRef(Type,IKistlContext)
            var result = ctx.GetQuery<TypeRef>().SingleOrDefault(tRef => tRef.Assembly.AssemblyName == assembly && tRef.FullName == fullname && tRef.GenericArguments.Count == 0);
            if (result == null)
            {
                result = ctx.Create<TypeRef>();
                result.FullName = fullname;
                result.Assembly = ctx.GetQuery<Assembly>().SingleOrDefault(a => a.AssemblyName == assembly);
                if (result.Assembly == null)
                {
                    result.Assembly = ctx.Create<Assembly>();
                    result.Assembly.AssemblyName = assembly;
                    result.Assembly.Module = ctx.Find<Module>(4); // GUI Module
                }
            }
            return result;
        }

        private void AddDefaultPMDescriptorToObjectClass()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("AddDefaultPMDescriptorToObjectClass"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var guiModule = ctx.Find<Module>(4);
                    var ocClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "ObjectClass");
                    var pmdClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "PresentableModelDescriptor");

                    var controlRel = ctx.Create<Relation>();
                    controlRel.Description = "This relation between a PresentableModelDescriptor and the described PresentableModel's Type";
                    controlRel.A = ctx.Create<RelationEnd>();
                    controlRel.A.RoleName = "Presentable";
                    controlRel.A.Type = ocClass;
                    controlRel.A.Multiplicity = Multiplicity.ZeroOrMore;
                    controlRel.A.Navigator = ctx.Create<ObjectReferenceProperty>();
                    controlRel.A.Navigator.CategoryTags = "Main";
                    controlRel.A.Navigator.Description = "The default PresentableModel to use for this ObjectClass";
                    controlRel.A.Navigator.Module = guiModule;
                    controlRel.A.Navigator.ObjectClass = ocClass;
                    controlRel.A.Navigator.PropertyName = "DefaultPresentableModelDescriptor";
                    controlRel.A.Navigator.ReferenceObjectClass = pmdClass;
                    controlRel.A.Role = (int)RelationEndRole.A;

                    controlRel.B = ctx.Create<RelationEnd>();
                    controlRel.B.RoleName = "DefaultPresentableModelDescriptor";
                    controlRel.B.Type = pmdClass;
                    controlRel.B.Multiplicity = Multiplicity.One;
                    controlRel.B.Role = (int)RelationEndRole.B;

                    controlRel.Storage = StorageType.MergeIntoA;

                    ctx.SubmitChanges();
                }
            }
        }

        private void CreatePresentableModelDescriptorClass()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("CreatePresentableModelDescriptorClass"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var guiModule = ctx.Find<Module>(4);
                    var typeRefClass = ctx.GetQuery<ObjectClass>().Single(oc => oc.ClassName == "TypeRef");

                    var pmdClass = ctx.Create<ObjectClass>();
                    pmdClass.ClassName = "PresentableModelDescriptor";
                    pmdClass.TableName = "PresentableModelDescriptors";
                    pmdClass.Module = guiModule;
                    pmdClass.IsFrozenObject = true;

                    var vtProp = ctx.Create<EnumerationProperty>();
                    vtProp.CategoryTags = "Summary,Main";
                    vtProp.Description = "The default visual type used for this PresentableModel";
                    vtProp.Enumeration = ctx.Find<Enumeration>(55);
                    vtProp.Module = guiModule;
                    vtProp.ObjectClass = pmdClass;
                    vtProp.PropertyName = "DefaultVisualType";

                    var descProp = ctx.Create<StringProperty>();
                    descProp.CategoryTags = "Summary,Main";
                    descProp.Description = "describe this PresentableModel";
                    descProp.Length = 4000;
                    descProp.Module = guiModule;
                    descProp.ObjectClass = pmdClass;
                    descProp.PropertyName = "Description";

                    var controlRel = ctx.Create<Relation>();
                    controlRel.Description = "This relation between a PresentableModelDescriptor and the described PresentableModel's Type";
                    controlRel.A = ctx.Create<RelationEnd>();
                    controlRel.A.RoleName = "Descriptor";
                    controlRel.A.Type = pmdClass;
                    controlRel.A.Multiplicity = Multiplicity.ZeroOrMore;
                    controlRel.A.Navigator = ctx.Create<ObjectReferenceProperty>();
                    controlRel.A.Navigator.CategoryTags = "Main";
                    controlRel.A.Navigator.Description = "The described CLR class' reference";
                    controlRel.A.Navigator.Module = guiModule;
                    controlRel.A.Navigator.ObjectClass = pmdClass;
                    controlRel.A.Navigator.PropertyName = "PresentableModelRef";
                    controlRel.A.Navigator.ReferenceObjectClass = typeRefClass;
                    controlRel.A.Role = (int)RelationEndRole.A;

                    controlRel.B = ctx.Create<RelationEnd>();
                    controlRel.B.RoleName = "PresentableModelRef";
                    controlRel.B.Type = typeRefClass;
                    controlRel.B.Multiplicity = Multiplicity.One;
                    controlRel.B.Role = (int)RelationEndRole.B;

                    controlRel.Storage = StorageType.MergeIntoA;

                    ctx.SubmitChanges();

                }
            }
        }

        private void CreateViewDescriptorClass()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Creating ViewDescriptor class for GUI"))
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
