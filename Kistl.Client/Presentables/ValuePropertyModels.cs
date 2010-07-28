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

namespace Kistl.Client.Presentables
{
    /// <summary>
    /// A Model describing a read-only value of type <paramref name="TValue"/>, usually read from a property or a method return value.
    /// </summary>
    /// <typeparam name="TValue">the type of the presented value</typeparam>
    public interface IReadOnlyValueModel<TValue>
        : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets a value indicating whether or not the property has a value.
        /// </summary>
        /// <seealso cref="IsNull"/>
        bool HasValue { get; }

        /// <summary>
        /// Gets a value indicating whether or not the property is null.
        /// </summary>
        /// <seealso cref="HasValue"/>
        bool IsNull { get; }

        /// <summary>
        /// Gets a label to display with the Value.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets a tooltip to display with the Value.
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        /// Gets the value of this model.
        /// </summary>
        TValue Value { get; }
    }

    /// <summary>
    /// This interface provides a method for nullable ValueModels to allow removing the value easily.
    /// </summary>
    public interface IClearableValue
    {
        /// <summary>
        /// Clears the value of this Model. After calling this method the value should be <value>null</value> or "empty".
        /// </summary>
        void ClearValue();

        ICommand ClearValueCommand { get; }
    }

    public interface IValueModel<TValue>
        : IReadOnlyValueModel<TValue>, IClearableValue, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the value of this model.
        /// </summary>
        new TValue Value { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not to allow <value>null</value> as input.
        /// </summary>
        bool AllowNullInput { get; }
    }

    public abstract class BasePropertyModel : ViewModel
    {
        public new delegate BasePropertyModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        protected BasePropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, Property prop)
            : base(appCtx, dataCtx)
        {
        }

        protected BasePropertyModel(bool designMode)
            : base(designMode)
        {
        }
    }

    public abstract class PropertyModel<TValue>
        : BasePropertyModel, IPropertyValueModel, IDataErrorInfo, ILabeledViewModel
    {
        public new delegate PropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        protected PropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, Property prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (prop == null)
                throw new ArgumentNullException("prop");

            this.AllowNullInput = false;
            this.Object = obj;
            this.Property = prop;

            this.Property.PropertyChanged += this.PropertyPropertyChangedHandler;
            this.Object.PropertyChanged += this.ObjectPropertyChangedHandler;

            this.UpdatePropertyValue();
            this.CheckConstraints();
        }

        #region Public Interface

        public string Label
        {
            get
            {
                return IsInDesignMode ? "Some Label" : Property.Name;
            }
        }

        public string ToolTip
        {
            get
            {
                return IsInDesignMode
                    ? "[Design Mode ACTIVE] This property has some value that could be edited here."
                    : Property.Description;
            }
        }

        public override string Name
        {
            get { return Label; }
        }

        public bool AllowNullInput { get; protected set; }
        public bool Requiered { get { return !AllowNullInput; } }

        public ViewModel Model
        {
            get
            {
                return this;
            }
        }

        public override ControlKind RequestedKind
        {
            get
            {
                return base.RequestedKind ?? Property.RequestedKind ?? Property.ValueModelDescriptor.DefaultKind;
            }
            set
            {
                base.RequestedKind = value;
            }
        }

        #endregion

        #region Utilities and UI callbacks

        /// <summary>
        /// Updates the Value in the cache.
        /// </summary>
        protected abstract void UpdatePropertyValue();

        /// <summary>
        /// Checks constraints on the object and puts the results into the cache.
        /// </summary> 
        protected void CheckConstraints()
        {
            if (Object is IDataErrorInfo)
            {
                this.ValueError = ((IDataErrorInfo)Object)[Property.Name];
            }
        }

        #endregion

        #region PropertyChanged event handlers

        private void ObjectPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Property.Name)
            {
                this.UpdatePropertyValue();
            }

            // TODO: ask constraints about dependencies and reduce check frequency on object changes
            this.CheckConstraints();
        }

        private void PropertyPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                    OnPropertyChanged("Label");
                    break;
                case "Description":
                    OnPropertyChanged("ToolTip");
                    break;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                return this["Value"];
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Value")
                {
                    return this.ValueError;
                }
                else
                {
                    return null;
                }
            }
        }

        private string _errorCache;

        protected string ValueError
        {
            get
            {
                return _errorCache;
            }
            set
            {
                if (_errorCache != value)
                {
                    _errorCache = value;

                    // notify listeners that the error state of the Value has changed
                    OnPropertyChanged("Value");
                }
            }
        }

        #endregion

        protected INotifyingObject Object { get; private set; }

        protected Property Property { get; private set; }

        #region Design Mode

        protected PropertyModel(bool designMode)
            : base(designMode)
        {
        }

        #endregion

    }

    public class NullableValuePropertyModel<TValue>
        : PropertyModel<Nullable<TValue>>, IValueModel<Nullable<TValue>>, IValueModel<string>
        where TValue : struct
    {
        public new delegate NullableValuePropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        public NullableValuePropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            this.AllowNullInput = prop.IsNullable();
        }

        #region Public Interface

        public bool HasValue
        {
            get
            {
                return _valueCache != null;
            }
            set
            {
                if (!value)
                    this.Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                return _valueCache == null;
            }
            set
            {
                if (value)
                    this.Value = null;
            }
        }

        public void ClearValue()
        {
            if (this.AllowNullInput) Value = null;
            else throw new InvalidOperationException("\"null\" input not allowed");
        }
        private ICommand _ClearValueCommand = null;
        public ICommand ClearValueCommand
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

        private Nullable<TValue> _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model.
        /// </summary>
        public Nullable<TValue> Value
        {
            get
            {
                return _valueCache;
            }
            set
            {
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                if (!this.AllowNullInput && value == null)
                    throw new InvalidOperationException("\"null\" input not allowed");

                _valueCache = value;

                if (!object.Equals(GetPropertyValue(), value))
                {
                    SetPropertyValue(value);
                    CheckConstraints();

                    OnPropertyChanged("Value");
                    OnPropertyChanged("IsNull");
                    OnPropertyChanged("HasValue");
                }
            }
        }

        string IValueModel<string>.Value
        {
            get
            {
                return _valueCache != null ? _valueCache.ToString() : String.Empty;
            }
            set
            {
                this.Value = String.IsNullOrEmpty(value) ? null : (Nullable<TValue>)System.Convert.ChangeType(value, typeof(TValue));
            }
        }

        string IReadOnlyValueModel<string>.Value
        {
            get
            {
                return _valueCache != null ? _valueCache.ToString() : String.Empty;
            }
        }

        #endregion

        #region Utilities and UI callbacks

        protected override void UpdatePropertyValue()
        {
            this.Value = GetPropertyValue();
        }

        /// <summary>
        /// Loads the Value from the object.
        /// </summary>
        /// <returns></returns>
        protected virtual TValue? GetPropertyValue()
        {
            return Object.GetPropertyValue<Nullable<TValue>>(Property.Name);
        }

        /// <summary>
        /// Loads the Value from the object.
        /// </summary>
        /// <returns></returns>
        protected virtual void SetPropertyValue(TValue? val)
        {
            Object.SetPropertyValue<Nullable<TValue>>(Property.Name, val);
        }

        #endregion

        #region Design Mode

        public static NullableValuePropertyModel<TValue> CreateDesignMock(TValue value)
        {
            return new NullableValuePropertyModel<TValue>(true, value);
        }

        protected NullableValuePropertyModel(bool designMode, TValue value)
            : base(designMode)
        {
            this.AllowNullInput = true;
            _valueCache = value;
        }

        #endregion
    }

    public class ReferencePropertyModel<TValue>
        : PropertyModel<TValue>, IValueModel<TValue>
        where TValue : class
    {
        public new delegate ReferencePropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        public ReferencePropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            this.AllowNullInput = prop.IsNullable();
            //var x = new Factory((a, b, c) => { return null; });
            //Delegate.CreateDelegate(typeof(UntypedPropertyModel.Factory), x.Method);
            //Delegate y = new UntypedPropertyModel.Factory(x);
            //var z = Activator.CreateInstance(typeof(UntypedPropertyModel.Factory), x);
        }

        #region Public Interface

        public bool HasValue
        {
            get
            {
                return _valueCache != null;
            }
            set
            {
                if (!value)
                    this.Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                return _valueCache == null;
            }
            set
            {
                if (value)
                    this.Value = null;
            }
        }

        public void ClearValue()
        {
            if (this.AllowNullInput) this.Value = null;
            else throw new InvalidOperationException();
        }
        private ICommand _ClearValueCommand = null;
        public ICommand ClearValueCommand
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

        private TValue _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public TValue Value
        {
            get
            {
                return _valueCache;
            }
            set
            {
                _valueCache = value;

                if (!object.Equals(Object.GetPropertyValue<TValue>(Property.Name), value))
                {
                    Object.SetPropertyValue<TValue>(Property.Name, value);
                    CheckConstraints();

                    OnPropertyChanged("Value");
                    OnPropertyChanged("IsNull");
                    OnPropertyChanged("HasValue");
                }
            }
        }

        #endregion

        #region Utilities and UI callbacks

        protected override void UpdatePropertyValue()
        {
            this.Value = Object.GetPropertyValue<TValue>(Property.Name);
        }

        #endregion

    }

    public class ChooseReferencePropertyModel<TValue>
        : ReferencePropertyModel<TValue>
        where TValue : class
    {
        public new delegate ChooseReferencePropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        public ChooseReferencePropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            PossibleValues = new ObservableCollection<TValue>();
        }

        #region Public Interface

        public ObservableCollection<TValue> PossibleValues { get; private set; }

        #endregion
    }

    public class EnumerationPropertyModel<TValue>
        : NullableValuePropertyModel<TValue>
        where TValue : struct
    {
        public new delegate EnumerationPropertyModel<TValue> Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        public EnumerationPropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, EnumerationProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            this.PossibleValues = new ReadOnlyCollection<KeyValuePair<TValue, string>>(Enum.GetValues(typeof(TValue))
                .Cast<TValue>().Select(e => new KeyValuePair<TValue, string>(e, e.ToString())).ToList());
        }

        #region Public Interface

        public ReadOnlyCollection<KeyValuePair<TValue, string>> PossibleValues { get; private set; }

        #endregion
    }

    public class EnumerationPropertyModel
        : NullableValuePropertyModel<int>
    {
        public new delegate EnumerationPropertyModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        public EnumerationPropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, EnumerationProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            var enumValues = prop.Enumeration.EnumerationEntries.Select(e => new KeyValuePair<int?, string>(e.Value, e.Name));
            this.PossibleValues = new ReadOnlyCollection<KeyValuePair<int?, string>>(
                new[] { new KeyValuePair<int?, string>(null, "") }
                .Concat(enumValues)
                .ToList()
            );
        }

        #region Public Interface

        public ReadOnlyCollection<KeyValuePair<int?, string>> PossibleValues { get; private set; }

        #endregion

        #region Utilities and UI callbacks

        protected override int? GetPropertyValue()
        {
            // Work around the fact that the conversion from enumeration to int? is not possible.
            object val = Object.GetPropertyValue<object>(Property.Name);
            if (val == null)
            {
                return null;
            }
            else
            {
                return (int)val;
            }
        }

        protected override void SetPropertyValue(int? val)
        {
            // Work around the fact that the conversion from enumeration to int? is not possible.
            if (val == null)
            {
                Object.SetPropertyValue<object>(Property.Name, null);
            }
            else
            {
                Object.SetPropertyValue<object>(Property.Name, Enum.ToObject(((EnumerationProperty)Property).Enumeration.GetDataType(), val));
            }
        }

        #endregion
    }

    [ViewModelDescriptor("KistlBase", DefaultKind = "Kistl.App.GUI.MultiLineTextboxKind", Description = "PropertyViewModel for multiline string properties")]
    public class MultiLineStringPropertyModel
        : ReferencePropertyModel<string>
    {
        public new delegate MultiLineStringPropertyModel Factory(IKistlContext dataCtx, INotifyingObject obj, Property prop);

        public MultiLineStringPropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            INotifyingObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
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

        public string ShortText
        {
            get
            {
                if (!string.IsNullOrEmpty(Value) && Value.Length > 20)
                {
                    return Value.Replace("\r", "").Replace('\n', ' ').Substring(0, 20) + "...";
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

}
