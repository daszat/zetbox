
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.GUI;

    public abstract class BaseValueViewModel : ViewModel, IValueViewModel, IFormattedValueViewModel, IDataErrorInfo, ILabeledViewModel
    {
        public new delegate BaseValueViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public BaseValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx)
        {
            this.ValueModel = mdl;
            this.ValueModel.PropertyChanged += new PropertyChangedEventHandler(Model_PropertyChanged);
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Value":
                    NotifyValueChanged();
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
                    _ClearValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>()
                        .Invoke(DataContext, "Clear value", "Sets the value to nothing", () => ClearValue(), () => AllowNullInput && !DataContext.IsReadonly);
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
        protected abstract void ParseValue(string str);

        public string FormattedValue
        {
            get
            {
                return FormatValue();
            }
            set
            {
                ParseValue(value);
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { return ValueModel.Error; }
        }

        public string this[string columnName]
        {
            get { return ValueModel[columnName]; }
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

        public abstract TValue Value { get; set; }

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

        protected override void ParseValue(string str)
        {
            this.Value = String.IsNullOrEmpty(str) ? null : (Nullable<TValue>)System.Convert.ChangeType(str, typeof(TValue));
        }

        public override TValue? Value
        {
            get
            {
                return ValueModel.Value;
            }
            set
            {
                ValueModel.Value = value;
            }
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

        protected override void ParseValue(string str)
        {
            this.Value = String.IsNullOrEmpty(str) ? null : (TValue)System.Convert.ChangeType(str, typeof(TValue));
        }

        public override TValue Value
        {
            get
            {
                return (TValue)ValueModel.Value;
            }
            set
            {
                ValueModel.Value = value;
            }
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
                    var enumValues = EnumModel.Enumeration.EnumerationEntries.Select(e => new KeyValuePair<int?, string>(e.Value, e.Name));
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
            return PossibleValues.Single(key => key.Key == Value.Value).Value;
        }

        protected override void ParseValue(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Value = null;
            }
            else
            {
                var item = PossibleValues.SingleOrDefault(value => string.Compare(value.Value, str, true) == 0);
                if (item.Key != null)
                {
                    Value = item.Key.Value;
                }
                else
                {
                    // TODO: Set Error
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
                    _EditCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Edit", "Opens a Editor Dialog", () => Edit(), null);
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
                else if (value == null && Value != null)
                {
                    // Preserve time
                    Value = DateTime.MinValue.Add(Value.Value.TimeOfDay);
                }
                else if (value != null && Value != null)
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
                else if (value == null && Value != null)
                {
                    // Preserve date
                    Value = Value.Value.Date;
                }
                else if (value != null && Value != null)
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

        public override DateTime? Value
        {
            get
            {
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
                    base.Value = GetValue();
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
                    base.Value = GetValue();
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

        private DateTime? GetValue()
        {
            return Year != null && Month != null ? new DateTime(Year.Value, Month.Value, 1) : (DateTime?)null;
        }

        public override DateTime? Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                base.Value = value;
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
