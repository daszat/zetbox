
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
    using System.Diagnostics;
    using Kistl.API.Utils;

    public static class MethodResultExtensionsThisShouldBeMovedToAZBoxMethod
    {
        public static IValueModel GetValueModel(this Method method, INotifyingObject obj)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (obj == null)
                throw new ArgumentNullException("obj");

            var retParam = method.GetReturnParameter();

            if (retParam is BoolParameter && !retParam.IsList)
            {
                return new NullableStructMethodResultModel<bool>(obj, method);
            }
            else if (retParam is DateTimeParameter && !retParam.IsList)
            {
                return new DateTimeMethodResultModel(obj, method);
            }
            else if (retParam is DoubleParameter && !retParam.IsList)
            {
                return new NullableStructMethodResultModel<double>(obj, method);
            }
            else if (retParam is IntParameter && !retParam.IsList)
            {
                return new NullableStructMethodResultModel<int>(obj, method);
            }
            else if (retParam is DecimalParameter && !retParam.IsList)
            {
                return new NullableStructMethodResultModel<decimal>(obj, method);
            }
            else if (retParam is StringParameter && !retParam.IsList)
            {
                return new ClassMethodResultModel<string>(obj, method);
            }
            else if (retParam is ObjectParameter && !retParam.IsList)
            {
                return new ObjectReferenceMethodResultModel(obj, method);
            }
            //else if (retParam is EnumParameter && !retParam.IsList)
            //{
            //    return (ModelFactory.CreateViewModel<NullableResultModel<?>.Factory>().Invoke(DataContext, _object, pm));
            //}
            else
            {
                Logging.Log.WarnFormat("No model for method: '{0}' of return type '{1}'", method, method.GetReturnParameter().GetParameterTypeString());
                return null;
            }
        }
    }

    public abstract class BaseMethodResultModel : IValueModel
    {
        public BaseMethodResultModel(INotifyingObject obj, Method m)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (m == null) throw new ArgumentNullException("m");

            this.Method = m;
            this.Object = obj;
            this.Object.PropertyChanged += Object_PropertyChanged;
        }

        void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CallMethod();
            NotifyValueChanged();
        }

        public Method Method { get; private set; }
        public INotifyingObject Object { get; private set; }
        private BaseParameter _ReturnParameter = null;
        public BaseParameter ReturnParameter
        {
            get
            {
                if (_ReturnParameter == null)
                {
                    _ReturnParameter = Method.GetReturnParameter();
                }
                return _ReturnParameter;
            }
        }

        protected abstract void CallMethod();

        #region IValueModel Members

        private bool? _AllowNullInput = null;
        public bool AllowNullInput
        {
            get
            {
                if (_AllowNullInput == null)
                {
                    _AllowNullInput = false; // TODO: No support for nullable return parameter yet; Method.IsNullable();
                }
                return _AllowNullInput.Value;
            }
        }

        public string Label
        {
            get { return !string.IsNullOrEmpty(Method.Label) ? Method.Label : Method.Name; }
        }

        public string Description
        {
            get { return Method.Description; }
        }

        public bool IsReadOnly { get { return true; } }

        public void ClearValue()
        {
            throw new NotSupportedException("Method results cannot be cleared");
        }

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
            // TODO: To be implemented
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
                // TODO: To be implemented
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

    public abstract class MethodResultModel<TValue> : BaseMethodResultModel, IValueModel<TValue>
    {
        public MethodResultModel(INotifyingObject obj, Method m)
            : base(obj, m)
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

    public class NullableStructMethodResultModel<TValue> : MethodResultModel<Nullable<TValue>>
        where TValue : struct
    {
        public NullableStructMethodResultModel(INotifyingObject obj, Method m)
            : base(obj, m)
        {
        }

        protected override void CallMethod()
        {
            _valueCache = Object.CallMethod<TValue>(Method.Name);
            _valueCacheInitialized = true;
            NotifyValueChanged();
        }

        private bool _valueCacheInitialized = false;
        private Nullable<TValue> _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override Nullable<TValue> Value
        {
            get
            {
                if (!_valueCacheInitialized) CallMethod();
                return _valueCache;
            }
            set { throw new NotSupportedException("Value of Methodresults cannot be set"); }
        }
    }

    public class ClassMethodResultModel<TValue> : MethodResultModel<TValue>
        where TValue : class
    {
        public ClassMethodResultModel(INotifyingObject obj, Method m)
            : base(obj, m)
        {
        }

        protected override void CallMethod()
        {
            _valueCache = Object.CallMethod<TValue>(Method.Name);
            _valueCacheInitialized = true;
            NotifyValueChanged();
        }

        private bool _valueCacheInitialized = false;
        private TValue _valueCache;

        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get
            {
                if (!_valueCacheInitialized) CallMethod();
                return _valueCache;
            }
            set { throw new NotSupportedException("Value of Methodresults cannot be set"); }
        }
    }

    public class DateTimeMethodResultModel : NullableStructMethodResultModel<DateTime>, IDateTimeValueModel
    {
        public DateTimeMethodResultModel(INotifyingObject obj, Method m)
            : base(obj, m)
        {
        }

        public DateTimeStyles DateTimeStyle
        {
            get { return DateTimeStyles.DateTime; }
        }
    }

    public class ObjectReferenceMethodResultModel : ClassMethodResultModel<IDataObject>, IObjectReferenceValueModel
    {
        public ObjectReferenceMethodResultModel(INotifyingObject obj, Method m)
            : base(obj, m)
        {
        }

        #region IObjectReferenceValueModel Members

        private ObjectClass _referencedClass = null;
        public ObjectClass ReferencedClass
        {
            get
            {
                if (_referencedClass == null)
                {
                    _referencedClass = (ObjectClass)((ObjectParameter)ReturnParameter).DataType;
                }
                return _referencedClass;
            }
        }

        #endregion
    }
}
