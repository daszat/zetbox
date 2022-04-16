// <autogenerated/>

namespace Zetbox.App.Test
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
    [System.Diagnostics.DebuggerDisplay("AnyReferenceTestObject")]
    public class AnyReferenceTestObjectNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, AnyReferenceTestObject, Zetbox.API.IExportableInternal
    {
        private static readonly Guid _objectClassID = new Guid("3a8d152e-b8d1-4439-bfe4-367731218ce9");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public AnyReferenceTestObjectNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public AnyReferenceTestObjectNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new AnyReferenceTestObjectProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public AnyReferenceTestObjectNHibernateImpl(Func<IFrozenContext> lazyCtx, AnyReferenceTestObjectProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
            if (this.Proxy.Any == null)
            {
                this.Proxy.Any = new Zetbox.App.Base.AnyReferenceNHibernateImpl(this, "Any", lazyCtx, null);
            }
            else
            {
                this.Proxy.Any.AttachToObject(this, "Any");
            }

            _isExportGuidSet = Proxy.ID > 0;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly AnyReferenceTestObjectProxy Proxy;

        /// <summary>
        /// 
        /// </summary>
        // CompoundObject property
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.CompoundObjectPropertyTemplate
        // implement the user-visible interface
        public Zetbox.App.Base.AnyReference Any
        {
            get { return AnyImpl; }
            set { AnyImpl = (Zetbox.App.Base.AnyReferenceNHibernateImpl)value; }
        }

        /// <summary>backing property for Any, takes care of attaching/detaching the values</summary>
		[System.Runtime.Serialization.IgnoreDataMember]
        public Zetbox.App.Base.AnyReferenceNHibernateImpl AnyImpl
        {
            get
            {
                return this.Proxy.Any;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value == null)
                    throw new ArgumentNullException("value");
                if (!object.Equals(this.Proxy.Any, value))
                {
                    var __oldValue = this.Proxy.Any;
                    var __newValue = value;

                    NotifyPropertyChanging("Any", __oldValue, __newValue);

                    if (this.Proxy.Any != null)
                    {
                        this.Proxy.Any.DetachFromObject(this, "Any");
                    }
                    __newValue = (Zetbox.App.Base.AnyReferenceNHibernateImpl)__newValue.Clone();
                    this.Proxy.Any = __newValue;
                    this.Proxy.Any.AttachToObject(this, "Any");

                    NotifyPropertyChanged("Any", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;
                }
            }
        }
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.CompoundObjectPropertyTemplate
        public static event PropertyIsValidHandler<Zetbox.App.Test.AnyReferenceTestObject> OnAny_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string DisplayName
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.DisplayName;
                if (OnDisplayName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnDisplayName_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.DisplayName != value)
                {
                    var __oldValue = Proxy.DisplayName;
                    var __newValue = value;
                    if (OnDisplayName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("DisplayName", __oldValue, __newValue);
                    Proxy.DisplayName = __newValue;
                    NotifyPropertyChanged("DisplayName", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnDisplayName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnDisplayName_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("DisplayName");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.AnyReferenceTestObject, string> OnDisplayName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.AnyReferenceTestObject, string> OnDisplayName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.AnyReferenceTestObject, string> OnDisplayName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.AnyReferenceTestObject> OnDisplayName_IsValid;

        /// <summary>
        /// Export Guid
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Guid ExportGuid
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchExportGuidOrDefault();
                if (OnExportGuid_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnExportGuid_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isExportGuidSet = true;
                if (Proxy.ExportGuid != value)
                {
                    var __oldValue = Proxy.ExportGuid;
                    var __newValue = value;
                    if (OnExportGuid_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("ExportGuid", __oldValue, __newValue);
                    Proxy.ExportGuid = __newValue;
                    NotifyPropertyChanged("ExportGuid", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnExportGuid_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnExportGuid_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("ExportGuid");
                }
            }
        }


        private Guid FetchExportGuidOrDefault()
        {
            var __result = Proxy.ExportGuid;
                if (!_isExportGuidSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("65839be9-d3b7-4910-9812-c2d44d008c41"));
                    if (__p != null) {
                        _isExportGuidSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this.Proxy.ExportGuid = (Guid)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Zetbox.App.Test.AnyReferenceTestObject.ExportGuid'");
                    }
                }
            return __result;
        }

        private bool _isExportGuidSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.AnyReferenceTestObject, Guid> OnExportGuid_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.AnyReferenceTestObject, Guid> OnExportGuid_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.AnyReferenceTestObject, Guid> OnExportGuid_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.AnyReferenceTestObject> OnExportGuid_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(AnyReferenceTestObject);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (AnyReferenceTestObject)obj;
            var otherImpl = (AnyReferenceTestObjectNHibernateImpl)obj;
            var me = (AnyReferenceTestObject)this;

            me.DisplayName = other.DisplayName;
            me.ExportGuid = other.ExportGuid;
            if (me.Any == null && other.Any != null) {
                me.Any = (Zetbox.App.Base.AnyReference)other.Any.Clone();
            } else if (me.Any != null && other.Any == null) {
                me.Any = null;
            } else if (me.Any != null && other.Any != null) {
                me.Any.ApplyChangesFrom(other.Any);
            }
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
                case "Any":
                case "DisplayName":
                case "ExportGuid":
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
                    new PropertyDescriptorNHibernateImpl<AnyReferenceTestObject, Zetbox.App.Base.AnyReference>(
                        lazyCtx,
                        new Guid("de93b394-d1e8-4c17-a1df-30aaff7f27f5"),
                        "Any",
                        null,
                        obj => obj.Any,
                        (obj, val) => obj.Any = val,
						obj => OnAny_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<AnyReferenceTestObject, string>(
                        lazyCtx,
                        new Guid("3e806c0f-5ef0-47d1-b504-47b5d2dd59fb"),
                        "DisplayName",
                        null,
                        obj => obj.DisplayName,
                        (obj, val) => obj.DisplayName = val,
						obj => OnDisplayName_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<AnyReferenceTestObject, Guid>(
                        lazyCtx,
                        new Guid("65839be9-d3b7-4910-9812-c2d44d008c41"),
                        "ExportGuid",
                        null,
                        obj => obj.ExportGuid,
                        (obj, val) => obj.ExportGuid = val,
						obj => OnExportGuid_IsValid), 
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
        [EventBasedMethod("OnToString_AnyReferenceTestObject")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_AnyReferenceTestObject != null)
            {
                OnToString_AnyReferenceTestObject(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<AnyReferenceTestObject> OnToString_AnyReferenceTestObject;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_AnyReferenceTestObject")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_AnyReferenceTestObject != null)
            {
                OnObjectIsValid_AnyReferenceTestObject(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<AnyReferenceTestObject> OnObjectIsValid_AnyReferenceTestObject;

        [EventBasedMethod("OnNotifyPreSave_AnyReferenceTestObject")]
        public override void NotifyPreSave()
        {
            FetchExportGuidOrDefault();
            base.NotifyPreSave();
            if (OnNotifyPreSave_AnyReferenceTestObject != null) OnNotifyPreSave_AnyReferenceTestObject(this);
        }
        public static event ObjectEventHandler<AnyReferenceTestObject> OnNotifyPreSave_AnyReferenceTestObject;

        [EventBasedMethod("OnNotifyPostSave_AnyReferenceTestObject")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_AnyReferenceTestObject != null) OnNotifyPostSave_AnyReferenceTestObject(this);
        }
        public static event ObjectEventHandler<AnyReferenceTestObject> OnNotifyPostSave_AnyReferenceTestObject;

        [EventBasedMethod("OnNotifyCreated_AnyReferenceTestObject")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("DisplayName");
            base.NotifyCreated();
            if (OnNotifyCreated_AnyReferenceTestObject != null) OnNotifyCreated_AnyReferenceTestObject(this);
        }
        public static event ObjectEventHandler<AnyReferenceTestObject> OnNotifyCreated_AnyReferenceTestObject;

        [EventBasedMethod("OnNotifyDeleting_AnyReferenceTestObject")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_AnyReferenceTestObject != null) OnNotifyDeleting_AnyReferenceTestObject(this);


        }
        public static event ObjectEventHandler<AnyReferenceTestObject> OnNotifyDeleting_AnyReferenceTestObject;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class AnyReferenceTestObjectProxy
            : IProxyObject, ISortKey<int>
        {
            public AnyReferenceTestObjectProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(AnyReferenceTestObjectNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(AnyReferenceTestObjectProxy); } }

            public virtual Zetbox.App.Base.AnyReferenceNHibernateImpl Any { get; set; }

            public virtual string DisplayName { get; set; }

            public virtual Guid ExportGuid { get; set; }


			[System.Runtime.Serialization.IgnoreDataMember]
			int ISortKey<int>.InternalSortKey { get { return ID; } }
        }

        // make proxy available for the provider
        [System.Runtime.Serialization.IgnoreDataMember]
        public override IProxyObject NHibernateProxy { get { return Proxy; } }
        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Any);
            binStream.Write(this.Proxy.DisplayName);
            binStream.Write(this._isExportGuidSet);
            if (this._isExportGuidSet) {
                binStream.Write(this.Proxy.ExportGuid);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            {
                // use backing store to avoid notifications
                this.AnyImpl = binStream.ReadCompoundObject<Zetbox.App.Base.AnyReferenceNHibernateImpl>();
                this.AnyImpl.AttachToObject(this, "Any");
            }
            this.Proxy.DisplayName = binStream.ReadString();
            this._isExportGuidSet = binStream.ReadBoolean();
            if (this._isExportGuidSet) {
                this.Proxy.ExportGuid = binStream.ReadGuid();
            }
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            xml.WriteAttributeString("ExportGuid", this.Proxy.ExportGuid.ToString());
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Test")) XmlStreamer.ExportCompoundObject(this.Any, xml, "Any", "Zetbox.App.Test");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Test")) XmlStreamer.ToStream(this.Proxy.DisplayName, xml, "DisplayName", "Zetbox.App.Test");
        }

        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Test|Any":
                XmlStreamer.MergeImportCompoundObject(this.AnyImpl, xml);
                break;
            case "Zetbox.App.Test|DisplayName":
                this.Proxy.DisplayName = XmlStreamer.ReadString(xml);
                break;
            case "Zetbox.App.Test|ExportGuid":
                // Import must have default value set
                this.Proxy.ExportGuid = XmlStreamer.ReadGuid(xml);
                this._isExportGuidSet = true;
                break;
            }
        }

        #endregion

    }
}