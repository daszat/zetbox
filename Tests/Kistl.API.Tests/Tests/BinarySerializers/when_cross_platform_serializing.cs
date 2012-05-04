
namespace Kistl.API.Tests.BinarySerializers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Autofac;
    using NUnit.Framework;

    [TestFixture]
    public class when_cross_platform_serializing : AbstractApiTestFixture
    {
        private KistlStreamReader.Factory readerFactory;
        private KistlStreamWriter.Factory writerFactory;

        private MemoryStream stream;
        private KistlStreamReader reader;
        private KistlStreamWriter writer;

        public override void SetUp()
        {
            base.SetUp();
            readerFactory = scope.Resolve<KistlStreamReader.Factory>();
            writerFactory = scope.Resolve<KistlStreamWriter.Factory>();

            stream = new MemoryStream();
            reader = readerFactory.Invoke(new BinaryReader(stream));
            writer = writerFactory.Invoke(new BinaryWriter(stream));
        }

        private void ResetStream()
        {
            // Use this to dump out the transferred bytes
            Console.WriteLine("new byte[] { " + string.Join(", ", stream.ToArray().Select(b => b.ToString()).ToArray()) + " }");

            stream.Seek(0, SeekOrigin.Begin);
        }

        private const double dblTest = 0.5;
        private readonly byte[] dblExpected = new byte[] { 0, 0, 0, 0, 0, 0, 224, 63 };
        private readonly byte[] serializableTypeExpected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 
            255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 12, 2, 0, 0, 0, 76, 75, 105, 115, 116, 108, 46, 65, 80, 73, 44, 32, 86, 101, 114,
            115, 105, 111, 110, 61, 49, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117, 114, 101, 61, 110, 101, 117, 116, 114, 97,
            108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121, 84, 111, 107, 101, 110, 61, 99, 99, 100, 102, 49, 54, 101, 52, 100, 100,
            55, 98, 54, 100, 55, 56, 5, 1, 0, 0, 0, 26, 75, 105, 115, 116, 108, 46, 65, 80, 73, 46, 83, 101, 114, 105, 97, 108, 105, 122,
            97, 98, 108, 101, 84, 121, 112, 101, 3, 0, 0, 0, 25, 60, 84, 121, 112, 101, 78, 97, 109, 101, 62, 107, 95, 95, 66, 97, 99, 107,
            105, 110, 103, 70, 105, 101, 108, 100, 38, 60, 65, 115, 115, 101, 109, 98, 108, 121, 81, 117, 97, 108, 105, 102, 105, 101, 100,
            78, 97, 109, 101, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 37, 60, 71, 101, 110, 101, 114, 105,
            99, 84, 121, 112, 101, 80, 97, 114, 97, 109, 101, 116, 101, 114, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101,
            108, 100, 1, 1, 4, 28, 75, 105, 115, 116, 108, 46, 65, 80, 73, 46, 83, 101, 114, 105, 97, 108, 105, 122, 97, 98, 108, 101, 84,
            121, 112, 101, 91, 93, 2, 0, 0, 0, 2, 0, 0, 0, 6, 3, 0, 0, 0, 13, 83, 121, 115, 116, 101, 109, 46, 83, 116, 114, 105, 110, 103,
            6, 4, 0, 0, 0, 90, 83, 121, 115, 116, 101, 109, 46, 83, 116, 114, 105, 110, 103, 44, 32, 109, 115, 99, 111, 114, 108, 105, 98,
            44, 32, 86, 101, 114, 115, 105, 111, 110, 61, 50, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117, 114, 101, 61, 110,
            101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121, 84, 111, 107, 101, 110, 61, 98, 55, 55, 97, 53,
            99, 53, 54, 49, 57, 51, 52, 101, 48, 56, 57, 9, 5, 0, 0, 0, 7, 5, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 4, 26, 75, 105, 115, 116,
            108, 46, 65, 80, 73, 46, 83, 101, 114, 105, 97, 108, 105, 122, 97, 98, 108, 101, 84, 121, 112, 101, 2, 0, 0, 0, 11 };
        private readonly byte[] serializableTypeIListStringExpected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0,
            0, 0, 255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 12, 2, 0, 0, 0, 76, 75, 105, 115, 116, 108, 46, 65, 80, 73, 44, 32, 86, 101,
            114, 115, 105, 111, 110, 61, 49, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117, 114, 101, 61, 110, 101, 117, 116, 114,
            97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121, 84, 111, 107, 101, 110, 61, 99, 99, 100, 102, 49, 54, 101, 52, 100,
            100, 55, 98, 54, 100, 55, 56, 5, 1, 0, 0, 0, 26, 75, 105, 115, 116, 108, 46, 65, 80, 73, 46, 83, 101, 114, 105, 97, 108, 105,
            122, 97, 98, 108, 101, 84, 121, 112, 101, 3, 0, 0, 0, 25, 60, 84, 121, 112, 101, 78, 97, 109, 101, 62, 107, 95, 95, 66, 97, 99,
            107, 105, 110, 103, 70, 105, 101, 108, 100, 38, 60, 65, 115, 115, 101, 109, 98, 108, 121, 81, 117, 97, 108, 105, 102, 105, 101,
            100, 78, 97, 109, 101, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105, 101, 108, 100, 37, 60, 71, 101, 110, 101, 114,
            105, 99, 84, 121, 112, 101, 80, 97, 114, 97, 109, 101, 116, 101, 114, 62, 107, 95, 95, 66, 97, 99, 107, 105, 110, 103, 70, 105,
            101, 108, 100, 1, 1, 4, 28, 75, 105, 115, 116, 108, 46, 65, 80, 73, 46, 83, 101, 114, 105, 97, 108, 105, 122, 97, 98, 108, 101,
            84, 121, 112, 101, 91, 93, 2, 0, 0, 0, 2, 0, 0, 0, 6, 3, 0, 0, 0, 34, 83, 121, 115, 116, 101, 109, 46, 67, 111, 108, 108, 101,
            99, 116, 105, 111, 110, 115, 46, 71, 101, 110, 101, 114, 105, 99, 46, 73, 76, 105, 115, 116, 96, 49, 6, 4, 0, 0, 0, 111, 83,
            121, 115, 116, 101, 109, 46, 67, 111, 108, 108, 101, 99, 116, 105, 111, 110, 115, 46, 71, 101, 110, 101, 114, 105, 99, 46, 73,
            76, 105, 115, 116, 96, 49, 44, 32, 109, 115, 99, 111, 114, 108, 105, 98, 44, 32, 86, 101, 114, 115, 105, 111, 110, 61, 50, 46,
            48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117, 114, 101, 61, 110, 101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108,
            105, 99, 75, 101, 121, 84, 111, 107, 101, 110, 61, 98, 55, 55, 97, 53, 99, 53, 54, 49, 57, 51, 52, 101, 48, 56, 57, 9, 5, 0, 0,
            0, 7, 5, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 4, 26, 75, 105, 115, 116, 108, 46, 65, 80, 73, 46, 83, 101, 114, 105, 97, 108, 105,
            122, 97, 98, 108, 101, 84, 121, 112, 101, 2, 0, 0, 0, 9, 6, 0, 0, 0, 1, 6, 0, 0, 0, 1, 0, 0, 0, 6, 7, 0, 0, 0, 13, 83, 121, 115,
            116, 101, 109, 46, 83, 116, 114, 105, 110, 103, 6, 8, 0, 0, 0, 90, 83, 121, 115, 116, 101, 109, 46, 83, 116, 114, 105, 110, 103,
            44, 32, 109, 115, 99, 111, 114, 108, 105, 98, 44, 32, 86, 101, 114, 115, 105, 111, 110, 61, 50, 46, 48, 46, 48, 46, 48, 44, 32,
            67, 117, 108, 116, 117, 114, 101, 61, 110, 101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121, 84, 111,
            107, 101, 110, 61, 98, 55, 55, 97, 53, 99, 53, 54, 49, 57, 51, 52, 101, 48, 56, 57, 9, 9, 0, 0, 0, 7, 9, 0, 0, 0, 0, 1, 0, 0, 0,
            0, 0, 0, 0, 4, 26, 75, 105, 115, 116, 108, 46, 65, 80, 73, 46, 83, 101, 114, 105, 97, 108, 105, 122, 97, 98, 108, 101, 84, 121,
            112, 101, 2, 0, 0, 0, 11 };

        [Test]
        public void should_transfer_double()
        {
            writer.Write(dblTest);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(dblExpected));
        }

        [Test]
        public void should_transfer_SerializableType_string()
        {
            var st = iftFactory(typeof(string)).ToSerializableType();
            writer.Write(st);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(serializableTypeExpected));
        }

        [Test]
        public void should_transfer_SerializableType_IListString()
        {
            var st = iftFactory(typeof(IList<string>)).ToSerializableType();
            writer.Write(st);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(serializableTypeIListStringExpected));
        }

        [Test]
        public void should_read_double()
        {
            writer.WriteRaw(dblExpected);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(dblExpected));
            var result = reader.ReadDouble();
            Assert.That(result, Is.EqualTo(dblTest));
        }

        [Test]
        public void should_read_SerializableType_string()
        {
            writer.WriteRaw(serializableTypeExpected);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(serializableTypeExpected));

            var st = iftFactory(typeof(string)).ToSerializableType();
            var result = reader.ReadSerializableType();
            Assert.That(result, Is.EqualTo(st));
        }

        [Test]
        public void should_read_SerializableType_IListString()
        {
            writer.WriteRaw(serializableTypeIListStringExpected);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(serializableTypeIListStringExpected));

            var st = iftFactory(typeof(IList<string>)).ToSerializableType();
            var result = reader.ReadSerializableType();
            Assert.That(result, Is.EqualTo(st));
        }

        private readonly byte[] binFormatterExpected = new byte[] { 0, 1, 0, 0, 0, 255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 17, 1, 0, 0, 0, 2, 0, 0, 0, 6, 2, 0, 0, 0, 1, 97, 6, 3, 0, 0, 0, 1, 98, 11 };

        [Test]
        public void basic_BinaryFormatter_write()
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, new string[] { "a", "b" });
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(binFormatterExpected));
        }

        [Test]
        public void basic_BinaryFormatter_read()
        {
            stream.Write(binFormatterExpected, 0, binFormatterExpected.Length);
            ResetStream();

            BinaryFormatter bf = new BinaryFormatter();
            var result = bf.Deserialize(stream);
            Assert.That(result, Is.EqualTo(new string[] { "a", "b" }));
        }
    }
}
