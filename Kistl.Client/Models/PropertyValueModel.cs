
namespace Kistl.Client.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;
    using Kistl.API;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using System.ComponentModel;

    /// <summary>
    /// For autofac
    /// </summary>
    public abstract class BasePropertyValueModel : IValueModel
    {
        public delegate BasePropertyValueModel Factory(INotifyingObject obj, Property prop);

        public BasePropertyValueModel(INotifyingObject obj, Property prop)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (prop == null) throw new ArgumentNullException("prop");

            this.Property = prop;
            this.Object = obj;
        }

        public Property Property { get; private set; }
        public INotifyingObject Object { get; private set; }

        #region IValueModel Members

        private bool? _AllowNullInput = null;
        public bool AllowNullInput
        {
            get
            {
                if (_AllowNullInput == null)
                {
                    _AllowNullInput = Property.IsNullable();
                }
                return _AllowNullInput.Value;
            }
        }

        public string Label
        {
            get { return !string.IsNullOrEmpty(Property.Label) ? Property.Label : Property.Name; }
        }

        public string Description
        {
            get { return Property.Description; }
        }

        private bool? _IsReadOnly = null;
        public bool IsReadOnly
        {
            get
            {
                if (_IsReadOnly == null)
                {
                    _IsReadOnly = Property.IsReadOnly();
                }
                return _IsReadOnly.Value;
            }
        }

        public abstract void ClearValue();

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies all listeners of PropertyChanged about a change in a property
        /// </summary>
        /// <param name="propertyName">the changed property</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var tmp = PropertyChanged;
            if (tmp != null)
            {
                tmp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void NotifyValueChanged()
        {
            OnPropertyChanged("Value");
        }
        #endregion

        #region Error Checks
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
                    NotifyValueChanged();
                }
            }
        }

        #endregion
    }

    public abstract class PropertyValueModel<TValue> : BasePropertyValueModel, IValueModel<TValue>
    {
        public new delegate PropertyValueModel<TValue> Factory(INotifyingObject obj, Property prop);

        public PropertyValueModel(INotifyingObject obj, Property prop)
            : base(obj, prop)
        {
        }

        #region IValueModel<TValue> Members

        public abstract TValue Value { get; set; }

        #endregion
    }

    public class NullableStructPropertyValueModel<TValue> : PropertyValueModel<Nullable<TValue>>
        where TValue : struct
    {
        public new delegate NullableStructPropertyValueModel<TValue> Factory(INotifyingObject obj, Property prop);

        public NullableStructPropertyValueModel(INotifyingObject obj, Property prop)
            : base(obj, prop)
        {
        }

        #region IValueModel<TValue> Members
        private Nullable<TValue> _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model.
        /// </summary>
        public override Nullable<TValue> Value
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
                    NotifyValueChanged();
                }
            }
        }
        #endregion

        #region Value Handling
        public override void ClearValue()
        {
            if (this.AllowNullInput) this.Value = null;
            else throw new InvalidOperationException();
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
    }

    public class ClassPropertyValueModel<TValue> : PropertyValueModel<TValue>
        where TValue : class
    {
        public new delegate ClassPropertyValueModel<TValue> Factory(INotifyingObject obj, Property prop);

        public ClassPropertyValueModel(INotifyingObject obj, Property prop)
            : base(obj, prop)
        {
        }

        #region IValueModel<TValue> Members
        private TValue _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override TValue Value
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

                    NotifyValueChanged();
                }
            }
        }
        #endregion

        #region Value Handling
        public override void ClearValue()
        {
            if (this.AllowNullInput) this.Value = null;
            else throw new InvalidOperationException();
        }
        #endregion
    }
}
