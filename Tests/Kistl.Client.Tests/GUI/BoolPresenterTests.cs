using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

namespace Kistl.GUI.Tests
{
    [TestFixture]
    public class BoolPresenterTests : NullablePresenterTests<TestObject, bool, TestBoolControl, BoolPresenter>
    {
        public BoolPresenterTests()
            : base(
                new PresenterHarness<TestObject, TestBoolControl, BoolPresenter>(
                    new TestObjectHarness(),
                    new ControlHarness<TestBoolControl>(TestObject.TestBoolVisual, Toolkit.TEST)))
        { }

        protected override bool? GetObjectValue() { return Object.TestBool; }
        protected override bool? GetWidgetValue() { return Widget.Value; }
        protected override void SetObjectValue(bool? v) { Object.TestBool = v; }
        protected override void UserInput(bool? v) { Widget.SimulateUserInput(v); }
        protected override IEnumerable<bool> SomeValues()
        {
            return new List<bool>(new[] { true, false });
        }
    }
}
