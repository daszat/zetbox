namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;

    [Implementor]
    public class NavigationSearchScreenActions
    {
        private static IViewModelFactory _modelFactory;

        public NavigationSearchScreenActions(IViewModelFactory factory)
        {
            _modelFactory = factory;
        }

        [Invocation]
        public static void GetDefaultViewModel(Kistl.App.GUI.NavigationSearchScreen obj, MethodReturnEventArgs<object> e, Kistl.API.IKistlContext dataCtx, System.Object parent)
        {
            e.Result = _modelFactory.CreateViewModel<NavigationSearchScreenViewModel.Factory>().Invoke(dataCtx, (ViewModel)parent, obj);
        }
    }
}
