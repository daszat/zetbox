
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class when_initializing
        : OneNFixture
    {
        [Test]
        public void should_be_NSide_property_not_null()
        {
            Assert.That(oneSide1.NSide, Is.Not.Null);
            Assert.That(oneSide2.NSide, Is.Not.Null);
            Assert.That(oneSide3.NSide, Is.Not.Null);
        }

        [Test]
        public void should_be_NSide_property_empty()
        {
            Assert.That(oneSide1.NSide, Is.Empty);
            Assert.That(oneSide2.NSide, Is.Empty);
            Assert.That(oneSide3.NSide, Is.Empty);
        }

        [Test]
        public void should_be_OneSide_property_null()
        {
            Assert.That(nSide1.OneSide, Is.Null);
            Assert.That(nSide2.OneSide, Is.Null);
        }
    }
}
