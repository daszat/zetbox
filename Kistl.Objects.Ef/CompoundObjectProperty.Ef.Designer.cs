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
    /// Metadefinition Object for Compound Object Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="CompoundObjectProperty")]
    [System.Diagnostics.DebuggerDisplay("CompoundObjectProperty")]
    public class CompoundObjectPropertyEfImpl : Kistl.App.Base.PropertyEfImpl, CompoundObjectProperty
    {
        [Obsolete]
        public CompoundObjectPropertyEfImpl()
            : base(null)
        {
        }

        public CompoundObjectPropertyEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Definition of this Compound Object
        /// </summary>
    /*
    Relation: FK_CompoundObjectProperty_has_CompoundObjectDefinition
    A: ZeroOrMore CompoundObjectProperty as CompoundObjectProperty
    B: One CompoundObject as CompoundObjectDefinition
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObjectDefinition
        // fkBackingName=_fk_CompoundObjectDefinition; fkGuidBackingName=_fk_guid_CompoundObjectDefinition;
        // referencedInterface=Kistl.App.Base.CompoundObject; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.CompoundObject CompoundObjectDefinition
        {
            get { return CompoundObjectDefinitionImpl; }
            set { CompoundObjectDefinitionImpl = (Kistl.App.Base.CompoundObjectEfImpl)value; }
        }

        private int? _fk_CompoundObjectDefinition;

        private Guid? _fk_guid_CompoundObjectDefinition = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_CompoundObjectProperty_has_CompoundObjectDefinition", "CompoundObjectDefinition")]
        public Kistl.App.Base.CompoundObjectEfImpl CompoundObjectDefinitionImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.CompoundObjectEfImpl __value;
                EntityReference<Kistl.App.Base.CompoundObjectEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.CompoundObjectEfImpl>(
                        "Model.FK_CompoundObjectProperty_has_CompoundObjectDefinition",
                        "CompoundObjectDefinition");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnCompoundObjectDefinition_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.CompoundObject>(__value);
                    OnCompoundObjectDefinition_Getter(this, e);
                    __value = (Kistl.App.Base.CompoundObjectEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Base.CompoundObjectEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.CompoundObjectEfImpl>(
                        "Model.FK_CompoundObjectProperty_has_CompoundObjectDefinition",
                        "CompoundObjectDefinition");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Base.CompoundObjectEfImpl __oldValue = (Kistl.App.Base.CompoundObjectEfImpl)r.Value;
                Kistl.App.Base.CompoundObjectEfImpl __newValue = (Kistl.App.Base.CompoundObjectEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("CompoundObjectDefinition", null, __oldValue, __newValue);

                if (OnCompoundObjectDefinition_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObjectDefinition_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.CompoundObjectEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Base.CompoundObjectEfImpl)__newValue;

                if (OnCompoundObjectDefinition_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObjectDefinition_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("CompoundObjectDefinition", null, __oldValue, __newValue);
            }
        }

        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObjectDefinition
		public static event PropertyGetterHandler<Kistl.App.Base.CompoundObjectProperty, Kistl.App.Base.CompoundObject> OnCompoundObjectDefinition_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.CompoundObjectProperty, Kistl.App.Base.CompoundObject> OnCompoundObjectDefinition_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.CompoundObjectProperty, Kistl.App.Base.CompoundObject> OnCompoundObjectDefinition_PostSetter;

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
                if (!CurrentAccessRights.HasReadRights()) return default(bool);
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
		public static event PropertyGetterHandler<Kistl.App.Base.CompoundObjectProperty, bool> OnHasPersistentOrder_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.CompoundObjectProperty, bool> OnHasPersistentOrder_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.CompoundObjectProperty, bool> OnHasPersistentOrder_PostSetter;

        /// <summary>
        /// Whether or not this CompoundObject property is list valued
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
                if (!CurrentAccessRights.HasReadRights()) return default(bool);
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
		public static event PropertyGetterHandler<Kistl.App.Base.CompoundObjectProperty, bool> OnIsList_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.CompoundObjectProperty, bool> OnIsList_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.CompoundObjectProperty, bool> OnIsList_PostSetter;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_CompoundObjectProperty")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_CompoundObjectProperty != null)
            {
                OnGetLabel_CompoundObjectProperty(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<CompoundObjectProperty> OnGetLabel_CompoundObjectProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyType_CompoundObjectProperty")]
        public override System.Type GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_CompoundObjectProperty != null)
            {
                OnGetPropertyType_CompoundObjectProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<CompoundObjectProperty> OnGetPropertyType_CompoundObjectProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyTypeString_CompoundObjectProperty")]
        public override string GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_CompoundObjectProperty != null)
            {
                OnGetPropertyTypeString_CompoundObjectProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<CompoundObjectProperty> OnGetPropertyTypeString_CompoundObjectProperty;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(CompoundObjectProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (CompoundObjectProperty)obj;
            var otherImpl = (CompoundObjectPropertyEfImpl)obj;
            var me = (CompoundObjectProperty)this;

            me.HasPersistentOrder = other.HasPersistentOrder;
            me.IsList = other.IsList;
            this._fk_CompoundObjectDefinition = otherImpl._fk_CompoundObjectDefinition;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_CompoundObjectDefinition.HasValue)
                CompoundObjectDefinitionImpl = (Kistl.App.Base.CompoundObjectEfImpl)Context.FindPersistenceObject<Kistl.App.Base.CompoundObject>(_fk_guid_CompoundObjectDefinition.Value);
            else
            if (_fk_CompoundObjectDefinition.HasValue)
                CompoundObjectDefinitionImpl = (Kistl.App.Base.CompoundObjectEfImpl)Context.Find<Kistl.App.Base.CompoundObject>(_fk_CompoundObjectDefinition.Value);
            else
                CompoundObjectDefinitionImpl = null;
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
                    new PropertyDescriptorEfImpl<CompoundObjectPropertyEfImpl, Kistl.App.Base.CompoundObject>(
                        lazyCtx,
                        new Guid("0d78c157-c106-4728-9af2-7992da7c935d"),
                        "CompoundObjectDefinition",
                        null,
                        obj => obj.CompoundObjectDefinition,
                        (obj, val) => obj.CompoundObjectDefinition = val),
                    // else
                    new PropertyDescriptorEfImpl<CompoundObjectPropertyEfImpl, bool>(
                        lazyCtx,
                        new Guid("7c806f25-d85e-4d9f-b082-0cdaa7b60790"),
                        "HasPersistentOrder",
                        null,
                        obj => obj.HasPersistentOrder,
                        (obj, val) => obj.HasPersistentOrder = val),
                    // else
                    new PropertyDescriptorEfImpl<CompoundObjectPropertyEfImpl, bool>(
                        lazyCtx,
                        new Guid("8a861113-a48a-40c5-bdec-6ceafef86f48"),
                        "IsList",
                        null,
                        obj => obj.IsList,
                        (obj, val) => obj.IsList = val),
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
        [EventBasedMethod("OnToString_CompoundObjectProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CompoundObjectProperty != null)
            {
                OnToString_CompoundObjectProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<CompoundObjectProperty> OnToString_CompoundObjectProperty;

        [EventBasedMethod("OnPreSave_CompoundObjectProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_CompoundObjectProperty != null) OnPreSave_CompoundObjectProperty(this);
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnPreSave_CompoundObjectProperty;

        [EventBasedMethod("OnPostSave_CompoundObjectProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_CompoundObjectProperty != null) OnPostSave_CompoundObjectProperty(this);
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnPostSave_CompoundObjectProperty;

        [EventBasedMethod("OnCreated_CompoundObjectProperty")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_CompoundObjectProperty != null) OnCreated_CompoundObjectProperty(this);
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnCreated_CompoundObjectProperty;

        [EventBasedMethod("OnDeleting_CompoundObjectProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_CompoundObjectProperty != null) OnDeleting_CompoundObjectProperty(this);
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnDeleting_CompoundObjectProperty;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.CompoundObjectEfImpl>("Model.FK_CompoundObjectProperty_has_CompoundObjectDefinition", "CompoundObjectDefinition").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
            BinarySerializer.ToStream(this._HasPersistentOrder, binStream);
            BinarySerializer.ToStream(this._IsList, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_CompoundObjectDefinition, binStream);
            BinarySerializer.FromStream(out this._HasPersistentOrder, binStream);
            BinarySerializer.FromStream(out this._IsList, binStream);
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
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.CompoundObjectEfImpl>("Model.FK_CompoundObjectProperty_has_CompoundObjectDefinition", "CompoundObjectDefinition").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "CompoundObjectDefinition", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.ToStream(this._IsList, xml, "IsList", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_CompoundObjectDefinition, xml, "CompoundObjectDefinition", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsList, xml, "IsList", "Kistl.App.Base");
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
            if (!CurrentAccessRights.HasReadRights()) return;
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(CompoundObjectDefinition != null ? CompoundObjectDefinition.ExportGuid : (Guid?)null, xml, "CompoundObjectDefinition", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._IsList, xml, "IsList", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.FromStream(ref this._fk_guid_CompoundObjectDefinition, xml, "CompoundObjectDefinition", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._HasPersistentOrder, xml, "HasPersistentOrder", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsList, xml, "IsList", "Kistl.App.Base");
        }

        #endregion

    }
}