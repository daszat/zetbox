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

namespace Zetbox.API.AbstractConsumerTests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public abstract class when_initializing
        : NMTestFixture
    {

        [Test]
        public void should_not_be_null()
        {
            Assert.That(aSide1.BSide, Is.Not.Null);
            Assert.That(aSide2.BSide, Is.Not.Null);
            Assert.That(bSide1.ASide, Is.Not.Null);
            Assert.That(bSide2.ASide, Is.Not.Null);
        }

        [Test]
        public void should_have_only_created_objects_attached()
        {
            Assert.That(ctx.AttachedObjects, Is.EquivalentTo(new object[] { aSide1, aSide2, bSide1, bSide2 }));
        }

        [Test]
        public void should_not_be_empty()
        {
            Assert.That(aSide1.BSide, Is.Empty);
            Assert.That(aSide2.BSide, Is.Empty);
            Assert.That(bSide1.ASide, Is.Empty);
            Assert.That(bSide2.ASide, Is.Empty);
        }

        [Test]
        public void should_submit_and_reload()
        {
            SubmitAndReload();
        }
    }
}