namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.Presentables;

    [Implementor]
    public class NavigationScreenActions
    {
        private static IViewModelFactory _modelFactory;

        public NavigationScreenActions(IViewModelFactory factory)
        {
            _modelFactory = factory;
        }

        [Invocation]
        public static void GetDefaultViewModel(Kistl.App.GUI.NavigationScreen obj, MethodReturnEventArgs<object> e, Kistl.API.IKistlContext dataCtx, System.Object parent)
        {
            e.Result = _modelFactory.CreateViewModel<NavigationScreenViewModel.Factory>().Invoke(dataCtx, (ViewModel)parent, obj);
        }
    }
}
