
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
    using Kistl.Client.Presentables;

    public static class PropertyExtensionsThisShouldBeMovedToAZBoxMethod
    {
        public static IValueModel GetPropertyValueModel(this Property prop, INotifyingObject obj)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (prop is IntProperty)
            {
                return new NullableStructPropertyValueModel<int>(obj, prop);
            }
            else if (prop is BoolProperty)
            {
                return new BoolPropertyValueModel(obj, (BoolProperty)prop);
            }
            else if (prop is DoubleProperty)
            {
                return new NullableStructPropertyValueModel<double>(obj, prop);
            }
            else if (prop is DecimalProperty)
            {
                return new NullableStructPropertyValueModel<decimal>(obj, prop);
            }
            else if (prop is GuidProperty)
            {
                return new NullableStructPropertyValueModel<Guid>(obj, prop);
            }
            else if (prop is DateTimeProperty)
            {
                return new DateTimePropertyValueModel(obj, (DateTimeProperty)prop);
            }
            else if (prop is EnumerationProperty)
            {
                return new EnumerationPropertyValueModel(obj, (EnumerationProperty)prop);
            }
            else if (prop is StringProperty)
            {
                return new ClassPropertyValueModel<string>(obj, prop);
            }
            else if (prop is ObjectReferenceProperty)
            {
                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)prop;
                if (objRefProp.GetIsList())
                {
                    var sorted = objRefProp.RelationEnd.Parent.GetOtherEnd(objRefProp.RelationEnd).HasPersistentOrder;
                    if (sorted)
                    {
                        return new ObjectListPropertyValueModel(obj, objRefProp);
                    }
                    else
                    {
                        return new ObjectCollectionPropertyValueModel(obj, objRefProp);
                    }
                }
                else
                {
                    return new ObjectReferencePropertyValueModel(obj, objRefProp);
                }
            }
            else if (prop is CompoundObjectProperty)
            {
                var cop = (CompoundObjectProperty)prop;
                if (cop.IsList)
                {
                    return new CompoundCollectionPropertyValueModel(obj, cop);
                }
                else
                {
                    return new CompoundObjectPropertyValueModel(obj, cop);
                }
            }
            else
            {
                throw new NotImplementedException(string.Format("GetValueModel is not implemented for {0} properties yet", prop.GetPropertyTypeString()));
            }
        }

        public static IValueModel GetDetachedValueModel(this Property prop, bool allowNullInput)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");

            var lb = prop.GetLabel();

            if (prop is IntProperty)
            {
                return new NullableStructValueModel<int>(lb, prop.Description, allowNullInput, false, prop.RequestedKind);
            }
            else if (prop is BoolProperty)
            {
                return new BoolValueModel(lb, prop.Description, allowNullInput, false, prop.RequestedKind);
            }
            else if (prop is DoubleProperty)
            {
                return new NullableStructValueModel<double>(lb, prop.Description, allowNullInput, false, prop.RequestedKind);
            }
            else if (prop is DecimalProperty)
            {
                return new NullableStructValueModel<decimal>(lb, prop.Description, allowNullInput, false, prop.RequestedKind);
            }
            else if (prop is GuidProperty)
            {
                return new NullableStructValueModel<Guid>(lb, prop.Description, allowNullInput, false, prop.RequestedKind);
            }
            else if (prop is DateTimeProperty)
            {
                var dtp = (DateTimeProperty)prop;
                return new DateTimeValueModel(lb, prop.Description, allowNullInput, false, dtp.DateTimeStyle ?? DateTimeStyles.DateTime, prop.RequestedKind);
            }
            else if (prop is EnumerationProperty)
            {
                return new EnumerationValueModel(lb, prop.Description, allowNullInput, false, prop.RequestedKind, ((EnumerationProperty)prop).Enumeration);
            }
            else if (prop is StringProperty)
            {
                return new ClassValueModel<string>(lb, prop.Description, allowNullInput, false, prop.RequestedKind);
            }
            else if (prop is ObjectReferenceProperty)
            {
                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)prop;
                if (objRefProp.GetIsList())
                {
                    //var sorted = objRefProp.RelationEnd.Parent.GetOtherEnd(objRefProp.RelationEnd).HasPersistentOrder;
                    //if (sorted)
                    //{
                    //    return new ObjectListValueModel(obj, objRefProp);
                    //}
                    //else
                    //{
                    //    return new ObjectCollectionValueModel(obj, objRefProp);
                    //}
                }
                else
                {
                    return new ObjectReferenceValueModel(lb, prop.Description, allowNullInput, false, prop.RequestedKind, objRefProp.GetReferencedObjectClass());
                }
            }
            else if (prop is CompoundObjectProperty)
            {
                return new CompoundObjectValueModel(lb, prop.Description, allowNullInput, false, prop.RequestedKind, ((CompoundObjectProperty)prop).CompoundObjectDefinition);
            }

            throw new NotImplementedException(string.Format("GetValueModel is not implemented for {0} properties yet", prop.GetPropertyTypeString()));
        }
    }

    /// <summary>
    /// For autofac
    /// </summary>
    public abstract class BasePropertyValueModel
        : IValueModel
    {
        public BasePropertyValueModel(INotifyingObject obj, Property prop)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (prop == null)
                throw new ArgumentNullException("prop");

            this.Property = prop;
            this.Object = obj;

            this.Object.PropertyChanged += Object_PropertyChanged;
        }

        void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Property.Name)
            {
                UpdateValueCache();
                NotifyValueChanged();
            }
        }

        protected abstract void UpdateValueCache();

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
            get { return Property.GetLabel(); }
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

        public abstract object GetUntypedValue();

        public ControlKind RequestedKind { get { return Property.RequestedKind; } }
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

    public abstract class PropertyValueModel<TValue>
        : BasePropertyValueModel, IValueModel<TValue>
    {
        public PropertyValueModel(INotifyingObject obj, Property prop)
            : base(obj, prop)
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

    public class NullableStructPropertyValueModel<TValue> : PropertyValueModel<Nullable<TValue>>
        where TValue : struct
    {
        public NullableStructPropertyValueModel(INotifyingObject obj, Property prop)
            : base(obj, prop)
        {
        }

        #region IValueModel<TValue> Members
        private bool _valueCacheInitialized = false;
        private Nullable<TValue> _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model.
        /// </summary>
        public override Nullable<TValue> Value
        {
            get
            {
                if (!_valueCacheInitialized)
                {
                    UpdateValueCache();
                }
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

        protected override void UpdateValueCache()
        {
            _valueCache = GetPropertyValue();
            _valueCacheInitialized = true;
        }
        #endregion

        #region Value Handling
        public override void ClearValue()
        {
            if (this.AllowNullInput)
                this.Value = null;
            else
                throw new InvalidOperationException("Property does not allow null values");
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

    public class ClassPropertyValueModel<TValue>
        : PropertyValueModel<TValue>
        where TValue : class
    {
        public ClassPropertyValueModel(INotifyingObject obj, Property prop)
            : base(obj, prop)
        {
        }

        #region IValueModel<TValue> Members
        private bool _valueCacheInitialized = false;
        private TValue _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get
            {
                if (!_valueCacheInitialized)
                {
                    UpdateValueCache();
                }
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

        protected override void UpdateValueCache()
        {
            _valueCache = Object.GetPropertyValue<TValue>(Property.Name);
            _valueCacheInitialized = true;
        }
        #endregion

        #region Value Handling
        public override void ClearValue()
        {
            if (this.AllowNullInput)
                this.Value = null;
            else
                throw new InvalidOperationException("Property does not allow null values");
        }
        #endregion
    }

    public class ObjectReferencePropertyValueModel
        : ClassPropertyValueModel<IDataObject>, IObjectReferenceValueModel
    {
        protected readonly ObjectReferenceProperty objRefProp;

        public ObjectReferencePropertyValueModel(INotifyingObject obj, ObjectReferenceProperty prop)
            : base(obj, prop)
        {
            this.objRefProp = prop;
        }

        #region IValueModel<TValue> Members
        private bool _valueCacheInititalized = false;
        private IDataObject _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override IDataObject Value
        {
            get
            {
                if (!_valueCacheInititalized)
                {
                    UpdateValueCache();
                }
                return _valueCache;
            }
            set
            {
                _valueCache = value;
                _valueCacheInititalized = true;
                Object.SetPropertyValue<IDataObject>(Property.Name, value);
                CheckConstraints();
                NotifyValueChanged();
            }
        }

        protected override void UpdateValueCache()
        {
            _valueCache = Object.GetPropertyValue<IDataObject>(Property.Name);
            _valueCacheInititalized = true;
        }
        #endregion

        #region IObjectReferenceValueModel Members

        private ObjectClass _referencedClass = null;
        public ObjectClass ReferencedClass
        {
            get
            {
                if (_referencedClass == null)
                {
                    _referencedClass = objRefProp.GetReferencedObjectClass();
                }
                return _referencedClass;
            }
        }

        #endregion
    }

    public class CompoundObjectPropertyValueModel
    : ClassPropertyValueModel<ICompoundObject>, ICompoundObjectValueModel
    {
        protected readonly CompoundObjectProperty cProp;

        public CompoundObjectPropertyValueModel(INotifyingObject obj, CompoundObjectProperty prop)
            : base(obj, prop)
        {
            this.cProp = prop;
        }

        #region IValueModel<TValue> Members
        private bool _valueCacheInititalized = false;
        private ICompoundObject _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override ICompoundObject Value
        {
            get
            {
                if (!_valueCacheInititalized)
                {
                    UpdateValueCache();
                }
                return _valueCache;
            }
            set
            {
                _valueCache = value;
                _valueCacheInititalized = true;
                Object.SetPropertyValue<ICompoundObject>(Property.Name, value);
                CheckConstraints();
                NotifyValueChanged();
            }
        }

        protected override void UpdateValueCache()
        {
            _valueCache = Object.GetPropertyValue<ICompoundObject>(Property.Name);
            _valueCacheInititalized = true;
        }
        #endregion

        #region IObjectReferenceValueModel Members

        public CompoundObject CompoundObjectDefinition
        {
            get
            {
                return cProp.CompoundObjectDefinition;
            }
        }

        #endregion
    }

    public class CompoundCollectionPropertyValueModel
        : ClassPropertyValueModel<IList<ICompoundObject>>, ICompoundCollectionValueModel
    {
        private readonly CompoundObjectProperty _property;

        public CompoundCollectionPropertyValueModel(INotifyingObject obj, CompoundObjectProperty prop)
            : base(obj, prop)
        {
            _property = prop;
        }

        #region IValueModel<TValue> Members

        protected IList<ICompoundObject> valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override IList<ICompoundObject> Value
        {
            get
            {
                UpdateValueCache();
                return valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected IEnumerable underlyingCollectionCache = null;
        public IEnumerable UnderlyingCollection
        {
            get
            {
                UpdateValueCache();
                return underlyingCollectionCache;
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

        protected override void UpdateValueCache()
        {
            if (valueCache == null) // Once is OK
            {
                var lst = Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name);
                lst.CollectionChanged += ValueCollectionChanged;

                underlyingCollectionCache = (IEnumerable)lst;

                valueCache = MagicCollectionFactory.WrapAsList<ICompoundObject>(lst);
            }
        }

        public CompoundObject CompoundObjectDefinition
        {
            get { return _property.CompoundObjectDefinition; }
        }
    }

    public abstract class BaseObjectCollectionPropertyValueModel<TCollection>
        : ClassPropertyValueModel<TCollection>, IObjectCollectionValueModel<TCollection>
        where TCollection : class
    {
        protected readonly ObjectReferenceProperty objRefProp;

        public BaseObjectCollectionPropertyValueModel(INotifyingObject obj, ObjectReferenceProperty prop)
            : base(obj, prop)
        {
            this.objRefProp = prop;
        }

        #region IValueModel<TValue> Members

        protected bool valueCacheInititalized = false;
        protected TCollection valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override TCollection Value
        {
            get
            {
                if (!valueCacheInititalized)
                {
                    UpdateValueCache();
                }
                return valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected IEnumerable underlyingCollectionCache = null;
        public IEnumerable UnderlyingCollection
        {
            get
            {
                if (!valueCacheInititalized)
                {
                    UpdateValueCache();
                }
                return underlyingCollectionCache;
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
                if (_referencedClass == null)
                {
                    _referencedClass = objRefProp.GetReferencedObjectClass();
                }
                return _referencedClass;
            }
        }

        private RelationEnd _relEnd = null;
        public RelationEnd RelEnd
        {
            get
            {
                if (_relEnd == null)
                {
                    _relEnd = objRefProp.RelationEnd;
                }
                return _relEnd;
            }
        }

        public bool? IsInlineEditable
        {
            get { return this.objRefProp.IsInlineEditable; }
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

    public class ObjectCollectionPropertyValueModel
        : BaseObjectCollectionPropertyValueModel<ICollection<IDataObject>>
    {
        public ObjectCollectionPropertyValueModel(INotifyingObject obj, ObjectReferenceProperty prop)
            : base(obj, prop)
        {
        }

        protected override void UpdateValueCache()
        {
            if (valueCache == null) // Once is OK
            {
                var lst = Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name);
                lst.CollectionChanged += ValueCollectionChanged;

                underlyingCollectionCache = (IEnumerable)lst;

                valueCache = MagicCollectionFactory.WrapAsCollection<IDataObject>(lst);
            }
            valueCacheInititalized = true;
        }
    }

    public class ObjectListPropertyValueModel
        : BaseObjectCollectionPropertyValueModel<IList<IDataObject>>
    {
        public ObjectListPropertyValueModel(INotifyingObject obj, ObjectReferenceProperty prop)
            : base(obj, prop)
        {
        }

        protected override void UpdateValueCache()
        {
            if (valueCache == null) // Once is OK
            {
                var lst = Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name);
                lst.CollectionChanged += ValueCollectionChanged;

                underlyingCollectionCache = (IEnumerable)lst;

                valueCache = MagicCollectionFactory.WrapAsList<IDataObject>(lst);
            }
            valueCacheInititalized = true;
        }
    }

    public class EnumerationPropertyValueModel : NullableStructPropertyValueModel<int>, IEnumerationValueModel
    {
        protected readonly EnumerationProperty enumProp;

        public EnumerationPropertyValueModel(INotifyingObject obj, EnumerationProperty prop)
            : base(obj, prop)
        {
            enumProp = prop;
        }

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

        #region IEnumerationValueModel Members

        public Enumeration Enumeration
        {
            get { return enumProp.Enumeration; }
        }

        public IEnumerable<KeyValuePair<int, string>> GetEntries()
        {
            return enumProp.Enumeration.EnumerationEntries.Select(ee => new KeyValuePair<int, string>(ee.Value, ee.GetLabel()));
        }

        #endregion
    }

    public class DateTimePropertyValueModel : NullableStructPropertyValueModel<DateTime>, IValueModel<TimeSpan?>, IDateTimeValueModel
    {
        protected readonly DateTimeProperty dtProp;

        public DateTimePropertyValueModel(INotifyingObject obj, DateTimeProperty prop)
            : base(obj, prop)
        {
            dtProp = prop;
        }


        #region IDateTimeValueModel Members

        public DateTimeStyles DateTimeStyle
        {
            get { return dtProp.DateTimeStyle ?? DateTimeStyles.DateTime; }
        }

        #endregion

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

    public class BoolPropertyValueModel : NullableStructPropertyValueModel<bool>, IBoolValueModel
    {
        protected readonly BoolProperty bProp;

        public BoolPropertyValueModel(INotifyingObject obj, BoolProperty prop)
            : base(obj, prop)
        {
            bProp = prop;
        }

        public Icon FalseIcon
        {
            get { return bProp.FalseIcon; }
        }

        public string FalseLabel
        {
            get { return bProp.FalseLabel; }
        }

        public Icon NullIcon
        {
            get { return bProp.NullIcon; }
        }

        public string NullLabel
        {
            get { return bProp.NullLabel; }
        }

        public Icon TrueIcon
        {
            get { return bProp.TrueIcon; }
        }

        public string TrueLabel
        {
            get { return bProp.TrueLabel; }
        }
    }
}
