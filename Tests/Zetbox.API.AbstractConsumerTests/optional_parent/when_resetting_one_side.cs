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

    public abstract class when_resetting_one_side
        : OneNFixture
    {
        protected virtual DataObjectState GetExpectedModifiedState()
        {
            return hasSubmitted ? DataObjectState.Modified : DataObjectState.New;
        }

        protected virtual DataObjectState GetExpectedUnmodifiedState()
        {
            return hasSubmitted ? DataObjectState.Unmodified : DataObjectState.New;
        }

        [Test]
        [Category("legacy tests")]
        public void should_work()
        {
            nSide1.OneSide = oneSide1;

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide1), "Setting the first property destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.EquivalentTo(new[] { nSide1 }), "first NSide list not correct");
            Assert.That(oneSide1.ObjectState, Is.EqualTo(GetExpectedUnmodifiedState()), "oneSide1 unmodified #1");
            Assert.That(oneSide2.ObjectState, Is.EqualTo(GetExpectedUnmodifiedState()), "oneSide unmodified #1");
            Assert.That(nSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()), "nSide1 modified #3");

            nSide1.OneSide = oneSide2;

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide2), "Setting the second property destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.Empty, "first NSide list was not cleared");
            Assert.That(oneSide2.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }), "second NSide list not correct");

            Assert.That(oneSide1.ObjectState, Is.EqualTo(GetExpectedUnmodifiedState()), "oneSide1 unmodified #2");
            Assert.That(oneSide2.ObjectState, Is.EqualTo(GetExpectedUnmodifiedState()), "oneSide2 unmodified #2");
            Assert.That(nSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()), "nSide1 modified #3");

            SubmitAndReload();

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide2), "reloading destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.Empty, "reloading destroyed the first NSide list");
            Assert.That(oneSide2.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }), "reloading destroyed the second NSide list");
            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Unmodified), "oneSide1 unmodified #3");
            Assert.That(oneSide2.ObjectState, Is.EqualTo(DataObjectState.Unmodified), "oneSide2 unmodified #3");
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Unmodified), "nSide1 modified #3");
        }
    }
}
