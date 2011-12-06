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
    using ViewModelDescriptors = Kistl.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class YearFilterConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public YearFilterConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static void CreateFilterModel(Kistl.App.GUI.YearFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            e.Result = YearValueFilterModel.Create(FrozenContext, obj.GetLabel(), FilterValueSource.FromProperty(obj.Property), obj.IsCurrentYearDefault ?? false);
        }

        [Invocation]
        public static void NotifyCreated(Kistl.App.GUI.YearFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Kistl_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(obj.Context);
        }
    }
}
