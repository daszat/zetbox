using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Tests.Skeletons
{

    public abstract class IPersistenceObjectTests<T>
        : IStreamableTests<T>
        where T : IPersistenceObject, new()
    {

        [Test]
        public void should_not_be_attached_when_created()
        {
            Assert.That(obj.IsAttached, Is.False);
        }

        [Test]
        public void should_be_attached_after_attaching()
        {
            Assert.Ignore("Cannot get context without Kistl.API.{Client,Server} reference");
            //using (var ctx = ????.GetContext())
            //{
            //    ctx.Attach(obj);
            //    Assert.That(obj.IsAttached, Is.False);
            //}
        }

        [Test]
        [Ignore("Obsolete, DAL Provider will manage ObjectState")]
        public void ObjectState_should_be_New_when_created()
        {
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }


        [Test]
        public void should_roundtrip_IPersistenceObject_correctly()
        {
            T result = SerializationRoundtrip(obj);

            Assert.That(result.ID, Is.EqualTo(obj.ID));
            // [Ignore("Obsolete, DAL Provider will manage ObjectState")]
            // Assert.That(result.ObjectState, Is.EqualTo(obj.ObjectState));
        }

        [Test]
        [Ignore("Obsolete, DAL Provider will manage ObjectState")]
        /// ObjectState is just for serialization....
        public void ObjectState_CreatedObject_Modified()
        {
            obj.NotifyPropertyChanged("test", null, null);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }


    }

}
