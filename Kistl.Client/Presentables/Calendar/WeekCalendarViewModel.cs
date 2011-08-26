namespace Kistl.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;
    using Kistl.API;
    using Kistl.Client.Presentables;

    [ViewModelDescriptor]
    public class WeekCalendarViewModel : Kistl.Client.Presentables.ViewModel
    {
        public new delegate WeekCalendarViewModel Factory(IKistlContext dataCtx, ViewModel parent, Func<DateTime, DateTime, IEnumerable<CalendarItemViewModel>> source);

        public WeekCalendarViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, Func<DateTime, DateTime, IEnumerable<CalendarItemViewModel>> source)
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

        private Func<DateTime, DateTime, IEnumerable<CalendarItemViewModel>> _Source = null;

        private ViewModel _selectedItem;
        public ViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    var vivm = FindCalendarItemViewModel(_selectedItem);
                    if (vivm != null) vivm.IsSelected = false;

                    _selectedItem = value;
                    vivm = FindCalendarItemViewModel(_selectedItem);
                    if(vivm != null) vivm.IsSelected = true;

                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private CalendarItemViewModel FindCalendarItemViewModel(ViewModel mdl)
        {
            if (mdl == null) return null;
            return DayItems.SelectMany(i => i.CalendarItems.Where(c => c.ObjectViewModel == mdl)).FirstOrDefault();
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
        private void LoadItemsInternal()
        {
            _allItems = _Source(From, To).ToList();
        }


        public void NewItem(DateTime dt)
        {
            var result = new NewItemCreatingEventArgs();
            OnNewItemCreating(dt, result);

            if (result.CalendarViewModel == null || result.ObjectViewModel == null)
            {
                // Abort
                return;
            }

            if (_allItems == null) LoadItemsInternal();
            _allItems.Add(result.CalendarViewModel);
            UpdateItems();
            SelectedItem = result.ObjectViewModel;
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
    }

    public class NewItemCreatingEventArgs : EventArgs
    {
        public DataObjectViewModel ObjectViewModel;
        public CalendarItemViewModel CalendarViewModel;
    }

    public delegate void NewItemCreatingEventHandler(DateTime dt, NewItemCreatingEventArgs e);

}
