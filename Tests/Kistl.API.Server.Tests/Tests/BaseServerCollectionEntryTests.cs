using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.API;
using Kistl.API.Server;
using NUnit.Framework.SyntaxHelpers;
using System.IO;

namespace Kistl.API.Server.Tests
{
    [TestFixture]
    public class BaseServerCollectionEntryTests
    {
        private TestObjClass_TestNameCollectionEntry obj;

        [SetUp]
        public void SetUp()
        {
            obj = new TestObjClass_TestNameCollectionEntry();
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

            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                TestObjClass_TestNameCollectionEntry result = new TestObjClass_TestNameCollectionEntry();
                result.FromStream(sr);

                Assert.That(result.ID, Is.EqualTo(obj.ID));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader()
        {
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                TestObjClass_TestNameCollectionEntry result = new TestObjClass_TestNameCollectionEntry();
                result.FromStream(null);
            }
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            obj.NotifyPropertyChanging("Value");
            obj.Value = "test";
            obj.NotifyPropertyChanged("Value");
        }
    }
}
