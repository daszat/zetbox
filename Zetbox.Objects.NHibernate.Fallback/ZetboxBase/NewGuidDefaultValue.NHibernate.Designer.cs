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
    /// Creates a new Guid
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NewGuidDefaultValue")]
    public class NewGuidDefaultValueNHibernateImpl : Zetbox.App.Base.DefaultPropertyValueNHibernateImpl, NewGuidDefaultValue
    {
        private static readonly Guid _objectClassID = new Guid("a9fc1ec8-a91e-4569-b311-ec85c22a15c3");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public NewGuidDefaultValueNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public NewGuidDefaultValueNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new NewGuidDefaultValueProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public NewGuidDefaultValueNHibernateImpl(Func<IFrozenContext> lazyCtx, NewGuidDefaultValueProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly NewGuidDefaultValueProxy Proxy;

        /// <summary>
        /// GetDefaultValue
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDefaultValue_NewGuidDefaultValue")]
        public override System.Object GetDefaultValue()
        {
            var e = new MethodReturnEventArgs<System.Object>();
            if (OnGetDefaultValue_NewGuidDefaultValue != null)
            {
                OnGetDefaultValue_NewGuidDefaultValue(this, e);
            }
            else
            {
                e.Result = base.GetDefaultValue();
            }
            return e.Result;
        }
        public static event GetDefaultValue_Handler<NewGuidDefaultValue> OnGetDefaultValue_NewGuidDefaultValue;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<NewGuidDefaultValue> OnGetDefaultValue_NewGuidDefaultValue_CanExec;

        [EventBasedMethod("OnGetDefaultValue_NewGuidDefaultValue_CanExec")]
        public override bool GetDefaultValueCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDefaultValue_NewGuidDefaultValue_CanExec != null)
				{
					OnGetDefaultValue_NewGuidDefaultValue_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDefaultValueCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<NewGuidDefaultValue> OnGetDefaultValue_NewGuidDefaultValue_CanExecReason;

        [EventBasedMethod("OnGetDefaultValue_NewGuidDefaultValue_CanExecReason")]
        public override string GetDefaultValueCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDefaultValue_NewGuidDefaultValue_CanExecReason != null)
				{
					OnGetDefaultValue_NewGuidDefaultValue_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetDefaultValueCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(NewGuidDefaultValue);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (NewGuidDefaultValue)obj;
            var otherImpl = (NewGuidDefaultValueNHibernateImpl)obj;
            var me = (NewGuidDefaultValue)this;

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
        [EventBasedMethod("OnToString_NewGuidDefaultValue")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_NewGuidDefaultValue != null)
            {
                OnToString_NewGuidDefaultValue(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<NewGuidDefaultValue> OnToString_NewGuidDefaultValue;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_NewGuidDefaultValue")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_NewGuidDefaultValue != null)
            {
                OnObjectIsValid_NewGuidDefaultValue(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<NewGuidDefaultValue> OnObjectIsValid_NewGuidDefaultValue;

        [EventBasedMethod("OnNotifyPreSave_NewGuidDefaultValue")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_NewGuidDefaultValue != null) OnNotifyPreSave_NewGuidDefaultValue(this);
        }
        public static event ObjectEventHandler<NewGuidDefaultValue> OnNotifyPreSave_NewGuidDefaultValue;

        [EventBasedMethod("OnNotifyPostSave_NewGuidDefaultValue")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_NewGuidDefaultValue != null) OnNotifyPostSave_NewGuidDefaultValue(this);
        }
        public static event ObjectEventHandler<NewGuidDefaultValue> OnNotifyPostSave_NewGuidDefaultValue;

        [EventBasedMethod("OnNotifyCreated_NewGuidDefaultValue")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_NewGuidDefaultValue != null) OnNotifyCreated_NewGuidDefaultValue(this);
        }
        public static event ObjectEventHandler<NewGuidDefaultValue> OnNotifyCreated_NewGuidDefaultValue;

        [EventBasedMethod("OnNotifyDeleting_NewGuidDefaultValue")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_NewGuidDefaultValue != null) OnNotifyDeleting_NewGuidDefaultValue(this);


        }
        public static event ObjectEventHandler<NewGuidDefaultValue> OnNotifyDeleting_NewGuidDefaultValue;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class NewGuidDefaultValueProxy
            : Zetbox.App.Base.DefaultPropertyValueNHibernateImpl.DefaultPropertyValueProxy
        {
            public NewGuidDefaultValueProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(NewGuidDefaultValueNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(NewGuidDefaultValueProxy); } }

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