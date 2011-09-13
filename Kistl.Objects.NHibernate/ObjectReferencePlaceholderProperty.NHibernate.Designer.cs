// <autogenerated/>

namespace Kistl.App.Base
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
    /// A placeholder for data object references in interfaces
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectReferencePlaceholderProperty")]
    public class ObjectReferencePlaceholderPropertyNHibernateImpl : Kistl.App.Base.PropertyNHibernateImpl, ObjectReferencePlaceholderProperty
    {
        public ObjectReferencePlaceholderPropertyNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public ObjectReferencePlaceholderPropertyNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new ObjectReferencePlaceholderPropertyProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public ObjectReferencePlaceholderPropertyNHibernateImpl(Func<IFrozenContext> lazyCtx, ObjectReferencePlaceholderPropertyProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly ObjectReferencePlaceholderPropertyProxy Proxy;

        /// <summary>
        /// Whether or not the list has a persistent ordering of elements
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool HasPersistentOrder
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.HasPersistentOrder;
                if (OnHasPersistentOrder_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnHasPersistentOrder_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.HasPersistentOrder != value)
                {
                    var __oldValue = Proxy.HasPersistentOrder;
                    var __newValue = value;
                    if (OnHasPersistentOrder_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnHasPersistentOrder_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("HasPersistentOrder", __oldValue, __newValue);
                    Proxy.HasPersistentOrder = __newValue;
                    NotifyPropertyChanged("HasPersistentOrder", __oldValue, __newValue);
                    if (OnHasPersistentOrder_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnHasPersistentOrder_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnHasPersistentOrder_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnHasPersistentOrder_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnHasPersistentOrder_PostSetter;

        /// <summary>
        /// Suggested implementors role name. If empty, class name will be used
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string ImplementorRoleName
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.ImplementorRoleName;
                if (OnImplementorRoleName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnImplementorRoleName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.ImplementorRoleName != value)
                {
                    var __oldValue = Proxy.ImplementorRoleName;
                    var __newValue = value;
                    if (OnImplementorRoleName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnImplementorRoleName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ImplementorRoleName", __oldValue, __newValue);
                    Proxy.ImplementorRoleName = __newValue;
                    NotifyPropertyChanged("ImplementorRoleName", __oldValue, __newValue);
                    if (OnImplementorRoleName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnImplementorRoleName_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnImplementorRoleName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnImplementorRoleName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnImplementorRoleName_PostSetter;

        /// <summary>
        /// Whether or not this property placeholder is list valued
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool IsList
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.IsList;
                if (OnIsList_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnIsList_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.IsList != value)
                {
                    var __oldValue = Proxy.IsList;
                    var __newValue = value;
                    if (OnIsList_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsList_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsList", __oldValue, __newValue);
                    Proxy.IsList = __newValue;
                    NotifyPropertyChanged("IsList", __oldValue, __newValue);
                    if (OnIsList_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsList_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnIsList_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnIsList_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnIsList_PostSetter;

        /// <summary>
        /// Suggested role name for the referenced item
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string ItemRoleName
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.ItemRoleName;
                if (OnItemRoleName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnItemRoleName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.ItemRoleName != value)
                {
                    var __oldValue = Proxy.ItemRoleName;
                    var __newValue = value;
                    if (OnItemRoleName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnItemRoleName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ItemRoleName", __oldValue, __newValue);
                    Proxy.ItemRoleName = __newValue;
                    NotifyPropertyChanged("ItemRoleName", __oldValue, __newValue);
                    if (OnItemRoleName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnItemRoleName_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnItemRoleName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnItemRoleName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnItemRoleName_PostSetter;

        /// <summary>
        /// The ObjectClass that is referenced by this placeholder
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ReferencedObjectClass
        // fkBackingName=this.Proxy.ReferencedObjectClass; fkGuidBackingName=_fk_guid_ReferencedObjectClass;
        // referencedInterface=Kistl.App.Base.ObjectClass; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass ReferencedObjectClass
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return null;
                Kistl.App.Base.ObjectClassNHibernateImpl __value = (Kistl.App.Base.ObjectClassNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ReferencedObjectClass);

                if (OnReferencedObjectClass_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.ObjectClass>(__value);
                    OnReferencedObjectClass_Getter(this, e);
                    __value = (Kistl.App.Base.ObjectClassNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.ReferencedObjectClass == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.ObjectClassNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ReferencedObjectClass);
                var __newValue = (Kistl.App.Base.ObjectClassNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("ReferencedObjectClass", __oldValue, __newValue);

                if (OnReferencedObjectClass_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnReferencedObjectClass_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.ObjectClassNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.ReferencedObjectClass = null;
                }
                else
                {
                    this.Proxy.ReferencedObjectClass = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("ReferencedObjectClass", __oldValue, __newValue);

                if (OnReferencedObjectClass_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnReferencedObjectClass_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for ReferencedObjectClass's id, used on dehydration only</summary>
        private int? _fk_ReferencedObjectClass = null;

        /// <summary>Backing store for ReferencedObjectClass's guid, used on import only</summary>
        private Guid? _fk_guid_ReferencedObjectClass = null;

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ReferencedObjectClass
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, Kistl.App.Base.ObjectClass> OnReferencedObjectClass_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, Kistl.App.Base.ObjectClass> OnReferencedObjectClass_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, Kistl.App.Base.ObjectClass> OnReferencedObjectClass_PostSetter;

        /// <summary>
        /// Suggested verb for the new relation
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string Verb
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Verb;
                if (OnVerb_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnVerb_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Verb != value)
                {
                    var __oldValue = Proxy.Verb;
                    var __newValue = value;
                    if (OnVerb_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnVerb_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Verb", __oldValue, __newValue);
                    Proxy.Verb = __newValue;
                    NotifyPropertyChanged("Verb", __oldValue, __newValue);
                    if (OnVerb_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnVerb_PostSetter(this, __e);
                    }
                }
            }
        }
        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnVerb_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnVerb_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnVerb_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_ObjectReferencePlaceholderProperty")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_ObjectReferencePlaceholderProperty != null)
            {
                OnGetLabel_ObjectReferencePlaceholderProperty(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<ObjectReferencePlaceholderProperty> OnGetLabel_ObjectReferencePlaceholderProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyType_ObjectReferencePlaceholderProperty")]
        public override System.Type GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_ObjectReferencePlaceholderProperty != null)
            {
                OnGetPropertyType_ObjectReferencePlaceholderProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<ObjectReferencePlaceholderProperty> OnGetPropertyType_ObjectReferencePlaceholderProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyTypeString_ObjectReferencePlaceholderProperty")]
        public override string GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_ObjectReferencePlaceholderProperty != null)
            {
                OnGetPropertyTypeString_ObjectReferencePlaceholderProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<ObjectReferencePlaceholderProperty> OnGetPropertyTypeString_ObjectReferencePlaceholderProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(ObjectReferencePlaceholderProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ObjectReferencePlaceholderProperty)obj;
            var otherImpl = (ObjectReferencePlaceholderPropertyNHibernateImpl)obj;
            var me = (ObjectReferencePlaceholderProperty)this;

            me.HasPersistentOrder = other.HasPersistentOrder;
            me.ImplementorRoleName = other.ImplementorRoleName;
            me.IsList = other.IsList;
            me.ItemRoleName = other.ItemRoleName;
            me.Verb = other.Verb;
            this._fk_ReferencedObjectClass = otherImpl._fk_ReferencedObjectClass;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
        }

        public override void UpdateParent(string propertyName, int? id)
        {
            switch(propertyName)
            {
                case "ReferencedObjectClass":
                    {
                        var __oldValue = (Kistl.App.Base.ObjectClassNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ReferencedObjectClass);
                        var __newValue = (Kistl.App.Base.ObjectClassNHibernateImpl)(id == null ? null : OurContext.Find<Kistl.App.Base.ObjectClass>(id.Value));
                        NotifyPropertyChanging("ReferencedObjectClass", __oldValue, __newValue);
                        this.Proxy.ReferencedObjectClass = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("ReferencedObjectClass", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, id);
                    break;
            }
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_ReferencedObjectClass.HasValue)
                this.Proxy.ReferencedObjectClass = ((Kistl.App.Base.ObjectClassNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.ObjectClass>(_fk_guid_ReferencedObjectClass.Value)).Proxy;
            else
            if (_fk_ReferencedObjectClass.HasValue)
                this.Proxy.ReferencedObjectClass = ((Kistl.App.Base.ObjectClassNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.ObjectClass>(_fk_ReferencedObjectClass.Value)).Proxy;
            else
                this.Proxy.ReferencedObjectClass = null;
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
                    // else
                    new PropertyDescriptorNHibernateImpl<ObjectReferencePlaceholderPropertyNHibernateImpl, bool>(
                        lazyCtx,
                        new Guid("7e52aa2a-aa3a-4f5b-8171-c6c2f364108b"),
                        "HasPersistentOrder",
                        null,
                        obj => obj.HasPersistentOrder,
                        (obj, val) => obj.HasPersistentOrder = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<ObjectReferencePlaceholderPropertyNHibernateImpl, string>(
                        lazyCtx,
                        new Guid("b5fa31d8-ad30-4aeb-b5a0-8b4b117b1d29"),
                        "ImplementorRoleName",
                        null,
                        obj => obj.ImplementorRoleName,
                        (obj, val) => obj.ImplementorRoleName = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<ObjectReferencePlaceholderPropertyNHibernateImpl, bool>(
                        lazyCtx,
                        new Guid("52692870-0bd4-47b6-99dc-eb8bf4238f24"),
                        "IsList",
                        null,
                        obj => obj.IsList,
                        (obj, val) => obj.IsList = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<ObjectReferencePlaceholderPropertyNHibernateImpl, string>(
                        lazyCtx,
                        new Guid("06d56d44-bc5f-428b-a6b5-4348573425f9"),
                        "ItemRoleName",
                        null,
                        obj => obj.ItemRoleName,
                        (obj, val) => obj.ItemRoleName = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<ObjectReferencePlaceholderPropertyNHibernateImpl, Kistl.App.Base.ObjectClass>(
                        lazyCtx,
                        new Guid("41da7ae6-aff7-44cf-83be-6150bf7578fd"),
                        "ReferencedObjectClass",
                        null,
                        obj => obj.ReferencedObjectClass,
                        (obj, val) => obj.ReferencedObjectClass = val),
                    // else
                    new PropertyDescriptorNHibernateImpl<ObjectReferencePlaceholderPropertyNHibernateImpl, string>(
                        lazyCtx,
                        new Guid("dd98c4f1-bf83-4d9a-8885-546457fc6591"),
                        "Verb",
                        null,
                        obj => obj.Verb,
                        (obj, val) => obj.Verb = val),
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
        [EventBasedMethod("OnToString_ObjectReferencePlaceholderProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectReferencePlaceholderProperty != null)
            {
                OnToString_ObjectReferencePlaceholderProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ObjectReferencePlaceholderProperty> OnToString_ObjectReferencePlaceholderProperty;

        [EventBasedMethod("OnPreSave_ObjectReferencePlaceholderProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectReferencePlaceholderProperty != null) OnPreSave_ObjectReferencePlaceholderProperty(this);
        }
        public static event ObjectEventHandler<ObjectReferencePlaceholderProperty> OnPreSave_ObjectReferencePlaceholderProperty;

        [EventBasedMethod("OnPostSave_ObjectReferencePlaceholderProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectReferencePlaceholderProperty != null) OnPostSave_ObjectReferencePlaceholderProperty(this);
        }
        public static event ObjectEventHandler<ObjectReferencePlaceholderProperty> OnPostSave_ObjectReferencePlaceholderProperty;

        [EventBasedMethod("OnCreated_ObjectReferencePlaceholderProperty")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_ObjectReferencePlaceholderProperty != null) OnCreated_ObjectReferencePlaceholderProperty(this);
        }
        public static event ObjectEventHandler<ObjectReferencePlaceholderProperty> OnCreated_ObjectReferencePlaceholderProperty;

        [EventBasedMethod("OnDeleting_ObjectReferencePlaceholderProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_ObjectReferencePlaceholderProperty != null) OnDeleting_ObjectReferencePlaceholderProperty(this);
        }
        public static event ObjectEventHandler<ObjectReferencePlaceholderProperty> OnDeleting_ObjectReferencePlaceholderProperty;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            // Follow ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass
            if (this.ReferencedObjectClass != null && this.ReferencedObjectClass.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.ReferencedObjectClass);

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            return result;
        }


        public class ObjectReferencePlaceholderPropertyProxy
            : Kistl.App.Base.PropertyNHibernateImpl.PropertyProxy
        {
            public ObjectReferencePlaceholderPropertyProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(ObjectReferencePlaceholderPropertyNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(ObjectReferencePlaceholderPropertyProxy); } }

            public virtual bool HasPersistentOrder { get; set; }

            public virtual string ImplementorRoleName { get; set; }

            public virtual bool IsList { get; set; }

            public virtual string ItemRoleName { get; set; }

            public virtual Kistl.App.Base.ObjectClassNHibernateImpl.ObjectClassProxy ReferencedObjectClass { get; set; }

            public virtual string Verb { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this.Proxy.HasPersistentOrder, binStream);
            BinarySerializer.ToStream(this.Proxy.ImplementorRoleName, binStream);
            BinarySerializer.ToStream(this.Proxy.IsList, binStream);
            BinarySerializer.ToStream(this.Proxy.ItemRoleName, binStream);
            BinarySerializer.ToStream(this.Proxy.ReferencedObjectClass != null ? this.Proxy.ReferencedObjectClass.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this.Proxy.Verb, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                bool tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.HasPersistentOrder = tmp;
            }
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.ImplementorRoleName = tmp;
            }
            {
                bool tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.IsList = tmp;
            }
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.ItemRoleName = tmp;
            }
            BinarySerializer.FromStream(out this._fk_ReferencedObjectClass, binStream);
            {
                string tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.Verb = tmp;
            }
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
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.ToStream(this.Proxy.HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.ImplementorRoleName, xml, "ImplementorRoleName", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.IsList, xml, "IsList", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.ItemRoleName, xml, "ItemRoleName", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.ReferencedObjectClass != null ? this.Proxy.ReferencedObjectClass.ID : (int?)null, xml, "ReferencedObjectClass", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.Verb, xml, "Verb", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                bool tmp = this.Proxy.HasPersistentOrder;
                XmlStreamer.FromStream(ref tmp, xml, "HasPersistentOrder", "Kistl.App.Base");
                this.Proxy.HasPersistentOrder = tmp;
            }
            {
                // yuck
                string tmp = this.Proxy.ImplementorRoleName;
                XmlStreamer.FromStream(ref tmp, xml, "ImplementorRoleName", "Kistl.App.Base");
                this.Proxy.ImplementorRoleName = tmp;
            }
            {
                // yuck
                bool tmp = this.Proxy.IsList;
                XmlStreamer.FromStream(ref tmp, xml, "IsList", "Kistl.App.Base");
                this.Proxy.IsList = tmp;
            }
            {
                // yuck
                string tmp = this.Proxy.ItemRoleName;
                XmlStreamer.FromStream(ref tmp, xml, "ItemRoleName", "Kistl.App.Base");
                this.Proxy.ItemRoleName = tmp;
            }
            XmlStreamer.FromStream(ref this._fk_ReferencedObjectClass, xml, "ReferencedObjectClass", "Kistl.App.Base");
            {
                // yuck
                string tmp = this.Proxy.Verb;
                XmlStreamer.FromStream(ref tmp, xml, "Verb", "Kistl.App.Base");
                this.Proxy.Verb = tmp;
            }
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            base.Export(xml, modules);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.ImplementorRoleName, xml, "ImplementorRoleName", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.IsList, xml, "IsList", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.ItemRoleName, xml, "ItemRoleName", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.ReferencedObjectClass != null ? this.Proxy.ReferencedObjectClass.ExportGuid : (Guid?)null, xml, "ReferencedObjectClass", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.Verb, xml, "Verb", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            {
                // yuck
                bool tmp = this.Proxy.HasPersistentOrder;
                XmlStreamer.FromStream(ref tmp, xml, "HasPersistentOrder", "Kistl.App.Base");
                this.Proxy.HasPersistentOrder = tmp;
            }
            {
                // yuck
                string tmp = this.Proxy.ImplementorRoleName;
                XmlStreamer.FromStream(ref tmp, xml, "ImplementorRoleName", "Kistl.App.Base");
                this.Proxy.ImplementorRoleName = tmp;
            }
            {
                // yuck
                bool tmp = this.Proxy.IsList;
                XmlStreamer.FromStream(ref tmp, xml, "IsList", "Kistl.App.Base");
                this.Proxy.IsList = tmp;
            }
            {
                // yuck
                string tmp = this.Proxy.ItemRoleName;
                XmlStreamer.FromStream(ref tmp, xml, "ItemRoleName", "Kistl.App.Base");
                this.Proxy.ItemRoleName = tmp;
            }
            XmlStreamer.FromStream(ref this._fk_guid_ReferencedObjectClass, xml, "ReferencedObjectClass", "Kistl.App.Base");
            {
                // yuck
                string tmp = this.Proxy.Verb;
                XmlStreamer.FromStream(ref tmp, xml, "Verb", "Kistl.App.Base");
                this.Proxy.Verb = tmp;
            }
        }

        #endregion

    }
}