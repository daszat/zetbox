
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
            var result = rule.GetCurrent(start, now);

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
        public void when_monthly_first_current()
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "1";

            var result = rule.GetCurrent(start, now);
            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly_first_next()
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "1";

            var result = rule.GetNext(start, now);
            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(10));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly_last_current()
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "-1";

            var result = rule.GetCurrent(start, now);
            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(8));
            Assert.That(result.Day, Is.EqualTo(31));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_monthly_last_next()
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "-1";

            var result = rule.GetNext(start, now);
            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(30));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        #region relative next
        [TestCase(2013, 7, 1, 2013, 7, 31)]
        [TestCase(2013, 7, 19, 2013, 7, 31)]
        [TestCase(2013, 7, 31, 2013, 8, 31)]
        public void as_relative_monthly_last_next(int sy, int sm, int sd, int ey, int em, int ed)
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "-1";
            var dt = new DateTime(sy, sm, sd);

            var result = rule.GetNext(dt, dt);
            Assert.That(result, Is.EqualTo(new DateTime(ey, em, ed)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [TestCase(2013, 7, 1, 2013, 7, 5)]
        [TestCase(2013, 7, 5, 2013, 8, 5)]
        [TestCase(2013, 7, 19, 2013, 8, 5)]
        public void as_relative_monthly_5th_next(int sy, int sm, int sd, int ey, int em, int ed)
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "5";
            var dt = new DateTime(sy, sm, sd);

            var result = rule.GetNext(dt, dt);
            Assert.That(result, Is.EqualTo(new DateTime(ey, em, ed)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [TestCase(2013, 7, 1, 2013, 7, 31)]
        [TestCase(2013, 7, 5, 2013, 7, 31)]
        [TestCase(2013, 7, 19, 2013, 7, 31)]
        [TestCase(2013, 7, 31, 2013, 9, 30)]
        [TestCase(2013, 8, 1, 2013, 8, 31)]
        [TestCase(2013, 8, 5, 2013, 8, 31)]
        [TestCase(2013, 8, 19, 2013, 8, 31)]
        [TestCase(2013, 8, 31, 2013, 10, 31)]
        public void as_relative_monthly_interval_2_last_next(int sy, int sm, int sd, int ey, int em, int ed)
        {
            rule.Frequency = Frequency.Monthly;
            rule.Interval = 2;
            rule.ByMonthDay = "-1";
            var dt = new DateTime(sy, sm, sd);

            var result = rule.GetNext(dt, dt);
            Assert.That(result, Is.EqualTo(new DateTime(ey, em, ed)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }
        #endregion



        #region get relative
        // Miete
        [TestCase(2013, 7, 1, 2013, 7, 5)]
        [TestCase(2013, 7, 5, 2013, 8, 5)]
        [TestCase(2013, 7, 19, 2013, 8, 5)]
        public void as_relative_on_next_5th(int sy, int sm, int sd, int ey, int em, int ed)
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "5";
            var dt = new DateTime(sy, sm, sd);

            var result = rule.GetRelative(dt);
            Assert.That(result, Is.EqualTo(new DateTime(ey, em, ed)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void as_relative_with_count_1()
        {
            rule.Frequency = Frequency.Monthly;
            rule.ByMonthDay = "5";
            rule.Count = 1;

            Assert.That(() => rule.GetRelative(now), Throws.InvalidOperationException);
        }

        // Est
        [TestCase(2013, 7, 1, 2013, 8, 15)]
        [TestCase(2013, 7, 5, 2013, 8, 15)]
        [TestCase(2013, 7, 19, 2013, 9, 15)]
        public void as_relative_on_next_months_15th(int sy, int sm, int sd, int ey, int em, int ed)
        {
            rule.Frequency = Frequency.Monthly;
            rule.Interval = 1;
            rule.ByMonthDay = "15";
            rule.Count = 2;
            var dt = new DateTime(sy, sm, sd);

            var result = rule.GetRelative(dt);
            Assert.That(result, Is.EqualTo(new DateTime(ey, em, ed)));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }
        #endregion

        [Test]
        public void when_weekly_current()
        {
            rule.Frequency = Frequency.Weekly;

            var result = rule.GetCurrent(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(23));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_weekly_next()
        {
            rule.Frequency = Frequency.Weekly;

            var result = rule.GetNext(start, now);

            Assert.That(result.Year, Is.EqualTo(now.Year));
            Assert.That(result.Month, Is.EqualTo(9));
            Assert.That(result.Day, Is.EqualTo(30));
            Assert.That(result.TimeOfDay, Is.EqualTo(TimeSpan.Zero));
        }

        [Test]
        public void when_mondays_current()
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
        public void when_mondays_next()
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
