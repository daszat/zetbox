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
    using Zetbox.API.Utils;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class WeekCalendarViewModel : Zetbox.Client.Presentables.ViewModel, IRefreshCommandListener
    {
        public new delegate WeekCalendarViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> source);

        public WeekCalendarViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> source)
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

        private RefreshCommand _RefreshCommand = null;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(
                        DataContext,
                        this);
                }
                return _RefreshCommand;
            }
        }

        public void Refresh()
        {
            if (_allAppointments != null)
            {
                foreach (var a in _allAppointments)
                {
                    a.PropertyChanged -= AppointmentViewModelChanged;
                }
                _allAppointments = null;
            }
            EnsureAppointments();
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

        public string DetailsLabel
        {
            get { return "Details"; }
        }

        private Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> _Source = null;
        public Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> Source
        {
            get
            {
                if (_Source == null)
                {
                    _Source = GetSource();
                }
                return _Source;
            }
            set
            {
                _Source = value;
                Refresh();
            }
        }

        protected virtual Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> GetSource()
        {
            return (f, u) => new IAppointmentViewModel[] { };
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


        private List<IAppointmentViewModel> _allAppointments;

        private void EnsureAppointments()
        {
            _allAppointments = Source(From, To).ToList();
            foreach (var a in _allAppointments)
            {
                a.PropertyChanged += AppointmentViewModelChanged;
            }

            RecreateItems();
        }

        private List<CalendarItemViewModel> _allItems;
        private void RecreateItems()
        {
            _allItems = new List<CalendarItemViewModel>();
            foreach (var a in _allAppointments)
            {
                var items = CreateCalendarItemViewModel(a);
                if (items != null && items.Count > 0) _allItems.AddRange(items);
            }

            foreach (var day in DayItems)
            {
                day.CalendarItems = _allItems
                    .Where(i => i.From.Date == day.Day);
            }
        }

        private List<CalendarItemViewModel> CreateCalendarItemViewModel(IAppointmentViewModel a)
        {
            if (a.From <= a.Until)
            {
                List<CalendarItemViewModel> result = new List<CalendarItemViewModel>();
                var from = a.From;
                var until = a.Until;
                if (from < this.From) from = this.From;
                if (until > this.To) until = this.To;
                for (var current = from; current < until; current = current.Date.AddDays(1))
                {
                    var vmdl = ViewModelFactory.CreateViewModel<CalendarItemViewModel.Factory>()
                    .Invoke(
                        DataContext,
                        this,
                        a);
                    vmdl.From = current == a.From ? current : current.Date;
                    vmdl.Until = current.Date == a.Until.Date ? a.Until : current.Date.AddDays(1);

                    vmdl.IsAllDay = vmdl.From.TimeOfDay == TimeSpan.Zero && vmdl.Until.TimeOfDay == TimeSpan.Zero;
                    result.Add(vmdl);
                }
                return result;
            }
            else
            {
                Logging.Client.WarnFormat("Appointment item {0} has an invalid time range of {1} - {2}", a.Subject, a.From, a.Until);
                return null;
            }
        }

        private void AppointmentViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "From":
                case "Until":
                    RecreateItems();
                    break;
            }
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

            EnsureAppointments();

            var items = CreateCalendarItemViewModel(result.AppointmentViewModel);
            if (items != null && items.Count > 0)
            {
                _allItems.AddRange(items);
                RecreateItems();
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
