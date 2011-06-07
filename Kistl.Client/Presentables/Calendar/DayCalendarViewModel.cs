namespace Kistl.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;

    public class DayCalendarViewModel : Kistl.Client.Presentables.ViewModel
    {
        public new delegate DayCalendarViewModel Factory(IKistlContext dataCtx, WeekCalendarViewModel parent, DateTime day);

        public DayCalendarViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, WeekCalendarViewModel parent, DateTime day)
            : base(dependencies, dataCtx, parent)
        {
            this.Day = day;
        }

        public WeekCalendarViewModel WeekCalendar
        {
            get
            {
                return (WeekCalendarViewModel)Parent;
            }
        }

        private DateTime _Day = DateTime.Today;
        public DateTime Day
        {
            get
            {
                return _Day;
            }
            set
            {
                if (_Day != value)
                {
                    _Day = value;
                }
            }
        }

        public string DayText
        {
            get
            {
                return Day.ToShortDateString();
            }
        }

        private List<TimeSlotItemViewModel> _TimeSlotItems;
        public IEnumerable<TimeSlotItemViewModel> TimeSlotItems
        {
            get
            {
                if (_TimeSlotItems == null)
                {
                    _TimeSlotItems = new List<TimeSlotItemViewModel>();
                    for (int i = 0; i < 24; i++)
                    {
                        _TimeSlotItems.Add(new TimeSlotItemViewModel(Day, i, 0));
                        _TimeSlotItems.Add(new TimeSlotItemViewModel(Day, i, 30));
                    }
                }
                return _TimeSlotItems;
            }
        }

        private List<CalendarItemViewModel> _CalendarItems;
        public IEnumerable<CalendarItemViewModel> CalendarItems
        {
            get
            {
                return _CalendarItems;
            }
            set
            {
                _CalendarItems = new List<CalendarItemViewModel>(value);
                foreach (var i in _CalendarItems)
                {
                    i.DayCalendar = this;
                }

                CalcOverlapping();
                OnPropertyChanged("CalendarItems");
            }
        }

        public void CalcOverlapping()
        {
            var overlappingChain = new List<CalendarItemViewModel>();
            foreach (var item in _CalendarItems.OrderBy(i => i.From))
            {
                if (overlappingChain.Count > 0)
                {
                    var last = overlappingChain.Max(i => i.To);
                    if (item.From < last)
                    {
                        // is overlapping, continue
                    }
                    else
                    {
                        CalcOverlappingChain(overlappingChain);
                        // Start a new one
                        overlappingChain = new List<CalendarItemViewModel>();
                    }
                }
                overlappingChain.Add(item);
            }

            CalcOverlappingChain(overlappingChain);
        }

        private static void CalcOverlappingChain(List<CalendarItemViewModel> overlappingChain)
        {
            if (overlappingChain.Count > 1)
            {
                // calc prev overlapping chain
                int idx = 0;
                foreach (var ovItem in overlappingChain)
                {
                    ovItem.OverlappingIndex = idx++;
                    ovItem.OverlappingWidth = 1.0 / (double)overlappingChain.Count;
                }
            }
        }

        private double _ActualWidth;
        public double ActualWidth
        {
            get
            {
                return _ActualWidth;
            }
            set
            {
                _ActualWidth = value;
                OnPropertyChanged("ActualWidth");
            }
        }

        public override string Name
        {
            get { return DayText; }
        }

        public void NewTermin(DateTime dt)
        {
            WeekCalendar.NewItem(dt);
        }
    }
}
