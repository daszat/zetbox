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
    using Zetbox.API.Async;
    using Zetbox.App.Base;
    using Zetbox.API.Utils;

    [ViewModelDescriptor]
    public class NextEventsSummaryViewModel : ViewModel, IRefreshCommandListener
    {
        public new delegate NextEventsSummaryViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        private FetchCache _fetchCache;
        private Func<IZetboxContext> _ctxFactory;

        public NextEventsSummaryViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent)
        {
            _ctxFactory = ctxFactory;
            _fetchCache = new FetchCache(ViewModelFactory, DataContext, this);
        }

        public override string Name
        {
            get { return "NextEventsSummaryViewModel"; }
        }

        #region Labels
        public string NextEventsLabel { get { return CalendarResources.NextEventsLabel; } }
        public string DateLabel { get { return CalendarResources.DateLabel; } }
        public string FromLabel { get { return CalendarResources.FromLabel; } }
        public string UntilLabel { get { return CalendarResources.UntilLabel; } }
        public string SummaryLabel { get { return CalendarResources.SummaryLabel; } }
        public string LocationLabel { get { return CalendarResources.LocationLabel; } }
        #endregion

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
            if (_NextEventsTask != null)
                _NextEventsTask.Invalidate();
            OnPropertyChanged("NextEvents");
            OnPropertyChanged("NextEventsAsync");
        }

        #region NextEventsAsync

        private PropertyTask<IEnumerable<CalendarItemViewModel>> _NextEventsTask;
        private PropertyTask<IEnumerable<CalendarItemViewModel>> EnsureNextEventsTask()
        {
            if (_NextEventsTask != null) return _NextEventsTask;

            return _NextEventsTask = new PropertyTask<IEnumerable<CalendarItemViewModel>>(
                notifier: () =>
                {
                    OnPropertyChanged("NextEvents");
                    OnPropertyChanged("NextEventsAsync");
                },
                createTask: () =>
                {
                    var now = DateTime.Now;
                    var tomorrow = now.AddDays(1);

                    var fetchCalendar = FetchCalendar();
                    var fetchTaskFactory = new ZbTask<ZbTask<IEnumerable<EventViewModel>>>(fetchCalendar)
                        .OnResult(t =>
                        {
                            _fetchCache.SetCalendars(fetchCalendar.Result);
                            t.Result = _fetchCache.FetchEventsAsync(now, tomorrow);
                        });
                    return new ZbFutureTask<IEnumerable<EventViewModel>, IEnumerable<CalendarItemViewModel>>(fetchTaskFactory)
                        .OnResult(t =>
                        {
                            t.Result = fetchTaskFactory
                                .Result
                                .Result
                                .SelectMany(e => e.CreateCalendarItemViewModels(now, tomorrow))
                                .OrderBy(i => i.From.Date)
                                .ThenByDescending(i => i.IsAllDay)
                                .ThenBy(i => i.From.TimeOfDay)
                                .ToList();
                        });
                },
                set: (IEnumerable<CalendarItemViewModel> value) =>
                {
                    throw new NotImplementedException();
                });
        }

        private ZbTask<List<int>> FetchCalendar()
        {
            var myCalendarsTask = DataContext
                .GetQuery<Calendar>().Where(i => i.Owner == CurrentIdentity)
                .ToListAsync();
            var configTask = GetSavedConfigAsync();
            return new ZbTask<List<int>>(new ZbTask[] { myCalendarsTask, configTask })
                .OnResult(t =>
                {
                    t.Result = myCalendarsTask.Result
                        .Select(i => i.ID)
                        .ToList()
                        .Union(configTask.Result.Configs
                        .Select(c => c.Calendar))
                        .ToList();
                });
        }

        protected ZbTask<CalendarConfigurationList> GetSavedConfigAsync()
        {
            if (CurrentIdentity == null) return new ZbTask<CalendarConfigurationList>(new CalendarConfigurationList());
            var ctx = _ctxFactory();
            var idenityTask = ctx.FindAsync<Identity>(CurrentIdentity.ID);
            return new ZbTask<CalendarConfigurationList>(idenityTask)
                .OnResult(t =>
                {
                    try
                    {
                        CalendarConfigurationList obj;
                        var identity = idenityTask.Result;
                        try
                        {
                            obj = !string.IsNullOrEmpty(identity.CalendarConfiguration) ? identity.CalendarConfiguration.FromXmlString<CalendarConfigurationList>() : new CalendarConfigurationList();
                        }
                        catch (Exception ex)
                        {
                            Logging.Client.Warn("Error during deserializing CalendarConfigurationList, creating a new one", ex);
                            obj = new CalendarConfigurationList();
                        }
                        t.Result = obj;
                    }
                    finally
                    {
                        ctx.Dispose();
                    }
                });
        }


        public IEnumerable<CalendarItemViewModel> NextEvents
        {
            get { return EnsureNextEventsTask().Get(); }
        }

        public IEnumerable<CalendarItemViewModel> NextEventsAsync
        {
            get { return EnsureNextEventsTask().GetAsync(); }
        }

        #endregion
    }
}
