
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.GUI;

    [ViewModelDescriptor("GUI",
        DefaultKind = "Kistl.App.GUI.Navigator",
        Description = "Container for navigating between NavigationScreens. Displays the current position in the screen hierarchy and allows forward/backward stepping in the history.")]
    public class NavigatorViewModel
        : WindowViewModel
    {
        public new delegate NavigatorViewModel Factory(IKistlContext dataCtx, NavigationScreen root);

        private readonly NavigationScreen _root;
        private NavigationScreen _current;

        public NavigatorViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, NavigationScreen root)
            : base(dependencies, dataCtx)
        {
            _current = _root = root;
        }

        public override string Name
        {
            get { return _current.Title; }
        }
    }
}
