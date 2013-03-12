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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("InvokingConstraint")]
    public class InvokingConstraintNHibernateImpl : Zetbox.App.Base.ConstraintNHibernateImpl, InvokingConstraint
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
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for GetErrorTextInvocation
        // fkBackingName=this.Proxy.GetErrorTextInvocation; fkGuidBackingName=_fk_guid_GetErrorTextInvocation;
        // referencedInterface=Zetbox.App.Base.ConstraintInvocation; moduleNamespace=Zetbox.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.ConstraintInvocation GetErrorTextInvocation
        {
            get
            {
                Zetbox.App.Base.ConstraintInvocationNHibernateImpl __value = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.GetErrorTextInvocation);

                if (OnGetErrorTextInvocation_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.ConstraintInvocation>(__value);
                    OnGetErrorTextInvocation_Getter(this, e);
                    __value = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.GetErrorTextInvocation == null)
                {
                    SetInitializedProperty("GetErrorTextInvocation");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.GetErrorTextInvocation);
                var __newValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("GetErrorTextInvocation");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("GetErrorTextInvocation", __oldValue, __newValue);

                if (OnGetErrorTextInvocation_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnGetErrorTextInvocation_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
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
                if(IsAttached) UpdateChangedInfo = true;

                if (OnGetErrorTextInvocation_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnGetErrorTextInvocation_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for GetErrorTextInvocation's id, used on dehydration only</summary>
        private int? _fk_GetErrorTextInvocation = null;

        /// <summary>Backing store for GetErrorTextInvocation's guid, used on import only</summary>
        private Guid? _fk_guid_GetErrorTextInvocation = null;

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for GetErrorTextInvocation
		public static event PropertyGetterHandler<Zetbox.App.Base.InvokingConstraint, Zetbox.App.Base.ConstraintInvocation> OnGetErrorTextInvocation_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.InvokingConstraint, Zetbox.App.Base.ConstraintInvocation> OnGetErrorTextInvocation_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.InvokingConstraint, Zetbox.App.Base.ConstraintInvocation> OnGetErrorTextInvocation_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.InvokingConstraint> OnGetErrorTextInvocation_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for IsValidInvocation
        // fkBackingName=this.Proxy.IsValidInvocation; fkGuidBackingName=_fk_guid_IsValidInvocation;
        // referencedInterface=Zetbox.App.Base.ConstraintInvocation; moduleNamespace=Zetbox.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Base.ConstraintInvocation IsValidInvocation
        {
            get
            {
                Zetbox.App.Base.ConstraintInvocationNHibernateImpl __value = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.IsValidInvocation);

                if (OnIsValidInvocation_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.ConstraintInvocation>(__value);
                    OnIsValidInvocation_Getter(this, e);
                    __value = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.IsValidInvocation == null)
                {
                    SetInitializedProperty("IsValidInvocation");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.IsValidInvocation);
                var __newValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("IsValidInvocation");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("IsValidInvocation", __oldValue, __newValue);

                if (OnIsValidInvocation_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnIsValidInvocation_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)e.Result;
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
                if(IsAttached) UpdateChangedInfo = true;

                if (OnIsValidInvocation_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.ConstraintInvocation>(__oldValue, __newValue);
                    OnIsValidInvocation_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for IsValidInvocation's id, used on dehydration only</summary>
        private int? _fk_IsValidInvocation = null;

        /// <summary>Backing store for IsValidInvocation's guid, used on import only</summary>
        private Guid? _fk_guid_IsValidInvocation = null;

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for IsValidInvocation
		public static event PropertyGetterHandler<Zetbox.App.Base.InvokingConstraint, Zetbox.App.Base.ConstraintInvocation> OnIsValidInvocation_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.InvokingConstraint, Zetbox.App.Base.ConstraintInvocation> OnIsValidInvocation_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.InvokingConstraint, Zetbox.App.Base.ConstraintInvocation> OnIsValidInvocation_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.InvokingConstraint> OnIsValidInvocation_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
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
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
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
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
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
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
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
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

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
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "GetErrorTextInvocation":
                    {
                        var __oldValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.GetErrorTextInvocation);
                        var __newValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)parentObj;
                        NotifyPropertyChanging("GetErrorTextInvocation", __oldValue, __newValue);
                        this.Proxy.GetErrorTextInvocation = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("GetErrorTextInvocation", __oldValue, __newValue);
                    }
                    break;
                case "IsValidInvocation":
                    {
                        var __oldValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.IsValidInvocation);
                        var __newValue = (Zetbox.App.Base.ConstraintInvocationNHibernateImpl)parentObj;
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
        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

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
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_GetErrorTextInvocation.HasValue)
                this.GetErrorTextInvocation = ((Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Base.ConstraintInvocation>(_fk_guid_GetErrorTextInvocation.Value));
            else
            if (_fk_GetErrorTextInvocation.HasValue)
                this.GetErrorTextInvocation = ((Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Base.ConstraintInvocation>(_fk_GetErrorTextInvocation.Value));
            else
                this.GetErrorTextInvocation = null;

            if (_fk_guid_IsValidInvocation.HasValue)
                this.IsValidInvocation = ((Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Base.ConstraintInvocation>(_fk_guid_IsValidInvocation.Value));
            else
            if (_fk_IsValidInvocation.HasValue)
                this.IsValidInvocation = ((Zetbox.App.Base.ConstraintInvocationNHibernateImpl)OurContext.FindPersistenceObject<Zetbox.App.Base.ConstraintInvocation>(_fk_IsValidInvocation.Value));
            else
                this.IsValidInvocation = null;
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
                    new PropertyDescriptorNHibernateImpl<InvokingConstraint, Zetbox.App.Base.ConstraintInvocation>(
                        lazyCtx,
                        new Guid("3b5d70f7-b6fd-4e39-b912-5a644a5de716"),
                        "GetErrorTextInvocation",
                        null,
                        obj => obj.GetErrorTextInvocation,
                        (obj, val) => obj.GetErrorTextInvocation = val,
						obj => OnGetErrorTextInvocation_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<InvokingConstraint, Zetbox.App.Base.ConstraintInvocation>(
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
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

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
            base.NotifyDeleting();
            if (OnNotifyDeleting_InvokingConstraint != null) OnNotifyDeleting_InvokingConstraint(this);

            // FK_Constraint_invokes_GetErrorTextInvocation
            if (GetErrorTextInvocation != null) {
                ((NHibernatePersistenceObject)GetErrorTextInvocation).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)GetErrorTextInvocation);
            }
            // FK_Constraint_invokes_IsValidInvocation
            if (IsValidInvocation != null) {
                ((NHibernatePersistenceObject)IsValidInvocation).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)IsValidInvocation);
            }

            GetErrorTextInvocation = null;
            IsValidInvocation = null;
        }
        public static event ObjectEventHandler<InvokingConstraint> OnNotifyDeleting_InvokingConstraint;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class InvokingConstraintProxy
            : Zetbox.App.Base.ConstraintNHibernateImpl.ConstraintProxy
        {
            public InvokingConstraintProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(InvokingConstraintNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(InvokingConstraintProxy); } }

            public virtual Zetbox.App.Base.ConstraintInvocationNHibernateImpl.ConstraintInvocationProxy GetErrorTextInvocation { get; set; }

            public virtual Zetbox.App.Base.ConstraintInvocationNHibernateImpl.ConstraintInvocationProxy IsValidInvocation { get; set; }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.GetErrorTextInvocation != null ? OurContext.GetIdFromProxy(this.Proxy.GetErrorTextInvocation) : (int?)null);
            binStream.Write(this.Proxy.IsValidInvocation != null ? OurContext.GetIdFromProxy(this.Proxy.IsValidInvocation) : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            binStream.Read(out this._fk_GetErrorTextInvocation);
            binStream.Read(out this._fk_IsValidInvocation);
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this.Proxy.GetErrorTextInvocation != null ? this.Proxy.GetErrorTextInvocation.ExportGuid : (Guid?)null, xml, "GetErrorTextInvocation", "Zetbox.App.Base");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this.Proxy.IsValidInvocation != null ? this.Proxy.IsValidInvocation.ExportGuid : (Guid?)null, xml, "IsValidInvocation", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|GetErrorTextInvocation":
                this._fk_guid_GetErrorTextInvocation = XmlStreamer.ReadNullableGuid(xml);
                break;
            case "Zetbox.App.Base|IsValidInvocation":
                this._fk_guid_IsValidInvocation = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}