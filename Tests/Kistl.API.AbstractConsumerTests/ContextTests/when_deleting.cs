
namespace Kistl.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class when_deleting
        : AbstractTestFixture
    {
        [Test]
        public void should_remove_one_to_n()
        {
            var ctx = GetContext();
            var one = ctx.Create<One_to_N_relations_One>();
            var n1 = ctx.Create<One_to_N_relations_N>();
            var n2 = ctx.Create<One_to_N_relations_N>();

            one.NSide.Add(n1);
            one.NSide.Add(n2);

            ctx.SubmitChanges();

            ctx.Delete(one);
            ctx.Delete(n1);
            ctx.Delete(n2);

            Assert.That(one.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(n1.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(n2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            ctx.SubmitChanges();
        }
    }
}
