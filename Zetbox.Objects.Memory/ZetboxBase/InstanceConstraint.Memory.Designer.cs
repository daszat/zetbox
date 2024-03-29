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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("InstanceConstraint")]
    public abstract class InstanceConstraintMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, InstanceConstraint, Zetbox.API.IExportableInternal
    {
        private static readonly Guid _objectClassID = new Guid("25a83f49-3cff-4baf-850d-8d185bb329ec");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public InstanceConstraintMemoryImpl()
            : base(null)
        {
        }

        public InstanceConstraintMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Constrained
        // fkBackingName=_fk_Constrained; fkGuidBackingName=_fk_guid_Constrained;
        // referencedInterface=Zetbox.App.Base.DataType; moduleNamespace=Zetbox.App.Base;
        // inverse Navigator=Constraints; is list;
        // PositionStorage=none;
        // Target exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Base.DataType Constrained
        {
            get { return ConstrainedImpl; }
            set { ConstrainedImpl = (Zetbox.App.Base.DataTypeMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        public System.Threading.Tasks.Task<Zetbox.App.Base.DataType> GetProp_Constrained()
        {
            return TriggerFetchConstrainedAsync();
        }

        public async System.Threading.Tasks.Task SetProp_Constrained(Zetbox.App.Base.DataType newValue)
        {
            await TriggerFetchConstrainedAsync();
            ConstrainedImpl = (Zetbox.App.Base.DataTypeMemoryImpl)newValue;
        }

        private int? __fk_ConstrainedCache;

        private int? _fk_Constrained {
            get
            {
                return __fk_ConstrainedCache;
            }
            set
            {
                __fk_ConstrainedCache = value;
                // Recreate task to clear it's cache
                _triggerFetchConstrainedTask = null;
            }
        }

        /// <summary>ForeignKey Property for Constrained's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Constrained
		{
			get { return _fk_Constrained; }
			set { _fk_Constrained = value; }
		}

        private Guid? _fk_guid_Constrained = null;

        System.Threading.Tasks.Task<Zetbox.App.Base.DataType> _triggerFetchConstrainedTask;
        public System.Threading.Tasks.Task<Zetbox.App.Base.DataType> TriggerFetchConstrainedAsync()
        {
            if (_triggerFetchConstrainedTask != null) return _triggerFetchConstrainedTask;

            System.Threading.Tasks.Task<Zetbox.App.Base.DataType> task;

            if (_fk_Constrained.HasValue)
                task = Context.FindAsync<Zetbox.App.Base.DataType>(_fk_Constrained.Value);
            else
                task = System.Threading.Tasks.Task.FromResult<Zetbox.App.Base.DataType>(null);

            task.OnResult(t =>
            {
                if (OnConstrained_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.DataType>(t.Result);
                    OnConstrained_Getter(this, e);
                    // TODO: t.Result = e.Result;
                }
            });

            return _triggerFetchConstrainedTask = task;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Base.DataTypeMemoryImpl ConstrainedImpl
        {
            get
            {
                var task = TriggerFetchConstrainedAsync();
                task.TryRunSynchronously();
                return (Zetbox.App.Base.DataTypeMemoryImpl)task.Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Constrained == null) || (value != null && value.ID == _fk_Constrained))
                {
                    SetInitializedProperty("Constrained");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = ConstrainedImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Constrained", __oldValue, __newValue);

                if (OnConstrained_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.DataType>(__oldValue, __newValue);
                    OnConstrained_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.DataTypeMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Constrained = __newValue == null ? (int?)null : __newValue.ID;

                // now fixup redundant, inverse references
                // The inverse navigator will also fire events when changed, so should
                // only be touched after setting the local value above.
                // TODO: for complete correctness, the "other" Changing event should also fire
                //       before the local value is changed
                if (__oldValue != null)
                {
                    // remove from old list
                    (__oldValue.Constraints as IRelationListSync<Zetbox.App.Base.InstanceConstraint>).RemoveWithoutClearParent(this);
                }

                if (__newValue != null)
                {
                    // add to new list
                    (__newValue.Constraints as IRelationListSync<Zetbox.App.Base.InstanceConstraint>).AddWithoutSetParent(this);
                }
                // everything is done. fire the Changed event
                NotifyPropertyChanged("Constrained", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnConstrained_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.DataType>(__oldValue, __newValue);
                    OnConstrained_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Constrained
		public static event PropertyGetterHandler<Zetbox.App.Base.InstanceConstraint, Zetbox.App.Base.DataType> OnConstrained_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.InstanceConstraint, Zetbox.App.Base.DataType> OnConstrained_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.InstanceConstraint, Zetbox.App.Base.DataType> OnConstrained_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.InstanceConstraint> OnConstrained_IsValid;

        /// <summary>
        /// Export Guid
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public Guid ExportGuid
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ExportGuid;
                if (!_isExportGuidSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("8ef28076-900c-4294-920c-5d0d91e925bb"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._ExportGuid = (Guid)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'InstanceConstraint.ExportGuid'");
                    }
                }
                if (OnExportGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnExportGuid_Getter(this, __e);
                    __result = _ExportGuid = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isExportGuidSet = true;
                if (_ExportGuid != value)
                {
                    var __oldValue = _ExportGuid;
                    var __newValue = value;
                    if (OnExportGuid_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    _ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnExportGuid_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("ExportGuid");
                }
            }
        }
        private Guid _ExportGuid;
        private bool _isExportGuidSet = false;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.InstanceConstraint, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.InstanceConstraint, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.InstanceConstraint, Guid> OnExportGuid_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.InstanceConstraint> OnExportGuid_IsValid;

        /// <summary>
        /// The reason of this constraint
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Reason
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Reason;
                if (OnReason_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnReason_Getter(this, __e);
                    __result = _Reason = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Reason != value)
                {
                    var __oldValue = _Reason;
                    var __newValue = value;
                    if (OnReason_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnReason_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Reason", __oldValue, __newValue);
                    _Reason = __newValue;
                    NotifyPropertyChanged("Reason", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnReason_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnReason_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Reason");
                }
            }
        }
        private string _Reason;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.InstanceConstraint, string> OnReason_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.InstanceConstraint, string> OnReason_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.InstanceConstraint, string> OnReason_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.InstanceConstraint> OnReason_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_InstanceConstraint")]
        public virtual async System.Threading.Tasks.Task<string> GetErrorText(Zetbox.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_InstanceConstraint != null)
            {
                await OnGetErrorText_InstanceConstraint(this, e, constrainedObject);
            }
            else
            {
                throw new NotImplementedException("No handler registered on InstanceConstraint.GetErrorText");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, Zetbox.API.IDataObject constrainedObject);
        public static event GetErrorText_Handler<InstanceConstraint> OnGetErrorText_InstanceConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<InstanceConstraint> OnGetErrorText_InstanceConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_InstanceConstraint_CanExec")]
        public virtual bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_InstanceConstraint_CanExec != null)
				{
					OnGetErrorText_InstanceConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<InstanceConstraint> OnGetErrorText_InstanceConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_InstanceConstraint_CanExecReason")]
        public virtual string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_InstanceConstraint_CanExecReason != null)
				{
					OnGetErrorText_InstanceConstraint_CanExecReason(this, e);
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
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnIsValid_InstanceConstraint")]
        public virtual async System.Threading.Tasks.Task<bool> IsValid(Zetbox.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_InstanceConstraint != null)
            {
                await OnIsValid_InstanceConstraint(this, e, constrainedObject);
            }
            else
            {
                throw new NotImplementedException("No handler registered on InstanceConstraint.IsValid");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, Zetbox.API.IDataObject constrainedObject);
        public static event IsValid_Handler<InstanceConstraint> OnIsValid_InstanceConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<InstanceConstraint> OnIsValid_InstanceConstraint_CanExec;

        [EventBasedMethod("OnIsValid_InstanceConstraint_CanExec")]
        public virtual bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_InstanceConstraint_CanExec != null)
				{
					OnIsValid_InstanceConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<InstanceConstraint> OnIsValid_InstanceConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_InstanceConstraint_CanExecReason")]
        public virtual string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_InstanceConstraint_CanExecReason != null)
				{
					OnIsValid_InstanceConstraint_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(InstanceConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (InstanceConstraint)obj;
            var otherImpl = (InstanceConstraintMemoryImpl)obj;
            var me = (InstanceConstraint)this;

            me.ExportGuid = other.ExportGuid;
            me.Reason = other.Reason;
            this._fk_Constrained = otherImpl._fk_Constrained;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Constrained":
                    {
                        var __oldValue = _fk_Constrained;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Constrained", __oldValue, __newValue);
                        _fk_Constrained = __newValue;
                        NotifyPropertyChanged("Constrained", __oldValue, __newValue);
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
                case "Constrained":
                case "ExportGuid":
                case "Reason":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Constrained":
                return TriggerFetchConstrainedAsync();
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

            if (_fk_guid_Constrained.HasValue)
                ConstrainedImpl = (Zetbox.App.Base.DataTypeMemoryImpl)(await Context.FindPersistenceObjectAsync<Zetbox.App.Base.DataType>(_fk_guid_Constrained.Value));
            else
            if (_fk_Constrained.HasValue)
                ConstrainedImpl = (Zetbox.App.Base.DataTypeMemoryImpl)(await Context.FindAsync<Zetbox.App.Base.DataType>(_fk_Constrained.Value));
            else
                ConstrainedImpl = null;
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
                    new PropertyDescriptorMemoryImpl<InstanceConstraint, Zetbox.App.Base.DataType>(
                        lazyCtx,
                        new Guid("d89723fc-1050-45a1-9b74-5575a677bc2b"),
                        "Constrained",
                        null,
                        obj => obj.Constrained,
                        (obj, val) => obj.Constrained = val,
						obj => OnConstrained_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<InstanceConstraint, Guid>(
                        lazyCtx,
                        new Guid("8ef28076-900c-4294-920c-5d0d91e925bb"),
                        "ExportGuid",
                        null,
                        obj => obj.ExportGuid,
                        (obj, val) => obj.ExportGuid = val,
						obj => OnExportGuid_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<InstanceConstraint, string>(
                        lazyCtx,
                        new Guid("83be7495-e0e0-48fc-872a-70de9c0f7a88"),
                        "Reason",
                        null,
                        obj => obj.Reason,
                        (obj, val) => obj.Reason = val,
						obj => OnReason_IsValid), 
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
        [EventBasedMethod("OnToString_InstanceConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_InstanceConstraint != null)
            {
                OnToString_InstanceConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<InstanceConstraint> OnToString_InstanceConstraint;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_InstanceConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_InstanceConstraint != null)
            {
                OnObjectIsValid_InstanceConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<InstanceConstraint> OnObjectIsValid_InstanceConstraint;

        [EventBasedMethod("OnNotifyPreSave_InstanceConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_InstanceConstraint != null) OnNotifyPreSave_InstanceConstraint(this);
        }
        public static event ObjectEventHandler<InstanceConstraint> OnNotifyPreSave_InstanceConstraint;

        [EventBasedMethod("OnNotifyPostSave_InstanceConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_InstanceConstraint != null) OnNotifyPostSave_InstanceConstraint(this);
        }
        public static event ObjectEventHandler<InstanceConstraint> OnNotifyPostSave_InstanceConstraint;

        [EventBasedMethod("OnNotifyCreated_InstanceConstraint")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Constrained");
            SetNotInitializedProperty("Reason");
            base.NotifyCreated();
            if (OnNotifyCreated_InstanceConstraint != null) OnNotifyCreated_InstanceConstraint(this);
        }
        public static event ObjectEventHandler<InstanceConstraint> OnNotifyCreated_InstanceConstraint;

        [EventBasedMethod("OnNotifyDeleting_InstanceConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_InstanceConstraint != null) OnNotifyDeleting_InstanceConstraint(this);
            Constrained = null;
        }
        public static event ObjectEventHandler<InstanceConstraint> OnNotifyDeleting_InstanceConstraint;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(_fk_Constrained != null ? _fk_Constrained : (int?)null);
            binStream.Write(this._isExportGuidSet);
            if (this._isExportGuidSet) {
                binStream.Write(this._ExportGuid);
            }
            binStream.Write(this._Reason);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_Constrained = binStream.ReadNullableInt32();
            this._isExportGuidSet = binStream.ReadBoolean();
            if (this._isExportGuidSet) {
                this._ExportGuid = binStream.ReadGuid();
            }
            this._Reason = binStream.ReadString();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            xml.WriteAttributeString("ExportGuid", this._ExportGuid.ToString());
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(Constrained != null ? Constrained.ExportGuid : (Guid?)null, xml, "Constrained", "Zetbox.App.Base");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._Reason, xml, "Reason", "Zetbox.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|Constrained":
                this._fk_guid_Constrained = XmlStreamer.ReadNullableGuid(xml);
                break;
            case "Zetbox.App.Base|ExportGuid":
                // Import must have default value set
                this._ExportGuid = XmlStreamer.ReadGuid(xml);
                this._isExportGuidSet = true;
                break;
            case "Zetbox.App.Base|Reason":
                this._Reason = XmlStreamer.ReadString(xml);
                break;
            }
        }

        #endregion

    }
}