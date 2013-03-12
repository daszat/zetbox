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
    [System.Diagnostics.DebuggerDisplay("IsValidNamespaceConstraint")]
    public class IsValidNamespaceConstraintNHibernateImpl : Zetbox.App.Base.IsValidIdentifierConstraintNHibernateImpl, IsValidNamespaceConstraint
    {
        private static readonly Guid _objectClassID = new Guid("94916227-138b-49e5-b62e-b982a45a5c21");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public IsValidNamespaceConstraintNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public IsValidNamespaceConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new IsValidNamespaceConstraintProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public IsValidNamespaceConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx, IsValidNamespaceConstraintProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly IsValidNamespaceConstraintProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_IsValidNamespaceConstraint")]
        public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IsValidNamespaceConstraint != null)
            {
                OnGetErrorText_IsValidNamespaceConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<IsValidNamespaceConstraint> OnGetErrorText_IsValidNamespaceConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IsValidNamespaceConstraint> OnGetErrorText_IsValidNamespaceConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_IsValidNamespaceConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_IsValidNamespaceConstraint_CanExec != null)
				{
					OnGetErrorText_IsValidNamespaceConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IsValidNamespaceConstraint> OnGetErrorText_IsValidNamespaceConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_IsValidNamespaceConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_IsValidNamespaceConstraint_CanExecReason != null)
				{
					OnGetErrorText_IsValidNamespaceConstraint_CanExecReason(this, e);
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
        [EventBasedMethod("OnIsValid_IsValidNamespaceConstraint")]
        public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IsValidNamespaceConstraint != null)
            {
                OnIsValid_IsValidNamespaceConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event IsValid_Handler<IsValidNamespaceConstraint> OnIsValid_IsValidNamespaceConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IsValidNamespaceConstraint> OnIsValid_IsValidNamespaceConstraint_CanExec;

        [EventBasedMethod("OnIsValid_IsValidNamespaceConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_IsValidNamespaceConstraint_CanExec != null)
				{
					OnIsValid_IsValidNamespaceConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IsValidNamespaceConstraint> OnIsValid_IsValidNamespaceConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_IsValidNamespaceConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_IsValidNamespaceConstraint_CanExecReason != null)
				{
					OnIsValid_IsValidNamespaceConstraint_CanExecReason(this, e);
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
            return typeof(IsValidNamespaceConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (IsValidNamespaceConstraint)obj;
            var otherImpl = (IsValidNamespaceConstraintNHibernateImpl)obj;
            var me = (IsValidNamespaceConstraint)this;

        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_IsValidNamespaceConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IsValidNamespaceConstraint != null)
            {
                OnToString_IsValidNamespaceConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<IsValidNamespaceConstraint> OnToString_IsValidNamespaceConstraint;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_IsValidNamespaceConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_IsValidNamespaceConstraint != null)
            {
                OnObjectIsValid_IsValidNamespaceConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<IsValidNamespaceConstraint> OnObjectIsValid_IsValidNamespaceConstraint;

        [EventBasedMethod("OnNotifyPreSave_IsValidNamespaceConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_IsValidNamespaceConstraint != null) OnNotifyPreSave_IsValidNamespaceConstraint(this);
        }
        public static event ObjectEventHandler<IsValidNamespaceConstraint> OnNotifyPreSave_IsValidNamespaceConstraint;

        [EventBasedMethod("OnNotifyPostSave_IsValidNamespaceConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_IsValidNamespaceConstraint != null) OnNotifyPostSave_IsValidNamespaceConstraint(this);
        }
        public static event ObjectEventHandler<IsValidNamespaceConstraint> OnNotifyPostSave_IsValidNamespaceConstraint;

        [EventBasedMethod("OnNotifyCreated_IsValidNamespaceConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_IsValidNamespaceConstraint != null) OnNotifyCreated_IsValidNamespaceConstraint(this);
        }
        public static event ObjectEventHandler<IsValidNamespaceConstraint> OnNotifyCreated_IsValidNamespaceConstraint;

        [EventBasedMethod("OnNotifyDeleting_IsValidNamespaceConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_IsValidNamespaceConstraint != null) OnNotifyDeleting_IsValidNamespaceConstraint(this);


        }
        public static event ObjectEventHandler<IsValidNamespaceConstraint> OnNotifyDeleting_IsValidNamespaceConstraint;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class IsValidNamespaceConstraintProxy
            : Zetbox.App.Base.IsValidIdentifierConstraintNHibernateImpl.IsValidIdentifierConstraintProxy
        {
            public IsValidNamespaceConstraintProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(IsValidNamespaceConstraintNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(IsValidNamespaceConstraintProxy); } }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
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
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        #endregion

    }
}