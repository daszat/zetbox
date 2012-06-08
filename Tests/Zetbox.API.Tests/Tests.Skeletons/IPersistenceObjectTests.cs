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
        protected IKistlContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
            ctx.Attach(obj);

        }
        public override void TearDown()
        {
            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }
            base.TearDown();
        }

        [Test]
        public void should_roundtrip_IPersistenceObject_correctly()
        {
            T result = SerializationRoundtrip(obj);

            Assert.That(result.ID, Is.EqualTo(obj.ID));
        }
    }

}
