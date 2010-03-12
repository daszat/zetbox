using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Autofac;
using Kistl.API;

namespace Kistl.Server.Tests.Blobs
{
    [TestFixture]
    [Ignore("This test suite does not support custom actions")]
    public class when_using
            : Kistl.API.AbstractConsumerTests.Blobs.when_using
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

        public override void DisposeContext()
        {
            base.DisposeContext();
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        public override IKistlContext GetContext()
        {
            return GetContainer().Resolve<IKistlContext>();
        }
    }
}
