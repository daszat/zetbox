
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
    public class RecurrenceRuleTests : AbstractClientTestFixture
    {
        private IZetboxContext ctx;
        private RecurrenceRule rule;
        private DateTime now;
        private DateTime start;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            rule = ctx.CreateCompoundObject<RecurrenceRule>();
            now = new DateTime(2012, 9, 26);
            start = new DateTime(2012, 1, 1);
        }

        [Test]
        public void when_empty_should_return_now()
        {
            var result = rule.GetCurrent(now);

            Assert.That(result, Is.EqualTo(now));
        }

        [Test]
        public void when_yearly_current()
        {
            rule.Frequency = Frequency.Yearly;

            var result = rule.GetCurrent(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_yearly_next()
        {
            rule.Frequency = Frequency.Yearly;

            var result = rule.GetNext(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year + 1));
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_quaterly_current()
        {
            rule.Frequency = Frequency.Monthly;
            rule.Interval = 3;

            var result = rule.GetCurrent(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(7));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_quaterly_next()
        {
            rule.Frequency = Frequency.Monthly;
            rule.Interval = 3;

            var result = rule.GetNext(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly_current()
        {
            rule.Frequency = Frequency.Monthly;

            var result = rule.GetCurrent(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly_next()
        {
            rule.Frequency = Frequency.Monthly;

            var result = rule.GetNext(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_weekly_current()
        {
            rule.Frequency = Frequency.Weekly;
            rule.ByDay = "MO";

            var result = rule.GetCurrent(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(24));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_weekly_next()
        {
            rule.Frequency = Frequency.Weekly;
            rule.ByDay = "MO";

            var result = rule.GetNext(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_daily_current()
        {
            rule.Frequency = Frequency.Daily;

            var result = rule.GetCurrent(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_daily_next()
        {
            rule.Frequency = Frequency.Daily;

            var result = rule.GetNext(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(27));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }
    }
}
