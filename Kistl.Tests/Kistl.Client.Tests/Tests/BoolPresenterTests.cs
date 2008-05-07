using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Kistl.Client.Mocks;
using Kistl.GUI;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class BoolPresenterTests : NullablePresenterTests<bool, TestBoolControl, BoolPresenter>
    {

        [SetUp]
        public void InitControls()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolDescriptor);
        }

        protected override bool? GetObjectValue() { return obj.TestBool; }
        protected override bool? GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(bool? v) { obj.TestBool = v; }
        protected override void UserInput(bool? v) { widget.SimulateUserInput(v); }

        protected void HandleUserInput(bool newBoolValue)
        {
            AssertWidgetHasValidValue();

            widget.SimulateUserInput(newBoolValue);

            Assert.That(obj.TestBool, Is.EqualTo(newBoolValue));
            AssertWidgetHasValidValue();

        }

        [Test]
        public void HandleTrueUserInput()
        {
            HandleUserInput(true);
        }

        [Test]
        public void HandleFalseUserInput()
        {
            HandleUserInput(false);
        }

    }
}
