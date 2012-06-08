
namespace Zetbox.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class when_deleting_n_m
        : AbstractTestFixture
    {
        IZetboxContext ctx;
        N_to_M_relations_A a1;
        N_to_M_relations_B b1;
        N_to_M_relations_B b2;

        public override void SetUp()
        {
            base.SetUp();

            ctx = GetContext();

            a1 = ctx.Create<N_to_M_relations_A>();
            b1 = ctx.Create<N_to_M_relations_B>();
            b2 = ctx.Create<N_to_M_relations_B>();

            a1.BSide.Add(b1);
            a1.BSide.Add(b2);

            ctx.SubmitChanges();
        }

        [Test]
        public void should_remove_n_m()
        {
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
