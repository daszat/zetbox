
namespace Kistl.API.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Tests;
    using Kistl.App.Projekte;

    using NUnit.Framework;
    using Kistl.API.Client.Mocks;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public class NMRelationBSideTests
        : IListTests<ClientRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__>, Mitarbeiter>
    {
        private IKistlContext ctx;
        private Projekt__Implementation__ parent;

        public NMRelationBSideTests(int items)
            : base(items) { }

        protected void EnsureContext()
        {
            if (ctx == null)
            {
                var testCtx = new ClientApplicationContextMock();
                testCtx.SetInterfaceAssembly_Objects();
                ctx = KistlContext.GetContext();
            }
        }

        [TearDown]
        protected void DisposeContext()
        {
            if (ctx != null)
            {
                ctx = null;
            }
        }

        protected override Mitarbeiter NewItem()
        {
            EnsureContext();
            var result = ctx.Create<Mitarbeiter>();
            result.Name = "item#" + NewItemNumber();
            return result;
        }

        protected override ClientRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__> CreateCollection(List<Mitarbeiter> items)
        {
            EnsureContext();
            parent = (Projekt__Implementation__)ctx.Create<Projekt>();
            parent.Name = "proj#" + NewItemNumber();
            foreach (var item in items)
            {
                parent.Mitarbeiter.Add(item);
            }
            return (ClientRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__>)parent.Mitarbeiter;
        }
    }
}
