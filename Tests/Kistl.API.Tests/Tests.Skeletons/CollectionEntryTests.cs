using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.API.Tests.Skeletons
{

    public abstract class CollectionEntryTests<CEType>
        where CEType : ICollectionEntry, new()
    {
        private CEType obj;

        [SetUp]
        public virtual void SetUp()
        {
            obj = new CEType();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_to_null_stream()
        {
            obj.ToStream(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_from_null_stream()
        {
            obj.FromStream(null);
        }


        [Test]
        public void should_not_be_attached_after_init()
        {
            Assert.That(obj.IsAttached, Is.False);
        }

        [Test]
        public void should_roundtrip_correctly()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            CEType result = new CEType();
            result.FromStream(sr);

            Assert.That(result.ID, Is.EqualTo(obj.ID));
        }
    }

}
