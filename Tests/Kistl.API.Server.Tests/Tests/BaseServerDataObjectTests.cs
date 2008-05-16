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
    public class BaseServerDataObjectTests
    {
        private TestObjClass obj;
        private CustomActionsManagerAPITest currentCustomActionsManager;

        [SetUp]
        public void SetUp()
        {
            currentCustomActionsManager = (CustomActionsManagerAPITest)CustomActionsManagerFactory.Current;
            currentCustomActionsManager.Reset();

            obj = new TestObjClass();
        }

        [Test]
        public void Type()
        {
            Assert.That(obj.Type.NameDataObject, Is.EqualTo("API.Server.Tests.TestObjClass"));
        }

        [Test]
        public void EventAttached()
        {
            Assert.That(currentCustomActionsManager.IsObjectAttached(obj), Is.True);
        }

        [Test]
        public void ObjectState_CreatedObject()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void ObjectState_ObjectWithID()
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
            obj.ID = 10;
            obj.NotifyPropertyChanged("test");
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        /// ObjectState is just for serialization....
        public void ObjectState_New_then_UnModified()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            obj.ID = 10;
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
        public void CopyTo()
        {
            TestObjClass result = new TestObjClass();

            obj.ID = 10;

            obj.CopyTo(result);
            Assert.That(result.ID, Is.EqualTo(obj.ID));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_Null()
        {
            TestObjClass result = null;
            obj.CopyTo(result);
        }

        [Test]
        public void Clone()
        {
            Assert.That(obj.Clone(), Is.Not.Null);
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
                TestObjClass result = new TestObjClass();
                result.FromStream(sr);

                Assert.That(result.Type, Is.EqualTo(obj.Type));
                Assert.That(result.ID, Is.EqualTo(obj.ID));
                Assert.That(result.ObjectState, Is.EqualTo(obj.ObjectState));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader()
        {
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                TestObjClass result = new TestObjClass();
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

            ObjectType wrongType = new ObjectType("test.test.test");
            BinarySerializer.ToBinary(wrongType, sw);

            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                ms.Seek(0, SeekOrigin.Begin);
                TestObjClass result = new TestObjClass();
                result.FromStream(sr);
            }
        }

        [Test]
        public void AttachToContext()
        {
            Assert.That(obj.Context, Is.Null);
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Detached));
            }
        }

        [Test]
        public void DetachFromContext()
        {
            Assert.That(obj.Context, Is.Null);
            obj.ID = 10;
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                ctx.Attach(obj);
                Assert.That(obj.Context, Is.Not.Null);

                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);

                Assert.That(obj.EntityState, Is.EqualTo(System.Data.EntityState.Unchanged));
            }
        }
    }
}
