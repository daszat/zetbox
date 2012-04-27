
namespace Kistl.API.Tests.Skeletons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Utils;
    using NUnit.Framework;

    public class IStreamableTests<T> : SerializerTestFixture
        where T : IStreamable, new()
    {
        protected T obj;

        public override void SetUp()
        {
            base.SetUp();
            obj = CreateObjectInstance();
        }

        protected T CreateObjectInstance()
        {
            return new T();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_to_null_stream()
        {
            obj.ToStream((KistlStreamWriter)null, null, false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void fails_on_serializing_from_null_stream()
        {
            obj.FromStream((KistlStreamReader)null);
        }

        [Test]
        public void should_roundtrip_without_errors()
        {
            SerializationRoundtrip(obj);
        }

        protected T SerializationRoundtrip(T obj)
        {
            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            if (!typeof(T).IsICompoundObject())
            {
                var t = sr.ReadSerializableType();
                Assert.That(typeof(T).IsAssignableFrom(t.GetType()), string.Format("{0} not assignable to {1}", t, typeof(T)));
            }

            T result = new T();
            result.FromStream(sr);
            return result;
        }
    }
}
