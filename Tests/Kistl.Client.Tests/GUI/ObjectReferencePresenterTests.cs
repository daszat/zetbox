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
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

namespace Kistl.GUI.Tests
{

    [TestFixture]
    public class ObjectReferencePresenterTests : ObjectReferencePresenterInfrastructure<TestObjectReferenceControl>
    {
        public ObjectReferencePresenterTests()
            : base(Toolkit.TEST)
        {
        }

        protected override void UserInput(IDataObject v) { Widget.SimulateUserInput(v); }
    }

    public abstract class ObjectReferencePresenterInfrastructure<CONTROL> : ReferencePresenterTests<TestObject, IDataObject, CONTROL, ObjectReferencePresenter>
        where CONTROL : IObjectReferenceControl
    {
        public ObjectReferencePresenterInfrastructure(Toolkit toolkit)
            : base(
                new PresenterHarness<TestObject, CONTROL, ObjectReferencePresenter>(
                    new TestObjectHarness(),
                    new ControlHarness<CONTROL>(TestObject.TestObjectReferenceVisual, toolkit)))
        {
            Toolkit = toolkit;
        }


        private List<IDataObject> Items = new List<IDataObject>(new[] {
                    new TestObject() { ID = 2 },
                    new TestObject() { ID = 3 },
                    new TestObject() { ID = 4 },
                });

        protected Toolkit Toolkit { get; private set; }

        protected override IDataObject GetObjectValue() { return Object.TestObjectReference; }
        protected override IDataObject GetWidgetValue() { return Widget.Value; }
        protected override void SetObjectValue(IDataObject v) { Object.TestObjectReference = v; }
        protected override IDataObject DefaultValue() { return null; }
        protected override IEnumerable<IDataObject> SomeValues() { return Items; }


        [SetUp]
        public new void SetUp()
        {
            ObjectHarness.SetUp();
            IQueryable<IDataObject> idoq = Mockery.NewMock<IQueryable<IDataObject>>("IDataObjectQueryable");

            Expect.Once.On(MockContext).
                Method("GetQuery").
                With(new ObjectType(typeof(TestObject))).
                Will(Return.Value(idoq));

            Expect.Once.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(Items.GetEnumerator()));

            ControlHarness.SetUp();
            PresenterHarness.SetUp();
        }

        protected override void AssertWidgetHasValidValue()
        {
            Assert.IsTrue(Widget.IsValidValue, "the widget should be in a valid state after this operation");
            Assert.AreEqual(Items.Count, Widget.ItemsSource.Count, "the widget's ItemSource should contain exactly one entry for each item");

            foreach (IDataObject ido in Items)
            {
                Assert.That(Widget.ItemsSource.Contains(ido), string.Format("cannot find entry '{0}' in ItemsSource", ido));
            }
        }

    }
}
