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
    /// Metadefinition Object for Enum Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EnumParameter")]
    public class EnumParameterMemoryImpl : Zetbox.App.Base.BaseParameterMemoryImpl, EnumParameter
    {
        private static readonly Guid _objectClassID = new Guid("041eaa58-84cb-405f-a6ea-c3d77e4acd82");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public EnumParameterMemoryImpl()
            : base(null)
        {
        }

        public EnumParameterMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Enumeration
        // fkBackingName=_fk_Enumeration; fkGuidBackingName=_fk_guid_Enumeration;
        // referencedInterface=Zetbox.App.Base.Enumeration; moduleNamespace=Zetbox.App.Base;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Base.Enumeration Enumeration
        {
            get { return EnumerationImpl; }
            set { EnumerationImpl = (Zetbox.App.Base.EnumerationMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? __fk_EnumerationCache;

        private int? _fk_Enumeration {
            get
            {
                return __fk_EnumerationCache;
            }
            set
            {
                __fk_EnumerationCache = value;
                // Recreate task to clear it's cache
                _triggerFetchEnumerationTask = null;
            }
        }

        /// <summary>ForeignKey Property for Enumeration's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Enumeration
		{
			get { return _fk_Enumeration; }
			set { _fk_Enumeration = value; }
		}

        private Guid? _fk_guid_Enumeration = null;

        Zetbox.API.Async.ZbTask<Zetbox.App.Base.Enumeration> _triggerFetchEnumerationTask;
        public Zetbox.API.Async.ZbTask<Zetbox.App.Base.Enumeration> TriggerFetchEnumerationAsync()
        {
            if (_triggerFetchEnumerationTask != null) return _triggerFetchEnumerationTask;

            if (_fk_Enumeration.HasValue)
                _triggerFetchEnumerationTask = Context.FindAsync<Zetbox.App.Base.Enumeration>(_fk_Enumeration.Value);
            else
                _triggerFetchEnumerationTask = new Zetbox.API.Async.ZbTask<Zetbox.App.Base.Enumeration>(Zetbox.API.Async.ZbTask.Synchron, () => null);

            _triggerFetchEnumerationTask.OnResult(t =>
            {
                if (OnEnumeration_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Base.Enumeration>(t.Result);
                    OnEnumeration_Getter(this, e);
                    t.Result = e.Result;
                }
            });

            return _triggerFetchEnumerationTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Base.EnumerationMemoryImpl EnumerationImpl
        {
            get
            {
                return (Zetbox.App.Base.EnumerationMemoryImpl)TriggerFetchEnumerationAsync().Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_Enumeration == null) || (value != null && value.ID == _fk_Enumeration))
                {
                    SetInitializedProperty("Enumeration");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = EnumerationImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Enumeration", __oldValue, __newValue);

                if (OnEnumeration_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Base.Enumeration>(__oldValue, __newValue);
                    OnEnumeration_PreSetter(this, e);
                    __newValue = (Zetbox.App.Base.EnumerationMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_Enumeration = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Enumeration", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnEnumeration_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Base.Enumeration>(__oldValue, __newValue);
                    OnEnumeration_PostSetter(this, e);
                }
            }
        }
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Enumeration
		public static event PropertyGetterHandler<Zetbox.App.Base.EnumParameter, Zetbox.App.Base.Enumeration> OnEnumeration_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.EnumParameter, Zetbox.App.Base.Enumeration> OnEnumeration_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.EnumParameter, Zetbox.App.Base.Enumeration> OnEnumeration_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.EnumParameter> OnEnumeration_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_EnumParameter")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_EnumParameter != null)
            {
                OnGetLabel_EnumParameter(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<EnumParameter> OnGetLabel_EnumParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumParameter> OnGetLabel_EnumParameter_CanExec;

        [EventBasedMethod("OnGetLabel_EnumParameter_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_EnumParameter_CanExec != null)
				{
					OnGetLabel_EnumParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumParameter> OnGetLabel_EnumParameter_CanExecReason;

        [EventBasedMethod("OnGetLabel_EnumParameter_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_EnumParameter_CanExecReason != null)
				{
					OnGetLabel_EnumParameter_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetParameterType_EnumParameter")]
        public override System.Type GetParameterType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_EnumParameter != null)
            {
                OnGetParameterType_EnumParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
        public static event GetParameterType_Handler<EnumParameter> OnGetParameterType_EnumParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumParameter> OnGetParameterType_EnumParameter_CanExec;

        [EventBasedMethod("OnGetParameterType_EnumParameter_CanExec")]
        public override bool GetParameterTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterType_EnumParameter_CanExec != null)
				{
					OnGetParameterType_EnumParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumParameter> OnGetParameterType_EnumParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterType_EnumParameter_CanExecReason")]
        public override string GetParameterTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterType_EnumParameter_CanExecReason != null)
				{
					OnGetParameterType_EnumParameter_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetParameterTypeString_EnumParameter")]
        public override string GetParameterTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_EnumParameter != null)
            {
                OnGetParameterTypeString_EnumParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
        public static event GetParameterTypeString_Handler<EnumParameter> OnGetParameterTypeString_EnumParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<EnumParameter> OnGetParameterTypeString_EnumParameter_CanExec;

        [EventBasedMethod("OnGetParameterTypeString_EnumParameter_CanExec")]
        public override bool GetParameterTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterTypeString_EnumParameter_CanExec != null)
				{
					OnGetParameterTypeString_EnumParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<EnumParameter> OnGetParameterTypeString_EnumParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterTypeString_EnumParameter_CanExecReason")]
        public override string GetParameterTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterTypeString_EnumParameter_CanExecReason != null)
				{
					OnGetParameterTypeString_EnumParameter_CanExecReason(this, e);
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
            return typeof(EnumParameter);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (EnumParameter)obj;
            var otherImpl = (EnumParameterMemoryImpl)obj;
            var me = (EnumParameter)this;

            this._fk_Enumeration = otherImpl._fk_Enumeration;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "Enumeration":
                    {
                        var __oldValue = _fk_Enumeration;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("Enumeration", __oldValue, __newValue);
                        _fk_Enumeration = __newValue;
                        NotifyPropertyChanged("Enumeration", __oldValue, __newValue);
                    }
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }
        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Enumeration":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Enumeration":
                return TriggerFetchEnumerationAsync();
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

            if (_fk_guid_Enumeration.HasValue)
                EnumerationImpl = (Zetbox.App.Base.EnumerationMemoryImpl)Context.FindPersistenceObject<Zetbox.App.Base.Enumeration>(_fk_guid_Enumeration.Value);
            else
            if (_fk_Enumeration.HasValue)
                EnumerationImpl = (Zetbox.App.Base.EnumerationMemoryImpl)Context.Find<Zetbox.App.Base.Enumeration>(_fk_Enumeration.Value);
            else
                EnumerationImpl = null;
            // fix cached lists references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
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
                    new PropertyDescriptorMemoryImpl<EnumParameter, Zetbox.App.Base.Enumeration>(
                        lazyCtx,
                        new Guid("b5212dc9-376e-4414-a400-d994779fda18"),
                        "Enumeration",
                        null,
                        obj => obj.Enumeration,
                        (obj, val) => obj.Enumeration = val,
						obj => OnEnumeration_IsValid), 
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
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_EnumParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EnumParameter != null)
            {
                OnToString_EnumParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<EnumParameter> OnToString_EnumParameter;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_EnumParameter")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_EnumParameter != null)
            {
                OnObjectIsValid_EnumParameter(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<EnumParameter> OnObjectIsValid_EnumParameter;

        [EventBasedMethod("OnNotifyPreSave_EnumParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_EnumParameter != null) OnNotifyPreSave_EnumParameter(this);
        }
        public static event ObjectEventHandler<EnumParameter> OnNotifyPreSave_EnumParameter;

        [EventBasedMethod("OnNotifyPostSave_EnumParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_EnumParameter != null) OnNotifyPostSave_EnumParameter(this);
        }
        public static event ObjectEventHandler<EnumParameter> OnNotifyPostSave_EnumParameter;

        [EventBasedMethod("OnNotifyCreated_EnumParameter")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Enumeration");
            base.NotifyCreated();
            if (OnNotifyCreated_EnumParameter != null) OnNotifyCreated_EnumParameter(this);
        }
        public static event ObjectEventHandler<EnumParameter> OnNotifyCreated_EnumParameter;

        [EventBasedMethod("OnNotifyDeleting_EnumParameter")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_EnumParameter != null) OnNotifyDeleting_EnumParameter(this);
            Enumeration = null;
        }
        public static event ObjectEventHandler<EnumParameter> OnNotifyDeleting_EnumParameter;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(Enumeration != null ? Enumeration.ID : (int?)null);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._fk_Enumeration = binStream.ReadNullableInt32();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(Enumeration != null ? Enumeration.ExportGuid : (Guid?)null, xml, "Enumeration", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|Enumeration":
                this._fk_guid_Enumeration = XmlStreamer.ReadNullableGuid(xml);
                break;
            }
        }

        #endregion

    }
}