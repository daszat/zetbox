// <autogenerated/>

namespace Kistl.App.GUI
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
    /// Filter configuration for filtering on a single value of a Property 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SinglePropertyFilterConfiguration")]
    public class SinglePropertyFilterConfigurationMemoryImpl : Kistl.App.GUI.PropertyFilterConfigurationMemoryImpl, SinglePropertyFilterConfiguration
    {
        private static readonly Guid _objectClassID = new Guid("0c65fbda-a2ac-475c-af94-ee705381ee08");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public SinglePropertyFilterConfigurationMemoryImpl()
            : base(null)
        {
        }

        public SinglePropertyFilterConfigurationMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnCreateFilterModel_SinglePropertyFilterConfiguration")]
        public override Kistl.API.IFilterModel CreateFilterModel()
        {
            var e = new MethodReturnEventArgs<Kistl.API.IFilterModel>();
            if (OnCreateFilterModel_SinglePropertyFilterConfiguration != null)
            {
                OnCreateFilterModel_SinglePropertyFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.CreateFilterModel();
            }
            return e.Result;
        }
        public static event CreateFilterModel_Handler<SinglePropertyFilterConfiguration> OnCreateFilterModel_SinglePropertyFilterConfiguration;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<SinglePropertyFilterConfiguration> OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExec;

        [EventBasedMethod("OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExec")]
        public override bool CreateFilterModelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExec != null)
				{
					OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.CreateFilterModelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<SinglePropertyFilterConfiguration> OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExecReason")]
        public override string CreateFilterModelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExecReason != null)
				{
					OnCreateFilterModel_SinglePropertyFilterConfiguration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.CreateFilterModelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_SinglePropertyFilterConfiguration")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_SinglePropertyFilterConfiguration != null)
            {
                OnGetLabel_SinglePropertyFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<SinglePropertyFilterConfiguration> OnGetLabel_SinglePropertyFilterConfiguration;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<SinglePropertyFilterConfiguration> OnGetLabel_SinglePropertyFilterConfiguration_CanExec;

        [EventBasedMethod("OnGetLabel_SinglePropertyFilterConfiguration_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_SinglePropertyFilterConfiguration_CanExec != null)
				{
					OnGetLabel_SinglePropertyFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<SinglePropertyFilterConfiguration> OnGetLabel_SinglePropertyFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnGetLabel_SinglePropertyFilterConfiguration_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_SinglePropertyFilterConfiguration_CanExecReason != null)
				{
					OnGetLabel_SinglePropertyFilterConfiguration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(SinglePropertyFilterConfiguration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (SinglePropertyFilterConfiguration)obj;
            var otherImpl = (SinglePropertyFilterConfigurationMemoryImpl)obj;
            var me = (SinglePropertyFilterConfiguration)this;

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
        [EventBasedMethod("OnToString_SinglePropertyFilterConfiguration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_SinglePropertyFilterConfiguration != null)
            {
                OnToString_SinglePropertyFilterConfiguration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<SinglePropertyFilterConfiguration> OnToString_SinglePropertyFilterConfiguration;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_SinglePropertyFilterConfiguration")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_SinglePropertyFilterConfiguration != null)
            {
                OnObjectIsValid_SinglePropertyFilterConfiguration(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<SinglePropertyFilterConfiguration> OnObjectIsValid_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyPreSave_SinglePropertyFilterConfiguration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_SinglePropertyFilterConfiguration != null) OnNotifyPreSave_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnNotifyPreSave_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyPostSave_SinglePropertyFilterConfiguration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_SinglePropertyFilterConfiguration != null) OnNotifyPostSave_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnNotifyPostSave_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyCreated_SinglePropertyFilterConfiguration")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_SinglePropertyFilterConfiguration != null) OnNotifyCreated_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnNotifyCreated_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnNotifyDeleting_SinglePropertyFilterConfiguration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_SinglePropertyFilterConfiguration != null) OnNotifyDeleting_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnNotifyDeleting_SinglePropertyFilterConfiguration;

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