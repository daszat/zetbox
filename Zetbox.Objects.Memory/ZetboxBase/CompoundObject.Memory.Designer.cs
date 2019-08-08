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

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// Metadefinition Object for Compound Objects.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CompoundObject")]
    public class CompoundObjectMemoryImpl : Zetbox.App.Base.DataTypeMemoryImpl, CompoundObject
    {
        private static readonly Guid _objectClassID = new Guid("2cb3f778-dd6a-46c7-ad2b-5f8691313035");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public CompoundObjectMemoryImpl()
            : base(null)
        {
        }

        public CompoundObjectMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// An optional default ViewModelDescriptor for Properties of this type
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DefaultPropertyViewModelDescriptor
        // fkBackingName=_fk_DefaultPropertyViewModelDescriptor; fkGuidBackingName=_fk_guid_DefaultPropertyViewModelDescriptor;
        // referencedInterface=Zetbox.App.GUI.ViewModelDescriptor; moduleNamespace=Zetbox.App.Base;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.GUI.ViewModelDescriptor DefaultPropertyViewModelDescriptor
        {
            get { return DefaultPropertyViewModelDescriptorImpl; }
            set { DefaultPropertyViewModelDescriptorImpl = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? __fk_DefaultPropertyViewModelDescriptorCache;

        private int? _fk_DefaultPropertyViewModelDescriptor {
            get
            {
                return __fk_DefaultPropertyViewModelDescriptorCache;
            }
            set
            {
                __fk_DefaultPropertyViewModelDescriptorCache = value;
                // Recreate task to clear it's cache
                _triggerFetchDefaultPropertyViewModelDescriptorTask = null;
            }
        }

        /// <summary>ForeignKey Property for DefaultPropertyViewModelDescriptor's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_DefaultPropertyViewModelDescriptor
		{
			get { return _fk_DefaultPropertyViewModelDescriptor; }
			set { _fk_DefaultPropertyViewModelDescriptor = value; }
		}

        private Guid? _fk_guid_DefaultPropertyViewModelDescriptor = null;

        Zetbox.API.Async.ZbTask<Zetbox.App.GUI.ViewModelDescriptor> _triggerFetchDefaultPropertyViewModelDescriptorTask;
        public Zetbox.API.Async.ZbTask<Zetbox.App.GUI.ViewModelDescriptor> TriggerFetchDefaultPropertyViewModelDescriptorAsync()
        {
            if (_triggerFetchDefaultPropertyViewModelDescriptorTask != null) return _triggerFetchDefaultPropertyViewModelDescriptorTask;

            if (_fk_DefaultPropertyViewModelDescriptor.HasValue)
                _triggerFetchDefaultPropertyViewModelDescriptorTask = Context.FindAsync<Zetbox.App.GUI.ViewModelDescriptor>(_fk_DefaultPropertyViewModelDescriptor.Value);
            else
                _triggerFetchDefaultPropertyViewModelDescriptorTask = new Zetbox.API.Async.ZbTask<Zetbox.App.GUI.ViewModelDescriptor>(Zetbox.API.Async.ZbTask.Synchron, () => null);

            _triggerFetchDefaultPropertyViewModelDescriptorTask.OnResult(t =>
            {
                if (OnDefaultPropertyViewModelDescriptor_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.GUI.ViewModelDescriptor>(t.Result);
                    OnDefaultPropertyViewModelDescriptor_Getter(this, e);
                    t.Result = e.Result;
                }
            });

            return _triggerFetchDefaultPropertyViewModelDescriptorTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.GUI.ViewModelDescriptorMemoryImpl DefaultPropertyViewModelDescriptorImpl
        {
            get
            {
                return (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)TriggerFetchDefaultPropertyViewModelDescriptorAsync().Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_DefaultPropertyViewModelDescriptor == null) || (value != null && value.ID == _fk_DefaultPropertyViewModelDescriptor))
                {
                    SetInitializedProperty("DefaultPropertyViewModelDescriptor");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = DefaultPropertyViewModelDescriptorImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("DefaultPropertyViewModelDescriptor", __oldValue, __newValue);

                if (OnDefaultPropertyViewModelDescriptor_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.GUI.ViewModelDescriptor>(__oldValue, __newValue);
                    OnDefaultPropertyViewModelDescriptor_PreSetter(this, e);
                    __newValue = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_DefaultPropertyViewModelDescriptor = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("DefaultPropertyViewModelDescriptor", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnDefaultPropertyViewModelDescriptor_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.GUI.ViewModelDescriptor>(__oldValue, __newValue);
                    OnDefaultPropertyViewModelDescriptor_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DefaultPropertyViewModelDescriptor
		public static event PropertyGetterHandler<Zetbox.App.Base.CompoundObject, Zetbox.App.GUI.ViewModelDescriptor> OnDefaultPropertyViewModelDescriptor_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CompoundObject, Zetbox.App.GUI.ViewModelDescriptor> OnDefaultPropertyViewModelDescriptor_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CompoundObject, Zetbox.App.GUI.ViewModelDescriptor> OnDefaultPropertyViewModelDescriptor_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CompoundObject> OnDefaultPropertyViewModelDescriptor_IsValid;

        /// <summary>
        /// The default ViewModel to use for this Compound Object
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DefaultViewModelDescriptor
        // fkBackingName=_fk_DefaultViewModelDescriptor; fkGuidBackingName=_fk_guid_DefaultViewModelDescriptor;
        // referencedInterface=Zetbox.App.GUI.ViewModelDescriptor; moduleNamespace=Zetbox.App.Base;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.GUI.ViewModelDescriptor DefaultViewModelDescriptor
        {
            get { return DefaultViewModelDescriptorImpl; }
            set { DefaultViewModelDescriptorImpl = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? __fk_DefaultViewModelDescriptorCache;

        private int? _fk_DefaultViewModelDescriptor {
            get
            {
                return __fk_DefaultViewModelDescriptorCache;
            }
            set
            {
                __fk_DefaultViewModelDescriptorCache = value;
                // Recreate task to clear it's cache
                _triggerFetchDefaultViewModelDescriptorTask = null;
            }
        }

        /// <summary>ForeignKey Property for DefaultViewModelDescriptor's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_DefaultViewModelDescriptor
		{
			get { return _fk_DefaultViewModelDescriptor; }
			set { _fk_DefaultViewModelDescriptor = value; }
		}

        private Guid? _fk_guid_DefaultViewModelDescriptor = null;

        Zetbox.API.Async.ZbTask<Zetbox.App.GUI.ViewModelDescriptor> _triggerFetchDefaultViewModelDescriptorTask;
        public Zetbox.API.Async.ZbTask<Zetbox.App.GUI.ViewModelDescriptor> TriggerFetchDefaultViewModelDescriptorAsync()
        {
            if (_triggerFetchDefaultViewModelDescriptorTask != null) return _triggerFetchDefaultViewModelDescriptorTask;

            if (_fk_DefaultViewModelDescriptor.HasValue)
                _triggerFetchDefaultViewModelDescriptorTask = Context.FindAsync<Zetbox.App.GUI.ViewModelDescriptor>(_fk_DefaultViewModelDescriptor.Value);
            else
                _triggerFetchDefaultViewModelDescriptorTask = new Zetbox.API.Async.ZbTask<Zetbox.App.GUI.ViewModelDescriptor>(Zetbox.API.Async.ZbTask.Synchron, () => null);

            _triggerFetchDefaultViewModelDescriptorTask.OnResult(t =>
            {
                if (OnDefaultViewModelDescriptor_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.GUI.ViewModelDescriptor>(t.Result);
                    OnDefaultViewModelDescriptor_Getter(this, e);
                    t.Result = e.Result;
                }
            });

            return _triggerFetchDefaultViewModelDescriptorTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.GUI.ViewModelDescriptorMemoryImpl DefaultViewModelDescriptorImpl
        {
            get
            {
                return (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)TriggerFetchDefaultViewModelDescriptorAsync().Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_DefaultViewModelDescriptor == null) || (value != null && value.ID == _fk_DefaultViewModelDescriptor))
                {
                    SetInitializedProperty("DefaultViewModelDescriptor");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = DefaultViewModelDescriptorImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("DefaultViewModelDescriptor", __oldValue, __newValue);

                if (OnDefaultViewModelDescriptor_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.GUI.ViewModelDescriptor>(__oldValue, __newValue);
                    OnDefaultViewModelDescriptor_PreSetter(this, e);
                    __newValue = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_DefaultViewModelDescriptor = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("DefaultViewModelDescriptor", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnDefaultViewModelDescriptor_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.GUI.ViewModelDescriptor>(__oldValue, __newValue);
                    OnDefaultViewModelDescriptor_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for DefaultViewModelDescriptor
		public static event PropertyGetterHandler<Zetbox.App.Base.CompoundObject, Zetbox.App.GUI.ViewModelDescriptor> OnDefaultViewModelDescriptor_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CompoundObject, Zetbox.App.GUI.ViewModelDescriptor> OnDefaultViewModelDescriptor_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CompoundObject, Zetbox.App.GUI.ViewModelDescriptor> OnDefaultViewModelDescriptor_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CompoundObject> OnDefaultViewModelDescriptor_IsValid;

        /// <summary>
        /// Property wizard
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnAddProperty_CompoundObject")]
        public override Zetbox.App.Base.Property AddProperty()
        {
            var e = new MethodReturnEventArgs<Zetbox.App.Base.Property>();
            if (OnAddProperty_CompoundObject != null)
            {
                OnAddProperty_CompoundObject(this, e);
            }
            else
            {
                e.Result = base.AddProperty();
            }
            return e.Result;
        }
        public static event AddProperty_Handler<CompoundObject> OnAddProperty_CompoundObject;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObject> OnAddProperty_CompoundObject_CanExec;

        [EventBasedMethod("OnAddProperty_CompoundObject_CanExec")]
        public override bool AddPropertyCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnAddProperty_CompoundObject_CanExec != null)
				{
					OnAddProperty_CompoundObject_CanExec(this, e);
				}
				else
				{
					e.Result = base.AddPropertyCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObject> OnAddProperty_CompoundObject_CanExecReason;

        [EventBasedMethod("OnAddProperty_CompoundObject_CanExecReason")]
        public override string AddPropertyCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnAddProperty_CompoundObject_CanExecReason != null)
				{
					OnAddProperty_CompoundObject_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.AddPropertyCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDataType_CompoundObject")]
        public override System.Type GetDataType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_CompoundObject != null)
            {
                OnGetDataType_CompoundObject(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
            return e.Result;
        }
        public static event GetDataType_Handler<CompoundObject> OnGetDataType_CompoundObject;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObject> OnGetDataType_CompoundObject_CanExec;

        [EventBasedMethod("OnGetDataType_CompoundObject_CanExec")]
        public override bool GetDataTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDataType_CompoundObject_CanExec != null)
				{
					OnGetDataType_CompoundObject_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObject> OnGetDataType_CompoundObject_CanExecReason;

        [EventBasedMethod("OnGetDataType_CompoundObject_CanExecReason")]
        public override string GetDataTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDataType_CompoundObject_CanExecReason != null)
				{
					OnGetDataType_CompoundObject_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDataTypeString_CompoundObject")]
        public override string GetDataTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_CompoundObject != null)
            {
                OnGetDataTypeString_CompoundObject(this, e);
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
        public static event GetDataTypeString_Handler<CompoundObject> OnGetDataTypeString_CompoundObject;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObject> OnGetDataTypeString_CompoundObject_CanExec;

        [EventBasedMethod("OnGetDataTypeString_CompoundObject_CanExec")]
        public override bool GetDataTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDataTypeString_CompoundObject_CanExec != null)
				{
					OnGetDataTypeString_CompoundObject_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObject> OnGetDataTypeString_CompoundObject_CanExecReason;

        [EventBasedMethod("OnGetDataTypeString_CompoundObject_CanExecReason")]
        public override string GetDataTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDataTypeString_CompoundObject_CanExecReason != null)
				{
					OnGetDataTypeString_CompoundObject_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetDataTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetName_CompoundObject")]
        public virtual string GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_CompoundObject != null)
            {
                OnGetName_CompoundObject(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on CompoundObject.GetName");
            }
            return e.Result;
        }
        public delegate void GetName_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
        public static event GetName_Handler<CompoundObject> OnGetName_CompoundObject;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObject> OnGetName_CompoundObject_CanExec;

        [EventBasedMethod("OnGetName_CompoundObject_CanExec")]
        public virtual bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_CompoundObject_CanExec != null)
				{
					OnGetName_CompoundObject_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObject> OnGetName_CompoundObject_CanExecReason;

        [EventBasedMethod("OnGetName_CompoundObject_CanExecReason")]
        public virtual string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_CompoundObject_CanExecReason != null)
				{
					OnGetName_CompoundObject_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Implements all available interfaces as Properties and Methods
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnImplementInterfaces_CompoundObject")]
        public override void ImplementInterfaces()
        {
            // base.ImplementInterfaces();
            if (OnImplementInterfaces_CompoundObject != null)
            {
                OnImplementInterfaces_CompoundObject(this);
            }
            else
            {
                base.ImplementInterfaces();
            }
        }
        public static event ImplementInterfaces_Handler<CompoundObject> OnImplementInterfaces_CompoundObject;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CompoundObject> OnImplementInterfaces_CompoundObject_CanExec;

        [EventBasedMethod("OnImplementInterfaces_CompoundObject_CanExec")]
        public override bool ImplementInterfacesCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnImplementInterfaces_CompoundObject_CanExec != null)
				{
					OnImplementInterfaces_CompoundObject_CanExec(this, e);
				}
				else
				{
					e.Result = base.ImplementInterfacesCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CompoundObject> OnImplementInterfaces_CompoundObject_CanExecReason;

        [EventBasedMethod("OnImplementInterfaces_CompoundObject_CanExecReason")]
        public override string ImplementInterfacesCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnImplementInterfaces_CompoundObject_CanExecReason != null)
				{
					OnImplementInterfaces_CompoundObject_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.ImplementInterfacesCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(CompoundObject);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (CompoundObject)obj;
            var otherImpl = (CompoundObjectMemoryImpl)obj;
            var me = (CompoundObject)this;

            this._fk_DefaultPropertyViewModelDescriptor = otherImpl._fk_DefaultPropertyViewModelDescriptor;
            this._fk_DefaultViewModelDescriptor = otherImpl._fk_DefaultViewModelDescriptor;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "DefaultPropertyViewModelDescriptor":
                    {
                        var __oldValue = _fk_DefaultPropertyViewModelDescriptor;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("DefaultPropertyViewModelDescriptor", __oldValue, __newValue);
                        _fk_DefaultPropertyViewModelDescriptor = __newValue;
                        NotifyPropertyChanged("DefaultPropertyViewModelDescriptor", __oldValue, __newValue);
                    }
                    break;
                case "DefaultViewModelDescriptor":
                    {
                        var __oldValue = _fk_DefaultViewModelDescriptor;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("DefaultViewModelDescriptor", __oldValue, __newValue);
                        _fk_DefaultViewModelDescriptor = __newValue;
                        NotifyPropertyChanged("DefaultViewModelDescriptor", __oldValue, __newValue);
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
                case "DefaultPropertyViewModelDescriptor":
                case "DefaultViewModelDescriptor":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "DefaultPropertyViewModelDescriptor":
                return TriggerFetchDefaultPropertyViewModelDescriptorAsync();
            case "DefaultViewModelDescriptor":
                return TriggerFetchDefaultViewModelDescriptorAsync();
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

            if (_fk_guid_DefaultPropertyViewModelDescriptor.HasValue)
                DefaultPropertyViewModelDescriptorImpl = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)Context.FindPersistenceObject<Zetbox.App.GUI.ViewModelDescriptor>(_fk_guid_DefaultPropertyViewModelDescriptor.Value);
            else
            if (_fk_DefaultPropertyViewModelDescriptor.HasValue)
                DefaultPropertyViewModelDescriptorImpl = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)Context.Find<Zetbox.App.GUI.ViewModelDescriptor>(_fk_DefaultPropertyViewModelDescriptor.Value);
            else
                DefaultPropertyViewModelDescriptorImpl = null;

            if (_fk_guid_DefaultViewModelDescriptor.HasValue)
                DefaultViewModelDescriptorImpl = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)Context.FindPersistenceObject<Zetbox.App.GUI.ViewModelDescriptor>(_fk_guid_DefaultViewModelDescriptor.Value);
            else
            if (_fk_DefaultViewModelDescriptor.HasValue)
                DefaultViewModelDescriptorImpl = (Zetbox.App.GUI.ViewModelDescriptorMemoryImpl)Context.Find<Zetbox.App.GUI.ViewModelDescriptor>(_fk_DefaultViewModelDescriptor.Value);
            else
                DefaultViewModelDescriptorImpl = null;
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
                    new PropertyDescriptorMemoryImpl<CompoundObject, Zetbox.App.GUI.ViewModelDescriptor>(
                        lazyCtx,
                        new Guid("908757d2-053b-40c5-89f8-9e5f79b5fe83"),
                        "DefaultPropertyViewModelDescriptor",
                        null,
                        obj => obj.DefaultPropertyViewModelDescriptor,
                        (obj, val) => obj.DefaultPropertyViewModelDescriptor = val,
						obj => OnDefaultPropertyViewModelDescriptor_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<CompoundObject, Zetbox.App.GUI.ViewModelDescriptor>(
                        lazyCtx,
                        new Guid("863dece6-ff86-41c5-82ad-ec520adf6309"),
                        "DefaultViewModelDescriptor",
                        null,
                        obj => obj.DefaultViewModelDescriptor,
                        (obj, val) => obj.DefaultViewModelDescriptor = val,
						obj => OnDefaultViewModelDescriptor_IsValid), 
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
        [EventBasedMethod("OnToString_CompoundObject")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CompoundObject != null)
            {
                OnToString_CompoundObject(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<CompoundObject> OnToString_CompoundObject;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_CompoundObject")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_CompoundObject != null)
            {
                OnObjectIsValid_CompoundObject(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<CompoundObject> OnObjectIsValid_CompoundObject;

        [EventBasedMethod("OnNotifyPreSave_CompoundObject")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_CompoundObject != null) OnNotifyPreSave_CompoundObject(this);
        }
        public static event ObjectEventHandler<CompoundObject> OnNotifyPreSave_CompoundObject;

        [EventBasedMethod("OnNotifyPostSave_CompoundObject")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_CompoundObject != null) OnNotifyPostSave_CompoundObject(this);
        }
        public static event ObjectEventHandler<CompoundObject> OnNotifyPostSave_CompoundObject;

        [EventBasedMethod("OnNotifyCreated_CompoundObject")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("DefaultPropertyViewModelDescriptor");
            SetNotInitializedProperty("DefaultViewModelDescriptor");
            base.NotifyCreated();
            if (OnNotifyCreated_CompoundObject != null) OnNotifyCreated_CompoundObject(this);
        }
        public static event ObjectEventHandler<CompoundObject> OnNotifyCreated_CompoundObject;

        [EventBasedMethod("OnNotifyDeleting_CompoundObject")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_CompoundObject != null) OnNotifyDeleting_CompoundObject(this);
            DefaultPropertyViewModelDescriptor = null;
            DefaultViewModelDescriptor = null;
        }
        public static event ObjectEventHandler<CompoundObject> OnNotifyDeleting_CompoundObject;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(DefaultPropertyViewModelDescriptor != null ? DefaultPropertyViewModelDescriptor.ID : (int?)null);
            binStream.Write(DefaultViewModelDescriptor != null ? DefaultViewModelDescriptor.ID : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_DefaultPropertyViewModelDescriptor = binStream.ReadNullableInt32();
            this._fk_DefaultViewModelDescriptor = binStream.ReadNullableInt32();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(DefaultPropertyViewModelDescriptor != null ? DefaultPropertyViewModelDescriptor.ExportGuid : (Guid?)null, xml, "DefaultPropertyViewModelDescriptor", "Zetbox.App.Base");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(DefaultViewModelDescriptor != null ? DefaultViewModelDescriptor.ExportGuid : (Guid?)null, xml, "DefaultViewModelDescriptor", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|DefaultPropertyViewModelDescriptor":
                this._fk_guid_DefaultPropertyViewModelDescriptor = XmlStreamer.ReadNullableGuid(xml);
                break;
            case "Zetbox.App.Base|DefaultViewModelDescriptor":
                this._fk_guid_DefaultViewModelDescriptor = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}