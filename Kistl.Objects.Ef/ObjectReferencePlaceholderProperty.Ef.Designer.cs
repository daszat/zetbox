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

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// A placeholder for data object references in interfaces
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ObjectReferencePlaceholderProperty")]
    [System.Diagnostics.DebuggerDisplay("ObjectReferencePlaceholderProperty")]
    public class ObjectReferencePlaceholderPropertyEfImpl : Kistl.App.Base.PropertyEfImpl, ObjectReferencePlaceholderProperty
    {
        [Obsolete]
        public ObjectReferencePlaceholderPropertyEfImpl()
            : base(null)
        {
        }

        public ObjectReferencePlaceholderPropertyEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Whether or not the list has a persistent ordering of elements
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public bool HasPersistentOrder
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _HasPersistentOrder;
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
                if (_HasPersistentOrder != value)
                {
                    var __oldValue = _HasPersistentOrder;
                    var __newValue = value;
                    if (OnHasPersistentOrder_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnHasPersistentOrder_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("HasPersistentOrder", __oldValue, __newValue);
                    _HasPersistentOrder = __newValue;
                    NotifyPropertyChanged("HasPersistentOrder", __oldValue, __newValue);
                    if (OnHasPersistentOrder_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnHasPersistentOrder_PostSetter(this, __e);
                    }
                }
            }
        }
        private bool _HasPersistentOrder;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnHasPersistentOrder_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnHasPersistentOrder_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnHasPersistentOrder_PostSetter;

        /// <summary>
        /// Suggested implementors role name. If empty, class name will be used
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string ImplementorRoleName
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ImplementorRoleName;
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
                if (_ImplementorRoleName != value)
                {
                    var __oldValue = _ImplementorRoleName;
                    var __newValue = value;
                    if (OnImplementorRoleName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnImplementorRoleName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ImplementorRoleName", __oldValue, __newValue);
                    _ImplementorRoleName = __newValue;
                    NotifyPropertyChanged("ImplementorRoleName", __oldValue, __newValue);
                    if (OnImplementorRoleName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnImplementorRoleName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _ImplementorRoleName;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnImplementorRoleName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnImplementorRoleName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnImplementorRoleName_PostSetter;

        /// <summary>
        /// Whether or not this property placeholder is list valued
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public bool IsList
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(bool);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _IsList;
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
                if (_IsList != value)
                {
                    var __oldValue = _IsList;
                    var __newValue = value;
                    if (OnIsList_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsList_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsList", __oldValue, __newValue);
                    _IsList = __newValue;
                    NotifyPropertyChanged("IsList", __oldValue, __newValue);
                    if (OnIsList_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsList_PostSetter(this, __e);
                    }
                }
            }
        }
        private bool _IsList;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnIsList_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnIsList_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, bool> OnIsList_PostSetter;

        /// <summary>
        /// Suggested role name for the referenced item
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string ItemRoleName
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ItemRoleName;
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
                if (_ItemRoleName != value)
                {
                    var __oldValue = _ItemRoleName;
                    var __newValue = value;
                    if (OnItemRoleName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnItemRoleName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ItemRoleName", __oldValue, __newValue);
                    _ItemRoleName = __newValue;
                    NotifyPropertyChanged("ItemRoleName", __oldValue, __newValue);
                    if (OnItemRoleName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnItemRoleName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _ItemRoleName;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnItemRoleName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnItemRoleName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, string> OnItemRoleName_PostSetter;

        /// <summary>
        /// The ObjectClass that is referenced by this placeholder
        /// </summary>
    /*
    Relation: FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass
    A: ZeroOrMore ObjectReferencePlaceholderProperty as ObjectReferencePlaceholderProperty
    B: One ObjectClass as ReferencedObjectClass
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ReferencedObjectClass
        // fkBackingName=_fk_ReferencedObjectClass; fkGuidBackingName=_fk_guid_ReferencedObjectClass;
        // referencedInterface=Kistl.App.Base.ObjectClass; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass ReferencedObjectClass
        {
            get { return ReferencedObjectClassImpl; }
            set { ReferencedObjectClassImpl = (Kistl.App.Base.ObjectClassEfImpl)value; }
        }

        private int? _fk_ReferencedObjectClass;

        private Guid? _fk_guid_ReferencedObjectClass = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass", "ReferencedObjectClass")]
        public Kistl.App.Base.ObjectClassEfImpl ReferencedObjectClassImpl
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return null;
                Kistl.App.Base.ObjectClassEfImpl __value;
                EntityReference<Kistl.App.Base.ObjectClassEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClassEfImpl>(
                        "Model.FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass",
                        "ReferencedObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnReferencedObjectClass_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.ObjectClass>(__value);
                    OnReferencedObjectClass_Getter(this, e);
                    __value = (Kistl.App.Base.ObjectClassEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Base.ObjectClassEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClassEfImpl>(
                        "Model.FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass",
                        "ReferencedObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Base.ObjectClassEfImpl __oldValue = (Kistl.App.Base.ObjectClassEfImpl)r.Value;
                Kistl.App.Base.ObjectClassEfImpl __newValue = (Kistl.App.Base.ObjectClassEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("ReferencedObjectClass", null, __oldValue, __newValue);

                if (OnReferencedObjectClass_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnReferencedObjectClass_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.ObjectClassEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Base.ObjectClassEfImpl)__newValue;

                if (OnReferencedObjectClass_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnReferencedObjectClass_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("ReferencedObjectClass", null, __oldValue, __newValue);
            }
        }

        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ReferencedObjectClass
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, Kistl.App.Base.ObjectClass> OnReferencedObjectClass_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, Kistl.App.Base.ObjectClass> OnReferencedObjectClass_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferencePlaceholderProperty, Kistl.App.Base.ObjectClass> OnReferencedObjectClass_PostSetter;

        /// <summary>
        /// Suggested verb for the new relation
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Verb
        {
            get
            {
                if (CurrentAccessRights == Kistl.API.AccessRights.None) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Verb;
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
                if (_Verb != value)
                {
                    var __oldValue = _Verb;
                    var __newValue = value;
                    if (OnVerb_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnVerb_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Verb", __oldValue, __newValue);
                    _Verb = __newValue;
                    NotifyPropertyChanged("Verb", __oldValue, __newValue);
                    if (OnVerb_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnVerb_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Verb;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
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
            var otherImpl = (ObjectReferencePlaceholderPropertyEfImpl)obj;
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
        }

		public override void UpdateParent(string propertyName, int? id)
		{
			int? __oldValue, __newValue = id;
			
			switch(propertyName)
			{
                case "ReferencedObjectClass":
                    __oldValue = _fk_ReferencedObjectClass;
                    NotifyPropertyChanging("ReferencedObjectClass", __oldValue, __newValue);
                    _fk_ReferencedObjectClass = __newValue;
                    NotifyPropertyChanged("ReferencedObjectClass", __oldValue, __newValue);
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
                ReferencedObjectClassImpl = (Kistl.App.Base.ObjectClassEfImpl)Context.FindPersistenceObject<Kistl.App.Base.ObjectClass>(_fk_guid_ReferencedObjectClass.Value);
            else
            if (_fk_ReferencedObjectClass.HasValue)
                ReferencedObjectClassImpl = (Kistl.App.Base.ObjectClassEfImpl)Context.Find<Kistl.App.Base.ObjectClass>(_fk_ReferencedObjectClass.Value);
            else
                ReferencedObjectClassImpl = null;
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
                    new PropertyDescriptorEfImpl<ObjectReferencePlaceholderPropertyEfImpl, bool>(
                        lazyCtx,
                        new Guid("7e52aa2a-aa3a-4f5b-8171-c6c2f364108b"),
                        "HasPersistentOrder",
                        null,
                        obj => obj.HasPersistentOrder,
                        (obj, val) => obj.HasPersistentOrder = val),
                    // else
                    new PropertyDescriptorEfImpl<ObjectReferencePlaceholderPropertyEfImpl, string>(
                        lazyCtx,
                        new Guid("b5fa31d8-ad30-4aeb-b5a0-8b4b117b1d29"),
                        "ImplementorRoleName",
                        null,
                        obj => obj.ImplementorRoleName,
                        (obj, val) => obj.ImplementorRoleName = val),
                    // else
                    new PropertyDescriptorEfImpl<ObjectReferencePlaceholderPropertyEfImpl, bool>(
                        lazyCtx,
                        new Guid("52692870-0bd4-47b6-99dc-eb8bf4238f24"),
                        "IsList",
                        null,
                        obj => obj.IsList,
                        (obj, val) => obj.IsList = val),
                    // else
                    new PropertyDescriptorEfImpl<ObjectReferencePlaceholderPropertyEfImpl, string>(
                        lazyCtx,
                        new Guid("06d56d44-bc5f-428b-a6b5-4348573425f9"),
                        "ItemRoleName",
                        null,
                        obj => obj.ItemRoleName,
                        (obj, val) => obj.ItemRoleName = val),
                    // else
                    new PropertyDescriptorEfImpl<ObjectReferencePlaceholderPropertyEfImpl, Kistl.App.Base.ObjectClass>(
                        lazyCtx,
                        new Guid("41da7ae6-aff7-44cf-83be-6150bf7578fd"),
                        "ReferencedObjectClass",
                        null,
                        obj => obj.ReferencedObjectClass,
                        (obj, val) => obj.ReferencedObjectClass = val),
                    // else
                    new PropertyDescriptorEfImpl<ObjectReferencePlaceholderPropertyEfImpl, string>(
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
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

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

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            BinarySerializer.ToStream(this._HasPersistentOrder, binStream);
            BinarySerializer.ToStream(this._ImplementorRoleName, binStream);
            BinarySerializer.ToStream(this._IsList, binStream);
            BinarySerializer.ToStream(this._ItemRoleName, binStream);
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClassEfImpl>("Model.FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass", "ReferencedObjectClass").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
			if (auxObjects != null) {
				auxObjects.Add(ReferencedObjectClass);
			}
            BinarySerializer.ToStream(this._Verb, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._HasPersistentOrder, binStream);
            BinarySerializer.FromStream(out this._ImplementorRoleName, binStream);
            BinarySerializer.FromStream(out this._IsList, binStream);
            BinarySerializer.FromStream(out this._ItemRoleName, binStream);
            BinarySerializer.FromStream(out this._fk_ReferencedObjectClass, binStream);
            BinarySerializer.FromStream(out this._Verb, binStream);
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
            XmlStreamer.ToStream(this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ImplementorRoleName, xml, "ImplementorRoleName", "Kistl.App.Base");
            XmlStreamer.ToStream(this._IsList, xml, "IsList", "Kistl.App.Base");
            XmlStreamer.ToStream(this._ItemRoleName, xml, "ItemRoleName", "Kistl.App.Base");
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClassEfImpl>("Model.FK_ObjectReferencePlaceholderProperty_ofType_ReferencedObjectClass", "ReferencedObjectClass").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "ReferencedObjectClass", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._Verb, xml, "Verb", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ImplementorRoleName, xml, "ImplementorRoleName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsList, xml, "IsList", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ItemRoleName, xml, "ItemRoleName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_ReferencedObjectClass, xml, "ReferencedObjectClass", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Verb, xml, "Verb", "Kistl.App.Base");
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ImplementorRoleName, xml, "ImplementorRoleName", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._IsList, xml, "IsList", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._ItemRoleName, xml, "ItemRoleName", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(ReferencedObjectClass != null ? ReferencedObjectClass.ExportGuid : (Guid?)null, xml, "ReferencedObjectClass", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Verb, xml, "Verb", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
            XmlStreamer.FromStream(ref this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ImplementorRoleName, xml, "ImplementorRoleName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsList, xml, "IsList", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._ItemRoleName, xml, "ItemRoleName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_ReferencedObjectClass, xml, "ReferencedObjectClass", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Verb, xml, "Verb", "Kistl.App.Base");
        }

        #endregion

    }
}