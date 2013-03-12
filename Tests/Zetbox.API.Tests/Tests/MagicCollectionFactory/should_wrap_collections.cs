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

namespace Zetbox.API.Tests.MagicCollectionFactory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    [TestFixture]
    public class should_wrap_collections : AbstractApiTestFixture
    {
        #region Basic tests to check Type-comparisons
        // mostly used to improve understanding of the new API with regards to co- and contra variance

        [TestCase(typeof(IEnumerable<object>), typeof(List<string>))]
        public void infra_a_isAssignableFrom_b(Type a, Type b)
        {
            Assert.That(a.IsAssignableFrom(b));
        }

        [TestCase(typeof(IEnumerable<object>), typeof(List<string>))]
        public void infra_a_isInstanceOfType_b(Type a, Type b)
        {
            object bInstance = Activator.CreateInstance(b);
            Assert.That(a.IsInstanceOfType(bInstance));
        }

        [TestCase(typeof(List<string>), typeof(IEnumerable<object>))]
        public void infra_a_isNotSubclassOf_b(Type a, Type b)
        {
            Assert.That(!a.IsSubclassOf(b));
        }

        #endregion

        [Test]
        public void from_string_to_object()
        {
            TestFactory(baseList => Utils.MagicCollectionFactory.WrapAsCollectionHelper<string, object>(baseList));
        }

        [Test]
        public void from_any_string_to_object()
        {
            TestFactory(baseList => Utils.MagicCollectionFactory.WrapAsCollection<object>((object)baseList));
        }

        private static void TestFactory(Func<List<string>, ICollection<object>> factory)
        {
            List<string> baseList = new List<string>() { "a", "b" };

            ICollection<object> wrappedList = null;

            Assert.DoesNotThrow(() => wrappedList = factory(baseList));

            Assert.That(wrappedList, Is.EquivalentTo(baseList));

            Assert.DoesNotThrow(() => wrappedList.Add("c"));
            Assert.Throws<InvalidCastException>(() => wrappedList.Add(new object()));

            Assert.That(wrappedList, Is.EquivalentTo(baseList));
            Assert.That(baseList, Is.EquivalentTo(new[] { "a", "b", "c" }));
        }
    }
}
