
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

    public class CustomClientActions_GUI
    {
        private static IFrozenContext ForzenContext;

        public CustomClientActions_GUI(IFrozenContext frozenCtx)
        {
            ForzenContext = frozenCtx;
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
                obj.DefaultKind,
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
        public static readonly Guid ViewModelDescriptor_SingleValueFilterViewModel = new Guid("4FF2B6EC-A47F-431B-AA6D-D10B39F8D628");
        public static readonly Guid ViewModelDescriptor_OptionalPredicateFilterViewModel = new Guid("29550c38-e240-46e4-b856-c7066d8395eb");
        public static readonly Guid ViewModelDescriptor_RangeFilterViewModel = new Guid("47ea2958-1036-4fe2-afb5-84568651d817");
        public static readonly Guid ViewModelDescriptor_NullableValuePropertyModel_Bool = new Guid("09d1f453-d0d9-429e-88e7-e84b33de7c2e");

        #region SinglePropertyFilterConfiguration
        public static void OnCreateFilterModel_SinglePropertyFilterConfiguration(Kistl.App.GUI.SinglePropertyFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            var mdl = new SingleValueFilterModel();
            mdl.Label = !string.IsNullOrEmpty(obj.Label) ? obj.Label : (!string.IsNullOrEmpty(obj.Property.Label) ? obj.Property.Label : obj.Property.Name);
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
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_SingleValueFilterViewModel);
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
            mdl.FilterArguments.Add(new FilterArgumentConfig(valueMdl, /*cfg.ArgumentViewModel ?? */ ForzenContext.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_NullableValuePropertyModel_Bool)));
            
            e.Result = mdl;
        }

        public static void OnNotifyCreated_OptionalPredicateFilterConfiguration(Kistl.App.GUI.OptionalPredicateFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_OptionalPredicateFilterViewModel);
        }

        #endregion

        #region RangeFilterConfiguration
        public static void OnCreateFilterModel_RangeFilterConfiguration(Kistl.App.GUI.RangeFilterConfiguration obj, MethodReturnEventArgs<Kistl.API.IFilterModel> e)
        {
            var mdl = new RangeFilterModel();
            mdl.Label = !string.IsNullOrEmpty(obj.Label) ? obj.Label : (!string.IsNullOrEmpty(obj.Property.Label) ? obj.Property.Label : obj.Property.Name);
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            e.Result = mdl;
        }

        public static void OnNotifyCreated_RangeFilterConfiguration(Kistl.App.GUI.RangeFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_RangeFilterViewModel);
        }
        #endregion



        #endregion
    }
}
