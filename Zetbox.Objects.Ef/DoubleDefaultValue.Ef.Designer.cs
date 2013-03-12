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
    /// Sets a double property with the configured default value
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="DoubleDefaultValueEfImpl")]
    [System.Diagnostics.DebuggerDisplay("DoubleDefaultValue")]
    public class DoubleDefaultValueEfImpl : Zetbox.App.Base.DefaultPropertyValueEfImpl, DoubleDefaultValue
    {
        private static readonly Guid _objectClassID = new Guid("f55c5d76-23cc-4f33-b689-b4d8332d53b5");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public DoubleDefaultValueEfImpl()
            : base(null)
        {
        }

        public DoubleDefaultValueEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Specify the default value here.
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public double DoubleValue
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DoubleValue;
                if (OnDoubleValue_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<double>(__result);
                    OnDoubleValue_Getter(this, __e);
                    __result = _DoubleValue = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DoubleValue != value)
                {
                    var __oldValue = _DoubleValue;
                    var __newValue = value;
                    if (OnDoubleValue_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<double>(__oldValue, __newValue);
                        OnDoubleValue_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DoubleValue", __oldValue, __newValue);
                    _DoubleValue = __newValue;
                    NotifyPropertyChanged("DoubleValue", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDoubleValue_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<double>(__oldValue, __newValue);
                        OnDoubleValue_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("DoubleValue");
                }
            }
        }
        private double _DoubleValue_store;
        private double _DoubleValue {
            get { return _DoubleValue_store; }
            set {
                ReportEfPropertyChanging("DoubleValue");
                _DoubleValue_store = value;
                ReportEfPropertyChanged("DoubleValue");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.DoubleDefaultValue, double> OnDoubleValue_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.DoubleDefaultValue, double> OnDoubleValue_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.DoubleDefaultValue, double> OnDoubleValue_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.DoubleDefaultValue> OnDoubleValue_IsValid;

        /// <summary>
        /// GetDefaultValue
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDefaultValue_DoubleDefaultValue")]
        public override System.Object GetDefaultValue()
        {
            var e = new MethodReturnEventArgs<System.Object>();
            if (OnGetDefaultValue_DoubleDefaultValue != null)
            {
                OnGetDefaultValue_DoubleDefaultValue(this, e);
            }
            else
            {
                e.Result = base.GetDefaultValue();
            }
            return e.Result;
        }
        public static event GetDefaultValue_Handler<DoubleDefaultValue> OnGetDefaultValue_DoubleDefaultValue;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DoubleDefaultValue> OnGetDefaultValue_DoubleDefaultValue_CanExec;

        [EventBasedMethod("OnGetDefaultValue_DoubleDefaultValue_CanExec")]
        public override bool GetDefaultValueCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDefaultValue_DoubleDefaultValue_CanExec != null)
				{
					OnGetDefaultValue_DoubleDefaultValue_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDefaultValueCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DoubleDefaultValue> OnGetDefaultValue_DoubleDefaultValue_CanExecReason;

        [EventBasedMethod("OnGetDefaultValue_DoubleDefaultValue_CanExecReason")]
        public override string GetDefaultValueCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDefaultValue_DoubleDefaultValue_CanExecReason != null)
				{
					OnGetDefaultValue_DoubleDefaultValue_CanExecReason(this, e);
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
            return typeof(DoubleDefaultValue);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (DoubleDefaultValue)obj;
            var otherImpl = (DoubleDefaultValueEfImpl)obj;
            var me = (DoubleDefaultValue)this;

            me.DoubleValue = other.DoubleValue;
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "DoubleValue":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
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
                    new PropertyDescriptorEfImpl<DoubleDefaultValue, double>(
                        lazyCtx,
                        new Guid("aa155407-9526-443c-b8f2-727813ece828"),
                        "DoubleValue",
                        null,
                        obj => obj.DoubleValue,
                        (obj, val) => obj.DoubleValue = val,
						obj => OnDoubleValue_IsValid), 
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
        [EventBasedMethod("OnToString_DoubleDefaultValue")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DoubleDefaultValue != null)
            {
                OnToString_DoubleDefaultValue(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DoubleDefaultValue> OnToString_DoubleDefaultValue;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_DoubleDefaultValue")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_DoubleDefaultValue != null)
            {
                OnObjectIsValid_DoubleDefaultValue(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<DoubleDefaultValue> OnObjectIsValid_DoubleDefaultValue;

        [EventBasedMethod("OnNotifyPreSave_DoubleDefaultValue")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_DoubleDefaultValue != null) OnNotifyPreSave_DoubleDefaultValue(this);
        }
        public static event ObjectEventHandler<DoubleDefaultValue> OnNotifyPreSave_DoubleDefaultValue;

        [EventBasedMethod("OnNotifyPostSave_DoubleDefaultValue")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_DoubleDefaultValue != null) OnNotifyPostSave_DoubleDefaultValue(this);
        }
        public static event ObjectEventHandler<DoubleDefaultValue> OnNotifyPostSave_DoubleDefaultValue;

        [EventBasedMethod("OnNotifyCreated_DoubleDefaultValue")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("DoubleValue");
            base.NotifyCreated();
            if (OnNotifyCreated_DoubleDefaultValue != null) OnNotifyCreated_DoubleDefaultValue(this);
        }
        public static event ObjectEventHandler<DoubleDefaultValue> OnNotifyCreated_DoubleDefaultValue;

        [EventBasedMethod("OnNotifyDeleting_DoubleDefaultValue")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_DoubleDefaultValue != null) OnNotifyDeleting_DoubleDefaultValue(this);
        }
        public static event ObjectEventHandler<DoubleDefaultValue> OnNotifyDeleting_DoubleDefaultValue;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._DoubleValue);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._DoubleValue = binStream.ReadDouble();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream(this._DoubleValue, xml, "DoubleValue", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|DoubleValue":
                this._DoubleValue = XmlStreamer.ReadDouble(xml);
                break;
            }
        }

        #endregion

    }
}