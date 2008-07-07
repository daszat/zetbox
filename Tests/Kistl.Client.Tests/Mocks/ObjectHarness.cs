using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

using Kistl.API;

namespace Kistl.Client.Mocks
{
    /// <summary>
    /// Create a nominally usable IDataObject Mock
    /// </summary>
    /// <typeparam name="OBJECT"></typeparam>
    public class ObjectHarness<OBJECT>
        where OBJECT : IDataObject
    {

        public Mockery Mockery { get; private set; }
        public IKistlContext MockContext { get; private set; }
        public OBJECT Instance { get; private set; }

        public virtual void SetUp()
        {
            Mockery = new Mockery();
            MockContext = Mockery.NewMock<IKistlContext>("MockContext");
            TestObject.GlobalContext = MockContext;
            Instance = CreateObject();
        }

        public virtual void TestSetUpCorrect()
        {
            Assert.IsNotNull(Mockery, "Mockery should have been initialised");
            Assert.IsNotNull(MockContext, "MockContext should have been initialised");
            Assert.IsNotNull(Instance, "obj should have been initialised");
        }

        protected virtual OBJECT CreateObject()
        {
            return Mockery.NewMock<OBJECT>();
        }
    }

    /// <summary>
    /// extend ObjectHarness to use the TestObject instead of a primitive Mock
    /// </summary>
    public class TestObjectHarness : ObjectHarness<TestObject>
    {
        public TestObjectHarness()
            : base()
        {
        }

        private void RegisterObject(IDataObject obj)
        {
            Stub.On(MockContext).
                Method("Find").
                With(obj.ID).
                Will(Return.Value(obj));
            obj.AttachToContext(TestObject.GlobalContext);
        }
        public override void SetUp()
        {
            base.SetUp();

            RegisterObject(TestObject.ObjectClass);

            RegisterObject(TestObject.ObjectReferencePropertyClass);

            RegisterObject(TestObject.Module);

            RegisterObject(TestObject.KistlAppBaseModule);

            RegisterObject(TestObject.TestObjectReferenceDescriptor);

            RegisterObject(TestObject.TestObjectListDescriptor);

        }

        private int _TestObjectID = 100;

        protected override TestObject CreateObject()
        {
            return new TestObject() { ID = _TestObjectID++ };
        }
    }
}

