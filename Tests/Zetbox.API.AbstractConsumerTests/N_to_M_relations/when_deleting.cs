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

    public abstract class when_deleting
        : NMTestFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            aSide1.BSide.Add(bSide1);
            SubmitAndReload();
        }

        [Test]
        public void should_delete_RelationEntry()
        {
            aSide1.BSide.Remove(bSide1);
            Assert.That(ctx.AttachedObjects.OfType<IRelationEntry>().Single().ObjectState, Is.EqualTo(DataObjectState.Deleted));
            ctx.SubmitChanges();
        }

        [Test]
        [Ignore("This is a philosophical question. When a Object is referenced by another, should it realy be deleted? Whit if this breaks a Business Rule? You can only be sure, if cascade deletion is defined and implemented.")]
        public void should_remove_from_Collection()
        {
            ctx.Delete(bSide1);
            // the RelationEntry is not loaded in this scenario
            // Assert.That(ctx.AttachedObjects.OfType<IRelationEntry>().Single().ObjectState, Is.EqualTo(DataObjectState.Deleted));
            ctx.SubmitChanges();
        }
    }
}
