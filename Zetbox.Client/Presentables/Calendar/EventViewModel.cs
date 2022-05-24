namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Calendar;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using Zetbox.API.Utils;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class EventViewModel : DataObjectViewModel
    {
        public new delegate EventViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Event evt);

        public EventViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Event evt)
            : base(appCtx, dataCtx, parent, evt)
        {
            this.Event = evt;
            this.isReadOnlyStore = Event.IsViewReadOnly;
        }

        public Event Event { get; private set; }

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);
            switch (propName)
            {
                case "Calendar":
                    if (_calendarViewModel != null)
                    {
                        _calendarViewModel.PropertyChanged -= _calendarViewModel_PropertyChanged;
                        _calendarViewModel = null;
                        OnPropertyChanged("CalendarViewModel");
                    }
                    break;
                case "IsViewReadOnly":
                    this.IsReadOnly = Event.IsViewReadOnly;
                    break;
            }
        }

        public string RecurrenceLabel
        {
            get
            {
                return CalendarResources.RecurrenceLabel;
            }
        }

        private NullableDateTimePropertyViewModel _startDateVmdl;
        private NullableDateTimePropertyViewModel _endDateVmdl;

        protected override void OnPropertyModelsByNameCreated()
        {
            base.OnPropertyModelsByNameCreated();

            _startDateVmdl = (NullableDateTimePropertyViewModel)PropertyModelsByName["StartDate"];
            _endDateVmdl = (NullableDateTimePropertyViewModel)PropertyModelsByName["EndDate"];

            SetDateTimeStyle(Event.IsAllDay);

            _startDateVmdl.InputAccepted += (s, e) =>
            {
                if (e.NewValue.HasValue && e.OldValue.HasValue)
                {
                    Event.EndDate = Event.EndDate + (e.NewValue.Value - e.OldValue.Value);
                }
            };

            var allDayVmdl = (NullableBoolPropertyViewModel)PropertyModelsByName["IsAllDay"];
            allDayVmdl.InputAccepted += (s, e) =>
            {
                SetDateTimeStyle(e.NewValue == true);
            };
        }

        private void SetDateTimeStyle(bool isAllDay)
        {
            _startDateVmdl.DateTimeStyle = isAllDay ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
            _endDateVmdl.DateTimeStyle = isAllDay ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
        }

        public override string Name
        {
            get { return Event.Summary; }
        }

        private CalendarViewModel _calendarViewModel;
        public CalendarViewModel CalendarViewModel
        {
            get
            {
                if (_calendarViewModel == null)
                {
                    _calendarViewModel = (CalendarViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, Event.Calendar);
                    _calendarViewModel.PropertyChanged += _calendarViewModel_PropertyChanged;
                }
                return _calendarViewModel;
            }
        }

        void _calendarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Color":
                    OnPropertyChanged("Color");
                    break;
            }
        }

        public string Color
        {
            get
            {
                return CalendarViewModel.Color;
            }
        }

        public async Task<IList<CalendarItemViewModel>> CreateCalendarItemViewModels(DateTime displayFrom, DateTime displayTo)
        {
            var startDate = Event.StartDate;
            var endDate = Event.EndDate;
            if (endDate < startDate)
            {
                Logging.Client.WarnFormat("Appointment item {0} has an invalid time range of {1} > {2}", Event.Summary, startDate, endDate);
                endDate = startDate.AddMinutes(30);
            }

            List<CalendarItemViewModel> result = new List<CalendarItemViewModel>();
            var duration = endDate - startDate;

            List<DateTime> occurences;
            if (Event.Recurrence.Frequency == null)
            {
                occurences = new List<DateTime>() { startDate };
            }
            else
            {
                if (displayFrom < startDate)
                    displayFrom = startDate;

                if (Event.Recurrence.Until.HasValue && Event.Recurrence.Until.Value < displayTo)
                    displayTo = Event.Recurrence.Until.Value;

                occurences = (await Event.Recurrence.GetWithinInterval(Event.StartDate, displayFrom, displayTo)).ToList();
            }

            foreach (var o in occurences)
            {
                var event_StartDate = o;
                var event_EndDate = o + duration;

                var from = event_StartDate;
                var until = event_EndDate;

                if (from < displayFrom) from = displayFrom;
                if (until > displayTo) until = displayTo;
                if (Event.IsAllDay)
                {
                    until = until.AddDays(1);
                }

                for (var current = from; current < until; current = current.Date.AddDays(1))
                {
                    var vmdl = ViewModelFactory.CreateViewModel<CalendarItemViewModel.Factory>()
                    .Invoke(
                        DataContext,
                        this,
                        this);
                    vmdl.From = current.Date == event_StartDate.Date ? current : current.Date;
                    vmdl.Until = current.Date == event_EndDate.Date ? event_EndDate : current.Date.AddDays(1);
                    result.Add(vmdl);
                }
            }
            return result;

        }
    }
}
