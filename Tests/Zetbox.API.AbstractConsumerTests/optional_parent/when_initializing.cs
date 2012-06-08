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

namespace Zetbox.API.AbstractConsumerTests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class when_initializing
        : OneNFixture
    {
        [Test]
        public void should_be_NSide_property_not_null()
        {
            Assert.That(oneSide1.NSide, Is.Not.Null);
            Assert.That(oneSide2.NSide, Is.Not.Null);
            Assert.That(oneSide3.NSide, Is.Not.Null);
        }

        [Test]
        public void should_be_NSide_property_empty()
        {
            Assert.That(oneSide1.NSide, Is.Empty);
            Assert.That(oneSide2.NSide, Is.Empty);
            Assert.That(oneSide3.NSide, Is.Empty);
        }

        [Test]
        public void should_be_OneSide_property_null()
        {
            Assert.That(nSide1.OneSide, Is.Null);
            Assert.That(nSide2.OneSide, Is.Null);
        }
    }
}
