using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Server.Mocks;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.API.Server.Tests
{
    [TestFixture]
    public class BaseServerStructObjectTests
    {
        private TestStruct obj;

        [SetUp]
        public void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            obj = new TestStruct() { TestInt = 1, TestString = "Hello World" };
        }

        [Test]
        public void Clone()
        {
            TestStruct c = (TestStruct)obj.Clone();
            Assert.That(c.TestInt, Is.EqualTo(obj.TestInt));
            Assert.That(c.TestString, Is.EqualTo(obj.TestString));
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToStream_Null()
        {
            obj.ToStream(null);
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

            using (IKistlContext ctx = Kistl.API.Server.KistlContext.InitSession())
            {
                TestStruct result = new TestStruct();
                result.FromStream(sr);

                Assert.That(result.TestInt, Is.EqualTo(obj.TestInt));
                Assert.That(result.TestString, Is.EqualTo(obj.TestString));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader()
        {
            using (IKistlContext ctx = Kistl.API.Server.KistlContext.InitSession())
            {
                TestStruct result = new TestStruct();
                result.FromStream(null);
            }
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

            obj.NotifyPropertyChanging("TestString");
            obj.TestString = "test";
            obj.NotifyPropertyChanged("TestString");

            Assert.That(hasChanged, Is.True);
            Assert.That(hasChanging, Is.True);

            obj.PropertyChanged -= changedHandler;
            obj.PropertyChanging -= changingHanlder;
        }
    }
}
