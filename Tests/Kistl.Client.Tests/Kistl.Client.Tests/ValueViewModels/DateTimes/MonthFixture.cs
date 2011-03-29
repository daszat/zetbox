
namespace Kistl.Client.Tests.ValueViewModels.DateTimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ValueViewModels;
    using Moq;
    using NUnit.Framework;

    public class MonthFixture
        : AbstractClientTestFixture
    {
        protected NullableMonthPropertyViewModel obj;
        protected Mock<Models.IDateTimeValueModel> valueModelMock;

        public override void SetUp()
        {
            base.SetUp();

            valueModelMock = new Mock<Models.IDateTimeValueModel>(MockBehavior.Strict);
            // ignore Error handling for now
            valueModelMock.SetupGet(o => o.Error).Returns(String.Empty);
            valueModelMock.SetupGet(o => o.DateTimeStyle).Returns(DateTimeStyles.Date);
            valueModelMock.SetupProperty(o => o.Value);

            obj = new NullableMonthPropertyViewModel(
                scope.Resolve<IViewModelDependencies>(), 
                scope.Resolve<BaseMemoryContext>(),
                valueModelMock.Object);
        }

        [Test]
        public void should_set_DatePartVisible()
        {
            Assert.That(obj.DatePartVisible, Is.True);

            valueModelMock.Verify();
        }

        [Test]
        public void should_not_set_TimePartVisible()
        {
            Assert.That(obj.TimePartVisible, Is.False);
            valueModelMock.Verify();
        }

        [TestCase("2011-01-02 10:00", 2011, 1)]
        public void should_accept_valid_FormattedValue(string newValue, int year, int month)
        {
            var date = (DateTime)System.Convert.ChangeType(newValue, typeof(DateTime));
            obj.FormattedValue = newValue;

            Assert.That(obj.Value, Is.EqualTo(date));
            Assert.That(obj.FormattedValue, Is.EqualTo(date.ToShortDateString()));
            Assert.That(obj.Year, Is.EqualTo(year));
            Assert.That(obj.Month, Is.EqualTo(month));

            valueModelMock.Verify();
        }

        // TODO: [TestCase(null)]
        [TestCase("")]
        [TestCase("foo")]
        public void should_not_fail_invalid_FormattedValue(string newValue)
        {
            var oldValue = obj.Value;
            var oldFormattedValue = obj.FormattedValue;

            obj.FormattedValue = newValue;

            Assert.That(obj.Value, Is.EqualTo(oldValue));
            Assert.That(obj.FormattedValue, Is.EqualTo(newValue));

            valueModelMock.Verify();
        }

        [TestCase("2011-01-02 10:00", 2011, 1)]
        public void should_accept_Value(string newValue, int year, int month)
        {
            var date = (DateTime)System.Convert.ChangeType(newValue, typeof(DateTime));
            obj.Value = date;

            Assert.That(obj.Value, Is.EqualTo(date));
            Assert.That(obj.FormattedValue, Is.EqualTo(date.ToShortDateString()));
            Assert.That(obj.Year, Is.EqualTo(year));
            Assert.That(obj.Month, Is.EqualTo(month));

            valueModelMock.Verify();
        }

        [TestCase("2011-01-01 10:00", "10:15", "2011-01-01 10:15")]
        public void should_accept_TimePartString(string initalValue, string timePart, string resultValue)
        {
            var initialDate = (DateTime)System.Convert.ChangeType(initalValue, typeof(DateTime));
            var resultDate = (DateTime)System.Convert.ChangeType(resultValue, typeof(DateTime));

            obj.Value = initialDate;
            obj.TimePartString = timePart;

            Assert.That(obj.Value, Is.EqualTo(resultDate));
            Assert.That(obj.FormattedValue, Is.EqualTo(resultDate.ToShortDateString()));

            valueModelMock.Verify();
        }

        [TestCase("2011-01-13 10:00", 2, "2011-02-13 10:00")]
        public void should_accept_Month_changes(string initalValue, int newMonth, string resultValue)
        {
            var initialDate = (DateTime)System.Convert.ChangeType(initalValue, typeof(DateTime));
            var resultDate = (DateTime)System.Convert.ChangeType(resultValue, typeof(DateTime));

            obj.Value = initialDate;
            obj.Month = newMonth;

            Assert.That(obj.Value, Is.EqualTo(resultDate));
            Assert.That(obj.FormattedValue, Is.EqualTo(resultDate.ToShortDateString()));

            valueModelMock.Verify();
        }

        [TestCase("2011-01-13 10:00", 2010, "2010-01-13 10:00")]
        public void should_accept_Year_changes(string initalValue, int newYear, string resultValue)
        {
            var initialDate = (DateTime)System.Convert.ChangeType(initalValue, typeof(DateTime));
            var resultDate = (DateTime)System.Convert.ChangeType(resultValue, typeof(DateTime));

            obj.Value = initialDate;
            obj.Year = newYear;

            Assert.That(obj.Value, Is.EqualTo(resultDate));
            Assert.That(obj.FormattedValue, Is.EqualTo(resultDate.ToShortDateString()));

            valueModelMock.Verify();
        }
    }
}
