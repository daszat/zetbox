using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class ObjectReferencePresenterTests : PresenterTest<TestObjectReferenceControl, PointerPresenter>
    {
        private List<IDataObject> ItemsSource = new List<IDataObject>(new[] {
                    new TestObject() { ID = 2 },
                    new TestObject() { ID = 3 },
                    new TestObject() { ID = 4 },
                });

        protected override void CustomSetUp()
        {
            IQueryable<IDataObject> idoq = mocks.NewMock<IQueryable<IDataObject>>("IDataObjectQueryable");

            Expect.Once.On(MockContext).
                Method("GetQuery").
                With(new ObjectType(typeof(TestObject))).
                Will(Return.Value(idoq));

            Expect.Once.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(ItemsSource.GetEnumerator()));

            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceDescriptor);
        }

        private void AssertWidgetValidity(bool widgetValid)
        {
            Assert.AreEqual(widgetValid, widget.IsValidValue, String.Format("the widget should be in a {0}valid state after this operation", widgetValid ? "" : "in"));
            System.Collections.ArrayList items = new System.Collections.ArrayList();
            widget.ItemsSource.ForEach<object>(i => items.Add(i));
            foreach (IDataObject ido in ItemsSource)
            {
                Assert.Contains(ido, items, "Missing IDataObject in ItemsSource");
            }
        }

        private void FinalAssert(bool widgetValid)
        {
            AssertWidgetValidity(widgetValid);
            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        private void ExpectFind(ObjectType type, int ID, object result)
        {
            Expect.Once.On(MockContext).
                Method("Find").
                With(type, ID).
                Will(Return.Value(result));
        }


        [Test]
        public void HandleNoUserInput()
        {
            Assert.IsNull(obj.TestObjectReference, "ObjectReferenceProperty should default to null");
            Assert.IsNull(widget.Value, "widget should be initialised to correct value");

            FinalAssert(true);
        }

        [Test]
        public void HandleValidUserInput()
        {
            int expectedID = 3;
            IDataObject expectedObject = ItemsSource.Single(i => i.ID == expectedID);

            ExpectFind(new ObjectType(typeof(TestObject)), expectedID, ItemsSource.Single(i => i.ID == expectedID));

            widget.SimulateUserInput(expectedObject);

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReference should have been updated");
            Assert.AreEqual(expectedObject, widget.Value, "widget should display correct value");

            FinalAssert(true);
        }

        [Test]
        public void HandleProgrammaticChange()
        {
            int expectedID = 3;
            IDataObject expectedObject = ItemsSource.Single(i => i.ID == expectedID);

            obj.TestObjectReference = expectedObject;

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReference should have been updated");
            Assert.AreEqual(expectedObject, widget.Value, "widget should display correct value");

            FinalAssert(true);
        }

        [Test]
        public void HandleInvalidUserInput()
        {
            int expectedID = 3;
            IDataObject expectedObject = ItemsSource.Single(i => i.ID == expectedID);
            int unexpectedID = 66;
            IDataObject unexpectedObject = new TestObject() { ID = unexpectedID };

            ExpectFind(new ObjectType(typeof(TestObject)), expectedID, expectedObject);
            // TODO: This Exepectation has to change as soon as better validation mechanisms are in place
            ExpectFind(new ObjectType(typeof(TestObject)), unexpectedID, null);
            // ExpectFind(new ObjectType(typeof(TestObject)), unexpectedID, unexpectedObject);


            widget.SimulateUserInput(expectedObject);

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReference should have been updated");
            Assert.AreEqual(expectedObject, widget.Value, "widget should display correct value");

            AssertWidgetValidity(true);

            widget.SimulateUserInput(unexpectedObject);

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReferenceProperty should not have changed");
            Assert.AreEqual(unexpectedObject, widget.Value, "widget should display user's value");

            FinalAssert(false);
        }
    }
}
