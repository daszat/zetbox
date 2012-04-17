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

    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NotNullableConstraint")]
    public class NotNullableConstraintMemoryImpl : Kistl.App.Base.ConstraintMemoryImpl, NotNullableConstraint
    {
        private static readonly Guid _objectClassID = new Guid("8604ef0c-f933-4f66-b7df-21d27c9003b2");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public NotNullableConstraintMemoryImpl()
            : base(null)
        {
        }

        public NotNullableConstraintMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_NotNullableConstraint")]
        public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_NotNullableConstraint != null)
            {
                OnGetErrorText_NotNullableConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<NotNullableConstraint> OnGetErrorText_NotNullableConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<NotNullableConstraint> OnGetErrorText_NotNullableConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_NotNullableConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_NotNullableConstraint_CanExec != null)
				{
					OnGetErrorText_NotNullableConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<NotNullableConstraint> OnGetErrorText_NotNullableConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_NotNullableConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_NotNullableConstraint_CanExecReason != null)
				{
					OnGetErrorText_NotNullableConstraint_CanExecReason(this, e);
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
        [EventBasedMethod("OnIsValid_NotNullableConstraint")]
        public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_NotNullableConstraint != null)
            {
                OnIsValid_NotNullableConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event IsValid_Handler<NotNullableConstraint> OnIsValid_NotNullableConstraint;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<NotNullableConstraint> OnIsValid_NotNullableConstraint_CanExec;

        [EventBasedMethod("OnIsValid_NotNullableConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_NotNullableConstraint_CanExec != null)
				{
					OnIsValid_NotNullableConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<NotNullableConstraint> OnIsValid_NotNullableConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_NotNullableConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_NotNullableConstraint_CanExecReason != null)
				{
					OnIsValid_NotNullableConstraint_CanExecReason(this, e);
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
            return typeof(NotNullableConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (NotNullableConstraint)obj;
            var otherImpl = (NotNullableConstraintMemoryImpl)obj;
            var me = (NotNullableConstraint)this;

        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange


        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_NotNullableConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_NotNullableConstraint != null)
            {
                OnToString_NotNullableConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<NotNullableConstraint> OnToString_NotNullableConstraint;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_NotNullableConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_NotNullableConstraint != null)
            {
                OnObjectIsValid_NotNullableConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<NotNullableConstraint> OnObjectIsValid_NotNullableConstraint;

        [EventBasedMethod("OnNotifyPreSave_NotNullableConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_NotNullableConstraint != null) OnNotifyPreSave_NotNullableConstraint(this);
        }
        public static event ObjectEventHandler<NotNullableConstraint> OnNotifyPreSave_NotNullableConstraint;

        [EventBasedMethod("OnNotifyPostSave_NotNullableConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_NotNullableConstraint != null) OnNotifyPostSave_NotNullableConstraint(this);
        }
        public static event ObjectEventHandler<NotNullableConstraint> OnNotifyPostSave_NotNullableConstraint;

        [EventBasedMethod("OnNotifyCreated_NotNullableConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_NotNullableConstraint != null) OnNotifyCreated_NotNullableConstraint(this);
        }
        public static event ObjectEventHandler<NotNullableConstraint> OnNotifyCreated_NotNullableConstraint;

        [EventBasedMethod("OnNotifyDeleting_NotNullableConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_NotNullableConstraint != null) OnNotifyDeleting_NotNullableConstraint(this);
        }
        public static event ObjectEventHandler<NotNullableConstraint> OnNotifyDeleting_NotNullableConstraint;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
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
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
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