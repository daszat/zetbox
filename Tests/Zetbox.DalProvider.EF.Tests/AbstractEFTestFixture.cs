using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.DalProvider.Ef
{
    public class AbstractEFTestFixture : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
    {
        protected IZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
        }
    }
}
