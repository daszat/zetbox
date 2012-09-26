
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

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            rule = ctx.CreateCompoundObject<RecurrenceRule>();
            now = new DateTime(2012, 9, 26);
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
            rule.EveryYear = true;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_yearly_next()
        {
            rule.EveryYear = true;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year + 1));
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_quaterly_current()
        {
            rule.EveryQuater = true;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(7));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_quaterly_next()
        {
            rule.EveryQuater = true;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly_current()
        {
            rule.EveryMonth = true;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly_next()
        {
            rule.EveryMonth = true;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_weekly_current()
        {
            rule.EveryDayOfWeek = App.Base.DayOfWeek.Monday;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(24));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_weekly_next()
        {
            rule.EveryDayOfWeek = App.Base.DayOfWeek.Monday;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_daily_current()
        {
            rule.EveryDay = true;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_daily_next()
        {
            rule.EveryDay = true;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(27));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_months_offset_current()
        {
            rule.MonthsOffset = 1;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_months_offset_next()
        {
            rule.MonthsOffset = 1;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_days_offset_current()
        {
            rule.DaysOffset = 1;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(27));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_days_offset_next()
        {
            rule.DaysOffset = 1;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(27));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_hours_offset_current()
        {
            rule.HoursOffset = 1;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.FromHours(1)));
        }

        [Test]
        public void when_hours_offset_next()
        {
            rule.HoursOffset = 1;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.FromHours(1)));
        }

        [Test]
        public void when_minutes_offset_current()
        {
            rule.MinutesOffset = 1;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.FromMinutes(1)));
        }

        [Test]
        public void when_minutes_offset_next()
        {
            rule.MinutesOffset = 1;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(26));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.FromMinutes(1)));
        }

        [Test]
        public void when_monthly_and_minutes_offset_current()
        {
            rule.EveryMonth = true;
            rule.MinutesOffset = 1;

            var result = rule.GetCurrent(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.FromMinutes(1)));
        }

        [Test]
        public void when_monthly_and_minutes_offset_next()
        {
            rule.EveryMonth = true;
            rule.MinutesOffset = 1;

            var result = rule.GetNext(now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.FromMinutes(1)));
        }
    }
}
