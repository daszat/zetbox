
namespace Kistl.DalProvider.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.DalProvider.Client.Mocks;
    using NUnit.Framework;

    [TestFixture]
    public class BaseClientDataObjectTests : Kistl.API.AbstractConsumerTests.AbstractTestFixture
    {
        private BaseClientDataObjectMockImpl obj;
        private bool PropertyChangedCalled = false;

        public override void SetUp()
        {
            base.SetUp();

            PropertyChangedCalled = false;

            obj = new BaseClientDataObjectMockImpl(null);
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
            BaseClientDataObjectMockImpl result = new BaseClientDataObjectMockImpl(null);

            obj.SetPrivatePropertyValue<int>("ID", 10);

            obj.ApplyChangesFrom(result);
            Assert.That(result.ID, Is.EqualTo(obj.ID));
            Assert.That(PropertyChangedCalled, Is.True);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplyChanges_Null()
        {
            BaseClientDataObjectMockImpl result = null;
            obj.ApplyChangesFrom(result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToStream_Null()
        {
            obj.ToStream((KistlStreamWriter)null, null, false);
        }

        [Test]
        public void Stream()
        {
            var typeMap = scope.Resolve<TypeMap>();
            using (var ms = new MemoryStream())
            using (var sw = new KistlStreamWriter(typeMap, new BinaryWriter(ms)))
            using (var sr = new KistlStreamReader(typeMap, new BinaryReader(ms)))
            {
                var ctx = GetContext();

                ctx.Attach(obj);
                obj.ToStream(sw, null, false);

                Assert.That(ms.Length, Is.GreaterThan(0));

                ms.Seek(0, SeekOrigin.Begin);

                var t = sr.ReadSerializableType();

                BaseClientDataObjectMockImpl result = new BaseClientDataObjectMockImpl(null);
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
            using (IKistlContext ctx = GetContext())
            {
                BaseClientDataObjectMockImpl result = new BaseClientDataObjectMockImpl(null);
                result.FromStream((KistlStreamReader)null);
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
