using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Tests.Skeletons;
using Kistl.App.Projekte;
using Kistl.DalProvider.Ef.Mocks;

using NUnit.Framework;

namespace Kistl.DalProvider.Ef.Tests
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
