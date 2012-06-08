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
    public class NavigationActionActions
    {
        private static IViewModelFactory _modelFactory;

        public NavigationActionActions(IViewModelFactory factory)
        {
            _modelFactory = factory;
        }

        [Invocation]
        public static void GetDefaultViewModel(Zetbox.App.GUI.NavigationAction obj, MethodReturnEventArgs<object> e, Zetbox.API.IZetboxContext dataCtx, System.Object parent)
        {
            e.Result = _modelFactory.CreateViewModel<NavigationActionViewModel.Factory>().Invoke(dataCtx, (ViewModel)parent, obj);
        }
    }
}
