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
    /// Metadefinition Object for CLR Object Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CLRObjectParameter")]
    public class CLRObjectParameterMemoryImpl : Zetbox.App.Base.BaseParameterMemoryImpl, CLRObjectParameter
    {
        private static readonly Guid _objectClassID = new Guid("012dfab4-934b-443e-853a-11a5da5b0627");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public CLRObjectParameterMemoryImpl()
            : base(null)
        {
        }

        public CLRObjectParameterMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// The expected argument type.
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string TypeRef
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TypeRef;
                if (OnTypeRef_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnTypeRef_Getter(this, __e);
                    __result = _TypeRef = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TypeRef != value)
                {
                    var __oldValue = _TypeRef;
                    var __newValue = value;
                    if (OnTypeRef_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnTypeRef_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TypeRef", __oldValue, __newValue);
                    _TypeRef = __newValue;
                    NotifyPropertyChanged("TypeRef", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnTypeRef_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnTypeRef_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("TypeRef");
                }
            }
        }
        private string _TypeRef;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.CLRObjectParameter, string> OnTypeRef_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.CLRObjectParameter, string> OnTypeRef_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.CLRObjectParameter, string> OnTypeRef_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.CLRObjectParameter> OnTypeRef_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_CLRObjectParameter")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_CLRObjectParameter != null)
            {
                OnGetLabel_CLRObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<CLRObjectParameter> OnGetLabel_CLRObjectParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CLRObjectParameter> OnGetLabel_CLRObjectParameter_CanExec;

        [EventBasedMethod("OnGetLabel_CLRObjectParameter_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_CLRObjectParameter_CanExec != null)
				{
					OnGetLabel_CLRObjectParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CLRObjectParameter> OnGetLabel_CLRObjectParameter_CanExecReason;

        [EventBasedMethod("OnGetLabel_CLRObjectParameter_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_CLRObjectParameter_CanExecReason != null)
				{
					OnGetLabel_CLRObjectParameter_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetParameterType_CLRObjectParameter")]
        public override System.Type GetParameterType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_CLRObjectParameter != null)
            {
                OnGetParameterType_CLRObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
        public static event GetParameterType_Handler<CLRObjectParameter> OnGetParameterType_CLRObjectParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CLRObjectParameter> OnGetParameterType_CLRObjectParameter_CanExec;

        [EventBasedMethod("OnGetParameterType_CLRObjectParameter_CanExec")]
        public override bool GetParameterTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterType_CLRObjectParameter_CanExec != null)
				{
					OnGetParameterType_CLRObjectParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CLRObjectParameter> OnGetParameterType_CLRObjectParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterType_CLRObjectParameter_CanExecReason")]
        public override string GetParameterTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterType_CLRObjectParameter_CanExecReason != null)
				{
					OnGetParameterType_CLRObjectParameter_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetParameterTypeString_CLRObjectParameter")]
        public override string GetParameterTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_CLRObjectParameter != null)
            {
                OnGetParameterTypeString_CLRObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
        public static event GetParameterTypeString_Handler<CLRObjectParameter> OnGetParameterTypeString_CLRObjectParameter;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<CLRObjectParameter> OnGetParameterTypeString_CLRObjectParameter_CanExec;

        [EventBasedMethod("OnGetParameterTypeString_CLRObjectParameter_CanExec")]
        public override bool GetParameterTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterTypeString_CLRObjectParameter_CanExec != null)
				{
					OnGetParameterTypeString_CLRObjectParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<CLRObjectParameter> OnGetParameterTypeString_CLRObjectParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterTypeString_CLRObjectParameter_CanExecReason")]
        public override string GetParameterTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterTypeString_CLRObjectParameter_CanExecReason != null)
				{
					OnGetParameterTypeString_CLRObjectParameter_CanExecReason(this, e);
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
            return typeof(CLRObjectParameter);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (CLRObjectParameter)obj;
            var otherImpl = (CLRObjectParameterMemoryImpl)obj;
            var me = (CLRObjectParameter)this;

            me.TypeRef = other.TypeRef;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "TypeRef":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
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
                    new PropertyDescriptorMemoryImpl<CLRObjectParameter, string>(
                        lazyCtx,
                        new Guid("9cb49ab3-72bd-46d0-a3b0-f4b20428aee5"),
                        "TypeRef",
                        null,
                        obj => obj.TypeRef,
                        (obj, val) => obj.TypeRef = val,
						obj => OnTypeRef_IsValid), 
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
        [EventBasedMethod("OnToString_CLRObjectParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CLRObjectParameter != null)
            {
                OnToString_CLRObjectParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<CLRObjectParameter> OnToString_CLRObjectParameter;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_CLRObjectParameter")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_CLRObjectParameter != null)
            {
                OnObjectIsValid_CLRObjectParameter(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<CLRObjectParameter> OnObjectIsValid_CLRObjectParameter;

        [EventBasedMethod("OnNotifyPreSave_CLRObjectParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_CLRObjectParameter != null) OnNotifyPreSave_CLRObjectParameter(this);
        }
        public static event ObjectEventHandler<CLRObjectParameter> OnNotifyPreSave_CLRObjectParameter;

        [EventBasedMethod("OnNotifyPostSave_CLRObjectParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_CLRObjectParameter != null) OnNotifyPostSave_CLRObjectParameter(this);
        }
        public static event ObjectEventHandler<CLRObjectParameter> OnNotifyPostSave_CLRObjectParameter;

        [EventBasedMethod("OnNotifyCreated_CLRObjectParameter")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("TypeRef");
            base.NotifyCreated();
            if (OnNotifyCreated_CLRObjectParameter != null) OnNotifyCreated_CLRObjectParameter(this);
        }
        public static event ObjectEventHandler<CLRObjectParameter> OnNotifyCreated_CLRObjectParameter;

        [EventBasedMethod("OnNotifyDeleting_CLRObjectParameter")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_CLRObjectParameter != null) OnNotifyDeleting_CLRObjectParameter(this);
        }
        public static event ObjectEventHandler<CLRObjectParameter> OnNotifyDeleting_CLRObjectParameter;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._TypeRef);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._TypeRef = binStream.ReadString();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._TypeRef, xml, "TypeRef", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|TypeRef":
                this._TypeRef = XmlStreamer.ReadString(xml);
                break;
            }
        }

        #endregion

    }
}