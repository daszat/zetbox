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
    [System.Diagnostics.DebuggerDisplay("FullTextIndexConstraint")]
    public class FullTextIndexConstraintMemoryImpl : Zetbox.App.Base.IndexConstraintMemoryImpl, FullTextIndexConstraint
    {
        private static readonly Guid _objectClassID = new Guid("5a841301-7d78-436d-94dd-7cb505f14a40");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public FullTextIndexConstraintMemoryImpl()
            : base(null)
        {
        }

        public FullTextIndexConstraintMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_FullTextIndexConstraint")]
        public override string GetErrorText(Zetbox.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_FullTextIndexConstraint != null)
            {
                OnGetErrorText_FullTextIndexConstraint(this, e, constrainedObject);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<FullTextIndexConstraint> OnGetErrorText_FullTextIndexConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<FullTextIndexConstraint> OnGetErrorText_FullTextIndexConstraint_CanExec;

        [EventBasedMethod("OnGetErrorText_FullTextIndexConstraint_CanExec")]
        public override bool GetErrorTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetErrorText_FullTextIndexConstraint_CanExec != null)
				{
					OnGetErrorText_FullTextIndexConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetErrorTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<FullTextIndexConstraint> OnGetErrorText_FullTextIndexConstraint_CanExecReason;

        [EventBasedMethod("OnGetErrorText_FullTextIndexConstraint_CanExecReason")]
        public override string GetErrorTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetErrorText_FullTextIndexConstraint_CanExecReason != null)
				{
					OnGetErrorText_FullTextIndexConstraint_CanExecReason(this, e);
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
        [EventBasedMethod("OnIsValid_FullTextIndexConstraint")]
        public override bool IsValid(Zetbox.API.IDataObject constrainedObject)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_FullTextIndexConstraint != null)
            {
                OnIsValid_FullTextIndexConstraint(this, e, constrainedObject);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject);
            }
            return e.Result;
        }
        public static event IsValid_Handler<FullTextIndexConstraint> OnIsValid_FullTextIndexConstraint;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<FullTextIndexConstraint> OnIsValid_FullTextIndexConstraint_CanExec;

        [EventBasedMethod("OnIsValid_FullTextIndexConstraint_CanExec")]
        public override bool IsValidCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnIsValid_FullTextIndexConstraint_CanExec != null)
				{
					OnIsValid_FullTextIndexConstraint_CanExec(this, e);
				}
				else
				{
					e.Result = base.IsValidCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<FullTextIndexConstraint> OnIsValid_FullTextIndexConstraint_CanExecReason;

        [EventBasedMethod("OnIsValid_FullTextIndexConstraint_CanExecReason")]
        public override string IsValidCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnIsValid_FullTextIndexConstraint_CanExecReason != null)
				{
					OnIsValid_FullTextIndexConstraint_CanExecReason(this, e);
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
            return typeof(FullTextIndexConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (FullTextIndexConstraint)obj;
            var otherImpl = (FullTextIndexConstraintMemoryImpl)obj;
            var me = (FullTextIndexConstraint)this;

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
            // fix cached lists references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_FullTextIndexConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_FullTextIndexConstraint != null)
            {
                OnToString_FullTextIndexConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<FullTextIndexConstraint> OnToString_FullTextIndexConstraint;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_FullTextIndexConstraint")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_FullTextIndexConstraint != null)
            {
                OnObjectIsValid_FullTextIndexConstraint(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<FullTextIndexConstraint> OnObjectIsValid_FullTextIndexConstraint;

        [EventBasedMethod("OnNotifyPreSave_FullTextIndexConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_FullTextIndexConstraint != null) OnNotifyPreSave_FullTextIndexConstraint(this);
        }
        public static event ObjectEventHandler<FullTextIndexConstraint> OnNotifyPreSave_FullTextIndexConstraint;

        [EventBasedMethod("OnNotifyPostSave_FullTextIndexConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_FullTextIndexConstraint != null) OnNotifyPostSave_FullTextIndexConstraint(this);
        }
        public static event ObjectEventHandler<FullTextIndexConstraint> OnNotifyPostSave_FullTextIndexConstraint;

        [EventBasedMethod("OnNotifyCreated_FullTextIndexConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_FullTextIndexConstraint != null) OnNotifyCreated_FullTextIndexConstraint(this);
        }
        public static event ObjectEventHandler<FullTextIndexConstraint> OnNotifyCreated_FullTextIndexConstraint;

        [EventBasedMethod("OnNotifyDeleting_FullTextIndexConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_FullTextIndexConstraint != null) OnNotifyDeleting_FullTextIndexConstraint(this);
        }
        public static event ObjectEventHandler<FullTextIndexConstraint> OnNotifyDeleting_FullTextIndexConstraint;

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