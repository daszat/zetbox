// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// Metadefinition Object for Compound Object Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="CompoundObjectPropertyEfImpl")]
    [System.Diagnostics.DebuggerDisplay("CompoundObjectProperty")]
    public class CompoundObjectPropertyEfImpl : Zetbox.App.Base.PropertyEfImpl, CompoundObjectProperty
    {
        private static readonly Guid _objectClassID = new Guid("7b5ba73f-91f4-4542-9542-4f418b5c109f");
        public override Guid ObjectClassID { get { return _objectClassID; } }

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
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObjectDefinition
        // fkBackingName=_fk_CompoundObjectDefinition; fkGuidBackingName=_fk_guid_CompoundObjectDefinition;
        // referencedInterface=Zetbox.App.Base.CompoundObject; moduleNamespace=Zetbox.App.Base;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.CompoundObject CompoundObjectDefinition
        {
            get { return CompoundObjectDefinitionImpl; }
            set { CompoundObjectDefinitionImpl = (Zetbox.App.Base.CompoundObjectEfImpl)value; }
        }

        private int? _fk_CompoundObjectDefinition;

        /// <summary>ForeignKey Property for CompoundObjectDefinition's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_CompoundObjectDefinition
		{
			get { return CompoundObjectDefinition != null ? CompoundObjectDefinition.ID : (int?)null; }
			set { _fk_CompoundObjectDefinition = value; }
		}

        private Guid? _fk_guid_CompoundObjectDefinition = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_CompoundObjectProperty_has_CompoundObjectDefinition", "CompoundObjectDefinition")]
        public Zetbox.App.Base.CompoundObjectEfImpl CompoundObjectDefinitionImpl
        {
            get
            {
                Zetbox.App.Base.CompoundObjectEfImpl __value;
                EntityReference<Zetbox.App.Base.CompoundObjectEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.CompoundObjectEfImpl>(
                        "Model.FK_CompoundObjectProperty_has_CompoundObjectDefinition",
                        "CompoundObjectDefinition");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                __value = r.Value;
                if (OnCompoundObjectDefinition_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.CompoundObject>(__value);
                    OnCompoundObjectDefinition_Getter(this, e);
                    __value = (Zetbox.App.Base.CompoundObjectEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<Zetbox.App.Base.CompoundObjectEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Base.CompoundObjectEfImpl>(
                        "Model.FK_CompoundObjectProperty_has_CompoundObjectDefinition",
                        "CompoundObjectDefinition");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Zetbox.App.Base.CompoundObjectEfImpl __oldValue = (Zetbox.App.Base.CompoundObjectEfImpl)r.Value;
                Zetbox.App.Base.CompoundObjectEfImpl __newValue = (Zetbox.App.Base.CompoundObjectEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("CompoundObjectDefinition", __oldValue, __newValue);

                if (OnCompoundObjectDefinition_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObjectDefinition_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.CompoundObjectEfImpl)e.Result;
                }

                r.Value = (Zetbox.App.Base.CompoundObjectEfImpl)__newValue;

                if (OnCompoundObjectDefinition_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.CompoundObject>(__oldValue, __newValue);
                    OnCompoundObjectDefinition_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("CompoundObjectDefinition", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;
            }
        }

        public Zetbox.API.Async.ZbTask TriggerFetchCompoundObjectDefinitionAsync()
        {
            return new Zetbox.API.Async.ZbTask<Zetbox.App.Base.CompoundObject>(this.CompoundObjectDefinition);
        }

        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for CompoundObjectDefinition
		public static event PropertyGetterHandler<Zetbox.App.Base.CompoundObjectProperty, Zetbox.App.Base.CompoundObject> OnCompoundObjectDefinition_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CompoundObjectProperty, Zetbox.App.Base.CompoundObject> OnCompoundObjectDefinition_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CompoundObjectProperty, Zetbox.App.Base.CompoundObject> OnCompoundObjectDefinition_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CompoundObjectProperty> OnCompoundObjectDefinition_IsValid;

        /// <summary>
        /// Whether or not the list has a persistent ordering of elements
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public bool HasPersistentOrder
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _HasPersistentOrder;
                if (OnHasPersistentOrder_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnHasPersistentOrder_Getter(this, __e);
                    __result = _HasPersistentOrder = __e.Result;
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
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnHasPersistentOrder_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnHasPersistentOrder_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("HasPersistentOrder");
                }
            }
        }
        private bool _HasPersistentOrder_store;
        private bool _HasPersistentOrder {
            get { return _HasPersistentOrder_store; }
            set {
                ReportEfPropertyChanging("HasPersistentOrder");
                _HasPersistentOrder_store = value;
                ReportEfPropertyChanged("HasPersistentOrder");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.CompoundObjectProperty, bool> OnHasPersistentOrder_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CompoundObjectProperty, bool> OnHasPersistentOrder_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CompoundObjectProperty, bool> OnHasPersistentOrder_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CompoundObjectProperty> OnHasPersistentOrder_IsValid;

        /// <summary>
        /// Whether or not this CompoundObject property is list valued
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public bool IsList
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _IsList;
                if (OnIsList_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool>(__result);
                    OnIsList_Getter(this, __e);
                    __result = _IsList = __e.Result;
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
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnIsList_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool>(__oldValue, __newValue);
                        OnIsList_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("IsList");
                }
            }
        }
        private bool _IsList_store;
        private bool _IsList {
            get { return _IsList_store; }
            set {
                ReportEfPropertyChanging("IsList");
                _IsList_store = value;
                ReportEfPropertyChanged("IsList");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.CompoundObjectProperty, bool> OnIsList_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CompoundObjectProperty, bool> OnIsList_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CompoundObjectProperty, bool> OnIsList_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CompoundObjectProperty> OnIsList_IsValid;

        /// <summary>
        /// Returns the translated description
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDescription_CompoundObjectProperty")]
        public override string GetDescription()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDescription_CompoundObjectProperty != null)
            {
                OnGetDescription_CompoundObjectProperty(this, e);
            }
            else
            {
                e.Result = base.GetDescription();
            }
            return e.Result;
        }
        public static event GetDescription_Handler<CompoundObjectProperty> OnGetDescription_CompoundObjectProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectProperty> OnGetDescription_CompoundObjectProperty_CanExec;

        [EventBasedMethod("OnGetDescription_CompoundObjectProperty_CanExec")]
        public override bool GetDescriptionCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDescription_CompoundObjectProperty_CanExec != null)
				{
					OnGetDescription_CompoundObjectProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDescriptionCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectProperty> OnGetDescription_CompoundObjectProperty_CanExecReason;

        [EventBasedMethod("OnGetDescription_CompoundObjectProperty_CanExecReason")]
        public override string GetDescriptionCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDescription_CompoundObjectProperty_CanExecReason != null)
				{
					OnGetDescription_CompoundObjectProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetDescriptionCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// The element type for multi-valued properties. The property type string in all other cases.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetElementTypeString_CompoundObjectProperty")]
        public override string GetElementTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetElementTypeString_CompoundObjectProperty != null)
            {
                OnGetElementTypeString_CompoundObjectProperty(this, e);
            }
            else
            {
                e.Result = base.GetElementTypeString();
            }
            return e.Result;
        }
        public static event GetElementTypeString_Handler<CompoundObjectProperty> OnGetElementTypeString_CompoundObjectProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectProperty> OnGetElementTypeString_CompoundObjectProperty_CanExec;

        [EventBasedMethod("OnGetElementTypeString_CompoundObjectProperty_CanExec")]
        public override bool GetElementTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetElementTypeString_CompoundObjectProperty_CanExec != null)
				{
					OnGetElementTypeString_CompoundObjectProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectProperty> OnGetElementTypeString_CompoundObjectProperty_CanExecReason;

        [EventBasedMethod("OnGetElementTypeString_CompoundObjectProperty_CanExecReason")]
        public override string GetElementTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetElementTypeString_CompoundObjectProperty_CanExecReason != null)
				{
					OnGetElementTypeString_CompoundObjectProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
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
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectProperty> OnGetLabel_CompoundObjectProperty_CanExec;

        [EventBasedMethod("OnGetLabel_CompoundObjectProperty_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_CompoundObjectProperty_CanExec != null)
				{
					OnGetLabel_CompoundObjectProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectProperty> OnGetLabel_CompoundObjectProperty_CanExecReason;

        [EventBasedMethod("OnGetLabel_CompoundObjectProperty_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_CompoundObjectProperty_CanExecReason != null)
				{
					OnGetLabel_CompoundObjectProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetName_CompoundObjectProperty")]
        public override string GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_CompoundObjectProperty != null)
            {
                OnGetName_CompoundObjectProperty(this, e);
            }
            else
            {
                e.Result = base.GetName();
            }
            return e.Result;
        }
        public static event GetName_Handler<CompoundObjectProperty> OnGetName_CompoundObjectProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectProperty> OnGetName_CompoundObjectProperty_CanExec;

        [EventBasedMethod("OnGetName_CompoundObjectProperty_CanExec")]
        public override bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_CompoundObjectProperty_CanExec != null)
				{
					OnGetName_CompoundObjectProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectProperty> OnGetName_CompoundObjectProperty_CanExecReason;

        [EventBasedMethod("OnGetName_CompoundObjectProperty_CanExecReason")]
        public override string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_CompoundObjectProperty_CanExecReason != null)
				{
					OnGetName_CompoundObjectProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
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
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectProperty> OnGetPropertyType_CompoundObjectProperty_CanExec;

        [EventBasedMethod("OnGetPropertyType_CompoundObjectProperty_CanExec")]
        public override bool GetPropertyTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyType_CompoundObjectProperty_CanExec != null)
				{
					OnGetPropertyType_CompoundObjectProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectProperty> OnGetPropertyType_CompoundObjectProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyType_CompoundObjectProperty_CanExecReason")]
        public override string GetPropertyTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyType_CompoundObjectProperty_CanExecReason != null)
				{
					OnGetPropertyType_CompoundObjectProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
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
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObjectProperty> OnGetPropertyTypeString_CompoundObjectProperty_CanExec;

        [EventBasedMethod("OnGetPropertyTypeString_CompoundObjectProperty_CanExec")]
        public override bool GetPropertyTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyTypeString_CompoundObjectProperty_CanExec != null)
				{
					OnGetPropertyTypeString_CompoundObjectProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObjectProperty> OnGetPropertyTypeString_CompoundObjectProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyTypeString_CompoundObjectProperty_CanExecReason")]
        public override string GetPropertyTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyTypeString_CompoundObjectProperty_CanExecReason != null)
				{
					OnGetPropertyTypeString_CompoundObjectProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

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
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "CompoundObjectDefinition":
                case "HasPersistentOrder":
                case "IsList":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "CompoundObjectDefinition":
                return TriggerFetchCompoundObjectDefinitionAsync();
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_CompoundObjectDefinition.HasValue)
                CompoundObjectDefinitionImpl = (Zetbox.App.Base.CompoundObjectEfImpl)Context.FindPersistenceObject<Zetbox.App.Base.CompoundObject>(_fk_guid_CompoundObjectDefinition.Value);
            else
            if (_fk_CompoundObjectDefinition.HasValue)
                CompoundObjectDefinitionImpl = (Zetbox.App.Base.CompoundObjectEfImpl)Context.Find<Zetbox.App.Base.CompoundObject>(_fk_CompoundObjectDefinition.Value);
            else
                CompoundObjectDefinitionImpl = null;
            // fix cached lists references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
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
                    new PropertyDescriptorEfImpl<CompoundObjectProperty, Zetbox.App.Base.CompoundObject>(
                        lazyCtx,
                        new Guid("0d78c157-c106-4728-9af2-7992da7c935d"),
                        "CompoundObjectDefinition",
                        null,
                        obj => obj.CompoundObjectDefinition,
                        (obj, val) => obj.CompoundObjectDefinition = val,
						obj => OnCompoundObjectDefinition_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<CompoundObjectProperty, bool>(
                        lazyCtx,
                        new Guid("7c806f25-d85e-4d9f-b082-0cdaa7b60790"),
                        "HasPersistentOrder",
                        null,
                        obj => obj.HasPersistentOrder,
                        (obj, val) => obj.HasPersistentOrder = val,
						obj => OnHasPersistentOrder_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<CompoundObjectProperty, bool>(
                        lazyCtx,
                        new Guid("8a861113-a48a-40c5-bdec-6ceafef86f48"),
                        "IsList",
                        null,
                        obj => obj.IsList,
                        (obj, val) => obj.IsList = val,
						obj => OnIsList_IsValid), 
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
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

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

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_CompoundObjectProperty")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_CompoundObjectProperty != null)
            {
                OnObjectIsValid_CompoundObjectProperty(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<CompoundObjectProperty> OnObjectIsValid_CompoundObjectProperty;

        [EventBasedMethod("OnNotifyPreSave_CompoundObjectProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_CompoundObjectProperty != null) OnNotifyPreSave_CompoundObjectProperty(this);
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnNotifyPreSave_CompoundObjectProperty;

        [EventBasedMethod("OnNotifyPostSave_CompoundObjectProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_CompoundObjectProperty != null) OnNotifyPostSave_CompoundObjectProperty(this);
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnNotifyPostSave_CompoundObjectProperty;

        [EventBasedMethod("OnNotifyCreated_CompoundObjectProperty")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("CompoundObjectDefinition");
            SetNotInitializedProperty("HasPersistentOrder");
            SetNotInitializedProperty("IsList");
            base.NotifyCreated();
            if (OnNotifyCreated_CompoundObjectProperty != null) OnNotifyCreated_CompoundObjectProperty(this);
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnNotifyCreated_CompoundObjectProperty;

        [EventBasedMethod("OnNotifyDeleting_CompoundObjectProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_CompoundObjectProperty != null) OnNotifyDeleting_CompoundObjectProperty(this);
            CompoundObjectDefinition = null;
        }
        public static event ObjectEventHandler<CompoundObjectProperty> OnNotifyDeleting_CompoundObjectProperty;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var r = this.RelationshipManager.GetRelatedReference<Zetbox.App.Base.CompoundObjectEfImpl>("Model.FK_CompoundObjectProperty_has_CompoundObjectDefinition", "CompoundObjectDefinition");
                var key = r.EntityKey;
                binStream.Write(r.Value != null ? r.Value.ID : (key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null));
            }
            binStream.Write(this._HasPersistentOrder);
            binStream.Write(this._IsList);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_CompoundObjectDefinition);
            this._HasPersistentOrder = binStream.ReadBoolean();
            this._IsList = binStream.ReadBoolean();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(CompoundObjectDefinition != null ? CompoundObjectDefinition.ExportGuid : (Guid?)null, xml, "CompoundObjectDefinition", "Zetbox.App.Base");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._HasPersistentOrder, xml, "HasPersistentOrder", "Zetbox.App.Base");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._IsList, xml, "IsList", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|CompoundObjectDefinition":
                this._fk_guid_CompoundObjectDefinition = XmlStreamer.ReadNullableGuid(xml);
                break;
            case "Zetbox.App.Base|HasPersistentOrder":
                this._HasPersistentOrder = XmlStreamer.ReadBoolean(xml);
                break;
            case "Zetbox.App.Base|IsList":
                this._IsList = XmlStreamer.ReadBoolean(xml);
                break;
            }
        }

        #endregion

    }
}