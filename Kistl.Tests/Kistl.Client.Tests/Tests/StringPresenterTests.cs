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
    public class StringPresenterTests : ReferencePresenterTests<string, TestStringControl, StringPresenter>
    {
        [SetUp]
        public void InitControls()
        {
            Init(TestStringControl.Info, TestObject.TestStringProperty);
        }

        protected override string GetObjectValue() { return obj.TestString; }
        protected override string GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(string v) { obj.TestString = v; }
        protected override void UserInput(string v) { widget.SimulateUserInput(v); }

        protected void HandleUserInput(string newStringValue)
        {
            AssertWidgetHasValidValue();

            widget.SimulateUserInput(newStringValue);

            Assert.That(obj.TestString, Is.EqualTo(newStringValue));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleEmptyString()
        {
            HandleUserInput("");
        }

        [Test]
        public void HandleNumbers()
        {
            foreach (string s in new[] { "00012346789", "0", "0.0", "0.1", "0.002", "10.0e100", "0x120" })
            {
                HandleUserInput(s);
            }
        }

        [Test]
        public void HandleStrings()
        {
            foreach (string s in new[] { "...", "<xss>", "!'\"§!$§%&/(){}&amp;", "normal string",
                "very long string: very long string: very long string: very long string: very long string: very long string: " +
                "very long string: very long string: very long string: very long string: very long string: very long string: " +
                "very long string: very long string: very long string: very long string: very long string: very long string: " +
                "very long string: very long string: very long string: very long string: very long string: very long string"
            })
            {
                HandleUserInput(s);
            }
        }
    }
}
