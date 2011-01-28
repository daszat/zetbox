
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

    public abstract class when_adding_to_NSide_property
        : when_changing_one_to_n_relations
    {
        protected override void DoModification()
        {
            oneSide.NSide.Add(nSide);
        }

        [Test]
        public override void should_persist_OneSide_property_value()
        {
            DoModification();

            Assert.That(nSide.OneSide, Is.EqualTo(oneSide));

            SubmitAndReload();

            Assert.That(nSide.OneSide, Is.EqualTo(oneSide));
        }

        [Test]
        public override void should_persist_NSide_property_value()
        {
            DoModification();

            Assert.That(oneSide.NSide, Has.Member(nSide));

            SubmitAndReload();

            Assert.That(oneSide.NSide, Has.Member(nSide));
        }
    }
}
