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
    /// Item is readonly in view but changable on the server/client
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ViewReadOnlyConstraint")]
    public class ViewReadOnlyConstraintNHibernateImpl : Zetbox.App.Base.ReadOnlyConstraintNHibernateImpl, ViewReadOnlyConstraint
    {
        private static readonly Guid _objectClassID = new Guid("3ccd892a-17f7-4cf5-95bd-b25b3f6fa785");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public ViewReadOnlyConstraintNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public ViewReadOnlyConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new ViewReadOnlyConstraintProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public ViewReadOnlyConstraintNHibernateImpl(Func<IFrozenContext> lazyCtx, ViewReadOnlyConstraintProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly ViewReadOnlyConstraintProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_ViewReadOnlyConstraint")]
        public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_ViewReadOnlyConstraint != null)
            {
                OnGetErrorText_ViewReadOnlyConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<ViewReadOnlyConstraint> OnGetErrorText_ViewReadOnlyConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ViewReadOnlyConstraint> OnGetErrorText_ViewReadOnlyConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_ViewReadOnlyConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_ViewReadOnlyConstraint_CanExec != null)
				{
					OnGetErrorText_ViewReadOnlyConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ViewReadOnlyConstraint> OnGetErrorText_ViewReadOnlyConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_ViewReadOnlyConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_ViewReadOnlyConstraint_CanExecReason != null)
				{
					OnGetErrorText_ViewReadOnlyConstraint_CanExecReason(this, e);
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
        [EventBasedMethod("OnIsValid_ViewReadOnlyConstraint")]
        public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_ViewReadOnlyConstraint != null)
            {
                OnIsValid_ViewReadOnlyConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event IsValid_Handler<ViewReadOnlyConstraint> OnIsValid_ViewReadOnlyConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ViewReadOnlyConstraint> OnIsValid_ViewReadOnlyConstraint_CanExec;

        [EventBasedMethod("OnIsValid_ViewReadOnlyConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_ViewReadOnlyConstraint_CanExec != null)
				{
					OnIsValid_ViewReadOnlyConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ViewReadOnlyConstraint> OnIsValid_ViewReadOnlyConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_ViewReadOnlyConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_ViewReadOnlyConstraint_CanExecReason != null)
				{
					OnIsValid_ViewReadOnlyConstraint_CanExecReason(this, e);
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
            return typeof(ViewReadOnlyConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ViewReadOnlyConstraint)obj;
            var otherImpl = (ViewReadOnlyConstraintNHibernateImpl)obj;
            var me = (ViewReadOnlyConstraint)this;

        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
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
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_ViewReadOnlyConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ViewReadOnlyConstraint != null)
            {
                OnToString_ViewReadOnlyConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ViewReadOnlyConstraint> OnToString_ViewReadOnlyConstraint;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_ViewReadOnlyConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_ViewReadOnlyConstraint != null)
            {
                OnObjectIsValid_ViewReadOnlyConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<ViewReadOnlyConstraint> OnObjectIsValid_ViewReadOnlyConstraint;

        [EventBasedMethod("OnNotifyPreSave_ViewReadOnlyConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_ViewReadOnlyConstraint != null) OnNotifyPreSave_ViewReadOnlyConstraint(this);
        }
        public static event ObjectEventHandler<ViewReadOnlyConstraint> OnNotifyPreSave_ViewReadOnlyConstraint;

        [EventBasedMethod("OnNotifyPostSave_ViewReadOnlyConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_ViewReadOnlyConstraint != null) OnNotifyPostSave_ViewReadOnlyConstraint(this);
        }
        public static event ObjectEventHandler<ViewReadOnlyConstraint> OnNotifyPostSave_ViewReadOnlyConstraint;

        [EventBasedMethod("OnNotifyCreated_ViewReadOnlyConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_ViewReadOnlyConstraint != null) OnNotifyCreated_ViewReadOnlyConstraint(this);
        }
        public static event ObjectEventHandler<ViewReadOnlyConstraint> OnNotifyCreated_ViewReadOnlyConstraint;

        [EventBasedMethod("OnNotifyDeleting_ViewReadOnlyConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_ViewReadOnlyConstraint != null) OnNotifyDeleting_ViewReadOnlyConstraint(this);


        }
        public static event ObjectEventHandler<ViewReadOnlyConstraint> OnNotifyDeleting_ViewReadOnlyConstraint;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class ViewReadOnlyConstraintProxy
            : Zetbox.App.Base.ReadOnlyConstraintNHibernateImpl.ReadOnlyConstraintProxy
        {
            public ViewReadOnlyConstraintProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(ViewReadOnlyConstraintNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(ViewReadOnlyConstraintProxy); } }

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