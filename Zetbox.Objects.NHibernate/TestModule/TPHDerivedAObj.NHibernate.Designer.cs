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
    /// TPH derived object named &quot;A&quot;
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TPHDerivedAObj")]
    public class TPHDerivedAObjNHibernateImpl : Zetbox.App.Test.TPHBaseObjNHibernateImpl, TPHDerivedAObj
    {
        private static readonly Guid _objectClassID = new Guid("6275b4a6-8b7f-44d5-8a29-4fa39e81c688");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public TPHDerivedAObjNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public TPHDerivedAObjNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new TPHDerivedAObjProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public TPHDerivedAObjNHibernateImpl(Func<IFrozenContext> lazyCtx, TPHDerivedAObjProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly TPHDerivedAObjProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int AInt
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.AInt;
                if (OnAInt_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnAInt_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.AInt != value)
                {
                    var __oldValue = Proxy.AInt;
                    var __newValue = value;
                    if (OnAInt_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnAInt_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("AInt", __oldValue, __newValue);
                    Proxy.AInt = __newValue;
                    NotifyPropertyChanged("AInt", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnAInt_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnAInt_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("AInt");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TPHDerivedAObj, int> OnAInt_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TPHDerivedAObj, int> OnAInt_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TPHDerivedAObj, int> OnAInt_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TPHDerivedAObj> OnAInt_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string AString
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.AString;
                if (OnAString_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnAString_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.AString != value)
                {
                    var __oldValue = Proxy.AString;
                    var __newValue = value;
                    if (OnAString_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnAString_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("AString", __oldValue, __newValue);
                    Proxy.AString = __newValue;
                    NotifyPropertyChanged("AString", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnAString_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnAString_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("AString");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TPHDerivedAObj, string> OnAString_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TPHDerivedAObj, string> OnAString_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TPHDerivedAObj, string> OnAString_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TPHDerivedAObj> OnAString_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(TPHDerivedAObj);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TPHDerivedAObj)obj;
            var otherImpl = (TPHDerivedAObjNHibernateImpl)obj;
            var me = (TPHDerivedAObj)this;

            me.AInt = other.AInt;
            me.AString = other.AString;
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
                case "AInt":
                case "AString":
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
                    new PropertyDescriptorNHibernateImpl<TPHDerivedAObj, int>(
                        lazyCtx,
                        new Guid("1d532ccc-af58-4ae6-8819-bfe55527b0c9"),
                        "AInt",
                        null,
                        obj => obj.AInt,
                        (obj, val) => obj.AInt = val,
						obj => OnAInt_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<TPHDerivedAObj, string>(
                        lazyCtx,
                        new Guid("3515669a-3901-499a-9ee6-cb91577f81e7"),
                        "AString",
                        null,
                        obj => obj.AString,
                        (obj, val) => obj.AString = val,
						obj => OnAString_IsValid), 
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
        [EventBasedMethod("OnToString_TPHDerivedAObj")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TPHDerivedAObj != null)
            {
                OnToString_TPHDerivedAObj(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TPHDerivedAObj> OnToString_TPHDerivedAObj;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_TPHDerivedAObj")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_TPHDerivedAObj != null)
            {
                OnObjectIsValid_TPHDerivedAObj(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<TPHDerivedAObj> OnObjectIsValid_TPHDerivedAObj;

        [EventBasedMethod("OnNotifyPreSave_TPHDerivedAObj")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_TPHDerivedAObj != null) OnNotifyPreSave_TPHDerivedAObj(this);
        }
        public static event ObjectEventHandler<TPHDerivedAObj> OnNotifyPreSave_TPHDerivedAObj;

        [EventBasedMethod("OnNotifyPostSave_TPHDerivedAObj")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_TPHDerivedAObj != null) OnNotifyPostSave_TPHDerivedAObj(this);
        }
        public static event ObjectEventHandler<TPHDerivedAObj> OnNotifyPostSave_TPHDerivedAObj;

        [EventBasedMethod("OnNotifyCreated_TPHDerivedAObj")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("AInt");
            SetNotInitializedProperty("AString");
            base.NotifyCreated();
            if (OnNotifyCreated_TPHDerivedAObj != null) OnNotifyCreated_TPHDerivedAObj(this);
        }
        public static event ObjectEventHandler<TPHDerivedAObj> OnNotifyCreated_TPHDerivedAObj;

        [EventBasedMethod("OnNotifyDeleting_TPHDerivedAObj")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_TPHDerivedAObj != null) OnNotifyDeleting_TPHDerivedAObj(this);


        }
        public static event ObjectEventHandler<TPHDerivedAObj> OnNotifyDeleting_TPHDerivedAObj;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class TPHDerivedAObjProxy
            : Zetbox.App.Test.TPHBaseObjNHibernateImpl.TPHBaseObjProxy
        {
            public TPHDerivedAObjProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(TPHDerivedAObjNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(TPHDerivedAObjProxy); } }

            public virtual int AInt { get; set; }

            public virtual string AString { get; set; }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.AInt);
            binStream.Write(this.Proxy.AString);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this.Proxy.AInt = binStream.ReadInt32();
            this.Proxy.AString = binStream.ReadString();
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
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Test")) XmlStreamer.ToStream(this.Proxy.AInt, xml, "AInt", "Zetbox.App.Test");
            if (modules.Contains("*") || modules.Contains("Zetbox.App.Test")) XmlStreamer.ToStream(this.Proxy.AString, xml, "AString", "Zetbox.App.Test");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            switch (xml.NamespaceURI + "|" + xml.LocalName) {
            case "Zetbox.App.Test|AInt":
                this.Proxy.AInt = XmlStreamer.ReadInt32(xml);
                break;
            case "Zetbox.App.Test|AString":
                this.Proxy.AString = XmlStreamer.ReadString(xml);
                break;
            }
        }

        #endregion

    }
}