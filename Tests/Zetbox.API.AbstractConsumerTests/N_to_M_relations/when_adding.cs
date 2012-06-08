// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

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
