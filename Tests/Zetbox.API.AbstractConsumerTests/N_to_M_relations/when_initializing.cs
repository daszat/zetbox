
namespace Zetbox.API.AbstractConsumerTests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public abstract class when_initializing
        : NMTestFixture
    {

        [Test]
        public void should_not_be_null()
        {
            Assert.That(aSide1.BSide, Is.Not.Null);
            Assert.That(aSide2.BSide, Is.Not.Null);
            Assert.That(bSide1.ASide, Is.Not.Null);
            Assert.That(bSide2.ASide, Is.Not.Null);
        }

        [Test]
        public void should_have_only_created_objects_attached()
        {
            Assert.That(ctx.AttachedObjects, Is.EquivalentTo(new object[] { aSide1, aSide2, bSide1, bSide2 }));
        }

        [Test]
        public void should_not_be_empty()
        {
            Assert.That(aSide1.BSide, Is.Empty);
            Assert.That(aSide2.BSide, Is.Empty);
            Assert.That(bSide1.ASide, Is.Empty);
            Assert.That(bSide2.ASide, Is.Empty);
        }

        [Test]
        public void should_submit_and_reload()
        {
            SubmitAndReload();
        }
    }
}