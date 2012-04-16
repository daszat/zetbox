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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("InvokingConstraint")]
    public class InvokingConstraintNHibernateImpl : Kistl.App.Base.ConstraintNHibernateImpl, InvokingConstraint
    {
        private static readonly Guid _objectClassID = new Guid("f5965ba1-6d47-4a4a-a143-eff28d7c66ad");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public InvokingConstraintNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public InvokingConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new InvokingConstraintProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public InvokingConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx, InvokingConstraintProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly InvokingConstraintProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for GetErrorTextInvocation
        // fkBackingName=this.Proxy.GetErrorTextInvocation; fkGuidBackingName=_fk_guid_GetErrorTextInvocation;
        // referencedInterface=Kistl.App.Base.ConstraintInvocation; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ConstraintInvocation GetErrorTextInvocation
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.ConstraintInvocationNHibernateImpl __value = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.GetErrorTextInvocation);

                if (OnGetErrorTextInvocation_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.ConstraintInvocation>(__value);
                    OnGetErrorTextInvocation_Getter(this, e);
                    __value = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.GetErrorTextInvocation == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.GetErrorTextInvocation);
                var __newValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("GetErrorTextInvocation", __oldValue, __newValue);

                if (OnGetErrorTextInvocation_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnGetErrorTextInvocation_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.GetErrorTextInvocation = null;
                }
                else
                {
                    this.Proxy.GetErrorTextInvocation = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("GetErrorTextInvocation", __oldValue, __newValue);

                if (OnGetErrorTextInvocation_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnGetErrorTextInvocation_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for GetErrorTextInvocation's id, used on dehydration only</summary>
        private int? _fk_GetErrorTextInvocation = null;

        /// <summary>Backing store for GetErrorTextInvocation's guid, used on import only</summary>
        private Guid? _fk_guid_GetErrorTextInvocation = null;

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for GetErrorTextInvocation
		public static event PropertyGetterHandler<Kistl.App.Base.InvokingConstraint, Kistl.App.Base.ConstraintInvocation> OnGetErrorTextInvocation_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.InvokingConstraint, Kistl.App.Base.ConstraintInvocation> OnGetErrorTextInvocation_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.InvokingConstraint, Kistl.App.Base.ConstraintInvocation> OnGetErrorTextInvocation_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.InvokingConstraint> OnGetErrorTextInvocation_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for IsValidInvocation
        // fkBackingName=this.Proxy.IsValidInvocation; fkGuidBackingName=_fk_guid_IsValidInvocation;
        // referencedInterface=Kistl.App.Base.ConstraintInvocation; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ConstraintInvocation IsValidInvocation
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.ConstraintInvocationNHibernateImpl __value = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.IsValidInvocation);

                if (OnIsValidInvocation_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.ConstraintInvocation>(__value);
                    OnIsValidInvocation_Getter(this, e);
                    __value = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.IsValidInvocation == null)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.IsValidInvocation);
                var __newValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                    return;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("IsValidInvocation", __oldValue, __newValue);

                if (OnIsValidInvocation_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnIsValidInvocation_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.IsValidInvocation = null;
                }
                else
                {
                    this.Proxy.IsValidInvocation = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("IsValidInvocation", __oldValue, __newValue);

                if (OnIsValidInvocation_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnIsValidInvocation_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for IsValidInvocation's id, used on dehydration only</summary>
        private int? _fk_IsValidInvocation = null;

        /// <summary>Backing store for IsValidInvocation's guid, used on import only</summary>
        private Guid? _fk_guid_IsValidInvocation = null;

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for IsValidInvocation
		public static event PropertyGetterHandler<Kistl.App.Base.InvokingConstraint, Kistl.App.Base.ConstraintInvocation> OnIsValidInvocation_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.InvokingConstraint, Kistl.App.Base.ConstraintInvocation> OnIsValidInvocation_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.InvokingConstraint, Kistl.App.Base.ConstraintInvocation> OnIsValidInvocation_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.InvokingConstraint> OnIsValidInvocation_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_InvokingConstraint")]
        public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_InvokingConstraint != null)
            {
                OnGetErrorText_InvokingConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<InvokingConstraint> OnGetErrorText_InvokingConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<InvokingConstraint> OnGetErrorText_InvokingConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_InvokingConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_InvokingConstraint_CanExec != null)
				{
					OnGetErrorText_InvokingConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<InvokingConstraint> OnGetErrorText_InvokingConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_InvokingConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_InvokingConstraint_CanExecReason != null)
				{
					OnGetErrorText_InvokingConstraint_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnIsValid_InvokingConstraint")]
        public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_InvokingConstraint != null)
            {
                OnIsValid_InvokingConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event IsValid_Handler<InvokingConstraint> OnIsValid_InvokingConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<InvokingConstraint> OnIsValid_InvokingConstraint_CanExec;

        [EventBasedMethod("OnIsValid_InvokingConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_InvokingConstraint_CanExec != null)
				{
					OnIsValid_InvokingConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<InvokingConstraint> OnIsValid_InvokingConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_InvokingConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_InvokingConstraint_CanExecReason != null)
				{
					OnIsValid_InvokingConstraint_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(InvokingConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (InvokingConstraint)obj;
            var otherImpl = (InvokingConstraintNHibernateImpl)obj;
            var me = (InvokingConstraint)this;

            this._fk_GetErrorTextInvocation = otherImpl._fk_GetErrorTextInvocation;
            this._fk_IsValidInvocation = otherImpl._fk_IsValidInvocation;
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
                case "GetErrorTextInvocation":
                    {
                        var __oldValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.GetErrorTextInvocation);
                        var __newValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)parentObj;
                        NotifyPropertyChanging("GetErrorTextInvocation", __oldValue, __newValue);
                        this.Proxy.GetErrorTextInvocation = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("GetErrorTextInvocation", __oldValue, __newValue);
                    }
                    break;
                case "IsValidInvocation":
                    {
                        var __oldValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.IsValidInvocation);
                        var __newValue = (Kistl.App.Base.ConstraintInvocationNHibernateImpl)parentObj;
                        NotifyPropertyChanging("IsValidInvocation", __oldValue, __newValue);
                        this.Proxy.IsValidInvocation = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("IsValidInvocation", __oldValue, __newValue);
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
                case "GetErrorTextInvocation":
                case "IsValidInvocation":
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

            if (_fk_guid_GetErrorTextInvocation.HasValue)
                this.GetErrorTextInvocation = ((Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.ConstraintInvocation>(_fk_guid_GetErrorTextInvocation.Value));
            else
            if (_fk_GetErrorTextInvocation.HasValue)
                this.GetErrorTextInvocation = ((Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.ConstraintInvocation>(_fk_GetErrorTextInvocation.Value));
            else
                this.GetErrorTextInvocation = null;

            if (_fk_guid_IsValidInvocation.HasValue)
                this.IsValidInvocation = ((Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.ConstraintInvocation>(_fk_guid_IsValidInvocation.Value));
            else
            if (_fk_IsValidInvocation.HasValue)
                this.IsValidInvocation = ((Kistl.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Kistl.App.Base.ConstraintInvocation>(_fk_IsValidInvocation.Value));
            else
                this.IsValidInvocation = null;
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
                    new PropertyDescriptorNHibernateImpl<InvokingConstraint, Kistl.App.Base.ConstraintInvocation>(
                        lazyCtx,
                        new Guid("3b5d70f7-b6fd-4e39-b912-5a644a5de716"),
                        "GetErrorTextInvocation",
                        null,
                        obj => obj.GetErrorTextInvocation,
                        (obj, val) => obj.GetErrorTextInvocation = val,
						obj => OnGetErrorTextInvocation_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<InvokingConstraint, Kistl.App.Base.ConstraintInvocation>(
                        lazyCtx,
                        new Guid("3c98da56-1c21-4849-87b1-81bf72d17e70"),
                        "IsValidInvocation",
                        null,
                        obj => obj.IsValidInvocation,
                        (obj, val) => obj.IsValidInvocation = val,
						obj => OnIsValidInvocation_IsValid), 
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
        [EventBasedMethod("OnToString_InvokingConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_InvokingConstraint != null)
            {
                OnToString_InvokingConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<InvokingConstraint> OnToString_InvokingConstraint;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_InvokingConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_InvokingConstraint != null)
            {
                OnObjectIsValid_InvokingConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<InvokingConstraint> OnObjectIsValid_InvokingConstraint;

        [EventBasedMethod("OnNotifyPreSave_InvokingConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_InvokingConstraint != null) OnNotifyPreSave_InvokingConstraint(this);
        }
        public static event ObjectEventHandler<InvokingConstraint> OnNotifyPreSave_InvokingConstraint;

        [EventBasedMethod("OnNotifyPostSave_InvokingConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_InvokingConstraint != null) OnNotifyPostSave_InvokingConstraint(this);
        }
        public static event ObjectEventHandler<InvokingConstraint> OnNotifyPostSave_InvokingConstraint;

        [EventBasedMethod("OnNotifyCreated_InvokingConstraint")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("GetErrorTextInvocation");
            SetNotInitializedProperty("IsValidInvocation");
            base.NotifyCreated();
            if (OnNotifyCreated_InvokingConstraint != null) OnNotifyCreated_InvokingConstraint(this);
        }
        public static event ObjectEventHandler<InvokingConstraint> OnNotifyCreated_InvokingConstraint;

        [EventBasedMethod("OnNotifyDeleting_InvokingConstraint")]
        public override void NotifyDeleting()
        {
            GetErrorTextInvocation = null;
            IsValidInvocation = null;
            base.NotifyDeleting();
            if (OnNotifyDeleting_InvokingConstraint != null) OnNotifyDeleting_InvokingConstraint(this);
        }
        public static event ObjectEventHandler<InvokingConstraint> OnNotifyDeleting_InvokingConstraint;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            // Follow Constraint_invokes_GetErrorTextInvocation
            if (this.GetErrorTextInvocation != null && this.GetErrorTextInvocation.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.GetErrorTextInvocation);

            // Follow Constraint_invokes_IsValidInvocation
            if (this.IsValidInvocation != null && this.IsValidInvocation.ObjectState == DataObjectState.Deleted)
                result.Add((NHibernatePersistenceObject)this.IsValidInvocation);

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            return result;
        }


        public class InvokingConstraintProxy
            : Kistl.App.Base.ConstraintNHibernateImpl.ConstraintProxy
        {
            public InvokingConstraintProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(InvokingConstraintNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(InvokingConstraintProxy); } }

            public virtual Kistl.App.Base.ConstraintInvocationNHibernateImpl.ConstraintInvocationProxy GetErrorTextInvocation { get; set; }

            public virtual Kistl.App.Base.ConstraintInvocationNHibernateImpl.ConstraintInvocationProxy IsValidInvocation { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this.Proxy.GetErrorTextInvocation != null ? OurContext.GetIdFromProxy(this.Proxy.GetErrorTextInvocation) : (int?)null, binStream);
            BinarySerializer.ToStream(this.Proxy.IsValidInvocation != null ? OurContext.GetIdFromProxy(this.Proxy.IsValidInvocation) : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_GetErrorTextInvocation, binStream);
            BinarySerializer.FromStream(out this._fk_IsValidInvocation, binStream);
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
            XmlStreamer.ToStream(this.Proxy.GetErrorTextInvocation != null ? OurContext.GetIdFromProxy(this.Proxy.GetErrorTextInvocation) : (int?)null, xml, "GetErrorTextInvocation", "Kistl.App.Base");
            XmlStreamer.ToStream(this.Proxy.IsValidInvocation != null ? OurContext.GetIdFromProxy(this.Proxy.IsValidInvocation) : (int?)null, xml, "IsValidInvocation", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_GetErrorTextInvocation, xml, "GetErrorTextInvocation", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_IsValidInvocation, xml, "IsValidInvocation", "Kistl.App.Base");
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.GetErrorTextInvocation != null ? this.Proxy.GetErrorTextInvocation.ExportGuid : (Guid?)null, xml, "GetErrorTextInvocation", "Kistl.App.Base");
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this.Proxy.IsValidInvocation != null ? this.Proxy.IsValidInvocation.ExportGuid : (Guid?)null, xml, "IsValidInvocation", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.FromStream(ref this._fk_guid_GetErrorTextInvocation, xml, "GetErrorTextInvocation", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_guid_IsValidInvocation, xml, "IsValidInvocation", "Kistl.App.Base");
        }

        #endregion

    }
}