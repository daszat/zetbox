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
    public class DateRangeFilterConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public DateRangeFilterConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static void CreateFilterModel(Zetbox.App.GUI.DateRangeFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            e.Result = DateRangeFilterModel.Create(
                FrozenContext,
                obj.GetLabel(),
                FilterValueSource.FromProperty(obj.Property),
                obj.RequestedKind,
                obj.IsCurrentYearDefault ?? false,
                obj.IsCurrentQuaterDefault ?? false,
                obj.IsCurrentMonthDefault ?? false);
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.GUI.DateRangeFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_DateRangeFilterViewModel
               .Find(obj.Context);
        }
    }
}
