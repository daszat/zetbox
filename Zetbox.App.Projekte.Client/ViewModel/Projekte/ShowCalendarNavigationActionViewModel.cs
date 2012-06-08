namespace Zetbox.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class ShowCalendarNavigationActionViewModel : NavigationActionViewModel
    {
        public new delegate ShowCalendarNavigationActionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationAction screen);

        private readonly Func<IZetboxContext> _ctxFactory;

        public ShowCalendarNavigationActionViewModel(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, ViewModel parent, NavigationAction screen)
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
