namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;

    [Implementor]
    public class NavigationSearchScreenActions
    {
        private static IViewModelFactory _modelFactory;

        public NavigationSearchScreenActions(IViewModelFactory factory)
        {
            _modelFactory = factory;
        }

        [Invocation]
        public static void GetDefaultViewModel(Zetbox.App.GUI.NavigationSearchScreen obj, MethodReturnEventArgs<object> e, Zetbox.API.IZetboxContext dataCtx, System.Object parent)
        {
            e.Result = _modelFactory.CreateViewModel<NavigationSearchScreenViewModel.Factory>().Invoke(dataCtx, (ViewModel)parent, obj);
        }
    }
}
