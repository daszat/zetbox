namespace Kistl.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class ShowCalendarNavigationActionViewModel : NavigationActionViewModel
    {
        public new delegate ShowCalendarNavigationActionViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationAction screen);

        public ShowCalendarNavigationActionViewModel(IViewModelDependencies appCtx,
            IKistlContext dataCtx, ViewModel parent, NavigationAction screen)
            : base(appCtx, dataCtx, parent, screen)
        {
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            ViewModelFactory.ShowMessage("Opening calendar", "Hello World");
        }
    }
}
