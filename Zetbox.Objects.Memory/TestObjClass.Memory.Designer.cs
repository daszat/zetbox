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

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestObjClass")]
    public class TestObjClassMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, TestObjClass
    {
        private static readonly Guid _objectClassID = new Guid("19f38f05-e88e-44c6-bfdf-d502b3632028");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public TestObjClassMemoryImpl()
            : base(null)
        {
        }

        public TestObjClassMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // enumeration property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public Zetbox.App.Test.TestEnum CalculatedEnumeration
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _CalculatedEnumeration;
                if (_CalculatedEnumeration_IsDirty && OnCalculatedEnumeration_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Test.TestEnum>(__result);
                    OnCalculatedEnumeration_Getter(this, __e);
                    _CalculatedEnumeration_IsDirty = false;
                    __result = _CalculatedEnumeration = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_CalculatedEnumeration != value)
                {
                    var __oldValue = _CalculatedEnumeration;
                    var __newValue = value;
                    NotifyPropertyChanging("CalculatedEnumeration", __oldValue, __newValue);
                    _CalculatedEnumeration = __newValue;
                    NotifyPropertyChanged("CalculatedEnumeration", __oldValue, __newValue);
			        _CalculatedEnumeration_IsDirty = false;

                }
				else 
				{
					SetInitializedProperty("CalculatedEnumeration");
				}
            }
        }
        private Zetbox.App.Test.TestEnum _CalculatedEnumeration;
        private bool _CalculatedEnumeration_IsDirty = false;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnCalculatedEnumeration_Getter;

        /// <summary>
        /// test
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int? MyIntProperty
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _MyIntProperty;
                if (OnMyIntProperty_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnMyIntProperty_Getter(this, __e);
                    __result = _MyIntProperty = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_MyIntProperty != value)
                {
                    var __oldValue = _MyIntProperty;
                    var __newValue = value;
                    if (OnMyIntProperty_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMyIntProperty_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("MyIntProperty", __oldValue, __newValue);
                    _MyIntProperty = __newValue;
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
        private int? _MyIntProperty;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnMyIntProperty_IsValid;

        /// <summary>
        /// testtest
        /// </summary>
	        // BEGIN Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ObjectProp
        // fkBackingName=_fk_ObjectProp; fkGuidBackingName=_fk_guid_ObjectProp;
        // referencedInterface=Zetbox.App.Projekte.Kunde; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=none; is reference;
        // PositionStorage=none;
        // Target not exportable; does call events

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        // BEGIN Zetbox.Generator.Templates.Properties.DelegatingProperty
        public Zetbox.App.Projekte.Kunde ObjectProp
        {
            get { return ObjectPropImpl; }
            set { ObjectPropImpl = (Zetbox.App.Projekte.KundeMemoryImpl)value; }
        }
        // END Zetbox.Generator.Templates.Properties.DelegatingProperty

        private int? __fk_ObjectPropCache;

        private int? _fk_ObjectProp {
            get
            {
                return __fk_ObjectPropCache;
            }
            set
            {
                __fk_ObjectPropCache = value;
                // Recreate task to clear it's cache
                _triggerFetchObjectPropTask = null;
            }
        }


        Zetbox.API.Async.ZbTask<Zetbox.App.Projekte.Kunde> _triggerFetchObjectPropTask;
        public Zetbox.API.Async.ZbTask<Zetbox.App.Projekte.Kunde> TriggerFetchObjectPropAsync()
        {
            if (_triggerFetchObjectPropTask != null) return _triggerFetchObjectPropTask;

            if (_fk_ObjectProp.HasValue)
                _triggerFetchObjectPropTask = Context.FindAsync<Zetbox.App.Projekte.Kunde>(_fk_ObjectProp.Value);
            else
                _triggerFetchObjectPropTask = new Zetbox.API.Async.ZbTask<Zetbox.App.Projekte.Kunde>(Zetbox.API.Async.ZbTask.Synchron, () => null);

            _triggerFetchObjectPropTask.OnResult(t =>
            {
                if (OnObjectProp_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Projekte.Kunde>(t.Result);
                    OnObjectProp_Getter(this, e);
                    t.Result = e.Result;
                }
            });

            return _triggerFetchObjectPropTask;
        }

        // internal implementation
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Zetbox.App.Projekte.KundeMemoryImpl ObjectPropImpl
        {
            get
            {
                return (Zetbox.App.Projekte.KundeMemoryImpl)TriggerFetchObjectPropAsync().Result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if ((value == null && _fk_ObjectProp == null) || (value != null && value.ID == _fk_ObjectProp))
				{
					SetInitializedProperty("ObjectProp");
                    return;
				}

                // cache old value to remove inverse references later
                var __oldValue = ObjectPropImpl;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);

                if (OnObjectProp_PreSetter != null && IsAttached)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Projekte.Kunde>(__oldValue, __newValue);
                    OnObjectProp_PreSetter(this, e);
                    __newValue = (Zetbox.App.Projekte.KundeMemoryImpl)e.Result;
                }

                // next, set the local reference
                _fk_ObjectProp = __newValue == null ? (int?)null : __newValue.ID;

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
        // END Zetbox.Generator.Templates.Properties.ObjectReferencePropertyTemplate for ObjectProp
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnObjectProp_IsValid;

        /// <summary>
        /// String Property
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string StringProp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _StringProp;
                if (OnStringProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnStringProp_Getter(this, __e);
                    __result = _StringProp = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_StringProp != value)
                {
                    var __oldValue = _StringProp;
                    var __newValue = value;
                    if (OnStringProp_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("StringProp", __oldValue, __newValue);
                    _StringProp = __newValue;
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
        private string _StringProp;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnStringProp_IsValid;

        /// <summary>
        /// Test Enumeration Property
        /// </summary>
        // enumeration property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public Zetbox.App.Test.TestEnum TestEnumProp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestEnumProp;
                if (OnTestEnumProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Test.TestEnum>(__result);
                    OnTestEnumProp_Getter(this, __e);
                    __result = _TestEnumProp = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TestEnumProp != value)
                {
                    var __oldValue = _TestEnumProp;
                    var __newValue = value;
                    if (OnTestEnumProp_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestEnumProp", __oldValue, __newValue);
                    _TestEnumProp = __newValue;
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
        private Zetbox.App.Test.TestEnum _TestEnumProp;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnTestEnumProp_IsValid;

        /// <summary>
        /// Tests whether enums with defaults work
        /// </summary>
        // enumeration property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public Zetbox.App.Test.TestEnum TestEnumWithDefault
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestEnumWithDefault;
                if (!_isTestEnumWithDefaultSet && ObjectState == DataObjectState.New) {
                    var __p = FrozenContext.FindPersistenceObject<Zetbox.App.Base.Property>(new Guid("bfb214b1-b933-4810-b33b-98a1276b84b3"));
                    if (__p != null) {
                        _isTestEnumWithDefaultSet = true;
                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum
                        object __tmp_value = __p.DefaultValue.GetDefaultValue();
                        __result = this._TestEnumWithDefault = (Zetbox.App.Test.TestEnum)__tmp_value;
                    } else {
                        Zetbox.API.Utils.Logging.Log.Warn("Unable to get default value for property 'TestObjClass.TestEnumWithDefault'");
                    }
                }
                if (OnTestEnumWithDefault_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Test.TestEnum>(__result);
                    OnTestEnumWithDefault_Getter(this, __e);
                    __result = _TestEnumWithDefault = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                _isTestEnumWithDefaultSet = true;
                if (_TestEnumWithDefault != value)
                {
                    var __oldValue = _TestEnumWithDefault;
                    var __newValue = value;
                    if (OnTestEnumWithDefault_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumWithDefault_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestEnumWithDefault", __oldValue, __newValue);
                    _TestEnumWithDefault = __newValue;
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
        private Zetbox.App.Test.TestEnum _TestEnumWithDefault;
        private bool _isTestEnumWithDefaultSet = false;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumWithDefault_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumWithDefault_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumWithDefault_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestObjClass> OnTestEnumWithDefault_IsValid;

        /// <summary>
        /// testmethod
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnTestMethod_TestObjClass")]
        public virtual void TestMethod(DateTime DateTimeParamForTestMethod)
        {
            // base.TestMethod();
            if (OnTestMethod_TestObjClass != null)
            {
                OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            }
            else
            {
                throw new NotImplementedException("No handler registered on method TestObjClass.TestMethod");
            }
        }
        public delegate void TestMethod_Handler<T>(T obj, DateTime DateTimeParamForTestMethod);
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
            var otherImpl = (TestObjClassMemoryImpl)obj;
            var me = (TestObjClass)this;

            this.CalculatedEnumeration = otherImpl.CalculatedEnumeration;
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
                        var __oldValue = _fk_ObjectProp;
                        var __newValue = parentObj == null ? (int?)null : parentObj.ID;
                        NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);
                        _fk_ObjectProp = __newValue;
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

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "ObjectProp":
                return TriggerFetchObjectPropAsync();
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

            if (_fk_ObjectProp.HasValue)
                ObjectPropImpl = (Zetbox.App.Projekte.KundeMemoryImpl)Context.Find<Zetbox.App.Projekte.Kunde>(_fk_ObjectProp.Value);
            else
                ObjectPropImpl = null;
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
                    new PropertyDescriptorMemoryImpl<TestObjClass, Zetbox.App.Test.TestEnum>(
                        lazyCtx,
                        new Guid("e5101331-312b-4518-b7f8-750ca7b61d80"),
                        "CalculatedEnumeration",
                        null,
                        obj => obj.CalculatedEnumeration,
                        null, // calculated property
						null), // no constraints on calculated properties
                    // else
                    new PropertyDescriptorMemoryImpl<TestObjClass, int?>(
                        lazyCtx,
                        new Guid("29c0242b-cd1c-42b4-8ca0-be0a209afcbf"),
                        "MyIntProperty",
                        null,
                        obj => obj.MyIntProperty,
                        (obj, val) => obj.MyIntProperty = val,
						obj => OnMyIntProperty_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<TestObjClass, Zetbox.App.Projekte.Kunde>(
                        lazyCtx,
                        new Guid("e93b3fc2-2fc9-4577-9a93-a51ed2a4190f"),
                        "ObjectProp",
                        null,
                        obj => obj.ObjectProp,
                        (obj, val) => obj.ObjectProp = val,
						obj => OnObjectProp_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<TestObjClass, string>(
                        lazyCtx,
                        new Guid("c9a3769e-7a53-4e1d-b894-72dc1b4e9aea"),
                        "StringProp",
                        null,
                        obj => obj.StringProp,
                        (obj, val) => obj.StringProp = val,
						obj => OnStringProp_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<TestObjClass, Zetbox.App.Test.TestEnum>(
                        lazyCtx,
                        new Guid("89470dda-4ac6-4bb4-9221-d16f80f8d95a"),
                        "TestEnumProp",
                        null,
                        obj => obj.TestEnumProp,
                        (obj, val) => obj.TestEnumProp = val,
						obj => OnTestEnumProp_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<TestObjClass, Zetbox.App.Test.TestEnum>(
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
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

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
            e.IsValid = b.IsValid;
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
            ObjectProp = null;
        }
        public static event ObjectEventHandler<TestObjClass> OnNotifyDeleting_TestObjClass;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write((int?)this._CalculatedEnumeration);
            binStream.Write(this._MyIntProperty);
            binStream.Write(ObjectProp != null ? ObjectProp.ID : (int?)null);
            binStream.Write(this._StringProp);
            binStream.Write((int?)this._TestEnumProp);
            binStream.Write((int?)this._TestEnumWithDefault);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._CalculatedEnumeration = (Zetbox.App.Test.TestEnum)binStream.ReadNullableInt32();
            this._MyIntProperty = binStream.ReadNullableInt32();
            this._fk_ObjectProp = binStream.ReadNullableInt32();
            this._StringProp = binStream.ReadString();
            this._TestEnumProp = (Zetbox.App.Test.TestEnum)binStream.ReadNullableInt32();
            this._TestEnumWithDefault = (Zetbox.App.Test.TestEnum)binStream.ReadNullableInt32();
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