namespace Kistl.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.API.Utils;

    [ViewModelDescriptor]
    public class WeekCalendarViewModel : Kistl.Client.Presentables.ViewModel
    {
        public new delegate WeekCalendarViewModel Factory(IKistlContext dataCtx, ViewModel parent, Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> source);

        public WeekCalendarViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> source)
            : base(dependencies, dataCtx, parent)
        {
            this._Source = source;
        }

        private ICommandViewModel _NextWeekCommand = null;
        public ICommandViewModel NextWeekCommand
        {
            get
            {
                if (_NextWeekCommand == null)
                {
                    _NextWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Nächste", "", NextWeek, null, null);
                }
                return _NextWeekCommand;
            }
        }

        public void NextWeek()
        {
            From = From.AddDays(7);
        }

        private ICommandViewModel _PrevWeekCommand = null;
        public ICommandViewModel PrevWeekCommand
        {
            get
            {
                if (_PrevWeekCommand == null)
                {
                    _PrevWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Vorherige", "", PrevWeek, null, null);
                }
                return _PrevWeekCommand;
            }
        }

        public void PrevWeek()
        {
            From = From.AddDays(-7);
        }

        private ICommandViewModel _ThisWeekCommand = null;
        public ICommandViewModel ThisWeekCommand
        {
            get
            {
                if (_ThisWeekCommand == null)
                {
                    _ThisWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Heute", "", ThisWeek, null, null);
                }
                return _ThisWeekCommand;
            }
        }

        public void ThisWeek()
        {
            From = DateTime.Today.FirstWeekDay();
        }


        private ICommandViewModel _RefreshCommand = null;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        "Aktualisieren",
                        "Die angezeigten Daten neu laden",
                        LoadItems,
                        null,
                        null);
                }
                return _RefreshCommand;
            }
        }

        private DateTime _From = DateTime.Today.FirstWeekDay();
        public DateTime From
        {
            get
            {
                return _From;
            }
            set
            {
                if (_From != value)
                {
                    _From = value;
                    _DayItems = null;
                    LoadItems(); // Get new data
                    OnPropertyChanged("From");
                    OnPropertyChanged("To");
                    OnPropertyChanged("DayItems");
                }
            }
        }

        public DateTime To
        {
            get
            {
                return _From.AddDays(7);
            }
        }

        private List<DayCalendarViewModel> _DayItems;
        public IEnumerable<DayCalendarViewModel> DayItems
        {
            get
            {
                if (_DayItems == null)
                {
                    _DayItems = new List<DayCalendarViewModel>();
                    for (int i = 0; i < 7; i++)
                    {
                        _DayItems.Add(ViewModelFactory.CreateViewModel<DayCalendarViewModel.Factory>().Invoke(DataContext, this, From.AddDays(i)));
                    }
                }
                return _DayItems;
            }
        }

        public override string Name
        {
            get { return "Wochenkalender"; }
        }

        private Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> _Source = null;
        public Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> Source
        {
            get { return _Source; }
            set
            {
                _Source = value;
                LoadItems();
            }
        }

        private IAppointmentViewModel _selectedItem;
        public IAppointmentViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    var vivms = FindCalendarItemViewModel(_selectedItem);
                    if (vivms != null) vivms.ForEach(i => i.IsSelected = false);

                    _selectedItem = value;
                    vivms = FindCalendarItemViewModel(_selectedItem);
                    if (vivms != null) vivms.ForEach(i => i.IsSelected = true);

                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private IEnumerable<CalendarItemViewModel> FindCalendarItemViewModel(IAppointmentViewModel mdl)
        {
            if (mdl == null) return null;
            return DayItems.SelectMany(i => i.CalendarItems.Where(c => c.ObjectViewModel == mdl));
        }

        public void UpdateItems()
        {
            if (_allItems == null) LoadItemsInternal();
            foreach (var day in DayItems)
            {
                day.CalendarItems = _allItems
                    .Where(i => i.From.Date == day.Day);
            }
        }

        public void LoadItems()
        {
            LoadItemsInternal();
            UpdateItems();
        }

        private List<CalendarItemViewModel> _allItems;
        private List<IAppointmentViewModel> _allAppointments;
        private void LoadItemsInternal()
        {
            if(_allAppointments != null)
            {
                foreach (var a in _allAppointments)
                {
                    a.Changed -= appointment_Changed;
                }
            }

            _allAppointments = _Source(From, To).ToList();
            _allItems = new List<CalendarItemViewModel>();

            foreach (var a in _allAppointments)
            {
                var items = CreateCalendarItemViewModel(a);
                if (items != null && items.Count > 0) _allItems.AddRange(items);
            }
        }

        private List<CalendarItemViewModel> CreateCalendarItemViewModel(IAppointmentViewModel a)
        {
            a.Changed += appointment_Changed;
            if (a.From <= a.Until)
            {
                List<CalendarItemViewModel> result = new List<CalendarItemViewModel>();
                for (var current = a.From; current < a.Until; current = current.Date.AddDays(1))
                {
                    var vmdl = ViewModelFactory.CreateViewModel<CalendarItemViewModel.Factory>()
                    .Invoke(
                        DataContext,
                        this,
                        a);
                    vmdl.From = current == a.From ? current : current.Date;
                    vmdl.Until = current.Date == a.Until.Date ? a.Until : current.Date.AddDays(1);

                    vmdl.IsAllDay = vmdl.From.TimeOfDay == TimeSpan.Zero && vmdl.Until.TimeOfDay == TimeSpan.Zero;
                    result.Add( vmdl);
                }
                return result;
            }
            else
            {
                Logging.Client.WarnFormat("Appointment item {0} has an invalid time range of {1} - {2}", a.Subject, a.From, a.Until);
                return null;
            }
        }

        void appointment_Changed(object sender, EventArgs e)
        {
            UpdateItems();
        }


        public void NewItem(DateTime dt)
        {
            var result = new NewItemCreatingEventArgs();
            OnNewItemCreating(dt, result);

            if (result.AppointmentViewModel == null)
            {
                // Abort
                return;
            }

            if (_allItems == null) LoadItemsInternal();
            var items = CreateCalendarItemViewModel(result.AppointmentViewModel);
            if (items != null && items.Count > 0)
            {
                _allItems.AddRange(items);
                UpdateItems();
                SelectedItem = result.AppointmentViewModel;
            }
        }

        /// <summary>
        /// Fired when a new Items should be created. The receiver is responsible for createing the new Item plus the corresponding Calender Item ViewModel.
        /// If either CalendarViewModel or ObjectViewModel of the result is null, the operation will be aborted.
        /// </summary>
        public event NewItemCreatingEventHandler NewItemCreating;
        protected void OnNewItemCreating(DateTime dt, NewItemCreatingEventArgs e)
        {
            var temp = NewItemCreating;
            if (temp != null)
            {
                temp(dt, e);
            }
        }

        public static readonly string DefaultColor = "#F1F5E3";
    }

    public class NewItemCreatingEventArgs : EventArgs
    {
        public IAppointmentViewModel AppointmentViewModel;
    }

    public delegate void NewItemCreatingEventHandler(DateTime dt, NewItemCreatingEventArgs e);

}
