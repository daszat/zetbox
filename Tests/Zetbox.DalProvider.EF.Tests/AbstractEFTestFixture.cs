using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.DalProvider.Ef
{
    public class AbstractEFTestFixture : Kistl.API.AbstractConsumerTests.AbstractTestFixture
    {
        protected IKistlContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
        }
    }
}
