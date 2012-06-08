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

    public abstract class when_changing_one_to_n_relations
        : OneNFixture
    {
        protected abstract void DoModification();

        protected abstract NUnit.Framework.Constraints.Constraint GetOneSideChangingConstraint();
        protected abstract NUnit.Framework.Constraints.Constraint GetOneSideChangedConstraint();

        /// <summary>
        /// Returns New or Modified depending on whether SubmitAndReload() was already called.
        /// Set hasSubmitted to trigger
        /// </summary>
        protected virtual DataObjectState GetExpectedModifiedState()
        {
            return hasSubmitted ? DataObjectState.Modified : DataObjectState.New;
        }

        [Test]
        [Ignore("Case#2162: Currently NHibernate has troubles with correclty sequencing the notifications")]
        public void should_notify_OneSide_property()
        {
            TestChangeNotification(nSide1, "OneSide",
                DoModification,
                () => { Assert.That(oneSide1.NSide, GetOneSideChangingConstraint(), "changing event should be triggered before the value has changed"); },
                () => { Assert.That(oneSide1.NSide, GetOneSideChangedConstraint(), "changed event should be triggered after the value has changed"); }
            );
        }

        [Test]
        public void should_notify_OneSide_property_no_content_check()
        {
            TestChangeNotification(nSide1, "OneSide",
                DoModification,
                null,
                null
            );
        }

        [Test]
        [Ignore("Case#2162: Currently NHibernate has troubles with correclty sequencing the notifications")]
        public void should_notify_NSide_property()
        {
            TestChangeNotification(oneSide1, "NSide",
                DoModification,
                null,
                null
            );
        }

        [Test]
        public void should_not_set_one_side_modified()
        {
            DoModification();

            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Unmodified).Or.EqualTo(DataObjectState.New));
        }

        [Test]
        public void should_set_n_side_modified()
        {
            DoModification();

            Assert.That(nSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()));
        }

        public abstract void should_persist_OneSide_property_value();

        public abstract void should_persist_NSide_property_value();
    }
}
