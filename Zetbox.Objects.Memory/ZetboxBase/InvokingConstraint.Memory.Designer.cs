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
    [System.Diagnostics.DebuggerDisplay("InvokingConstraint")]
    public class InvokingConstraintMemoryImpl : Zetbox.App.Base.ConstraintMemoryImpl, InvokingConstraint
    {
        private static readonly Guid _objectClassID = new Guid("f5965ba1-6d47-4a4a-a143-eff28d7c66ad");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public InvokingConstraintMemoryImpl()
            : base(null)
        {
        }

        public InvokingConstraintMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_InvokingConstraint")]
        public override async System.Threading.Tasks.Task<string> GetErrorText(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_InvokingConstraint != null)
            {
                await OnGetErrorText_InvokingConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = await base.GetErrorText(constrainedObject, constrainedValue);
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
        public override async System.Threading.Tasks.Task<bool> IsValid(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_InvokingConstraint != null)
            {
                await OnIsValid_InvokingConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = await base.IsValid(constrainedObject, constrainedValue);
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
            var otherImpl = (InvokingConstraintMemoryImpl)obj;
            var me = (InvokingConstraint)this;

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
            // fix cached lists references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

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
            base.NotifyCreated();
            if (OnNotifyCreated_InvokingConstraint != null) OnNotifyCreated_InvokingConstraint(this);
        }
        public static event ObjectEventHandler<InvokingConstraint> OnNotifyCreated_InvokingConstraint;

        [EventBasedMethod("OnNotifyDeleting_InvokingConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_InvokingConstraint != null) OnNotifyDeleting_InvokingConstraint(this);
        }
        public static event ObjectEventHandler<InvokingConstraint> OnNotifyDeleting_InvokingConstraint;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

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