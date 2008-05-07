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
            Init(TestDateTimeControl.Info, TestObject.TestDateTimeDescriptor);
        }

        protected override DateTime? GetObjectValue() { return obj.TestDateTime; }
        protected override DateTime? GetWidgetValue() { return widget.Value; }
        protected override void SetObjectValue(DateTime? v) { obj.TestDateTime = v; }
        protected override void UserInput(DateTime? v) { widget.SimulateUserInput(v); }
        protected override IEnumerable<DateTime> SomeValues()
        {
            List<DateTime> result = new List<DateTime>();
            result.AddRange(new [] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.Today });
            foreach (string s in new[] { "2008-01-01 11:11", "11:11", "2008-01-01" })
            {
                result.Add(DateTime.Parse(s, System.Globalization.CultureInfo.InvariantCulture));
            }
            return result;
        }
    }
}
