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

        protected IKistlContext ctx { get; private set; }
        protected TestCustomObject obj { get; private set; }

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            obj = ctx.GetQuery<TestCustomObject>().First();
        }

        [TearDown]
        public void DisposeContext()
        {
            ctx.Dispose();
        }
    }

}
