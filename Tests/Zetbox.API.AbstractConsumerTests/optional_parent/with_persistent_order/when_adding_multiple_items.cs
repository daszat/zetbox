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

namespace Zetbox.API.AbstractConsumerTests.optional_parent.with_persistent_order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    public abstract class when_adding_multiple_items
        : OrderedOneNFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();

            // add in inverse creation oder to try(!) to provoke IDs != _pos order
            oneSide1.NEnds.Add(nSide2);
            oneSide1.NEnds.Add(nSide1);
        }

        [Test]
        public void should_persist_order()
        {
            Assert.That(oneSide1.NEnds, Is.EqualTo(new[] { nSide2, nSide1 }));
        }
    }
}
