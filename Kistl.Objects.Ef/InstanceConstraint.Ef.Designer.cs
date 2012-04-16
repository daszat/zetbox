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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="InstanceConstraint")]
    [System.Diagnostics.DebuggerDisplay("InstanceConstraint")]
    public class InstanceConstraintEfImpl : BaseServerDataObject_EntityFramework, InstanceConstraint, Kistl.API.IExportableInternal
    {
        private static readonly Guid _objectClassID = new Guid("25a83f49-3cff-4baf-850d-8d185bb329ec");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public InstanceConstraintEfImpl()
            : base(null)
        {
        }

        public InstanceConstraintEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Constraint_on_Constrained
    A: ZeroOrMore InstanceConstraint as Constraint
    B: ZeroOrOne DataType as Constrained
    Preferred Storage: MergeIntoA
    */
        // object reference property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Constrained
        // fkBackingName=_fk_Constrained; fkGuidBackingName=_fk_guid_Constrained;
        // referencedInterface=Kistl.App.Base.DataType; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=Constraints; is list;
        // PositionStorage=none;
        // Target exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType Constrained
        {
            get { return ConstrainedImpl; }
            set { ConstrainedImpl = (Kistl.App.Base.DataTypeEfImpl)value; }
        }

        private int? _fk_Constrained;

        private Guid? _fk_guid_Constrained = null;

        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Constraint_on_Constrained", "Constrained")]
        public Kistl.App.Base.DataTypeEfImpl ConstrainedImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.DataTypeEfImpl __value;
                EntityReference<Kistl.App.Base.DataTypeEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataTypeEfImpl>(
                        "Model.FK_Constraint_on_Constrained",
                        "Constrained");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnConstrained_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.DataType>(__value);
                    OnConstrained_Getter(this, e);
                    __value = (Kistl.App.Base.DataTypeEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Base.DataTypeEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataTypeEfImpl>(
                        "Model.FK_Constraint_on_Constrained",
                        "Constrained");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Base.DataTypeEfImpl __oldValue = (Kistl.App.Base.DataTypeEfImpl)r.Value;
                Kistl.App.Base.DataTypeEfImpl __newValue = (Kistl.App.Base.DataTypeEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("Constrained", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Constraints", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Constraints", null, null, null);
                }

                if (OnConstrained_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.DataType>(__oldValue, __newValue);
                    OnConstrained_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.DataTypeEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Base.DataTypeEfImpl)__newValue;

                if (OnConstrained_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.DataType>(__oldValue, __newValue);
                    OnConstrained_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("Constrained", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("Constraints", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("Constraints", null, null, null);
                }
            }
        }

        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Constrained
		public static event PropertyGetterHandler<Kistl.App.Base.InstanceConstraint, Kistl.App.Base.DataType> OnConstrained_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.InstanceConstraint, Kistl.App.Base.DataType> OnConstrained_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.InstanceConstraint, Kistl.App.Base.DataType> OnConstrained_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.InstanceConstraint> OnConstrained_IsValid;

        /// <summary>
        /// Export Guid
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public Guid ExportGuid
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(Guid);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ExportGuid;
                if (!_isExportGuidSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Kistl.App.Base.Property>(new Guid("8ef28076-900c-4294-920c-5d0d91e925bb"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._ExportGuid = (Guid)__tmp_value;
                    } else {
                        Kistl.API.Utils.Logging.Log.Warn("Unable to get default value for property 'InstanceConstraint.ExportGuid'");
                    }
                }
                if (OnExportGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnExportGuid_Getter(this, __e);
                    __result = __e.Result;
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
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.InstanceConstraint, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.InstanceConstraint, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.InstanceConstraint, Guid> OnExportGuid_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.InstanceConstraint> OnExportGuid_IsValid;

        /// <summary>
        /// The reason of this constraint
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Reason
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Reason;
                if (OnReason_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnReason_Getter(this, __e);
                    __result = __e.Result;
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
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Base.InstanceConstraint, string> OnReason_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.InstanceConstraint, string> OnReason_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.InstanceConstraint, string> OnReason_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.InstanceConstraint> OnReason_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_InstanceConstraint")]
        public virtual string GetErrorText(Kistl.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_InstanceConstraint != null)
            {
                OnGetErrorText_InstanceConstraint(this, e, constrainedObject);
            }
            else
            {
                throw new NotImplementedException("No handler registered on InstanceConstraint.GetErrorText");
            }
            return e.Result;
        }
        public delegate void GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, Kistl.API.IDataObject constrainedObject);
        public static event GetErrorText_Handler<InstanceConstraint> OnGetErrorText_InstanceConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
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
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnIsValid_InstanceConstraint")]
        public virtual bool IsValid(Kistl.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_InstanceConstraint != null)
            {
                OnIsValid_InstanceConstraint(this, e, constrainedObject);
            }
            else
            {
                throw new NotImplementedException("No handler registered on InstanceConstraint.IsValid");
            }
            return e.Result;
        }
        public delegate void IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, Kistl.API.IDataObject constrainedObject);
        public static event IsValid_Handler<InstanceConstraint> OnIsValid_InstanceConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
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
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(InstanceConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (InstanceConstraint)obj;
            var otherImpl = (InstanceConstraintEfImpl)obj;
            var me = (InstanceConstraint)this;

            me.ExportGuid = other.ExportGuid;
            me.Reason = other.Reason;
            this._fk_Constrained = otherImpl._fk_Constrained;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

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

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_Constrained.HasValue)
                ConstrainedImpl = (Kistl.App.Base.DataTypeEfImpl)Context.FindPersistenceObject<Kistl.App.Base.DataType>(_fk_guid_Constrained.Value);
            else
            if (_fk_Constrained.HasValue)
                ConstrainedImpl = (Kistl.App.Base.DataTypeEfImpl)Context.Find<Kistl.App.Base.DataType>(_fk_Constrained.Value);
            else
                ConstrainedImpl = null;
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
                    new PropertyDescriptorEfImpl<InstanceConstraint, Kistl.App.Base.DataType>(
                        lazyCtx,
                        new Guid("d89723fc-1050-45a1-9b74-5575a677bc2b"),
                        "Constrained",
                        null,
                        obj => obj.Constrained,
                        (obj, val) => obj.Constrained = val,
						obj => OnConstrained_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<InstanceConstraint, Guid>(
                        lazyCtx,
                        new Guid("8ef28076-900c-4294-920c-5d0d91e925bb"),
                        "ExportGuid",
                        null,
                        obj => obj.ExportGuid,
                        (obj, val) => obj.ExportGuid = val,
						obj => OnExportGuid_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<InstanceConstraint, string>(
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

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
            e.IsValid = b.IsValid;
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
            Constrained = null;
            base.NotifyDeleting();
            if (OnNotifyDeleting_InstanceConstraint != null) OnNotifyDeleting_InstanceConstraint(this);
        }
        public static event ObjectEventHandler<InstanceConstraint> OnNotifyDeleting_InstanceConstraint;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var __oldValue = _ID;
                    var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);

                }
				else 
				{
					SetInitializedProperty("ID");
				}
            }
        }
        private int _ID;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.DataTypeEfImpl>("Model.FK_Constraint_on_Constrained", "Constrained").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
            BinarySerializer.ToStream(this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.ToStream(this._ExportGuid, binStream);
            }
            BinarySerializer.ToStream(this._Reason, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_Constrained, binStream);
            BinarySerializer.FromStream(out this._isExportGuidSet, binStream);
            if (this._isExportGuidSet) {
                BinarySerializer.FromStream(out this._ExportGuid, binStream);
            }
            BinarySerializer.FromStream(out this._Reason, binStream);
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
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Base.DataTypeEfImpl>("Model.FK_Constraint_on_Constrained", "Constrained").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "Constrained", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.ToStream(this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.ToStream(this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_Constrained, xml, "Constrained", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._isExportGuidSet, xml, "IsExportGuidSet", "Kistl.App.Base");
            if (this._isExportGuidSet) {
                XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            }
            XmlStreamer.FromStream(ref this._Reason, xml, "Reason", "Kistl.App.Base");
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Constrained != null ? Constrained.ExportGuid : (Guid?)null, xml, "Constrained", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.FromStream(ref this._fk_guid_Constrained, xml, "Constrained", "Kistl.App.Base");
            // Import must have default value set
            XmlStreamer.FromStream(ref this._ExportGuid, xml, "ExportGuid", "Kistl.App.Base");
            this._isExportGuidSet = true;
            XmlStreamer.FromStream(ref this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        #endregion

    }
}