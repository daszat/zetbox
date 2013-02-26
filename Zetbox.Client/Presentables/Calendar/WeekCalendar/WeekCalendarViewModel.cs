// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class WeekCalendarViewModel : Zetbox.Client.Presentables.ViewModel, ICalendarDisplayViewModel
    {
        public new delegate WeekCalendarViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Func<DateTime, DateTime, ZbTask<IEnumerable<EventViewModel>>> source);

        public WeekCalendarViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, Func<DateTime, DateTime, ZbTask<IEnumerable<EventViewModel>>> source)
            : base(dependencies, dataCtx, parent)
        {
            if (source == null) throw new ArgumentNullException("source");
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
                    _NextWeekCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.forward_png.Find(FrozenContext));
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
                    _PrevWeekCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.back_png.Find(FrozenContext));
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
                    _ThisWeekCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.date_png.Find(FrozenContext));
                }
                return _ThisWeekCommand;
            }
        }

        public void ThisWeek()
        {
            From = DateTime.Today.FirstWeekDay();
        }

        public void Refresh()
        {
            var taskFrom = From;
            var taskTo = To;
            _Source(From, To).OnResult(t =>
            {
                if (From == taskFrom && To == taskTo)
                {
                    var allItems = t.Result
                        .SelectMany(e => CreateCalendarItemViewModels(e))
                        .ToLookup(c => c.From.Date);

                    foreach (var day in DayItems)
                    {
                        day.CalendarItems = allItems[day.Day];
                    }
                }
                FindCalendarItemViewModel(_selectedItem)
                    .ForEach(i => i.IsSelected = true);
            });
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
                var realValue = value.Date.FirstWeekDay();
                if (_From != realValue)
                {
                    _From = realValue;
                    if (_DayItems != null)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            _DayItems[i].Day = realValue.AddDays(i);
                            _DayItems[i].CalendarItems = null;
                        }
                    }
                    Refresh(); // Get new data
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

        private bool _showFullWeek = false;
        public bool ShowFullWeek
        {
            get
            {
                return _showFullWeek;
            }
            set
            {
                if (_showFullWeek != value)
                {
                    _showFullWeek = value;
                    if (_DayItems != null && _DayItems.Count == 7)
                    {
                        _DayItems[5].IsVisible = value;
                        _DayItems[6].IsVisible = value;
                    }
                    OnPropertyChanged("ShowFullWeek");
                }
            }
        }

        public string ShowFullWeekLabel
        {
            get
            {
                return "Volle Woche anzeigen";
            }
        }

        private NullableDateTimePropertyViewModel _jumpToDate;
        private IDateTimeValueModel _jumpToDateMdl;
        public ViewModel JumpToDateValue
        {
            get
            {
                if (_jumpToDate == null)
                {
                    _jumpToDateMdl = new DateTimeValueModel("Gehe zu", "", true, false, App.Base.DateTimeStyles.Date);
                    _jumpToDate = ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(DataContext, this, _jumpToDateMdl);
                }
                return _jumpToDate;
            }
        }

        private ICommandViewModel _JumpToDateCommand = null;
        public ICommandViewModel JumpToDateCommand
        {
            get
            {
                if (_JumpToDateCommand == null)
                {
                    _JumpToDateCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Anzeigen", "Zeigt das Datum im Kalender an",
                        JumpToDate,
                        () => _jumpToDateMdl != null && _jumpToDateMdl.Value.HasValue,
                        null);
                }
                return _JumpToDateCommand;
            }
        }

        public void JumpToDate()
        {
            if (_jumpToDateMdl != null && _jumpToDateMdl.Value.HasValue)
            {
                From = _jumpToDateMdl.Value.Value.FirstWeekDay();
            }
        }

        private List<DayCalendarViewModel> _DayItems;
        public List<DayCalendarViewModel> DayItems
        {
            get
            {
                if (_DayItems == null)
                {
                    _DayItems = new List<DayCalendarViewModel>();
                    for (int i = 0; i < 7; i++)
                    {
                        var item = ViewModelFactory.CreateViewModel<DayCalendarViewModel.Factory>().Invoke(DataContext, this, From.AddDays(i));
                        if (ShowFullWeek == false && (i == 5 || i == 6))
                        {
                            item.IsVisible = false;
                        }
                        _DayItems.Add(item);
                    }
                }
                return _DayItems;
            }
        }

        public override string Name
        {
            get { return "Wochenkalender"; }
        }

        private readonly Func<DateTime, DateTime, ZbTask<IEnumerable<EventViewModel>>> _Source = null;

        private EventViewModel _selectedItem;
        public EventViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    FindCalendarItemViewModel(_selectedItem)
                        .ForEach(i => i.IsSelected = false);

                    _selectedItem = value;
                    FindCalendarItemViewModel(_selectedItem)
                        .ForEach(i => i.IsSelected = true);

                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private IEnumerable<CalendarItemViewModel> FindCalendarItemViewModel(EventViewModel mdl)
        {
            if (mdl == null) Enumerable.Empty<CalendarItemViewModel>();
            return DayItems.SelectMany(i => i.CalendarItems.Where(c => c.EventViewModel == mdl));
        }

        private List<CalendarItemViewModel> CreateCalendarItemViewModels(EventViewModel evt)
        {
            if (evt.Event.StartDate <= evt.Event.EndDate)
            {
                List<CalendarItemViewModel> result = new List<CalendarItemViewModel>();
                var from = evt.Event.StartDate;
                var until = evt.Event.EndDate;
                if (from < this.From) from = this.From;
                if (until > this.To) until = this.To;
                if (evt.Event.IsAllDay)
                {
                    until = until.AddDays(1);
                }

                for (var current = from; current < until; current = current.Date.AddDays(1))
                {
                    var vmdl = ViewModelFactory.CreateViewModel<CalendarItemViewModel.Factory>()
                    .Invoke(
                        DataContext,
                        this,
                        evt);
                    vmdl.From = current == evt.Event.StartDate ? current : current.Date;
                    vmdl.Until = current.Date == evt.Event.EndDate.Date ? evt.Event.EndDate : current.Date.AddDays(1);
                    result.Add(vmdl);
                }
                return result;
            }
            else
            {
                Logging.Client.WarnFormat("Appointment item {0} has an invalid time range of {1} - {2}", evt.Event.Summary, evt.Event.StartDate, evt.Event.EndDate);
                return new List<CalendarItemViewModel>();
            }
        }

        public event EventHandler<NewEventArgs> New;
        public void NotifyNew(DateTime dt)
        {
            var temp = New;
            if (temp != null)
            {
                temp(this, new NewEventArgs(dt));
            }
        }

        public event EventHandler<OpenEventArgs> Open;
        public void NotifyOpen(EventViewModel evt)
        {
            var temp = Open;
            if (temp != null)
            {
                temp(this, new OpenEventArgs(evt));
            }
        }

        public static readonly string DefaultColor = "#F1F5E3";
    }

    public class NewEventArgs : EventArgs
    {
        public NewEventArgs(DateTime dt)
        {
            Date = dt;
        }
        public DateTime Date { get; private set; }
    }
    public class OpenEventArgs : EventArgs
    {
        public OpenEventArgs(EventViewModel evt)
        {
            Event = evt;
        }
        public EventViewModel Event { get; private set; }
    }
}
