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

    using Kistl.API.Utils;
    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.NHibernate;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MonthFilterConfiguration")]
    public class MonthFilterConfigurationNHibernateImpl : Kistl.App.GUI.PropertyFilterConfigurationNHibernateImpl, MonthFilterConfiguration
    {
        private static readonly Guid _objectClassID = new Guid("4d690bc6-7e4f-4967-a50a-770dd5c24590");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public MonthFilterConfigurationNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public MonthFilterConfigurationNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new MonthFilterConfigurationProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public MonthFilterConfigurationNHibernateImpl(Func<IFrozenContext> lazyCtx, MonthFilterConfigurationProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly MonthFilterConfigurationProxy Proxy;

        /// <summary>
        /// If true, the current month will be the default value
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public bool? IsCurrentMonthDefault
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(bool?);
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

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.GUI.MonthFilterConfiguration, bool?> OnIsCurrentMonthDefault_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.GUI.MonthFilterConfiguration, bool?> OnIsCurrentMonthDefault_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.GUI.MonthFilterConfiguration, bool?> OnIsCurrentMonthDefault_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.GUI.MonthFilterConfiguration> OnIsCurrentMonthDefault_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnCreateFilterModel_MonthFilterConfiguration")]
        public override Kistl.API.IFilterModel CreateFilterModel()
        {
            var e = new MethodReturnEventArgs<Kistl.API.IFilterModel>();
            if (OnCreateFilterModel_MonthFilterConfiguration != null)
            {
                OnCreateFilterModel_MonthFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.CreateFilterModel();
            }
            return e.Result;
        }
        public static event CreateFilterModel_Handler<MonthFilterConfiguration> OnCreateFilterModel_MonthFilterConfiguration;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MonthFilterConfiguration> OnCreateFilterModel_MonthFilterConfiguration_CanExec;

        [EventBasedMethod("OnCreateFilterModel_MonthFilterConfiguration_CanExec")]
        public override bool CreateFilterModelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnCreateFilterModel_MonthFilterConfiguration_CanExec != null)
				{
					OnCreateFilterModel_MonthFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.CreateFilterModelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MonthFilterConfiguration> OnCreateFilterModel_MonthFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnCreateFilterModel_MonthFilterConfiguration_CanExecReason")]
        public override string CreateFilterModelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnCreateFilterModel_MonthFilterConfiguration_CanExecReason != null)
				{
					OnCreateFilterModel_MonthFilterConfiguration_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetLabel_MonthFilterConfiguration")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_MonthFilterConfiguration != null)
            {
                OnGetLabel_MonthFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<MonthFilterConfiguration> OnGetLabel_MonthFilterConfiguration;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<MonthFilterConfiguration> OnGetLabel_MonthFilterConfiguration_CanExec;

        [EventBasedMethod("OnGetLabel_MonthFilterConfiguration_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_MonthFilterConfiguration_CanExec != null)
				{
					OnGetLabel_MonthFilterConfiguration_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<MonthFilterConfiguration> OnGetLabel_MonthFilterConfiguration_CanExecReason;

        [EventBasedMethod("OnGetLabel_MonthFilterConfiguration_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_MonthFilterConfiguration_CanExecReason != null)
				{
					OnGetLabel_MonthFilterConfiguration_CanExecReason(this, e);
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
            return typeof(MonthFilterConfiguration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (MonthFilterConfiguration)obj;
            var otherImpl = (MonthFilterConfigurationNHibernateImpl)obj;
            var me = (MonthFilterConfiguration)this;

            me.IsCurrentMonthDefault = other.IsCurrentMonthDefault;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            var nhCtx = (NHibernateContext)ctx;
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
                    new PropertyDescriptorNHibernateImpl<MonthFilterConfiguration, bool?>(
                        lazyCtx,
                        new Guid("363661ad-85ce-4bc2-b249-c3cce65a1971"),
                        "IsCurrentMonthDefault",
                        null,
                        obj => obj.IsCurrentMonthDefault,
                        (obj, val) => obj.IsCurrentMonthDefault = val,
						obj => OnIsCurrentMonthDefault_IsValid), 
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
        #region Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_MonthFilterConfiguration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MonthFilterConfiguration != null)
            {
                OnToString_MonthFilterConfiguration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<MonthFilterConfiguration> OnToString_MonthFilterConfiguration;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_MonthFilterConfiguration")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_MonthFilterConfiguration != null)
            {
                OnObjectIsValid_MonthFilterConfiguration(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<MonthFilterConfiguration> OnObjectIsValid_MonthFilterConfiguration;

        [EventBasedMethod("OnNotifyPreSave_MonthFilterConfiguration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_MonthFilterConfiguration != null) OnNotifyPreSave_MonthFilterConfiguration(this);
        }
        public static event ObjectEventHandler<MonthFilterConfiguration> OnNotifyPreSave_MonthFilterConfiguration;

        [EventBasedMethod("OnNotifyPostSave_MonthFilterConfiguration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_MonthFilterConfiguration != null) OnNotifyPostSave_MonthFilterConfiguration(this);
        }
        public static event ObjectEventHandler<MonthFilterConfiguration> OnNotifyPostSave_MonthFilterConfiguration;

        [EventBasedMethod("OnNotifyCreated_MonthFilterConfiguration")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("IsCurrentMonthDefault");
            base.NotifyCreated();
            if (OnNotifyCreated_MonthFilterConfiguration != null) OnNotifyCreated_MonthFilterConfiguration(this);
        }
        public static event ObjectEventHandler<MonthFilterConfiguration> OnNotifyCreated_MonthFilterConfiguration;

        [EventBasedMethod("OnNotifyDeleting_MonthFilterConfiguration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_MonthFilterConfiguration != null) OnNotifyDeleting_MonthFilterConfiguration(this);
        }
        public static event ObjectEventHandler<MonthFilterConfiguration> OnNotifyDeleting_MonthFilterConfiguration;

        #endregion // Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods
        public override List<NHibernatePersistenceObject> GetParentsToDelete()
        {
            var result = base.GetParentsToDelete();

            return result;
        }

        public override List<NHibernatePersistenceObject> GetChildrenToDelete()
        {
            var result = base.GetChildrenToDelete();

            return result;
        }


        public class MonthFilterConfigurationProxy
            : Kistl.App.GUI.PropertyFilterConfigurationNHibernateImpl.PropertyFilterConfigurationProxy
        {
            public MonthFilterConfigurationProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(MonthFilterConfigurationNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(MonthFilterConfigurationProxy); } }

            public virtual bool? IsCurrentMonthDefault { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this.Proxy.IsCurrentMonthDefault, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                bool? tmp;
                BinarySerializer.FromStream(out tmp, binStream);
                this.Proxy.IsCurrentMonthDefault = tmp;
            }
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
            XmlStreamer.ToStream(this.Proxy.IsCurrentMonthDefault, xml, "IsCurrentMonthDefault", "Kistl.App.GUI");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                // yuck
                bool? tmp = this.Proxy.IsCurrentMonthDefault;
                XmlStreamer.FromStream(ref tmp, xml, "IsCurrentMonthDefault", "Kistl.App.GUI");
                this.Proxy.IsCurrentMonthDefault = tmp;
            }
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.GUI")) XmlStreamer.ToStream(this.Proxy.IsCurrentMonthDefault, xml, "IsCurrentMonthDefault", "Kistl.App.GUI");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            {
                // yuck
                bool? tmp = this.Proxy.IsCurrentMonthDefault;
                XmlStreamer.FromStream(ref tmp, xml, "IsCurrentMonthDefault", "Kistl.App.GUI");
                this.Proxy.IsCurrentMonthDefault = tmp;
            }
        }

        #endregion

    }
}