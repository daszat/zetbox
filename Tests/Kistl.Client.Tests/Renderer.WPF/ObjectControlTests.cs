using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;
using NUnit.Framework.Constraints;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;
using Kistl.GUI.Tests;

namespace Kistl.GUI.Renderer.WPF.Tests
{

    [TestFixture]
    public class ObjectReferenceControlTests
        : ValueControlTests<IDataObject, ObjectReferenceControl>
    {
        public ObjectReferenceControlTests()
            : base(
                new ControlHarness<ObjectReferenceControl>(TestObject.TestObjectReferenceVisual, Toolkit.WPF),
                IDataObjectValues.TestValues
            )
        {
        }

        [SetUp]
        public void SetItemSource()
        {
            base.AddEvents();
            Widget.ItemsSource = Values.Valids;
            AssertThatNoEventsFired();
        }

        [Test]
        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(ObjectReferenceControl.ValueProperty, v));
        }

        [Test]
        public void TestComboboxUserInput()
        {
            TestProperty<IDataObject>(
                w => w.Value,
                delegate(ObjectReferenceControl w, IDataObject v)
                {
                    w.SetValue(ObjectReferenceControl.ValueProperty, v);
                    Assert.AreEqual(v, ((ITestObjectReferenceControl)w).ComboboxValue, "Combobox didn't receive correct value");
                },
                Values,
                AssertThatOnlyUserInputEventFired
                );
        }

        [Test]
        public void TestComboboxProgrammatically()
        {
            TestProperty<IDataObject>(
                w => w.Value,
                delegate(ObjectReferenceControl w, IDataObject v)
                {
                    ((IValueControl<IDataObject>)w).Value = v;
                    Assert.AreEqual(v, ((ITestObjectReferenceControl)w).ComboboxValue, "Combobox didn't receive correct value");
                },
                Values,
                AssertThatNoEventsFired
                );
        }

        [Test]
        [Ignore("unimplemented")]
        public void TestItemsSource() { }

        [Test]
        public void TestObjectType()
        {
            TestProperty<Type>(
                w => w.ObjectType, (w, v) => w.ObjectType = v,
                new Values<Type>()
                {
                    Valids = new[] {
                        typeof(String), 
                        typeof(TestStringControl),
                        typeof(StringProperty) 
                    }
                },
                AssertThatNoEventsFired
                );
        }
    }

    [TestFixture]
    public class ObjectListControlTests
        : ValueControlTests<IList<IDataObject>, ObjectListControl>
    {
        public ObjectListControlTests()
            : base(
                new ControlHarness<ObjectListControl>(TestObject.TestObjectListVisual, Toolkit.WPF),
                new ListValues<IDataObject>(IDataObjectValues.TestValues, true, true)
            )
        {
        }

        [SetUp]
        public void SetItemSource()
        {
            base.AddEvents();
            Widget.ItemsSource = IDataObjectValues.TestValues.Valids;
        }

        [Test]
        public void TestNullValue()
        {
            Widget.Value = null;
            Assert.That(
                Widget.Value, new EmptyCollectionConstraint(),
                "ListControls should mutate NULLs into empty lists");
        }

        [Test]
        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(ObjectListControl.ValueProperty, v));
        }

        [Test]
        public void TestListboxUserInput()
        {
            TestProperty<IList<IDataObject>>(
                w => w.Value,
                delegate(ObjectListControl w, IList<IDataObject> v)
                {
                    w.SetValue(ObjectListControl.ValueProperty, v);
                    Assert.AreEqual(v, ((ITestReferenceListControl)w).ListboxValue, "Listbox didn't receive correct value");
                },
                Values,
                AssertThatOnlyUserInputEventFired
                );
        }

        [Test]
        public void TestListboxProgrammatically()
        {
            TestProperty<IList<IDataObject>>(
                w => w.Value,
                delegate(ObjectListControl w, IList<IDataObject> v)
                {
                    ((IValueControl<IList<IDataObject>>)w).Value = v;
                    Assert.AreEqual(v, ((ITestReferenceListControl)w).ListboxValue, "Listbox didn't receive correct value");
                },
                Values,
                AssertThatNoEventsFired
                );
        }

        [Test]
        [Ignore("unimplemented")]
        public void TestItemsSource() { }

        [Test]
        public void TestObjectType()
        {
            TestProperty<Type>(
                w => w.ObjectType, (w, v) => w.ObjectType = v,
                new Values<Type>()
                {
                    Valids = new[] {
                        typeof(String), 
                        typeof(TestStringControl),
                        typeof(StringProperty) 
                    }
                },
                AssertThatNoEventsFired
                );
        }
    }



}
