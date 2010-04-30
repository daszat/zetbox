using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Autofac;

namespace Kistl.API.Tests
{
    public abstract class AbstractApiTestFixture : AbstractConsumerTests.AbstractTestFixture
    {
        protected ITypeTransformations typeTrans;

        public override void SetUp()
        {
            base.SetUp();
            typeTrans = scope.Resolve<ITypeTransformations>();
        }
    }
}
