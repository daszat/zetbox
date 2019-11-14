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
namespace Zetbox.API
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
    using Zetbox.API.Utils;

    /// <summary>
    /// Implement basic functionality needed by all persistent objects.
    /// </summary>
    public abstract class BasePersistenceObject
        : BaseNotifyingObject, IPersistenceObject, IDataErrorInfo, ICustomTypeDescriptor, ISortKey<int>
    {
        // TODO: Remove this
        protected BasePersistenceObject(Func<IFrozenContext> lazyCtx)
        {
            _lazyCtx = lazyCtx;
        }

        // TODO 4.0: replace Func<> with Lazy<>
        // http://www.davidhayden.me/2010/01/auto-factories-in-autofac-for-lazy-instantiation-lazydependencymodule.html
        private Func<IFrozenContext> _lazyCtx;
        private IFrozenContext _frozenContext;
        [System.Runtime.Serialization.IgnoreDataMember]
        public IFrozenContext FrozenContext
        {
            get
            {
                if (_frozenContext == null && _lazyCtx != null)
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
        [System.Runtime.Serialization.IgnoreDataMember]
        public bool IsReadonly
        {
            get
            {
                return this.Context != null && !IsRecordingNotifications
                    ? this.Context.IsReadonly || CurrentAccessRights.HasOnlyReadRightsOrNone() // when attached -> eval. Don't look at the implementation below (CurrentAccessRights), it may be overridden
                    : false; // unattached - cannot be readonly
            }
        }

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// Base implementations returnes always Full
        /// </summary>
        private Zetbox.API.AccessRights? __currentAccessRights;
        [System.Runtime.Serialization.IgnoreDataMember]
        public virtual Zetbox.API.AccessRights CurrentAccessRights
        {
            get
            {
                if (Context == null) return Zetbox.API.AccessRights.FullInstance;
                if (__currentAccessRights == null)
                {
                    __currentAccessRights = Context.GetGroupAccessRights(Context.GetInterfaceType(this.GetImplementedInterface()));
                    // exclude create rights - not instance specific
                    __currentAccessRights &= ~Zetbox.API.AccessRights.Create;
                }
                return __currentAccessRights.Value;
            }
        }

        protected virtual void ResetCurrentAccessRights()
        {
            __currentAccessRights = null;
        }

        /// <summary>
        /// Gets the <see cref="IZetboxContext"/> containing this object.
        /// </summary>
        [XmlIgnore]
        [System.Runtime.Serialization.IgnoreDataMember]
        public IZetboxContext Context { get; private set; }
        /// <summary>
        /// Gets the <see cref="IReadOnlyZetboxContext"/> containing this object.
        /// </summary>
        [XmlIgnore]
        [System.Runtime.Serialization.IgnoreDataMember]
        public IReadOnlyZetboxContext ReadOnlyContext { get { return Context; } }

        /// <summary>
        /// Gets a value indicating whether or not this object is attached to a context.
        /// </summary>
        [XmlIgnore]
        [System.Runtime.Serialization.IgnoreDataMember]
        public abstract bool IsAttached { get; }

        /// <summary>
        /// Gets a value indicating the current state of this object.
        /// </summary>
        [System.Runtime.Serialization.IgnoreDataMember]
        public abstract DataObjectState ObjectState { get; }

        public virtual void SetNew()
        {
            // Newly created objects get full rights
            __currentAccessRights = Zetbox.API.AccessRights.FullInstance;
        }

        public abstract void SetUnmodified();
        public abstract void SetDeleted();
        public abstract void SetUnDeleted();

        /// <summary>
        /// Attach this Object to a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to attach this Object to.</param>
        /// <param name="lazyFrozenContext">lazyFrozenContext to attach to the object</param>
        public virtual void AttachToContext(IZetboxContext ctx, Func<IFrozenContext> lazyFrozenContext)
        {
            if (lazyFrozenContext != null)
                _lazyCtx = lazyFrozenContext;

            if (this.Context != null && this.Context != ctx)
                throw new WrongZetboxContextException("Object cannot be attached to a new Context while attached to another Context.");

            this.Context = ctx;
        }

        /// <summary>
        /// Detach this Object from a Context. This Method is called by the Context.
        /// </summary>
        /// <param name="ctx">Context to detach this Object from.</param>
        public virtual void DetachFromContext(IZetboxContext ctx)
        {
            if (this.Context != ctx)
                throw new WrongZetboxContextException("Object is not attached to the given context.");

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

        public virtual void SynchronizeCollections<T>(ICollection<T> me, ICollection<T> other) where T : BasePersistenceObject, IValueCollectionEntry
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
                    // record notifications of collection entry changes while we're recording ourselves.
                    if (IsRecordingNotifications)
                        meItem.RecordNotifications();
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

        #region Lifecycle Events

        /// <summary>
        /// Fires an Event after an Object is created.
        /// </summary>
        public virtual void NotifyCreated() { }
        /// <summary>
        /// Fires an Event before an Object is deleted.
        /// </summary>
        public virtual void NotifyDeleting() { }

        #endregion

        #region IStreamable Members

        /// <summary>
        /// Base method for serializing this Object.
        /// </summary>
        /// <param name="sw">Stream to serialize to</param>
        /// <param name="auxObjects">pass a List here to collect auxiliary, eagerly loaded objects. Ignored if null.</param>
        /// <param name="eagerLoadLists">True if Lists should be eager loaded.</param>
        public virtual void ToStream(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");

            sw.Write(ReadOnlyContext.GetInterfaceType(this.GetImplementedInterface()).ToSerializableType());
            sw.Write(this.ID);
        }

        /// <summary>
        /// Base method for deserializing this Object.
        /// </summary>
        /// <param name="sr">Stream to serialize from</param>
        public virtual IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            if (this.IsAttached)
                throw new InvalidOperationException("Deserializing attached objects is not allowed");

            sr.ReadConverter(i => this.ID = i);
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

        public ObjectIsValidResult Validate()
        {
            return ObjectIsValid();
        }

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
        [System.Runtime.Serialization.IgnoreDataMember]
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
        [System.Runtime.Serialization.IgnoreDataMember]
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

        #region TransientState
        [NonSerialized]
        private Dictionary<object, object> _transientState;
        /// <inheritdoc />
        [XmlIgnore]
        [System.Runtime.Serialization.IgnoreDataMember]
        public Dictionary<object, object> TransientState
        {
            get
            {
                if (_transientState == null)
                {
                    _transientState = new Dictionary<object, object>();
                }
                return _transientState;
            }
        }

        #endregion

        #region ISortKey<int>
        [System.Runtime.Serialization.IgnoreDataMember]
        int ISortKey<int>.InternalSortKey => ID;
        #endregion
    }
}
