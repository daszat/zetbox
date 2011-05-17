
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

        [Test]
        public void should_remove_n_m()
        {
            var ctx = GetContext();
            var a1 = ctx.Create<N_to_M_relations_A>();
            var b1 = ctx.Create<N_to_M_relations_B>();
            var b2 = ctx.Create<N_to_M_relations_B>();

            a1.BSide.Add(b1);
            a1.BSide.Add(b2);

            ctx.SubmitChanges();

            ctx.Delete(a1);
            ctx.Delete(b1);
            ctx.Delete(b2);

            Assert.That(a1.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(b1.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(b2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            ctx.SubmitChanges();
        }
    }
}
