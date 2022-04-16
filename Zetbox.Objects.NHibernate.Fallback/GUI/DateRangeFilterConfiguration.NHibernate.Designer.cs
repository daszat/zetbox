// <autogenerated/>

namespace Zetbox.App.GUI
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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DateRangeFilterConfiguration")]
    public class DateRangeFilterConfigurationNHibernateImpl : Zetbox.App.GUI.PropertyFilterConfigurationNHibernateImpl, DateRangeFilterConfiguration
    {
        private static readonly Guid _objectClassID = new Guid("eb9ea5f9-d4b7-4e77-92c3-3eb60853e881");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public DateRangeFilterConfigurationNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public DateRangeFilterConfigurationNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new DateRangeFilterConfigurationProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public DateRangeFilterConfigurationNHibernateImpl(Func<IFrozenContext> lazyCtx, DateRangeFilterConfigurationProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly DateRangeFilterConfigurationProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool? IsCurrentMonthDefault
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.IsCurrentMonthDefault;
                if (OnIsCurrentMonthDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool?>(__result);
                    OnIsCurrentMonthDefault_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.IsCurrentMonthDefault != value)
                {
                    var __oldValue = Proxy.IsCurrentMonthDefault;
                    var __newValue = value;
                    if (OnIsCurrentMonthDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentMonthDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsCurrentMonthDefault", __oldValue, __newValue);
                    Proxy.IsCurrentMonthDefault = __newValue;
                    NotifyPropertyChanged("IsCurrentMonthDefault", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnIsCurrentMonthDefault_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentMonthDefault_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("IsCurrentMonthDefault");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentMonthDefault_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentMonthDefault_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentMonthDefault_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.DateRangeFilterConfiguration> OnIsCurrentMonthDefault_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool? IsCurrentQuaterDefault
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.IsCurrentQuaterDefault;
                if (OnIsCurrentQuaterDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool?>(__result);
                    OnIsCurrentQuaterDefault_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.IsCurrentQuaterDefault != value)
                {
                    var __oldValue = Proxy.IsCurrentQuaterDefault;
                    var __newValue = value;
                    if (OnIsCurrentQuaterDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentQuaterDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsCurrentQuaterDefault", __oldValue, __newValue);
                    Proxy.IsCurrentQuaterDefault = __newValue;
                    NotifyPropertyChanged("IsCurrentQuaterDefault", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnIsCurrentQuaterDefault_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentQuaterDefault_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("IsCurrentQuaterDefault");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentQuaterDefault_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentQuaterDefault_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentQuaterDefault_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.DateRangeFilterConfiguration> OnIsCurrentQuaterDefault_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool? IsCurrentYearDefault
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.IsCurrentYearDefault;
                if (OnIsCurrentYearDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<bool?>(__result);
                    OnIsCurrentYearDefault_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.IsCurrentYearDefault != value)
                {
                    var __oldValue = Proxy.IsCurrentYearDefault;
                    var __newValue = value;
                    if (OnIsCurrentYearDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentYearDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("IsCurrentYearDefault", __oldValue, __newValue);
                    Proxy.IsCurrentYearDefault = __newValue;
                    NotifyPropertyChanged("IsCurrentYearDefault", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnIsCurrentYearDefault_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<bool?>(__oldValue, __newValue);
                        OnIsCurrentYearDefault_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("IsCurrentYearDefault");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentYearDefault_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentYearDefault_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.GUI.DateRangeFilterConfiguration, bool?> OnIsCurrentYearDefault_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.GUI.DateRangeFilterConfiguration> OnIsCurrentYearDefault_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnCreateFilterModel_DateRangeFilterConfiguration")]
        public override Zetbox.API.IFilterModel CreateFilterModel(Zetbox.API.IZetboxContext ctx)
        {
            var e = new MethodReturnEventArgs<Zetbox.API.IFilterModel>();
            if (OnCreateFilterModel_DateRangeFilterConfiguration != null)
            {
                OnCreateFilterModel_DateRangeFilterConfiguration(this, e, ctx);
            }
            else
            {
                e.Result = base.CreateFilterModel(ctx);
            }
            return e.Result;
        }
        public static event CreateFilterModel_Handler<DateRangeFilterConfiguration> OnCreateFilterModel_DateRangeFilterConfiguration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateRangeFilterConfiguration> OnCreateFilterModel_DateRangeFilterConfiguration_CanExec;

        [EventBasedMethod("OnCreateFilterModel_DateRangeFilterConfiguration_CanExec")]
        public override bool CreateFilterModelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnCreateFilterModel_DateRangeFilterConfiguration_CanExec != null)
				{
					OnCreateFilterModel_DateRangeFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.CreateFilterModelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateRangeFilterConfiguration> OnCreateFilterModel_DateRangeFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnCreateFilterModel_DateRangeFilterConfiguration_CanExecReason")]
        public override string CreateFilterModelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnCreateFilterModel_DateRangeFilterConfiguration_CanExecReason != null)
				{
					OnCreateFilterModel_DateRangeFilterConfiguration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.CreateFilterModelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_DateRangeFilterConfiguration")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_DateRangeFilterConfiguration != null)
            {
                OnGetLabel_DateRangeFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<DateRangeFilterConfiguration> OnGetLabel_DateRangeFilterConfiguration;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateRangeFilterConfiguration> OnGetLabel_DateRangeFilterConfiguration_CanExec;

        [EventBasedMethod("OnGetLabel_DateRangeFilterConfiguration_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_DateRangeFilterConfiguration_CanExec != null)
				{
					OnGetLabel_DateRangeFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateRangeFilterConfiguration> OnGetLabel_DateRangeFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnGetLabel_DateRangeFilterConfiguration_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_DateRangeFilterConfiguration_CanExecReason != null)
				{
					OnGetLabel_DateRangeFilterConfiguration_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(DateRangeFilterConfiguration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (DateRangeFilterConfiguration)obj;
            var otherImpl = (DateRangeFilterConfigurationNHibernateImpl)obj;
            var me = (DateRangeFilterConfiguration)this;

            me.IsCurrentMonthDefault = other.IsCurrentMonthDefault;
            me.IsCurrentQuaterDefault = other.IsCurrentQuaterDefault;
            me.IsCurrentYearDefault = other.IsCurrentYearDefault;
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
                case "IsCurrentMonthDefault":
                case "IsCurrentQuaterDefault":
                case "IsCurrentYearDefault":
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
                    new PropertyDescriptorNHibernateImpl<DateRangeFilterConfiguration, bool?>(
                        lazyCtx,
                        new Guid("c38eda0e-59b7-4e37-9d16-5f4fb7ae0b2d"),
                        "IsCurrentMonthDefault",
                        null,
                        obj => obj.IsCurrentMonthDefault,
                        (obj, val) => obj.IsCurrentMonthDefault = val,
						obj => OnIsCurrentMonthDefault_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<DateRangeFilterConfiguration, bool?>(
                        lazyCtx,
                        new Guid("edb97f39-355d-4a87-9aa1-ad9eccafb369"),
                        "IsCurrentQuaterDefault",
                        null,
                        obj => obj.IsCurrentQuaterDefault,
                        (obj, val) => obj.IsCurrentQuaterDefault = val,
						obj => OnIsCurrentQuaterDefault_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<DateRangeFilterConfiguration, bool?>(
                        lazyCtx,
                        new Guid("8f9bbaf8-4624-49d9-9b49-878142189cf7"),
                        "IsCurrentYearDefault",
                        null,
                        obj => obj.IsCurrentYearDefault,
                        (obj, val) => obj.IsCurrentYearDefault = val,
						obj => OnIsCurrentYearDefault_IsValid), 
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
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_DateRangeFilterConfiguration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DateRangeFilterConfiguration != null)
            {
                OnToString_DateRangeFilterConfiguration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DateRangeFilterConfiguration> OnToString_DateRangeFilterConfiguration;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_DateRangeFilterConfiguration")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_DateRangeFilterConfiguration != null)
            {
                OnObjectIsValid_DateRangeFilterConfiguration(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<DateRangeFilterConfiguration> OnObjectIsValid_DateRangeFilterConfiguration;

        [EventBasedMethod("OnNotifyPreSave_DateRangeFilterConfiguration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_DateRangeFilterConfiguration != null) OnNotifyPreSave_DateRangeFilterConfiguration(this);
        }
        public static event ObjectEventHandler<DateRangeFilterConfiguration> OnNotifyPreSave_DateRangeFilterConfiguration;

        [EventBasedMethod("OnNotifyPostSave_DateRangeFilterConfiguration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_DateRangeFilterConfiguration != null) OnNotifyPostSave_DateRangeFilterConfiguration(this);
        }
        public static event ObjectEventHandler<DateRangeFilterConfiguration> OnNotifyPostSave_DateRangeFilterConfiguration;

        [EventBasedMethod("OnNotifyCreated_DateRangeFilterConfiguration")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("IsCurrentMonthDefault");
            SetNotInitializedProperty("IsCurrentQuaterDefault");
            SetNotInitializedProperty("IsCurrentYearDefault");
            base.NotifyCreated();
            if (OnNotifyCreated_DateRangeFilterConfiguration != null) OnNotifyCreated_DateRangeFilterConfiguration(this);
        }
        public static event ObjectEventHandler<DateRangeFilterConfiguration> OnNotifyCreated_DateRangeFilterConfiguration;

        [EventBasedMethod("OnNotifyDeleting_DateRangeFilterConfiguration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_DateRangeFilterConfiguration != null) OnNotifyDeleting_DateRangeFilterConfiguration(this);


        }
        public static event ObjectEventHandler<DateRangeFilterConfiguration> OnNotifyDeleting_DateRangeFilterConfiguration;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class DateRangeFilterConfigurationProxy
            : Zetbox.App.GUI.PropertyFilterConfigurationNHibernateImpl.PropertyFilterConfigurationProxy
        {
            public DateRangeFilterConfigurationProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(DateRangeFilterConfigurationNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(DateRangeFilterConfigurationProxy); } }

            public virtual bool? IsCurrentMonthDefault { get; set; }

            public virtual bool? IsCurrentQuaterDefault { get; set; }

            public virtual bool? IsCurrentYearDefault { get; set; }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.IsCurrentMonthDefault);
            binStream.Write(this.Proxy.IsCurrentQuaterDefault);
            binStream.Write(this.Proxy.IsCurrentYearDefault);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this.Proxy.IsCurrentMonthDefault = binStream.ReadNullableBoolean();
            this.Proxy.IsCurrentQuaterDefault = binStream.ReadNullableBoolean();
            this.Proxy.IsCurrentYearDefault = binStream.ReadNullableBoolean();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.GUI")) XmlStreamer.ToStream(this.Proxy.IsCurrentMonthDefault, xml, "IsCurrentMonthDefault", "Zetbox.App.GUI");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.GUI")) XmlStreamer.ToStream(this.Proxy.IsCurrentQuaterDefault, xml, "IsCurrentQuaterDefault", "Zetbox.App.GUI");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.GUI")) XmlStreamer.ToStream(this.Proxy.IsCurrentYearDefault, xml, "IsCurrentYearDefault", "Zetbox.App.GUI");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.GUI|IsCurrentMonthDefault":
                this.Proxy.IsCurrentMonthDefault = XmlStreamer.ReadNullableBoolean(xml);
                break;
            case "Zetbox.App.GUI|IsCurrentQuaterDefault":
                this.Proxy.IsCurrentQuaterDefault = XmlStreamer.ReadNullableBoolean(xml);
                break;
            case "Zetbox.App.GUI|IsCurrentYearDefault":
                this.Proxy.IsCurrentYearDefault = XmlStreamer.ReadNullableBoolean(xml);
                break;
            }
        }

        #endregion

    }
}