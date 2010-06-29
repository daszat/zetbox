
namespace Kistl.DalProvider.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.DalProvider.Client.Mocks;
    using NUnit.Framework;

    [TestFixture]
    public class BaseClientDataObjectTests : Kistl.API.AbstractConsumerTests.AbstractTestFixture
    {
        private BaseClientDataObjectMock__Implementation__ obj;
        private bool PropertyChangedCalled = false;

        public override void SetUp()
        {
            base.SetUp();

            PropertyChangedCalled = false;

            obj = new BaseClientDataObjectMock__Implementation__(null);
            obj.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(obj_PropertyChanged);
        }

        void obj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PropertyChangedCalled = true;
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
            obj.NotifyPropertyChanged("test", null, null);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void ObjectState_ObjectWithID_Modified()
        {
            obj.SetPrivatePropertyValue<int>("ID", 10);
            obj.NotifyPropertyChanged("test", null, null);
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
        public void NotifyPropertyChanged_ing()
        {
            obj.NotifyPropertyChanging("ID", null, null);
            obj.NotifyPropertyChanged("ID", null, null);
            Assert.That(PropertyChangedCalled, Is.True);
        }

        [Test]
        public void ApplyChanges()
        {
            BaseClientDataObjectMock__Implementation__ result = new BaseClientDataObjectMock__Implementation__(null);

            obj.SetPrivatePropertyValue<int>("ID", 10);

            obj.ApplyChangesFrom(result);
            Assert.That(result.ID, Is.EqualTo(obj.ID));
            Assert.That(PropertyChangedCalled, Is.True);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplyChanges_Null()
        {
            BaseClientDataObjectMock__Implementation__ result = null;
            obj.ApplyChangesFrom(result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToStream_Null()
        {
            obj.ToStream((BinaryWriter)null, null, false);
        }

        [Test]
        public void Stream()
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(ms);
            BinaryReader sr = new BinaryReader(ms);

            using (var ctx = GetContext())
            {
                ctx.Attach(obj);
                obj.ToStream(sw, null, false);

                Assert.That(ms.Length, Is.GreaterThan(0));

                ms.Seek(0, SeekOrigin.Begin);
            }

            SerializableType t;
            BinarySerializer.FromStream(out t, sr);

            BaseClientDataObjectMock__Implementation__ result = new BaseClientDataObjectMock__Implementation__(null);
            result.FromStream(sr);

            Assert.That(result.GetType(), Is.EqualTo(obj.GetType()));
            Assert.That(result.ID, Is.EqualTo(obj.ID));
            Assert.That(result.ObjectState, Is.EqualTo(obj.ObjectState));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromStream_Null_StreamReader()
        {
            using (IKistlContext ctx = GetContext())
            {
                BaseClientDataObjectMock__Implementation__ result = new BaseClientDataObjectMock__Implementation__(null);
                result.FromStream((BinaryReader)null);
            }
        }

        [Test]
        public void AttachToContext()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IKistlContext ctx = GetContext())
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
            using (IKistlContext ctx = GetContext())
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
            using (IKistlContext ctx = GetContext())
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
            using (IKistlContext ctx = GetContext())
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
            using (IKistlContext ctx = GetContext())
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
        [ExpectedException(typeof(WrongKistlContextException))]
        public void DetachFromContext_NotAttached()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IKistlContext ctx = GetContext())
            {
                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            }
        }
    }
}
