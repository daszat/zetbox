using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Tests.Skeletons
{

    public class IStreamableTests<T> : AbstractApiTestFixture
        where T : IStreamable, new()
    {

        protected T obj;

        public override void SetUp()
        {
            base.SetUp();
            obj = new T();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_to_null_stream()
        {
            obj.ToStream((BinaryWriter)null, null, false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_from_null_stream()
        {
            obj.FromStream((BinaryReader)null);
        }

        [Test]
        public void should_roundtrip_without_errors()
        {
            T result = SerializationRoundtrip(obj);
        }

        protected T SerializationRoundtrip(T obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            if (!typeof(T).IsICompoundObject())
            {
                SerializableType t;
                BinarySerializer.FromStream(out t, sr);
            }

            T result = new T();
            result.FromStream(sr);
            return result;
        }
    }

}
