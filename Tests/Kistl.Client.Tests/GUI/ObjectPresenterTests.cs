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
using Kistl.GUI.Renderer.WPF.Tests;

namespace Kistl.GUI.Tests
{
    public sealed class IDataObjectValues : ValuesAdapter<IDataObject>
    {
        private IDataObjectValues() { }
        private static IDataObjectValues _Values = new IDataObjectValues();
        public static IDataObjectValues TestValues { get { return _Values; } }

        public override IDataObject[] Valids { get { return (IDataObject[])_Items.Clone(); } }
        public override IDataObject[] Invalids { get { return new IDataObject[] { }; } }

        private IDataObject[] _Items = new[] {
            new TestObject() { ID = 2 },
            null,
            new TestObject() { ID = 3 },
            new TestObject() { ID = 4 },
            new TestObject() { ID = 7 },
            new TestObject() { ID = Int16.MaxValue+1 },
        };
    }

    public abstract class ObjectReferencePresenterInfrastructure<TYPE, CONTROL, PRESENTER>
        : ReferencePresenterTests<TestObject, TYPE, CONTROL, PRESENTER>
        where TYPE : class
        where CONTROL : IReferenceControl<TYPE>
        where PRESENTER : IPresenter
    {
        public ObjectReferencePresenterInfrastructure(
            PresenterHarness<TestObject, CONTROL, PRESENTER> presenterHarness,
            Toolkit toolkit,
            IValues<TYPE> values)
            : base(presenterHarness, values)
        {
            Toolkit = toolkit;
        }


        protected Toolkit Toolkit { get; private set; }

        protected override TYPE DefaultValue() { return null; }

        // replace PresenterTests.SetUp to inject the MockContext
        [SetUp]
        public new void SetUp()
        {
            ObjectHarness.SetUp();
            var idoq = Mockery.NewMock<IQueryable<IDataObject>>("IDataObjectQueryable");

            Expect.Once.On(MockContext).
                Method("Find").
                With(-1).
                Will(Return.Value(TestObject.TestObjectReferenceDescriptor));

            Expect.Once.On(MockContext).
                Method("GetQuery").
                With(new ObjectType(typeof(TestObject))).
                Will(Return.Value(idoq));

            Expect.Once.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(new List<IDataObject>(IDataObjectValues.TestValues.Valids).GetEnumerator()));

            ControlHarness.SetUp();
            PresenterHarness.SetUp();
        }

        protected override void AssertWidgetHasValidValue()
        {
            Assert.IsTrue(Widget.IsValidValue, "the widget should be in a valid state after this operation");

            // don't use Values.Valids here, as ObjectListPresenter also uses the TestValues
            // as ItemSource. 
            // TODO: recode this after arrival of Validation stuff
            Assert.AreEqual(IDataObjectValues.TestValues.Valids.Length, Widget.ItemsSource.Count, "the widget's ItemSource should contain exactly one entry for each item");

            foreach (IDataObject ido in IDataObjectValues.TestValues.Valids)
            {
                Assert.That(Widget.ItemsSource.Contains(ido), string.Format("cannot find entry '{0}' in ItemsSource", ido));
            }
        }

    }

    [TestFixture]
    public class ObjectReferencePresenterTests
        : ObjectReferencePresenterInfrastructure<IDataObject, TestObjectReferenceControl, ObjectReferencePresenter>
    {
        public ObjectReferencePresenterTests()
            : base(
                new PresenterHarness<TestObject, TestObjectReferenceControl, ObjectReferencePresenter>(
                    new TestObjectHarness(),
                    typeof(ObjectReferenceProperty),
                    new ControlHarness<TestObjectReferenceControl>(
                        TestObject.TestObjectReferenceVisual,
                        Toolkit.TEST)),
                Toolkit.TEST,
                IDataObjectValues.TestValues
            )
        {
        }

        protected override IDataObject GetObjectValue() { return Object.TestObjectReference; }
        protected override void SetObjectValue(IDataObject v) { Object.TestObjectReference = v; }

        protected override void UserInput(IDataObject v) { Widget.SimulateUserInput(v); }

    }

    [TestFixture]
    public class ObjectListPresenterTests
        : ObjectReferencePresenterInfrastructure<IList<IDataObject>, TestObjectListControl, ObjectListPresenter>
    {
        public ObjectListPresenterTests()
            : base(
                new PresenterHarness<TestObject, TestObjectListControl, ObjectListPresenter>(
                    new TestObjectHarness(),
                    typeof(ObjectReferenceProperty),
                    new ControlHarness<TestObjectListControl>(
                        TestObject.TestObjectListVisual,
                        Toolkit.TEST)),
                Toolkit.TEST,
                new ListValues<IDataObject>(IDataObjectValues.TestValues, true, true)
            )
        {
        }

        protected override IList<IDataObject> GetObjectValue() { return Object.TestObjectList; }
        protected override void SetObjectValue(IList<IDataObject> v) { Object.TestObjectList = v; }
        protected override IList<IDataObject> DefaultValue()
        {
            return new List<IDataObject>();
        }

        protected override void UserInput(IList<IDataObject> v) { Widget.SimulateUserInput(v); }

    }

    [TestFixture]
    public class BackReferencePresenterTests
        : ObjectReferencePresenterInfrastructure<IList<IDataObject>, TestObjectListControl, BackReferencePresenter>
    {
        public BackReferencePresenterTests()
            : base(
                new PresenterHarness<TestObject, TestObjectListControl, BackReferencePresenter>(
                    new TestObjectHarness(),
                    typeof(BackReferenceProperty),
                    new ControlHarness<TestObjectListControl>(
                        TestObject.TestBackReferenceVisual,
                        Toolkit.TEST)),
                Toolkit.TEST,
                new ListValues<IDataObject>(IDataObjectValues.TestValues, true, true)
            )
        {
        }

        // replace PresenterTests.SetUp to inject the MockContext
        [SetUp]
        public new void SetUp()
        {
            ObjectHarness.SetUp();
            var idoq = Mockery.NewMock<IQueryable<IDataObject>>("IDataObjectQueryable");

            Stub.On(MockContext).
                Method("Find").
                With(-1).
                Will(Return.Value(TestObject.TestObjectReferenceDescriptor));

            Expect.Once.On(MockContext).
                Method("GetQuery").
                With(new ObjectType(typeof(TestObject))).
                Will(Return.Value(idoq));

            Expect.Once.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(new List<IDataObject>(IDataObjectValues.TestValues.Valids).GetEnumerator()));

            ControlHarness.SetUp();
            PresenterHarness.SetUp();
        }

        protected override IList<IDataObject> GetObjectValue() { return Object.TestBackReference; }
        protected override void SetObjectValue(IList<IDataObject> v) { Object.TestBackReference = v; }
        protected override IList<IDataObject> DefaultValue()
        {
            return new List<IDataObject>();
        }

        protected override void UserInput(IList<IDataObject> v) { Widget.SimulateUserInput(v); }

    }

}
