// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Tests.ValueViewModels.DateTimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Moq;
    using NUnit.Framework;

    [Ignore("Refactor the SUT to provide Date, DateTime and Time ViewModels separately to ease testing")]
    public class DateTimeFixture
        : AbstractClientTestFixture
    {
        protected NullableDateTimePropertyViewModel obj;
        protected Mock<Models.IDateTimeValueModel> valueModelMock;

        public override void SetUp()
        {
            base.SetUp();

            valueModelMock = new Mock<Models.IDateTimeValueModel>(MockBehavior.Strict);
            // ignore Error handling for now
            valueModelMock.SetupGet(o => o.Error).Returns(String.Empty);
            valueModelMock.SetupGet(o => o.DateTimeStyle).Returns(DateTimeStyles.DateTime);
            valueModelMock.SetupProperty(o => o.Value);
            valueModelMock.SetupGet(o => o.Label).Returns("ValueLabel");

            obj = new NullableDateTimePropertyViewModel(
                scope.Resolve<IViewModelDependencies>(), 
                scope.Resolve<BaseMemoryContext>(),
                null,
                valueModelMock.Object);
        }

        [Test]
        public void should_set_DatePartVisible()
        {
            Assert.That(obj.DatePartVisible, Is.True);

            valueModelMock.Verify();
        }

        [Test]
        public void should_set_TimePartVisible()
        {
            Assert.That(obj.TimePartVisible, Is.True);
            valueModelMock.Verify();
        }

        [TestCase("2011-01-01 10:00")]
        public void should_accept_valid_FormattedValue(string newValue)
        {
            var date = (DateTime)System.Convert.ChangeType(newValue, typeof(DateTime));
            obj.FormattedValue = newValue;

            Assert.That(obj.Value, Is.EqualTo(date));
            Assert.That(obj.FormattedValue, Is.EqualTo(date.ToString()));

            valueModelMock.Verify();
        }

        // TODO: [TestCase(null)]
        [TestCase("")]
        [TestCase("foo")]
        public void should_not_fail_invalid_FormattedValue(string newValue)
        {
            var oldValue = obj.Value;

            obj.FormattedValue = newValue;

            Assert.That(obj.Value, Is.EqualTo(oldValue));
            Assert.That(obj.FormattedValue, Is.EqualTo(newValue));

            valueModelMock.Verify();
        }

        [TestCase("2011-01-01 10:00")]
        public void should_accept_Value(string newValue)
        {
            var date = (DateTime)System.Convert.ChangeType(newValue, typeof(DateTime));
            obj.Value = date;

            Assert.That(obj.Value, Is.EqualTo(date));
            Assert.That(obj.FormattedValue, Is.EqualTo(date.ToString()));

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
            Assert.That(obj.FormattedValue, Is.EqualTo(resultDate.ToString()));

            valueModelMock.Verify();
        }
    }
}
