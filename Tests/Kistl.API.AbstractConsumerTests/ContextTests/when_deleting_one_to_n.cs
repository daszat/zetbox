
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

    public abstract class when_deleting_one_to_n
        : AbstractTestFixture
    {
        IKistlContext ctx;
        One_to_N_relations_One one;
        One_to_N_relations_N n1;
        One_to_N_relations_N n2;

        public override void SetUp()
        {
            base.SetUp();

            ctx = GetContext();

            one = ctx.Create<One_to_N_relations_One>();
            n1 = ctx.Create<One_to_N_relations_N>();
            n2 = ctx.Create<One_to_N_relations_N>();

            one.NSide.Add(n1);
            one.NSide.Add(n2);

            ctx.SubmitChanges();
        }

        [Test]
        public void should_delete_all_objects_same_ctx()
        {
            ctx.Delete(one);
            ctx.Delete(n1);
            ctx.Delete(n2);

            Assert.That(one.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(n1.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(n2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            ctx.SubmitChanges();
            Assert.That(ctx.AttachedObjects, Is.Empty);
        }

        [Test]
        public void should_delete_only_n()
        {
            var testCtx = GetContext();
            var testN2 = testCtx.Find<One_to_N_relations_N>(n2.ID);

            Assert.That(testCtx.AttachedObjects.Count(), Is.EqualTo(1));

            testCtx.Delete(testN2);

            Assert.That(testN2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            testCtx.SubmitChanges();
        }

        [Test]
        public void should_delete_only_n_same_ctx()
        {
            ctx.Delete(n2);

            Assert.That(one.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(n1.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(n2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            ctx.SubmitChanges();
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(2));
        }

        [Test]
        public void should_delete_only_n_remove()
        {
            one.NSide.Remove(n2);
            ctx.Delete(n2);

            Assert.That(one.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(n1.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(n2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            ctx.SubmitChanges();
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(2));
        }

        [Test]
        public void should_delete_only_n_pre_load_set_null()
        {
            var testCtx = GetContext();
            var testN2 = testCtx.Find<One_to_N_relations_N>(n2.ID);

            Assert.That(testN2.OneSide, Is.Not.Null);
            var testOne = testN2.OneSide;

            testN2.OneSide = null;
            testCtx.Delete(testN2);

            Assert.That(testOne.NSide, Has.Count.EqualTo(1));
            Assert.That(testN2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            testCtx.SubmitChanges();
            Assert.That(testOne.NSide, Has.Count.EqualTo(1));
        }

        [Test]
        public void should_delete_only_n_pre_load_remove()
        {
            var testCtx = GetContext();
            var testN2 = testCtx.Find<One_to_N_relations_N>(n2.ID);

            Assert.That(testN2.OneSide, Is.Not.Null);
            var testOne = testN2.OneSide;

            testOne.NSide.Remove(testN2);
            testCtx.Delete(testN2);

            Assert.That(testOne.NSide, Has.Count.EqualTo(1));
            Assert.That(testN2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            testCtx.SubmitChanges();
            Assert.That(testOne.NSide, Has.Count.EqualTo(1));
        }

        [Test]
        public void should_delete_reverse_all()
        {
            // use a different delete order as in should_delete
            // this should make no difference
            ctx.Delete(n1);
            ctx.Delete(n2);
            ctx.Delete(one);

            Assert.That(one.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(n1.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(n2.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            ctx.SubmitChanges();
            Assert.That(ctx.AttachedObjects, Is.Empty);
        }

        [Test]
        public void should_delete_only_one()
        {
            ctx.Delete(one);

            Assert.That(one.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(n1.OneSide, Is.Null);
            Assert.That(n1.ObjectState, Is.EqualTo(DataObjectState.Modified));
            Assert.That(n2.OneSide, Is.Null);
            Assert.That(n2.ObjectState, Is.EqualTo(DataObjectState.Modified));

            ctx.SubmitChanges();
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(2));
        }
    }
}
