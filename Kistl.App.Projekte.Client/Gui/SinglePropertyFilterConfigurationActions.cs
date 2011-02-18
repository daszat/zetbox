namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.Client.Models;
    using Kistl.App.Base;
    using Kistl.Client;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class SinglePropertyFilterConfigurationActions
    {
        private static IViewModelFactory _factory;

        public SinglePropertyFilterConfigurationActions(IViewModelFactory factory)
        {
            _factory = factory;
        }

        [Invocation]
        public static void CreateFilterModel(Kistl.App.GUI.SinglePropertyFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            var mdl = new SingleValueFilterModel();
            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(true), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            if (obj.Property is StringProperty)
            {
                mdl.Operator = FilterOperators.Contains;
            }
            e.Result = mdl;
        }

        [Invocation]
        public static void NotifyCreated(Kistl.App.GUI.SinglePropertyFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel);
        }
    }
}
