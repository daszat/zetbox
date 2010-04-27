using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Autofac;
using Kistl.API.Utils;
using Kistl.API.Configuration;

namespace Kistl.API.AbstractConsumerTests
{
    public abstract class AbstractTextFixture
    {
        protected ILifetimeScope scope;

        [SetUp]
        public void SetUp()
        {
            scope = AbstractSetup.BeginLifetimeScope();
        }

        [TearDown]
        public void TearDown()
        {
            scope.Dispose();
        }

        public virtual IKistlContext GetContext()
        {
            return scope.Resolve<IKistlContext>();
        }
    }
}
