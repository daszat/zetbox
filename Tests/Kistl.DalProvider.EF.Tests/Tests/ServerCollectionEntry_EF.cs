using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Server.Mocks;
using Kistl.API.Tests.Skeletons;
using Kistl.App.Projekte;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Data;
using System.Data.Objects;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class ServerCollectionEntry_EF
        : CollectionEntryTests<Kunde_EMailsCollectionEntry__Implementation__>
    {
        public override void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            base.SetUp();
        }

        [Test]
        public void should_have_a_RelationshipManager()
        {
            Assert.That(obj.RelationshipManager, Is.Not.Null);
        }

        [Test]
        public void should_have_no_EntityKey_when_created()
        {
            Assert.That(obj.EntityKey, Is.Null);
        }

        [Test]
        public void should_have_Detached_EntityState_when_created()
        {
            Assert.That(obj.EntityState, Is.EqualTo(EntityState.Detached));
        }

        [Test]
        public void should_be_attached_to_EFContext_after_attaching()
        {
            using (var ctx = KistlContext.GetContext())
            {
                ctx.Attach(obj);
                Assert.That(obj.EntityState, Is.Not.EqualTo(EntityState.Detached));
                Assert.That(obj.IsAttached, Is.EqualTo(true));
                ObjectContext objCtx = obj.GetEFContext();
                Assert.That(objCtx, Is.Not.Null);
            }
        }

    }

}
