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
    /// Metadefinition Object for Double Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DoubleProperty")]
    public class DoublePropertyMemoryImpl : Zetbox.App.Base.ValueTypePropertyMemoryImpl, DoubleProperty
    {
        private static readonly Guid _objectClassID = new Guid("404782b3-fbbc-4190-9b96-43dad7177090");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public DoublePropertyMemoryImpl()
            : base(null)
        {
        }

        public DoublePropertyMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Returns the translated description
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDescription_DoubleProperty")]
        public override async System.Threading.Tasks.Task<string> GetDescription()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDescription_DoubleProperty != null)
            {
                await OnGetDescription_DoubleProperty(this, e);
            }
            else
            {
                e.Result = await base.GetDescription();
            }
            return e.Result;
        }
        public static event GetDescription_Handler<DoubleProperty> OnGetDescription_DoubleProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DoubleProperty> OnGetDescription_DoubleProperty_CanExec;

        [EventBasedMethod("OnGetDescription_DoubleProperty_CanExec")]
        public override bool GetDescriptionCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDescription_DoubleProperty_CanExec != null)
				{
					OnGetDescription_DoubleProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDescriptionCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DoubleProperty> OnGetDescription_DoubleProperty_CanExecReason;

        [EventBasedMethod("OnGetDescription_DoubleProperty_CanExecReason")]
        public override string GetDescriptionCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDescription_DoubleProperty_CanExecReason != null)
				{
					OnGetDescription_DoubleProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetDescriptionCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// The element type for multi-valued properties. The property type string in all other cases.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetElementTypeString_DoubleProperty")]
        public override async System.Threading.Tasks.Task<string> GetElementTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetElementTypeString_DoubleProperty != null)
            {
                await OnGetElementTypeString_DoubleProperty(this, e);
            }
            else
            {
                e.Result = await base.GetElementTypeString();
            }
            return e.Result;
        }
        public static event GetElementTypeString_Handler<DoubleProperty> OnGetElementTypeString_DoubleProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DoubleProperty> OnGetElementTypeString_DoubleProperty_CanExec;

        [EventBasedMethod("OnGetElementTypeString_DoubleProperty_CanExec")]
        public override bool GetElementTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetElementTypeString_DoubleProperty_CanExec != null)
				{
					OnGetElementTypeString_DoubleProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DoubleProperty> OnGetElementTypeString_DoubleProperty_CanExecReason;

        [EventBasedMethod("OnGetElementTypeString_DoubleProperty_CanExecReason")]
        public override string GetElementTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetElementTypeString_DoubleProperty_CanExecReason != null)
				{
					OnGetElementTypeString_DoubleProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_DoubleProperty")]
        public override async System.Threading.Tasks.Task<string> GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_DoubleProperty != null)
            {
                await OnGetLabel_DoubleProperty(this, e);
            }
            else
            {
                e.Result = await base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<DoubleProperty> OnGetLabel_DoubleProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DoubleProperty> OnGetLabel_DoubleProperty_CanExec;

        [EventBasedMethod("OnGetLabel_DoubleProperty_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_DoubleProperty_CanExec != null)
				{
					OnGetLabel_DoubleProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DoubleProperty> OnGetLabel_DoubleProperty_CanExecReason;

        [EventBasedMethod("OnGetLabel_DoubleProperty_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_DoubleProperty_CanExecReason != null)
				{
					OnGetLabel_DoubleProperty_CanExecReason(this, e);
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
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetName_DoubleProperty")]
        public override async System.Threading.Tasks.Task<string> GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_DoubleProperty != null)
            {
                await OnGetName_DoubleProperty(this, e);
            }
            else
            {
                e.Result = await base.GetName();
            }
            return e.Result;
        }
        public static event GetName_Handler<DoubleProperty> OnGetName_DoubleProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DoubleProperty> OnGetName_DoubleProperty_CanExec;

        [EventBasedMethod("OnGetName_DoubleProperty_CanExec")]
        public override bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_DoubleProperty_CanExec != null)
				{
					OnGetName_DoubleProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DoubleProperty> OnGetName_DoubleProperty_CanExecReason;

        [EventBasedMethod("OnGetName_DoubleProperty_CanExecReason")]
        public override string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_DoubleProperty_CanExecReason != null)
				{
					OnGetName_DoubleProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyType_DoubleProperty")]
        public override async System.Threading.Tasks.Task<System.Type> GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_DoubleProperty != null)
            {
                await OnGetPropertyType_DoubleProperty(this, e);
            }
            else
            {
                e.Result = await base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<DoubleProperty> OnGetPropertyType_DoubleProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DoubleProperty> OnGetPropertyType_DoubleProperty_CanExec;

        [EventBasedMethod("OnGetPropertyType_DoubleProperty_CanExec")]
        public override bool GetPropertyTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyType_DoubleProperty_CanExec != null)
				{
					OnGetPropertyType_DoubleProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DoubleProperty> OnGetPropertyType_DoubleProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyType_DoubleProperty_CanExecReason")]
        public override string GetPropertyTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyType_DoubleProperty_CanExecReason != null)
				{
					OnGetPropertyType_DoubleProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyTypeString_DoubleProperty")]
        public override async System.Threading.Tasks.Task<string> GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_DoubleProperty != null)
            {
                await OnGetPropertyTypeString_DoubleProperty(this, e);
            }
            else
            {
                e.Result = await base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<DoubleProperty> OnGetPropertyTypeString_DoubleProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DoubleProperty> OnGetPropertyTypeString_DoubleProperty_CanExec;

        [EventBasedMethod("OnGetPropertyTypeString_DoubleProperty_CanExec")]
        public override bool GetPropertyTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyTypeString_DoubleProperty_CanExec != null)
				{
					OnGetPropertyTypeString_DoubleProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DoubleProperty> OnGetPropertyTypeString_DoubleProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyTypeString_DoubleProperty_CanExecReason")]
        public override string GetPropertyTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyTypeString_DoubleProperty_CanExecReason != null)
				{
					OnGetPropertyTypeString_DoubleProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(DoubleProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (DoubleProperty)obj;
            var otherImpl = (DoublePropertyMemoryImpl)obj;
            var me = (DoubleProperty)this;

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
        [EventBasedMethod("OnToString_DoubleProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DoubleProperty != null)
            {
                OnToString_DoubleProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DoubleProperty> OnToString_DoubleProperty;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_DoubleProperty")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_DoubleProperty != null)
            {
                OnObjectIsValid_DoubleProperty(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<DoubleProperty> OnObjectIsValid_DoubleProperty;

        [EventBasedMethod("OnNotifyPreSave_DoubleProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_DoubleProperty != null) OnNotifyPreSave_DoubleProperty(this);
        }
        public static event ObjectEventHandler<DoubleProperty> OnNotifyPreSave_DoubleProperty;

        [EventBasedMethod("OnNotifyPostSave_DoubleProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_DoubleProperty != null) OnNotifyPostSave_DoubleProperty(this);
        }
        public static event ObjectEventHandler<DoubleProperty> OnNotifyPostSave_DoubleProperty;

        [EventBasedMethod("OnNotifyCreated_DoubleProperty")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_DoubleProperty != null) OnNotifyCreated_DoubleProperty(this);
        }
        public static event ObjectEventHandler<DoubleProperty> OnNotifyCreated_DoubleProperty;

        [EventBasedMethod("OnNotifyDeleting_DoubleProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_DoubleProperty != null) OnNotifyDeleting_DoubleProperty(this);
        }
        public static event ObjectEventHandler<DoubleProperty> OnNotifyDeleting_DoubleProperty;

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