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
    [System.Diagnostics.DebuggerDisplay("PropertyGuidTest")]
    public class PropertyGuidTestNHibernateImpl : Zetbox.App.Test.PropertyTestBaseNHibernateImpl, PropertyGuidTest
    {
        private static readonly Guid _objectClassID = new Guid("3cd78f02-b9b0-4a15-a910-d2ae25c76219");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public PropertyGuidTestNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public PropertyGuidTestNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new PropertyGuidTestProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public PropertyGuidTestNHibernateImpl(Func<IFrozenContext> lazyCtx, PropertyGuidTestProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
            _isNullableWithDefaultSet = Proxy.ID > 0;
            _isStandardWithDefaultSet = Proxy.ID > 0;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly PropertyGuidTestProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Guid? Nullable
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Nullable;
                if (OnNullable_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid?>(__result);
                    OnNullable_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Nullable != value)
                {
                    var __oldValue = Proxy.Nullable;
                    var __newValue = value;
                    if (OnNullable_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnNullable_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Nullable", __oldValue, __newValue);
                    Proxy.Nullable = __newValue;
                    NotifyPropertyChanged("Nullable", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnNullable_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnNullable_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Nullable");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid?> OnNullable_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid?> OnNullable_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid?> OnNullable_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.PropertyGuidTest> OnNullable_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Guid? NullableWithDefault
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchNullableWithDefaultOrDefault();
                if (OnNullableWithDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid?>(__result);
                    OnNullableWithDefault_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isNullableWithDefaultSet = true;
                if (Proxy.NullableWithDefault != value)
                {
                    var __oldValue = Proxy.NullableWithDefault;
                    var __newValue = value;
                    if (OnNullableWithDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnNullableWithDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("NullableWithDefault", __oldValue, __newValue);
                    Proxy.NullableWithDefault = __newValue;
                    NotifyPropertyChanged("NullableWithDefault", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnNullableWithDefault_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid?>(__oldValue, __newValue);
                        OnNullableWithDefault_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("NullableWithDefault");
                }
            }
        }


        private Guid? FetchNullableWithDefaultOrDefault()
        {
            var __result = Proxy.NullableWithDefault;
                if (!_isNullableWithDefaultSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("14dcc090-f91d-4c4b-9070-3606bd610f8d"));
                    if (__p != null) {
                        _isNullableWithDefaultSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                            if (__tmp_value == null)
                                __result = this.Proxy.NullableWithDefault = null;
                            else
                            __result = this.Proxy.NullableWithDefault = (Guid)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Zetbox.App.Test.PropertyGuidTest.NullableWithDefault'");
                    }
                }
            return __result;
        }

        private bool _isNullableWithDefaultSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid?> OnNullableWithDefault_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid?> OnNullableWithDefault_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid?> OnNullableWithDefault_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.PropertyGuidTest> OnNullableWithDefault_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Guid Standard
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.Standard;
                if (OnStandard_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnStandard_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.Standard != value)
                {
                    var __oldValue = Proxy.Standard;
                    var __newValue = value;
                    if (OnStandard_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnStandard_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Standard", __oldValue, __newValue);
                    Proxy.Standard = __newValue;
                    NotifyPropertyChanged("Standard", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnStandard_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnStandard_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Standard");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid> OnStandard_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid> OnStandard_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid> OnStandard_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.PropertyGuidTest> OnStandard_IsValid;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Guid StandardWithDefault
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchStandardWithDefaultOrDefault();
                if (OnStandardWithDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Guid>(__result);
                    OnStandardWithDefault_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isStandardWithDefaultSet = true;
                if (Proxy.StandardWithDefault != value)
                {
                    var __oldValue = Proxy.StandardWithDefault;
                    var __newValue = value;
                    if (OnStandardWithDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnStandardWithDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("StandardWithDefault", __oldValue, __newValue);
                    Proxy.StandardWithDefault = __newValue;
                    NotifyPropertyChanged("StandardWithDefault", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnStandardWithDefault_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Guid>(__oldValue, __newValue);
                        OnStandardWithDefault_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("StandardWithDefault");
                }
            }
        }


        private Guid FetchStandardWithDefaultOrDefault()
        {
            var __result = Proxy.StandardWithDefault;
                if (!_isStandardWithDefaultSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("6d6e1b73-8b47-4d6d-879e-fa7cabbb3278"));
                    if (__p != null) {
                        _isStandardWithDefaultSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this.Proxy.StandardWithDefault = (Guid)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Zetbox.App.Test.PropertyGuidTest.StandardWithDefault'");
                    }
                }
            return __result;
        }

        private bool _isStandardWithDefaultSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid> OnStandardWithDefault_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid> OnStandardWithDefault_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.PropertyGuidTest, Guid> OnStandardWithDefault_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.PropertyGuidTest> OnStandardWithDefault_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(PropertyGuidTest);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (PropertyGuidTest)obj;
            var otherImpl = (PropertyGuidTestNHibernateImpl)obj;
            var me = (PropertyGuidTest)this;

            me.Nullable = other.Nullable;
            me.NullableWithDefault = other.NullableWithDefault;
            me.Standard = other.Standard;
            me.StandardWithDefault = other.StandardWithDefault;
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
                case "Nullable":
                case "NullableWithDefault":
                case "Standard":
                case "StandardWithDefault":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

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
                    new PropertyDescriptorNHibernateImpl<PropertyGuidTest, Guid?>(
                        lazyCtx,
                        new Guid("974e7c02-bdd2-4f59-a261-efa2c1bed043"),
                        "Nullable",
                        null,
                        obj => obj.Nullable,
                        (obj, val) => obj.Nullable = val,
						obj => OnNullable_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<PropertyGuidTest, Guid?>(
                        lazyCtx,
                        new Guid("14dcc090-f91d-4c4b-9070-3606bd610f8d"),
                        "NullableWithDefault",
                        null,
                        obj => obj.NullableWithDefault,
                        (obj, val) => obj.NullableWithDefault = val,
						obj => OnNullableWithDefault_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<PropertyGuidTest, Guid>(
                        lazyCtx,
                        new Guid("5cad6516-e9da-4b6e-97d7-ee91af20a1ef"),
                        "Standard",
                        null,
                        obj => obj.Standard,
                        (obj, val) => obj.Standard = val,
						obj => OnStandard_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<PropertyGuidTest, Guid>(
                        lazyCtx,
                        new Guid("6d6e1b73-8b47-4d6d-879e-fa7cabbb3278"),
                        "StandardWithDefault",
                        null,
                        obj => obj.StandardWithDefault,
                        (obj, val) => obj.StandardWithDefault = val,
						obj => OnStandardWithDefault_IsValid), 
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
        [EventBasedMethod("OnToString_PropertyGuidTest")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PropertyGuidTest != null)
            {
                OnToString_PropertyGuidTest(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<PropertyGuidTest> OnToString_PropertyGuidTest;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_PropertyGuidTest")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_PropertyGuidTest != null)
            {
                OnObjectIsValid_PropertyGuidTest(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<PropertyGuidTest> OnObjectIsValid_PropertyGuidTest;

        [EventBasedMethod("OnNotifyPreSave_PropertyGuidTest")]
        public override void NotifyPreSave()
        {
            FetchNullableWithDefaultOrDefault();
            FetchStandardWithDefaultOrDefault();
            base.NotifyPreSave();
            if (OnNotifyPreSave_PropertyGuidTest != null) OnNotifyPreSave_PropertyGuidTest(this);
        }
        public static event ObjectEventHandler<PropertyGuidTest> OnNotifyPreSave_PropertyGuidTest;

        [EventBasedMethod("OnNotifyPostSave_PropertyGuidTest")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_PropertyGuidTest != null) OnNotifyPostSave_PropertyGuidTest(this);
        }
        public static event ObjectEventHandler<PropertyGuidTest> OnNotifyPostSave_PropertyGuidTest;

        [EventBasedMethod("OnNotifyCreated_PropertyGuidTest")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Nullable");
            SetNotInitializedProperty("Standard");
            base.NotifyCreated();
            if (OnNotifyCreated_PropertyGuidTest != null) OnNotifyCreated_PropertyGuidTest(this);
        }
        public static event ObjectEventHandler<PropertyGuidTest> OnNotifyCreated_PropertyGuidTest;

        [EventBasedMethod("OnNotifyDeleting_PropertyGuidTest")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_PropertyGuidTest != null) OnNotifyDeleting_PropertyGuidTest(this);


        }
        public static event ObjectEventHandler<PropertyGuidTest> OnNotifyDeleting_PropertyGuidTest;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class PropertyGuidTestProxy
            : Zetbox.App.Test.PropertyTestBaseNHibernateImpl.PropertyTestBaseProxy
        {
            public PropertyGuidTestProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(PropertyGuidTestNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(PropertyGuidTestProxy); } }

            public virtual Guid? Nullable { get; set; }

            public virtual Guid? NullableWithDefault { get; set; }

            public virtual Guid Standard { get; set; }

            public virtual Guid StandardWithDefault { get; set; }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this.Proxy.Nullable);
            binStream.Write(this._isNullableWithDefaultSet);
            if (this._isNullableWithDefaultSet) {
                binStream.Write(this.Proxy.NullableWithDefault);
            }
            binStream.Write(this.Proxy.Standard);
            binStream.Write(this._isStandardWithDefaultSet);
            if (this._isStandardWithDefaultSet) {
                binStream.Write(this.Proxy.StandardWithDefault);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this.Proxy.Nullable = binStream.ReadNullableGuid();
            this._isNullableWithDefaultSet = binStream.ReadBoolean();
            if (this._isNullableWithDefaultSet) {
                this.Proxy.NullableWithDefault = binStream.ReadNullableGuid();
            }
            this.Proxy.Standard = binStream.ReadGuid();
            this._isStandardWithDefaultSet = binStream.ReadBoolean();
            if (this._isStandardWithDefaultSet) {
                this.Proxy.StandardWithDefault = binStream.ReadGuid();
            }
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}