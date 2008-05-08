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

namespace Kistl.GUI.Tests
{
    [TestFixture]
    public class ObjectReferencePresenterTests : PresenterTest<TestObjectReferenceControl, PointerPresenter>
    {
        private List<IDataObject> Items = new List<IDataObject>(new[] {
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
                Will(Return.Value(Items.GetEnumerator()));

            Init(TestObjectReferenceControl.Info, TestObject.TestObjectReferenceDescriptor);
        }

        private void AssertWidgetValidity(bool widgetValid)
        {
            Assert.AreEqual(widgetValid, widget.IsValidValue, String.Format("the widget should be in a {0}valid state after this operation", widgetValid ? "" : "in"));
            Assert.AreEqual(Items.Count, widget.ItemsSource.Count, "the widget's ItemSource should contain exactly one entry for each item");

            foreach (string ido in Items.Select(i => i.ToString()))
            {
                Assert.That(widget.ItemsSource.Contains(ido), string.Format("cannot find entry '{0}' in ItemsSource", ido));
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
            Assert.AreEqual(Helper.INVALIDID, widget.Value, "widget should be initialised to correct value");

            FinalAssert(true);
        }

        [Test]
        public void HandleValidUserInput()
        {
            int expectedID = 3;
            IDataObject expectedObject = (IDataObject)Items.Single(i => i.ID == expectedID);
            int expectedIndex = Items.IndexOf(expectedObject);

            // users select index of item
            widget.SimulateUserInput(expectedIndex);

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReference should have been updated");
            Assert.AreEqual(expectedIndex, widget.Value, "widget should display correct value");

            FinalAssert(true);
        }

        [Test]
        public void HandleProgrammaticChange()
        {
            int expectedID = 3;
            // Clone object to exercise all parts of the Presenter
            IDataObject expectedObject = (IDataObject)Items.Single(i => i.ID == expectedID);
            int expectedIndex = Items.IndexOf(expectedObject);

            obj.TestObjectReference = expectedObject;

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReference should have been updated");
            Assert.AreEqual(expectedIndex, widget.Value, "widget should display correct value");

            FinalAssert(true);
        }

        [Test]
        public void HandleInvalidUserInput()
        {
            int expectedID = 3;
            IDataObject expectedObject = (IDataObject)Items.Single(i => i.ID == expectedID);
            int expectedIndex = Items.IndexOf(expectedObject);

            int unexpectedID = 66;
            IDataObject unexpectedObject = new TestObject() { ID = unexpectedID };
            // highest valid index is Count - 1
            int unexpectedIndex = Items.Count; 

            widget.SimulateUserInput(expectedIndex);

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReference should have been updated");
            Assert.AreEqual(expectedIndex, widget.Value, "widget should display correct value");

            AssertWidgetValidity(true);

            widget.SimulateUserInput(unexpectedIndex);

            Assert.AreEqual(expectedObject, obj.TestObjectReference, "ObjectReferenceProperty should not have changed");
            Assert.AreEqual(unexpectedIndex, widget.Value, "widget should display user's value");

            FinalAssert(false);
        }
    }
}
