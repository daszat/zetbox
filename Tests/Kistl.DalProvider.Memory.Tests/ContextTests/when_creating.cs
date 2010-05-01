
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
    public class when_creating : AbstractMemoryContextTextFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.DalProvider.Memory.Tests.ContextTests.when_creating");

        [Test]
        public void should_resolve_readwrite()
        {
            IKistlContext ctx = GetMemoryContext();

            Assert.That(ctx, Is.Not.Null);
            Assert.That(ctx.IsReadonly, Is.False);
        }

        [Test]
        public void should_be_queryable()
        {
            IKistlContext ctx = GetMemoryContext();

            IQueryable<ObjectClass> query = ctx.GetQuery<ObjectClass>();
            Assert.That(query, Is.Not.Null);

            var list = query.ToList();

            Assert.That(list, Is.Empty);
        }
    }
}
