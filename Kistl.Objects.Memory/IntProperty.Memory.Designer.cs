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
    /// Metadefinition Object for Int Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IntProperty")]
    public class IntPropertyMemoryImpl : Kistl.App.Base.ValueTypePropertyMemoryImpl, IntProperty
    {
        private static readonly Guid _objectClassID = new Guid("6f818a25-4513-4fd3-8ed1-a92429d3910d");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public IntPropertyMemoryImpl()
            : base(null)
        {
        }

        public IntPropertyMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// The element type for multi-valued properties. The property type string in all other cases.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetElementTypeString_IntProperty")]
        public override string GetElementTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetElementTypeString_IntProperty != null)
            {
                OnGetElementTypeString_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetElementTypeString();
            }
            return e.Result;
        }
        public static event GetElementTypeString_Handler<IntProperty> OnGetElementTypeString_IntProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IntProperty> OnGetElementTypeString_IntProperty_CanExec;

        [EventBasedMethod("OnGetElementTypeString_IntProperty_CanExec")]
        public override bool GetElementTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetElementTypeString_IntProperty_CanExec != null)
				{
					OnGetElementTypeString_IntProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IntProperty> OnGetElementTypeString_IntProperty_CanExecReason;

        [EventBasedMethod("OnGetElementTypeString_IntProperty_CanExecReason")]
        public override string GetElementTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetElementTypeString_IntProperty_CanExecReason != null)
				{
					OnGetElementTypeString_IntProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_IntProperty")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_IntProperty != null)
            {
                OnGetLabel_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<IntProperty> OnGetLabel_IntProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IntProperty> OnGetLabel_IntProperty_CanExec;

        [EventBasedMethod("OnGetLabel_IntProperty_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_IntProperty_CanExec != null)
				{
					OnGetLabel_IntProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IntProperty> OnGetLabel_IntProperty_CanExecReason;

        [EventBasedMethod("OnGetLabel_IntProperty_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_IntProperty_CanExecReason != null)
				{
					OnGetLabel_IntProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetName_IntProperty")]
        public override string GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_IntProperty != null)
            {
                OnGetName_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetName();
            }
            return e.Result;
        }
        public static event GetName_Handler<IntProperty> OnGetName_IntProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IntProperty> OnGetName_IntProperty_CanExec;

        [EventBasedMethod("OnGetName_IntProperty_CanExec")]
        public override bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_IntProperty_CanExec != null)
				{
					OnGetName_IntProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IntProperty> OnGetName_IntProperty_CanExecReason;

        [EventBasedMethod("OnGetName_IntProperty_CanExecReason")]
        public override string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_IntProperty_CanExecReason != null)
				{
					OnGetName_IntProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyType_IntProperty")]
        public override System.Type GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_IntProperty != null)
            {
                OnGetPropertyType_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<IntProperty> OnGetPropertyType_IntProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IntProperty> OnGetPropertyType_IntProperty_CanExec;

        [EventBasedMethod("OnGetPropertyType_IntProperty_CanExec")]
        public override bool GetPropertyTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyType_IntProperty_CanExec != null)
				{
					OnGetPropertyType_IntProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IntProperty> OnGetPropertyType_IntProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyType_IntProperty_CanExecReason")]
        public override string GetPropertyTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyType_IntProperty_CanExecReason != null)
				{
					OnGetPropertyType_IntProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetPropertyTypeString_IntProperty")]
        public override string GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_IntProperty != null)
            {
                OnGetPropertyTypeString_IntProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<IntProperty> OnGetPropertyTypeString_IntProperty;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<IntProperty> OnGetPropertyTypeString_IntProperty_CanExec;

        [EventBasedMethod("OnGetPropertyTypeString_IntProperty_CanExec")]
        public override bool GetPropertyTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyTypeString_IntProperty_CanExec != null)
				{
					OnGetPropertyTypeString_IntProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<IntProperty> OnGetPropertyTypeString_IntProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyTypeString_IntProperty_CanExecReason")]
        public override string GetPropertyTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyTypeString_IntProperty_CanExecReason != null)
				{
					OnGetPropertyTypeString_IntProperty_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(IntProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (IntProperty)obj;
            var otherImpl = (IntPropertyMemoryImpl)obj;
            var me = (IntProperty)this;

        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }


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
        [EventBasedMethod("OnToString_IntProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IntProperty != null)
            {
                OnToString_IntProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<IntProperty> OnToString_IntProperty;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_IntProperty")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_IntProperty != null)
            {
                OnObjectIsValid_IntProperty(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<IntProperty> OnObjectIsValid_IntProperty;

        [EventBasedMethod("OnNotifyPreSave_IntProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_IntProperty != null) OnNotifyPreSave_IntProperty(this);
        }
        public static event ObjectEventHandler<IntProperty> OnNotifyPreSave_IntProperty;

        [EventBasedMethod("OnNotifyPostSave_IntProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_IntProperty != null) OnNotifyPostSave_IntProperty(this);
        }
        public static event ObjectEventHandler<IntProperty> OnNotifyPostSave_IntProperty;

        [EventBasedMethod("OnNotifyCreated_IntProperty")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_IntProperty != null) OnNotifyCreated_IntProperty(this);
        }
        public static event ObjectEventHandler<IntProperty> OnNotifyCreated_IntProperty;

        [EventBasedMethod("OnNotifyDeleting_IntProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_IntProperty != null) OnNotifyDeleting_IntProperty(this);
        }
        public static event ObjectEventHandler<IntProperty> OnNotifyDeleting_IntProperty;

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