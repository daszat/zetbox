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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.DalProvider.Ef.Mocks;

using NUnit.Framework;

namespace Zetbox.DalProvider.Ef.Tests
{
    [TestFixture]
    public class QueryTranslatorTests : AbstractEFTestFixture
    {
        public interface TestInterface
        {
            int ID { get; }
        }

        public class TestClass : TestInterface
        {
            public int ID { get; set; }
        }

        private TestClass tc;

        [Test]
        public void test_against_class()
        {
            tc = new TestClass() { ID = 1 };
            var q = ctx.GetQuery<ObjectReferenceProperty>().Where(orp => orp.ID == tc.ID);
            foreach (var orp in q)
            {
                Assert.That(orp, Is.Not.Null);
            }
        }

        private TestInterface ti;
        [Test]
        public void test_against_interface()
        {
            ti = new TestClass() { ID = 1 };
            var q = ctx.GetQuery<ObjectReferenceProperty>().Where(orp => orp.ID == ti.ID);
            foreach (var orp in q)
            {
                Assert.That(orp, Is.Not.Null);
            }
        }

        private DataType dt;
        [Test]
        public void test_against_zetbox_object()
        {
            dt = ctx.GetQuery<DataType>().First();
            var q = ctx.GetQuery<ObjectReferenceProperty>().Where(orp => orp.ID == dt.ID);
            foreach (var orp in q)
            {
                Assert.That(orp, Is.Not.Null);
            }
        }

        [Test]
        public void query_with_enum()
        {
            var q = ctx.GetQuery<Relation>().Where(r => r.Storage == StorageType.Separate).ToList();
            Assert.IsNotEmpty(q);
            foreach (var r in q)
            {
                Assert.That(r, Is.Not.Null);
                Assert.That(r.Storage, Is.EqualTo(StorageType.Separate));
            }
        }

        /// <summary>
        /// </summary>
        [Test]
        public void query_with_objectfilter()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var module = ctx.GetQuery<Module>().Where(m => m.Name == "ZetboxBase").First();
                Assert.That(module, Is.Not.Null);
                var result = ctx.GetQuery<ObjectClass>().Where(c => c.Module == module).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }

    }
}
