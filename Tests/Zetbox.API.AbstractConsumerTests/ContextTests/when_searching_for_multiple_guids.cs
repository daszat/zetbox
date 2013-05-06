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

    public class when_searching_for_multiple_guids
        : AbstractTestFixture
    {
        IZetboxContext ctx;
        List<ObjectClass> objs;
        List<Guid> guids;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            var objs = ctx.GetQuery<ObjectClass>().Take(10).ToList();
            guids = objs.Select(cls => cls.ExportGuid).ToList();
        }

        [Test]
        public void should_return_all_objects()
        {
            ctx = GetContext();

            var foundGuids = ctx.FindPersistenceObjects<ObjectClass>(guids).ToList().Select(cls => cls.ExportGuid);
            Assert.That(foundGuids, Is.EquivalentTo(guids));
        }

        [Test]
        public void should_not_return_nulls()
        {
            var lookupGuids = guids.Concat(new[] { Guid.Empty }); // add illegal value

            ctx = GetContext();

            var foundObjs = ctx.FindPersistenceObjects<ObjectClass>(lookupGuids).ToList();
            Assert.That(foundObjs, Has.No.Member(null), "FindPersistenceObjects returned a null entry");

            var foundGuids = foundObjs.Select(cls => cls.ExportGuid);
            Assert.That(foundGuids, Is.EquivalentTo(guids));
        }
    }
}
