using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{

    public interface IReadOnlyValueModel<TValue>
        : INotifyPropertyChanged
    {
        /// <summary>
        /// Whether or not the property has a value. <seealso cref="IsNull"/>
        /// </summary>
        bool HasValue { get; }
        /// <summary>
        /// Whether or not the property is null. <seealso cref="HasValue"/>
        /// </summary>
        bool IsNull { get; }

        /// <summary>
        /// A label to display with the Value
        /// </summary>
        string Label { get; }

        /// <summary>
        /// A tooltip to display with the Value
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        /// The value of this model
        /// </summary>
        TValue Value { get; }

    }

    public interface IClearableValue
    {
        void ClearValue();
    }

    public interface IValueModel<TValue>
        : IReadOnlyValueModel<TValue>, IClearableValue, INotifyPropertyChanged
    {
        /// <summary>
        /// The value of this model
        /// </summary>
        new TValue Value { get; set; }

        /// <summary>
        /// Whether or not to allow <value>null</value> as input
        /// </summary>
        bool AllowNullInput { get; }
    }

    public abstract class PropertyModel<TValue> : PresentableModel, IDataErrorInfo
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

            Object = obj;
            Property = bp;

            Property.PropertyChanged += PropertyPropertyChangedHandler;
            Object.PropertyChanged += ObjectPropertyChangedHandler;

            GetPropertyValue();
            CheckConstraints();
        }

        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return IsInDesignMode ? "Some Label" : Property.PropertyName; } }
        public string ToolTip
        {
            get
            {
                return IsInDesignMode
                    ? "[Design Mode ACTIVE] This property has some value that could be edited here."
                    // TODO: proxying implementations might block on that
                    : Property.AltText;
            }
        }

        #endregion

        #region Utilities and UI callbacks

        /// <summary>
        /// Loads the Value from the object into the cache.
        /// </summary>
        protected abstract void GetPropertyValue();

        /// <summary>
        /// Checks constraints on the object and puts the results into the cache.
        /// </summary> 
        protected void CheckConstraints()
        {
            ValueError = Object[Property.PropertyName];
        }

        #endregion

        #region PropertyChanged event handlers

        private void ObjectPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Property.PropertyName)
            {
                GetPropertyValue();
            }

            // TODO: ask constraints about dependencies and reduce check frequency on object changes
            CheckConstraints();
        }

        private void PropertyPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PropertyName": OnPropertyChanged("Label"); break;
                case "AltText": OnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error { get { return this["Value"]; } }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Value")
                    return ValueError;
                else
                    return null;
            }
        }

        private string _errorCache;
        protected string ValueError
        {
            get { UI.Verify(); return _errorCache; }
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

        protected PropertyModel(bool designMode) : base(designMode) { }

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
            AllowNullInput = prop.IsNullable;
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
                    Value = null;
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
                    Value = null;
            }
        }

        public bool AllowNullInput { get; private set; }

        public void ClearValue()
        {
            if (AllowNullInput) Value = null;
            else throw new InvalidOperationException("\"null\" input not allowed");
        }

        private Nullable<TValue> _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public Nullable<TValue> Value
        {
            get { return _valueCache; }
            set
            {
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                if (!AllowNullInput && value == null)
                    throw new InvalidOperationException("\"null\" input not allowed");

                _valueCache = value;

                Object.SetPropertyValue<Nullable<TValue>>(Property.PropertyName, value);
                CheckConstraints();

                OnPropertyChanged("Value");
                OnPropertyChanged("IsNull");
                OnPropertyChanged("HasValue");
            }
        }

        string IValueModel<string>.Value
        {
            get
            {
                return _valueCache != null ? _valueCache.ToString() : "";
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
                return _valueCache != null ? _valueCache.ToString() : "";
            }
        }


        #endregion

        #region Utilities and UI callbacks

        protected override void GetPropertyValue()
        {
            Value = Object.GetPropertyValue<Nullable<TValue>>(Property.PropertyName);
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
            AllowNullInput = true;
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
            AllowNullInput = prop.IsNullable;
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
                    Value = null;
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
                    Value = null;
            }
        }

        public bool AllowNullInput { get; private set; }

        public void ClearValue()
        {
            if (AllowNullInput) Value = null;
            else throw new InvalidOperationException();
        }

        private TValue _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public TValue Value
        {
            get { return _valueCache; }
            set
            {
                _valueCache = value;

                Object.SetPropertyValue<TValue>(Property.PropertyName, value);
                CheckConstraints();

                OnPropertyChanged("Value");
                OnPropertyChanged("IsNull");
                OnPropertyChanged("HasValue");
            }
        }

        #endregion

        #region Utilities and UI callbacks

        protected override void GetPropertyValue()
        {
            Value = Object.GetPropertyValue<TValue>(Property.PropertyName);
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
            PossibleValues = new ReadOnlyCollection<TValue>(Enum.GetValues(typeof(TValue)).Cast<TValue>().ToList());
            PossibleStringValues = new ReadOnlyCollection<string>(PossibleValues.Select(v => v.ToString()).ToList());
        }

        #region Public Interface

        public ReadOnlyCollection<TValue> PossibleValues { get; private set; }

        /// <summary>
        /// When using these values in the UI, please use the value's index to
        /// lookup the actual Value in <see cref="PossibleValues"/>.
        /// </summary>
        public ReadOnlyCollection<string> PossibleStringValues { get; private set; }

        #endregion
    }

}
