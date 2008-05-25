using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Kistl.Client.Mocks;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

namespace Kistl.GUI.Tests
{
    public sealed class DateTimeValues : IValues<DateTime?>
    {
        public DateTime?[] Valids
        {
            get
            {
                List<DateTime?> result = new List<DateTime?>();
                result.AddRange(new DateTime?[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.Today });
                foreach (string s in new[] { "2008-01-01 11:11", "11:11", "2008-01-01" })
                {
                    result.Add(DateTime.Parse(s, System.Globalization.CultureInfo.InvariantCulture));
                }
                return result.ToArray();
            }
        }
        public DateTime?[] Invalids { get { return new DateTime?[] { }; } }
    }

    [TestFixture]
    public class DateTimePresenterTests : NullablePresenterTests<TestObject, DateTime, TestDateTimeControl, DateTimePresenter>
    {

        public DateTimePresenterTests()
            : base(
                new PresenterHarness<TestObject, TestDateTimeControl, DateTimePresenter>(
                    new TestObjectHarness(),
                    new ControlHarness<TestDateTimeControl>(TestObject.TestDateTimeVisual, Toolkit.TEST)),
                new DateTimeValues())
        { }

        protected override DateTime? GetObjectValue() { return Object.TestDateTime; }
        protected override DateTime? GetWidgetValue() { return Widget.Value; }
        protected override void SetObjectValue(DateTime? v) { Object.TestDateTime = v; }
        protected override void UserInput(DateTime? v) { Widget.SimulateUserInput(v); }
    }
}
