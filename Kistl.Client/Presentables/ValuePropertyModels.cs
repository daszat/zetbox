using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.App.GUI;
using Kistl.Client.Presentables.GUI;
using Kistl.Client.Presentables.ValueViewModels;

// Legacy code, will be removed tomorrow
namespace Kistl.Client.Presentables
{
    ///// <summary>
    ///// Non generic base class to enable easy autofac usage
    ///// </summary>
    //public abstract class BasePropertyViewModel : ViewModel
    //{
    //    public new delegate BasePropertyViewModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    protected BasePropertyViewModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, Property prop)
    //        : base(appCtx, dataCtx)
    //    {
    //        this.Property = prop;
    //        this.Object = obj;
    //    }

    //    protected BasePropertyViewModel(bool designMode)
    //        : base(designMode)
    //    {
    //    }

    //    public abstract bool IsReadOnly { get; set; }

    //    public Property Property { get; private set; }
    //    public INotifyingObject Object { get; private set; }
    //}

    //public abstract class PropertyModel<TValue>
    //    : BasePropertyViewModel, IDataErrorInfo, ILabeledViewModel
    //{
    //    public new delegate PropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    protected PropertyModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, Property prop)
    //        : base(appCtx, dataCtx, obj, prop)
    //    {
    //        if (obj == null)
    //            throw new ArgumentNullException("obj");
    //        if (prop == null)
    //            throw new ArgumentNullException("prop");

    //        this.AllowNullInput = false;
    //        this.isPropertyReadOnly = prop.IsReadOnly();

    //        this.Property.PropertyChanged += this.PropertyPropertyChangedHandler;
    //        this.Object.PropertyChanged += this.ObjectPropertyChangedHandler;

    //        this.UpdatePropertyValue();
    //        this.CheckConstraints();
    //    }

    //    #region Public Interface

    //    public string Label
    //    {
    //        get
    //        {
    //            return IsInDesignMode ? "Some Label" : (!string.IsNullOrEmpty(Property.Label) ? Property.Label : Property.Name);
    //        }
    //    }

    //    public string ToolTip
    //    {
    //        get
    //        {
    //            return IsInDesignMode
    //                ? "[Design Mode ACTIVE] This property has some value that could be edited here."
    //                : Property.Description;
    //        }
    //    }

    //    public override string Name
    //    {
    //        get { return Label; }
    //    }

    //    public bool AllowNullInput { get; protected set; }
    //    public bool Requiered { get { return !AllowNullInput; } }

    //    public ViewModel Model
    //    {
    //        get
    //        {
    //            return this;
    //        }
    //    }

    //    public override ControlKind RequestedKind
    //    {
    //        get
    //        {
    //            return base.RequestedKind ?? Property.RequestedKind ?? Property.ValueModelDescriptor.DefaultKind;
    //        }
    //        set
    //        {
    //            base.RequestedKind = value;
    //        }
    //    }

    //    protected bool isReadOnlyStore = false;
    //    protected bool isPropertyReadOnly = false;
    //    public override bool IsReadOnly
    //    {
    //        get
    //        {
    //            return isPropertyReadOnly || isReadOnlyStore;
    //        }
    //        set
    //        {
    //            if (isReadOnlyStore != value)
    //            {
    //                isReadOnlyStore = value;
    //                OnPropertyChanged("IsReadOnly");
    //            }
    //        }
    //    }

    //    #endregion

    //    #region Utilities and UI callbacks

    //    /// <summary>
    //    /// Updates the Value in the cache.
    //    /// </summary>
    //    protected abstract void UpdatePropertyValue();

    //    protected void NotifyValueChanged()
    //    {
    //        OnPropertyChanged("Value");
    //        OnPropertyChanged("FormattedValue");
    //        OnPropertyChanged("IsNull");
    //        OnPropertyChanged("HasValue");
    //    }

    //    /// <summary>
    //    /// Checks constraints on the object and puts the results into the cache.
    //    /// </summary> 
    //    protected void CheckConstraints()
    //    {
    //        if (Object is IDataErrorInfo)
    //        {
    //            this.ValueError = ((IDataErrorInfo)Object)[Property.Name];
    //        }
    //    }

    //    #endregion

    //    #region PropertyChanged event handlers

    //    private void ObjectPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
    //    {
    //        if (e.PropertyName == Property.Name)
    //        {
    //            this.UpdatePropertyValue();
    //        }

    //        // TODO: ask constraints about dependencies and reduce check frequency on object changes
    //        this.CheckConstraints();
    //    }

    //    private void PropertyPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
    //    {
    //        switch (e.PropertyName)
    //        {
    //            case "Name":
    //                OnPropertyChanged("Label");
    //                break;
    //            case "Description":
    //                OnPropertyChanged("ToolTip");
    //                break;
    //        }
    //    }

