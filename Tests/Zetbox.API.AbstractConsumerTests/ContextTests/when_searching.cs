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

namespace Zetbox.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    public abstract class when_searching
        : AbstractTestFixture
    {

        [Test]
        public void and_skipping()
        {
            var ctx = GetContext();
            var baseQuery = ctx.GetQuery<ObjectClass>().OrderBy(o => o.ID);
            var objs = baseQuery.Take(10).ToList();
            Assert.That(objs.Count, Is.EqualTo(10), "Did not receive correct amount of objects after Take(10)");
            var firstHalf = baseQuery.Take(5).ToList();
            Assert.That(firstHalf.Count, Is.EqualTo(5), "Did not receive correct amount of objects after Take(5)");
            var secondHalf = baseQuery.Skip(5).Take(5).ToList();
            Assert.That(secondHalf.Count, Is.EqualTo(5), "Did not receive correct amount of objects after Skip(5).Take(5)");

            Assert.That(firstHalf[0], Is.SameAs(objs[0]), "0");
            Assert.That(firstHalf[1], Is.SameAs(objs[1]), "1");
            Assert.That(firstHalf[2], Is.SameAs(objs[2]), "2");
            Assert.That(firstHalf[3], Is.SameAs(objs[3]), "3");
            Assert.That(firstHalf[4], Is.SameAs(objs[4]), "4");
            Assert.That(secondHalf[0], Is.SameAs(objs[5]), "5");
            Assert.That(secondHalf[1], Is.SameAs(objs[6]), "6");
            Assert.That(secondHalf[2], Is.SameAs(objs[7]), "7");
            Assert.That(secondHalf[3], Is.SameAs(objs[8]), "8");
            Assert.That(secondHalf[4], Is.SameAs(objs[9]), "9");
        }

        //[TestCase(10, 10, 10)]
        //[TestCase(20, 10, 10)]
        //[TestCase(10, 20, 10, Ignore = true, IgnoreReason = "NHibernate bug, see Case 9162")]
        //public void and_taking_twice(int first, int second, int expected)
        //{
        //    var ctx = GetContext();
        //    var baseQuery = ctx.GetQuery<ObjectClass>().OrderBy(o => o.ID);
        //    var objs = baseQuery.Take(first).Take(second).ToList();
        //    Assert.That(objs.Count, Is.EqualTo(expected), string.Format("Did not receive correct amount of objects after Take({0}).Take({1})", first, second));
        //}

        [TestCase(10, 10, 10)]
        [TestCase(20, 10, 10)]
        [TestCase(10, 20, 10)]
        public void and_taking_twice_after_where(int first, int second, int expected)
        {
            var ctx = GetContext();
            var baseQuery = ctx.GetQuery<ObjectClass>().OrderBy(o => o.ID);
            var objs = baseQuery.Take(first).Where(o => o.Name.Length > 1).Take(second).ToList();
            Assert.That(objs.Count, Is.EqualTo(expected), string.Format("Did not receive correct amount of objects after Take({0}).Take({1})", first, second));
        }

        [Test]
        public void should_reject_projections_to_IPersistenceObject()
        {
            var ctx = GetContext();
            var qry = ctx.GetQuery<TestObjClass>().Select(t => new { X = t });

            Assert.That(() => qry.ToList(), Throws.InstanceOf<NotSupportedException>());
        }
    }
}
