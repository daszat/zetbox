namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class DateRangeConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public DateRangeConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static void CreateFilterModel(Kistl.App.GUI.DateRangeFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
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
        public static void NotifyCreated(Kistl.App.GUI.DateRangeFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_DateRangeFilterViewModel);
        }
    }
}
