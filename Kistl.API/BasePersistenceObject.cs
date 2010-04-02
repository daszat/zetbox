namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Implement basic functionality needed by all persistent objects.
    /// </summary>
    public abstract class BasePersistenceObject
        : BaseNotifyingObject, IPersistenceObject, IDataErrorInfo, ICustomTypeDescriptor
    {
        /// <summary>
        /// Gets or sets the primary key of this object. By convention all persistent objects have to have this synthesised primary key.
        /// </summary>
        public abstract int ID { get; set; }

        /// <summary>
        /// Gets a value indicating whether values of this object can be set. This is only a shorthand for asking the context for read-only status.
        /// </summary>
        [XmlIgnore]
        public bool IsReadonly
        {
            get { return this.Context != null ? this.Context.IsReadonly : false; }
        }

        /// <summary>
        /// Gets the <see cref="IKistlContext"/> containing this object.
        /// </summary>
        [XmlIgnore]
        public IKistlContext Context { get; private set; }
        /// <summary>
        /// Gets the <see cref="IReadOnlyKistlContext"/> containing this object.
        /// </summary>
        [XmlIgnore]
        public IReadOnlyKistlContext ReadOnlyContext { get { return Context; } }

        /// <summary>
        /// Gets a value indicating whether or not this object is attached to a context.
        /// </summary>
        [XmlIgnore]
        public abstract bool IsAttached { get; }

        /// <summary>
        /// Gets a value indicating the current state of this object.
        /// </summary>
        public abstract DataObjectState ObjectState { get; }

        /// <summary>
        /// Attach this Object to a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to attach this Object to.</param>
        public virtual void AttachToContext(IKistlContext ctx)
        {
            if (this.Context != null && this.Context != ctx)
                throw new WrongKistlContextException("Object cannot be attached to a new Context while attached to another Context.");

            this.Context = ctx;
        }

        /// <summary>
        /// Detach this Object from a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to detach this Object from.</param>
        public virtual void DetachFromContext(IKistlContext ctx)
        {
            if (this.Context != ctx)
                throw new WrongKistlContextException("Object is not attached to the given context.");

            this.Context = null;
        }

        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj">the object to copy from</param>
        public virtual void ApplyChangesFrom(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj.GetType().ToInterfaceType() != this.GetType().ToInterfaceType())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }

        /// <summary>
        /// Returns the most specific <see cref="InterfaceType"/> implemented by this object.
        /// </summary>
        /// <returns>the <see cref="InterfaceType"/> of this object</returns>
        public abstract InterfaceType GetInterfaceType();

        #region IStreamable Members

        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        /// <param name="auxObjects">pass a List here to collect auxiliary, eagerly loaded objects. Ignored if null.</param>
        /// <param name="eagerLoadLists">True if Lists should be eager loaded.</param>
        public virtual void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");

            BinarySerializer.ToStream(new SerializableType(this.GetInterfaceType()), sw);
            BinarySerializer.ToStream(this.ID, sw);
        }

        /// <summary>
        /// Base method for deserializing this Object.
        /// </summary>
        /// <param name="sr">Stream to serialize from</param>
        public virtual void FromStream(BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            if (this.IsAttached)
                throw new InvalidOperationException("Deserializing attached objects is not allowed");

            BinarySerializer.FromStreamConverter(i => this.ID = i, sr);
        }

        /// <summary>
        /// Base method for serializing this Object to XML.
        /// </summary>
        /// <param name="xml">Stream to serialize to</param>
        /// <param name="modules">an array of module names to constrain the output</param>
        [Obsolete]
        public virtual void ToStream(XmlWriter xml, string[] modules)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public virtual void ToStream(XmlWriter xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");
        }

        /// <summary>
        /// Base method for deserializing this Object from XML.
        /// </summary>
        /// <param name="xml">Stream to serialize from</param>
        public virtual void FromStream(XmlReader xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");
        }

        /// <summary>
        /// Reloads all references to other objects from the underlying storage. Providers may use this to update internal metadata after deserialisation.
        /// </summary>
        public virtual void ReloadReferences()
        {
        }

        #endregion

        #region IDataErrorInfo Members

        /// <summary>
        /// Returns a value indicating whether or not this object is in a valid configuration.
        /// </summary>
        /// <returns>a value indicating whether or not this object is in a valid configuration.</returns>
        public virtual bool IsValid()
        {
            return String.IsNullOrEmpty(((IDataErrorInfo)this).Error);
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="prop">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. Returns 
        /// <value>null</value> if there is nothing to report.</returns>
        protected virtual string GetPropertyError(string prop)
        {
            // TODO: implement proper interface here
            var cpd = GetProperties()[prop] as IValidatingPropertyDescriptor;
            if (cpd != null)
            {
                return String.Join("; ", cpd.GetValidationErrors(this));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object. Returns 
        /// <value>String.Empty</value> if there is nothing to report.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [XmlIgnore]
        string IDataErrorInfo.Error
        {
            get
            {
                var errors = GetProperties()
                    .OfType<IValidatingPropertyDescriptor>()
                    .Select(vpd =>
                    {
                        var errorStrings = vpd.GetValidationErrors(this);
                        if (errorStrings == null || errorStrings.Length == 0)
                        {
                            return null;
                        }

                        var error = String.Join(",", errorStrings);
                        return vpd.UnderlyingDescriptor.Name + ": " + error;
                    })
                    .Where(err => !String.IsNullOrEmpty(err))
                    .ToArray();
                return String.Join("\n", errors);
            }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. Returns 
        /// <value>String.Empty</value> if there is nothing to report.</returns>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [XmlIgnore]
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return GetPropertyError(columnName);
            }
        }


        #endregion

        #region ICustomTypeDescriptor Members

        protected virtual string GetName() { return this.GetType().FullName; }

        protected virtual PropertyDescriptor[] AccessPropertyDescriptors() { return _properties; }

        string ICustomTypeDescriptor.GetClassName() { return GetName(); }

        string ICustomTypeDescriptor.GetComponentName() { return GetName(); }

        TypeConverter ICustomTypeDescriptor.GetConverter() { return null; }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() { return null; }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() { return null; }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType) { return null; }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) { return this; }

        #region Attributes

        private static readonly Attribute[] _attributes = new Attribute[] { };

        protected virtual void CollectAttributes(List<Attribute> attrs)
        {
            attrs.AddRange(_attributes);
        }

        private static AttributeCollection _attributeCollection;
        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            if (_attributeCollection == null)
            {
                var attrs = new List<Attribute>();
                CollectAttributes(attrs);
                _attributeCollection = new AttributeCollection(attrs.ToArray());
            }

            return _attributeCollection;
        }

        #endregion

        #region Events

        private static readonly EventDescriptor[] _events = new EventDescriptor[] { };

        protected virtual void CollectEvents(List<EventDescriptor> events)
        {
            events.AddRange(_events);
        }

        private static EventDescriptorCollection _eventDescriptorCollection = null;

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            if (_eventDescriptorCollection == null)
            {
                var events = new List<EventDescriptor>();
                CollectEvents(events);
                _eventDescriptorCollection = new EventDescriptorCollection(events.ToArray(), true);
            }

            return _eventDescriptorCollection;
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return new EventDescriptorCollection(
                ((ICustomTypeDescriptor)this)
                    .GetEvents()
                    .OfType<EventDescriptor>()
                    .Where(ev => ev.Attributes.Matches(attributes))
                    .ToArray());
        }

        #endregion

        #region Properties

        private static readonly PropertyDescriptor[] _properties = new PropertyDescriptor[] {
            new BaseCustomPropertyDescriptor<IDataObject, int>(
                "ID",
                null,
                obj => obj.ID,
                null)
        };

        protected virtual void CollectProperties(List<PropertyDescriptor> props)
        {
            props.AddRange(_properties);
        }

        // TODO: make this per-actual-Type instead of per-Instance
        private PropertyDescriptorCollection _propertyDescriptorCollection = null;

        public PropertyDescriptorCollection GetProperties()
        {
            if (_propertyDescriptorCollection == null)
            {
                var props = new List<PropertyDescriptor>();
                CollectProperties(props);
                _propertyDescriptorCollection = new PropertyDescriptorCollection(props.ToArray(), true);
            }

            return _propertyDescriptorCollection;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(
                ((ICustomTypeDescriptor)this)
                    .GetProperties()
                    .OfType<PropertyDescriptor>()
                    .Where(ev => ev.Attributes.Matches(attributes))
                    .ToArray());
        }

        #endregion

        #endregion
    }
}
