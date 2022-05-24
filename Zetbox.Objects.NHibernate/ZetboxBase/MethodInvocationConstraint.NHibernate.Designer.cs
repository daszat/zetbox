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
    [System.Diagnostics.DebuggerDisplay("MethodInvocationConstraint")]
    public class MethodInvocationConstraintNHibernateImpl : Zetbox.App.Base.ConstraintNHibernateImpl, MethodInvocationConstraint
    {
        private static readonly Guid _objectClassID = new Guid("2667704b-ea27-44ff-a6b2-0ef42ffccedc");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public MethodInvocationConstraintNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public MethodInvocationConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new MethodInvocationConstraintProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public MethodInvocationConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx, MethodInvocationConstraintProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly MethodInvocationConstraintProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_MethodInvocationConstraint")]
        public override async System.Threading.Tasks.Task<string> GetErrorText(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_MethodInvocationConstraint != null)
            {
                await OnGetErrorText_MethodInvocationConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = await base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<MethodInvocationConstraint> OnGetErrorText_MethodInvocationConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodInvocationConstraint> OnGetErrorText_MethodInvocationConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_MethodInvocationConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_MethodInvocationConstraint_CanExec != null)
				{
					OnGetErrorText_MethodInvocationConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodInvocationConstraint> OnGetErrorText_MethodInvocationConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_MethodInvocationConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_MethodInvocationConstraint_CanExecReason != null)
				{
					OnGetErrorText_MethodInvocationConstraint_CanExecReason(this, e);
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
        [EventBasedMethod("OnIsValid_MethodInvocationConstraint")]
        public override async System.Threading.Tasks.Task<bool> IsValid(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_MethodInvocationConstraint != null)
            {
                await OnIsValid_MethodInvocationConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = await base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event IsValid_Handler<MethodInvocationConstraint> OnIsValid_MethodInvocationConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MethodInvocationConstraint> OnIsValid_MethodInvocationConstraint_CanExec;

        [EventBasedMethod("OnIsValid_MethodInvocationConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_MethodInvocationConstraint_CanExec != null)
				{
					OnIsValid_MethodInvocationConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MethodInvocationConstraint> OnIsValid_MethodInvocationConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_MethodInvocationConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_MethodInvocationConstraint_CanExecReason != null)
				{
					OnIsValid_MethodInvocationConstraint_CanExecReason(this, e);
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
            return typeof(MethodInvocationConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (MethodInvocationConstraint)obj;
            var otherImpl = (MethodInvocationConstraintNHibernateImpl)obj;
            var me = (MethodInvocationConstraint)this;

        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
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
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_MethodInvocationConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodInvocationConstraint != null)
            {
                OnToString_MethodInvocationConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<MethodInvocationConstraint> OnToString_MethodInvocationConstraint;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_MethodInvocationConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_MethodInvocationConstraint != null)
            {
                OnObjectIsValid_MethodInvocationConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<MethodInvocationConstraint> OnObjectIsValid_MethodInvocationConstraint;

        [EventBasedMethod("OnNotifyPreSave_MethodInvocationConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_MethodInvocationConstraint != null) OnNotifyPreSave_MethodInvocationConstraint(this);
        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnNotifyPreSave_MethodInvocationConstraint;

        [EventBasedMethod("OnNotifyPostSave_MethodInvocationConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_MethodInvocationConstraint != null) OnNotifyPostSave_MethodInvocationConstraint(this);
        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnNotifyPostSave_MethodInvocationConstraint;

        [EventBasedMethod("OnNotifyCreated_MethodInvocationConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_MethodInvocationConstraint != null) OnNotifyCreated_MethodInvocationConstraint(this);
        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnNotifyCreated_MethodInvocationConstraint;

        [EventBasedMethod("OnNotifyDeleting_MethodInvocationConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_MethodInvocationConstraint != null) OnNotifyDeleting_MethodInvocationConstraint(this);


        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnNotifyDeleting_MethodInvocationConstraint;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class MethodInvocationConstraintProxy
            : Zetbox.App.Base.ConstraintNHibernateImpl.ConstraintProxy
        {
            public MethodInvocationConstraintProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(MethodInvocationConstraintNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(MethodInvocationConstraintProxy); } }

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