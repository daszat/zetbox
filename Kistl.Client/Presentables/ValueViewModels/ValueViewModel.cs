namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Models;
    using System.ComponentModel;
    using System.Collections.ObjectModel;
    using Kistl.Client.Presentables.GUI;
    using Kistl.App.Base;

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

        protected IValueModel ValueModel { get; private set; }

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

        private ICommand _ClearValueCommand = null;
        public virtual ICommand ClearValueCommand
        {
            get
            {
                if (_ClearValueCommand == null)
                {
                    _ClearValueCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
                        .Invoke(DataContext, "Clear value", "Sets the value to nothing", () => ClearValue(), () => AllowNullInput);
                }
                return _ClearValueCommand;
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

        public bool Requiered
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

        public abstract TValue Value {get; set;}

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

        private ICommand _EditCommand = null;
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(DataContext, "Edit", "Opens a Editor Dialog", () => Edit(), null);
                }
                return _EditCommand;
            }
        }

        public void Edit()
        {
            ModelFactory.ShowModel(
                    ModelFactory.CreateViewModel<MultiLineEditorDialogModel.Factory>().Invoke(
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
    }
}
