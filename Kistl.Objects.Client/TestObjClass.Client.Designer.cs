
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

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TestObjClass")]
    public class TestObjClass__Implementation__ : BaseClientDataObject, TestObjClass
    {


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
        /// testtest
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Kunde ObjectProp
        {
            get
            {
                if (fk_ObjectProp.HasValue)
                    return Context.Find<Kistl.App.Projekte.Kunde>(fk_ObjectProp.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ObjectProp
        {
            get
            {
                return _fk_ObjectProp;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ObjectProp != value)
                {
                    NotifyPropertyChanging("ObjectProp");
                    _fk_ObjectProp = value;
                    NotifyPropertyChanging("ObjectProp");
                }
            }
        }
        private int? _fk_ObjectProp;

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



		public override Type GetInterfaceType()
		{
			return typeof(TestObjClass);
		}

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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._MyIntProperty, binStream);
            BinarySerializer.ToStream(this._fk_ObjectProp, binStream);
            BinarySerializer.ToStream(this._StringProp, binStream);
            BinarySerializer.ToStream((int)((TestObjClass)this).TestEnumProp, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._MyIntProperty, binStream);
            BinarySerializer.FromStream(out this._fk_ObjectProp, binStream);
            BinarySerializer.FromStream(out this._StringProp, binStream);
            BinarySerializer.FromStreamConverter(v => ((TestObjClass)this).TestEnumProp = (Kistl.App.Test.TestEnum)v, binStream);
        }

#endregion

    }


}