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
    /// Metadefinition Object for Object Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectReferenceParameter")]
    public class ObjectReferenceParameterMemoryImpl : Kistl.App.Base.BaseParameterMemoryImpl, ObjectReferenceParameter
    {
        private static readonly Guid _objectClassID = new Guid("3fb8bf11-cab6-478f-b9b8-3f6d70a70d37");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public ObjectReferenceParameterMemoryImpl()
            : base(null)
        {
        }

        public ObjectReferenceParameterMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Kistl-Typ des Parameters
        /// </summary>
	        // BEGIN Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ObjectClass
        // fkBackingName=_fk_ObjectClass; fkGuidBackingName=_fk_guid_ObjectClass;
        // referencedInterface=Kistl.App.Base.ObjectClass; moduleNamespace=Kistl.App.Base;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Kistl.Generator.Templates.Properties.DelegatingProperty
        public Kistl.App.Base.ObjectClass ObjectClass
        {
            get { return ObjectClassImpl; }
            set { ObjectClassImpl = (Kistl.App.Base.ObjectClassMemoryImpl)value; }
        }
        // END Kistl.Generator.Templates.Properties.DelegatingProperty

        private int? _fk_ObjectClass;

        private Guid? _fk_guid_ObjectClass = null;

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Kistl.App.Base.ObjectClassMemoryImpl ObjectClassImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Base.ObjectClassMemoryImpl __value;
                if (_fk_ObjectClass.HasValue)
                    __value = (Kistl.App.Base.ObjectClassMemoryImpl)Context.Find<Kistl.App.Base.ObjectClass>(_fk_ObjectClass.Value);
                else
                    __value = null;

                if (OnObjectClass_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Base.ObjectClass>(__value);
                    OnObjectClass_Getter(this, e);
                    __value = (Kistl.App.Base.ObjectClassMemoryImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_ObjectClass == null)
                    return;
                else if (value != null && value.ID == _fk_ObjectClass)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = ObjectClassImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("ObjectClass", __oldValue, __newValue);

                if (OnObjectClass_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnObjectClass_PreSetter(this, e);
                    __newValue = (Kistl.App.Base.ObjectClassMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_ObjectClass = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("ObjectClass", __oldValue, __newValue);

                if (OnObjectClass_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Base.ObjectClass>(__oldValue, __newValue);
                    OnObjectClass_PostSetter(this, e);
                }
            }
        }
        // END Kistl.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ObjectClass
		public static event PropertyGetterHandler<Kistl.App.Base.ObjectReferenceParameter, Kistl.App.Base.ObjectClass> OnObjectClass_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.ObjectReferenceParameter, Kistl.App.Base.ObjectClass> OnObjectClass_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.ObjectReferenceParameter, Kistl.App.Base.ObjectClass> OnObjectClass_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.ObjectReferenceParameter> OnObjectClass_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_ObjectReferenceParameter")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_ObjectReferenceParameter != null)
            {
                OnGetLabel_ObjectReferenceParameter(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<ObjectReferenceParameter> OnGetLabel_ObjectReferenceParameter;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ObjectReferenceParameter> OnGetLabel_ObjectReferenceParameter_CanExec;

        [EventBasedMethod("OnGetLabel_ObjectReferenceParameter_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_ObjectReferenceParameter_CanExec != null)
				{
					OnGetLabel_ObjectReferenceParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ObjectReferenceParameter> OnGetLabel_ObjectReferenceParameter_CanExecReason;

        [EventBasedMethod("OnGetLabel_ObjectReferenceParameter_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_ObjectReferenceParameter_CanExecReason != null)
				{
					OnGetLabel_ObjectReferenceParameter_CanExecReason(this, e);
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
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterType_ObjectReferenceParameter")]
        public override System.Type GetParameterType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_ObjectReferenceParameter != null)
            {
                OnGetParameterType_ObjectReferenceParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
        public static event GetParameterType_Handler<ObjectReferenceParameter> OnGetParameterType_ObjectReferenceParameter;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ObjectReferenceParameter> OnGetParameterType_ObjectReferenceParameter_CanExec;

        [EventBasedMethod("OnGetParameterType_ObjectReferenceParameter_CanExec")]
        public override bool GetParameterTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterType_ObjectReferenceParameter_CanExec != null)
				{
					OnGetParameterType_ObjectReferenceParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ObjectReferenceParameter> OnGetParameterType_ObjectReferenceParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterType_ObjectReferenceParameter_CanExecReason")]
        public override string GetParameterTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterType_ObjectReferenceParameter_CanExecReason != null)
				{
					OnGetParameterType_ObjectReferenceParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetParameterTypeString_ObjectReferenceParameter")]
        public override string GetParameterTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_ObjectReferenceParameter != null)
            {
                OnGetParameterTypeString_ObjectReferenceParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
        public static event GetParameterTypeString_Handler<ObjectReferenceParameter> OnGetParameterTypeString_ObjectReferenceParameter;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ObjectReferenceParameter> OnGetParameterTypeString_ObjectReferenceParameter_CanExec;

        [EventBasedMethod("OnGetParameterTypeString_ObjectReferenceParameter_CanExec")]
        public override bool GetParameterTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterTypeString_ObjectReferenceParameter_CanExec != null)
				{
					OnGetParameterTypeString_ObjectReferenceParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ObjectReferenceParameter> OnGetParameterTypeString_ObjectReferenceParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterTypeString_ObjectReferenceParameter_CanExecReason")]
        public override string GetParameterTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterTypeString_ObjectReferenceParameter_CanExecReason != null)
				{
					OnGetParameterTypeString_ObjectReferenceParameter_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(ObjectReferenceParameter);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ObjectReferenceParameter)obj;
            var otherImpl = (ObjectReferenceParameterMemoryImpl)obj;
            var me = (ObjectReferenceParameter)this;

            this._fk_ObjectClass = otherImpl._fk_ObjectClass;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "ObjectClass":
                    {
                        var __oldValue = _fk_ObjectClass;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("ObjectClass", __oldValue, __newValue);
                        _fk_ObjectClass = __newValue;
                        NotifyPropertyChanged("ObjectClass", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "ObjectClass":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_guid_ObjectClass.HasValue)
                ObjectClassImpl = (Kistl.App.Base.ObjectClassMemoryImpl)Context.FindPersistenceObject<Kistl.App.Base.ObjectClass>(_fk_guid_ObjectClass.Value);
            else
            if (_fk_ObjectClass.HasValue)
                ObjectClassImpl = (Kistl.App.Base.ObjectClassMemoryImpl)Context.Find<Kistl.App.Base.ObjectClass>(_fk_ObjectClass.Value);
            else
                ObjectClassImpl = null;
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // else
                    new PropertyDescriptorMemoryImpl<ObjectReferenceParameter, Kistl.App.Base.ObjectClass>(
                        lazyCtx,
                        new Guid("9bd64c60-7282-47f0-8069-528a175fcc92"),
                        "ObjectClass",
                        null,
                        obj => obj.ObjectClass,
                        (obj, val) => obj.ObjectClass = val,
						obj => OnObjectClass_IsValid), 
                    // position columns
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_ObjectReferenceParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectReferenceParameter != null)
            {
                OnToString_ObjectReferenceParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ObjectReferenceParameter> OnToString_ObjectReferenceParameter;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_ObjectReferenceParameter")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_ObjectReferenceParameter != null)
            {
                OnObjectIsValid_ObjectReferenceParameter(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<ObjectReferenceParameter> OnObjectIsValid_ObjectReferenceParameter;

        [EventBasedMethod("OnNotifyPreSave_ObjectReferenceParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_ObjectReferenceParameter != null) OnNotifyPreSave_ObjectReferenceParameter(this);
        }
        public static event ObjectEventHandler<ObjectReferenceParameter> OnNotifyPreSave_ObjectReferenceParameter;

        [EventBasedMethod("OnNotifyPostSave_ObjectReferenceParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_ObjectReferenceParameter != null) OnNotifyPostSave_ObjectReferenceParameter(this);
        }
        public static event ObjectEventHandler<ObjectReferenceParameter> OnNotifyPostSave_ObjectReferenceParameter;

        [EventBasedMethod("OnNotifyCreated_ObjectReferenceParameter")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("ObjectClass");
            base.NotifyCreated();
            if (OnNotifyCreated_ObjectReferenceParameter != null) OnNotifyCreated_ObjectReferenceParameter(this);
        }
        public static event ObjectEventHandler<ObjectReferenceParameter> OnNotifyCreated_ObjectReferenceParameter;

        [EventBasedMethod("OnNotifyDeleting_ObjectReferenceParameter")]
        public override void NotifyDeleting()
        {
            ObjectClass = null;
            base.NotifyDeleting();
            if (OnNotifyDeleting_ObjectReferenceParameter != null) OnNotifyDeleting_ObjectReferenceParameter(this);
        }
        public static event ObjectEventHandler<ObjectReferenceParameter> OnNotifyDeleting_ObjectReferenceParameter;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(ObjectClass != null ? ObjectClass.ID : (int?)null, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._fk_ObjectClass, binStream);
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
            XmlStreamer.ToStream(ObjectClass != null ? ObjectClass.ID : (int?)null, xml, "ObjectClass", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._fk_ObjectClass, xml, "ObjectClass", "Kistl.App.Base");
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(ObjectClass != null ? ObjectClass.ExportGuid : (Guid?)null, xml, "ObjectClass", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.FromStream(ref this._fk_guid_ObjectClass, xml, "ObjectClass", "Kistl.App.Base");
        }

        #endregion

    }
}