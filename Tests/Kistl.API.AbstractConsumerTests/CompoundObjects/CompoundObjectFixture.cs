using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.CompoundObjects
{

    public abstract class CompoundObjectFixture
    {
        public abstract IKistlContext GetContext();

        public virtual TestCustomObject GetObject()
        {
            return ctx.GetQuery<TestCustomObject>().First();
        }

        protected IKistlContext ctx;
        protected TestCustomObject obj;



        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            obj = GetObject();
        }

        [TearDown]
        public virtual void DisposeContext()
        {
            ctx.Dispose();
        }
    }

}
