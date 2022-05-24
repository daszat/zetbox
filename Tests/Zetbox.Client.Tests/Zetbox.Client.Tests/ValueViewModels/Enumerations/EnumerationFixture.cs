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

namespace Zetbox.Client.Tests.ValueViewModels.Enumerations
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

    public class EnumerationFixture
        : AbstractClientTestFixture
    {
        protected enum TestEnum
        {
            Zero = 0,
            One = 1,
            Two,
            Three
        }
        private KeyValuePair<int, string>[] values;
        private KeyValuePair<int?, string>[] valuesWithNull;

        protected EnumerationValueViewModel obj;
        protected Mock<Models.IEnumerationValueModel> valueModelMock;

        public override void SetUp()
        {
            base.SetUp();

            values = new[]
            {
                new KeyValuePair<int, string>(0, "Zero"),
                new KeyValuePair<int, string>(1, "One"),
                new KeyValuePair<int, string>(2, "Two"),
                new KeyValuePair<int, string>(3, "Three"),
            };

            valuesWithNull = new[]
            {
                new KeyValuePair<int?, string>(null, String.Empty),
                new KeyValuePair<int?, string>(0, "Zero"),
                new KeyValuePair<int?, string>(1, "One"),
                new KeyValuePair<int?, string>(2, "Two"),
                new KeyValuePair<int?, string>(3, "Three"),
            };

            valueModelMock = new Mock<Models.IEnumerationValueModel>(MockBehavior.Strict);
            // ignore Error handling for now
            valueModelMock.SetupGet<string>(o => o.Error).Returns(String.Empty);
            valueModelMock.SetupProperty(o => o.Value);
            valueModelMock.SetupGet(o => o.Label).Returns("ValueLabel");
            valueModelMock.SetupGet(o => o.AllowNullInput).Returns(true);
            valueModelMock.SetupGet(o => o.ReportErrors).Returns(true);

            valueModelMock
                .Setup<IEnumerable<KeyValuePair<int, string>>>(o => o.GetEntries().Result)
                .Returns(values);

            obj = new EnumerationValueViewModel(scope.Resolve<IViewModelDependencies>(), scope.Resolve<BaseMemoryContext>(), null, valueModelMock.Object);
        }

        [Test]
        public void should_list_possible_values_using_GetEntries()
        {
            Assert.That(obj.PossibleValues, Is.EquivalentTo(valuesWithNull));

            valueModelMock.Verify(o => o.GetEntries(), Times.Once());
        }

        [Test]
        public void should_accept_enum_strings_as_FormattedValue()
        {
            obj.FormattedValue = "Two";

            Assert.That(obj.Value, Is.EqualTo(2));
        }

        [Test]
        public void should_set_Error()
        {
            obj.FormattedValue = "Foo";
            Assert.That(obj.IsValid, Is.False);
        }

        [Test]
        public void should_format_Value()
        {
            obj.Value = 3;

            Assert.That(obj.FormattedValue, Is.EqualTo("Three"));
        }
    }
}
