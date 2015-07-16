namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using cal = Zetbox.App.Calendar;

    public interface IEventInputViewModel
    {
        EventViewModel CreateNew();
        bool IsValid { get; }
        ValidationError ValidationError { get; }
    }

    [ViewModelDescriptor]
    public class EventInputViewModel : ViewModel, IEventInputViewModel
    {
        public new delegate EventInputViewModel Factory(IZetboxContext dataCtx, ViewModel parent, cal.CalendarBook targetCalendar, DateTime selectedStartDate, bool isAllDay);

        public EventInputViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, cal.CalendarBook targetCalendar, DateTime selectedStartDate, bool isAllDay)
            : base(appCtx, dataCtx, parent)
        {
            if (targetCalendar == null) throw new ArgumentNullException("targetCalendar");
            SelectedStartDate = selectedStartDate;
            InitialIsAllDay = isAllDay;
            TargetCalendar = targetCalendar;
        }

        public DateTime SelectedStartDate { get; private set; }
        public bool InitialIsAllDay { get; private set; }
        public cal.CalendarBook TargetCalendar { get; private set; }

        public string RecurrenceLabel
        {
            get
            {
                return CalendarResources.RecurrenceLabel;
            }
        }

        public override string Name
        {
            get { return "Event"; }
        }

        #region Properties
        private NullableDateTimePropertyViewModel _startDate = null;
        public NullableDateTimePropertyViewModel StartDate
        {
            get
            {
                if (_startDate == null)
                {
                    _startDate = ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(DataContext, this,
                        new DateTimeValueModel(CalendarResources.FromLabel, "", false, false) { Value = SelectedStartDate });
                    _startDate.DateTimeStyle = InitialIsAllDay == true ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
                    _startDate.InputAccepted += (s, e) =>
                    {
                        if (e.NewValue.HasValue && e.OldValue.HasValue)
                        {
                            EndDate.Value = EndDate.Value.Value + (e.NewValue.Value - e.OldValue.Value);
                        }
                    };
                }
                return _startDate;
            }
        }

        private NullableDateTimePropertyViewModel _endDate = null;
        public NullableDateTimePropertyViewModel EndDate
        {
            get
            {
                if (_endDate == null)
                {
                    _endDate = ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(DataContext, this,
                        new DateTimeValueModel(CalendarResources.UntilLabel, "", false, false) { Value = SelectedStartDate.AddHours(1) });
                    _endDate.DateTimeStyle = InitialIsAllDay == true ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
                }
                return _endDate;
            }
        }

        private NullableBoolPropertyViewModel _isAllDay = null;
        public NullableBoolPropertyViewModel IsAllDay
        {
            get
            {
                if (_isAllDay == null)
                {
                    _isAllDay = ViewModelFactory.CreateViewModel<NullableBoolPropertyViewModel.Factory>().Invoke(DataContext, this,
                        new BoolValueModel(CalendarResources.AllDayLabel, "", false, false) { Value = InitialIsAllDay });
                    _isAllDay.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_isAllDay_PropertyChanged);
                }
                return _isAllDay;
            }
        }

        void _isAllDay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                StartDate.DateTimeStyle = IsAllDay.Value == true ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
                EndDate.DateTimeStyle = IsAllDay.Value == true ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
            }
        }

        private StringValueViewModel _summary = null;
        public StringValueViewModel Summary
        {
            get
            {
                if (_summary == null)
                {
                    _summary = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(CalendarResources.SummaryLabel, "", false, false));
                }
                return _summary;
            }
        }
        private StringValueViewModel _location = null;
        public StringValueViewModel Location
        {
            get
            {
                if (_location == null)
                {
                    _location = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(CalendarResources.LocationLabel, "", true, false));
                }
                return _location;
            }
        }
        private StringValueViewModel _body = null;
        public StringValueViewModel Body
        {
            get
            {
                if (_body == null)
                {
                    _body = ViewModelFactory.CreateViewModel<MultiLineStringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(CalendarResources.BodyLabel, "", true, false));
                }
                return _body;
            }
        }

        private CompoundObjectPropertyViewModel _recurrence = null;
        public CompoundObjectPropertyViewModel Recurrence
        {
            get
            {
                if (_recurrence == null)
                {
                    _recurrence = ViewModelFactory.CreateViewModel<CompoundObjectPropertyViewModel.Factory>()
                        .Invoke(DataContext, this,
                            new CompoundObjectValueModel(
                                DataContext,
                                CalendarResources.RecurrenceLabel,
                                "",
                                true,
                                false,
                                FrozenContext.FindPersistenceObject<CompoundObject>(new Guid("3d4ec88b-fe8e-452e-a71d-03143a75aeb0"))));
                }
                return _recurrence;
            }
        }
        #endregion

        public override ValidationError Validate()
        {
            var result = base.Validate();

            Summary.Validate();
            if (!Summary.IsValid)
            {
                result = EnsureError(result);
                result.Children.Add(Summary.ValidationError);
            }

            StartDate.Validate();
            if (!StartDate.IsValid)
            {
                result = EnsureError(result);
                result.Children.Add(StartDate.ValidationError);
            }

            EndDate.Validate();
            if (!EndDate.IsValid)
            {
                result = EnsureError(result);
                result.Children.Add(EndDate.ValidationError);
            }

            if(result != null)
            {
                result.AddError(DataObjectViewModelResources.ErrorInvalidProperties);
            }

            return result;
        }

        public virtual EventViewModel CreateNew()
        {
            var obj = DataContext.Create<cal.Event>();
            obj.Calendar = TargetCalendar;
            obj.Summary = Summary.Value;
            obj.StartDate = StartDate.Value.Value;
            obj.EndDate = EndDate.Value.Value;
            obj.IsAllDay = IsAllDay.Value.Value;
            obj.Location = Location.Value;
            obj.Body = Body.Value;
            obj.Recurrence = (RecurrenceRule)Recurrence.Value.Object;

            return (EventViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, obj);
        }
    }
}
