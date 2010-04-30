
namespace Kistl.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Kistl.API.Mocks;
    using Kistl.API.Server.Mocks;

    using NUnit.Framework;
    using Autofac;

    [TestFixture]
    public class BaseServerDataObjectTests : AbstractApiServerTestFixture
    {
        private TestObjClass__Implementation__ obj;
        private ITypeTransformations typeTrans;

        public override void SetUp()
        {
            base.SetUp();
            obj = new TestObjClass__Implementation__();
            typeTrans = scope.Resolve<ITypeTransformations>();
        }

        public void InitialiseObject(TestObjClass__Implementation__ objImpl)
        {

            objImpl.ID = TestObjClassSerializationMock.TestObjClassId;

            //objImpl.ObjectState = TestObjClassSerializationMock.TestObjectState;

            var baseClass = new TestObjClass__Implementation__();
            baseClass.ID = TestObjClassSerializationMock.TestBaseClassId.Value;
            objImpl.BaseTestObjClass = baseClass;

            objImpl.StringProp = TestObjClassSerializationMock.TestStringPropValue;

            objImpl.SubClasses.Clear();
            foreach (var subClassId in TestObjClassSerializationMock.TestSubClassesIds)
            {
                var subClass = new TestObjClass__Implementation__();
                subClass.ID = subClassId;
                objImpl.SubClasses.Add(subClass);
            }

            objImpl.TestEnumProp = TestEnum.TestSerializationValue;

            objImpl.TestNames__Implementation__.Clear();
            for (int i = 0; i < TestObjClassSerializationMock.TestTestNamesIds.Length; i++)
            {
                var ce = new TestObjClass_TestNameCollectionEntry__Implementation__();
                ce.ID = TestObjClassSerializationMock.TestTestNamesIds[i];
                ce.Parent = objImpl;
                ce.Value = TestObjClassSerializationMock.TestTestNamesValues[i];

                objImpl.TestNames__Implementation__.Add(ce);
            }
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            obj.NotifyPropertyChanging("StringProp", null, null);
            obj.StringProp = "test";
            obj.NotifyPropertyChanged("StringProp", null, null);
        }

        [Test]
        public void ToStream_to_null_fails()
        {
            Assert.That(() => obj.ToStream((BinaryWriter)null, null, false), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void ToStream_creates_correct_Stream()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            InitialiseObject(obj);
            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            TestObjClassSerializationMock.AssertCorrectContents<TestObjClass, TestEnum>(sr, typeTrans);
        }

        [Test]
        public void FromStream_creates_correct_Object()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms, UTF8Encoding.UTF8);
            BinaryReader sr = new BinaryReader(ms, UTF8Encoding.UTF8);

            TestObjClassSerializationMock.ToStream<TestObjClass, TestEnum>(sw, typeTrans);
            sw.Flush();

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            obj.FromStream(sr);

            TestObjClassSerializationMock.AssertCorrectContentsEnum<TestObjClass>(obj);
        }

        [Test]
        public void FromStream_Null_StreamReader_fails()
        {
            TestObjClass result = new TestObjClass__Implementation__();
            Assert.That(() => result.FromStream((BinaryReader)null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void FromStream_Attached()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            var ctxMock = GetContext();
            TestObjClass result = ctxMock.Create<TestObjClass>();
            Assert.That(result.IsAttached, Is.True);
            Assert.That(() => result.FromStream(sr), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void should_use_interfacetype_on_the_stream()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw, null, false);
            ms.Seek(0, SeekOrigin.Begin);

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);
            Assert.That(t, Is.EqualTo(typeTrans.AsInterfaceType(typeof(TestObjClass)).ToSerializableType()));
        }
    }
}