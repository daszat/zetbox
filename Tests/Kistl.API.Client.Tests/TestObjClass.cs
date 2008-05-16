using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kistl.API;
using Kistl.API.Client;
using System.Xml.Serialization;

namespace Kistl.API.Client.Tests
{
    public class TestObjClass : BaseClientDataObject, ICloneable
    {

        private int _ID = Helper.INVALIDID;

        private string _StringProp;

        private int _TestEnumProp;

        private List<TestObjClass> _Children;

        private int _fk_Parent = Helper.INVALIDID;

        public TestObjClass()
        {
        }

        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public string StringProp
        {
            get
            {
                return _StringProp;
            }
            set
            {
                NotifyPropertyChanging("StringProp");
                _StringProp = value;
                NotifyPropertyChanged("StringProp"); ;
            }
        }

        public int TestEnumProp
        {
            get
            {
                return _TestEnumProp;
            }
            set
            {
                NotifyPropertyChanging("TestEnumProp");
                _TestEnumProp = value;
                NotifyPropertyChanged("TestEnumProp"); ;
            }
        }

        public List<TestObjClass_TestNameCollectionEntry> TestNames
        {
            get
            {
                return null;
            }
        }

        public List<TestObjClass> Children
        {
            get
            {
                if (_Children == null) _Children = Context.GetListOf<TestObjClass>(this, "Children");
                return _Children;
            }
        }

        [XmlIgnore()]
        public TestObjClass Parent
        {
            get
            {
                return Context.Find<TestObjClass>(fk_Parent);
            }
            set
            {
                fk_Parent = value.ID;
            }
        }

        public int fk_Parent
        {
            get
            {
                return _fk_Parent;
            }
            set
            {
                if (fk_Parent != value)
                {
                    NotifyPropertyChanging("Parent");
                    _fk_Parent = value;
                    NotifyPropertyChanged("Parent");
                }
            }
        }

        public event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;

        public event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;

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

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TestObjClass != null) OnPreSave_TestObjClass(this);
        }

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TestObjClass != null) OnPostSave_TestObjClass(this);
        }

        public override object Clone()
        {
            TestObjClass obj = new TestObjClass();
            CopyTo(obj);
            return obj;
        }

        public override void CopyTo(Kistl.API.IDataObject obj)
        {
            base.CopyTo(obj);
            ((TestObjClass)obj)._StringProp = this._StringProp;
            ((TestObjClass)obj)._TestEnumProp = this._TestEnumProp;
        }

        public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod)
        {
            if (OnTestMethod_TestObjClass != null)
            {
                OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            };
        }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._StringProp, sw);
            BinarySerializer.ToBinary(this._TestEnumProp, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._StringProp, sr);
            BinarySerializer.FromBinary(out this._TestEnumProp, sr);
        }

        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
    }

}
