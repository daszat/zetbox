
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
        protected override void DoModification()
        {
            // prepare
            oneSide.NSide.Add(nSide);
            SubmitAndReload();

            // and go
            oneSide.NSide.Remove(nSide);
        }

        [Test]
        public override void should_persist_OneSide_property_value()
        {
            DoModification();
         
            Assert.That(nSide.OneSide, Is.Null);

            SubmitAndReload();

            Assert.That(nSide.OneSide, Is.Null);
        }

        [Test]
        public override void should_persist_NSide_property_value()
        {
            DoModification();

            Assert.That(oneSide.NSide, Has.No.Member(nSide));

            SubmitAndReload();

            Assert.That(oneSide.NSide, Has.No.Member(nSide));
        }
    }
}
