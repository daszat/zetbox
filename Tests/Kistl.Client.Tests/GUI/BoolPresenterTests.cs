using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

namespace Kistl.GUI.Tests
{
    public sealed class BoolValues : ValuesAdapter<bool?>
    {
        public override bool?[] Valids { get { return new bool?[] { true, false }; } }
        public override bool?[] Invalids { get { return new bool?[] { }; } }
    }

    [TestFixture]
    public class BoolPresenterTests : NullablePresenterTests<TestObject, bool, TestBoolControl, BoolPresenter>
    {
        public BoolPresenterTests()
            : base(
                new PresenterHarness<TestObject, TestBoolControl, BoolPresenter>(
                    new TestObjectHarness(),
                    typeof(BoolProperty),
                    new ControlHarness<TestBoolControl>(TestObject.TestBoolVisual, Toolkit.TEST)),
                new BoolValues())
        { }

        protected override bool? GetObjectValue() { return Object.TestBool; }
        protected override bool? GetWidgetValue() { return Widget.Value; }
        protected override void SetObjectValue(bool? v) { Object.TestBool = v; }
        protected override void UserInput(bool? v) { Widget.SimulateUserInput(v); }
    }
}
