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
    using Zetbox.API.Async;

    public static class BaseParameterExtensionsThisShouldBeMovedToAZetboxMethod
    {
        public static IValueModel GetValueModel(this BaseParameter parameter, IZetboxContext ctx, bool allowNullInput)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            var lb = parameter.GetLabel();
            var description = parameter.Description;
            var isList = parameter.IsList;

            if (parameter is BoolParameter && !isList)
            {
                return new BoolValueModel(lb, description, allowNullInput, false);
            }
            else if (parameter is DateTimeParameter && !isList)
            {
                var dtp = (DateTimeParameter)parameter;
                return new DateTimeValueModel(lb, description, allowNullInput, false, dtp.DateTimeStyle ?? DateTimeStyles.DateTime);
            }
            else if (parameter is DoubleParameter && !isList)
            {
                return new NullableStructValueModel<double>(lb, description, allowNullInput, false);
            }
            else if (parameter is IntParameter && !isList)
            {
                return new NullableStructValueModel<int>(lb, description, allowNullInput, false);
            }
            else if (parameter is DecimalParameter && !isList)
            {
                return new DecimalValueModel(lb, description, allowNullInput, false);
            }
            else if (parameter is StringParameter && !isList)
            {
                return new ClassValueModel<string>(lb, description, allowNullInput, false);
            }
            else if (parameter is ObjectReferenceParameter && !isList)
            {
                return new ObjectReferenceValueModel(lb, description, allowNullInput, false, ((ObjectReferenceParameter)parameter).ObjectClass);
            }
            else if (parameter is EnumParameter && !isList)
            {
                return new EnumerationValueModel(lb, description, allowNullInput, false, ((EnumParameter)parameter).Enumeration);
            }
            else if (parameter is CompoundObjectParameter && !isList)
            {
                return new CompoundObjectValueModel(ctx, lb, description, allowNullInput, false, ((CompoundObjectParameter)parameter).CompoundObject);
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

        public abstract void SetUntypedValue(object val);

        public bool ReportErrors { get { return true; } }

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
        public abstract ZbTask<TValue> GetValueAsync();

        #endregion

        #region IValueModel Members
        public override object GetUntypedValue()
        {
            return this.Value;
        }

        public override void SetUntypedValue(object val)
        {
            this.Value = (TValue)val;
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

        public override ZbTask<TValue?> GetValueAsync()
        {
            return new ZbTask<TValue?>(ZbTask.Synchron, () => Value);
        }

        public override void ClearValue()
        {
            if (this.AllowNullInput)
                this.Value = null;
            else
                throw new InvalidOperationException("Does not allow null values");
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

        public DateTimeValueModel(string label, string description, bool allowNullInput, bool isReadOnly, DateTimeStyles dateTimeStyle, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            _dateTimeStyle = dateTimeStyle;
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
    
        ZbTask<TimeSpan?> IValueModel<TimeSpan?>.GetValueAsync()
        {
            return new ZbTask<TimeSpan?>(ZbTask.Synchron, () => ((IValueModel<TimeSpan?>)this).Value);
        }
    }

    public class BoolValueModel : NullableStructValueModel<bool>, IBoolValueModel
    {
        public BoolValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
            // Initialize with false
            // Case 3007
            // TODO: This was done to prevent ParamInputTasksViewModels to show tri state checkboxes
            // The better solution is to define default values on parameter of methods
            if (!allowNullInput) Value = false;
        }

        public BoolValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            // Initialize with false
            if (!allowNullInput) Value = false;
        }

        public Icon FalseIcon { get; set; }
        public string FalseLabel { get; set; }

        public Icon NullIcon { get; set; }
        public string NullLabel { get; set; }

        public Icon TrueIcon { get; set; }
        public string TrueLabel { get; set; }
    }

    public class DecimalValueModel : NullableStructValueModel<decimal>, IDecimalValueModel
    {
        public DecimalValueModel(string label, string description, bool allowNullInput, bool isReadOnly)
            : base(label, description, allowNullInput, isReadOnly)
        {
        }

        public DecimalValueModel(string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
        }

        public int? Precision
        {
            get;
            set;
        }

        public int? Scale
        {
            get;
            set;
        }
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

        public override ZbTask<TValue> GetValueAsync()
        {
            return new ZbTask<TValue>(ZbTask.Synchron, () => Value);
        }

        public override void ClearValue()
        {
            if (this.AllowNullInput)
                this.Value = null;
            else
                throw new InvalidOperationException("Does not allow null values");
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

        public RelationEnd RelEnd
        {
            get
            {
                return null;
            }
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
        public CompoundObjectValueModel(IZetboxContext ctx, string label, string description, bool allowNullInput, bool isReadOnly, CompoundObject def)
            : this(
                ctx,
                label, 
                description, 
                allowNullInput, 
                isReadOnly,
                def != null ? (def.RequestedKind ?? (def.DefaultPropertyViewModelDescriptor != null ? def.DefaultPropertyViewModelDescriptor.DefaultEditorKind : null)) : null, 
                def)
        {
        }

        public CompoundObjectValueModel(IZetboxContext ctx, string label, string description, bool allowNullInput, bool isReadOnly, ControlKind requestedKind, CompoundObject def)
            : base(label, description, allowNullInput, isReadOnly, requestedKind)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (def == null) throw new ArgumentNullException("def");

            this.CompoundObjectDefinition = def;
            this.Value = ctx.CreateCompoundObject(ctx.GetInterfaceType(def.GetDataType()));
        }

        #region IObjectReferenceValueModel Members

        public CompoundObject CompoundObjectDefinition
        {
            get;
            private set;
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

        public override void SetUntypedValue(object val)
        {
            base.SetUntypedValue(val);
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
