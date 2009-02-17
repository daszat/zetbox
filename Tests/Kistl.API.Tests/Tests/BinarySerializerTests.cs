using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;


namespace Kistl.API.Tests
{
    [TestFixture]
    public class BinarySerializerTests
    {
        MemoryStream ms;
        BinaryWriter sw;
        BinaryReader sr;

        [SetUp]
        public void SetUp()
        {
            ms = new MemoryStream();
            sw = new BinaryWriter(ms);
            sr = new BinaryReader(ms);
            var testCtx = new TestApplicationContext();
        }

        [Test]
        public void Bool()
        {
            bool toval, fromval;
            toval = true;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void BoolNull()
        {
            bool? toval, fromval;
            toval = null;

            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void BoolNullValue()
        {
            bool? toval, fromval;
            toval = true;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Ignore("This needs quite a bit of these tests yet")]
        public void BoolNullStream()
        {
            BinarySerializer.ToStream(true, null);
        }

        [Test]
        public void DateTime()
        {
            DateTime toval, fromval;
            toval = System.DateTime.Now;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DateTimeNull()
        {
            DateTime? toval, fromval;
            toval = null;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DateTimeNullValue()
        {
            DateTime? toval, fromval;
            toval = System.DateTime.Now;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void Int()
        {
            int toval, fromval;
            toval = 23;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void IntNull()
        {
            int? toval, fromval;
            toval = null;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void IntNullValue()
        {
            int? toval, fromval;
            toval = 24;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }


        [Test]
        public void Enum()
        {
            TestEnum toval, fromval;
            toval = TestEnum.First;
            BinarySerializer.ToStream((int)toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            int tmp;
            BinarySerializer.FromStream(out tmp, sr);
            fromval = (TestEnum)tmp;
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void Float()
        {
            float toval, fromval;
            toval = 23.0f;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void FloatNull()
        {
            float? toval, fromval;
            toval = null;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void FloatNullValue()
        {
            float? toval, fromval;
            toval = 24.0f;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }


        [Test]
        public void Double()
        {
            double toval, fromval;
            toval = 23.0;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DoubleNull()
        {
            double? toval, fromval;
            toval = null;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DoubleNullValue()
        {
            double? toval, fromval;
            toval = 24.0;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }


        [Test]
        public void Strings_roundtrip_correctly()
        {
            foreach (var testValue in new[] { "Hello World!", "stringprop testvalue" })
            {
                TestStringValue(testValue);
                // reset streams
                SetUp();
            }
        }

        private void TestStringValue(string toval)
        {
            uint guardValue = 0xdeadbeef;
            sw.Write(guardValue);
            BinarySerializer.ToStream(toval, sw);
            sw.Write(guardValue);
            ms.Seek(0, SeekOrigin.Begin);

            string fromval;
            Assert.That(sr.ReadUInt32(), Is.EqualTo(guardValue), "inital guard wrong");
            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(sr.ReadUInt32(), Is.EqualTo(guardValue), "final guard wrong");
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void null_String_roundtrips_correctly()
        {
            string toval, fromval;
            toval = null;
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void SerializableType_roundtrips_correctly()
        {
            uint guardValue = 0xdeadbeef;

            SerializableType toval, fromval;
            toval = new SerializableType(typeof(TestDataObject));

            sw.Write(guardValue);
            BinarySerializer.ToStream(toval, sw);
            sw.Write(guardValue);
            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);

            Assert.That(sr.ReadUInt32(), Is.EqualTo(guardValue), "inital guard wrong");
            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(sr.ReadUInt32(), Is.EqualTo(guardValue), "final guard wrong");

            Assert.That(fromval, Is.EqualTo(toval));
            Assert.That(ms.Position, Is.EqualTo(ms.Length), "stream not read completely");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SerializableTypeNull()
        {
            SerializableType toval;
            toval = null;
            BinarySerializer.ToStream(toval, sw);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SerializableType_FromStream_NullStream()
        {
            SerializableType fromval;
            BinarySerializer.FromStream(out fromval, null);
        }

        #region Collection Entries

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void cannot_deserialize_collection_entries_to_null_collection()
        {
            BinarySerializer.FromStreamCollectionEntries<TestCollectionEntry>(null, sr);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void cannot_deserialize_collection_entries_from_null_stream()
        {
            var collection = new List<TestCollectionEntry>();
            BinarySerializer.FromStreamCollectionEntries(collection, null);
        }

        [Test]
        public void can_roundtrip_list_with_no_entry()
        {
            TestToFromStreamCollectionEntries(new List<TestCollectionEntry>());
        }

        [Test]
        public void can_roundtrip_list_with_single_entry()
        {
            TestToFromStreamCollectionEntries(new List<TestCollectionEntry>(){
                new TestCollectionEntry() { ID = 10, TestName = "Test" }
            });
        }

        [Test]
        public void can_roundtrip_list_with_multiple_entries()
        {
            TestToFromStreamCollectionEntries(new List<TestCollectionEntry>(){
                new TestCollectionEntry() { ID = 10, TestName = "Test1" },
                new TestCollectionEntry() { ID = 20, TestName = "Test2" },
                new TestCollectionEntry() { ID = 30, TestName = "Test3" },
                new TestCollectionEntry() { ID = 40, TestName = "Test4" }
            });
        }

        private void TestToFromStreamCollectionEntries(List<TestCollectionEntry> toval)
        {
            List<TestCollectionEntry> fromval;
            BinarySerializer.ToStreamCollectionEntries(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            fromval = new List<TestCollectionEntry>();
            BinarySerializer.FromStreamCollectionEntries(fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }


        [Test]
        public void can_roundtrip_observablelist()
        {
            List<TestCollectionEntry> toval;
            toval = new List<TestCollectionEntry>();
            toval.Add(new TestCollectionEntry() { ID = 20, TestName = "Hello" });

            BinarySerializer.ToStreamCollectionEntries(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            ObservableCollection<TestCollectionEntry> fromvalobserbable = new ObservableCollection<TestCollectionEntry>();
            BinarySerializer.FromStreamCollectionEntries(fromvalobserbable, sr);
            Assert.That(toval, Is.EqualTo(fromvalobserbable));
        }

        #endregion

        [Test]
        public void IStruct()
        {
            TestStruct toval = new TestStruct() { ID = 1 };

            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            TestStruct fromval;
            BinarySerializer.FromStream<TestStruct>(out fromval, sr);
            Assert.That(fromval.ID, Is.EqualTo(fromval.ID));
        }

        [Test]
        public void IStructNull()
        {
            TestStruct toval = null;

            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            TestStruct fromval;
            BinarySerializer.FromStream<TestStruct>(out fromval, sr);
            Assert.That(fromval, Is.Null);
        }

        /// <summary>
        /// Test just serialization, not handling interfaces!
        /// </summary>
        [Test]
        public void SerializableExpression()
        {
            TestDataObject obj = new TestDataObject__Implementation__();
            TestObj obj2 = new TestObj();
            TestQuery<TestDataObject__Implementation__> ctx = new TestQuery<TestDataObject__Implementation__>();
            var list = from o in ctx
                       where o.IntProperty == 1
                       && o.IntProperty != 2
                       && o.IntProperty > 3
                       && o.IntProperty == obj.ID
                       && o.StringProperty == obj2.TestField
                       && o.StringProperty == obj.StringProperty
                       && o.StringProperty.StartsWith(obj2.TestField)
                       && (o.StringProperty.StartsWith("test")
                            || o.StringProperty == "test")
                       && !o.BoolProperty
                       select new { o.IntProperty, o.BoolProperty };

            SerializableExpression toval, fromval;
            toval = Kistl.API.SerializableExpression.FromExpression(list.Expression);
            BinarySerializer.ToStream(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromStream(out fromval, sr);
            Assert.That(fromval.NodeType, Is.EqualTo(toval.NodeType));
            Assert.That(list.Expression.NodeType, Is.EqualTo(fromval.ToExpression().NodeType));
        }
    }
}
