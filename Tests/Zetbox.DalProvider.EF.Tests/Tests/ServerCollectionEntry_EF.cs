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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.API.Tests.Skeletons;
using Zetbox.App.Projekte;
using Zetbox.DalProvider.Ef.Mocks;

using NUnit.Framework;

namespace Zetbox.DalProvider.Ef.Tests
{
    [TestFixture]
    public class ServerCollectionEntry_EF
        : CollectionEntryTests<Kunde_EMails_CollectionEntryEfImpl>
    {

        [Test]
        public void should_have_a_RelationshipManager()
        {
            Assert.That(obj.RelationshipManager, Is.Not.Null);
        }

        [Test]
        public void should_have_no_EntityKey_when_created()
        {
            var local_obj = CreateObjectInstance();
            Assert.That(local_obj.EntityKey, Is.Null);
        }

        [Test]
        public void should_have_Detached_EntityState_when_created()
        {
            var local_obj = CreateObjectInstance();
            Assert.That(local_obj.EntityState, Is.EqualTo(EntityState.Detached));
        }

        [Test]
        public void should_be_attached_to_EFContext_after_attaching()
        {
            Assert.That(obj.EntityState, Is.Not.EqualTo(EntityState.Detached));
            Assert.That(obj.IsAttached, Is.EqualTo(true));
            ObjectContext objCtx = obj.GetEFContext();
            Assert.That(objCtx, Is.Not.Null);
        }
    }
}
