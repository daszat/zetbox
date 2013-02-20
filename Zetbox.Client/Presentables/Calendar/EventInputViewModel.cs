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

    public interface IEventInputViewModel : IDataErrorInfo
    {
        EventViewModel CreateNew();
    }

    [ViewModelDescriptor]
    public class EventInputViewModel : ViewModel, IEventInputViewModel
    {
        public new delegate EventInputViewModel Factory(IZetboxContext dataCtx, ViewModel parent, DateTime selectedStartDate);
        private DateTime _selectedStartDate;

        public EventInputViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, DateTime selectedStartDate)
            : base(appCtx, dataCtx, parent)
        {
            _selectedStartDate = selectedStartDate;
        }

        public override string Name
        {
            get { return "Event"; }
        }

        #region Properties
        private DateTimeValueModel _startDateModel;
        private DateTimeValueModel _endDateModel;
        private void EnsureModels()
        {
            if (_startDateModel == null)
            {
                _startDateModel = new DateTimeValueModel("Von", "", false, false) { Value = _selectedStartDate };
            }
            if (_endDateModel == null)
            {
                _endDateModel = new DateTimeValueModel("Bis", "", false, false) { Value = _selectedStartDate.AddHours(1) };
            }
        }

        private NullableDateTimePropertyViewModel _startDate = null;
        public NullableDateTimePropertyViewModel StartDate
        {
            get
            {
                if (_startDate == null)
                {
                    EnsureModels();
                    _startDate = ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(DataContext, this, _startDateModel);
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
                    EnsureModels();
                    _endDate = ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(DataContext, this, _endDateModel);
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
                    _isAllDay = ViewModelFactory.CreateViewModel<NullableBoolPropertyViewModel.Factory>().Invoke(DataContext, this, new BoolValueModel("Ganztägig", "", false, false));
                    _isAllDay.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_isAllDay_PropertyChanged);
                }
                return _isAllDay;
            }
        }

        void _isAllDay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                EnsureModels();
                _startDateModel.DateTimeStyle = IsAllDay.Value == true ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
                _endDateModel.DateTimeStyle = IsAllDay.Value == true ? Zetbox.App.Base.DateTimeStyles.Date : App.Base.DateTimeStyles.DateTime;
            }
        }

        private ClassValueViewModel<string> _summary = null;
        public ClassValueViewModel<string> Summary
        {
            get
            {
                if (_summary == null)
                {
                    _summary = ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(DataContext, this, new ClassValueModel<string>("Bemerkung", "", false, false));
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
                    _location = ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(DataContext, this, new ClassValueModel<string>("Ort", "", true, false));
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
                    _body = ViewModelFactory.CreateViewModel<MultiLineStringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>("Text", "", true, false));
                }
                return _body;
            }
        }
        #endregion


        public string Error
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

        public EventViewModel CreateNew()
        {
            var obj = DataContext.Create<cal.Event>();
            obj.Summary = Summary.Value;
            obj.StartDate = StartDate.Value.Value;
            obj.EndDate = EndDate.Value.Value;
            obj.IsAllDay = IsAllDay.Value.Value;
            obj.Location = Location.Value;
            obj.Body = Body.Value;

            return (EventViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, obj);
        }
    }
}
