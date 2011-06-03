
namespace Kistl.Client.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;

    public static class BaseParameterExtensionsThisShouldBeMovedToAZBoxMethod
    {
        public static IValueModel GetValueModel(this BaseParameter parameter, bool allowNullInput)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            var lb = parameter.GetLabel();

            if (parameter is BoolParameter && !parameter.IsList)
            {
                return new BoolValueModel(lb, parameter.Description, allowNullInput, false);
            }
            else if (parameter is DateTimeParameter && !parameter.IsList)
            {
                return new DateTimeValueModel(lb, parameter.Description, allowNullInput, false);
            }
            else if (parameter is DoubleParameter && !parameter.IsList)
            {
                return new NullableStructValueModel<double>(lb, parameter.Description, allowNullInput, false);
            }
            else if (parameter is IntParameter && !parameter.IsList)
            {
                return new NullableStructValueModel<int>(lb, parameter.Description, allowNullInput, false);
            }
            else if (parameter is DecimalParameter && !parameter.IsList)
            {
                return new NullableStructValueModel<decimal>(lb, parameter.Description, allowNullInput, false);
            }
            else if (parameter is StringParameter && !parameter.IsList)
            {
                return new ClassValueModel<string>(lb, parameter.Description, allowNullInput, false);
            }
            else if (parameter is ObjectParameter && !parameter.IsList)
            {
                return new ObjectReferenceValueModel(lb, parameter.Description, allowNullInput, false, (ObjectClass)((ObjectParameter)parameter).DataType);
            }
            else if (parameter is EnumParameter && !parameter.IsList)
            {
                return new EnumerationValueModel(lb, parameter.Description, allowNullInput, false, ((EnumParameter)parameter).Enumeration);
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
            : this(label, description, allowNullInput, isReadOnly, null)
        {
        }

        public BaseValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
        {
            this.Label = label;
            this.Description = description;
            this.AllowNullInput = allowNullInput;
            this.IsReadOnly = isReadOnly;
            this.RequestedKind = requestedKind;
        }

        #region IValueModel Members

        public bool AllowNullInput { get; private set; }

        public string Label { get; private set; }

        public string Description { get; private set; }

        public bool IsReadOnly { get; private set; }

        public abstract void ClearValue();

        public abstract object GetUntypedValue();

        public ControlKind RequestedKind { get; private set; }
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

        public ValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
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

        public NullableStructValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
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

    public class DateTimeValueModel : NullableStructValueModel<DateTime>, IValueModel<TimeSpan?>, IDateTimeValueModel
    {
        public DateTimeValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
            _dateTimeStyle = DateTimeStyles.DateTime;
        }

        public DateTimeValueModel(string label, string description, bool allowNullInput, bool isReadOnly, DateTimeStyles dateTimeStyle)
            : base(label, description, allowNullInput, isReadOnly)
        {
            _dateTimeStyle = dateTimeStyle;
        }

        public DateTimeValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            _dateTimeStyle = DateTimeStyles.DateTime;
        }

        private readonly DateTimeStyles _dateTimeStyle;

        public DateTimeStyles DateTimeStyle
        {
            get { return _dateTimeStyle; }
        }

        TimeSpan? IValueModel<TimeSpan?>.Value
        {
            get
            {
                return this.Value != null ? this.Value.Value.TimeOfDay : (TimeSpan?)null;
            }
            set
            {
                if (value == null && this.Value == null)
                {
                    // Do nothing
                }
                else if (value == null && this.Value != null)
                {
                    // Preserve date
                    this.Value = this.Value.Value.Date;
                }
                else if (value != null && Value != null)
                {
                    // Preserve date
                    this.Value = this.Value.Value.Date.Add(value.Value);
                }
                else //if (value != null && Value == null)
                {
                    this.Value = DateTime.MinValue.Add(value.Value);
                }
            }
        }
    }

    public class BoolValueModel : NullableStructValueModel<bool>, IBoolValueModel
    {
        public BoolValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
        }

        public BoolValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
        }

        public Icon FalseIcon { get; set; }
        public string FalseLabel { get; set; }

        public Icon NullIcon { get; set; }
        public string NullLabel { get; set; }

        public Icon TrueIcon { get; set; }
        public string TrueLabel { get; set; }
    }

    public class ClassValueModel<TValue> : ValueModel<TValue>
        where TValue : class
    {
        public ClassValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
        }

        public ClassValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
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
            : this(label, description, allowNullInput, isReadOnly, null, referencedClass)
        {
        }

        public ObjectReferenceValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind, ObjectClass referencedClass)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            this.ReferencedClass = referencedClass;
        }

        #region IObjectReferenceValueModel Members

        public ObjectClass ReferencedClass
        {
            get;
            private set;
        }

        #endregion
    }

    /// <summary>
    /// ValueModel for collections and lists
    /// </summary>
    /// <typeparam name="TCollection">a ICollection or IList</typeparam>
    public class ObjectCollectionValueModel<TCollection>
        : ClassValueModel<TCollection>, IObjectCollectionValueModel<TCollection>
        where TCollection : class, IEnumerable
    {
        public ObjectCollectionValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ObjectClass referencedClass, TCollection collection)
            : this(label, description, allowNullInput, isReadOnly, null, referencedClass, collection)
        {
        }

        public ObjectCollectionValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind, ObjectClass referencedClass, TCollection collection)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            if (referencedClass == null) throw new ArgumentNullException("referencedClass");
            if (collection == null) throw new ArgumentNullException("collection");

            valueCache = collection;
            _referencedClass = referencedClass;
            if (collection is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)collection).CollectionChanged += ValueCollectionChanged;
            }
        }

        #region IValueModel<TValue> Members

        protected TCollection valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override TCollection Value
        {
            get
            {
                return valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public IEnumerable UnderlyingCollection
        {
            get
            {
                return valueCache;
            }
        }

        protected void ValueCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler temp = _CollectionChanged;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        #endregion

        #region IObjectCollectionValueModel<TCollection> Members

        private ObjectClass _referencedClass = null;
        public ObjectClass ReferencedClass
        {
            get
            {
                return _referencedClass;
            }
        }

        /// <summary>
        /// No RelEnd, returns always null
        /// </summary>
        public RelationEnd RelEnd
        {
            get
            {
                return null;
            }
        }

        public bool? IsInlineEditable
        {
            get { return false; }
        }
        #endregion

        #region INotifyCollectionChanged Members

        private event NotifyCollectionChangedEventHandler _CollectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                _CollectionChanged += value;
            }
            remove
            {
                _CollectionChanged -= value;
            }
        }

        #endregion
    }

    public class CompoundObjectValueModel : ClassValueModel<ICompoundObject>, ICompoundObjectValueModel
    {
        public CompoundObjectValueModel(string label, string description, bool allowNullInput, bool isReadOnly, CompoundObject def)
            : this(label, description, allowNullInput, isReadOnly, null, def)
        {
        }

        public CompoundObjectValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind, CompoundObject def)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            this.CompoundObjectDefinition = def;
        }


        #region IObjectReferenceValueModel Members

        public CompoundObject CompoundObjectDefinition
        {
            get;
            private set;
        }

        #endregion
    }

    public class ListValueModel<TValue, TCollection>
        : ClassValueModel<TCollection>, IListValueModel<TValue>
        where TCollection : class, IList<TValue>
    {
        protected TCollection valueCache;

        public ListValueModel(string label, string description, bool allowNullInput, bool isReadOnly, TCollection collection)
            : this(label, description, allowNullInput, isReadOnly, null, collection)
        {
        }

        public ListValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind, TCollection collection)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            valueCache = collection;
            if (collection is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)collection).CollectionChanged += ValueCollectionChanged;
            }
        }

        #region IValueModel<TValue> Members

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override TCollection Value
        {
            get
            {
                return valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        IList<TValue> IValueModel<IList<TValue>>.Value
        {
            get
            {
                return valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public IEnumerable UnderlyingCollection
        {
            get
            {
                return valueCache;
            }
        }

        protected void ValueCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler temp = _CollectionChanged;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        #endregion

        #region INotifyCollectionChanged Members

        private event NotifyCollectionChangedEventHandler _CollectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                _CollectionChanged += value;
            }
            remove
            {
                _CollectionChanged -= value;
            }
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

        public EnumerationValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind, Enumeration enumeration)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
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

        public IEnumerable<KeyValuePair<int, string>> GetEntries()
        {
            return enumDef.EnumerationEntries.Select(ee => new KeyValuePair<int, string>(ee.Value, ee.GetLabel()));
        }

        #endregion
    }
}
