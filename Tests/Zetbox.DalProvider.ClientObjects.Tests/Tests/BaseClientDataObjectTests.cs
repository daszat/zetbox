// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.DalProvider.Client.Mocks;
    using NUnit.Framework;
    using Zetbox.App.Test;

    [TestFixture]
    public class BaseClientDataObjectTests : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
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
            obj.ToStream((ZetboxStreamWriter)null, null, false);
        }

        [Test]
        public void Stream()
        {
            var typeMap = scope.Resolve<TypeMap>();
            using (var ms = new MemoryStream())
            using (var sw = new ZetboxStreamWriter(typeMap, new BinaryWriter(ms)))
            using (var sr = new ZetboxStreamReader(typeMap, new BinaryReader(ms)))
            {
                var ctx = GetContext();

                ctx.Attach(obj);
                obj.ToStream(sw, null, false);

                Assert.That(ms.Length, Is.GreaterThan(0));

                ms.Seek(0, SeekOrigin.Begin);

                var t = sr.ReadSerializableType();
                Assert.That(t.GetSystemType().IsAssignableFrom(typeof(ANewObjectClass)), string.Format("{0} not assignable to {1}", typeof(ANewObjectClass), t));

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
            using (IZetboxContext ctx = GetContext())
            {
                BaseClientDataObjectMockImpl result = new BaseClientDataObjectMockImpl(null);
                result.FromStream((ZetboxStreamReader)null);
            }
        }

        [Test]
        public void AttachToContext()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IZetboxContext ctx = GetContext())
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
            using (IZetboxContext ctx = GetContext())
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
            using (IZetboxContext ctx = GetContext())
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
            using (IZetboxContext ctx = GetContext())
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
            using (IZetboxContext ctx = GetContext())
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
        [ExpectedException(typeof(WrongZetboxContextException))]
        public void DetachFromContext_NotAttached()
        {
            Assert.That(obj.Context, Is.Null);
            obj.SetPrivatePropertyValue<int>("ID", 10);
            using (IZetboxContext ctx = GetContext())
            {
                obj.DetachFromContext(ctx);
                Assert.That(obj.Context, Is.Null);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            }
        }
    }
}
