namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class OptionalPredicateFilterConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public OptionalPredicateFilterConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static void CreateFilterModel(Zetbox.App.GUI.OptionalPredicateFilterConfiguration obj, MethodReturnEventArgs<Zetbox.API.IFilterModel> e)
        {
            var mdl = new OptionalPredicateFilterModel();
            mdl.Label = obj.Label;
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromExpression(obj.Predicate);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.RequestedKind = obj.RequestedKind;

            var valueMdl = new BoolValueModel("", "", false, false);
            valueMdl.Value = false;
            mdl.FilterArguments.Add(new FilterArgumentConfig(valueMdl, /*cfg.ArgumentViewModel ?? */ ViewModelDescriptors.Zetbox_Client_Presentables_ValueViewModels_NullableBoolPropertyViewModel.Find(FrozenContext)));

            e.Result = mdl;
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.GUI.OptionalPredicateFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_OptionalPredicateFilterViewModel.Find(obj.Context);
        }
    }
}