    //    #endregion

    //    #region IDataErrorInfo Members

    //    public string Error
    //    {
    //        get
    //        {
    //            return this["Value"];
    //        }
    //    }

    //    public string this[string columnName]
    //    {
    //        get
    //        {
    //            if (columnName == "Value" || columnName == "FormattedValue")
    //            {
    //                return this.ValueError;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //    }

    //    private string _errorCache;

    //    protected string ValueError
    //    {
    //        get
    //        {
    //            return _errorCache;
    //        }
    //        set
    //        {
    //            if (_errorCache != value)
    //            {
    //                _errorCache = value;

    //                // notify listeners that the error state of the Value has changed
    //                NotifyValueChanged();
    //            }
    //        }
    //    }

    //    #endregion

    //    #region Design Mode

    //    protected PropertyModel(bool designMode)
    //        : base(designMode)
    //    {
    //    }

    //    #endregion

    //}

    //public class NullableValuePropertyModel<TValue>
    //    : PropertyModel<Nullable<TValue>>, IValueViewModel<Nullable<TValue>>, IValueViewModel<string>, IFormattedValueViewModel
    //    where TValue : struct
    //{
    //    public new delegate NullableValuePropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    public NullableValuePropertyModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, ValueTypeProperty prop)
    //        : base(appCtx, dataCtx, obj, prop)
    //    {
    //        this.AllowNullInput = prop.IsNullable();
    //    }

    //    #region Public Interface

    //    public bool HasValue
    //    {
    //        get
    //        {
    //            return _valueCache != null;
    //        }
    //        set
    //        {
    //            if (!value)
    //                this.Value = null;
    //        }
    //    }

    //    public bool IsNull
    //    {
    //        get
    //        {
    //            return _valueCache == null;
    //        }
    //        set
    //        {
    //            if (value)
    //                this.Value = null;
    //        }
    //    }

    //    public void ClearValue()
    //    {
    //        if (this.AllowNullInput) Value = null;
    //        else throw new InvalidOperationException("\"null\" input not allowed");
    //    }
    //    private ICommand _ClearValueCommand = null;
    //    public ICommand ClearValueCommand
    //    {
    //        get
    //        {
    //            if (_ClearValueCommand == null)
    //            {
    //                _ClearValueCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
    //                    .Invoke(DataContext, "Clear value", "Sets the value to nothing", () => ClearValue(), () => AllowNullInput);
    //            }
    //            return _ClearValueCommand;
    //        }
    //    }

    //    private Nullable<TValue> _valueCache;

    //    /// <summary>
    //    /// Gets or sets the value of the property presented by this model.
    //    /// </summary>
    //    public Nullable<TValue> Value
    //    {
    //        get
    //        {
    //            return _valueCache;
    //        }
    //        set
    //        {
    //            if (!_valueCache.HasValue && !value.HasValue)
    //                return;

    //            if (!this.AllowNullInput && value == null)
    //                throw new InvalidOperationException("\"null\" input not allowed");

    //            _valueCache = value;

    //            if (!object.Equals(GetPropertyValue(), value))
    //            {
    //                SetPropertyValue(value);
    //                CheckConstraints();
    //                NotifyValueChanged();
    //            }
    //        }
    //    }

    //    protected virtual string FormatValue()
    //    {
    //        return Value != null ? Value.ToString() : String.Empty;
    //    }

    //    protected virtual void ParseValue(string str)
    //    {
    //        this.Value = String.IsNullOrEmpty(str) ? null : (Nullable<TValue>)System.Convert.ChangeType(str, typeof(TValue));
    //    }

    //    public string FormattedValue
    //    {
    //        get
    //        {
    //            return FormatValue();
    //        }
    //        set
    //        {
    //            ParseValue(value);
    //        }
    //    }

    //    string IValueViewModel<string>.Value
    //    {
    //        get
    //        {
    //            return FormatValue();
    //        }
    //        set
    //        {
    //            ParseValue(value);
    //        }
    //    }
    //    #endregion

    //    #region Utilities and UI callbacks

    //    protected override void UpdatePropertyValue()
    //    {
    //        this.Value = GetPropertyValue();
    //        NotifyValueChanged();
    //    }

    //    /// <summary>
    //    /// Loads the Value from the object.
    //    /// </summary>
    //    /// <returns></returns>
    //    protected virtual TValue? GetPropertyValue()
    //    {
    //        return Object.GetPropertyValue<Nullable<TValue>>(Property.Name);
    //    }

    //    /// <summary>
    //    /// Loads the Value from the object.
    //    /// </summary>
    //    /// <returns></returns>
    //    protected virtual void SetPropertyValue(TValue? val)
    //    {
    //        Object.SetPropertyValue<Nullable<TValue>>(Property.Name, val);
    //    }

