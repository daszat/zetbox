using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Tests.Skeletons
{

    public abstract class IPersistenceObjectTests<T>
        : IStreamableTests<T>
        where T : IPersistenceObject, new()
    {

        [Test]
        public void should_not_be_attached_when_created()
        {
            Assert.That(obj.IsAttached, Is.False);
        }

        [Test]
        public void should_roundtrip_IPersistenceObject_correctly()
        {
            T result = SerializationRoundtrip(obj);

            Assert.That(result.ID, Is.EqualTo(obj.ID));
            // [Ignore("Obsolete, DAL Provider will manage ObjectState")]
            // Assert.That(result.ObjectState, Is.EqualTo(obj.ObjectState));
        }
    }

}
