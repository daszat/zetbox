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
    public class BoolPresenterTests : ValuePresenterTests<bool, TestBoolControl, BoolPresenter>
    {
        [SetUp]
        public void InitControls()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolNotNullProperty);
        }

        protected override bool GetObjectValue() { return obj.TestBoolNotNull; }
        protected override bool GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(bool v) { obj.TestBoolNotNull = v; }
        protected override void UserInput(bool v) { widget.SimulateUserInput(v); }

        [Test]
        public void HandleTrueUserInput()
        {
            AssertWidgetHasValidValue();

            bool newBoolValue = true;
            widget.SimulateUserInput(newBoolValue);

            Assert.That(obj.TestBool, Is.EqualTo(newBoolValue));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleFalseUserInput()
        {
            AssertWidgetHasValidValue();

            bool newBoolValue = false;
            widget.SimulateUserInput(newBoolValue);

            Assert.That(obj.TestBool, Is.EqualTo(newBoolValue));
            AssertWidgetHasValidValue();
        }

    }

    [TestFixture]
    public class NullableBoolPresenterTests : NullablePresenterTests<bool, TestNullableBoolControl, BoolPresenter>
    {

        [SetUp]
        public void InitControls()
        {
            Init(TestNullableBoolControl.Info, TestObject.TestBoolProperty);
        }

        protected override bool? GetObjectValue() { return obj.TestBool; }
        protected override bool? GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(bool? v) { obj.TestBool = v; }
        protected override void UserInput(bool? v) { widget.SimulateUserInput(v); }

        [Test]
        public void HandleTrueUserInput()
        {
            AssertWidgetHasValidValue();

            bool newBoolValue = true;
            widget.SimulateUserInput(newBoolValue);
            
            Assert.That(obj.TestBool, Is.EqualTo(newBoolValue));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleFalseUserInput()
        {
            AssertWidgetHasValidValue();

            bool newBoolValue = false;
            widget.SimulateUserInput(newBoolValue);
            
            Assert.That(obj.TestBool, Is.EqualTo(newBoolValue));
            AssertWidgetHasValidValue();
        }


    }
}
