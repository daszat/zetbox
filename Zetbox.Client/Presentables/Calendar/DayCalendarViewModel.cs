namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;

    public class DayCalendarViewModel : Zetbox.Client.Presentables.ViewModel
    {
        public new delegate DayCalendarViewModel Factory(IZetboxContext dataCtx, WeekCalendarViewModel parent, DateTime day);

        public DayCalendarViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, WeekCalendarViewModel parent, DateTime day)
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
                return string.Format("{0}, {1}", Day.ToString("ddd"), Day.ToShortDateString());
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
                OnPropertyChanged("DayItems");
                OnPropertyChanged("AllDayItems");
            }
        }

        public IEnumerable<CalendarItemViewModel> DayItems
        {
            get
            {
                if (_CalendarItems == null) return null;
                return _CalendarItems.Where(i => i.IsAllDay == false);
            }
        }

        public IEnumerable<CalendarItemViewModel> AllDayItems
        {
            get
            {
                if (_CalendarItems == null) return null;
                return _CalendarItems.Where(i => i.IsAllDay == true);
            }
        }

        public void CalcOverlapping()
        {
            var overlappingChain = new List<CalendarItemViewModel>();
            foreach (var item in DayItems.OrderBy(i => i.From))
            {
                if (overlappingChain.Count > 0)
                {
                    var last = overlappingChain.Max(i => i.Until);
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
                // allocate items into slots: 
                // slots.Count is the number of required slots.
                // slots are filled left-to-right with the next available item.
                // when a slot is empty, we set it to null.
                var slots = new List<CalendarItemViewModel>();
                // remember the last time a slot was filled
                var openSince = new List<DateTime>();
                foreach (var ovItem in overlappingChain
                    .SelectMany(i => new[]
                        {
                            new { Item = i, Time = i.From, Start = true , Duration = (i.Until - i.From).TotalHours },
                            new { Item = i, Time = i.Until,   Start = false, Duration = (i.Until - i.From).TotalHours },
                        })
                    .OrderBy(i => i.Time)
                    .ThenBy(i => i.Item.From)
                    .ThenByDescending(i => i.Duration))
                {
                    if (ovItem.Start)
                    {
                        // add item into first free slot
                        int i = 0;
                        for (; i < slots.Count; i++)
                        {
                            if (slots[i] == null)
                            {
                                slots[i] = ovItem.Item;
                                openSince[i] = ovItem.Item.Until;
                                break;
                            }
                        }
                        // no free slot found, adding at the end
                        if (i == slots.Count)
                        {
                            slots.Add(ovItem.Item);
                            openSince.Add(ovItem.Item.Until);
                        }
                    }
                    else
                    {
                        // remove item from slots after calculating best width
                        int idx = -1;
                        for (int i = 0; i < slots.Count; i++)
                        {
                            if (slots[i] == ovItem.Item)
                            {
                                // found item. delete, initialise
                                slots[i] = null;
                                ovItem.Item.OverlappingIndex = idx = i;
                                ovItem.Item.OverlappingWidth = i < slots.Count - 1 ? 1 : -1; // can already be in last slot
                            }
                            else if (idx >= 0 && (slots[i] != null || openSince[i] > ovItem.Item.From))
                            {
                                // blocked!
                                break;
                            }
                            else if (idx >= 0 && slots[i] == null && openSince[i] <= ovItem.Item.From)
                            {
                                if (i < slots.Count - 1)
                                {
                                    // empty adjacent slot
                                    ovItem.Item.OverlappingWidth += 1;
                                }
                                else // if (i == slots.Count - 1)
                                {
                                    // reached end of slots, might be extended later
                                    ovItem.Item.OverlappingWidth = -1;
                                }
                            }
                        }
                    }
                }

                // Normalize width to needed slots, extend items with undetermined right side to fill all available space
                foreach (var item in overlappingChain)
                {
                    if (item.OverlappingWidth < 0)
                    {
                        item.OverlappingWidth = slots.Count - item.OverlappingIndex;
                    }
                    item.SlotWidth = 1.0d / slots.Count;
                    item.OverlappingWidth /= slots.Count;
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
