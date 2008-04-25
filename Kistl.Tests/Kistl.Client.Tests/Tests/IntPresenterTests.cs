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
    public class IntPresenterTests : PresenterTest<TestIntControl, IntPresenter>
    {
        protected void AssertWidgetHasValidValue()
        {
            Assert.That(widget.HasValidValue, Is.True, "the widget should be in a valid state after this operation");
        }

        [Test]
        public void HandleNoUserInput()
        {
            Init(TestIntControl.Info, TestObject.TestIntProperty);
            Assert.That(obj.TestInt, Is.Null, "IntProperty should default to null");
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInput()
        {
            Init(TestIntControl.Info, TestObject.TestIntProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            Assert.That(obj.TestInt, Is.Null);
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleNullUserInputInvalid()
        {
            Init(TestIntControl.Info, TestObject.TestIntNotNullProperty);
            AssertWidgetHasValidValue();
            widget.SimulateUserInput(null);
            // Input has to be rejected
            Assert.That(obj.TestIntNotNull, Is.Not.Null, "property value shouldn't be null");
            // widget has to be flagged as invalid
            Assert.That(widget.HasValidValue, Is.False, "widget should have been flagged as invalid");
        }

        [Test]
        public void HandleUserInput()
        {
            Init(TestIntControl.Info, TestObject.TestIntProperty);
            AssertWidgetHasValidValue();

            int newIntValue = 10;
            widget.SimulateUserInput(newIntValue);
            
            Assert.That(obj.TestInt, Is.EqualTo(newIntValue));
            AssertWidgetHasValidValue();
        }

    }
}
