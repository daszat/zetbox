
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class when_removing_from_NSide_property
        : when_changing_one_to_n_relations
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            // prepare
            oneSide1.NSide.Add(nSide1);
        }

        protected override NUnit.Framework.Constraints.Constraint GetOneSideChangingConstraint()
        {
            return Has.Member(nSide1);
        }

        protected override void DoModification()
        {
            // and go
            oneSide1.NSide.Remove(nSide1);
        }

        protected override NUnit.Framework.Constraints.Constraint GetOneSideChangedConstraint()
        {
            return Has.No.Member(nSide1);
        }

        [Test]
        public override void should_persist_OneSide_property_value()
        {
            DoModification();

            Assert.That(nSide1.OneSide, Is.Null);

            SubmitAndReload();

            Assert.That(nSide1.OneSide, Is.Null);
        }

        [Test]
        public override void should_persist_NSide_property_value()
        {
            DoModification();

            Assert.That(oneSide1.NSide, Has.No.Member(nSide1));

            SubmitAndReload();

            Assert.That(oneSide1.NSide, Has.No.Member(nSide1));
        }
    }
}
