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
    /// Metadefinition Object for Enumeration Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="EnumerationProperty")]
    [System.Diagnostics.DebuggerDisplay("EnumerationProperty")]
    public class EnumerationPropertyEfImpl : Kistl.App.Base.ValueTypePropertyEfImpl, EnumerationProperty
    {
        private static readonly Guid _objectClassID = new Guid("19a8d9f0-4de6-4cc9-a75d-c1499e3a103b");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public EnumerationPropertyEfImpl()
            : base(null)
        {
        }

        public EnumerationPropertyEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Enumeration der Eigenschaft
        /// </summary>
    /*
    Relation: FK_EnumerationProperty_has_Enumeration
    A: ZeroOrMore EnumerationProperty as EnumerationProperty
    B: One Enumeration as Enumeration
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Enumeration
        // fkBackingName=_fk_Enumeration; fkGuidBackingName=_fk_guid_Enumeration;
        // referencedInterface=Kistl.App.Base.Enumeration; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Enumeration Enumeration
        {
            get { return EnumerationImpl; }
            set { EnumerationImpl = (Kistl.App.Base.EnumerationEfImpl)value; }
        }

        private int? _fk_Enumeration;

        private Guid? _fk_guid_Enumeration = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_EnumerationProperty_has_Enumeration", "Enumeration")]
        public Kistl.App.Base.EnumerationEfImpl EnumerationImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.EnumerationEfImpl __value;
                EntityReference<Kistl.App.Base.EnumerationEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.EnumerationEfImpl>(
                        "Model.FK_EnumerationProperty_has_Enumeration",
                        "Enumeration");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnEnumeration_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.Enumeration>(__value);
                    OnEnumeration_Getter(this, e);
                    __value = (Kistl.App.Base.EnumerationEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Base.EnumerationEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.EnumerationEfImpl>(
                        "Model.FK_EnumerationProperty_has_Enumeration",
                        "Enumeration");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Base.EnumerationEfImpl __oldValue = (Kistl.App.Base.EnumerationEfImpl)r.Value;
                Kistl.App.Base.EnumerationEfImpl __newValue = (Kistl.App.Base.EnumerationEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("Enumeration", null, __oldValue, __newValue);

                if (OnEnumeration_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.Enumeration>(__oldValue, __newValue);
                    OnEnumeration_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.EnumerationEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Base.EnumerationEfImpl)__newValue;

                if (OnEnumeration_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.Enumeration>(__oldValue, __newValue);
                    OnEnumeration_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("Enumeration", null, __oldValue, __newValue);
            }
        }

        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Enumeration
		public static event PropertyGetterHandler<Kistl.App.Base.EnumerationProperty, Kistl.App.Base.Enumeration> OnEnumeration_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.EnumerationProperty, Kistl.App.Base.Enumeration> OnEnumeration_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.EnumerationProperty, Kistl.App.Base.Enumeration> OnEnumeration_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.EnumerationProperty> OnEnumeration_IsValid;

        /// <summary>
        /// The element type for multi-valued properties. The property type string in all other cases.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetElementTypeString_EnumerationProperty")]
        public override string GetElementTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetElementTypeString_EnumerationProperty != null)
            {
                OnGetElementTypeString_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetElementTypeString();
            }
            return e.Result;
        }
        public static event GetElementTypeString_Handler<EnumerationProperty> OnGetElementTypeString_EnumerationProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumerationProperty> OnGetElementTypeString_EnumerationProperty_CanExec;

        [EventBasedMethod("OnGetElementTypeString_EnumerationProperty_CanExec")]
        public override bool GetElementTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetElementTypeString_EnumerationProperty_CanExec != null)
				{
					OnGetElementTypeString_EnumerationProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumerationProperty> OnGetElementTypeString_EnumerationProperty_CanExecReason;

        [EventBasedMethod("OnGetElementTypeString_EnumerationProperty_CanExecReason")]
        public override string GetElementTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetElementTypeString_EnumerationProperty_CanExecReason != null)
				{
					OnGetElementTypeString_EnumerationProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_EnumerationProperty")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_EnumerationProperty != null)
            {
                OnGetLabel_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<EnumerationProperty> OnGetLabel_EnumerationProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumerationProperty> OnGetLabel_EnumerationProperty_CanExec;

        [EventBasedMethod("OnGetLabel_EnumerationProperty_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_EnumerationProperty_CanExec != null)
				{
					OnGetLabel_EnumerationProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumerationProperty> OnGetLabel_EnumerationProperty_CanExecReason;

        [EventBasedMethod("OnGetLabel_EnumerationProperty_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_EnumerationProperty_CanExecReason != null)
				{
					OnGetLabel_EnumerationProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetName_EnumerationProperty")]
        public override string GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_EnumerationProperty != null)
            {
                OnGetName_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetName();
            }
            return e.Result;
        }
        public static event GetName_Handler<EnumerationProperty> OnGetName_EnumerationProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumerationProperty> OnGetName_EnumerationProperty_CanExec;

        [EventBasedMethod("OnGetName_EnumerationProperty_CanExec")]
        public override bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_EnumerationProperty_CanExec != null)
				{
					OnGetName_EnumerationProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumerationProperty> OnGetName_EnumerationProperty_CanExecReason;

        [EventBasedMethod("OnGetName_EnumerationProperty_CanExecReason")]
        public override string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_EnumerationProperty_CanExecReason != null)
				{
					OnGetName_EnumerationProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyType_EnumerationProperty")]
        public override System.Type GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_EnumerationProperty != null)
            {
                OnGetPropertyType_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<EnumerationProperty> OnGetPropertyType_EnumerationProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumerationProperty> OnGetPropertyType_EnumerationProperty_CanExec;

        [EventBasedMethod("OnGetPropertyType_EnumerationProperty_CanExec")]
        public override bool GetPropertyTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyType_EnumerationProperty_CanExec != null)
				{
					OnGetPropertyType_EnumerationProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumerationProperty> OnGetPropertyType_EnumerationProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyType_EnumerationProperty_CanExecReason")]
        public override string GetPropertyTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyType_EnumerationProperty_CanExecReason != null)
				{
					OnGetPropertyType_EnumerationProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyTypeString_EnumerationProperty")]
        public override string GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_EnumerationProperty != null)
            {
                OnGetPropertyTypeString_EnumerationProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<EnumerationProperty> OnGetPropertyTypeString_EnumerationProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumerationProperty> OnGetPropertyTypeString_EnumerationProperty_CanExec;

        [EventBasedMethod("OnGetPropertyTypeString_EnumerationProperty_CanExec")]
        public override bool GetPropertyTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyTypeString_EnumerationProperty_CanExec != null)
				{
					OnGetPropertyTypeString_EnumerationProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumerationProperty> OnGetPropertyTypeString_EnumerationProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyTypeString_EnumerationProperty_CanExecReason")]
        public override string GetPropertyTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyTypeString_EnumerationProperty_CanExecReason != null)
				{
					OnGetPropertyTypeString_EnumerationProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(EnumerationProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (EnumerationProperty)obj;
            var otherImpl = (EnumerationPropertyEfImpl)obj;
            var me = (EnumerationProperty)this;

            this._fk_Enumeration = otherImpl._fk_Enumeration;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Enumeration":
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

            if (_fk_guid_Enumeration.HasValue)
                EnumerationImpl = (Kistl.App.Base.EnumerationEfImpl)Context.FindPersistenceObject<Kistl.App.Base.Enumeration>(_fk_guid_Enumeration.Value);
            else
            if (_fk_Enumeration.HasValue)
                EnumerationImpl = (Kistl.App.Base.EnumerationEfImpl)Context.Find<Kistl.App.Base.Enumeration>(_fk_Enumeration.Value);
            else
                EnumerationImpl = null;
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
                    new PropertyDescriptorEfImpl<EnumerationProperty, Kistl.App.Base.Enumeration>(
                        lazyCtx,
                        new Guid("1144c061-3610-495f-b8b4-951058bb0c23"),
                        "Enumeration",
                        null,
                        obj => obj.Enumeration,
                        (obj, val) => obj.Enumeration = val,
						obj => OnEnumeration_IsValid), 
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
        [EventBasedMethod("OnToString_EnumerationProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EnumerationProperty != null)
            {
                OnToString_EnumerationProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<EnumerationProperty> OnToString_EnumerationProperty;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_EnumerationProperty")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_EnumerationProperty != null)
            {
                OnObjectIsValid_EnumerationProperty(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<EnumerationProperty> OnObjectIsValid_EnumerationProperty;

        [EventBasedMethod("OnNotifyPreSave_EnumerationProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_EnumerationProperty != null) OnNotifyPreSave_EnumerationProperty(this);
        }
        public static event ObjectEventHandler<EnumerationProperty> OnNotifyPreSave_EnumerationProperty;

        [EventBasedMethod("OnNotifyPostSave_EnumerationProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_EnumerationProperty != null) OnNotifyPostSave_EnumerationProperty(this);
        }
        public static event ObjectEventHandler<EnumerationProperty> OnNotifyPostSave_EnumerationProperty;

        [EventBasedMethod("OnNotifyCreated_EnumerationProperty")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Enumeration");
            base.NotifyCreated();
            if (OnNotifyCreated_EnumerationProperty != null) OnNotifyCreated_EnumerationProperty(this);
        }
        public static event ObjectEventHandler<EnumerationProperty> OnNotifyCreated_EnumerationProperty;

        [EventBasedMethod("OnNotifyDeleting_EnumerationProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_EnumerationProperty != null) OnNotifyDeleting_EnumerationProperty(this);
        }
        public static event ObjectEventHandler<EnumerationProperty> OnNotifyDeleting_EnumerationProperty;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.EnumerationEfImpl>("Model.FK_EnumerationProperty_has_Enumeration", "Enumeration").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_Enumeration, binStream);
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
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.EnumerationEfImpl>("Model.FK_EnumerationProperty_has_Enumeration", "Enumeration").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "Enumeration", "Kistl.App.Base");
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_Enumeration, xml, "Enumeration", "Kistl.App.Base");
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
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Enumeration != null ? Enumeration.ExportGuid : (Guid?)null, xml, "Enumeration", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.FromStream(ref this._fk_guid_Enumeration, xml, "Enumeration", "Kistl.App.Base");
        }

        #endregion

    }
}