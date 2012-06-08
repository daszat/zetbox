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
    public static class SinglePropertyFilterConfigurationActions
    {
        [Invocation]
        public static void CreateFilterModel(Zetbox.App.GUI.SinglePropertyFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            var mdl = new SingleValueFilterModel();
            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.RequestedKind = obj.RequestedKind;

            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(true), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            if (obj.Property is StringProperty)
            {
                mdl.Operator = FilterOperators.Contains;
            }
            else if (obj.Property is EnumerationProperty)
            {
                mdl.RefreshOnFilterChanged = true;
            }
            else if (obj.Property is ObjectReferenceProperty)
            {
                mdl.RefreshOnFilterChanged = true;
            }
            e.Result = mdl;
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.GUI.SinglePropertyFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(obj.Context);
        }
    }
}
