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
                IDataObjectValues.TestValues
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

        // TODO: Test changeing ItemsSource
    }

    [TestFixture]
    public class ObjectListControlTests
        : ValueControlTests<IList<IDataObject>, ObjectListControl>
    {
        public ObjectListControlTests()
            : base(
                new ControlHarness<ObjectListControl>(TestObject.TestObjectListVisual, Toolkit.WPF),
                new ListValues<IDataObject>(IDataObjectValues.TestValues) { IsUnique = true, IsEmptyValid = true }
            )
        {
        }

        [SetUp]
        public void SetItemSource()
        {
            base.AddEvents();
            Widget.ItemsSource = IDataObjectValues.TestValues.Valids;
        }

        public override void TestUserInput()
        {
            TestUserInput((w, v) => w.SetValue(ObjectReferenceControl.ValueProperty, v));
        }

        [Test]
        public void TestCombobox()
        {
            /*
            TestProperty<IList<IDataObject>>(
                w => w.Value,
                delegate(ObjectReferenceControl w, IDataObject v)
                {
                    w.SetValue(ObjectReferenceControl.ValueProperty, v);
                    Assert.AreEqual(v, ((ITestObjectReferenceControl)w).ComboboxValue, "Combobox didn't receive correct value");
                },
                Values,
                AssertThatOnlyUserInputEventFired
                );
             */
            Assert.IsTrue(false, "The control should be tested whether it displays the right values to the user");
        }
        // TODO: Test changeing ItemsSource
    }

    public sealed class ListValues<TYPE> : IValues<IList<TYPE>>
    {
        public bool IsUnique { get; set; }
        public bool IsEmptyValid { get; set; }

        public IValues<TYPE> BaseValues { get; private set; }

        public ListValues(IValues<TYPE> baseValues)
        {
            BaseValues = baseValues;
            Valids = GenerateLists(BaseValues.Valids, IsEmptyValid);

            IList<IList<TYPE>> invalidResult = new List<IList<TYPE>>();

            IList<TYPE> invalidValues = new List<TYPE>(BaseValues.Invalids);
            if (invalidValues.Count == 0)
            {
                // call GenerateLists on empty Invalids array to pass IsEmptyValid
                Invalids = GenerateLists(BaseValues.Invalids, !IsEmptyValid);
            }
            else
            {
                IEnumerator<TYPE> invalidEnumeration = invalidValues.GetEnumerator();

                Invalids = GenerateLists(BaseValues.Valids, !IsEmptyValid).Select(
                    delegate(IList<TYPE> l)
                    {
                        l.Add(invalidEnumeration.Current);
                        if (!invalidEnumeration.MoveNext())
                        {
                            // start over
                            invalidEnumeration = invalidValues.GetEnumerator();
                        }
                        return l;
                    }).ToArray();
            }
        }

        private static IList<TYPE>[] GenerateLists(TYPE[] items, bool includeEmptyList)
        {
            return new IList<TYPE>[0];
        }

        #region IValues<IList<TYPE>> Members

        public IList<TYPE>[] Valids { get; private set; }
        public IList<TYPE>[] Invalids { get; private set; }

        #endregion
    }

}
