
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.GUI;

    [ViewModelDescriptor("GUI",
        DefaultKind = "Kistl.App.GUI.NavigationScreen",
        Description = "Displays the NavigationScreen and the reachable Children, either as list or tree.")]
    public class NavigationScreenViewModel
        : ViewModel
    {
        public new delegate NavigationScreenViewModel Factory(IKistlContext dataCtx, NavigationScreen screen);

        private readonly NavigationScreen _screen;

        public NavigationScreenViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreen screen)
            : base(dependencies, dataCtx)
        {
            _screen = screen;
        }

        public override string Name
        {
            get { return _screen.Title; }
        }
    }
}
