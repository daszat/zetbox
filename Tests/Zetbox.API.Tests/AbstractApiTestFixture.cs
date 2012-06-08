
namespace Zetbox.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    
    public abstract class AbstractApiTestFixture : AbstractConsumerTests.AbstractTestFixture
    {
        protected InterfaceType.Factory iftFactory;

        public override void SetUp()
        {
            base.SetUp();

            iftFactory = scope.Resolve<InterfaceType.Factory>();
        }
    }
}
