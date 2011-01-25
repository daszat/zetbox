
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.FilterViewModels;
    using Kistl.Client;
    using Kistl.Client.Presentables;

    public class CustomClientActions_GUI
    {
        private static IFrozenContext ForzenContext;
        private static IViewModelFactory _factory;

        public CustomClientActions_GUI(IFrozenContext frozenCtx, IViewModelFactory factory)
        {
            ForzenContext = frozenCtx;
            _factory = factory;
        }

        public static void OnToString_Icon(Icon obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.IconFile;
        }

        public static void OnToString_Visual(Visual obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Method != null)
            {
                e.Result = String.Format("Method Visual: {0}", obj.Method);
            }
            else if (obj.Property != null)
            {
                e.Result = String.Format("Property Visual: {0}", obj.Property);
            }
            else
            {
                e.Result = String.Format("Other Visual: {0}", obj.Description);
            }
        }

        public static void OnToString_ViewModelDescriptor(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} (default: {1}) [{2}]",
                obj.Description,
                obj.DefaultEditorKind,
                obj.ViewModelRef == null ? "(no type)" : obj.ViewModelRef.ToString());
        }


        public static void OnPrepareDefault_Template(Template obj, ObjectClass cls)
        {
            var displayedType = cls.GetDataType();
            obj.DisplayedTypeAssembly = obj.Context.GetQuery<Assembly>().Where(assembly => assembly.Name == displayedType.Assembly.FullName).SingleOrDefault();
            obj.DisplayedTypeFullName = displayedType.FullName;
            obj.DisplayName = String.Format("Default Template for {0}", displayedType.Name);
            //obj.VisualTree = TemplateHelper.CreateDefaultVisualTree(obj.Context, cls);
        }

        public static void OnToString_ViewDescriptor(ViewDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}/{1}: {2}",
                obj.Toolkit,
                obj.ControlKind != null ? obj.ControlKind.Name : "(unknown kind)",
                obj.ControlRef == null
                    ? "(none)"
                    : obj.ControlRef.ToString());
        }

        public static void OnToString_NavigationScreen(Kistl.App.GUI.NavigationScreen obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = String.Format("Screen: {0}",
                  obj.Title);
        }

        #region Filter

        public static void OnGetLabel_FilterConfiguration(Kistl.App.GUI.FilterConfiguration obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : string.Empty;
        }

        public static void OnGetLabel_PropertyFilterConfiguration(Kistl.App.GUI.PropertyFilterConfiguration obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Property.GetLabel();
        }

        #region SinglePropertyFilterConfiguration
        public static void OnCreateFilterModel_SinglePropertyFilterConfiguration(Kistl.App.GUI.SinglePropertyFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            var mdl = new SingleValueFilterModel();
            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            if (obj.Property is StringProperty)
            {
                mdl.Operator = FilterOperators.Contains;
            }
            e.Result = mdl;
        }

        public static void OnNotifyCreated_SinglePropertyFilterConfiguration(Kistl.App.GUI.SinglePropertyFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel);
        }
        #endregion

        #region MonthFilterConfiguration
        public static void OnCreateFilterModel_MonthFilterConfiguration(Kistl.App.GUI.MonthFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            var mdl = new MonthValueFilterModel();
            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(), /*cfg.ArgumentViewModel ?? */ obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableMonthPropertyViewModel)));

            if (obj.IsCurrentMonthDefault == true)
            {
                // Defaults to this month
                ((DateTimeValueModel)mdl.FilterArguments[0].Value).Value = DateTime.Today.FirstMonthDay();
            }
            
            e.Result = mdl;
        }

        public static void OnNotifyCreated_MonthFilterConfiguration(Kistl.App.GUI.MonthFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel);
        }
        #endregion

        #region OptionalPredicateFilterConfiguration
        public static void OnCreateFilterModel_OptionalPredicateFilterConfiguration(Kistl.App.GUI.OptionalPredicateFilterConfiguration obj, MethodReturnEventArgs<Kistl.API.IFilterModel> e)
        {
            var mdl = new OptionalPredicateFilterModel();
            mdl.Label = obj.Label;
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromExpression(obj.Predicate);

            mdl.ViewModelType = obj.ViewModelDescriptor;

            var valueMdl = new NullableStructValueModel<bool>("", "", false, false);
            valueMdl.Value = false;
            mdl.FilterArguments.Add(new FilterArgumentConfig(valueMdl, /*cfg.ArgumentViewModel ?? */ ForzenContext.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_NullableValuePropertyModel_Bool)));
            
            e.Result = mdl;
        }

        public static void OnNotifyCreated_OptionalPredicateFilterConfiguration(Kistl.App.GUI.OptionalPredicateFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_OptionalPredicateFilterViewModel);
        }

        #endregion

        #region RangeFilterConfiguration
        public static void OnCreateFilterModel_RangeFilterConfiguration(Kistl.App.GUI.RangeFilterConfiguration obj, MethodReturnEventArgs<Kistl.API.IFilterModel> e)
        {
            var mdl = new RangeFilterModel();
            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            e.Result = mdl;
        }

        public static void OnNotifyCreated_RangeFilterConfiguration(Kistl.App.GUI.RangeFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_RangeFilterViewModel);
        }
        #endregion

        #endregion

        #region Icon
        public static void OnUpload_Icon(Kistl.App.GUI.Icon obj)
        {
            // UI Code in Custom Actions!
            // ASP.NET would have a big Problem with that function
            string path = _factory.GetSourceFileNameFromUser();
            if (!string.IsNullOrEmpty(path))
            {
                var fi = new System.IO.FileInfo(path);
                int id = obj.Context.CreateBlob(fi, fi.GetMimeType());
                obj.Blob = obj.Context.Find<Kistl.App.Base.Blob>(id);
                obj.IconFile = obj.Blob.OriginalName;
            }
        }
        public static void OnOpen_Icon(Kistl.App.GUI.Icon obj)
        {
            if (obj.Blob != null)
            {
                obj.Blob.Open();
            }
        }
        #endregion
    }
}
