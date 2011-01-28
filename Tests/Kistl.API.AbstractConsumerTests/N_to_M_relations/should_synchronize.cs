
namespace Kistl.API.AbstractConsumerTests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Test;
    using NUnit.Framework;

    public abstract class should_synchronize
        : AbstractTestFixture
    {
        IKistlContext ctx;
        N_to_M_relations_A aSide1;
        N_to_M_relations_A aSide2;
        N_to_M_relations_B bSide1;
        N_to_M_relations_B bSide2;

        [SetUp]
        public void InitTestObjects()
        {
            ctx = GetContext();
            aSide1 = ctx.Create<N_to_M_relations_A>();
            aSide2 = ctx.Create<N_to_M_relations_A>();
            bSide1 = ctx.Create<N_to_M_relations_B>();
            bSide2 = ctx.Create<N_to_M_relations_B>();
        }

        [TearDown]
        public void ForgetTestObjects()
        {
            aSide1 = aSide2 = null;
            bSide1 = bSide2 = null;
            ctx = null;
        }

        private void SubmitAndReload()
        {
            ctx.SubmitChanges();
            ctx = GetContext();
            aSide1 = ctx.Find<N_to_M_relations_A>(aSide1.ID);
            aSide2 = ctx.Find<N_to_M_relations_A>(aSide2.ID);
            bSide1 = ctx.Find<N_to_M_relations_B>(bSide1.ID);
            bSide2 = ctx.Find<N_to_M_relations_B>(bSide2.ID);
        }

        [Test]
        public void init_correct()
        {
            Assert.That(aSide1.BSide, Is.Empty);
            Assert.That(aSide2.BSide, Is.Empty);
            Assert.That(bSide1.ASide, Is.Empty);
            Assert.That(bSide2.ASide, Is.Empty);
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(4));
            SubmitAndReload();
            Assert.That(aSide1.BSide, Is.Empty);
            Assert.That(aSide2.BSide, Is.Empty);
            Assert.That(bSide1.ASide, Is.Empty);
            Assert.That(bSide2.ASide, Is.Empty);
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(4));
        }

        [Test]
        public void when_setting_N_side_one_item()
        {
            aSide1.BSide.Add(bSide1);

            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(5));
            SubmitAndReload();
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(5));
        }

        [Test]
        public void when_setting_M_side_one_item()
        {
            bSide1.ASide.Add(aSide1);

            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(5));
            SubmitAndReload();
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(5));
        }

        [Test]
        public void when_setting_both_sides_one_item()
        {
            aSide1.BSide.Add(bSide1);
            bSide1.ASide.Add(aSide1);

            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide1 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
            SubmitAndReload();
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide1 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
        }

        [Test]
        public void when_setting_N_side_two_items()
        {
            aSide1.BSide.Add(bSide1);
            aSide1.BSide.Add(bSide2);

            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
            SubmitAndReload();
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
        }

        [Test]
        public void when_setting_M_side_two_items()
        {
            bSide1.ASide.Add(aSide1);
            bSide1.ASide.Add(aSide2);

            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
            SubmitAndReload();
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(6));
        }

        [Test]
        public void when_setting_both_sides_two_items()
        {
            bSide1.ASide.Add(aSide1);
            bSide1.ASide.Add(aSide2);

            aSide1.BSide.Add(bSide1);
            aSide2.BSide.Add(bSide1);

            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2, aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide1 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
            SubmitAndReload();
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2, aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide1 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide1 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
        }

        [Test]
        public void when_setting_N_side_matrix()
        {
            aSide1.BSide.Add(bSide1);
            aSide1.BSide.Add(bSide2);

            aSide2.BSide.Add(bSide1);
            aSide2.BSide.Add(bSide2);

            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
            SubmitAndReload();
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
        }

        [Test]
        public void when_setting_M_side_matrix()
        {
            bSide1.ASide.Add(aSide1);
            bSide1.ASide.Add(aSide2);

            bSide2.ASide.Add(aSide1);
            bSide2.ASide.Add(aSide2);

            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
            SubmitAndReload();
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(8));
        }

        [Test]
        public void when_setting_both_sides_matrix()
        {
            bSide1.ASide.Add(aSide1);
            bSide1.ASide.Add(aSide2);

            bSide2.ASide.Add(aSide1);
            bSide2.ASide.Add(aSide2);

            aSide1.BSide.Add(bSide1);
            aSide1.BSide.Add(bSide2);

            aSide2.BSide.Add(bSide1);
            aSide2.BSide.Add(bSide2);

            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2, aSide1, aSide2 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1, aSide2, aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2, bSide1, bSide2 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide2, bSide1, bSide2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(12));
            SubmitAndReload();
            Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2, aSide1, aSide2 }));
            Assert.That(bSide2.ASide, Is.EquivalentTo(new[] { aSide1, aSide2, aSide1, aSide2 }));
            Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2, bSide1, bSide2 }));
            Assert.That(aSide2.BSide, Is.EquivalentTo(new[] { bSide1, bSide2, bSide1, bSide2 }));
            Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(12));
        }

        [Test]
        public void when_deleting_items()
        {
            aSide1.BSide.Add(bSide1);
            SubmitAndReload();

            aSide1.BSide.Clear();

            ctx.Delete(aSide1);
            ctx.Delete(bSide1);
            ctx.SubmitChanges();
        }
    }
}
