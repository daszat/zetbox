using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;
using Kistl.API;
using Kistl.Client.Mocks;
using Kistl.GUI;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class ObjectReferencePresenterTests : PresenterTest<TestObjectReferenceControl, PointerPresenter>
    {
        protected void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, "the widget should be in a valid state after this operation");
        }

        [Test]
        public void HandleNoUserInput()
        {
            IQueryable<IDataObject> idoq = mocks.NewMock<IQueryable<IDataObject>>("IDataObjectQueryable");

            Expect.Once.On(MockContext).
                Method("GetQuery").
                Will(Return.Value(idoq));

            Expect.Once.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(new List<IDataObject>(new [] { null, new TestObject(MockContext) }).GetEnumerator() ));

            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceProperty);

            Assert.AreEqual(obj.TestObjectReference, Helper.INVALIDID, "ObjectReferenceProperty should default to INVALIDID");
            AssertWidgetHasValidValue();

            mocks.VerifyAllExpectationsHaveBeenMet();
        }
    }
}
