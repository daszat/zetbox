
namespace Kistl.API.AbstractConsumerTests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;

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
