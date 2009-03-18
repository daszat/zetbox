using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Mocks;
using Kistl.API.Server.Mocks;

namespace Kistl.API.Server.Tests
{

    public class TestObjClass__Implementation__ : BaseServerDataObject, TestObjClass
    {

        public TestObjClass__Implementation__()
        {
            SubClasses = new List<TestObjClass>();
            TestNames__Implementation__ = new List<TestObjClass_TestNameCollectionEntry__Implementation__>();
            TestNames = new TestNameCollectionWrapper(this, TestNames__Implementation__);
        }

        public override int ID { get; set; }

        public TestObjClass BaseTestObjClass
        {
            get
            {
                return _BaseTestObjClass;
            }
            set
            {
                NotifyPropertyChanging("BaseTestObjClass");
                _BaseTestObjClass = value;
                NotifyPropertyChanged("BaseTestObjClass"); ;
            }
        }
        private TestObjClass _BaseTestObjClass;

        public ICollection<TestObjClass> SubClasses { get; internal set; }

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
        private string _StringProp;

        public TestEnum TestEnumProp
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
        private TestEnum _TestEnumProp;

        public ICollection<string> TestNames { get; private set; }
        internal List<TestObjClass_TestNameCollectionEntry__Implementation__> TestNames__Implementation__ { get; private set; }

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
            int? id = this.BaseTestObjClass == null ? (int?)null : this.BaseTestObjClass.ID;
            BinarySerializer.ToStream(id, sw);

            BinarySerializer.ToStream(this._StringProp, sw);
            BinarySerializer.ToStream((int)this.TestEnumProp, sw);
            BinarySerializer.ToStreamCollectionEntries(this.TestNames__Implementation__, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            int? id;
            BinarySerializer.FromStream(out id, sr);
            if (id.HasValue)
            {
                this.BaseTestObjClass = KistlContextMock.TestObjClasses[id.Value];
            }
            else
            {
                this.BaseTestObjClass = null;
            }

            BinarySerializer.FromStream(out this._StringProp, sr);
            BinarySerializer.FromStreamConverter(value => this.TestEnumProp = (TestEnum)value, sr);
            BinarySerializer.FromStreamCollectionEntries(this.TestNames__Implementation__, sr);
        }

        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);

        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(TestObjClass));
        }

        public override bool IsAttached { get { return _IsAttached; } }
        private bool _IsAttached = false;

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            _IsAttached = true;
        }

        public override void DetachFromContext(IKistlContext ctx)
        {
            base.DetachFromContext(ctx);
            _IsAttached = false;
        }
    }

}
