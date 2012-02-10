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
    using Kistl.API.Utils;

    /// <summary>
    /// Implement basic functionality needed by all persistent objects.
    /// </summary>
    public abstract class BasePersistenceObject
        : BaseNotifyingObject, IPersistenceObject, IDataErrorInfo, ICustomTypeDescriptor, ISortKey<int>
    {
        // TODO 4.0: replace Func<> with Lazy<>
        // http://www.davidhayden.me/2010/01/auto-factories-in-autofac-for-lazy-instantiation-lazydependencymodule.html
        protected BasePersistenceObject(Func<IFrozenContext> lazyCtx)
        {
            _lazyCtx = lazyCtx;
        }

        private readonly Func<IFrozenContext> _lazyCtx;
        private IFrozenContext _frozenContext;
        protected IFrozenContext FrozenContext
        {
            get
            {
                if (_frozenContext == null)
                {
                    _frozenContext = _lazyCtx();
                }
                return _frozenContext;
            }
        }

        /// <summary>
        /// Gets or sets the primary key of this object. By convention all persistent objects have to have this synthesised primary key.
        /// </summary>
        public abstract int ID { get; set; }

        /// <summary>
        /// Gets a value indicating whether values of this object can be set.
        /// </summary>
        /// <remarks>
        /// true, when the context is readonly or there are not sufficient access rights.
        /// Always false, when the object is not attached or a playback is happening.
        /// </remarks>
        [XmlIgnore]
        public bool IsReadonly
        {
            get
            {
                return this.Context != null && !IsRecordingNotifications
                    ? this.Context.IsReadonly || CurrentAccessRights.HasOnlyReadRightsOrNone() // when attaced -> eval. Don't look at the implementation below (CurrentAccessRights), it may be overridden
                    : false; // unattached - cannot be readonly
            }
        }

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// Base implementations returnes always Full
        /// </summary>
        private Kistl.API.AccessRights? __currentAccessRights;
        public virtual Kistl.API.AccessRights CurrentAccessRights
        {
            get
            {
                if (Context == null) return Kistl.API.AccessRights.Full;
                if (__currentAccessRights == null)
                {
                    if (ObjectState == DataObjectState.New)
                    {
                        // Newly created objects get full rights
                        __currentAccessRights = Kistl.API.AccessRights.Full;
                    }
                    else
                    {
                        __currentAccessRights = Context.GetGroupAccessRights(Context.GetInterfaceType(this.GetImplementedInterface()));
                    }
                    __currentAccessRights &= ~Kistl.API.AccessRights.Create; // exclude create rights - not instance specific
                }
                return __currentAccessRights.Value;
            }
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

        public abstract void SetNew();
        public abstract void SetUnmodified();
        public abstract void SetDeleted();
        public abstract void SetUnDeleted();

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
            if (((BasePersistenceObject)obj).GetImplementedInterface() != this.GetImplementedInterface())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }

        public virtual void SynchronizeCollections<T>(ICollection<T> me, ICollection<T> other) where T : class, IValueCollectionEntry
        {
            if (me == null) throw new ArgumentNullException("me");
            if (other == null) throw new ArgumentNullException("other");

            // Add/Modify
            foreach (IPersistenceObject otherItem in other)
            {
                var meItem = me.SingleOrDefault(i => i.ID == otherItem.ID);
                if (meItem == null)
                {
                    me.Add((T)otherItem);
                    if (otherItem.ID < Helper.INVALIDID)
                        Context.Internals().AttachAsNew(otherItem);
                    else
                        Context.Attach(otherItem);
                }
                else
                {
                    meItem.ApplyChangesFrom(otherItem);
                }
            }

            // Delete
            var toDelete = new List<T>();
            foreach (IPersistenceObject meItem in me)
            {
                var otherItem = other.SingleOrDefault(i => i.ID == meItem.ID);
                if (otherItem == null)
                {
                    toDelete.Add((T)meItem);
                }
            }

            toDelete.ForEach(i => me.Remove(i));
        }

        public virtual void SynchronizeLists<T>(ICollection<T> me, ICollection<T> other) where T : class, IValueListEntry
        {
            if (me == null) throw new ArgumentNullException("me");
            if (other == null) throw new ArgumentNullException("other");

            var meList = me.OrderBy(i => i.Index ?? -1).ToList();
            var otherList = other.OrderBy(i => i.Index ?? -1).ToList();

            var diff = Diff.DiffInt(
                meList.Select(i => i == null ? -1 : i.ID).ToArray(),
                otherList.Select(i => i == null ? -1 : i.ID).ToArray());

            foreach (var item in diff)
            {
                int deleted = 0;
                while (deleted < item.deletedA)
                {
                    me.Remove(meList[item.StartA]);
                    meList.RemoveAt(item.StartA);
                    deleted += 1;
                }
                int added = 0;
                while (added < item.insertedB)
                {
                    var otherItem = otherList[item.StartB + added];
                    meList.Insert(item.StartA + added, otherItem);
                    me.Add(otherItem);
                    if (otherItem.ID < Helper.INVALIDID)
                        Context.Internals().AttachAsNew(otherItem);
                    else
                        Context.Attach(otherItem);
                    added += 1;
                }
            }
        }


        /// <summary>
        /// Returns the most specific System.Type implemented by this object.
        /// </summary>
        /// <returns>the System.Type of this object</returns>
        public abstract Type GetImplementedInterface();

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

            BinarySerializer.ToStream(ReadOnlyContext.GetInterfaceType(this.GetImplementedInterface()).ToSerializableType(), sw);
            BinarySerializer.ToStream(this.ID, sw);
        }

        /// <summary>
        /// Base method for deserializing this Object.
        /// </summary>
        /// <param name="sr">Stream to serialize from</param>
        public virtual IEnumerable<IPersistenceObject> FromStream(BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            if (this.IsAttached)
                throw new InvalidOperationException("Deserializing attached objects is not allowed");

            BinarySerializer.FromStreamConverter(i => this.ID = i, sr);
            return null;
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
        public virtual IEnumerable<IPersistenceObject> FromStream(XmlReader xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            return null;
        }

        /// <summary>
        /// Reloads all references to other objects from the underlying storage. Providers may use this to update internal metadata after deserialisation.
        /// </summary>
        public virtual void ReloadReferences()
        {
        }

        #endregion

        #region IDataErrorInfo Members

        protected virtual ObjectIsValidResult ObjectIsValid()
        {
            return ObjectIsValidResult.Valid;
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
                    .Concat(ObjectIsValid().Errors.AsEnumerable())
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

        protected virtual PropertyDescriptor[] AccessPropertyDescriptors() { return _properties; }

        string ICustomTypeDescriptor.GetClassName() { return this.GetType().FullName; }

        string ICustomTypeDescriptor.GetComponentName() { return this.GetType().FullName; }

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
                null,
                null)
        };

        protected virtual void CollectProperties(Func<IFrozenContext> lazyCtx, List<PropertyDescriptor> props)
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
                CollectProperties(_lazyCtx, props);
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