    //    #endregion

    //    #region Design Mode

    //    public static NullableValuePropertyModel<TValue> CreateDesignMock(TValue value)
    //    {
    //        return new NullableValuePropertyModel<TValue>(true, value);
    //    }

    //    protected NullableValuePropertyModel(bool designMode, TValue value)
    //        : base(designMode)
    //    {
    //        this.AllowNullInput = true;
    //        _valueCache = value;
    //    }

    //    #endregion
    //}

    //public class ReferencePropertyModel<TValue>
    //    : PropertyModel<TValue>, IValueViewModel<TValue>, IFormattedValueViewModel
    //    where TValue : class
    //{
    //    public new delegate ReferencePropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    public ReferencePropertyModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, ValueTypeProperty prop)
    //        : base(appCtx, dataCtx, obj, prop)
    //    {
    //        this.AllowNullInput = prop.IsNullable();
    //        //var x = new Factory((a, b, c) => { return null; });
    //        //Delegate.CreateDelegate(typeof(UntypedPropertyModel.Factory), x.Method);
    //        //Delegate y = new UntypedPropertyModel.Factory(x);
    //        //var z = Activator.CreateInstance(typeof(UntypedPropertyModel.Factory), x);
    //    }

    //    #region Public Interface

    //    public bool HasValue
    //    {
    //        get
    //        {
    //            return _valueCache != null;
    //        }
    //        set
    //        {
    //            if (!value)
    //                this.Value = null;
    //        }
    //    }

    //    public bool IsNull
    //    {
    //        get
    //        {
    //            return _valueCache == null;
    //        }
    //        set
    //        {
    //            if (value)
    //                this.Value = null;
    //        }
    //    }

    //    public void ClearValue()
    //    {
    //        if (this.AllowNullInput) this.Value = null;
    //        else throw new InvalidOperationException();
    //    }
    //    private ICommand _ClearValueCommand = null;
    //    public ICommand ClearValueCommand
    //    {
    //        get
    //        {
    //            if (_ClearValueCommand == null)
    //            {
    //                _ClearValueCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>()
    //                    .Invoke(DataContext, "Clear value", "Sets the value to nothing", () => ClearValue(), () => AllowNullInput);
    //            }
    //            return _ClearValueCommand;
    //        }
    //    }

    //    private TValue _valueCache;

    //    /// <summary>
    //    /// Gets or sets the value of the property presented by this model
    //    /// </summary>
    //    public TValue Value
    //    {
    //        get
    //        {
    //            return _valueCache;
    //        }
    //        set
    //        {
    //            _valueCache = value;

    //            if (!object.Equals(Object.GetPropertyValue<TValue>(Property.Name), value))
    //            {
    //                Object.SetPropertyValue<TValue>(Property.Name, value);
    //                CheckConstraints();

    //                OnPropertyChanged("Value");
    //                OnPropertyChanged("FormattedValue");
    //                OnPropertyChanged("IsNull");
    //                OnPropertyChanged("HasValue");
    //            }
    //        }
    //    }

    //    protected virtual string FormatValue()
    //    {
    //        return Value != null ? Value.ToString() : String.Empty;
    //    }

    //    protected virtual void ParseValue(string str)
    //    {
    //        this.Value = String.IsNullOrEmpty(str) ? null : (TValue)System.Convert.ChangeType(str, typeof(TValue));
    //    }

    //    public string FormattedValue
    //    {
    //        get
    //        {
    //            return FormatValue();
    //        }
    //        set
    //        {
    //            ParseValue(value);
    //        }
    //    }
    //    #endregion

    //    #region Utilities and UI callbacks

    //    protected override void UpdatePropertyValue()
    //    {
    //        this.Value = Object.GetPropertyValue<TValue>(Property.Name);
    //        NotifyValueChanged();
    //    }

    //    #endregion

    //}

    //public class ChooseReferencePropertyModel<TValue>
    //    : ReferencePropertyModel<TValue>
    //    where TValue : class
    //{
    //    public new delegate ChooseReferencePropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    public ChooseReferencePropertyModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, ValueTypeProperty prop)
    //        : base(appCtx, dataCtx, obj, prop)
    //    {
    //        PossibleValues = new ObservableCollection<TValue>();
    //    }

    //    #region Public Interface

    //    public ObservableCollection<TValue> PossibleValues { get; private set; }

    //    #endregion
    //}

    //public class EnumerationPropertyModel
    //    : NullableValuePropertyModel<int>
    //{
    //    public new delegate EnumerationPropertyModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    public EnumerationPropertyModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, EnumerationProperty prop)
    //        : base(appCtx, dataCtx, obj, prop)
    //    {
    //        var enumValues = prop.Enumeration.EnumerationEntries.Select(e => new KeyValuePair<int?, string>(e.Value, e.Name));
    //        this.PossibleValues = new ReadOnlyCollection<KeyValuePair<int?, string>>(
    //            new[] { new KeyValuePair<int?, string>(null, "") }
    //            .Concat(enumValues)
    //            .ToList()
    //        );
    //    }

