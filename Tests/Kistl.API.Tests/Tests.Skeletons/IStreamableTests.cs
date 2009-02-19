using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.API.Tests.Skeletons
{

    public class IStreamableTests<T>
        where T : IStreamable, new()
    {

        protected T obj;

        [SetUp]
        public virtual void SetUp()
        {
            obj = new T();
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
        public void should_roundtrip_without_errors()
        {
            T result = SerializationRoundtrip(obj);
        }

        protected T SerializationRoundtrip(T obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            T result = new T();
            result.FromStream(sr);
            return result;
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FromStream_of_WrongType_fails()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            SerializableType wrongType = new SerializableType(typeof(string));
            BinarySerializer.ToStream(wrongType, sw);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            T result = new T();
            result.FromStream(sr);
        }

    }

}
