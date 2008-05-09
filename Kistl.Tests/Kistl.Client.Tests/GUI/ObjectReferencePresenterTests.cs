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

namespace Kistl.GUI.Tests
{

    [TestFixture]
    public class ObjectReferencePresenterTests : ObjectReferencePresenterInfrastructure<TestObjectReferenceControl>
    {
        protected override Toolkit Toolkit { get { return Toolkit.TEST; } }
        protected override void UserInput(IDataObject v) { widget.SimulateUserInput(v); }
    }

    public abstract class ObjectReferencePresenterInfrastructure<CONTROL> : ReferencePresenterTests<IDataObject, CONTROL, ObjectReferencePresenter>
        where CONTROL : IObjectReferenceControl
    {
        private List<IDataObject> Items = new List<IDataObject>(new[] {
                    new TestObject() { ID = 2 },
                    new TestObject() { ID = 3 },
                    new TestObject() { ID = 4 },
                });

        protected abstract Toolkit Toolkit { get;  }

        protected override IDataObject GetObjectValue() { return obj.TestObjectReference; }
        protected override IDataObject GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(IDataObject v) { obj.TestObjectReference = v; }
        protected override IDataObject DefaultValue() { return null; }
        protected override IEnumerable<IDataObject> SomeValues() { return Items; }


        protected override void CustomSetUp()
        {
            IQueryable<IDataObject> idoq = Mockery.NewMock<IQueryable<IDataObject>>("IDataObjectQueryable");

            Expect.Once.On(MockContext).
                Method("GetQuery").
                With(new ObjectType(typeof(TestObject))).
                Will(Return.Value(idoq));

            Expect.Once.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(Items.GetEnumerator()));

            Visual orv = new Visual() { Name = VisualType.ObjectReference, Property = TestObject.TestObjectReferenceDescriptor };
            Init(
                KistlGUIContext.FindControlInfo(Toolkit.WPF, orv),
                TestObject.TestObjectReferenceDescriptor, 
                Toolkit);
        }

        protected override void AssertWidgetHasValidValue()
        {
            Assert.IsTrue(widget.IsValidValue, "the widget should be in a valid state after this operation");
            Assert.AreEqual(Items.Count, widget.ItemsSource.Count, "the widget's ItemSource should contain exactly one entry for each item");

            foreach (IDataObject ido in Items)
            {
                Assert.That(widget.ItemsSource.Contains(ido), string.Format("cannot find entry '{0}' in ItemsSource", ido));
            }
        }

        private void ExpectFind(ObjectType type, int ID, object result)
        {
            Expect.Once.On(MockContext).
                Method("Find").
                With(type, ID).
                Will(Return.Value(result));
        }

    }
}
