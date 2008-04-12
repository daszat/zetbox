using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.IO;
using System.Linq.Expressions;
using System.Collections.ObjectModel;


namespace API.Tests.Tests
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
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            Enum tmp;
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
        public void ObjectType()
        {
            ObjectType toval, fromval;
            toval = new ObjectType(typeof(TestDataObject));
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ObjectTypeNull()
        {
            ObjectType toval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
        }

        [Test]
        public void IEnumerable_IDataObject()
        {
            List<TestDataObject> toval, fromval;
            toval = new List<TestDataObject>();
            toval.Add(new TestDataObject());

            BinarySerializer.ToBinary(toval.OfType<IDataObject>(), sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr, null);
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

            BinarySerializer.FromBinaryCollectionEntries(out fromval, sr, null);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void ICollection_ObservableCollection_ICollectionEntry()
        {
            List<TestCollectionEntry> toval;
            toval = new List<TestCollectionEntry>();
            toval.Add(new TestCollectionEntry() { ID = 20, TestName = "Hello" });

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            ObservableCollection<TestCollectionEntry> fromvalobserbable;
            BinarySerializer.FromBinaryCollectionEntries(out fromvalobserbable, sr, null);
            Assert.That(fromvalobserbable[0].ID, Is.EqualTo(toval[0].ID));
        }

        [Test]
        public void SerializableExpression()
        {
            TestDataObject obj = new TestDataObject();
            TestQuery<TestDataObject> ctx = new TestQuery<TestDataObject>();
            var list = from o in ctx
                       where o.IntProperty == 1
                       && o.IntProperty != 2
                       && o.IntProperty > 3
                       && o.IntProperty == obj.ID
                       && o.StringProperty == obj.TestField
                       && o.StringProperty == obj.StringProperty
                       && o.StringProperty.StartsWith(obj.TestField)
                       && (o.StringProperty.StartsWith("test")
                            || o.StringProperty == "test")
                       && !o.BoolProperty
                       select new { o.IntProperty, o.BoolProperty };

            SerializableExpression toval, fromval;
            toval = Kistl.API.SerializableExpression.FromExpression(list.Expression, SerializableType.SerializeDirection.ClientToServer);
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval.NodeType, Is.EqualTo(toval.NodeType));
            Assert.That(list.Expression.NodeType, Is.EqualTo(fromval.ToExpression().NodeType));
        }
    }
}
