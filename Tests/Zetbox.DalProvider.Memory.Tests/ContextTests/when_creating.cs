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

namespace Zetbox.DalProvider.Memory.Tests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;
    using NUnit.Framework;

    [TestFixture]
    public class when_creating 
        : AbstractMemoryContextTextFixture
    {
        [Test]
        public void should_resolve_readwrite()
        {
            var ctx = GetMemoryContext();

            Assert.That(ctx, Is.Not.Null);
            Assert.That(ctx.IsReadonly, Is.False);
        }

        [Test]
        public void should_be_queryable()
        {
            var ctx = GetMemoryContext();

            var query = ctx.GetQuery<ObjectClass>();
            Assert.That(query, Is.Not.Null);

            var list = query.ToList();

            Assert.That(list, Is.Empty);
        }
    }
}