    //    #region Public Interface

    //    public ReadOnlyCollection<KeyValuePair<int?, string>> PossibleValues { get; private set; }

    //    #endregion

    //    #region Utilities and UI callbacks

    //    protected override int? GetPropertyValue()
    //    {
    //        // Work around the fact that the conversion from enumeration to int? is not possible.
    //        object val = Object.GetPropertyValue<object>(Property.Name);
    //        if (val == null)
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            return (int)val;
    //        }
    //    }

    //    protected override void SetPropertyValue(int? val)
    //    {
    //        // Work around the fact that the conversion from enumeration to int? is not possible.
    //        if (val == null)
    //        {
    //            Object.SetPropertyValue<object>(Property.Name, null);
    //        }
    //        else
    //        {
    //            Object.SetPropertyValue<object>(Property.Name, Enum.ToObject(((EnumerationProperty)Property).Enumeration.GetDataType(), val));
    //        }
    //    }

    //    protected override string FormatValue()
    //    {
    //        if (Value == null) return string.Empty;
    //        // This hurts, but looks funny
    //        return PossibleValues.Single(key => key.Key == Value.Value).Value;
    //    }

    //    protected override void ParseValue(string str)
    //    {
    //        if (string.IsNullOrEmpty(str))
    //        {
    //            Value = null;
    //        }
    //        else
    //        {
    //            var item = PossibleValues.SingleOrDefault(value => string.Compare(value.Value, str, true) == 0);
    //            if (item.Key != null)
    //            {
    //                Value = item.Key.Value;
    //            }
    //            else
    //            {
    //                // TODO: Set Error
    //            }
    //        }
    //    }

    //    #endregion
    //}

    //[ViewModelDescriptor("KistlBase", DefaultKind = "Kistl.App.GUI.MultiLineTextboxKind", Description = "PropertyViewModel for multiline string properties")]
    //public class MultiLineStringPropertyModel
    //    : ReferencePropertyModel<string>
    //{
    //    public new delegate MultiLineStringPropertyModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    public MultiLineStringPropertyModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, ValueTypeProperty prop)
    //        : base(appCtx, dataCtx, obj, prop)
    //    {
    //    }

    //    protected override void OnPropertyChanged(string propertyName)
    //    {
    //        base.OnPropertyChanged(propertyName);
    //        if (propertyName == "Value")
    //        {
    //            base.OnPropertyChanged("ShortText");
    //        }
    //    }

    //    public string ShortText
    //    {
    //        get
    //        {
    //            if (!string.IsNullOrEmpty(Value) && Value.Length > 20)
    //            {
    //                return Value.Replace("\r", "").Replace('\n', ' ').Substring(0, 20) + "...";
    //            }
    //            else
    //            {
    //                return Value;
    //            }
    //        }
    //    }

    //    private ICommand _EditCommand = null;
    //    public ICommand EditCommand
    //    {
    //        get
    //        {
    //            if (_EditCommand == null)
    //            {
    //                _EditCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(DataContext, "Edit", "Opens a Editor Dialog", () => Edit(), null);
    //            }
    //            return _EditCommand;
    //        }
    //    }

    //    public void Edit()
    //    {
    //        ModelFactory.ShowModel(
    //                ModelFactory.CreateViewModel<MultiLineEditorDialogModel.Factory>().Invoke(
    //                    DataContext,
    //                    Value,
    //                    (v) => Value = v),
    //                true);
    //    }
    //}

    //public class NullableDateTimePropertyModel
    //    : NullableValuePropertyModel<DateTime>
    //{
    //    public new delegate NullableDateTimePropertyModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

    //    private DateTimeProperty _dtProp;

    //    public NullableDateTimePropertyModel(
    //        IViewModelDependencies appCtx, IKistlContext dataCtx,
    //        INotifyingObject obj, ValueTypeProperty prop)
    //        : base(appCtx, dataCtx, obj, prop)
    //    {
    //        if (prop == null) throw new ArgumentNullException("prop");
    //        _dtProp = (DateTimeProperty)prop;
    //    }

    //    protected override string FormatValue()
    //    {
    //        if (Value == null) return string.Empty;
    //        switch(_dtProp.DateTimeStyle)
    //        {
    //            case DateTimeStyles.Date:
    //                return Value.Value.ToShortDateString();
    //            case DateTimeStyles.Time:
    //                return Value.Value.ToShortTimeString();
    //            default:
    //                return Value.Value.ToString();
    //        }
    //    }
    //}
}
