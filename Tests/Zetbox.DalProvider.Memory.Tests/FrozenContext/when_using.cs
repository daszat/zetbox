
namespace Zetbox.DalProvider.Memory.Tests.FrozenContext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Base;
    using NUnit.Framework;

    public sealed class when_using
        : AbstractTestFixture
    {
        [Test]
        public void should_have_references()
        {
            var ctx = scope.Resolve<IFrozenContext>();
            Assert.That(ctx, Is.Not.Null);
            Assert.That(ctx.GetQuery<ObjectClass>().Where(oc => oc.BaseObjectClass != null).ToList(), Is.Not.Empty);
            Assert.That(ctx.GetQuery<ObjectClass>().Where(oc => oc.DefaultViewModelDescriptor != null).ToList(), Is.Not.Empty);
        }

        [Test]
        [Ignore("Not implemented")]
        public void should_reject_modifications()
        {
            var ctx = scope.Resolve<IFrozenContext>();
            Assert.That(() =>
                {
                    ctx.GetQuery<ObjectClass>().Where(oc => oc.BaseObjectClass != null).First().BaseObjectClass = null;
                },
                Throws.InstanceOf<ReadOnlyObjectException>());
        }
    }
}
