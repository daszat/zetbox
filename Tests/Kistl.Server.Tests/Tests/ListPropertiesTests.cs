using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;

using Kistl.API;
using Kistl.API.AbstractConsumerTests;
using Kistl.API.Server;

using NUnit.Framework;

namespace Kistl.Server.Tests
{
    [TestFixture]
    public class ListPropertiesTests
        : AbstractListPropertiesTests
    {
        private IContainer container;
        private IContainer GetContainer()
        {
            if (container == null)
            {
                container = Kistl.Server.Tests.SetUp.CreateInnerContainer();
            }
            return container;
        }

        [TearDown]
        public void TearDownContainer()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        protected override IKistlContext GetContext()
        {
            return GetContainer().Resolve<IKistlContext>();
        }
    }
}
