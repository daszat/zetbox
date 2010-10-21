
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
    using Kistl.API.Utils;

    public static class BaseParameterExtensionsThisShouldBeMovedToAZBoxMethod
    {
        public static IValueModel GetValueModel(this BaseParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            var lb = !string.IsNullOrEmpty(parameter.Label) ? parameter.Label : parameter.Name;

            if (parameter is BoolParameter && !parameter.IsList)
            {
                return new NullableStructValueModel<bool>(lb, parameter.Description, false, false);
            }
            else if (parameter is DateTimeParameter && !parameter.IsList)
            {
                return new DateTimeValueModel(lb, parameter.Description, false, false);
            }
            else if (parameter is DoubleParameter && !parameter.IsList)
            {
                return new NullableStructValueModel<double>(lb, parameter.Description, false, false);
            }
            else if (parameter is IntParameter && !parameter.IsList)
            {
                return new NullableStructValueModel<int>(lb, parameter.Description, false, false);
            }
            else if (parameter is DecimalParameter && !parameter.IsList)
            {
                return new NullableStructValueModel<decimal>(lb, parameter.Description, false, false);
            }
            else if (parameter is StringParameter && !parameter.IsList)
            {
                return new ClassValueModel<string>(lb, parameter.Description, false, false);
            }
            else if (parameter is ObjectParameter && !parameter.IsList)
            {
                return new ObjectReferenceValueModel(lb, parameter.Description, false, false, (ObjectClass)((ObjectParameter)parameter).DataType);
            }
            else if (parameter is EnumParameter && !parameter.IsList)
            {
                return new EnumerationValueModel(lb, parameter.Description, false, false, ((EnumParameter)parameter).Enumeration);
            }
            else
            {
                Logging.Log.WarnFormat("No model for parameter '{0}' of type '{1}'", parameter, parameter.GetParameterTypeString());
                return null;
            }
        }
    }

    public abstract class BaseValueModel : IValueModel
    {
        public BaseValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
        {
            this.Label = label;
            this.Description = description;
            this.AllowNullInput = allowNullInput;
            this.IsReadOnly = isReadOnly;
        }

        #region IValueModel Members

        public bool AllowNullInput { get; private set; }

        public string Label { get; private set; }

        public string Description { get; private set; }

        public bool IsReadOnly { get; private set; }

        public abstract void ClearValue();

        public abstract object GetUntypedValue();
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

    }

    public abstract class ValueModel<TValue> : BaseValueModel, IValueModel<TValue>
    {
        public ValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
        }

        #region IValueModel<TValue> Members

        public abstract TValue Value { get; set; }

        #endregion

        #region IValueModel Members
        public override object GetUntypedValue()
        {
            return this.Value;
        }
        #endregion

    }

    public class NullableStructValueModel<TValue> : ValueModel<Nullable<TValue>>
        where TValue : struct
    {
        public NullableStructValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
        }

        private Nullable<TValue> _value;
        public override Nullable<TValue> Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!object.Equals(_value, value))
                {
                    _value = value;
                    NotifyValueChanged();
                }
            }
        }

        public override void ClearValue()
        {
            if (AllowNullInput) Value = null;
            else throw new NotSupportedException();
        }
    }

    public class DateTimeValueModel : NullableStructValueModel<DateTime>, IDateTimeValueModel
    {
        public DateTimeValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
        }

        public DateTimeStyles DateTimeStyle
        {
            get { return DateTimeStyles.DateTime; }
        }
    }

    public class ClassValueModel<TValue> : ValueModel<TValue>
        where TValue : class
    {
        public ClassValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
        }

        private TValue _value;
        public override TValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    NotifyValueChanged();
                }
            }
        }

        public override void ClearValue()
        {
            if (AllowNullInput) Value = null;
            else throw new NotSupportedException();
        }
    }

    public class ObjectReferenceValueModel : ClassValueModel<IDataObject>, IObjectReferenceValueModel
    {
        public ObjectReferenceValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ObjectClass referencedClass)
            : base(label, description, allowNullInput, isReadOnly)
        {
            this.ReferencedClass = referencedClass;
        }

        #region IObjectReferenceValueModel Members

        public ObjectClass ReferencedClass
        {
            get; private set;
        }

        #endregion
    }

    public class EnumerationValueModel : NullableStructValueModel<int>, IEnumerationValueModel
    {
        protected readonly Enumeration enumDef;

        public EnumerationValueModel(string label, string description, bool allowNullInput, bool isReadOnly, Enumeration enumeration)
            : base(label, description, allowNullInput, isReadOnly)
        {
            this.enumDef = enumeration;
        }

        public override object GetUntypedValue()
        {
            var val = (int?)base.GetUntypedValue();
            return Enum.GetValues(enumDef.GetDataType()).AsQueryable().OfType<object>().FirstOrDefault(i => (int)i == val);
        }

        #region IEnumerationValueModel Members

        public Enumeration Enumeration
        {
            get { return enumDef; }
        }

        #endregion
    }
}
