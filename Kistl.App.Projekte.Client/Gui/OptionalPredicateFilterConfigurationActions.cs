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
    public class OptionalPredicateFilterConfigurationActions
    {
        private static IViewModelFactory _factory;
        private static IFrozenContext ForzenContext;

        public OptionalPredicateFilterConfigurationActions(IFrozenContext frozenCtx, IViewModelFactory factory)
        {
            ForzenContext = frozenCtx;
            _factory = factory;
        }

        [Invocation]
        public static void CreateFilterModel(Kistl.App.GUI.OptionalPredicateFilterConfiguration obj, MethodReturnEventArgs<Kistl.API.IFilterModel> e)
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

        [Invocation]
        public static void NotifyCreated(Kistl.App.GUI.OptionalPredicateFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_OptionalPredicateFilterViewModel);
        }
    }
}
