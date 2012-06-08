
namespace Zetbox.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Mocks;
    using Zetbox.API.Server.Mocks;
    using Zetbox.API.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class BaseServerDataObjectTests : AbstractApiServerTestFixture
    {
        private TestObjClassImpl obj;
        private IZetboxContext ctx;
        private InterfaceType.Factory _iftFactory;

        public override void SetUp()
        {
            base.SetUp();
            _iftFactory = scope.Resolve<InterfaceType.Factory>();
            ctx = GetContext();
            obj = new TestObjClassImpl();
            ctx.Attach(obj);
        }

        public override void TearDown()
        {
            ctx.Dispose();
            base.TearDown();
        }

        public void InitialiseObject(TestObjClassImpl objImpl)
        {

            objImpl.ID = TestObjClassSerializationMock.TestObjClassId;

            //objImpl.ObjectState = TestObjClassSerializationMock.TestObjectState;

            var baseClass = new TestObjClassImpl();
            baseClass.ID = TestObjClassSerializationMock.TestBaseClassId.Value;
            objImpl.BaseTestObjClass = baseClass;

            objImpl.StringProp = TestObjClassSerializationMock.TestStringPropValue;

            objImpl.SubClasses.Clear();
            foreach (var subClassId in TestObjClassSerializationMock.TestSubClassesIds)
            {
                var subClass = new TestObjClassImpl();
                subClass.ID = subClassId;
                objImpl.SubClasses.Add(subClass);
                objImpl.Context.Attach(subClass);
            }

            objImpl.TestEnumProp = TestEnum.TestSerializationValue;

            objImpl.TestNamesImpl.Clear();
            for (int i = 0; i < TestObjClassSerializationMock.TestTestNamesIds.Length; i++)
            {
                var ce = new TestObjClass_TestNameCollectionEntryImpl();
                ce.ID = TestObjClassSerializationMock.TestTestNamesIds[i];
                ce.Parent = objImpl;
                ce.Value = TestObjClassSerializationMock.TestTestNamesValues[i];

                objImpl.Context.Attach(ce);
                objImpl.TestNamesImpl.Add(ce);
            }

            objImpl.SetUnmodified();
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
            Assert.That(() => obj.ToStream((ZetboxStreamWriter)null, null, false), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void ToStream_creates_correct_Stream()
        {
            var typeMap = scope.Resolve<TypeMap>();
            var ms = new MemoryStream();
            var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms));
            var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms));

            InitialiseObject(obj);
            obj.ToStream(sw, null, false);

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            TestObjClassSerializationMock.AssertCorrectContents<TestObjClass, TestEnum>(sr, _iftFactory);
        }

        [Test]
        public void FromStream_creates_correct_Object()
        {
            var typeMap = scope.Resolve<TypeMap>();
            var ms = new MemoryStream();
            var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms));
            var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms));

            TestObjClassSerializationMock.ToStream<TestObjClass, TestEnum>(sw, _iftFactory);
            sw.Flush();

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            var t = sr.ReadSerializableType();
            Assert.That(t.GetSystemType().IsAssignableFrom(typeof(TestObjClass)), string.Format("{0} not assignable to {1}", typeof(TestObjClass), t));

            var obj = new TestObjClassImpl();
            obj.FromStream(sr);
            ctx.Attach(obj);

            TestObjClassSerializationMock.AssertCorrectContentsEnum<TestObjClass>(obj);
        }

        [Test]
        public void FromStream_Null_StreamReader_fails()
        {
            TestObjClass result = new TestObjClassImpl();
            Assert.That(() => result.FromStream((ZetboxStreamReader)null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void FromStream_Attached()
        {
            var typeMap = scope.Resolve<TypeMap>();
            var ms = new MemoryStream();
            var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms));
            var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms));

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
            var typeMap = scope.Resolve<TypeMap>();
            var ms = new MemoryStream();
            var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms));
            var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms));

            obj.ToStream(sw, null, false);
            ms.Seek(0, SeekOrigin.Begin);

            var t = sr.ReadSerializableType();
            Assert.That(t, Is.EqualTo(_iftFactory(typeof(TestObjClass)).ToSerializableType()));
        }
    }
}