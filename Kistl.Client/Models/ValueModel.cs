
namespace Kistl.Client.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;

    public abstract class ValueModel<TValue> : IValueModel, IValueModel<TValue>
    {
        protected Func<TValue> getValue;
        protected Action<TValue> setValue;

        public ValueModel(string label, string description, bool allowNullInput, bool isReadOnly, Func<TValue> getValue, Action<TValue> setValue)
        {
            this.Label = label;
            this.Description = description;
            this.AllowNullInput = allowNullInput;
            this.IsReadOnly = isReadOnly;
            this.getValue = getValue;
            this.setValue = setValue;
        }

        #region IValueModel Members

        public bool AllowNullInput { get; private set;}

        public string Label { get; private set; }

        public string Description { get; private set; }

        public bool IsReadOnly { get; private set; }

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
                    return null;
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

        #region IValueModel<TValue> Members

        public TValue Value 
        {
            get
            {
                return getValue();
            }
            set
            {
                setValue(value);
                NotifyValueChanged();
            }
        }

        #endregion
    }

    //public class ValueModel<TValue> : BaseValueModel, IValueModel<TValue>
    //{
    //    public delegate ValueModel<TValue> Factory();

    //    public ValueModel() 
    //    {
    //    }
    //}

    public class NullableStructValueModel<TValue> : ValueModel<Nullable<TValue>>
        where TValue : struct
    {
        public NullableStructValueModel(string label, string description, bool allowNullInput, bool isReadOnly, Func<Nullable<TValue>> getValue, Action<Nullable<TValue>> setValue)
            : base(label, description, allowNullInput, isReadOnly, getValue, setValue)
        {
        }

        public override void ClearValue()
        {
            if (AllowNullInput) setValue(null);
            else throw new NotSupportedException();
        }
    }

    public class ClassValueModel<TValue> : ValueModel<TValue>
        where TValue : class
    {
        public ClassValueModel(string label, string description, bool allowNullInput, bool isReadOnly, Func<TValue> getValue, Action<TValue> setValue)
            : base(label, description, allowNullInput, isReadOnly, getValue, setValue)
        {
        }

        public override void ClearValue()
        {
            if (AllowNullInput) setValue(null);
            else throw new NotSupportedException();
        }
    }
}
