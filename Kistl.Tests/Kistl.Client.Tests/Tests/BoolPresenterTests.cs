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
    public class BoolPresenterTests : PresenterTest<TestBoolControl, BoolPresenter>
    {
        protected void AssertWidgetHasValidValue()
        {
            Assert.That(widget.HasValidValue, Is.True, "the widget should be in a valid state after this operation");
        }

        [Test]
        public void HandleNoUserInput()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolProperty);
            Assert.That(obj.TestBool, Is.Null, "BoolProperty should default to null");
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInput()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            Assert.That(obj.TestBool, Is.Null);
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInputInvalid()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolNotNullProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            // Input has to be rejected
            Assert.That(obj.TestBoolNotNull, Is.Not.Null, "property value shouldn't be null");
            // widget has to be flagged as invalid
            Assert.That(widget.HasValidValue, Is.False, "widget should have been flagged as invalid");
        }

        [Test]
        public void HandleTrueUserInput()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolProperty);
            AssertWidgetHasValidValue();

            bool newBoolValue = true;
            widget.SimulateUserInput(newBoolValue);

            Assert.That(obj.TestBool, Is.EqualTo(newBoolValue));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleFalseUserInput()
        {
            Init(TestBoolControl.Info, TestObject.TestBoolProperty);
            AssertWidgetHasValidValue();

            bool newBoolValue = false;
            widget.SimulateUserInput(newBoolValue);

            Assert.That(obj.TestBool, Is.EqualTo(newBoolValue));
            AssertWidgetHasValidValue();
        }

    }
}
