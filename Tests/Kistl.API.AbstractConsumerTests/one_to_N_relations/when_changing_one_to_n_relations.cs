
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

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
        public void should_set_one_side_modified()
        {
            DoModification();

            Assert.That(oneSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()));
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
