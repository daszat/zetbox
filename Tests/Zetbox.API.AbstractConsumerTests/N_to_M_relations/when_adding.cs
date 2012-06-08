
namespace Zetbox.API.AbstractConsumerTests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public static class when_adding
    {
        public abstract class on_A_side
            : NMTestFixture
        {
            [Test]
            public void should_remember_locally()
            {
                aSide1.BSide.Add(bSide1);
                Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            }

            [Test]
            public void should_persist_one_element()
            {
                aSide1.BSide.Add(bSide1);
                SubmitAndReload();
                Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            }

            [Test]
            public void should_remember_locally_two_items()
            {
                aSide1.BSide.Add(bSide1);
                Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
                aSide1.BSide.Add(bSide2);
                Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            }

            [Test]
            public void should_persist_two_elements()
            {
                aSide1.BSide.Add(bSide1);
                aSide1.BSide.Add(bSide2);
                SubmitAndReload();
                Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1, bSide2 }));
            }

            [Test]
            public void should_have_RelationEntry_in_context()
            {
                aSide1.BSide.Add(bSide1);
                Assert.That(ctx.AttachedObjects.OfType<IRelationEntry>().ToList(), Is.Not.Empty);
                Assert.That(ctx.AttachedObjects.OfType<IRelationEntry>().ToList(), Has.Count.EqualTo(1));
            }

            [Test]
            public void should_synchronize_other_side()
            {
                aSide1.BSide.Add(bSide1);
                Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            }

            [Test]
            public void should_synchronize_other_side_after_persisting()
            {
                aSide1.BSide.Add(bSide1);
                SubmitAndReload();
                Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            }
        }

        public abstract class on_B_side
            : NMTestFixture
        {
            [Test]
            public void should_remember_locally()
            {
                bSide1.ASide.Add(aSide1);
                Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            }

            [Test]
            public void should_persist_one_element()
            {
                bSide1.ASide.Add(aSide1);
                SubmitAndReload();
                Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
            }

            [Test]
            public void should_remember_locally_two_items()
            {
                bSide1.ASide.Add(aSide1);
                Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1 }));
                bSide1.ASide.Add(aSide2);
                Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            }

            [Test]
            public void should_persist_two_elements()
            {
                bSide1.ASide.Add(aSide1);
                bSide1.ASide.Add(aSide2);
                SubmitAndReload();
                Assert.That(bSide1.ASide, Is.EquivalentTo(new[] { aSide1, aSide2 }));
            }

            [Test]
            public void should_have_RelationEntry_in_context()
            {
                bSide1.ASide.Add(aSide1);
                Assert.That(ctx.AttachedObjects.OfType<IRelationEntry>().ToList(), Is.Not.Empty);
                Assert.That(ctx.AttachedObjects.OfType<IRelationEntry>().ToList(), Has.Count.EqualTo(1));
            }

            [Test]
            public void should_synchronize_other_side()
            {
                bSide1.ASide.Add(aSide1);
                Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            }
            [Test]

            public void should_synchronize_other_side_after_persisting()
            {
                bSide1.ASide.Add(aSide1);
                SubmitAndReload();
                Assert.That(aSide1.BSide, Is.EquivalentTo(new[] { bSide1 }));
            }
        }
    }
}
