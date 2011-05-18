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
    public class MonthFilterConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public MonthFilterConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static void CreateFilterModel(Kistl.App.GUI.MonthFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            e.Result = MonthValueFilterModel.Create(FrozenContext, obj.GetLabel(), FilterValueSource.FromProperty(obj.Property), obj.IsCurrentMonthDefault ?? false);
        }

        [Invocation]
        public static void NotifyCreated(Kistl.App.GUI.MonthFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(NamedObjects.ViewModelDescriptor_SingleValueFilterViewModel);
        }
  
    }
}
