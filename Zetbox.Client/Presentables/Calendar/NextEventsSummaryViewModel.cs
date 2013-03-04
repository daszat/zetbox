namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.App.Calendar;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class NextEventsSummaryViewModel : ViewModel, IRefreshCommandListener
    {
        public new delegate NextEventsSummaryViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public NextEventsSummaryViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "NextEventsSummaryViewModel"; }
        }

        public string NextEventsLabel
        {
            get
            {
                return CalendarResource.NextEventsLabel;
            }
        }

        private RefreshCommand _refreshCommand;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(
                        DataContext,
                        this);
                }
                return _refreshCommand;
            }
        }

        void IRefreshCommandListener.Refresh()
        {
            if (_nextEvents != null)
            {
                _nextEvents.Refresh();
            }
        }

        private InstanceListViewModel _nextEvents = null;
        public InstanceListViewModel NextEvents
        {
            get
            {
                if (_nextEvents == null)
                {
                    var now = DateTime.Now;
                    var tomorrow = now.AddDays(1);
                    var qry = DataContext.GetQuery<Event>()
                                    .Where(i => i.Calendar.Owner == CurrentIdentity)
                                    .Where(i => i.StartDate >= now && i.StartDate < tomorrow)
                                    .OrderBy(i => i.StartDate);
                    _nextEvents = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        typeof(Event).GetObjectClass(FrozenContext),
                        () => qry);
                    _nextEvents.EnableAutoFilter = false;
                    _nextEvents.ShowCommands = false;
                    _nextEvents.AllowFilter = false;
                    _nextEvents.AllowUserFilter = false;
                    _nextEvents.AllowSelectColumns = false;
                    _nextEvents.ViewMethod = InstanceListViewMethod.Details;
                }
                return _nextEvents;
            }
        }
    }
}
