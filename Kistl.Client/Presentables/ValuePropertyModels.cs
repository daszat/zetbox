using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;

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

    public abstract class PropertyModel<TValue>
        : PresentableModel, IPropertyValueModel, IDataErrorInfo
    {
        protected PropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Property bp)
            : base(appCtx, dataCtx)
        {
            if (obj == null)
                new ArgumentNullException("obj");
            if (bp == null)
                new ArgumentNullException("bp");

            this.Object = obj;
            this.Property = bp;

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
                return IsInDesignMode ? "Some Label" : Property.PropertyName;
            }
        }

        public string ToolTip
        {
            get
            {
                return IsInDesignMode
                    ? "[Design Mode ACTIVE] This property has some value that could be edited here."
                    : Property.AltText;
            }
        }

        public override string Name
        {
            get { return Label; }
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
            this.ValueError = Object[Property.PropertyName];
        }

        #endregion

        #region PropertyChanged event handlers

        private void ObjectPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Property.PropertyName)
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
                case "PropertyName":
                    OnPropertyChanged("Label");
                    break;
                case "AltText":
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

        protected IDataObject Object { get; private set; }

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
        public NullableValuePropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            this.AllowNullInput = prop.IsNullable;
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

        public bool AllowNullInput { get; private set; }

        public void ClearValue()
        {
            if (this.AllowNullInput) Value = null;
            else throw new InvalidOperationException("\"null\" input not allowed");
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
                    Object.SetPropertyValue<Nullable<TValue>>(Property.PropertyName, value);
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
            return Object.GetPropertyValue<Nullable<TValue>>(Property.PropertyName);
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
        public ReferencePropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, ValueTypeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            this.AllowNullInput = prop.IsNullable;
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

        public bool AllowNullInput { get; private set; }

        public void ClearValue()
        {
            if (this.AllowNullInput) this.Value = null;
            else throw new InvalidOperationException();
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

                if (!object.Equals(Object.GetPropertyValue<TValue>(Property.PropertyName), value))
                {
                    Object.SetPropertyValue<TValue>(Property.PropertyName, value);
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
            this.Value = Object.GetPropertyValue<TValue>(Property.PropertyName);
        }

        #endregion

    }

    public class ChooseReferencePropertyModel<TValue>
        : ReferencePropertyModel<TValue>
        where TValue : class
    {
        public ChooseReferencePropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, ValueTypeProperty prop)
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
        public EnumerationPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, EnumerationProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            this.PossibleValues = new ReadOnlyCollection<TValue>(Enum.GetValues(typeof(TValue)).Cast<TValue>().ToList());
            this.PossibleStringValues = new ReadOnlyCollection<string>(this.PossibleValues.Select(v => v.ToString()).ToList());
        }

        #region Public Interface

        public ReadOnlyCollection<TValue> PossibleValues { get; private set; }

        /// <summary>
        /// Gets a list of strings representing possible values.
        /// Please use the strings's index to lookup the actual Value in <see cref="PossibleValues"/>.
        /// </summary>
        public ReadOnlyCollection<string> PossibleStringValues { get; private set; }

        #endregion
    }

    public class EnumerationPropertyModel
        : NullableValuePropertyModel<int>
    {
        public EnumerationPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, EnumerationProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            this.PossibleValues = new ReadOnlyCollection<int>(prop.Enumeration.EnumerationEntries.Select(e => e.Value).ToList());
            this.PossibleStringValues = new ReadOnlyCollection<string>(prop.Enumeration.EnumerationEntries.Select(e => e.Name).ToList());
        }

        #region Public Interface

        public ReadOnlyCollection<int> PossibleValues { get; private set; }

        /// <summary>
        /// Gets a list of strings representing possible values.
        /// Please use the strings's index to lookup the actual Value in <see cref="PossibleValues"/>.
        /// </summary>
        public ReadOnlyCollection<string> PossibleStringValues { get; private set; }

        #endregion

        #region Utilities and UI callbacks

        protected override int? GetPropertyValue()
        {
            // Work around the fact that the conversion from enumeration to int? is not possible.
            object val = Object.GetPropertyValue<object>(Property.PropertyName);
            if (val == null)
            {
                return null;
            }
            else
            {
                return (int)val;
            }
        }

        #endregion
    }

}
