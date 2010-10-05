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

    public abstract class BaseValueViewModel : ViewModel, IValueViewModel, IFormattedValueViewModel, IDataErrorInfo
    {
        public new delegate BaseValueViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public BaseValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx)
        {
            this.Model = mdl;
            this.Model.PropertyChanged += new PropertyChangedEventHandler(Model_PropertyChanged);
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

        public IValueModel Model { get; private set; }

        public override string Name
        {
            get { return Model.Label; }
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
            get { return Model.AllowNullInput; }
        }

        public virtual string Label
        {
            get { return Model.Label; }
        }

        public virtual string ToolTip
        {
            get { return Model.Description; }
        }

        public virtual bool IsReadOnly
        {
            get { return Model.IsReadOnly; }
        }

        public virtual void ClearValue()
        {
            Model.ClearValue();
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
            get { return Model.Error; }
        }

        public string this[string columnName]
        {
            get { return Model[columnName]; }
        }

        #endregion
    }

    public abstract class ValueViewModel<TValue> : BaseValueViewModel, IValueViewModel<TValue>
    {
        public new delegate ValueViewModel<TValue> Factory(IKistlContext dataCtx, IValueModel mdl);

        public ValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
            : base(dependencies, dataCtx, mdl)
        {
            this.Model = (IValueModel<TValue>)mdl;
        }

        public new IValueModel<TValue> Model { get; private set; }

        #region IValueViewModel<TValue> Members

        public virtual TValue Value
        {
            get
            {
                return Model.Value;
            }
            set
            {
                Model.Value = value;
            }
        }

        #endregion

        public override bool HasValue
        {
            get { return Model.Value != null; }
        }

        protected override string FormatValue()
        {
            return Value != null ? Value.ToString() : String.Empty;
        }
    }

    public class NullableStructValueViewModel<TValue> : ValueViewModel<Nullable<TValue>>
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
    }

    public class ClassValueViewModel<TValue> : ValueViewModel<TValue>
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
    }

    public class EnumerationPropertyViewModel : NullableStructValueViewModel<int>
    {
        public new delegate EnumerationPropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public EnumerationPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
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

    public class MultiLineStringPropertyViewModel 
        : ClassValueViewModel<string>
    { 
        public new delegate MultiLineStringPropertyViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public MultiLineStringPropertyViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
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
