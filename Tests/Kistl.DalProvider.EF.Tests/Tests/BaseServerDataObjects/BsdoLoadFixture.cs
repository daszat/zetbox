using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.BaseServerDataObjects
{

    public class BsdoLoadFixture
    {
        public IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }

        protected IKistlContext ctx;
        protected TestCustomObject__Implementation__ obj;

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            obj = (TestCustomObject__Implementation__)ctx.GetQuery<TestCustomObject>().First();
        }


        [TearDown]
        public void DisposeContext()
        {
            ctx.Dispose();
        }
    }

}
