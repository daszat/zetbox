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

            obj = new TestObjClassImpl();
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
            ((TestObjClassImpl)obj).ID = 10;
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
            ((TestObjClassImpl)obj).ID = 10;
            obj.NotifyPropertyChanged("test");
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        /// ObjectState is just for serialization....
        public void ObjectState_New_then_UnModified()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            ((TestObjClassImpl)obj).ID = 10;
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
                TestObjClass result = new TestObjClassImpl();
                result.FromStream(sr);

                Assert.That(result.GetType(), Is.EqualTo(obj.GetType()));
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
                TestObjClass result = new TestObjClassImpl();
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

            SerializableType wrongType = new SerializableType(typeof(string));
            BinarySerializer.ToBinary(wrongType, sw);

            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                ms.Seek(0, SeekOrigin.Begin);
                TestObjClass result = new TestObjClassImpl();
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

            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                TestObjClass result = ctx.Create<TestObjClass>();
                result.FromStream(sr);

                Assert.That(result.GetType(), Is.EqualTo(obj.GetType()));
                Assert.That(result.ID, Is.EqualTo(obj.ID));
                Assert.That(result.ObjectState, Is.EqualTo(obj.ObjectState));
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
                Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(System.Data.EntityState.Detached));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AttachToContext_Other()
        {
            Assert.That(obj.Context, Is.Null);
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                using (IKistlContext ctx2 = Kistl.API.Server.KistlDataContext.GetContext())
                {
                    obj.AttachToContext(ctx2);
                    Assert.That(obj.Context, Is.Not.Null);
                    Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(System.Data.EntityState.Detached));
                }
            }
        }

        [Test]
        public void DetachFromContext()
        {
            Assert.That(obj.Context, Is.Null);
            ((TestObjClassImpl)obj).ID = 10;
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                ctx.Attach(obj);
                Assert.That(obj.Context, Is.Not.Null);

                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);

                Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(System.Data.EntityState.Unchanged));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DetachFromContext_Other()
        {
            Assert.That(obj.Context, Is.Null);
            ((TestObjClassImpl)obj).ID = 10;
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
            {
                ctx.Attach(obj);
                Assert.That(obj.Context, Is.Not.Null);

                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);
                Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(System.Data.EntityState.Unchanged));

                using (IKistlContext ctx2 = Kistl.API.Server.KistlDataContext.GetContext())
                {
                    obj.DetachFromContext(ctx2);
                    Assert.That(obj.Context, Is.Null);
                    Assert.That(((TestObjClassImpl)obj).EntityState, Is.EqualTo(System.Data.EntityState.Unchanged));
                }
            }
        }
    }
}
