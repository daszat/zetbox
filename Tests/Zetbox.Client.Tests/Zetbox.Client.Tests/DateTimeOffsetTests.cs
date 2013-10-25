
namespace Zetbox.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.App.Base;

    [TestFixture]
    public class DateTimeOffsetTests : AbstractClientTestFixture
    {
        private IZetboxContext ctx;
        private Zetbox.App.Base.DateTimeOffset offset;
        private DateTime start;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            offset = ctx.CreateCompoundObject<Zetbox.App.Base.DateTimeOffset>();
            start = new DateTime(2012, 1, 1);
        }

        [Test]
        public void when_empty_should_add_nothing()
        {
            var result = offset.AddTo(start);
            Assert.That(result, Is.EqualTo(start));
        }

        [Test]
        public void when_yearly([Range(-2, 2)]int to_add)
        {
            offset.Years = to_add;
            var result = offset.AddTo(start);
            Assert.That(result, Is.EqualTo(start.AddYears(to_add)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly([Range(-2, 2)]int to_add)
        {
            offset.Months = to_add;
            var result = offset.AddTo(start);
            Assert.That(result, Is.EqualTo(start.AddMonths(to_add)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_daily([Range(-2, 2)]int to_add)
        {
            offset.Days = to_add;
            var result = offset.AddTo(start);
            Assert.That(result, Is.EqualTo(start.AddDays(to_add)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_hourly([Range(-2, 2)]int to_add)
        {
            offset.Hours = to_add;
            var result = offset.AddTo(start);
            Assert.That(result, Is.EqualTo(start.AddHours(to_add)));
        }

        [Test]
        public void when_minutes([Range(-2, 2)]int to_add)
        {
            offset.Minutes = to_add;
            var result = offset.AddTo(start);
            Assert.That(result, Is.EqualTo(start.AddMinutes(to_add)));
        }

        [Test]
        public void when_seconds([Range(-2, 2)]int to_add)
        {
            offset.Seconds = to_add;
            var result = offset.AddTo(start);
            Assert.That(result, Is.EqualTo(start.AddSeconds(to_add)));
        }
    }
}
