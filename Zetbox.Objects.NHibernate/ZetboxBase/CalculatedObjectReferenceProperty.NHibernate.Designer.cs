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

    using Zetbox.API.Utils;
    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.NHibernate;

    /// <summary>
    /// a object reference that is calculated from the contents of the containing class
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CalculatedObjectReferenceProperty")]
    public class CalculatedObjectReferencePropertyNHibernateImpl : Zetbox.App.Base.PropertyNHibernateImpl, CalculatedObjectReferenceProperty
    {
        private static readonly Guid _objectClassID = new Guid("8708c578-6e55-4349-ba24-ede46ca6f585");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public CalculatedObjectReferencePropertyNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public CalculatedObjectReferencePropertyNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new CalculatedObjectReferencePropertyProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public CalculatedObjectReferencePropertyNHibernateImpl(Func<IFrozenContext> lazyCtx, CalculatedObjectReferencePropertyProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly CalculatedObjectReferencePropertyProxy Proxy;

        /// <summary>
        /// The properties on which the calculation depends. This is used to propagate change notifications.
        /// </summary>
        // collection entry list property
   		// Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.Base.Property> Inputs
		{
			get
			{
				if (_Inputs == null)
				{
					_Inputs 
						= new NHibernateBSideCollectionWrapper<Zetbox.App.Base.CalculatedObjectReferenceProperty, Zetbox.App.Base.Property, Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl>(
							this, 
							new ProjectedCollection<Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl.CalculatedReference_dependsOn_InputProperties_RelationEntryProxy, Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl>(
                                () => this.Proxy.Inputs,
                                p => (Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl)OurContext.AttachAndWrap(p),
                                ce => (Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl.CalculatedReference_dependsOn_InputProperties_RelationEntryProxy)((NHibernatePersistenceObject)ce).NHibernateProxy));
                    _Inputs.CollectionChanged += (s, e) => { this.NotifyPropertyChanged("Inputs", null, null); if(OnInputs_PostSetter != null && IsAttached) OnInputs_PostSetter(this); };
                    if (Inputs_was_eagerLoaded) { Inputs_was_eagerLoaded = false; }
				}
				return (ICollection<Zetbox.App.Base.Property>)_Inputs;
			}
		}

        public async System.Threading.Tasks.Task<ICollection<Zetbox.App.Base.Property>> GetProp_Inputs()
        {
            await TriggerFetchInputsAsync();
            return _Inputs;
        }

		private NHibernateBSideCollectionWrapper<Zetbox.App.Base.CalculatedObjectReferenceProperty, Zetbox.App.Base.Property, Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl> _Inputs;
		// ignored, but required for Serialization
        private bool Inputs_was_eagerLoaded = false;

        public System.Threading.Tasks.Task TriggerFetchInputsAsync()
        {
            return System.Threading.Tasks.Task.FromResult<ICollection<Zetbox.App.Base.Property>>(this.Inputs);
        }

public static event PropertyListChangedHandler<Zetbox.App.Base.CalculatedObjectReferenceProperty> OnInputs_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CalculatedObjectReferenceProperty> OnInputs_IsValid;

        /// <summary>
        /// the referenced class of objects
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ReferencedClass
        // fkBackingName=this.Proxy.ReferencedClass; fkGuidBackingName=_fk_guid_ReferencedClass;
        // referencedInterface=Zetbox.App.Base.ObjectClass; moduleNamespace=Zetbox.App.Base;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target exportable; does call events
        
        public System.Threading.Tasks.Task<Zetbox.App.Base.ObjectClass> GetProp_ReferencedClass()
        {
            return System.Threading.Tasks.Task.FromResult(ReferencedClass);
        }

        public async System.Threading.Tasks.Task SetProp_ReferencedClass(Zetbox.App.Base.ObjectClass newValue)
        {
            await TriggerFetchReferencedClassAsync();
            ReferencedClass = newValue;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		[System.Runtime.Serialization.IgnoreDataMember]
        public Zetbox.App.Base.ObjectClass ReferencedClass
        {
            get
            {
                Zetbox.App.Base.ObjectClassNHibernateImpl __value = (Zetbox.App.Base.ObjectClassNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ReferencedClass);

                if (OnReferencedClass_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.ObjectClass>(__value);
                    OnReferencedClass_Getter(this, e);
                    __value = (Zetbox.App.Base.ObjectClassNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.ReferencedClass == null)
                {
                    SetInitializedProperty("ReferencedClass");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Base.ObjectClassNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ReferencedClass);
                var __newValue = (Zetbox.App.Base.ObjectClassNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("ReferencedClass");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("ReferencedClass", __oldValue, __newValue);

                if (OnReferencedClass_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnReferencedClass_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.ObjectClassNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.ReferencedClass = null;
                }
                else
                {
                    this.Proxy.ReferencedClass = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("ReferencedClass", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnReferencedClass_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnReferencedClass_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for ReferencedClass's id, used on dehydration only</summary>
        private int? _fk_ReferencedClass = null;

        /// <summary>ForeignKey Property for ReferencedClass's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_ReferencedClass
		{
			get { return ReferencedClass != null ? ReferencedClass.ID : (int?)null; }
			set { _fk_ReferencedClass = value; }
		}

        /// <summary>Backing store for ReferencedClass's guid, used on import only</summary>
        private Guid? _fk_guid_ReferencedClass = null;

    public System.Threading.Tasks.Task TriggerFetchReferencedClassAsync()
    {
        return System.Threading.Tasks.Task.FromResult<Zetbox.App.Base.ObjectClass>(this.ReferencedClass);
    }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ReferencedClass
		public static event PropertyGetterHandler<Zetbox.App.Base.CalculatedObjectReferenceProperty, Zetbox.App.Base.ObjectClass> OnReferencedClass_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CalculatedObjectReferenceProperty, Zetbox.App.Base.ObjectClass> OnReferencedClass_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CalculatedObjectReferenceProperty, Zetbox.App.Base.ObjectClass> OnReferencedClass_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CalculatedObjectReferenceProperty> OnReferencedClass_IsValid;

        /// <summary>
        /// Returns the translated description
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDescription_CalculatedObjectReferenceProperty")]
        public override async System.Threading.Tasks.Task<string> GetDescription()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDescription_CalculatedObjectReferenceProperty != null)
            {
                await OnGetDescription_CalculatedObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = await base.GetDescription();
            }
            return e.Result;
        }
        public static event GetDescription_Handler<CalculatedObjectReferenceProperty> OnGetDescription_CalculatedObjectReferenceProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CalculatedObjectReferenceProperty> OnGetDescription_CalculatedObjectReferenceProperty_CanExec;

        [EventBasedMethod("OnGetDescription_CalculatedObjectReferenceProperty_CanExec")]
        public override bool GetDescriptionCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDescription_CalculatedObjectReferenceProperty_CanExec != null)
				{
					OnGetDescription_CalculatedObjectReferenceProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDescriptionCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CalculatedObjectReferenceProperty> OnGetDescription_CalculatedObjectReferenceProperty_CanExecReason;

        [EventBasedMethod("OnGetDescription_CalculatedObjectReferenceProperty_CanExecReason")]
        public override string GetDescriptionCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDescription_CalculatedObjectReferenceProperty_CanExecReason != null)
				{
					OnGetDescription_CalculatedObjectReferenceProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetElementTypeString_CalculatedObjectReferenceProperty")]
        public override async System.Threading.Tasks.Task<string> GetElementTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetElementTypeString_CalculatedObjectReferenceProperty != null)
            {
                await OnGetElementTypeString_CalculatedObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = await base.GetElementTypeString();
            }
            return e.Result;
        }
        public static event GetElementTypeString_Handler<CalculatedObjectReferenceProperty> OnGetElementTypeString_CalculatedObjectReferenceProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CalculatedObjectReferenceProperty> OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExec;

        [EventBasedMethod("OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExec")]
        public override bool GetElementTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExec != null)
				{
					OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CalculatedObjectReferenceProperty> OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExecReason;

        [EventBasedMethod("OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExecReason")]
        public override string GetElementTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExecReason != null)
				{
					OnGetElementTypeString_CalculatedObjectReferenceProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetLabel_CalculatedObjectReferenceProperty")]
        public override async System.Threading.Tasks.Task<string> GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_CalculatedObjectReferenceProperty != null)
            {
                await OnGetLabel_CalculatedObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = await base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<CalculatedObjectReferenceProperty> OnGetLabel_CalculatedObjectReferenceProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CalculatedObjectReferenceProperty> OnGetLabel_CalculatedObjectReferenceProperty_CanExec;

        [EventBasedMethod("OnGetLabel_CalculatedObjectReferenceProperty_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_CalculatedObjectReferenceProperty_CanExec != null)
				{
					OnGetLabel_CalculatedObjectReferenceProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CalculatedObjectReferenceProperty> OnGetLabel_CalculatedObjectReferenceProperty_CanExecReason;

        [EventBasedMethod("OnGetLabel_CalculatedObjectReferenceProperty_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_CalculatedObjectReferenceProperty_CanExecReason != null)
				{
					OnGetLabel_CalculatedObjectReferenceProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetName_CalculatedObjectReferenceProperty")]
        public override async System.Threading.Tasks.Task<string> GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_CalculatedObjectReferenceProperty != null)
            {
                await OnGetName_CalculatedObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = await base.GetName();
            }
            return e.Result;
        }
        public static event GetName_Handler<CalculatedObjectReferenceProperty> OnGetName_CalculatedObjectReferenceProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CalculatedObjectReferenceProperty> OnGetName_CalculatedObjectReferenceProperty_CanExec;

        [EventBasedMethod("OnGetName_CalculatedObjectReferenceProperty_CanExec")]
        public override bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_CalculatedObjectReferenceProperty_CanExec != null)
				{
					OnGetName_CalculatedObjectReferenceProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CalculatedObjectReferenceProperty> OnGetName_CalculatedObjectReferenceProperty_CanExecReason;

        [EventBasedMethod("OnGetName_CalculatedObjectReferenceProperty_CanExecReason")]
        public override string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_CalculatedObjectReferenceProperty_CanExecReason != null)
				{
					OnGetName_CalculatedObjectReferenceProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetPropertyType_CalculatedObjectReferenceProperty")]
        public override async System.Threading.Tasks.Task<System.Type> GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_CalculatedObjectReferenceProperty != null)
            {
                await OnGetPropertyType_CalculatedObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = await base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<CalculatedObjectReferenceProperty> OnGetPropertyType_CalculatedObjectReferenceProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CalculatedObjectReferenceProperty> OnGetPropertyType_CalculatedObjectReferenceProperty_CanExec;

        [EventBasedMethod("OnGetPropertyType_CalculatedObjectReferenceProperty_CanExec")]
        public override bool GetPropertyTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyType_CalculatedObjectReferenceProperty_CanExec != null)
				{
					OnGetPropertyType_CalculatedObjectReferenceProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CalculatedObjectReferenceProperty> OnGetPropertyType_CalculatedObjectReferenceProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyType_CalculatedObjectReferenceProperty_CanExecReason")]
        public override string GetPropertyTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyType_CalculatedObjectReferenceProperty_CanExecReason != null)
				{
					OnGetPropertyType_CalculatedObjectReferenceProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetPropertyTypeString_CalculatedObjectReferenceProperty")]
        public override async System.Threading.Tasks.Task<string> GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_CalculatedObjectReferenceProperty != null)
            {
                await OnGetPropertyTypeString_CalculatedObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = await base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<CalculatedObjectReferenceProperty> OnGetPropertyTypeString_CalculatedObjectReferenceProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CalculatedObjectReferenceProperty> OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExec;

        [EventBasedMethod("OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExec")]
        public override bool GetPropertyTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExec != null)
				{
					OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CalculatedObjectReferenceProperty> OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExecReason")]
        public override string GetPropertyTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExecReason != null)
				{
					OnGetPropertyTypeString_CalculatedObjectReferenceProperty_CanExecReason(this, e);
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
            return typeof(CalculatedObjectReferenceProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (CalculatedObjectReferenceProperty)obj;
            var otherImpl = (CalculatedObjectReferencePropertyNHibernateImpl)obj;
            var me = (CalculatedObjectReferenceProperty)this;

            this._fk_ReferencedClass = otherImpl._fk_ReferencedClass;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "ReferencedClass":
                    {
                        var __oldValue = (Zetbox.App.Base.ObjectClassNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ReferencedClass);
                        var __newValue = (Zetbox.App.Base.ObjectClassNHibernateImpl)parentObj;
                        NotifyPropertyChanging("ReferencedClass", __oldValue, __newValue);
                        this.Proxy.ReferencedClass = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("ReferencedClass", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "ReferencedClass":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Inputs":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Inputs":
                return TriggerFetchInputsAsync();
            case "ReferencedClass":
                return TriggerFetchReferencedClassAsync();
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override async System.Threading.Tasks.Task ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            await base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_ReferencedClass.HasValue)
                this.ReferencedClass = ((Zetbox.App.Base.ObjectClassNHibernateImpl)(await OurContext.FindPersistenceObjectAsync<Zetbox.App.Base.ObjectClass>(_fk_guid_ReferencedClass.Value)));
            else
            if (_fk_ReferencedClass.HasValue)
                this.ReferencedClass = ((Zetbox.App.Base.ObjectClassNHibernateImpl)(await OurContext.FindPersistenceObjectAsync<Zetbox.App.Base.ObjectClass>(_fk_ReferencedClass.Value)));
            else
                this.ReferencedClass = null;
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorNHibernateImpl<CalculatedObjectReferenceProperty, ICollection<Zetbox.App.Base.Property>>(
                        lazyCtx,
                        new Guid("bfda6511-087d-4381-9780-1f76f3abcffe"),
                        "Inputs",
                        null,
                        obj => obj.Inputs,
                        null, // lists are read-only properties
                        obj => OnInputs_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<CalculatedObjectReferenceProperty, Zetbox.App.Base.ObjectClass>(
                        lazyCtx,
                        new Guid("cd62d769-0752-4a72-832f-5935ece1198b"),
                        "ReferencedClass",
                        null,
                        obj => obj.ReferencedClass,
                        (obj, val) => obj.ReferencedClass = val,
						obj => OnReferencedClass_IsValid), 
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
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_CalculatedObjectReferenceProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CalculatedObjectReferenceProperty != null)
            {
                OnToString_CalculatedObjectReferenceProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<CalculatedObjectReferenceProperty> OnToString_CalculatedObjectReferenceProperty;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_CalculatedObjectReferenceProperty")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_CalculatedObjectReferenceProperty != null)
            {
                OnObjectIsValid_CalculatedObjectReferenceProperty(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<CalculatedObjectReferenceProperty> OnObjectIsValid_CalculatedObjectReferenceProperty;

        [EventBasedMethod("OnNotifyPreSave_CalculatedObjectReferenceProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_CalculatedObjectReferenceProperty != null) OnNotifyPreSave_CalculatedObjectReferenceProperty(this);
        }
        public static event ObjectEventHandler<CalculatedObjectReferenceProperty> OnNotifyPreSave_CalculatedObjectReferenceProperty;

        [EventBasedMethod("OnNotifyPostSave_CalculatedObjectReferenceProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_CalculatedObjectReferenceProperty != null) OnNotifyPostSave_CalculatedObjectReferenceProperty(this);
        }
        public static event ObjectEventHandler<CalculatedObjectReferenceProperty> OnNotifyPostSave_CalculatedObjectReferenceProperty;

        [EventBasedMethod("OnNotifyCreated_CalculatedObjectReferenceProperty")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("ReferencedClass");
            base.NotifyCreated();
            if (OnNotifyCreated_CalculatedObjectReferenceProperty != null) OnNotifyCreated_CalculatedObjectReferenceProperty(this);
        }
        public static event ObjectEventHandler<CalculatedObjectReferenceProperty> OnNotifyCreated_CalculatedObjectReferenceProperty;

        [EventBasedMethod("OnNotifyDeleting_CalculatedObjectReferenceProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_CalculatedObjectReferenceProperty != null) OnNotifyDeleting_CalculatedObjectReferenceProperty(this);

            // FK_CalculatedReference_references_ReferencedClass
            if (ReferencedClass != null) {
                ((NHibernatePersistenceObject)ReferencedClass).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)ReferencedClass);
            }

            Inputs.Clear();
            ReferencedClass = null;
        }
        public static event ObjectEventHandler<CalculatedObjectReferenceProperty> OnNotifyDeleting_CalculatedObjectReferenceProperty;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class CalculatedObjectReferencePropertyProxy
            : Zetbox.App.Base.PropertyNHibernateImpl.PropertyProxy
        {
            public CalculatedObjectReferencePropertyProxy()
            {
                Inputs = new Collection<Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl.CalculatedReference_dependsOn_InputProperties_RelationEntryProxy>();
            }

            public override Type ZetboxWrapper { get { return typeof(CalculatedObjectReferencePropertyNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(CalculatedObjectReferencePropertyProxy); } }

            public virtual ICollection<Zetbox.App.Base.CalculatedReference_dependsOn_InputProperties_RelationEntryNHibernateImpl.CalculatedReference_dependsOn_InputProperties_RelationEntryProxy> Inputs { get; set; }

            public virtual Zetbox.App.Base.ObjectClassNHibernateImpl.ObjectClassProxy ReferencedClass { get; set; }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;

            binStream.Write(eagerLoadLists);
            if (eagerLoadLists && auxObjects != null)
            {
                foreach(var obj in Inputs)
                {
                    auxObjects.Add(obj);
                }
				foreach(var relEntry in this.Proxy.Inputs)
				{
					auxObjects.Add(OurContext.AttachAndWrap(relEntry));
				}
            }
            binStream.Write(this.Proxy.ReferencedClass != null ? OurContext.GetIdFromProxy(this.Proxy.ReferencedClass) : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {

            Inputs_was_eagerLoaded = binStream.ReadBoolean();
            binStream.Read(out this._fk_ReferencedClass);
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this.Proxy.ReferencedClass != null ? this.Proxy.ReferencedClass.ExportGuid : (Guid?)null, xml, "ReferencedClass", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|ReferencedClass":
                this._fk_guid_ReferencedClass = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}