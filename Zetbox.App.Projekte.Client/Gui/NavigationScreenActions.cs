namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables;

    [Implementor]
    public class NavigationScreenActions
    {
        private static IViewModelFactory _modelFactory;

        public NavigationScreenActions(IViewModelFactory factory)
        {
            _modelFactory = factory;
        }

        [Invocation]
        public static void GetDefaultViewModel(Zetbox.App.GUI.NavigationScreen obj, MethodReturnEventArgs<object> e, Zetbox.API.IZetboxContext dataCtx, System.Object parent)
        {
            e.Result = _modelFactory.CreateViewModel<NavigationScreenViewModel.Factory>().Invoke(dataCtx, (ViewModel)parent, obj);
        }
    }
}
