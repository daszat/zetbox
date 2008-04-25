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
    public class DateTimePresenterTests : PresenterTest<TestDateTimeControl, DateTimePresenter>
    {
        protected void AssertWidgetHasValidValue()
        {
            Assert.That(widget.HasValidValue, Is.True, "the widget should be in a valid state after this operation");
        }

        [Test]
        public void HandleNoUserInput()
        {
            Init(TestDateTimeControl.Info, TestObject.TestDateTimeProperty);
            Assert.That(obj.TestDateTime, Is.Null, "DateTimeProperty should default to null");
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInput()
        {
            Init(TestDateTimeControl.Info, TestObject.TestDateTimeProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            Assert.That(obj.TestDateTime, Is.Null);
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInputInvalid()
        {
            Init(TestDateTimeControl.Info, TestObject.TestDateTimeNotNullProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            // Input has to be rejected
            Assert.That(obj.TestDateTimeNotNull, Is.Not.Null, "property value shouldn't be null");
            // widget has to be flagged as invalid
            Assert.That(widget.HasValidValue, Is.False, "widget should have been flagged as invalid");
        }

        [Test]
        public void HandleUserInput()
        {
            Init(TestDateTimeControl.Info, TestObject.TestDateTimeProperty);
            AssertWidgetHasValidValue();

            DateTime newDateTimeValue = DateTime.Now;
            widget.SimulateUserInput(newDateTimeValue);
            
            Assert.That(obj.TestDateTime, Is.EqualTo(newDateTimeValue));
            AssertWidgetHasValidValue();
        }

    }
}
