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
                new IDataObjectValues()
            )
        {
        }

        [SetUp]
        public void SetItemSource()
        {
            base.AddEvents();
            Widget.ItemsSource = Values.Valids;
        }

        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(ObjectReferenceControl.ValueProperty, v));
        }

        [Test]
        public void TestCombobox()
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
    }

}
