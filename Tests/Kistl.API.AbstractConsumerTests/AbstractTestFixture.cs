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
    public abstract class AbstractTestFixture
    {
        protected ILifetimeScope scope;

        [SetUp]
        public virtual void SetUp()
        {
            scope = AbstractSetUpFixture.BeginLifetimeScope();
        }

        [TearDown]
        public virtual void TearDown()
        {
            scope.Dispose();
        }

        protected IKistlContext GetContext()
        {
            return scope.Resolve<IKistlContext>();
        }
    }
}
