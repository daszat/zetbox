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

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// Metadefinition Object for String Parameter.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="StringParameterEfImpl")]
    [System.Diagnostics.DebuggerDisplay("StringParameter")]
    public class StringParameterEfImpl : Zetbox.App.Base.BaseParameterEfImpl, StringParameter
    {
        private static readonly Guid _objectClassID = new Guid("d3eee1cb-313d-465a-8a06-732ac119bc75");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public StringParameterEfImpl()
            : base(null)
        {
        }

        public StringParameterEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_StringParameter")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_StringParameter != null)
            {
                OnGetLabel_StringParameter(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<StringParameter> OnGetLabel_StringParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<StringParameter> OnGetLabel_StringParameter_CanExec;

        [EventBasedMethod("OnGetLabel_StringParameter_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_StringParameter_CanExec != null)
				{
					OnGetLabel_StringParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<StringParameter> OnGetLabel_StringParameter_CanExecReason;

        [EventBasedMethod("OnGetLabel_StringParameter_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_StringParameter_CanExecReason != null)
				{
					OnGetLabel_StringParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterType_StringParameter")]
        public override System.Type GetParameterType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_StringParameter != null)
            {
                OnGetParameterType_StringParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
        public static event GetParameterType_Handler<StringParameter> OnGetParameterType_StringParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<StringParameter> OnGetParameterType_StringParameter_CanExec;

        [EventBasedMethod("OnGetParameterType_StringParameter_CanExec")]
        public override bool GetParameterTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterType_StringParameter_CanExec != null)
				{
					OnGetParameterType_StringParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<StringParameter> OnGetParameterType_StringParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterType_StringParameter_CanExecReason")]
        public override string GetParameterTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterType_StringParameter_CanExecReason != null)
				{
					OnGetParameterType_StringParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterTypeString_StringParameter")]
        public override string GetParameterTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_StringParameter != null)
            {
                OnGetParameterTypeString_StringParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
        public static event GetParameterTypeString_Handler<StringParameter> OnGetParameterTypeString_StringParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<StringParameter> OnGetParameterTypeString_StringParameter_CanExec;

        [EventBasedMethod("OnGetParameterTypeString_StringParameter_CanExec")]
        public override bool GetParameterTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterTypeString_StringParameter_CanExec != null)
				{
					OnGetParameterTypeString_StringParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<StringParameter> OnGetParameterTypeString_StringParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterTypeString_StringParameter_CanExecReason")]
        public override string GetParameterTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterTypeString_StringParameter_CanExecReason != null)
				{
					OnGetParameterTypeString_StringParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(StringParameter);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (StringParameter)obj;
            var otherImpl = (StringParameterEfImpl)obj;
            var me = (StringParameter)this;

        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

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
        [EventBasedMethod("OnToString_StringParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StringParameter != null)
            {
                OnToString_StringParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<StringParameter> OnToString_StringParameter;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_StringParameter")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_StringParameter != null)
            {
                OnObjectIsValid_StringParameter(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<StringParameter> OnObjectIsValid_StringParameter;

        [EventBasedMethod("OnNotifyPreSave_StringParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_StringParameter != null) OnNotifyPreSave_StringParameter(this);
        }
        public static event ObjectEventHandler<StringParameter> OnNotifyPreSave_StringParameter;

        [EventBasedMethod("OnNotifyPostSave_StringParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_StringParameter != null) OnNotifyPostSave_StringParameter(this);
        }
        public static event ObjectEventHandler<StringParameter> OnNotifyPostSave_StringParameter;

        [EventBasedMethod("OnNotifyCreated_StringParameter")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_StringParameter != null) OnNotifyCreated_StringParameter(this);
        }
        public static event ObjectEventHandler<StringParameter> OnNotifyCreated_StringParameter;

        [EventBasedMethod("OnNotifyDeleting_StringParameter")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_StringParameter != null) OnNotifyDeleting_StringParameter(this);
        }
        public static event ObjectEventHandler<StringParameter> OnNotifyDeleting_StringParameter;

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