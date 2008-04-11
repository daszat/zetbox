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

        // [SetUp] Das geht irgenwie nicht - selber aufrufen!
        private void SetUp()
        {
            ms = new MemoryStream();
            sw = new BinaryWriter(ms);
            sr = new BinaryReader(ms);
        }

        [Test]
        public void Bool()
        {
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

            Kistl.App.Test.TestEnum toval, fromval;
            toval = Kistl.App.Test.TestEnum.First;
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            Enum tmp;
            BinarySerializer.FromBinary(out tmp, sr);
            fromval = (Kistl.App.Test.TestEnum)tmp;
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        public void Float()
        {
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

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
            SetUp();

            ObjectType toval, fromval;
            toval = new ObjectType(typeof(Kistl.App.Base.DataType));
            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr);
            Assert.That(fromval, Is.EqualTo(toval));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ObjectTypeNull()
        {
            SetUp();

            ObjectType toval;
            toval = null;
            BinarySerializer.ToBinary(toval, sw);
        }

        [Test]
        public void IEnumerable_IDataObject()
        {
            SetUp();

            List<Kistl.App.Test.TestObjClass> toval, fromval;
            toval = new List<Kistl.App.Test.TestObjClass>();
            toval.Add(new Kistl.App.Test.TestObjClass());

            BinarySerializer.ToBinary(toval.OfType<IDataObject>(), sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinary(out fromval, sr, null);
            Assert.That(fromval[0].ID, Is.EqualTo(toval[0].ID));
        }

        [Test]
        public void ICollection_ICollectionEntry()
        {
            SetUp();

            List<Kistl.App.Projekte.Kunde_EMailsCollectionEntry> toval, fromval;
            toval = new List<Kistl.App.Projekte.Kunde_EMailsCollectionEntry>();
            toval.Add(new Kistl.App.Projekte.Kunde_EMailsCollectionEntry());

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            BinarySerializer.FromBinaryCollectionEntries(out fromval, sr, null);
            Assert.That(fromval[0].ID, Is.EqualTo(toval[0].ID));
        }

        [Test]
        public void ICollection_ObservableCollection_ICollectionEntry()
        {
            SetUp();

            List<Kistl.App.Projekte.Kunde_EMailsCollectionEntry> toval;
            toval = new List<Kistl.App.Projekte.Kunde_EMailsCollectionEntry>();
            toval.Add(new Kistl.App.Projekte.Kunde_EMailsCollectionEntry());

            BinarySerializer.ToBinary(toval, sw);
            ms.Seek(0, SeekOrigin.Begin);

            ObservableCollection<Kistl.App.Projekte.Kunde_EMailsCollectionEntry> fromvalobserbable;
            BinarySerializer.FromBinaryCollectionEntries(out fromvalobserbable, sr, null);
            Assert.That(fromvalobserbable[0].ID, Is.EqualTo(toval[0].ID));
        }

        [Test]
        public void SerializableExpression()
        {
            SetUp();

            TestQuery<XMLObject> ctx = new TestQuery<XMLObject>();
            var list = from o in ctx
                       where o.IntProperty == 1
                       && o.IntProperty != 2
                       && o.IntProperty > 3
                       && o.IntProperty == ms.Length
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
