using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using System.Diagnostics;

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
        public PropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, BaseProperty bp)
            : base(appCtx, dataCtx)
        {
            if (obj == null)
                new ArgumentNullException("obj");
            if (bp == null)
                new ArgumentNullException("bp");

            Object = obj;
            Property = bp;

            Property.PropertyChanged += AsyncPropertyPropertyChanged;
            Object.PropertyChanged += AsyncObjectPropertyChanged;
            Async.Queue(DataContext, () =>
            {
                this.AsyncGetPropertyValue();
                this.AsyncCheckConstraints();
                UI.Queue(UI, () => { this.State = ModelState.Active; });
            });
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

        #region Async handlers and UI callbacks

        /// <summary>
        /// Loads the Value from the object into the cache.
        /// Called on the Async Thread.
        /// </summary>
        protected abstract void AsyncGetPropertyValue();

        /// <summary>
        /// Checks constraints on the object and puts the results into the cache.
        /// Called on the Async Thread.
        /// </summary> 
        protected void AsyncCheckConstraints()
        {
            Async.Verify();
            string newError = Object[Property.PropertyName];
            UI.Queue(UI, () => this.ValueError = newError);
        }

        #endregion

        #region PropertyChanged event handlers

        private void AsyncObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();

            // although we're already Async here, defer actually checking 
            // the object onto another thread
            Async.Queue(DataContext, () =>
            {
                // flag to the user that something's happening
                UI.Queue(UI, () => this.State = ModelState.Loading);

                if (e.PropertyName == Property.PropertyName)
                {
                    AsyncGetPropertyValue();
                }
                // TODO: ask constraints about dependencies and reduce check frequency
                AsyncCheckConstraints();
                // all updates done
                UI.Queue(UI, () => this.State = ModelState.Active);
            });
        }

        private void AsyncPropertyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();
            switch (e.PropertyName)
            {
                case "PropertyName": AsyncOnPropertyChanged("Label"); break;
                case "AltText": AsyncOnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error { get { return this["Value"]; } }

        public string this[string columnName]
        {
            get
            {
                UI.Verify();
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
                UI.Verify();
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
        protected BaseProperty Property { get; private set; }

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
                UI.Verify();
                return _valueCache != null;
            }
            set
            {
                UI.Verify();
                if (!value)
                    Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                UI.Verify();
                return _valueCache == null;
            }
            set
            {
                UI.Verify();
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
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                if (!AllowNullInput && value == null)
                    throw new InvalidOperationException("\"null\" input not allowed");

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(DataContext, () =>
                {
                    Object.SetPropertyValue<Nullable<TValue>>(Property.PropertyName, value);
                    AsyncCheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
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
                this.Value = string.IsNullOrEmpty(value) ? null : (Nullable<TValue>)System.Convert.ChangeType(value, typeof(TValue));
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

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            Nullable<TValue> newValue = Object.GetPropertyValue<Nullable<TValue>>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue);
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
                UI.Verify();
                return _valueCache != null;
            }
            set
            {
                UI.Verify();
                if (!value)
                    Value = null;
            }
        }

        public bool IsNull
        {
            get
            {
                UI.Verify();
                return _valueCache == null;
            }
            set
            {
                UI.Verify();
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
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(DataContext, () =>
                {
                    Object.SetPropertyValue<TValue>(Property.PropertyName, value);
                    AsyncCheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
                OnPropertyChanged("Value");
                OnPropertyChanged("IsNull");
                OnPropertyChanged("HasValue");
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void AsyncGetPropertyValue()
        {
            Async.Verify();
            TValue newValue = Object.GetPropertyValue<TValue>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue);
        }

        #endregion

    }

}
