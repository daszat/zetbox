
namespace Kistl.App.Test
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestObjClass")]
    public class TestObjClass__Implementation__ : BaseFrozenDataObject, TestObjClass
    {


        /// <summary>
        /// String Property
        /// </summary>
        // value type property
        public virtual string StringProp
        {
            get
            {
                return _StringProp;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_StringProp != value)
                {
                    NotifyPropertyChanging("StringProp");
                    _StringProp = value;
                    NotifyPropertyChanged("StringProp");;
                }
            }
        }
        private string _StringProp;

        /// <summary>
        /// Test Enumeration Property
        /// </summary>
        // enumeration property
        public virtual Kistl.App.Test.TestEnum TestEnumProp
        {
            get
            {
                return _TestEnumProp;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_TestEnumProp != value)
                {
                    NotifyPropertyChanging("TestEnumProp");
                    _TestEnumProp = value;
                    NotifyPropertyChanged("TestEnumProp");;
                }
            }
        }
        private Kistl.App.Test.TestEnum _TestEnumProp;

        /// <summary>
        /// testtest
        /// </summary>
        // object reference property
        public virtual Kistl.App.Projekte.Kunde ObjectProp
        {
            get
            {
                return _ObjectProp;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ObjectProp != value)
                {
                    NotifyPropertyChanging("ObjectProp");
                    _ObjectProp = value;
                    NotifyPropertyChanged("ObjectProp");;
                }
            }
        }
        private Kistl.App.Projekte.Kunde _ObjectProp;

        /// <summary>
        /// test
        /// </summary>
        // value type property
        public virtual int? MyIntProperty
        {
            get
            {
                return _MyIntProperty;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MyIntProperty != value)
                {
                    NotifyPropertyChanging("MyIntProperty");
                    _MyIntProperty = value;
                    NotifyPropertyChanged("MyIntProperty");;
                }
            }
        }
        private int? _MyIntProperty;

        /// <summary>
        /// testmethod
        /// </summary>

		public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod) 
		{
            // base.TestMethod();
            if (OnTestMethod_TestObjClass != null)
            {
				OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
			}
        }
		public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
		public event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
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
        public event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TestObjClass != null) OnPreSave_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TestObjClass != null) OnPostSave_TestObjClass(this);
        }
        public event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;


        internal TestObjClass__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}