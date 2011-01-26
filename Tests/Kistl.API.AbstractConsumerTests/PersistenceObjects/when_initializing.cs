
namespace Kistl.API.AbstractConsumerTests.PersistenceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.App.Base;
    using NUnit.Framework;

    public abstract class when_initializing
        : AbstractTestFixture
    {
        private IKistlContext ctx;

        [SetUp]
        public new void SetUp()
        {
            ctx = GetContext();
        }

        [Test]
        public void should_set_ExportGuid()
        {
            var m1 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.Null);
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(Guid.Empty));

            var m2 = ctx.Create<Method>();
            Assert.That(m1.ExportGuid, Is.Not.EqualTo(m2.ExportGuid));
        }
    }
}
