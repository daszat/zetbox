// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.API.Async;

    public static class PropertyExtensionsThisShouldBeMovedToAZetboxMethod
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
                return new DecimalPropertyValueModel(obj, (DecimalProperty)prop);
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
                var objRefProp = (ObjectReferenceProperty)prop;
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
            else if (prop is CalculatedObjectReferenceProperty)
            {
                var objRefProp = (CalculatedObjectReferenceProperty)prop;
                return new CalculatedObjectReferencePropertyValueModel(obj, objRefProp);
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

        public static IValueModel GetDetachedValueModel(this Property prop, IZetboxContext ctx, bool allowNullInput)
        {
            if (prop == null)
                throw new ArgumentNullException("prop");

            var lb = prop.GetLabel();
            var description = prop.Description;
            var rk = prop.RequestedKind;

            if (prop is IntProperty)
            {
                return new NullableStructValueModel<int>(lb, description, allowNullInput, false, rk);
            }
            else if (prop is BoolProperty)
            {
                return new BoolValueModel(lb, description, allowNullInput, false, rk);
            }
            else if (prop is DoubleProperty)
            {
                return new NullableStructValueModel<double>(lb, description, allowNullInput, false, rk);
            }
            else if (prop is DecimalProperty)
            {
                return new DecimalValueModel(lb, description, allowNullInput, false, rk);
            }
            else if (prop is GuidProperty)
            {
                return new NullableStructValueModel<Guid>(lb, description, allowNullInput, false, rk);
            }
            else if (prop is DateTimeProperty)
            {
                var dtp = (DateTimeProperty)prop;
                return new DateTimeValueModel(lb, description, allowNullInput, false, dtp.DateTimeStyle ?? DateTimeStyles.DateTime, rk);
            }
            else if (prop is EnumerationProperty)
            {
                return new EnumerationValueModel(lb, description, allowNullInput, false, rk, ((EnumerationProperty)prop).Enumeration);
            }
            else if (prop is StringProperty)
            {
                return new ClassValueModel<string>(lb, description, allowNullInput, false, rk);
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
                    return new ObjectReferenceValueModel(lb, description, allowNullInput, false, rk, objRefProp.GetReferencedObjectClass());
                }
            }
            else if (prop is CompoundObjectProperty)
            {
                return new CompoundObjectValueModel(ctx, lb, description, allowNullInput, false, rk, ((CompoundObjectProperty)prop).CompoundObjectDefinition);
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
            if (this.Object is IPersistenceObject)
            {
                DataContext = ((IPersistenceObject)this.Object).Context;
                DataContext.IsElevatedModeChanged += new EventHandler(Context_IsElevatedModeChanged);
            }
        }

        void Context_IsElevatedModeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsReadOnly");
        }

        void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Property.Name)
            {
                InvalidateValueCache();
                NotifyValueChanged();
            }
        }

        /// <summary>
        /// This method is called if a change in the underlying property is detected.
        /// It must invalidate all caches so that the next call to GetUntypedValue will return the new underlying value.
        /// </summary>
        protected abstract void InvalidateValueCache();

        public Property Property { get; private set; }
        public INotifyingObject Object { get; private set; }
        protected IZetboxContext DataContext { get; private set; }

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
                if (DataContext != null && DataContext.IsElevatedMode && !Property.IsCalculated()) return false;
                if (_IsReadOnly == null)
                {
                    _IsReadOnly = Property.IsReadOnly();
                }
                return _IsReadOnly.Value;
            }
        }

        public abstract void ClearValue();

        public abstract object GetUntypedValue();

        public abstract void SetUntypedValue(object val);

        public virtual bool ReportErrors
        {
            get
            {
                if (Object is IDataObject)
                {
                    var dataObj = (IDataObject)Object;
                    return dataObj.ObjectState != DataObjectState.Deleted;
                }
                return true;
            }
        }

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

        public abstract ZbTask<TValue> GetValueAsync();

        #endregion

        #region IValueModel Members
        public sealed override object GetUntypedValue()
        {
            return this.Value;
        }

        public sealed override void SetUntypedValue(object val)
        {
            this.Value = (TValue)val;
        }
        #endregion
    }

    public class NullableStructPropertyValueModel<TValue> : PropertyValueModel<TValue?>
        where TValue : struct
    {
        public NullableStructPropertyValueModel(INotifyingObject obj, Property prop)
            : base(obj, prop)
        {
        }

        #region IValueModel<TValue> Members
        private bool _valueCacheInitialized = false;
        private TValue? _valueCache;

        /// <summary>
        /// Gets or sets the value of the property presented by this model.
        /// </summary>
        public override TValue? Value
        {
            get
            {
                if (!_valueCacheInitialized)
                {
                    _valueCache = GetPropertyValue();
                    _valueCacheInitialized = true;
                }
                return _valueCache;
            }
            set
            {
                if (!_valueCache.HasValue && !value.HasValue)
                    return;

                if (!this.AllowNullInput && value == null)
                    throw new InvalidOperationException("\"null\" input not allowed");

                _valueCacheInitialized = true;
                _valueCache = value;

                if (!IsPropertyInitialized() || !object.Equals(GetPropertyValue(), value))
                {
                    SetPropertyValue(value);
                    CheckConstraints();
                    NotifyValueChanged();
                }
            }
        }

        public sealed override ZbTask<TValue?> GetValueAsync()
        {
            return new ZbTask<TValue?>(Value);
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

        protected virtual bool IsPropertyInitialized()
        {
            if (Object is IDataObject)
            {
                var obj = (IDataObject)Object;
                return obj.IsInitialized(Property.Name);
            }
            return true;
        }

        /// <summary>
        /// Loads the Value from the object.
        /// </summary>
        protected virtual TValue? GetPropertyValue()
        {
            if (IsPropertyInitialized())
            {
                return Object.GetPropertyValue<TValue?>(Property.Name);
            }
            else
            {
                return null;
            }
        }

        protected virtual void SetPropertyValue(TValue? value)
        {
            Object.SetPropertyValue<Nullable<TValue>>(Property.Name, value);
        }

        protected override void InvalidateValueCache()
        {
            _valueCacheInitialized = false;
            _valueCache = null;
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

        protected ZbTask<TValue> getValueTask = null;

        /// <summary>
        /// Gets or sets the value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get
            {
                if (getValueTask == null)
                {
                    getValueTask = GetValueAsync();
                }
                return getValueTask.Result;
            }
            set
            {
                if (!object.Equals(Object.GetPropertyValue<TValue>(Property.Name), value))
                {
                    InvalidateValueCache();

                    Object.SetPropertyValue<TValue>(Property.Name, value);

                    CheckConstraints();

                    NotifyValueChanged();
                }
            }
        }

        public override ZbTask<TValue> GetValueAsync()
        {
            return new ZbTask<TValue>(Object.GetPropertyValue<TValue>(Property.Name));
        }

        protected override void InvalidateValueCache()
        {
            getValueTask = null;
        }

        /// <summary>
        /// Get the ZbTask from TriggerFetch*Async for the presented Property.
        /// </summary>
        /// <returns>The ZbTask from TriggerFetch</returns>
        protected ZbTask GetTriggerFetchTask()
        {
            if (Object is IDataObject)
            {
                return ((IDataObject)Object).TriggerFetch(Property.Name);
            }
            else
            {
                throw new InvalidOperationException("Object is not an IDataObject - unable to call TriggerFetch");
            }
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

        public override ZbTask<IDataObject> GetValueAsync()
        {
            return new ZbTask<IDataObject>(GetTriggerFetchTask())
                .OnResult(t => t.Result = Object.GetPropertyValue<IDataObject>(Property.Name));
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

        #endregion
    }

    public class CalculatedObjectReferencePropertyValueModel
        : ClassPropertyValueModel<IDataObject>, IObjectReferenceValueModel
    {
        protected readonly CalculatedObjectReferenceProperty objRefProp;

        public CalculatedObjectReferencePropertyValueModel(INotifyingObject obj, CalculatedObjectReferenceProperty prop)
            : base(obj, prop)
        {
            this.objRefProp = prop;
        }

        #region IObjectReferenceValueModel Members

        public ObjectClass ReferencedClass { get { return this.objRefProp.ReferencedClass; } }

        public RelationEnd RelEnd { get { return null; } }

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

        #region ICompoundObjectValueModel Members

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
                return base.Value;
            }
            set
            {
                throw new NotSupportedException();
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

        private ZbTask<IList<ICompoundObject>> _getValueTask;
        public override ZbTask<IList<ICompoundObject>> GetValueAsync()
        {
            if (_getValueTask == null)
            {
                _getValueTask = new ZbTask<IList<ICompoundObject>>(new ZbTask<INotifyCollectionChanged>(Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name)))
                    .OnResult(t =>
                    {
                        var notifier = Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name);
                        notifier.CollectionChanged += ValueCollectionChanged;
                        t.Result = MagicCollectionFactory.WrapAsList<ICompoundObject>(notifier);
                    });
            }
            return _getValueTask;
        }

        protected override void InvalidateValueCache()
        {
            // Do not delete trigger fetch task as the underlying collection is const
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

        protected void ValueCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler temp = _CollectionChanged;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

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

        private ZbTask<ICollection<IDataObject>> _getValueTask;
        public override ZbTask<ICollection<IDataObject>> GetValueAsync()
        {
            if (_getValueTask == null)
            {
                _getValueTask = new ZbTask<ICollection<IDataObject>>(GetTriggerFetchTask())
                    .OnResult(t =>
                    {
                        var notifier = Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name);
                        notifier.CollectionChanged += ValueCollectionChanged;
                        t.Result = MagicCollectionFactory.WrapAsCollection<IDataObject>(notifier);
                    });
            }
            return _getValueTask;
        }

        protected override void InvalidateValueCache()
        {
            // Do not delete trigger fetch task as the underlying collection is const
        }
    }

    public class ObjectListPropertyValueModel
        : BaseObjectCollectionPropertyValueModel<IList<IDataObject>>
    {
        public ObjectListPropertyValueModel(INotifyingObject obj, ObjectReferenceProperty prop)
            : base(obj, prop)
        {
        }

        private ZbTask<IList<IDataObject>> _getValueTask;
        public override ZbTask<IList<IDataObject>> GetValueAsync()
        {
            if (_getValueTask == null)
            {
                _getValueTask = new ZbTask<IList<IDataObject>>(GetTriggerFetchTask())
                    .OnResult(t =>
                    {
                        var notifier = Object.GetPropertyValue<INotifyCollectionChanged>(Property.Name);
                        notifier.CollectionChanged += ValueCollectionChanged;
                        t.Result = MagicCollectionFactory.WrapAsList<IDataObject>(notifier);
                    });
            }
            return _getValueTask;
        }

        protected override void InvalidateValueCache()
        {
            // Do not delete trigger fetch task as the underlying collection is const
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

        ZbTask<TimeSpan?> IValueModel<TimeSpan?>.GetValueAsync()
        {
            return new ZbTask<TimeSpan?>(ZbTask.Synchron, () => ((IValueModel<TimeSpan?>)this).Value);
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

    public class DecimalPropertyValueModel : NullableStructPropertyValueModel<decimal>, IDecimalValueModel
    {
        protected readonly DecimalProperty dProp;

        public DecimalPropertyValueModel(INotifyingObject obj, DecimalProperty prop)
            : base(obj, prop)
        {
            dProp = prop;
        }

        public int? Precision
        {
            get { return dProp.Precision > 0 ? dProp.Precision : (int?)null; }
        }

        public int? Scale
        {
            get { return dProp.Scale > 0 ? dProp.Scale : (int?)null; }
        }
    }
}
