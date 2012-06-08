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
    public static class RangeFilterConfigurationActions
    {
        [Invocation]
        public static void CreateFilterModel(Zetbox.App.GUI.RangeFilterConfiguration obj, MethodReturnEventArgs<Zetbox.API.IFilterModel> e)
        {
            var mdl = new RangeFilterModel();
            mdl.Label = obj.GetLabel();
            mdl.Required = obj.Required;
            mdl.ValueSource = FilterValueSource.FromProperty(obj.Property);

            mdl.ViewModelType = obj.ViewModelDescriptor;
            mdl.RequestedKind = obj.RequestedKind;

            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(true), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            mdl.FilterArguments.Add(new FilterArgumentConfig(obj.Property.GetDetachedValueModel(true), /*cfg.ArgumentViewModel ?? */ obj.Property.ValueModelDescriptor));
            e.Result = mdl;
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.GUI.RangeFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_RangeFilterViewModel.Find(obj.Context);
        }
    }
}
