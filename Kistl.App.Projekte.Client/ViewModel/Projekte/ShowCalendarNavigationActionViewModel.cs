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

        private readonly Func<IKistlContext> _ctxFactory;

        public ShowCalendarNavigationActionViewModel(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ViewModel parent, NavigationAction screen)
            : base(appCtx, dataCtx, parent, screen)
        {
            _ctxFactory = ctxFactory;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            ViewModelFactory.ShowModel(ViewModelFactory.CreateViewModel<CalendarWorkspaceViewModel.Factory>().Invoke(_ctxFactory(), null), true);
        }
    }
}
