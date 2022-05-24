namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
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
        private readonly System.Timers.Timer _timer;
        private readonly SynchronizationContext _syncContext;

        public NextEventsSummaryViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            _fetchCache = new FetchCache(ViewModelFactory, DataContext, this);
            _syncContext = SynchronizationContext.Current;

            _timer = new System.Timers.Timer();
            _timer.Interval = 60 * 1000;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            _timer.Dispose();
            base.Dispose(disposing);
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
                createTask: async () =>
                {
                    var now = DateTime.Now;
                    var tomorrow = now.Date.AddDays(2).AddSeconds(-1); // I know.... :-(

                    var fetchCalendar = await FetchCalendar();
                    _fetchCache.SetCalendars(fetchCalendar);
                    var fetchTaskFactory = await _fetchCache.FetchEventsAsync(now, tomorrow);

                    var lst = await fetchTaskFactory.SelectMany(e => e.CreateCalendarItemViewModels(now, tomorrow));
                    return lst
                        .OrderBy(i => i.From.Date)
                        .ThenByDescending(i => i.IsAllDay)
                        .ThenBy(i => i.From.TimeOfDay)
                        .ToList();
                },
                set: (IEnumerable<CalendarItemViewModel> value) =>
                {
                    throw new NotImplementedException();
                });
        }

        private async System.Threading.Tasks.Task<List<int>> FetchCalendar()
        {
            var myCalendars = await DataContext
                .GetQuery<CalendarBook>().Where(i => i.Owner.ID == CurrentPrincipal.ID)
                .ToListAsync();
            var config = await GetSavedConfigAsync();
            return myCalendars
                        .Select(i => i.ID)
                        .ToList()
                        .Union(config.Configs
                        .Select(c => c.Calendar))
                        .ToList();
        }

        protected async System.Threading.Tasks.Task<CalendarConfigurationList> GetSavedConfigAsync()
        {
            if (CurrentPrincipal == null) return new CalendarConfigurationList();
            var ctx = ViewModelFactory.CreateNewContext();
            var identity = await ctx.FindAsync<Identity>(CurrentPrincipal.ID);
            try
            {
                CalendarConfigurationList obj;
                try
                {
                    obj = !string.IsNullOrEmpty(identity.CalendarConfiguration) ? identity.CalendarConfiguration.FromXmlString<CalendarConfigurationList>() : new CalendarConfigurationList();
                }
                catch (Exception ex)
                {
                    Logging.Client.Warn("Error during deserializing CalendarConfigurationList, creating a new one", ex);
                    obj = new CalendarConfigurationList();
                }
                return obj;
            }
            finally
            {
                ctx.Dispose();
            }
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

        public Task<bool> CanOpen()
        {
            return Task.FromResult(SelectedItem != null);
        }

        public Task<string> CanOpenReason()
        {
            return Task.FromResult(CommonCommandsResources.DataObjectCommand_NothingSelected);
        }

        public async Task Open()
        {
            if (!(await CanOpen())) return;
            var newScope = ViewModelFactory.CreateNewScope();
            var newCtx = newScope.ViewModelFactory.CreateNewContext();

            var source = await SelectedItem.Event.Source.GetObject(newCtx);
            if (source != null && !source.CurrentAccessRights.HasReadRights())
            {
                ViewModelFactory.ShowMessage(CalendarResources.CannotOpenNoRightsMessage, CalendarResources.CannotOpenNoRightsCaption);
                return;
            }

            var ws = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);
            if (source != null)
                ws.ShowObject(source);
            else
                ws.ShowObject(SelectedItem.Event);
            ws.Closed += (s, e) =>
            {
                _fetchCache.Invalidate();
                Refresh(); // A dialog makes it easy to know when the time for a refresh has come
            };

            await ViewModelFactory.ShowModel(ws, true);
        }

        #endregion
    }
}
