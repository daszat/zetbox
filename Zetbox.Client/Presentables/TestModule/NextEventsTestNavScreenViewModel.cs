namespace Zetbox.Client.Presentables.TestModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;
using Zetbox.Client.Presentables.Calendar;

    [ViewModelDescriptor]
    public class NextEventsTestNavScreenViewModel : NavigationScreenViewModel
    {
        public new delegate NextEventsTestNavScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);


        public NextEventsTestNavScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
        }

        private NextEventsSummaryViewModel _nextEvents;
        public NextEventsSummaryViewModel NextEvents
        {
            get
            {
                if (_nextEvents == null)
                {
                    _nextEvents = ViewModelFactory.CreateViewModel<NextEventsSummaryViewModel.Factory>().Invoke(DataContext, this);
                }
                return _nextEvents;
            }
        }
    }
}
