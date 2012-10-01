// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Tests.BinarySerializers
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
        private ZetboxStreamReader.Factory readerFactory;
        private ZetboxStreamWriter.Factory writerFactory;

        private MemoryStream stream;
        private ZetboxStreamReader reader;
        private ZetboxStreamWriter writer;

        public override void SetUp()
        {
            base.SetUp();
            readerFactory = scope.Resolve<ZetboxStreamReader.Factory>();
            writerFactory = scope.Resolve<ZetboxStreamWriter.Factory>();

            stream = new MemoryStream();
            reader = readerFactory.Invoke(new BinaryReader(stream));
            writer = writerFactory.Invoke(new BinaryWriter(stream));
        }

        private void ResetStream()
        {
            // Use this to dump out the transferred bytes
            Console.WriteLine("new byte[] { " + string.Join(", ", stream.ToArray().Select(b => b.ToString()).ToArray()) + " };");

            stream.Seek(0, SeekOrigin.Begin);
        }

        private const double dblTest = 0.5;
        private readonly byte[] dblExpected = new byte[] { 0, 0, 0, 0, 0, 0, 224, 63 };
        private readonly byte[] serializableTypeStringExpected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 13, 83, 121, 115, 116, 101, 109, 46, 83, 116, 114, 105, 110, 103, 1, 90, 83, 121, 115, 116, 101, 109, 46, 83, 116, 114, 105, 110, 103, 44, 32, 109, 115, 99, 111, 114, 108, 105, 98, 44, 32, 86, 101, 114, 115, 105, 111, 110, 61, 50, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117, 114, 101, 61, 110, 101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121, 84, 111, 107, 101, 110, 61, 98, 55, 55, 97, 53, 99, 53, 54, 49, 57, 51, 52, 101, 48, 56, 57, 1, 0, 0, 0, 0 };
        private readonly byte[] serializableTypeIListStringExpected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 34, 83, 121, 115, 116, 101, 109, 46, 67, 111, 108, 108, 101, 99, 116, 105, 111, 110, 115, 46, 71, 101, 110, 101, 114, 105, 99, 46, 73, 76, 105, 115, 116, 96, 49, 1, 111, 83, 121, 115, 116, 101, 109, 46, 67, 111, 108, 108, 101, 99, 116, 105, 111, 110, 115, 46, 71, 101, 110, 101, 114, 105, 99, 46, 73, 76, 105, 115, 116, 96, 49, 44, 32, 109, 115, 99, 111, 114, 108, 105, 98, 44, 32, 86, 101, 114, 115, 105, 111, 110, 61, 52, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117, 114, 101, 61, 110, 101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121, 84, 111, 107, 101, 110, 61, 98, 55, 55, 97, 53, 99, 53, 54, 49, 57, 51, 52, 101, 48, 56, 57, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 13, 83, 121, 115, 116, 101, 109, 46, 83, 116, 114, 105, 110, 103, 1, 90, 83, 121, 115, 116, 101, 109, 46, 83, 116, 114, 105, 110, 103, 44, 32, 109, 115, 99, 111, 114, 108, 105, 98, 44, 32, 86, 101, 114, 115, 105, 111, 110, 61, 52, 46, 48, 46, 48, 46, 48, 44, 32, 67, 117, 108, 116, 117, 114, 101, 61, 110, 101, 117, 116, 114, 97, 108, 44, 32, 80, 117, 98, 108, 105, 99, 75, 101, 121, 84, 111, 107, 101, 110, 61, 98, 55, 55, 97, 53, 99, 53, 54, 49, 57, 51, 52, 101, 48, 56, 57, 1, 0, 0, 0, 0 };
        private readonly byte[] binFormatterStringArrayExpected = new byte[] { 0, 1, 0, 0, 0, 255, 255, 255, 255, 1, 0, 0, 0, 0, 0, 0, 0, 17, 1, 0, 0, 0, 2, 0, 0, 0, 6, 2, 0, 0, 0, 1, 97, 6, 3, 0, 0, 0, 1, 98, 11 };

        [Test]
        public void should_write_double()
        {
            writer.Write(dblTest);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(dblExpected));
        }

        [Test]
        public void should_write_SerializableType_string()
        {
            var st = iftFactory(typeof(string)).ToSerializableType();
            writer.Write(st);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(serializableTypeStringExpected));
        }

        [Test]
        public void should_write_SerializableType_IListString()
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
            writer.WriteRaw(serializableTypeStringExpected);
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(serializableTypeStringExpected));

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

        [Test]
        [Ignore("hangs on minimal differences to mono")]
        public void basic_BinaryFormatter_write_string_array()
        {
            BinaryWrite(new string[] { "a", "b" });
            ResetStream();
            Assert.That(stream.ToArray(), Is.EqualTo(binFormatterStringArrayExpected));
        }

        [Test]
        [Ignore("hangs on minimal differences to mono")]
        public void basic_BinaryFormatter_read_string_array()
        {
            stream.Write(binFormatterStringArrayExpected, 0, binFormatterStringArrayExpected.Length);
            ResetStream();

            var result = BinaryRead();
            Assert.That(result, Is.EqualTo(new string[] { "a", "b" }));
        }

        private void BinaryWrite(object stuff)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, stuff);
        }

        private object BinaryRead()
        {
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }
    }
}
