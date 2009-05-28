using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kistl.API.Client.Tests
{
    public interface TestObjClass : IDataObject
    {
        System.Collections.Generic.IList<TestObjClass> Children { get; }
        int? fk_Parent { get; set; }
        TestObjClass Parent { get; set; }
        string StringProp { get; set; }
        int TestEnumProp { get; set; }
    }

    public class TestObjClass__Implementation__ : BaseClientDataObject, TestObjClass
    {

        private string _StringProp;

        private int _TestEnumProp;

        private OneNRelationCollection<TestObjClass> _Children;

        private int? _fk_Parent;

        public TestObjClass__Implementation__()
        {
        }



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

        public int TestEnumProp
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

        public IList<TestObjClass> Children
        {
            get
            {
                if (_Children == null) _Children = new OneNRelationCollection<TestObjClass>("Parent", this, Context.GetListOf<TestObjClass>(this, "Children"));
                return _Children;
            }
        }

        [XmlIgnore()]
        public TestObjClass Parent
        {
            get
            {
                if (fk_Parent == null) return null;
                return Context.Find<TestObjClass>(fk_Parent.Value);
            }
            set
            {
                fk_Parent = value.ID;
            }
        }

        public int? fk_Parent
        {
            get
            {
                return _fk_Parent;
            }
            set
            {
                if (fk_Parent != value)
                {
                    NotifyPropertyChanging("Parent", null, null);
                    _fk_Parent = value;
                    NotifyPropertyChanged("Parent", null, null);
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

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            if (_Children != null) _Children = new OneNRelationCollection<TestObjClass>("Parent", this, _Children.Select(i => ctx.Attach(i)).OfType<TestObjClass>());
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (!(obj is TestObjClass))
                throw new ArgumentException("obj");

            base.ApplyChangesFrom(obj);
            var other = (TestObjClass)obj;
            this._StringProp = other.StringProp;
            this._TestEnumProp = other.TestEnumProp;
        }

        public virtual void TestMethod(DateTime DateTimeParamForTestMethod)
        {
            if (OnTestMethod_TestObjClass != null)
            {
                OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            };
        }

        public override void ToStream(BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            base.ToStream(sw, auxObjects);
            BinarySerializer.ToStream(this._StringProp, sw);
            BinarySerializer.ToStream(this._TestEnumProp, sw);
        }

        public override void FromStream(BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromStream(out this._StringProp, sr);
            BinarySerializer.FromStream(out this._TestEnumProp, sr);
        }

        public delegate void TestMethod_Handler<T>(T obj, DateTime DateTimeParamForTestMethod);

        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(TestObjClass));
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        protected override string GetPropertyError(string prop)
        {
            throw new NotImplementedException();
        }
    }

}
