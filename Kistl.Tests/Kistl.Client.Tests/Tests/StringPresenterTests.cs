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
            Assert.That(widget.HasValidValue, Is.True);
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
            Assert.That(obj.TestStringNotNull, Is.Not.Null);
            // widget has to be flagged as invalid
            Assert.That(widget.HasValidValue, Is.False);
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
