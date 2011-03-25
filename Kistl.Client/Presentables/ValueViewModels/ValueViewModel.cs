
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.GUI;

    public enum ValueViewModelState
    {
        Blurred_UnmodifiedValue = 1,
        ImplicitFocus_WritingModel,
        ImplicitFocus_PartialUserInput,
    }

    public abstract class BaseValueViewModel : ViewModel, IValueViewModel, IFormattedValueViewModel, IDataErrorInfo, ILabeledViewModel
    {
        public new delegate BaseValueViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, Property prop, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache().LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(prop).Invoke(dataCtx, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, Method m, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache().LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(m).Invoke(dataCtx, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, ViewModelDescriptor desc, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache().LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(desc).Invoke(dataCtx, mdl));
        }

        public static BaseValueViewModel Fetch(IViewModelFactory f, IKistlContext dataCtx, BaseParameter param, IValueModel mdl)
        {
            return (BaseValueViewModel)dataCtx.GetViewModelCache().LookupOrCreate(mdl, () => f.CreateViewModel<BaseValueViewModel.Factory>(param).Invoke(dataCtx, mdl));
        }

        public BaseValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx)
        {
            this.ValueModel = mdl;
            this.ValueModel.PropertyChanged += Model_PropertyChanged;

            this.State = ValueViewModelState.Blurred_UnmodifiedValue;
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnValueModelPropertyChanged(e);
        }

        protected virtual void OnValueModelPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Value":
                    // MC1 && MC2
                    if (State == ValueViewModelState.Blurred_UnmodifiedValue)
                    {
                        OnModelChanged();
                    }
                    break;
                case "Error":
                    OnPropertyChanged("Error");
                    break;
            }
        }

        public IValueModel ValueModel { get; private set; }

        public override string Name
        {
            get { return ValueModel.Label; }
        }

        public override App.GUI.ControlKind RequestedKind
        {
            get
            {
                return base.RequestedKind ?? ValueModel.RequestedKind;
            }
            set
            {
                base.RequestedKind = value;
            }
        }

        #region Utilities and UI callbacks
        protected virtual void NotifyValueChanged()
        {
            OnPropertyChanged("Value");
            OnPropertyChanged("FormattedValue");
            OnPropertyChanged("IsNull");
            OnPropertyChanged("HasValue");
            OnPropertyChanged("Name");
        }
        #endregion

        #region IValueViewModel Members

        public abstract bool HasValue { get; }

        public virtual bool IsNull
        {
            get { return !HasValue; }
        }

        public virtual bool AllowNullInput
        {
            get { return ValueModel.AllowNullInput; }
        }

        public virtual string Label
        {
            get { return ValueModel.Label; }
        }

        public virtual string ToolTip
        {
            get { return ValueModel.Description; }
        }

        private bool _IsReadOnly;
        public virtual bool IsReadOnly
        {
            get
            {
                return ValueModel.IsReadOnly || _IsReadOnly;
            }
            set
            {
                if (_IsReadOnly != value)
                {
                    _IsReadOnly = value;
                    OnPropertyChanged("IsReadOnly");
                }
            }
        }

        public virtual void ClearValue()
        {
            ValueModel.ClearValue();
        }

        private ICommandViewModel _ClearValueCommand = null;
        public virtual ICommandViewModel ClearValueCommand
        {
            get
            {
                if (_ClearValueCommand == null)
                {
                    _ClearValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        ValueViewModelResources.ClearValueCommand_Name,
                        ValueViewModelResources.ClearValueCommand_Tooltip,
                        () => ClearValue(),
                        () => AllowNullInput && !DataContext.IsReadonly && !IsReadOnly);
                    //_ClearValueCommand.Icon = FrozenContext.FindPersistenceObject<Icon>(NamedObjects.
                }
                return _ClearValueCommand;
            }
        }

        public object UntypedValue
        {
            get
            {
                return ValueModel.GetUntypedValue();
            }
        }

        #endregion

        #region IFormattedValueViewModel Members

        protected abstract string FormatValue();

        /// <summary>
        /// Parse the given string and set the underlying Value. In case of an parse error, do not touch the value but set the error string
        /// </summary>
        /// <param name="str">string to parse</param>
        /// <param name="error">parse error to display</param>
        protected abstract void ParseValue(string str, out string error);

        private string _partialUserInputError;
        private string _partialUserInput;

        public virtual void CanocalizeInput()
        {
            if (string.IsNullOrEmpty(_partialUserInputError))
            {
                _partialUserInput = FormatValue();
                OnPropertyChanged("FormattedValue");
            }
        }

        public string FormattedValue
        {
            get
            {
                switch(State)  {
                    case ValueViewModelState.Blurred_UnmodifiedValue:
                    case ValueViewModelState.ImplicitFocus_WritingModel:
                        return FormatValue();
                    case ValueViewModelState.ImplicitFocus_PartialUserInput:
                        return _partialUserInput;
                    default:
                        throw new InvalidOperationException();
                }
            }
            set
            {
                if (_partialUserInput != value)
                {
                    _partialUserInput = value;
                    var oldPartialUserInputError = _partialUserInputError;
                    ParseValue(_partialUserInput, out _partialUserInputError);
                    if (String.IsNullOrEmpty(_partialUserInputError))
                    {
                        OnValidInput();
                    }
                    else
                    {
                        OnPartialInput();
                    }

                    if (_partialUserInputError != oldPartialUserInputError)
                    {
                        OnPropertyChanged("Error");
                    }
                    OnPropertyChanged("FormattedValue");
                }
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public virtual string Error
        {
            get
            {
                var baseError = ValueModel.Error;
                if (string.IsNullOrEmpty(baseError))
                {
                    return _partialUserInputError;
                }
                else
                {
                    return baseError + "\n" + _partialUserInputError;
                }
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "FormattedValue":
                        return _partialUserInputError;
                    default:
                        return ValueModel[columnName];
                }
            }
        }

        #endregion

        #region ILabeledViewModel Members
        public ViewModel Model
        {
            get
            {
                return this;
            }
        }

        public bool Required
        {
            get { return !this.AllowNullInput; }
        }

        #endregion

        #region State Machine

        protected ValueViewModelState State
        {
            get;
            private set;
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime the model changed and handles the MC1, MC2,
        /// and MC3 events.
        /// </summary>
        protected virtual void OnModelChanged()
        {
            switch (State)
            {
                case ValueViewModelState.Blurred_UnmodifiedValue:
                    // MC1
                    NotifyValueChanged();
                    break;
                // case F/UV
                case ValueViewModelState.ImplicitFocus_WritingModel:
                    // MC3, B1
                    _partialUserInput = null;
                    _partialUserInputError = null;
                    NotifyValueChanged();
                    State = ValueViewModelState.Blurred_UnmodifiedValue;
                    break;
                // case F/WM
                    // MC3
                    //NotifyValueChanged();
                    //State = F/UV;
                    //break;
                default:
                    // MC2 
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime valid input is received and handles 
        /// the VI event.
        /// </summary>
        protected virtual void OnValidInput()
        {
            switch (State)
            {
                case ValueViewModelState.Blurred_UnmodifiedValue:
                    State = ValueViewModelState.ImplicitFocus_WritingModel;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Part of the ValueViewModel state machine as described in the KistlGuide. 
        /// This method is called everytime partial input is received and handles 
        /// the PI event.
        /// </summary>
        protected virtual void OnPartialInput()
        {
            switch (State)
            {
                case ValueViewModelState.Blurred_UnmodifiedValue:
                    State = ValueViewModelState.ImplicitFocus_PartialUserInput;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Blur()
        {
            throw new InvalidOperationException();
        }

        public void Focus()
        {
            throw new InvalidOperationException();
        }

        #endregion
    }

    public abstract class ValueViewModel<TValue, TModel> : BaseValueViewModel, IValueViewModel<TValue>
    {
        public new delegate ValueViewModel<TValue, TModel> Factory(IKistlContext dataCtx, IValueModel mdl);

        public ValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
            this.ValueModel = (IValueModel<TModel>)mdl;
        }

        public new IValueModel<TModel> ValueModel { get; private set; }

        #region IValueViewModel<TValue> Members

        protected abstract TValue GetValue();
        protected abstract void SetValue(TValue value);

        public virtual TValue Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                OnValidInput();
                SetValue(value);
                OnModelChanged();
            }
        }

        #endregion

        public override bool HasValue
        {
            get { return ValueModel.Value != null; }
        }

        protected override string FormatValue()
        {
            return Value != null ? Value.ToString() : String.Empty;
        }
    }

    public class NullableStructValueViewModel<TValue> : ValueViewModel<Nullable<TValue>, Nullable<TValue>>
        where TValue : struct
    {
        public new delegate NullableStructValueViewModel<TValue> Factory(IKistlContext dataCtx, IValueModel mdl);

        public NullableStructValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }

        protected override void ParseValue(string str, out string error)
        {
            error = null;
            try
            {
                this.Value = String.IsNullOrEmpty(str) ? null : (Nullable<TValue>)System.Convert.ChangeType(str, typeof(TValue));
            }
            catch
            {
                error = "Unable to convert type";
            }
        }

        protected override TValue? GetValue()
        {
            return ValueModel.Value;
        }

        protected override void SetValue(TValue? value)
        {
            ValueModel.Value = value;
        }
    }

    public class ClassValueViewModel<TValue> : ValueViewModel<TValue, TValue>
        where TValue : class
    {
        public new delegate ClassValueViewModel<TValue> Factory(IKistlContext dataCtx, IValueModel mdl);

        public ClassValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }

        protected override void ParseValue(string str, out string error)
        {
            error = null;
            try
            {
                this.Value = String.IsNullOrEmpty(str) ? null : (TValue)System.Convert.ChangeType(str, typeof(TValue));
            }
            catch
            {
                error = "Cannot change type";
            }
        }

        protected override TValue GetValue()
        {
            return ValueModel.Value;
        }

        protected override void SetValue(TValue value)
        {
            ValueModel.Value = value;
        }
    }

    public class EnumerationValueViewModel : NullableStructValueViewModel<int>
    {
        public new delegate EnumerationValueViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public EnumerationValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
            this.EnumModel = (IEnumerationValueModel)mdl;
        }

        public IEnumerationValueModel EnumModel { get; private set; }

        private ReadOnlyCollection<KeyValuePair<int?, string>> _PossibleValues = null;
        public ReadOnlyCollection<KeyValuePair<int?, string>> PossibleValues
        {
            get
            {
                if (_PossibleValues == null)
                {
                    var enumValues = EnumModel.Enumeration.EnumerationEntries.Select(e => new KeyValuePair<int?, string>(e.Value, e.GetLabel()));
                    this._PossibleValues = new ReadOnlyCollection<KeyValuePair<int?, string>>(
                        new[] { new KeyValuePair<int?, string>(null, "") }
                        .Concat(enumValues)
                        .ToList()
                    );
                }
                return _PossibleValues;
            }
        }

        protected override string FormatValue()
        {
            if (Value == null) return string.Empty;
            // This hurts, but looks funny
            // Don't die on invalid values
            return PossibleValues.FirstOrDefault(key => key.Key == Value.Value).Value;
        }

        protected override void ParseValue(string str, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(str))
            {
                Value = null;
            }
            else
            {
                // Don't die on invalid values
                var item = PossibleValues.FirstOrDefault(value => string.Compare(value.Value, str, true) == 0);
                if (item.Key != null)
                {
                    Value = item.Key.Value;
                }
                else
                {
                    error = "Error converting Enumeration";
                }
            }
        }
    }

    public class MultiLineStringValueViewModel
        : ClassValueViewModel<string>
    {
        public new delegate MultiLineStringValueViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public MultiLineStringValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Value")
            {
                base.OnPropertyChanged("ShortText");
            }
        }

        private int _ShortTextLength = 20;
        public int ShortTextLength
        {
            get
            {
                return _ShortTextLength;
            }
            set
            {
                if (_ShortTextLength != value)
                {
                    _ShortTextLength = value;
                    OnPropertyChanged("ShortTextLength");
                    OnPropertyChanged("ShortText");
                }
            }
        }

        public string ShortText
        {
            get
            {
                if (!string.IsNullOrEmpty(Value) && Value.Length > ShortTextLength)
                {
                    return Value.Replace("\r", "").Replace('\n', ' ').Substring(0, ShortTextLength) + "...";
                }
                else
                {
                    return Value;
                }
            }
        }

        private ICommandViewModel _EditCommand = null;
        public ICommandViewModel EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        ValueViewModelResources.EditCommand_Name,
                        ValueViewModelResources.EditCommand_Tooltip,
                        () => Edit(),
                        null);
                }
                return _EditCommand;
            }
        }

        public void Edit()
        {
            ViewModelFactory.ShowModel(
                    ViewModelFactory.CreateViewModel<MultiLineEditorDialogViewModel.Factory>().Invoke(
                        DataContext,
                        Value,
                        (v) => Value = v),
                    true);
        }
    }

    public class NullableGuidPropertyViewModel : NullableStructValueViewModel<Guid>
    {
        public new delegate NullableGuidPropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public NullableGuidPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
        }

        protected override void ParseValue(string str, out string error)
        {
            error = null;
            try
            {
                this.Value = new Guid(str);
            }
            catch
            {
                error = "Error parsing Guid";
            }
        }
    }

    public class NullableDateTimePropertyViewModel
        : NullableStructValueViewModel<DateTime>
    {
        public new delegate NullableDateTimePropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public NullableDateTimePropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
            DateTimeModel = (IDateTimeValueModel)mdl;
        }

        public IDateTimeValueModel DateTimeModel { get; private set; }

        protected override string FormatValue()
        {
            if (Value == null) return string.Empty;
            switch (DateTimeModel.DateTimeStyle)
            {
                case DateTimeStyles.Date:
                    return Value.Value.ToShortDateString();
                case DateTimeStyles.Time:
                    return Value.Value.ToShortTimeString();
                default:
                    return Value.Value.ToString();
            }
        }

        public bool DatePartVisible
        {
            get
            {
                return DateTimeModel.DateTimeStyle == DateTimeStyles.Date || DateTimeModel.DateTimeStyle == DateTimeStyles.DateTime;
            }
        }

        public bool TimePartVisible
        {
            get
            {
                return DateTimeModel.DateTimeStyle == DateTimeStyles.Time || DateTimeModel.DateTimeStyle == DateTimeStyles.DateTime;
            }
        }

        public DateTime? DatePart
        {
            get
            {
                return Value != null ? Value.Value.Date : (DateTime?)null;
            }
            set
            {
                if (value == null && Value == null)
                {
                    // Do nothing
                }
                else if (value == null && Value != null && TimePartVisible)
                {
                    // Preserve time
                    Value = DateTime.MinValue.Add(Value.Value.TimeOfDay);
                }
                else if (value != null && Value != null && TimePartVisible)
                {
                    // Preserve time
                    Value = value.Value.Add(Value.Value.TimeOfDay);
                }
                else //if (value != null && Value == null)
                {
                    Value = value;
                }

                OnPropertyChanged("DatePart");
            }
        }

        public TimeSpan? TimePart
        {
            get
            {
                return Value != null ? Value.Value.TimeOfDay : (TimeSpan?)null;
            }
            set
            {
                if (value == null && Value == null)
                {
                    // Do nothing
                }
                else if (value == null && Value != null && DatePartVisible)
                {
                    // Preserve date
                    Value = Value.Value.Date;
                }
                else if (value != null && Value != null && DatePartVisible)
                {
                    // Preserve date
                    Value = Value.Value.Date.Add(value.Value);
                }
                else //if (value != null && Value == null)
                {
                    Value = DateTime.MinValue.Add(value.Value);
                }
                OnPropertyChanged("TimePart");
            }
        }

        private string _timePartInput;
        private string _timePartError;
        public string TimePartString
        {
            get
            {
                if (_timePartInput == null)
                {
                    _timePartInput = TimePart == null ? String.Empty : String.Format("{0:00}:{1:00}", TimePart.Value.Hours, TimePart.Value.Minutes);
                }
                return _timePartInput;
            }
            set
            {
                if (value == _timePartInput)
                {
                    return;
                }

                if (value == null)
                {
                    _timePartInput = String.Empty;
                    OnPropertyChanged("TimePartString");

                    _timePartError = null;
                    OnPropertyChanged("Error");

                    TimePart = TimeSpan.Zero;

                    return;
                }

                _timePartInput = value;
                OnPropertyChanged("TimePartString");

                MatchCollection matches;
                if (null != (matches = Regex.Matches(value, @"^(\d?\d):?(\d\d)$")))
                {
                    int hours, minutes;
                    if (matches.Count == 1
                        && matches[0].Groups.Count == 3
                        && Int32.TryParse(matches[0].Groups[1].Captures[0].Value, out hours)
                        && Int32.TryParse(matches[0].Groups[2].Captures[0].Value, out minutes)
                        && hours >= 0 && hours < 24
                        && minutes >= 0 && minutes < 60)
                    {
                        if (_timePartError != null)
                        {
                            _timePartError = null;
                            OnPropertyChanged("Error");
                        }

                        TimePart = new TimeSpan(hours, minutes, 0);
                    }
                    else
                    {
                        _timePartError = string.Format("Fehler bei Zeiteingabe hh:mm : {0}", value);
                        OnPropertyChanged("Error");
                    }
                }
            }
        }

        public override DateTime? Value
        {
            get
            {
                // It makes no sense to display the 01.01.0001 in the View - empty is better
                if (base.Value == DateTime.MinValue) return null;
                return base.Value;
            }
            set
            {
                if (base.Value != value)
                {
                    base.Value = value;
                    OnPropertyChanged("DatePart");
                    OnPropertyChanged("TimePart");
                }
            }
        }

        public override string Error
        {
            get
            {
                var baseError = base.Error;
                if (string.IsNullOrEmpty(baseError))
                {
                    return _timePartError;
                }
                else
                {
                    return baseError + "\n" + _timePartError;
                }
            }
        }

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "TimePartString":
                        return _timePartError;
                    default:
                        return base[columnName];
                }
            }
        }

        public override void CanocalizeInput()
        {
            if (string.IsNullOrEmpty(_timePartError))
            {
                _timePartInput = TimePart == null ? String.Empty : String.Format("{0:00}:{1:00}", TimePart.Value.Hours, TimePart.Value.Minutes);
                OnPropertyChanged("TimePartString");
            }
        }
    }

    public class NullableMonthPropertyViewModel
        : NullableDateTimePropertyViewModel
    {
        public new delegate NullableMonthPropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public NullableMonthPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {

        }

        private int? _year;
        public int? Year
        {
            get
            {
                UpdateValueCache();
                return _year;
            }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    base.Value = GetValueFromComponents();
                    OnPropertyChanged("Year");
                    OnPropertyChanged("Value");
                }
            }
        }


        private int? _month;
        public int? Month
        {
            get
            {
                UpdateValueCache();
                return _month;
            }
            set
            {
                if (_month != value)
                {
                    _month = value;
                    base.Value = GetValueFromComponents();
                    OnPropertyChanged("Month");
                    OnPropertyChanged("Value");
                }
            }
        }

        private bool _cacheInititalized = false;
        private void UpdateValueCache()
        {
            if (!_cacheInititalized)
            {
                _year = base.Value != null ? base.Value.Value.Year : (int?)null;
                _month = base.Value != null ? base.Value.Value.Month : (int?)null;
                _cacheInititalized = true;
            }
        }

        private DateTime? GetValueFromComponents()
        {
            return Year != null && Month != null && Year > 0 && Month >= 1 && Month <= 12 ? new DateTime(Year.Value, Month.Value, 1) : (DateTime?)null;
        }

        protected override DateTime? GetValue()
        {
            return GetValueFromComponents();
        }

        protected override void SetValue(DateTime? value)
        {
            base.SetValue(value);
            if (value != null)
            {
                _year = value.Value.Year;
                _month = value.Value.Month;
            }
            else
            {
                _year = null;
                _month = null;
            }
            OnPropertyChanged("Year");
            OnPropertyChanged("Month");
        }

        public class MonthViewModel
        {
            public MonthViewModel()
            {
            }

            public MonthViewModel(int month, string name)
            {
                this.Month = month;
                this.Name = name;
            }

            public int Month { get; set; }
            public string Name { get; set; }
        }

        private static IList<MonthViewModel> _Months = null;
        public IEnumerable<MonthViewModel> Months
        {
            get
            {
                if (_Months == null)
                {
                    _Months = new List<MonthViewModel>();
                    _Months.Add(new MonthViewModel());
                    for (int i = 1; i <= 12; i++)
                    {
                        var dt = new DateTime(2000, i, 1);
                        _Months.Add(new MonthViewModel(i, dt.ToString("MMMM")));
                    }
                }
                return _Months;
            }
        }
    }
}
