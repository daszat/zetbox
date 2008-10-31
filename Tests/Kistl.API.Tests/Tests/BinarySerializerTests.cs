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
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void BoolNull()
        {
            bool? toval, fromval;
            toval = null;

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void BoolNullValue()
        {
            bool? toval, fromval;
            toval = true;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Ignore("This needs quite a bit of these tests yet")]
        public void BoolNullStream()
        {
            BinarySerializer.ToBinary(true, null);
        }

        [Test]
        public void DateTime()
        {
            DateTime toval, fromval;
            toval = System.DateTime.Now;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DateTimeNull()
        {
            DateTime? toval, fromval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DateTimeNullValue()
        {
            DateTime? toval, fromval;
            toval = System.DateTime.Now;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void Int()
        {
            int toval, fromval;
            toval = 23;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void IntNull()
        {
            int? toval, fromval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void IntNullValue()
        {
            int? toval, fromval;
            toval = 24;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }


        [Test]
        public void Enum()
        {
            TestEnum toval, fromval;
            toval = TestEnum.First;
            BinarySerializer.ToBinary((int)toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            int tmp;
            BinarySerializer.FromBinary(out tmp, sr);
            fromval = (TestEnum)tmp;
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void Float()
        {
            float toval, fromval;
            toval = 23.0f;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void FloatNull()
        {
            float? toval, fromval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void FloatNullValue()
        {
            float? toval, fromval;
            toval = 24.0f;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }


        [Test]
        public void Double()
        {
            double toval, fromval;
            toval = 23.0;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DoubleNull()
        {
            double? toval, fromval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void DoubleNullValue()
        {
            double? toval, fromval;
            toval = 24.0;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }


        [Test]
        public void String()
        {
            string toval, fromval;
            toval = "Hello World!";
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void StringNull()
        {
            string toval, fromval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void SerializableType()
        {
            SerializableType toval, fromval;
            toval = new SerializableType(typeof(TestDataObject));
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SerializableTypeNull()
        {
            SerializableType toval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SerializableType_FromBinary_NullStream()
        {
            SerializableType fromval;
            BinarySerializer.FromBinary(out fromval, null);
        }

        [Test]
        public void IEnumerable_IDataObject()
        {
            List<TestDataObject> toval, fromval;
            toval = new List<TestDataObject>();
            toval.Add(new TestDataObject__Implementation__());

            BinarySerializer.ToBinary(toval.Cast<IDataObject>(), sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void ICollection_IDataObject()
        {
            List<TestDataObject> toval, fromval;
            toval = new List<TestDataObject>();
            toval.Add(new TestDataObject__Implementation__());

            BinarySerializer.ToBinary(toval.Cast<IDataObject>(), sw);
            ms.Seek(0, SeekOrigin.Begin);

            fromval = new List<TestDataObject>();
            BinarySerializer.FromBinary(fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void ICollection_ICollectionEntry()
        {
            List<TestCollectionEntry> toval, fromval;
            toval = new List<TestCollectionEntry>();
            toval.Add(new TestCollectionEntry() { ID = 10, TestName = "Test" });

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            fromval = new List<TestCollectionEntry>();
            BinarySerializer.FromBinaryCollectionEntries(fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ICollection_ICollectionEntryNull()
        {
            BinarySerializer.FromBinaryCollectionEntries<TestCollectionEntry>(null, sr);
        }

        [Test]
        public void ICollection_ObservableCollection_ICollectionEntry()
        {
            List<TestCollectionEntry> toval;
            toval = new List<TestCollectionEntry>();
            toval.Add(new TestCollectionEntry() { ID = 20, TestName = "Hello" });

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            ObservableCollection<TestCollectionEntry> fromvalobserbable = new ObservableCollection<TestCollectionEntry>();
            BinarySerializer.FromBinaryCollectionEntries(fromvalobserbable, sr);
            Assert.That(fromvalobserbable[0].ID, Is.EqualTo(toval[0].ID));
        }

        [Test]
        public void IStruct()
        {
            TestStruct toval = new TestStruct() { ID = 1 };

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            TestStruct fromval;
            BinarySerializer.FromBinary<TestStruct>(out fromval, sr);
            Assert.That(fromval.ID, Is.EqualTo(fromval.ID));
        }

        [Test]
        public void IStructNull()
        {
            TestStruct toval = null;

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            TestStruct fromval;
            BinarySerializer.FromBinary<TestStruct>(out fromval, sr);
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
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval.NodeType, Is.EqualTo(toval.NodeType));
            Assert.That(list.Expression.NodeType, Is.EqualTo(fromval.ToExpression().NodeType));
        }
    }
}
