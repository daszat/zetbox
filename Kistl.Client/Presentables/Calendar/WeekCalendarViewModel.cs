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
        public new delegate WeekCalendarViewModel Factory(IKistlContext dataCtx, Func<DateTime, DateTime, IEnumerable<CalendarItemViewModel>> source);

        public WeekCalendarViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, Func<DateTime, DateTime, IEnumerable<CalendarItemViewModel>> source)
            : base(dependencies, dataCtx)
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
                    _NextWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Nächste", "", NextWeek, null);
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
                    _PrevWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Vorherige", "", PrevWeek, null);
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
                    _ThisWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Heute", "", ThisWeek, null);
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
                    OnPropertyChanged("From");
                    OnPropertyChanged("To");
                    OnPropertyChanged("DayItems");
                    Refresh();
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
                        _DayItems.Add(ViewModelFactory.CreateViewModel<DayCalendarViewModel.Factory>().Invoke(DataContext, From.AddDays(i), this));
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

        private DataObjectViewModel _SelectedTermin;
        public DataObjectViewModel SelectedTermin
        {
            get
            {
                return _SelectedTermin;
            }
            set
            {
                if (_SelectedTermin != value)
                {
                    var vivm = FindCalendarItemViewModel(_SelectedTermin);
                    if (vivm != null)
                    {
                        vivm.IsSelected = false;
                    }

                    _SelectedTermin = value;
                    vivm = FindCalendarItemViewModel(_SelectedTermin);
                    vivm.IsSelected = true;

                    OnPropertyChanged("SelectedTermin");
                }
            }
        }

        private CalendarItemViewModel FindCalendarItemViewModel(DataObjectViewModel mdl)
        {
            if (mdl == null) return null;
            return DayItems.SelectMany(i => i.CalendarItems.Where(c => c.ObjectViewModel == mdl)).FirstOrDefault();
        }

        public void Refresh()
        {
            var allTermine = _Source(From, To);

            foreach (var day in DayItems)
            {
                day.CalendarItems = allTermine
                    .Where(i => i.From.Date == day.Day);
            }
        }

        public void NewTermin(DateTime dt)
        {
            OnNewTerminCreated(dt);
        }

        public delegate void NewTerminCreatedEventHandler(DateTime dt);
        public event NewTerminCreatedEventHandler NewTerminCreated;
        public void OnNewTerminCreated(DateTime dt)
        {
            var temp = NewTerminCreated;
            if (temp != null)
            {
                temp(dt);
            }
        }
    }
}
