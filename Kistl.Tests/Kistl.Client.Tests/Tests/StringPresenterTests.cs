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
    public class StringPresenterTests : PresenterTest<TestStringControl, StringPresenter>
    {
        protected void AssertWidgetHasValidValue()
        {
            Assert.That(widget.IsValidValue, Is.True, "the widget should be in a valid state after this operation");
        }

        [Test]
        public void HandleNoUserInput()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
            Assert.That(obj.TestString, Is.Null);
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInput()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            Assert.That(obj.TestString, Is.Null);
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInputInvalid()
        {
            Init(TestStringControl.Info, TestObject.TestStringNotNullProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            // Input has to be rejected
            Assert.That(obj.TestStringNotNull, Is.Not.Null, "property value shouldn't be null");
            // widget has to be flagged as invalid
            Assert.That(widget.IsValidValue, Is.False, "widget should have been flagged as invalid");
        }

        [Test]
        public void HandleUserInput()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
            AssertWidgetHasValidValue();

            string newStringValue = "new Value";
            widget.SimulateUserInput(newStringValue);
            
            Assert.That(obj.TestString, Is.EqualTo(newStringValue));
            AssertWidgetHasValidValue();
        }

    }
}
