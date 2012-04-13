// <autogenerated/>

namespace Kistl.App.GUI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.API.Utils;
    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.NHibernate;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Visual")]
    public class VisualNHibernateImpl : Kistl.DalProvider.NHibernate.DataObjectNHibernateImpl, Visual
    {
        private static readonly Guid _objectClassID = new Guid("98790e5d-808f-4e0b-8a1a-b304839f07ab");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public VisualNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public VisualNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new VisualProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public VisualNHibernateImpl(Func<IFrozenContext> lazyCtx, VisualProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly VisualProxy Proxy;

        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
        // collection entry list property
   		// Kistl.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.Visual> Children
		{
			get
			{
				if (_Children == null)
				{
					_Children 
						= new NHibernateBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl.Visual_contains_Visual_RelationEntryProxy, Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl>(
                                this.Proxy.Children,
                                p => (Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl.Visual_contains_Visual_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy),
                            entry => (IRelationListSync<Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl>)null);
                    if (Children_was_eagerLoaded) { Children_was_eagerLoaded = false; }
				}
				return (ICollection<Kistl.App.GUI.Visual>)_Children;
			}
		}

		private NHibernateBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl> _Children;
		// ignored, but required for Serialization
        private bool Children_was_eagerLoaded = false;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Visual> OnChildren_IsValid;

        /// <summary>
        /// The context menu for this Visual
        /// </summary>
        // collection entry list property
   		// Kistl.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Kistl.App.GUI.Visual> ContextMenu
		{
			get
			{
				if (_ContextMenu == null)
				{
					_ContextMenu 
						= new NHibernateBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl.Visual_hasContextMenu_Visual_RelationEntryProxy, Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl>(
                                this.Proxy.ContextMenu,
                                p => (Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl.Visual_hasContextMenu_Visual_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy),
                            entry => (IRelationListSync<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl>)null);
                    if (ContextMenu_was_eagerLoaded) { ContextMenu_was_eagerLoaded = false; }
				}
				return (ICollection<Kistl.App.GUI.Visual>)_ContextMenu;
			}
		}

		private NHibernateBSideCollectionWrapper<Kistl.App.GUI.Visual, Kistl.App.GUI.Visual, Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl> _ContextMenu;
		// ignored, but required for Serialization
        private bool ContextMenu_was_eagerLoaded = false;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Visual> OnContextMenu_IsValid;

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Description
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Description;
                if (OnDescription_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDescription_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Description != value)
                {
                    var __oldValue = Proxy.Description;
                    var __newValue = value;
                    if (OnDescription_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Description", __oldValue, __newValue);
                    Proxy.Description = __newValue;
                    NotifyPropertyChanged("Description", __oldValue, __newValue);

                    if (OnDescription_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDescription_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Description");
				}
            }
        }

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, string> OnDescription_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, string> OnDescription_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, string> OnDescription_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Visual> OnDescription_IsValid;

        /// <summary>
        /// The Method whose return value shoud be displayed
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Method
        // fkBackingName=this.Proxy.Method; fkGuidBackingName=_fk_guid_Method;
        // referencedInterface=Kistl.App.Base.Method; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Method Method
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.MethodNHibernateImpl __value = (Kistl.App.Base.MethodNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Method);

                if (OnMethod_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Method>(__value);
                    OnMethod_Getter(this, e);
                    __value = (Kistl.App.Base.MethodNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Method == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.MethodNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Method);
                var __newValue = (Kistl.App.Base.MethodNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Method", __oldValue, __newValue);

                if (OnMethod_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Method>(__oldValue, __newValue);
                    OnMethod_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.MethodNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Method = null;
                }
                else
                {
                    this.Proxy.Method = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Method", __oldValue, __newValue);

                if (OnMethod_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Method>(__oldValue, __newValue);
                    OnMethod_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Method's id, used on dehydration only</summary>
        private int? _fk_Method = null;


        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Method
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Method> OnMethod_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Visual> OnMethod_IsValid;

        /// <summary>
        /// The Property to display
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
        // fkBackingName=this.Proxy.Property; fkGuidBackingName=_fk_guid_Property;
        // referencedInterface=Kistl.App.Base.Property; moduleNamespace=Kistl.App.GUI;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property Property
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.PropertyNHibernateImpl __value = (Kistl.App.Base.PropertyNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Property);

                if (OnProperty_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Property>(__value);
                    OnProperty_Getter(this, e);
                    __value = (Kistl.App.Base.PropertyNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.Property == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.PropertyNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Property);
                var __newValue = (Kistl.App.Base.PropertyNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Property", __oldValue, __newValue);

                if (OnProperty_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.PropertyNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.Property = null;
                }
                else
                {
                    this.Proxy.Property = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Property", __oldValue, __newValue);

                if (OnProperty_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Property>(__oldValue, __newValue);
                    OnProperty_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for Property's id, used on dehydration only</summary>
        private int? _fk_Property = null;


        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Property
		public static event PropertyGetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.Visual, Kistl.App.Base.Property> OnProperty_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.Visual> OnProperty_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Visual);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Visual)obj;
            var otherImpl = (VisualNHibernateImpl)obj;
            var me = (Visual)this;

            me.Description = other.Description;
            this._fk_Method = otherImpl._fk_Method;
            this._fk_Property = otherImpl._fk_Property;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Method":
                    {
                        var __oldValue = (Kistl.App.Base.MethodNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Method);
                        var __newValue = (Kistl.App.Base.MethodNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Method", __oldValue, __newValue);
                        this.Proxy.Method = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Method", __oldValue, __newValue);
                    }
                    break;
                case "Property":
                    {
                        var __oldValue = (Kistl.App.Base.PropertyNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.Property);
                        var __newValue = (Kistl.App.Base.PropertyNHibernateImpl)parentObj;
                        NotifyPropertyChanging("Property", __oldValue, __newValue);
                        this.Proxy.Property = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("Property", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Description":
                case "Method":
                case "Property":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Method.HasValue)
                this.Method = ((Kistl.App.Base.MethodNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.Method>(_fk_Method.Value));
            else
                this.Method = null;

            if (_fk_Property.HasValue)
                this.Property = ((Kistl.App.Base.PropertyNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.Property>(_fk_Property.Value));
            else
                this.Property = null;
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<Visual, ICollection<Kistl.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("9f69c3bd-e274-4639-b30c-8d2a9599917b"),
                        "Children",
                        null,
                        obj => obj.Children,
                        null, // lists are read-only properties
                        obj => OnChildren_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<Visual, ICollection<Kistl.App.GUI.Visual>>(
                        lazyCtx,
                        new Guid("7b18f26e-0f3f-4554-b469-1029bd4ca10b"),
                        "ContextMenu",
                        null,
                        obj => obj.ContextMenu,
                        null, // lists are read-only properties
                        obj => OnContextMenu_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Visual, string>(
                        lazyCtx,
                        new Guid("8d3b7c91-2bbf-4dcf-bc37-318dc0fda92d"),
                        "Description",
                        null,
                        obj => obj.Description,
                        (obj, val) => obj.Description = val,
						obj => OnDescription_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Visual, Kistl.App.Base.Method>(
                        lazyCtx,
                        new Guid("0b55b2ba-3ac0-4631-8a73-1e8846c8e9b1"),
                        "Method",
                        null,
                        obj => obj.Method,
                        (obj, val) => obj.Method = val,
						obj => OnMethod_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<Visual, Kistl.App.Base.Property>(
                        lazyCtx,
                        new Guid("a432e3ff-61ed-4726-8559-f34516181065"),
                        "Property",
                        null,
                        obj => obj.Property,
                        (obj, val) => obj.Property = val,
						obj => OnProperty_IsValid), 
                    // position columns
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Visual")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Visual != null)
            {
                OnToString_Visual(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Visual> OnToString_Visual;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Visual")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Visual != null)
            {
                OnObjectIsValid_Visual(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Visual> OnObjectIsValid_Visual;

        [EventBasedMethod("OnNotifyPreSave_Visual")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Visual != null) OnNotifyPreSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnNotifyPreSave_Visual;

        [EventBasedMethod("OnNotifyPostSave_Visual")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Visual != null) OnNotifyPostSave_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnNotifyPostSave_Visual;

        [EventBasedMethod("OnNotifyCreated_Visual")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Description");
            SetNotInitializedProperty("Method");
            SetNotInitializedProperty("Property");
            base.NotifyCreated();
            if (OnNotifyCreated_Visual != null) OnNotifyCreated_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnNotifyCreated_Visual;

        [EventBasedMethod("OnNotifyDeleting_Visual")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Visual != null) OnNotifyDeleting_Visual(this);
        }
        public static event ObjectEventHandler<Visual> OnNotifyDeleting_Visual;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            // Follow Visual_has_Method
            if (this.Method != null && this.Method.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.Method);

            // Follow Visual_has_Property
            if (this.Property != null && this.Property.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.Property);

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            // Follow Template_has_VisualTree
            result.AddRange(Context.AttachedObjects
                .OfType<Kistl.App.GUI.Template>()
                .Where(child => child.VisualTree == this
                    && child.ObjectState == DataObjectState.Deleted)
                .Cast<NHibernatePersistenceObject>());

            return result;
        }


        public class VisualProxy
            : IProxyObject, ISortKey<int>
        {
            public VisualProxy()
            {
                Children = new Collection<Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl.Visual_contains_Visual_RelationEntryProxy>();
                ContextMenu = new Collection<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl.Visual_hasContextMenu_Visual_RelationEntryProxy>();
            }

            public virtual int ID { get; set; }

            public virtual Type ZBoxWrapper { get { return typeof(VisualNHibernateImpl); } }
            public virtual Type ZBoxProxy { get { return typeof(VisualProxy); } }

            public virtual ICollection<Kistl.App.GUI.Visual_contains_Visual_RelationEntryNHibernateImpl.Visual_contains_Visual_RelationEntryProxy> Children { get; set; }

            public virtual ICollection<Kistl.App.GUI.Visual_hasContextMenu_Visual_RelationEntryNHibernateImpl.Visual_hasContextMenu_Visual_RelationEntryProxy> ContextMenu { get; set; }

            public virtual string Description { get; set; }

            public virtual Kistl.App.Base.MethodNHibernateImpl.MethodProxy Method { get; set; }

            public virtual Kistl.App.Base.PropertyNHibernateImpl.PropertyProxy Property { get; set; }

        }

        // make proxy available for the provider
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this.Proxy.Description, binStream);
            BinarySerializer.ToStream(this.Proxy.Method != null ? OurContext.GetIdFromProxy(this.Proxy.Method) : (int?)null, binStream);
            BinarySerializer.ToStream(this.Proxy.Property != null ? OurContext.GetIdFromProxy(this.Proxy.Property) : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.Description = tmp;
            }
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._fk_Property, binStream);
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(this.Proxy.Description, xml, "Description", "Kistl.App.GUI");
            XmlStreamer.ToStream(this.Proxy.Method != null ? OurContext.GetIdFromProxy(this.Proxy.Method) : (int?)null, xml, "Method", "Kistl.App.GUI");
            XmlStreamer.ToStream(this.Proxy.Property != null ? OurContext.GetIdFromProxy(this.Proxy.Property) : (int?)null, xml, "Property", "Kistl.App.GUI");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                string tmp = this.Proxy.Description;
                XmlStreamer.FromStream(ref tmp, xml, "Description", "Kistl.App.GUI");
                this.Proxy.Description = tmp;
            }
            XmlStreamer.FromStream(ref this._fk_Method, xml, "Method", "Kistl.App.GUI");
            XmlStreamer.FromStream(ref this._fk_Property, xml, "Property", "Kistl.App.GUI");
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}