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
    public class YearFilterConfigurationActions
    {
        private static IFrozenContext FrozenContext;

        public YearFilterConfigurationActions(IFrozenContext frozenCtx)
        {
            FrozenContext = frozenCtx;
        }

        [Invocation]
        public static void CreateFilterModel(Zetbox.App.GUI.YearFilterConfiguration obj, MethodReturnEventArgs<IFilterModel> e)
        {
            e.Result = YearValueFilterModel.Create(FrozenContext, obj.GetLabel(), FilterValueSource.FromProperty(obj.Property), obj.IsCurrentYearDefault ?? false);
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.GUI.YearFilterConfiguration obj)
        {
            obj.ViewModelDescriptor = ViewModelDescriptors.Zetbox_Client_Presentables_FilterViewModels_SingleValueFilterViewModel.Find(obj.Context);
        }
    }
}
