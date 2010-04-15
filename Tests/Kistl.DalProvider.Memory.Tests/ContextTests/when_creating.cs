
namespace Kistl.DalProvider.Memory.Tests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API;
    using NUnit.Framework;
    using Kistl.App.Base;

    [TestFixture]
    public class when_creating
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory.Tests.ContextTests.when_creating");

        private ILifetimeScope container;

        [SetUp]
        public void BeginLifetimeScope()
        {
            container = Setup.MasterContainer.BeginLifetimeScope();
        }

        [Test]
        public void should_resolve_readwrite()
        {
            IKistlContext ctx = container.Resolve<BaseMemoryContext>();

            Assert.That(ctx, Is.Not.Null);
            Assert.That(ctx.IsReadonly, Is.False);
        }

        [Test]
        public void should_be_queryable()
        {
            IKistlContext ctx = container.Resolve<BaseMemoryContext>();

            IQueryable<ObjectClass> query = ctx.GetQuery<ObjectClass>();
            Assert.That(query, Is.Not.Null);

            var list = query.ToList();

            Assert.That(list, Is.Empty);
        }

        [TearDown]
        public void DisposeLifetimeScope()
        {
            if (container != null)
            {
                try
                {
                    container.Dispose();
                }
                finally
                {
                    container = null;
                }
            }
        }
    }
}
