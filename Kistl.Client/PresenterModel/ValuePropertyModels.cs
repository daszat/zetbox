using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using System.Diagnostics;

namespace Kistl.Client.PresenterModel
{

    public abstract class PropertyModel<TValue> : PresentableModel, IDataErrorInfo
    {
        public PropertyModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj, BaseProperty bp)
            : base(uiManager, asyncManager)
        {
            Object = obj;
            Property = bp;
            Property.PropertyChanged += PropertyPropertyChanged;
            Object.PropertyChanged += ObjectPropertyChanged;
            Async.Queue(Object.Context, () =>
            {
                this.GetPropertyValue();
                this.CheckConstraints();
                UI.Queue(UI, () => { this.State = ModelState.Active; });
            });
        }

        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return Property.PropertyName; } }
        // TODO: proxying implementations might block on that
        public string ToolTip { get { return Property.AltText; } }

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public abstract TValue Value { get; set; }

        #endregion

        #region Async handlers and UI callbacks

        /// <summary>
        /// Loads the Value from the object into the cache.
        /// Called on the Async Thread.
        /// </summary>
        protected abstract void GetPropertyValue();

        /// <summary>
        /// Checks constraints on the object and puts the results into the cache.
        /// Called on the Async Thread.
        /// </summary> 
        protected void CheckConstraints()
        {
            Async.Verify();
            string newError = Object[Property.PropertyName];
            UI.Queue(UI, () => this.ValueError = newError);
        }

        #endregion

        #region PropertyChanged event handlers

        private void ObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Queue(Object.Context, () =>
            {
                // flag to the user that something's happening
                UI.Queue(UI, () => this.State = ModelState.Loading);

                if (e.PropertyName == Property.PropertyName)
                {
                    GetPropertyValue();
                }
                // TODO: ask constraints about dependencies and reduce check frequency
                CheckConstraints();
                // all updates done
                UI.Queue(UI, () => this.State = ModelState.Active);
            });
        }


        private void PropertyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();
            switch (e.PropertyName)
            {
                case "PropertyName": InvokePropertyChanged("Label"); break;
                case "AltText": InvokePropertyChanged("ToolTip"); break;
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
    }

    public abstract class ValuePropertyModel<TValue>
        : PropertyModel<TValue>
        where TValue : struct
    {
        public ValuePropertyModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj, ValueTypeProperty prop)
            : base(uiManager, asyncManager, obj, prop)
        {
        }

        #region Public Interface

        public bool IsNull { get { UI.Verify(); return false; } }

        private TValue _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(Object.Context, () =>
                {
                    Object.SetPropertyValue<TValue>(Property.PropertyName, value);
                    CheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
                OnPropertyChanged("Value");
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void GetPropertyValue()
        {
            Async.Verify();
            TValue newValue = Object.GetPropertyValue<TValue>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue);
        }

        #endregion

    }


    public abstract class NullableValuePropertyModel<TValue>
        : PropertyModel<Nullable<TValue>>
        where TValue : struct
    {
        public NullableValuePropertyModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj, ValueTypeProperty prop)
            : base(uiManager, asyncManager, obj, prop)
        {
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


        private Nullable<TValue> _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override Nullable<TValue> Value
        {
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(Object.Context, () =>
                {
                    Object.SetPropertyValue<Nullable<TValue>>(Property.PropertyName, value);
                    CheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
                OnPropertyChanged("Value");
                OnPropertyChanged("IsNull");
                OnPropertyChanged("HasValue");
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void GetPropertyValue()
        {
            Async.Verify();
            Nullable<TValue> newValue = Object.GetPropertyValue<Nullable<TValue>>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue);
        }

        #endregion

    }

    public abstract class ReferencePropertyModel<TValue>
        : PropertyModel<TValue>
        where TValue : class
    {
        public ReferencePropertyModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj, ValueTypeProperty prop)
            : base(uiManager, asyncManager, obj, prop)
        {
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

        private TValue _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(Object.Context, () =>
                {
                    Object.SetPropertyValue<TValue>(Property.PropertyName, value);
                    CheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
                OnPropertyChanged("Value");
                OnPropertyChanged("IsNull");
                OnPropertyChanged("HasValue");
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void GetPropertyValue()
        {
            Async.Verify();
            TValue newValue = Object.GetPropertyValue<TValue>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue);
        }

        #endregion

    }

}
