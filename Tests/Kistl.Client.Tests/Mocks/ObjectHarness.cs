using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

using Kistl.API;
using Kistl.App.Base;
using Kistl.GUI.Tests;

namespace Kistl.Client.Mocks
{
    /// <summary>
    /// Create a nominally usable IDataObject Mock
    /// </summary>
    /// <typeparam name="OBJECT"></typeparam>
    public class ObjectHarness<OBJECT>
        where OBJECT : IDataObject
    {

        public OBJECT Instance { get; private set; }

        public virtual void SetUp()
        {
            Instance = CreateObject();
        }

        public virtual void TestSetUpCorrect()
        {
            Assert.IsNotNull(MainSetUp.Mockery, "Mockery should have been initialised");
            Assert.IsNotNull(MainSetUp.MockContext, "MockContext should have been initialised");
            Assert.IsNotNull(Instance, "obj should have been initialised");
        }

        protected virtual OBJECT CreateObject()
        {
            return MainSetUp.Mockery.NewMock<OBJECT>();
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

        public override void SetUp()
        {
            base.SetUp();

            MainSetUp.RegisterObject(TestObject.ObjectClass);
            MainSetUp.RegisterObject(TestObject.ObjectReferencePropertyClass);
            MainSetUp.RegisterObject(TestObject.Module);
            MainSetUp.RegisterObject(TestObject.KistlAppBaseModule);
            MainSetUp.RegisterObject(TestObject.TestObjectReferenceDescriptor);
            MainSetUp.RegisterObject(TestObject.TestObjectListDescriptor);

            IQueryable<TestObject> queryMock = MainSetUp.Mockery.NewMock<IQueryable<TestObject>>();

            Stub.On(MainSetUp.MockContext).
                Method("GetQuery").
                Will(Return.Value(queryMock));

            Stub.On(queryMock).  
                Method("GetEnumerator").
                Will(Return.Value(TestObjectValues.TestValues.Valids.ToList<TestObject>().GetEnumerator()));
            
        }

        private int _TestObjectID = 100;

        protected override TestObject CreateObject()
        {
            return new TestObject() { ID = _TestObjectID++ };
        }
    }
}

