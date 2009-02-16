using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.API.Client.Mocks;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.API.Client.Tests
{
    [TestFixture]
    public class BaseClientDataObjectTests
    {
        private TestObjClass obj;
        private CustomActionsManagerAPITest currentCustomActionsManager;
        private bool PropertyChangedCalled = false;

        [SetUp]
        public void SetUp()
        {
            var testCtx = new ClientApplicationContextMock();

            currentCustomActionsManager = (CustomActionsManagerAPITest)testCtx.CustomActionsManager;
            currentCustomActionsManager.Reset();

            PropertyChangedCalled = false;

            obj = new TestObjClass__Implementation__();
            obj.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(obj_PropertyChanged);
        }

        void obj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PropertyChangedCalled = true;
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
            obj.SetPrivatePropertyValue<int>("ID", 10);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        public void ObjectState_CreatedObject_Modified()
        {
            obj.NotifyPropertyChanged("test");
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void ObjectState_ObjectWithID_Modified()
        {
            obj.SetPrivatePropertyValue<int>("ID", 10);
            obj.NotifyPropertyChanged("test");
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void ObjectState_New_then_UnModified()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            obj.SetPrivatePropertyValue<int>("ID", 10);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }

        [Test]
        public void NotifyChange()
        {
            obj.NotifyChange();
            Assert.That(PropertyChangedCalled, Is.True);
        }

        [Test]
        public void NotifyPropertyChanged_ing()
        {
            obj.NotifyPropertyChanging("StringProp");
            obj.StringProp = "test";
            obj.NotifyPropertyChanged("StringProp");
            Assert.That(PropertyChangedCalled, Is.True);
        }

        [Test]
        public void ApplyChanges()
        {
            TestObjClass result = new TestObjClass__Implementation__();

            obj.SetPrivatePropertyValue<int>("ID", 10);

            ((TestObjClass__Implementation__)obj).ApplyChanges(result);
            Assert.That(result.ID, Is.EqualTo(obj.ID));
            Assert.That(PropertyChangedCalled, Is.True);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplyChanges_Null()
        {
            TestObjClass__Implementation__ result = null;
            ((TestObjClass__Implementation__)obj).ApplyChanges(result);
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

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass result = new TestObjClass__Implementation__();
                result.FromStream(sr);

                Assert.That(result.GetType(), Is.EqualTo(obj.GetType()));
                Assert.That(result.ID, Is.EqualTo(obj.ID));
                Assert.That(result.ObjectState, Is.EqualTo(obj.ObjectState));
                Assert.That(PropertyChangedCalled, Is.False);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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

            SerializableType wrongType = new SerializableType(typeof(string));
            BinarySerializer.ToStream(wrongType, sw);

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                ms.Seek(0, SeekOrigin.Begin);
                TestObjClass result = new TestObjClass__Implementation__();
                result.FromStream(sr);
            }
        }

        [Test]
        public void AttachToContext()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
                Assert.That(PropertyChangedCalled, Is.True);
            }
        }

        [Test]
        public void AttachToContext_New()
        {
            Assert.That(obj.Context, Is.Null);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                obj.SetPrivatePropertyValue<int>("ID", -1);
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
                Assert.That(PropertyChangedCalled, Is.True);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AttachToContext_New_InvalidID()
        {
            Assert.That(obj.Context, Is.Null);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                obj.SetPrivatePropertyValue<int>("ID", Helper.INVALIDID);
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
                Assert.That(PropertyChangedCalled, Is.False);
            }
        }

        [Test]
        public void AttachToContext_Twice()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

                obj.AttachToContext(ctx);
                Assert.That(obj.Context, Is.Not.Null);
            }
        }

        [Test]
        public void DetachFromContext()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                ctx.Attach(obj);
                Assert.That(obj.Context, Is.Not.Null);

                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
                Assert.That(PropertyChangedCalled, Is.True);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DetachFromContext_NotAttached()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            }
        }
    }
}
