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
    /// A TestClass with many properties
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestObjClass")]
    public class TestObjClassNHibernateImpl : Zetbox.DalProvider.NHibernate.DataObjectNHibernateImpl, TestObjClass
    {
        private static readonly Guid _objectClassID = new Guid("19f38f05-e88e-44c6-bfdf-d502b3632028");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public TestObjClassNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public TestObjClassNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new TestObjClassProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public TestObjClassNHibernateImpl(Func<IFrozenContext> lazyCtx, TestObjClassProxy proxy)
            : base(lazyCtx) // do not pass proxy to base data object
        {
            this.Proxy = proxy;
            _isTestEnumWithDefaultSet = Proxy.ID > 0;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal readonly TestObjClassProxy Proxy;

        /// <summary>
        /// 
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Zetbox.App.Test.TestEnum CalculatedEnumeration
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchCalculatedEnumerationOrDefault();
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.CalculatedEnumeration != value)
                {
                    var __oldValue = Proxy.CalculatedEnumeration;
                    var __newValue = value;
                    NotifyPropertyChanging("CalculatedEnumeration", __oldValue, __newValue);
                    Proxy.CalculatedEnumeration = __newValue;
                    NotifyPropertyChanged("CalculatedEnumeration", __oldValue, __newValue);
                    _CalculatedEnumeration_IsDirty = false;

                }
                else
                {
                    SetInitializedProperty("CalculatedEnumeration");
                }
            }
        }
        private bool _CalculatedEnumeration_IsDirty = false;


        private Zetbox.App.Test.TestEnum FetchCalculatedEnumerationOrDefault()
        {
           var __result = Proxy.CalculatedEnumeration;
            if (_CalculatedEnumeration_IsDirty && OnCalculatedEnumeration_Getter != null)
            {
                var __e = new PropertyGetterEventArgs<Zetbox.App.Test.TestEnum>(__result);
                OnCalculatedEnumeration_Getter(this, __e);
                _CalculatedEnumeration_IsDirty = false;
                __result = Proxy.CalculatedEnumeration = __e.Result;
            }
            return __result;
        }

        private bool _isCalculatedEnumerationSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnCalculatedEnumeration_Getter;

        /// <summary>
        /// test
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public int? MyIntProperty
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.MyIntProperty;
                if (OnMyIntProperty_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnMyIntProperty_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.MyIntProperty != value)
                {
                    var __oldValue = Proxy.MyIntProperty;
                    var __newValue = value;
                    if (OnMyIntProperty_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMyIntProperty_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("MyIntProperty", __oldValue, __newValue);
                    Proxy.MyIntProperty = __newValue;
                    NotifyPropertyChanged("MyIntProperty", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnMyIntProperty_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMyIntProperty_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("MyIntProperty");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnMyIntProperty_IsValid;

        /// <summary>
        /// testtest
        /// </summary>
        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ObjectProp
        // fkBackingName=this.Proxy.ObjectProp; fkGuidBackingName=_fk_guid_ObjectProp;
        // referencedInterface=Zetbox.App.Projekte.Kunde; moduleNamespace=Zetbox.App.Test;
        // no inverse navigator handling
        // PositionStorage=none;
        // Target not exportable; does call events
        
        public System.Threading.Tasks.Task<Zetbox.App.Projekte.Kunde> GetProp_ObjectProp()
        {
            return System.Threading.Tasks.Task.FromResult(ObjectProp);
        }

        public async System.Threading.Tasks.Task SetProp_ObjectProp(Zetbox.App.Projekte.Kunde newValue)
        {
            await TriggerFetchObjectPropAsync();
            ObjectProp = newValue;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		[System.Runtime.Serialization.IgnoreDataMember]
        public Zetbox.App.Projekte.Kunde ObjectProp
        {
            get
            {
                Zetbox.App.Projekte.KundeNHibernateImpl __value = (Zetbox.App.Projekte.KundeNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ObjectProp);

                if (OnObjectProp_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Projekte.Kunde>(__value);
                    OnObjectProp_Getter(this, e);
                    __value = (Zetbox.App.Projekte.KundeNHibernateImpl)e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noop with nulls
                if (value == null && this.Proxy.ObjectProp == null)
                {
                    SetInitializedProperty("ObjectProp");
                    return;
                }

                // cache old value to remove inverse references later
                var __oldValue = (Zetbox.App.Projekte.KundeNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ObjectProp);
                var __newValue = (Zetbox.App.Projekte.KundeNHibernateImpl)value;

                // shortcut noop on objects
                // can't use proxy's ID here, since that might be INVALIDID before persisting the first time.
                if (__oldValue == __newValue)
                {
                    SetInitializedProperty("ObjectProp");
                    return;
                }

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);

                if (OnObjectProp_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Projekte.Kunde>(__oldValue, __newValue);
                    OnObjectProp_PreSetter(this, e);
                    __newValue = (Zetbox.App.Projekte.KundeNHibernateImpl)e.Result;
                }

                // next, set the local reference
                if (__newValue == null)
                {
                    this.Proxy.ObjectProp = null;
                }
                else
                {
                    this.Proxy.ObjectProp = __newValue.Proxy;
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("ObjectProp", __oldValue, __newValue);
                if(IsAttached) UpdateChangedInfo = true;

                if (OnObjectProp_PostSetter != null && IsAttached)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Projekte.Kunde>(__oldValue, __newValue);
                    OnObjectProp_PostSetter(this, e);
                }
            }
        }

        /// <summary>Backing store for ObjectProp's id, used on dehydration only</summary>
        private int? _fk_ObjectProp = null;

        /// <summary>ForeignKey Property for ObjectProp's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_ObjectProp
		{
			get { return ObjectProp != null ? ObjectProp.ID : (int?)null; }
			set { _fk_ObjectProp = value; }
		}


    public System.Threading.Tasks.Task TriggerFetchObjectPropAsync()
    {
        return System.Threading.Tasks.Task.FromResult<Zetbox.App.Projekte.Kunde>(this.ObjectProp);
    }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ObjectProp
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnObjectProp_IsValid;

        /// <summary>
        /// String Property
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public string StringProp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.StringProp;
                if (OnStringProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnStringProp_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.StringProp != value)
                {
                    var __oldValue = Proxy.StringProp;
                    var __newValue = value;
                    if (OnStringProp_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("StringProp", __oldValue, __newValue);
                    Proxy.StringProp = __newValue;
                    NotifyPropertyChanged("StringProp", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnStringProp_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("StringProp");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnStringProp_IsValid;

        /// <summary>
        /// Test Enumeration Property
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Zetbox.App.Test.TestEnum TestEnumProp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = Proxy.TestEnumProp;
                if (OnTestEnumProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Test.TestEnum>(__result);
                    OnTestEnumProp_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (Proxy.TestEnumProp != value)
                {
                    var __oldValue = Proxy.TestEnumProp;
                    var __newValue = value;
                    if (OnTestEnumProp_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestEnumProp", __oldValue, __newValue);
                    Proxy.TestEnumProp = __newValue;
                    NotifyPropertyChanged("TestEnumProp", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnTestEnumProp_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumProp_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("TestEnumProp");
                }
            }
        }

        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnTestEnumProp_IsValid;

        /// <summary>
        /// Tests whether enums with defaults work
        /// </summary>

        // BEGIN Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
        public Zetbox.App.Test.TestEnum TestEnumWithDefault
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = FetchTestEnumWithDefaultOrDefault();
                if (OnTestEnumWithDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Test.TestEnum>(__result);
                    OnTestEnumWithDefault_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isTestEnumWithDefaultSet = true;
                if (Proxy.TestEnumWithDefault != value)
                {
                    var __oldValue = Proxy.TestEnumWithDefault;
                    var __newValue = value;
                    if (OnTestEnumWithDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumWithDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestEnumWithDefault", __oldValue, __newValue);
                    Proxy.TestEnumWithDefault = __newValue;
                    NotifyPropertyChanged("TestEnumWithDefault", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnTestEnumWithDefault_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumWithDefault_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("TestEnumWithDefault");
                }
            }
        }


        private Zetbox.App.Test.TestEnum FetchTestEnumWithDefaultOrDefault()
        {
            var __result = Proxy.TestEnumWithDefault;
                if (!_isTestEnumWithDefaultSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("bfb214b1-b933-4810-b33b-98a1276b84b3"));
                    if (__p != null) {
                        _isTestEnumWithDefaultSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this.Proxy.TestEnumWithDefault = (Zetbox.App.Test.TestEnum)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'Zetbox.App.Test.TestObjClass.TestEnumWithDefault'");
                    }
                }
            return __result;
        }

        private bool _isTestEnumWithDefaultSet = false;
        // END Zetbox.DalProvider.NHibernate.Generator.Templates.Properties.ProxyProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumWithDefault_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumWithDefault_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumWithDefault_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnTestEnumWithDefault_IsValid;

        /// <summary>
        /// testmethod
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnTestMethod_TestObjClass")]
        public virtual async System.Threading.Tasks.Task TestMethod(DateTime DateTimeParamForTestMethod)
        {
            // base.TestMethod();
            if (OnTestMethod_TestObjClass != null)
            {
                await OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method TestObjClass.TestMethod");
            }
        }
        public delegate System.Threading.Tasks.Task TestMethod_Handler<T>(T obj, DateTime DateTimeParamForTestMethod);
        public static event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<TestObjClass> OnTestMethod_TestObjClass_CanExec;

        [EventBasedMethod("OnTestMethod_TestObjClass_CanExec")]
        public virtual bool TestMethodCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnTestMethod_TestObjClass_CanExec != null)
				{
					OnTestMethod_TestObjClass_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<TestObjClass> OnTestMethod_TestObjClass_CanExecReason;

        [EventBasedMethod("OnTestMethod_TestObjClass_CanExecReason")]
        public virtual string TestMethodCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnTestMethod_TestObjClass_CanExecReason != null)
				{
					OnTestMethod_TestObjClass_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(TestObjClass);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TestObjClass)obj;
            var otherImpl = (TestObjClassNHibernateImpl)obj;
            var me = (TestObjClass)this;

            me.MyIntProperty = other.MyIntProperty;
            me.StringProp = other.StringProp;
            me.TestEnumProp = other.TestEnumProp;
            me.TestEnumWithDefault = other.TestEnumWithDefault;
            this._fk_ObjectProp = otherImpl._fk_ObjectProp;
        }
        public override void SetNew()
        {
            base.SetNew();
            _CalculatedEnumeration_IsDirty = true;
        }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            switch(propertyName)
            {
                case "ObjectProp":
                    {
                        var __oldValue = (Zetbox.App.Projekte.KundeNHibernateImpl)OurContext.AttachAndWrap(this.Proxy.ObjectProp);
                        var __newValue = (Zetbox.App.Projekte.KundeNHibernateImpl)parentObj;
                        NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);
                        this.Proxy.ObjectProp = __newValue == null ? null : __newValue.Proxy;
                        NotifyPropertyChanged("ObjectProp", __oldValue, __newValue);
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
                case "MyIntProperty":
                case "ObjectProp":
                case "StringProp":
                case "TestEnumProp":
                case "TestEnumWithDefault":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        public override void Recalculate(string property)
        {
            switch (property)
            {
                case "CalculatedEnumeration":
                    NotifyPropertyChanging(property, null, null);
                    _CalculatedEnumeration_IsDirty = true;
                    NotifyPropertyChanged(property, null, null);
                    return;
            }

            base.Recalculate(property);
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "ObjectProp":
                return TriggerFetchObjectPropAsync();
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

            if (_fk_ObjectProp.HasValue)
                this.ObjectProp = ((Zetbox.App.Projekte.KundeNHibernateImpl)(await OurContext.FindPersistenceObjectAsync<Zetbox.App.Projekte.Kunde>(_fk_ObjectProp.Value)));
            else
                this.ObjectProp = null;
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
                    new PropertyDescriptorNHibernateImpl<TestObjClass, Zetbox.App.Test.TestEnum>(
                        lazyCtx,
                        new Guid("e5101331-312b-4518-b7f8-750ca7b61d80"),
                        "CalculatedEnumeration",
                        null,
                        obj => obj.CalculatedEnumeration,
                        null, // calculated property
						null), // no constraints on calculated properties
                    // else
                    new PropertyDescriptorNHibernateImpl<TestObjClass, int?>(
                        lazyCtx,
                        new Guid("29c0242b-cd1c-42b4-8ca0-be0a209afcbf"),
                        "MyIntProperty",
                        null,
                        obj => obj.MyIntProperty,
                        (obj, val) => obj.MyIntProperty = val,
						obj => OnMyIntProperty_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<TestObjClass, Zetbox.App.Projekte.Kunde>(
                        lazyCtx,
                        new Guid("e93b3fc2-2fc9-4577-9a93-a51ed2a4190f"),
                        "ObjectProp",
                        null,
                        obj => obj.ObjectProp,
                        (obj, val) => obj.ObjectProp = val,
						obj => OnObjectProp_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<TestObjClass, string>(
                        lazyCtx,
                        new Guid("c9a3769e-7a53-4e1d-b894-72dc1b4e9aea"),
                        "StringProp",
                        null,
                        obj => obj.StringProp,
                        (obj, val) => obj.StringProp = val,
						obj => OnStringProp_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<TestObjClass, Zetbox.App.Test.TestEnum>(
                        lazyCtx,
                        new Guid("89470dda-4ac6-4bb4-9221-d16f80f8d95a"),
                        "TestEnumProp",
                        null,
                        obj => obj.TestEnumProp,
                        (obj, val) => obj.TestEnumProp = val,
						obj => OnTestEnumProp_IsValid), 
                    // else
                    new PropertyDescriptorNHibernateImpl<TestObjClass, Zetbox.App.Test.TestEnum>(
                        lazyCtx,
                        new Guid("bfb214b1-b933-4810-b33b-98a1276b84b3"),
                        "TestEnumWithDefault",
                        null,
                        obj => obj.TestEnumWithDefault,
                        (obj, val) => obj.TestEnumWithDefault = val,
						obj => OnTestEnumWithDefault_IsValid), 
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
        [EventBasedMethod("OnToString_TestObjClass")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestObjClass != null)
            {
                OnToString_TestObjClass(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_TestObjClass")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_TestObjClass != null)
            {
                OnObjectIsValid_TestObjClass(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<TestObjClass> OnObjectIsValid_TestObjClass;

        [EventBasedMethod("OnNotifyPreSave_TestObjClass")]
        public override void NotifyPreSave()
        {
            FetchCalculatedEnumerationOrDefault();
            FetchTestEnumWithDefaultOrDefault();
            base.NotifyPreSave();
            if (OnNotifyPreSave_TestObjClass != null) OnNotifyPreSave_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnNotifyPreSave_TestObjClass;

        [EventBasedMethod("OnNotifyPostSave_TestObjClass")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_TestObjClass != null) OnNotifyPostSave_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnNotifyPostSave_TestObjClass;

        [EventBasedMethod("OnNotifyCreated_TestObjClass")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("MyIntProperty");
            SetNotInitializedProperty("ObjectProp");
            SetNotInitializedProperty("StringProp");
            SetNotInitializedProperty("TestEnumProp");
            _CalculatedEnumeration_IsDirty = true;
            base.NotifyCreated();
            if (OnNotifyCreated_TestObjClass != null) OnNotifyCreated_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnNotifyCreated_TestObjClass;

        [EventBasedMethod("OnNotifyDeleting_TestObjClass")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_TestObjClass != null) OnNotifyDeleting_TestObjClass(this);

            // FK_TestObjClass_has_ObjectProp
            if (ObjectProp != null) {
                ((NHibernatePersistenceObject)ObjectProp).ChildrenToDelete.Add(this);
                ParentsToDelete.Add((NHibernatePersistenceObject)ObjectProp);
            }

            ObjectProp = null;
        }
        public static event ObjectEventHandler<TestObjClass> OnNotifyDeleting_TestObjClass;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class TestObjClassProxy
            : IProxyObject, ISortKey<int>
        {
            public TestObjClassProxy()
            {
            }

            public virtual int ID { get; set; }

            public virtual Type ZetboxWrapper { get { return typeof(TestObjClassNHibernateImpl); } }
            public virtual Type ZetboxProxy { get { return typeof(TestObjClassProxy); } }

            public virtual Zetbox.App.Test.TestEnum CalculatedEnumeration { get; set; }

            public virtual int? MyIntProperty { get; set; }

            public virtual Zetbox.App.Projekte.KundeNHibernateImpl.KundeProxy ObjectProp { get; set; }

            public virtual string StringProp { get; set; }

            public virtual Zetbox.App.Test.TestEnum TestEnumProp { get; set; }

            public virtual Zetbox.App.Test.TestEnum TestEnumWithDefault { get; set; }


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
            binStream.Write((int?)Proxy.CalculatedEnumeration);
            binStream.Write(this.Proxy.MyIntProperty);
            binStream.Write(this.Proxy.ObjectProp != null ? OurContext.GetIdFromProxy(this.Proxy.ObjectProp) : (int?)null);
            binStream.Write(this.Proxy.StringProp);
            binStream.Write((int?)Proxy.TestEnumProp);
            binStream.Write(this._isTestEnumWithDefaultSet);
            if (this._isTestEnumWithDefaultSet) {
                binStream.Write((int?)Proxy.TestEnumWithDefault);
            }
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            Proxy.CalculatedEnumeration = (Zetbox.App.Test.TestEnum)binStream.ReadNullableInt32();
            this.Proxy.MyIntProperty = binStream.ReadNullableInt32();
            binStream.Read(out this._fk_ObjectProp);
            this.Proxy.StringProp = binStream.ReadString();
            Proxy.TestEnumProp = (Zetbox.App.Test.TestEnum)binStream.ReadNullableInt32();
            this._isTestEnumWithDefaultSet = binStream.ReadBoolean();
            if (this._isTestEnumWithDefaultSet) {
                Proxy.TestEnumWithDefault = (Zetbox.App.Test.TestEnum)binStream.ReadNullableInt32();
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