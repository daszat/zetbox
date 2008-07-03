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
using System.Collections.ObjectModel;

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

    public sealed class TestObjectValues : ValuesAdapter<TestObject>
    {
        private TestObjectValues() { }
        private static TestObjectValues _Values = new TestObjectValues();
        public static TestObjectValues TestValues { get { return _Values; } }

        public override TestObject[] Valids { get { return (TestObject[])_Items.Clone(); } }
        public override TestObject[] Invalids { get { return new TestObject[] { }; } }

        private TestObject[] _Items = new[] {
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
        where TYPE : class, IDataObject
        where CONTROL : IReferenceControl
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
                With(typeof(TestObject)).
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
        : ObjectReferencePresenterInfrastructure<IDataObject, TestObjectReferenceControl, ObjectReferencePresenter<TestObject>>
    {
        public ObjectReferencePresenterTests()
            : base(
                new PresenterHarness<TestObject, TestObjectReferenceControl, ObjectReferencePresenter<TestObject>>(
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

    public abstract class ObjectReferenceListPresenterInfrastructure<TYPE, CONTROL, PRESENTER>
        : ReferenceListPresenterTests<TestObject, TYPE, CONTROL, PRESENTER>
        where TYPE : class, IDataObject
        where CONTROL : IReferenceListControl
        where PRESENTER : IPresenter
    {
        public ObjectReferenceListPresenterInfrastructure(
            PresenterHarness<TestObject, CONTROL, PRESENTER> presenterHarness,
            Toolkit toolkit,
            IValues<ObservableCollection<TYPE>> values)
            : base(presenterHarness, values)
        {
            Toolkit = toolkit;
        }

        protected Toolkit Toolkit { get; private set; }

        protected override IList<TYPE> DefaultValue() { return new List<TYPE>(0); }

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
                With(typeof(TestObject)).
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
    public class ObjectListPresenterTests
        : ObjectReferenceListPresenterInfrastructure<TestObject, TestObjectListControl, ObjectListPresenter<TestObject>>
    {
        public ObjectListPresenterTests()
            : base(
                new PresenterHarness<TestObject, TestObjectListControl, ObjectListPresenter<TestObject>>(
                    new TestObjectHarness(),
                    typeof(ObjectReferenceProperty),
                    new ControlHarness<TestObjectListControl>(
                        TestObject.TestObjectListVisual,
                        Toolkit.TEST)),
                Toolkit.TEST,
                new ListValues<TestObject>(TestObjectValues.TestValues, true, true)
            )
        {
        }

        protected override IList<TestObject> GetObjectValue() { return Object.TestObjectList; }
        protected override void SetObjectValue(IList<TestObject> v)
        {
            Object.TestObjectList.Clear();
            Object.TestObjectList.AddRange(v);
        }
        protected override IList<TestObject> DefaultValue()
        {
            return new List<TestObject>();
        }

        protected override void UserInput(IList<TestObject> v) { Widget.SimulateUserInput(new ObservableCollection<IDataObject>(v.Cast<IDataObject>().ToList())); }

    }

    [TestFixture]
    public class BackReferencePresenterTests
        : ObjectReferenceListPresenterInfrastructure<TestObject, TestObjectListControl, BackReferencePresenter<TestObject>>
    {
        public BackReferencePresenterTests()
            : base(
                new PresenterHarness<TestObject, TestObjectListControl, BackReferencePresenter<TestObject>>(
                    new TestObjectHarness(),
                    typeof(BackReferenceProperty),
                    new ControlHarness<TestObjectListControl>(
                        TestObject.TestBackReferenceVisual,
                        Toolkit.TEST)),
                Toolkit.TEST,
                new ListValues<TestObject>(TestObjectValues.TestValues, true, true)
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
                With(typeof(TestObject)).
                Will(Return.Value(idoq));

            Expect.Once.On(idoq).
                Method("GetEnumerator").
                Will(Return.Value(new List<IDataObject>(IDataObjectValues.TestValues.Valids).GetEnumerator()));

            ControlHarness.SetUp();
            PresenterHarness.SetUp();
        }

        protected override IList<TestObject> GetObjectValue() { return Object.TestBackReference; }
        protected override void SetObjectValue(IList<TestObject> v)
        {
            Object.TestBackReference.Clear();
            Object.TestBackReference.AddRange(v);
        }
        protected override IList<TestObject> DefaultValue()
        {
            return new List<TestObject>();
        }

        protected override void UserInput(IList<TestObject> v) { Widget.SimulateUserInput(new ObservableCollection<IDataObject>(v.Cast<IDataObject>().ToList())); }

    }
}
