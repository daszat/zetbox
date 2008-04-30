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
    public class DateTimePresenterTests : NullablePresenterTests<DateTime, TestDateTimeControl, DateTimePresenter>
    {
        [SetUp]
        public void InitControls()
        {
            Init(TestDateTimeControl.Info, TestObject.TestDateTimeProperty);
        }

        protected override DateTime? GetObjectValue() { return obj.TestDateTime; }
        protected override DateTime? GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(DateTime? v) { obj.TestDateTime = v; }
        protected override void UserInput(DateTime? v) { widget.SimulateUserInput(v); }

        protected void HandleUserInput(DateTime newDateTimeValue)
        {
            AssertWidgetHasValidValue();

            widget.SimulateUserInput(newDateTimeValue);

            Assert.That(obj.TestDateTime, Is.EqualTo(newDateTimeValue));
            AssertWidgetHasValidValue();
        }

        [Test]
        public void HandleExtrema()
        {
            foreach (DateTime d in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.Today })
            {
                HandleUserInput(d);
            }
        }

        [Test]
        public void HandleConstant()
        {
            foreach (string s in new[] { "2008-01-01 11:11", "11:11", "2008-01-01" })
            {
                HandleUserInput(DateTime.Parse(s, System.Globalization.CultureInfo.InvariantCulture));
            }
        }


    }
}
