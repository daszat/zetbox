
namespace Zetbox.API.AbstractConsumerTests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class when_clearing_nside
        : when_changing_one_to_n_relations
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            // prepare
            oneSide1.NSide.Add(nSide1);
            oneSide1.NSide.Add(nSide2);
        }

        protected override NUnit.Framework.Constraints.Constraint GetOneSideChangingConstraint()
        {
            return Is.EquivalentTo(new[] { nSide1, nSide2 });
        }

        protected override void DoModification()
        {
            // and go
            oneSide1.NSide.Clear();
        }

        protected override NUnit.Framework.Constraints.Constraint GetOneSideChangedConstraint()
        {
            return Is.Empty;
        }

        [Test]
        public override void should_persist_OneSide_property_value()
        {
            DoModification();

            Assert.That(nSide1.OneSide, Is.Null);
            Assert.That(nSide2.OneSide, Is.Null);

            SubmitAndReload();

            Assert.That(nSide1.OneSide, Is.Null);
            Assert.That(nSide2.OneSide, Is.Null);
        }

        [Test]
        public override void should_persist_NSide_property_value()
        {
            DoModification();

            Assert.That(oneSide1.NSide, Has.No.Member(nSide1));
            Assert.That(oneSide1.NSide, Has.No.Member(nSide2));

            SubmitAndReload();

            Assert.That(oneSide1.NSide, Has.No.Member(nSide1));
            Assert.That(oneSide1.NSide, Has.No.Member(nSide2));
        }
    }
}
