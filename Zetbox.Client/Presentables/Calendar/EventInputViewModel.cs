namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using System.ComponentModel;
    using cal = Zetbox.App.Calendar;
    using Zetbox.App.Base;

    public interface IEventInputViewModel : IDataErrorInfo
    {
        EventViewModel CreateNew();
    }

    [ViewModelDescriptor]
    public class EventInputViewModel : ViewModel, IEventInputViewModel
    {
        public new delegate EventInputViewModel Factory(IZetboxContext dataCtx, ViewModel parent, cal.Calendar targetCalendar, DateTime selectedStartDate, bool isAllDay);

        public EventInputViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, cal.Calendar targetCalendar, DateTime selectedStartDate, bool isAllDay)
            : base(appCtx, dataCtx, parent)
        {
            if (targetCalendar == null) throw new ArgumentNullException("targetCalendar");
            SelectedStartDate = selectedStartDate;
            InitialIsAllDay = isAllDay;
            TargetCalendar = targetCalendar;
        }

        public DateTime SelectedStartDate { get; private set; }
        public bool InitialIsAllDay { get; private set; }
        public cal.Calendar TargetCalendar { get; private set; }

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

        private ClassValueViewModel<string> _summary = null;
        public ClassValueViewModel<string> Summary
        {
            get
            {
                if (_summary == null)
                {
                    _summary = ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(CalendarResources.SummaryLabel, "", false, false));
                }
                return _summary;
            }
        }
        private ClassValueViewModel<string> _location = null;
        public ClassValueViewModel<string> Location
        {
            get
            {
                if (_location == null)
                {
                    _location = ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(DataContext, this, new ClassValueModel<string>(CalendarResources.LocationLabel, "", true, false));
                }
                return _location;
            }
        }
        private ClassValueViewModel<string> _body = null;
        public ClassValueViewModel<string> Body
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

        public virtual string Error
        {
            get
            {
                var sb = new StringBuilder();

                if (string.IsNullOrEmpty(Summary.Value)) sb.AppendLine("Summary is empty");
                if (!string.IsNullOrEmpty(StartDate.Error)) sb.AppendLine(StartDate.Error);
                if (!string.IsNullOrEmpty(EndDate.Error)) sb.AppendLine(EndDate.Error);

                return sb.ToString();
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get { return string.Empty; }
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
