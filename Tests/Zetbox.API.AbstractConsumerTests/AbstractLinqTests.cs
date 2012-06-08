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

namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    using NUnit.Framework;
    
    [TestFixture]
    public abstract class AbstractLinqTests
        : AbstractTestFixture
    {
        IZetboxContext ctx;

        [SetUp]
        public void Init()
        {
            using (IZetboxContext localCtx = GetContext())
            {
                var result = localCtx.GetQuery<TestObjClass>();
                var list = result.ToList();

                while (list.Count < 2)
                {
                    var newObj = localCtx.Create<TestObjClass>();
                    //newObj.ObjectProp = localCtx.GetQuery<Kunde>().First();
                    list.Add(newObj);
                }

                list[0].StringProp = "First";
                list[0].TestEnumProp = TestEnum.First;

                list[1].StringProp = "Second";
                list[1].TestEnumProp = TestEnum.Second;

                localCtx.SubmitChanges();
            }

            ctx = GetContext();
        }

        [TearDown]
        public void Dispose()
        {
            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }

            using (var localCtx = GetContext())
            {
                localCtx.GetQuery<TestObjClass>().ForEach(obj => { obj.ObjectProp = null; localCtx.Delete(obj); });
                localCtx.SubmitChanges();
            }
        }

        [Test]
        public void Where_filters_correctly()
        {
            var query = ctx.GetQuery<TestObjClass>();
            Func<IQueryable<TestObjClass>, ICollection> expression
                = q => q.Where(toc => toc.TestEnumProp == TestEnum.First).OrderBy(toc => toc.ID).ToList();

            var linqResult = expression(query);
            var localResult = expression(query.ToList().AsQueryable());

            Assert.That(localResult.Count, Is.GreaterThan(0), "setup failed: expected object not in database");
            Assert.That(linqResult, Is.EquivalentTo(localResult));
        }


        [Test]
        public void Count_returns_correct_count()
        {
            var query = ctx.GetQuery<TestObjClass>();
            Func<IQueryable<TestObjClass>, int> expression
                = q => q.Count();


            var linqResult = expression(query);
            var localResult = expression(query.ToList().AsQueryable());

            Assert.That(linqResult, Is.EqualTo(localResult));
        }
    }
}
