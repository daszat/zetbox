
namespace Kistl.API.AbstractConsumerTests.PersistenceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    using NUnit.Framework;

    public abstract class when_loaded
        : ObjectLoadFixture
    {

        [Test]
        public void should_be_unmodified()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        public void should_be_attached()
        {
            Assert.That(obj.IsAttached, Is.True);
        }
    }
}
