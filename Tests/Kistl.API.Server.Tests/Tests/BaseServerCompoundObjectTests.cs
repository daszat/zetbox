using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API.Server.Mocks;

using NUnit.Framework;

namespace Kistl.API.Server.Tests
{
    [TestFixture]
    public class BaseServerCompoundObjectTests
    {
        private TestCompoundObject obj;

        [SetUp]
        public void SetUp()
        {
            obj = new TestCompoundObject() { TestInt = 1, TestString = "Hello World" };
        }

        [Test]
        public void Clone()
        {
            TestCompoundObject c = (TestCompoundObject)obj.Clone();
            Assert.That(c.TestInt, Is.EqualTo(obj.TestInt));
            Assert.That(c.TestString, Is.EqualTo(obj.TestString));
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToStream_Null()
        {
            obj.ToStream((BinaryWriter)null);
        }

        [Test]
        public void Stream()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            obj.ToStream(sw);

            Assert.That(ms.Length, Is.GreaterThan(0));

            ms.Seek(0, SeekOrigin.Begin);

            TestCompoundObject result = new TestCompoundObject();
            result.FromStream(sr);

            Assert.That(result.TestInt, Is.EqualTo(obj.TestInt));
            Assert.That(result.TestString, Is.EqualTo(obj.TestString));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader()
        {
            TestCompoundObject result = new TestCompoundObject();
            result.FromStream((BinaryReader)null);
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            bool hasChanged = false;
            bool hasChanging = false;

            PropertyChangedEventHandler changedHandler = new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e) { hasChanged = true; });
            PropertyChangingEventHandler changingHanlder = new PropertyChangingEventHandler(delegate(object sender, PropertyChangingEventArgs e) { hasChanging = true; });

            obj.PropertyChanged += changedHandler;
            obj.PropertyChanging += changingHanlder;

            obj.NotifyPropertyChanging("TestString", null, null);
            obj.TestString = "test";
            obj.NotifyPropertyChanged("TestString", null, null);

            Assert.That(hasChanged, Is.True);
            Assert.That(hasChanging, Is.True);

            obj.PropertyChanged -= changedHandler;
            obj.PropertyChanging -= changingHanlder;
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
            Assert.That(t, Is.EqualTo(new SerializableType(new InterfaceType(typeof(ICompoundObject)))));
        }


    }
}
