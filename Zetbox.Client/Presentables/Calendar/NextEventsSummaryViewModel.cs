namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Calendar;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class NextEventsSummaryViewModel : ViewModel, IRefreshCommandListener
    {
        public new delegate NextEventsSummaryViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        private readonly FetchCache _fetchCache;
        private readonly Func<IZetboxContext> _ctxFactory;
        private readonly System.Timers.Timer _timer;
        private readonly SynchronizationContext _syncContext;

        public NextEventsSummaryViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent)
        {
            _ctxFactory = ctxFactory;
            _fetchCache = new FetchCache(ViewModelFactory, DataContext, this);
            _syncContext = SynchronizationContext.Current;

            _timer = new System.Timers.Timer();
            _timer.Interval = 60 * 1000;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();
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
        public string CalendarLabel { get { return CalendarResources.CalendarLabel; } }
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

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_syncContext != null)
            {
                _syncContext.Post((s) => Refresh(), null);
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            _fetchCache.Invalidate();
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
                    var tomorrow = now.Date.AddDays(2).AddSeconds(-1); // I know.... :-(

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
                .GetQuery<CalendarBook>().Where(i => i.Owner.ID == CurrentPrincipal.ID)
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
            if (CurrentPrincipal == null) return new ZbTask<CalendarConfigurationList>(new CalendarConfigurationList());
            var ctx = _ctxFactory();
            var idenityTask = ctx.FindAsync<Identity>(CurrentPrincipal.ID);
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

        #region SelectedItem
        private CalendarItemViewModel _selectedItem;
        public CalendarItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        #endregion

        #region Open command
        private ICommandViewModel _OpenCommand = null;
        public ICommandViewModel OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, CommonCommandsResources.OpenDataObjectCommand_Name, CommonCommandsResources.OpenDataObjectCommand_Tooltip, Open, CanOpen, CanOpenReason);
                }
                return _OpenCommand;
            }
        }

        public bool CanOpen()
        {
            return SelectedItem != null;
        }

        public string CanOpenReason()
        {
            return CommonCommandsResources.DataObjectCommand_NothingSelected;
        }

        public void Open()
        {
            if (!CanOpen()) return;
            var ctx = _ctxFactory();
            var source = SelectedItem.Event.Source.GetObject(ctx);
            if (source != null && !source.CurrentAccessRights.HasReadRights())
            {
                ViewModelFactory.ShowMessage(CalendarResources.CannotOpenNoRightsMessage, CalendarResources.CannotOpenNoRightsCaption);
                return;
            }

            var ws = ViewModelFactory.CreateViewModel<ObjectEditor.WorkspaceViewModel.Factory>().Invoke(ctx, null);
            if (source != null)
                ws.ShowObject(source);
            else
                ws.ShowObject(SelectedItem.Event);
            ViewModelFactory.ShowDialog(ws, this);

            _fetchCache.Invalidate();
            Refresh(); // A dialog makes it easy to know when the time for a refresh has come
        }

        #endregion
    }
}
