using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.PersistenceObjects
{

    public abstract class ObjectLoadFixture
    {
        public abstract IKistlContext GetContext();

        public virtual TestCustomObject GetObject()
        {
            return ctx.GetQuery<TestCustomObject>().First();
        }

        protected IKistlContext ctx { get; private set; }
        protected TestCustomObject obj { get; private set; }

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            obj = GetObject();
        }

        [TearDown]
        public void DisposeContext()
        {
            if (ctx != null)
                ctx.Dispose();
        }
    }

}
