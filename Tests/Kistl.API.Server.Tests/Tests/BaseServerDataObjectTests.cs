using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API.Mocks;
using Kistl.API.Server.Mocks;

using NUnit.Framework;

namespace Kistl.API.Server.Tests
{
    [TestFixture]
    public class BaseServerDataObjectTests
    {
        private TestObjClass__Implementation__ obj;
        private CustomActionsManagerAPITest currentCustomActionsManager;

        [SetUp]
        public void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            currentCustomActionsManager = (CustomActionsManagerAPITest)ApplicationContext.Current.CustomActionsManager;
            currentCustomActionsManager.Reset();

            obj = new TestObjClass__Implementation__();
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
                ce.A = objImpl;
                ce.B = TestObjClassSerializationMock.TestTestNamesValues[i];

                objImpl.TestNames__Implementation__.Add(ce);
            }
        }

        [Test]
        public void should_have_events_attached_after_init()
        {
            Assert.That(currentCustomActionsManager.IsObjectAttached(obj), Is.True);
        }

        [Test]
        public void ObjectState_should_be_new_after_init()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        // TODO: WTF? Please explain
        [Test]
        public void ObjectState_should_be_Unmodified_after_setting_ID()
        {
            obj.ID = 10;
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        /// ObjectState is just for serialization....
        public void ObjectState_CreatedObject_Modified()
        {
            obj.NotifyPropertyChanged("test");
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        /// ObjectState is just for serialization....
        public void ObjectState_ObjectWithID_Modified()
        {
            ((TestObjClass__Implementation__)obj).ID = 10;
            obj.NotifyPropertyChanged("test");
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        /// ObjectState is just for serialization....
        public void ObjectState_New_then_UnModified()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            ((TestObjClass__Implementation__)obj).ID = 10;
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void NotifyChange()
        {
            obj.NotifyChange();
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            obj.NotifyPropertyChanging("StringProp");
            obj.StringProp = "test";
            obj.NotifyPropertyChanged("StringProp");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToStream_to_null_fails()
        {
            obj.ToStream(null);
        }

        [Test]
        public void ToStream_creates_correct_Stream()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            InitialiseObject(obj);
            obj.ToStream(sw);

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            TestObjClassSerializationMock.AssertCorrectContents<TestObjClass, TestEnum>(sr);
        }

        [Test]
        public void FromStream_creates_correct_Object()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms, UTF8Encoding.UTF8);
            BinaryReader sr = new BinaryReader(ms, UTF8Encoding.UTF8);

            TestObjClassSerializationMock.ToStream<TestObjClass, TestEnum>(sw);
            sw.Flush();

            Assert.That(ms.Length, Is.GreaterThan(0));
            ms.Seek(0, SeekOrigin.Begin);

            obj.FromStream(sr);

            TestObjClassSerializationMock.AssertCorrectContentsEnum<TestObjClass>(obj);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader_fails()
        {
            using (IKistlContext ctx = Kistl.API.Server.KistlContext.GetContext())
            {
                TestObjClass result = new TestObjClass__Implementation__();
                result.FromStream(null);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FromStream_WrongType()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            SerializableType wrongType = new SerializableType(new InterfaceType(typeof(string)));
            BinarySerializer.ToStream(wrongType, sw);

            using (IKistlContext ctx = Kistl.API.Server.KistlContext.GetContext())
            {
                ms.Seek(0, SeekOrigin.Begin);
                TestObjClass result = new TestObjClass__Implementation__();
                result.FromStream(sr);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FromStream_Attached()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            using (IKistlContext ctx = Kistl.API.Server.KistlContext.GetContext())
            {
                TestObjClass result = ctx.Create<TestObjClass>();
                result.FromStream(sr);

                Assert.That(result.GetType(), Is.EqualTo(obj.GetType()));
                Assert.That(result.ID, Is.EqualTo(obj.ID));
                Assert.That(result.ObjectState, Is.EqualTo(obj.ObjectState));
            }
        }

        [Test]
        public void should_use_interfacetype_on_the_stream()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw);
            ms.Seek(0, SeekOrigin.Begin);

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);
            Assert.That(t, Is.EqualTo(new SerializableType(new InterfaceType(typeof(TestObjClass)))));
        }
	
    }
}