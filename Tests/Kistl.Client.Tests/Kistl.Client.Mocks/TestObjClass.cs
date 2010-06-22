
namespace Kistl.App.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Kistl.API;
    using Kistl.API.Client;

    public class TestObjClass__Implementation__ : BaseClientDataObject, TestObjClass
    {
        public TestObjClass__Implementation__()
            : base(null)
        {
        }

        /// <summary>
        /// test
        /// </summary>
        // value type property
        // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual int? MyIntProperty
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
                    __result = __e.Result;
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
                    if (OnMyIntProperty_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMyIntProperty_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("MyIntProperty", __oldValue, __newValue);
                    _MyIntProperty = __newValue;
                    NotifyPropertyChanged("MyIntProperty", __oldValue, __newValue);
                    if (OnMyIntProperty_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMyIntProperty_PostSetter(this, __e);
                    }
                }
            }
        }
        private int? _MyIntProperty;
        public static event PropertyGetterHandler<Kistl.App.Test.TestObjClass, int?> OnMyIntProperty_Getter;
        public static event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, int?> OnMyIntProperty_PreSetter;
        public static event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, int?> OnMyIntProperty_PostSetter;

        /// <summary>
        /// testtest
        /// </summary>
        // object reference property
        // BEGIN Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ObjectProp
        // rel(A): TestObjClass has ObjectProp
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Kunde ObjectProp
        {
            get
            {
                Kistl.App.Projekte.Kunde __value;
                if (_fk_ObjectProp.HasValue)
                    __value = Context.Find<Kistl.App.Projekte.Kunde>(_fk_ObjectProp.Value);
                else
                    __value = null;

                if (OnObjectProp_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Projekte.Kunde>(__value);
                    OnObjectProp_Getter(this, e);
                    __value = e.Result;
                }

                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                // shortcut noops
                if (value == null && _fk_ObjectProp == null)
                    return;
                else if (value != null && value.ID == _fk_ObjectProp)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = ObjectProp;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);

                if (OnObjectProp_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Projekte.Kunde>(__oldValue, __newValue);
                    OnObjectProp_PreSetter(this, e);
                    __newValue = e.Result;
                }

                // next, set the local reference
                _fk_ObjectProp = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("ObjectProp", __oldValue, __newValue);

                if (OnObjectProp_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Projekte.Kunde>(__oldValue, __newValue);
                    OnObjectProp_PostSetter(this, e);
                }

            }
        }

        internal int? _fk_ObjectProp;
        // END Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ObjectProp
        public static event PropertyGetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Projekte.Kunde> OnObjectProp_Getter;
        public static event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Projekte.Kunde> OnObjectProp_PreSetter;
        public static event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Projekte.Kunde> OnObjectProp_PostSetter;

        /// <summary>
        /// String Property
        /// </summary>
        // value type property
        // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string StringProp
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
                    __result = __e.Result;
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
                    if (OnStringProp_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("StringProp", __oldValue, __newValue);
                    _StringProp = __newValue;
                    NotifyPropertyChanged("StringProp", __oldValue, __newValue);
                    if (OnStringProp_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _StringProp;
        public static event PropertyGetterHandler<Kistl.App.Test.TestObjClass, string> OnStringProp_Getter;
        public static event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, string> OnStringProp_PreSetter;
        public static event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, string> OnStringProp_PostSetter;

        /// <summary>
        /// Test Enumeration Property
        /// </summary>
        // enumeration property
        // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual Kistl.App.Test.TestEnum TestEnumProp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestEnumProp;
                if (OnTestEnumProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Kistl.App.Test.TestEnum>(__result);
                    OnTestEnumProp_Getter(this, __e);
                    __result = __e.Result;
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
                    if (OnTestEnumProp_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<Kistl.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestEnumProp", __oldValue, __newValue);
                    _TestEnumProp = __newValue;
                    NotifyPropertyChanged("TestEnumProp", __oldValue, __newValue);
                    if (OnTestEnumProp_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<Kistl.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumProp_PostSetter(this, __e);
                    }
                }
            }
        }
        private Kistl.App.Test.TestEnum _TestEnumProp;
        public static event PropertyGetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Test.TestEnum> OnTestEnumProp_Getter;
        public static event PropertyPreSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Test.TestEnum> OnTestEnumProp_PreSetter;
        public static event PropertyPostSetterHandler<Kistl.App.Test.TestObjClass, Kistl.App.Test.TestEnum> OnTestEnumProp_PostSetter;

        /// <summary>
        /// testmethod
        /// </summary>
        [EventBasedMethod("OnTestMethod_TestObjClass")]
        public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod)
        {
            // base.TestMethod();
            if (OnTestMethod_TestObjClass != null)
            {
                OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            }
            else
            {
                throw new NotImplementedException("No handler registered on TestObjClass.TestMethod");
            }
        }
        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
        public static event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;



        public override Type GetImplementedInterface()
        {
            return typeof(TestObjClass);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TestObjClass)obj;
            var otherImpl = (TestObjClass__Implementation__)obj;
            var me = (TestObjClass)this;

            me.MyIntProperty = other.MyIntProperty;
            me.StringProp = other.StringProp;
            me.TestEnumProp = other.TestEnumProp;
            this._fk_ObjectProp = otherImpl._fk_ObjectProp;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        // tail template
        // Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

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

        [EventBasedMethod("OnPreSave_TestObjClass")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TestObjClass != null) OnPreSave_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        [EventBasedMethod("OnPostSave_TestObjClass")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TestObjClass != null) OnPostSave_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;

        [EventBasedMethod("OnCreated_TestObjClass")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_TestObjClass != null) OnCreated_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnCreated_TestObjClass;

        [EventBasedMethod("OnDeleting_TestObjClass")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_TestObjClass != null) OnDeleting_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnDeleting_TestObjClass;


        private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] { };

        protected override void CollectProperties(List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(props);
            props.AddRange(_properties);
        }


        public override void UpdateParent(string propertyName, int? id)
        {
            int? __oldValue, __newValue = id;

            switch (propertyName)
            {
                case "ObjectProp":
                    __oldValue = _fk_ObjectProp;
                    NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);
                    _fk_ObjectProp = __newValue;
                    NotifyPropertyChanged("ObjectProp", __oldValue, __newValue);
                    break;
                default:
                    base.UpdateParent(propertyName, id);
                    break;
            }
        }

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {

            base.ToStream(binStream, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(this._MyIntProperty, binStream);
            BinarySerializer.ToStream(this._fk_ObjectProp, binStream);
            BinarySerializer.ToStream(this._StringProp, binStream);
            BinarySerializer.ToStream((int?)((TestObjClass)this).TestEnumProp, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {

            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._MyIntProperty, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectProp, binStream);
            BinarySerializer.FromStream(out this._StringProp, binStream);
            {
                int? baseValue;
                BinarySerializer.FromStream(out baseValue, binStream);
                ((TestObjClass)this).TestEnumProp = (Kistl.App.Test.TestEnum)baseValue;
            }
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {

            base.ToStream(xml);
            XmlStreamer.ToStream(this._MyIntProperty, xml, "MyIntProperty", "Kistl.App.Test");
            XmlStreamer.ToStream(this._fk_ObjectProp, xml, "ObjectProp", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._StringProp, xml, "StringProp", "Kistl.App.Test");
            XmlStreamer.ToStream((int?)this.TestEnumProp, xml, "TestEnumProp", "Kistl.App.Test");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {

            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._MyIntProperty, xml, "MyIntProperty", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._fk_ObjectProp, xml, "ObjectProp", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._StringProp, xml, "StringProp", "Kistl.App.Test");
            XmlStreamer.FromStreamConverter(v => ((TestObjClass)this).TestEnumProp = (Kistl.App.Test.TestEnum)v, xml, "TestEnumProp", "Kistl.App.Test");
        }

        #endregion



    }
}
