using System;
using System.Collections.Generic;
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
    public class BaseServerCollectionEntryTests
    {
        private TestObjClass_TestNameCollectionEntry__Implementation__ obj;

        [SetUp]
        public void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            obj = new TestObjClass_TestNameCollectionEntry__Implementation__();
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
                TestObjClass_TestNameCollectionEntry__Implementation__ result = new TestObjClass_TestNameCollectionEntry__Implementation__();
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
                TestObjClass_TestNameCollectionEntry__Implementation__ result = new TestObjClass_TestNameCollectionEntry__Implementation__();
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
