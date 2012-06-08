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
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class when_adding_to_NSide_property
        : when_changing_one_to_n_relations
    {
        protected override NUnit.Framework.Constraints.Constraint GetOneSideChangingConstraint()
        {
            return Has.No.Member(nSide1);
        }

        protected override void DoModification()
        {
            oneSide1.NSide.Add(nSide1);
        }

        protected override NUnit.Framework.Constraints.Constraint GetOneSideChangedConstraint()
        {
            return Has.Member(nSide1);
        }

        [Test]
        public override void should_persist_OneSide_property_value()
        {
            DoModification();

            Assert.That(nSide1.OneSide, Is.EqualTo(oneSide1));

            SubmitAndReload();

            Assert.That(nSide1.OneSide, Is.EqualTo(oneSide1));
        }

        [Test]
        public override void should_persist_NSide_property_value()
        {
            DoModification();

            Assert.That(oneSide1.NSide, Has.Member(nSide1));

            SubmitAndReload();

            Assert.That(oneSide1.NSide, Has.Member(nSide1));
        }
    }
}
