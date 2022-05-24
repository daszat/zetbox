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
    using System.Threading.Tasks;
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
        public new delegate WeekCalendarViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Func<DateTime, DateTime, System.Threading.Tasks.Task<IEnumerable<EventViewModel>>> source);

        public WeekCalendarViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, Func<DateTime, DateTime, System.Threading.Tasks.Task<IEnumerable<EventViewModel>>> source)
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
                    _NextWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        WeekCalendar.WeekCalendarViewModelResources.NextWeekCommand_Label,
                        WeekCalendar.WeekCalendarViewModelResources.NextWeekCommand_Tooltip,
                        NextWeek, null, null);
                    Task.Run(async () => _NextWeekCommand.Icon = await IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.forward_png.Find(FrozenContext)));
                }
                return _NextWeekCommand;
            }
        }

        public Task NextWeek()
        {
            From = From.AddDays(7);

            return Task.CompletedTask;
        }

        private ICommandViewModel _PrevWeekCommand = null;
        public ICommandViewModel PrevWeekCommand
        {
            get
            {
                if (_PrevWeekCommand == null)
                {
                    _PrevWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        WeekCalendar.WeekCalendarViewModelResources.PrevWeekCommand_Label,
                        WeekCalendar.WeekCalendarViewModelResources.PrevWeekCommand_Tooltip,
                        PrevWeek, null, null);
                    Task.Run(async () => _PrevWeekCommand.Icon = await IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.back_png.Find(FrozenContext)));
                }
                return _PrevWeekCommand;
            }
        }

        public Task PrevWeek()
        {
            From = From.AddDays(-7);

            return Task.CompletedTask;
        }

        private ICommandViewModel _ThisWeekCommand = null;
        public ICommandViewModel ThisWeekCommand
        {
            get
            {
                if (_ThisWeekCommand == null)
                {
                    _ThisWeekCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        WeekCalendar.WeekCalendarViewModelResources.ThisWeekCommand_Label,
                        WeekCalendar.WeekCalendarViewModelResources.ThisWeekCommand_Tooltip,
                        ThisWeek, null, null);
                    Task.Run(async () => _ThisWeekCommand.Icon = await IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.date_png.Find(FrozenContext)));
                }
                return _ThisWeekCommand;
            }
        }

        public Task ThisWeek()
        {
            From = DateTime.Today.FirstWeekDay();

            return Task.CompletedTask;
        }

        public async Task Refresh()
        {
            if (DataContext.IsDisposed) return;

            var taskFrom = From;
            var taskTo = To;
            var t = await _Source(From, To);

            if (From == taskFrom && To == taskTo)
            {
                var tmp  = new List<CalendarItemViewModel>();
                foreach(var e in t)
                {
                    tmp.AddRange(await e.CreateCalendarItemViewModels(From, To));
                }

                var allItems = tmp.ToLookup(c => c.From.Date);

                foreach (var day in DayItems)
                {
                    day.CalendarItems = allItems[day.Day];
                }
            }
            FindCalendarItemViewModel(_selectedItem)
                .ForEach(i => i.IsSelected = true);
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
                    _ = Refresh(); // Get new data
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
                return WeekCalendar.WeekCalendarViewModelResources.ShowFullWeekLabel;
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
                    _JumpToDateCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        WeekCalendar.WeekCalendarViewModelResources.JumpToDateCommand_Label,
                        WeekCalendar.WeekCalendarViewModelResources.JumpToDateCommand_Tooltip,
                        JumpToDate,
                        () => Task.FromResult(_jumpToDateMdl != null && _jumpToDateMdl.Value.HasValue),
                        null);
                }
                return _JumpToDateCommand;
            }
        }

        public Task JumpToDate()
        {
            if (_jumpToDateMdl != null && _jumpToDateMdl.Value.HasValue)
            {
                From = _jumpToDateMdl.Value.Value.FirstWeekDay();
            }

            return Task.CompletedTask;
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
            get { return WeekCalendar.WeekCalendarViewModelResources.Name; }
        }

        private readonly Func<DateTime, DateTime, System.Threading.Tasks.Task<IEnumerable<EventViewModel>>> _Source = null;

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

        private CalendarViewModel _selectedCalendar;
        public CalendarViewModel SelectedCalendar
        {
            get
            {
                return _selectedCalendar;
            }
            set
            {
                if (_selectedCalendar != value)
                {
                    _selectedCalendar = value;
                    OnPropertyChanged("SelectedCalendar");
                }
            }
        }

        private IEnumerable<CalendarItemViewModel> FindCalendarItemViewModel(EventViewModel mdl)
        {
            if (mdl == null) Enumerable.Empty<CalendarItemViewModel>();
            return DayItems.SelectMany(i => i.CalendarItems.Where(c => c.EventViewModel == mdl));
        }

        public event EventHandler<NewEventArgs> New;
        public void NotifyNew(DateTime dt, bool isAllDay = false)
        {
            var temp = New;
            if (temp != null)
            {
                temp(this, new NewEventArgs(dt, isAllDay));
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
}
