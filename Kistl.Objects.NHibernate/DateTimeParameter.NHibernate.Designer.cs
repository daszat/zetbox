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

    using Kistl.API.Utils;
    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.NHibernate;

    /// <summary>
    /// Metadefinition Object for DateTime Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DateTimeParameter")]
    public class DateTimeParameterNHibernateImpl : Kistl.App.Base.BaseParameterNHibernateImpl, DateTimeParameter
    {
        private static readonly Guid _objectClassID = new Guid("cd6e3f93-5a1d-4c56-bec7-59a951d9fed6");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public DateTimeParameterNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public DateTimeParameterNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new DateTimeParameterProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public DateTimeParameterNHibernateImpl(Func<IFrozenContext> lazyCtx, DateTimeParameterProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly DateTimeParameterProxy Proxy;

        /// <summary>
        /// Style of Datetime. Can be Date, Time, or Date and Time.
        /// </summary>

        // BEGIN Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Kistl.App.Base.DateTimeStyles? DateTimeStyle
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(Kistl.App.Base.DateTimeStyles?);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.DateTimeStyle;
                if (OnDateTimeStyle_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Kistl.App.Base.DateTimeStyles?>(__result);
                    OnDateTimeStyle_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.DateTimeStyle != value)
                {
                    var __oldValue = Proxy.DateTimeStyle;
                    var __newValue = value;
                    if (OnDateTimeStyle_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Kistl.App.Base.DateTimeStyles?>(__oldValue, __newValue);
                        OnDateTimeStyle_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DateTimeStyle", __oldValue, __newValue);
                    Proxy.DateTimeStyle = __newValue;
                    NotifyPropertyChanged("DateTimeStyle", __oldValue, __newValue);

                    if (OnDateTimeStyle_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Kistl.App.Base.DateTimeStyles?>(__oldValue, __newValue);
                        OnDateTimeStyle_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("DateTimeStyle");
				}
            }
        }

        // END Kistl.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Kistl.App.Base.DateTimeParameter, Kistl.App.Base.DateTimeStyles?> OnDateTimeStyle_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Base.DateTimeParameter, Kistl.App.Base.DateTimeStyles?> OnDateTimeStyle_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Base.DateTimeParameter, Kistl.App.Base.DateTimeStyles?> OnDateTimeStyle_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Base.DateTimeParameter> OnDateTimeStyle_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_DateTimeParameter")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_DateTimeParameter != null)
            {
                OnGetLabel_DateTimeParameter(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<DateTimeParameter> OnGetLabel_DateTimeParameter;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeParameter> OnGetLabel_DateTimeParameter_CanExec;

        [EventBasedMethod("OnGetLabel_DateTimeParameter_CanExec")]
        public override bool GetLabelCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetLabel_DateTimeParameter_CanExec != null)
				{
					OnGetLabel_DateTimeParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetLabelCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeParameter> OnGetLabel_DateTimeParameter_CanExecReason;

        [EventBasedMethod("OnGetLabel_DateTimeParameter_CanExecReason")]
        public override string GetLabelCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetLabel_DateTimeParameter_CanExecReason != null)
				{
					OnGetLabel_DateTimeParameter_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetParameterType_DateTimeParameter")]
        public override System.Type GetParameterType()
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_DateTimeParameter != null)
            {
                OnGetParameterType_DateTimeParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
        public static event GetParameterType_Handler<DateTimeParameter> OnGetParameterType_DateTimeParameter;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeParameter> OnGetParameterType_DateTimeParameter_CanExec;

        [EventBasedMethod("OnGetParameterType_DateTimeParameter_CanExec")]
        public override bool GetParameterTypeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterType_DateTimeParameter_CanExec != null)
				{
					OnGetParameterType_DateTimeParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeParameter> OnGetParameterType_DateTimeParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterType_DateTimeParameter_CanExecReason")]
        public override string GetParameterTypeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterType_DateTimeParameter_CanExecReason != null)
				{
					OnGetParameterType_DateTimeParameter_CanExecReason(this, e);
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
        [EventBasedMethod("OnGetParameterTypeString_DateTimeParameter")]
        public override string GetParameterTypeString()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_DateTimeParameter != null)
            {
                OnGetParameterTypeString_DateTimeParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
        public static event GetParameterTypeString_Handler<DateTimeParameter> OnGetParameterTypeString_DateTimeParameter;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DateTimeParameter> OnGetParameterTypeString_DateTimeParameter_CanExec;

        [EventBasedMethod("OnGetParameterTypeString_DateTimeParameter_CanExec")]
        public override bool GetParameterTypeStringCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnGetParameterTypeString_DateTimeParameter_CanExec != null)
				{
					OnGetParameterTypeString_DateTimeParameter_CanExec(this, e);
				}
				else
				{
					e.Result = base.GetParameterTypeStringCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DateTimeParameter> OnGetParameterTypeString_DateTimeParameter_CanExecReason;

        [EventBasedMethod("OnGetParameterTypeString_DateTimeParameter_CanExecReason")]
        public override string GetParameterTypeStringCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnGetParameterTypeString_DateTimeParameter_CanExecReason != null)
				{
					OnGetParameterTypeString_DateTimeParameter_CanExecReason(this, e);
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
            return typeof(DateTimeParameter);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (DateTimeParameter)obj;
            var otherImpl = (DateTimeParameterNHibernateImpl)obj;
            var me = (DateTimeParameter)this;

            me.DateTimeStyle = other.DateTimeStyle;
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
                    new PropertyDescriptorNHibernateImpl<DateTimeParameter, Kistl.App.Base.DateTimeStyles?>(
                        lazyCtx,
                        new Guid("0d0f2e67-cfa2-4463-aaf2-2ce0e6d11fa0"),
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_DateTimeParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DateTimeParameter != null)
            {
                OnToString_DateTimeParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DateTimeParameter> OnToString_DateTimeParameter;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_DateTimeParameter")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_DateTimeParameter != null)
            {
                OnObjectIsValid_DateTimeParameter(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<DateTimeParameter> OnObjectIsValid_DateTimeParameter;

        [EventBasedMethod("OnNotifyPreSave_DateTimeParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_DateTimeParameter != null) OnNotifyPreSave_DateTimeParameter(this);
        }
        public static event ObjectEventHandler<DateTimeParameter> OnNotifyPreSave_DateTimeParameter;

        [EventBasedMethod("OnNotifyPostSave_DateTimeParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_DateTimeParameter != null) OnNotifyPostSave_DateTimeParameter(this);
        }
        public static event ObjectEventHandler<DateTimeParameter> OnNotifyPostSave_DateTimeParameter;

        [EventBasedMethod("OnNotifyCreated_DateTimeParameter")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("DateTimeStyle");
            base.NotifyCreated();
            if (OnNotifyCreated_DateTimeParameter != null) OnNotifyCreated_DateTimeParameter(this);
        }
        public static event ObjectEventHandler<DateTimeParameter> OnNotifyCreated_DateTimeParameter;

        [EventBasedMethod("OnNotifyDeleting_DateTimeParameter")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_DateTimeParameter != null) OnNotifyDeleting_DateTimeParameter(this);
        }
        public static event ObjectEventHandler<DateTimeParameter> OnNotifyDeleting_DateTimeParameter;

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


        public class DateTimeParameterProxy
            : Kistl.App.Base.BaseParameterNHibernateImpl.BaseParameterProxy
        {
            public DateTimeParameterProxy()
            {
            }

            public override Type ZBoxWrapper { get { return typeof(DateTimeParameterNHibernateImpl); } }

            public override Type ZBoxProxy { get { return typeof(DateTimeParameterProxy); } }

            public virtual Kistl.App.Base.DateTimeStyles? DateTimeStyle { get; set; }

        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream((int?)Proxy.DateTimeStyle, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            {
                int? baseValue;
                BinarySerializer.FromStream(out baseValue, binStream);
                Proxy.DateTimeStyle = (Kistl.App.Base.DateTimeStyles?)baseValue;
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
            XmlStreamer.ToStream((int?)Proxy.DateTimeStyle, xml, "DateTimeStyle", "Kistl.App.Base");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStreamConverter(v => Proxy.DateTimeStyle = (Kistl.App.Base.DateTimeStyles?)v, xml, "DateTimeStyle", "Kistl.App.Base");
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
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream((int?)Proxy.DateTimeStyle, xml, "DateTimeStyle", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.FromStreamConverter(v => Proxy.DateTimeStyle = (Kistl.App.Base.DateTimeStyles?)v, xml, "DateTimeStyle", "Kistl.App.Base");
        }

        #endregion

    }
}