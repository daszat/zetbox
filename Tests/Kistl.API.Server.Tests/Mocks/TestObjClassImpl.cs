using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Kistl.API.Mocks;

namespace Kistl.API.Server.Mocks
{

    public class TestObjClassImpl : BaseServerDataObject, TestObjClass
    {

        public TestObjClassImpl()
            : base(null)
        {
            SubClasses = new List<TestObjClass>();
            TestNamesImpl = new List<TestObjClass_TestNameCollectionEntryImpl>();
            _TestNames = null;
        }

        public override int ID { get; set; }

        public int fk_BaseTestObjClass = -1;
        public TestObjClass BaseTestObjClass
        {
            get
            {
                if (fk_BaseTestObjClass != -1 && _BaseTestObjClass == null)
                {
                    _BaseTestObjClass = Context.Find<TestObjClass>(fk_BaseTestObjClass);
                }
                return _BaseTestObjClass;
            }
            set
            {
                NotifyPropertyChanging("BaseTestObjClass", null, null);
                _BaseTestObjClass = value;
                fk_BaseTestObjClass = value != null ? value.ID : -1;
                NotifyPropertyChanged("BaseTestObjClass", null, null); ;
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
                NotifyPropertyChanging("StringProp", null, null);
                _StringProp = value;
                NotifyPropertyChanged("StringProp", null, null); ;
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
                NotifyPropertyChanging("TestEnumProp", null, null);
                _TestEnumProp = value;
                NotifyPropertyChanged("TestEnumProp", null, null); ;
            }
        }
        private TestEnum _TestEnumProp;

        private TestNameCollectionWrapper _TestNames;
        public ICollection<string> TestNames
        {
            get
            {
                if (_TestNames == null)
                {
                    _TestNames = new TestNameCollectionWrapper(this.Context, this, TestNamesImpl);
                }
                return _TestNames;
            }
        }
        internal List<TestObjClass_TestNameCollectionEntryImpl> TestNamesImpl { get; private set; }

        public event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnCreated_TestObjClass;

        public event ObjectEventHandler<TestObjClass> OnDeleting_TestObjClass;

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

        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_TestObjClass != null) OnCreated_TestObjClass(this);
        }

        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_TestObjClass != null) OnDeleting_TestObjClass(this);
        }

        public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod)
        {
            if (OnTestMethod_TestObjClass != null)
            {
                OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            };
        }

        public override void ToStream(System.IO.BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            int? id = this.BaseTestObjClass == null ? (int?)null : this.BaseTestObjClass.ID;
            BinarySerializer.ToStream(id, sw);

            BinarySerializer.ToStream(this._StringProp, sw);
            BinarySerializer.ToStream((int)this.TestEnumProp, sw);
            BinarySerializer.ToStreamCollectionEntries(this.TestNamesImpl, sw);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader sr)
        {
            var baseResult = base.FromStream(sr);
            var result = new List<IPersistenceObject>();

            int? id;
            BinarySerializer.FromStream(out id, sr);
            if (id.HasValue)
            {
                this.fk_BaseTestObjClass = id.Value;
            }
            else
            {
                this.fk_BaseTestObjClass = -1;
            }

            BinarySerializer.FromStream(out this._StringProp, sr);
            BinarySerializer.FromStreamConverter(value => this._TestEnumProp = (TestEnum)value, sr);
            BinarySerializer.FromStreamCollectionEntries(this.TestNamesImpl, sr);

            result.AddRange(this.TestNamesImpl.Cast<IPersistenceObject>());

            return baseResult == null
                ? result
                : result.Concat(baseResult);
        }

        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);

        public override Type GetImplementedInterface()
        {
            return typeof(TestObjClass);
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

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        protected override string GetPropertyError(string prop)
        {
            throw new NotImplementedException();
        }

        public override void UpdateParent(string propertyName, int? id)
        {
            throw new NotImplementedException();
        }
    }
}
