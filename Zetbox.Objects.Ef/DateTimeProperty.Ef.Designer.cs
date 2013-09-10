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
    /// Metadefinition Object for DateTime Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="DateTimePropertyEfImpl")]
    [System.Diagnostics.DebuggerDisplay("DateTimeProperty")]
    public class DateTimePropertyEfImpl : Zetbox.App.Base.ValueTypePropertyEfImpl, DateTimeProperty
    {
        private static readonly Guid _objectClassID = new Guid("1caadf11-7b95-4c68-8b42-87ac51b01ea0");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public DateTimePropertyEfImpl()
            : base(null)
        {
        }

        public DateTimePropertyEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Style of Datetime. Can be Date, Date and Time or Time
        /// </summary>
        // enumeration property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingEnumProperty
        public Zetbox.App.Base.DateTimeStyles? DateTimeStyle
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _DateTimeStyle;
                if (OnDateTimeStyle_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Base.DateTimeStyles?>(__result);
                    OnDateTimeStyle_Getter(this, __e);
                    __result = _DateTimeStyle = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_DateTimeStyle != value)
                {
                    var __oldValue = _DateTimeStyle;
                    var __newValue = value;
                    if (OnDateTimeStyle_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Zetbox.App.Base.DateTimeStyles?>(__oldValue, __newValue);
                        OnDateTimeStyle_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DateTimeStyle", __oldValue, __newValue);
                    _DateTimeStyle = __newValue;
                    NotifyPropertyChanged("DateTimeStyle", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDateTimeStyle_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Zetbox.App.Base.DateTimeStyles?>(__oldValue, __newValue);
                        OnDateTimeStyle_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("DateTimeStyle");
                }
            }
        }
        private Zetbox.App.Base.DateTimeStyles? _DateTimeStyle_store;
        private Zetbox.App.Base.DateTimeStyles? _DateTimeStyle {
            get { return _DateTimeStyle_store; }
            set {
                ReportEfPropertyChanging("DateTimeStyleImpl");
                _DateTimeStyle_store = value;
                ReportEfPropertyChanged("DateTimeStyleImpl");
            }
        }

        /// <summary>EF sees only this property, for DateTimeStyle</summary>
        [XmlIgnore()]
        [EdmScalarProperty()]
        public int? DateTimeStyleImpl
        {
            get
            {
                return (int?)this.DateTimeStyle;
            }
            set
            {
                this.DateTimeStyle = (Zetbox.App.Base.DateTimeStyles?)value;
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingEnumProperty
		public static event PropertyGetterHandler<Zetbox.App.Base.DateTimeProperty, Zetbox.App.Base.DateTimeStyles?> OnDateTimeStyle_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Base.DateTimeProperty, Zetbox.App.Base.DateTimeStyles?> OnDateTimeStyle_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Base.DateTimeProperty, Zetbox.App.Base.DateTimeStyles?> OnDateTimeStyle_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Base.DateTimeProperty> OnDateTimeStyle_IsValid;

        /// <summary>
        /// Returns the translated description
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDescription_DateTimeProperty")]
        public override string GetDescription()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDescription_DateTimeProperty != null)
            {
                OnGetDescription_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetDescription();
            }
            return e.Result;
        }
        public static event GetDescription_Handler<DateTimeProperty> OnGetDescription_DateTimeProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeProperty> OnGetDescription_DateTimeProperty_CanExec;

        [EventBasedMethod("OnGetDescription_DateTimeProperty_CanExec")]
        public override bool GetDescriptionCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetDescription_DateTimeProperty_CanExec != null)
				{
					OnGetDescription_DateTimeProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetDescriptionCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeProperty> OnGetDescription_DateTimeProperty_CanExecReason;

        [EventBasedMethod("OnGetDescription_DateTimeProperty_CanExecReason")]
        public override string GetDescriptionCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetDescription_DateTimeProperty_CanExecReason != null)
				{
					OnGetDescription_DateTimeProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetElementTypeString_DateTimeProperty")]
        public override string GetElementTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetElementTypeString_DateTimeProperty != null)
            {
                OnGetElementTypeString_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetElementTypeString();
            }
            return e.Result;
        }
        public static event GetElementTypeString_Handler<DateTimeProperty> OnGetElementTypeString_DateTimeProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeProperty> OnGetElementTypeString_DateTimeProperty_CanExec;

        [EventBasedMethod("OnGetElementTypeString_DateTimeProperty_CanExec")]
        public override bool GetElementTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetElementTypeString_DateTimeProperty_CanExec != null)
				{
					OnGetElementTypeString_DateTimeProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetElementTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeProperty> OnGetElementTypeString_DateTimeProperty_CanExecReason;

        [EventBasedMethod("OnGetElementTypeString_DateTimeProperty_CanExecReason")]
        public override string GetElementTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetElementTypeString_DateTimeProperty_CanExecReason != null)
				{
					OnGetElementTypeString_DateTimeProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetLabel_DateTimeProperty")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_DateTimeProperty != null)
            {
                OnGetLabel_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<DateTimeProperty> OnGetLabel_DateTimeProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeProperty> OnGetLabel_DateTimeProperty_CanExec;

        [EventBasedMethod("OnGetLabel_DateTimeProperty_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_DateTimeProperty_CanExec != null)
				{
					OnGetLabel_DateTimeProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeProperty> OnGetLabel_DateTimeProperty_CanExecReason;

        [EventBasedMethod("OnGetLabel_DateTimeProperty_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_DateTimeProperty_CanExecReason != null)
				{
					OnGetLabel_DateTimeProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetName_DateTimeProperty")]
        public override string GetName()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetName_DateTimeProperty != null)
            {
                OnGetName_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetName();
            }
            return e.Result;
        }
        public static event GetName_Handler<DateTimeProperty> OnGetName_DateTimeProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeProperty> OnGetName_DateTimeProperty_CanExec;

        [EventBasedMethod("OnGetName_DateTimeProperty_CanExec")]
        public override bool GetNameCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetName_DateTimeProperty_CanExec != null)
				{
					OnGetName_DateTimeProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetNameCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeProperty> OnGetName_DateTimeProperty_CanExecReason;

        [EventBasedMethod("OnGetName_DateTimeProperty_CanExecReason")]
        public override string GetNameCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetName_DateTimeProperty_CanExecReason != null)
				{
					OnGetName_DateTimeProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetPropertyType_DateTimeProperty")]
        public override System.Type GetPropertyType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_DateTimeProperty != null)
            {
                OnGetPropertyType_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
        public static event GetPropertyType_Handler<DateTimeProperty> OnGetPropertyType_DateTimeProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeProperty> OnGetPropertyType_DateTimeProperty_CanExec;

        [EventBasedMethod("OnGetPropertyType_DateTimeProperty_CanExec")]
        public override bool GetPropertyTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyType_DateTimeProperty_CanExec != null)
				{
					OnGetPropertyType_DateTimeProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeProperty> OnGetPropertyType_DateTimeProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyType_DateTimeProperty_CanExecReason")]
        public override string GetPropertyTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyType_DateTimeProperty_CanExecReason != null)
				{
					OnGetPropertyType_DateTimeProperty_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetPropertyTypeString_DateTimeProperty")]
        public override string GetPropertyTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_DateTimeProperty != null)
            {
                OnGetPropertyTypeString_DateTimeProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
        public static event GetPropertyTypeString_Handler<DateTimeProperty> OnGetPropertyTypeString_DateTimeProperty;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeProperty> OnGetPropertyTypeString_DateTimeProperty_CanExec;

        [EventBasedMethod("OnGetPropertyTypeString_DateTimeProperty_CanExec")]
        public override bool GetPropertyTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetPropertyTypeString_DateTimeProperty_CanExec != null)
				{
					OnGetPropertyTypeString_DateTimeProperty_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetPropertyTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeProperty> OnGetPropertyTypeString_DateTimeProperty_CanExecReason;

        [EventBasedMethod("OnGetPropertyTypeString_DateTimeProperty_CanExecReason")]
        public override string GetPropertyTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetPropertyTypeString_DateTimeProperty_CanExecReason != null)
				{
					OnGetPropertyTypeString_DateTimeProperty_CanExecReason(this, e);
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
            return typeof(DateTimeProperty);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (DateTimeProperty)obj;
            var otherImpl = (DateTimePropertyEfImpl)obj;
            var me = (DateTimeProperty)this;

            me.DateTimeStyle = other.DateTimeStyle;
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
                case "DateTimeStyle":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
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
                    new PropertyDescriptorEfImpl<DateTimeProperty, Zetbox.App.Base.DateTimeStyles?>(
                        lazyCtx,
                        new Guid("76b04254-3911-4753-ba11-cb1af074b056"),
                        "DateTimeStyle",
                        null,
                        obj => obj.DateTimeStyle,
                        (obj, val) => obj.DateTimeStyle = val,
						obj => OnDateTimeStyle_IsValid), 
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
        [EventBasedMethod("OnToString_DateTimeProperty")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DateTimeProperty != null)
            {
                OnToString_DateTimeProperty(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DateTimeProperty> OnToString_DateTimeProperty;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_DateTimeProperty")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_DateTimeProperty != null)
            {
                OnObjectIsValid_DateTimeProperty(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<DateTimeProperty> OnObjectIsValid_DateTimeProperty;

        [EventBasedMethod("OnNotifyPreSave_DateTimeProperty")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_DateTimeProperty != null) OnNotifyPreSave_DateTimeProperty(this);
        }
        public static event ObjectEventHandler<DateTimeProperty> OnNotifyPreSave_DateTimeProperty;

        [EventBasedMethod("OnNotifyPostSave_DateTimeProperty")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_DateTimeProperty != null) OnNotifyPostSave_DateTimeProperty(this);
        }
        public static event ObjectEventHandler<DateTimeProperty> OnNotifyPostSave_DateTimeProperty;

        [EventBasedMethod("OnNotifyCreated_DateTimeProperty")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("DateTimeStyle");
            base.NotifyCreated();
            if (OnNotifyCreated_DateTimeProperty != null) OnNotifyCreated_DateTimeProperty(this);
        }
        public static event ObjectEventHandler<DateTimeProperty> OnNotifyCreated_DateTimeProperty;

        [EventBasedMethod("OnNotifyDeleting_DateTimeProperty")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_DateTimeProperty != null) OnNotifyDeleting_DateTimeProperty(this);
        }
        public static event ObjectEventHandler<DateTimeProperty> OnNotifyDeleting_DateTimeProperty;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write((int?)_DateTimeStyle);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            _DateTimeStyle = (Zetbox.App.Base.DateTimeStyles?)binStream.ReadNullableInt32();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Base")) XmlStreamer.ToStream((int?)_DateTimeStyle, xml, "DateTimeStyle", "Zetbox.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Base|DateTimeStyle":
                _DateTimeStyle = (Zetbox.App.Base.DateTimeStyles?)XmlStreamer.ReadNullableInt32(xml);
               break;
            }
        }

        #endregion

    }
}