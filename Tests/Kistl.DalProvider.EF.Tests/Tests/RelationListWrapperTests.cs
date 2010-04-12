
namespace Kistl.DalProvider.EF.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Text;

    using Autofac;

    using Kistl.API;
    using Kistl.API.Tests;
    using Kistl.App.Projekte;

    using NUnit.Framework;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public class RelationListWrapperTests
        : BasicListTests<EntityRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__>, Mitarbeiter>
    {
        protected EntityCollection<Projekt_haben_Mitarbeiter_RelationEntry__Implementation__> wrappedCollection;

        private ILifetimeScope innerContainer;
        private IKistlContext ctx;
        private Projekt__Implementation__ parent;

        public RelationListWrapperTests(int items)
            : base(items) { }

        protected void EnsureContainer()
        {
            if (innerContainer == null)
            {
                innerContainer = KistlContext.Container.BeginLifetimeScope();
                ctx = innerContainer.Resolve<IKistlContext>();
            }
        }

        [TearDown]
        protected void DisposeContainer()
        {
            if (innerContainer != null)
            {
                try
                {
                    innerContainer.Dispose();
                }
                finally
                {
                    innerContainer = null;
                }
            }
        }

        protected override Mitarbeiter NewItem()
        {
            EnsureContainer();
            var result = ctx.Create<Mitarbeiter>();
            result.Name = "item#" + result.ID;
            return result;
        }

        protected override EntityRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__> CreateCollection(List<Mitarbeiter> items)
        {
            EnsureContainer();
            parent = (Projekt__Implementation__)ctx.Create<Projekt>();
            parent.Name = "proj#" + parent.ID;
            wrappedCollection = parent.Mitarbeiter__Implementation__;
            foreach (var item in items)
            {
                parent.Mitarbeiter.Add(item);
            }
            return (EntityRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntry__Implementation__>)parent.Mitarbeiter;
        }

        protected override void AssertInvariants(List<Mitarbeiter> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            Assert.That(wrappedCollection.Select(entry => entry.B).OrderBy(o => o.GetHashCode()), Is.EquivalentTo(expectedItems.OrderBy(o => o.GetHashCode())));

            foreach (var expected in expectedItems.Cast<Mitarbeiter__Implementation__>())
            {
                Assert.That(expected.Projekte, Has.Member(parent));
            }

            var removedItems = initialItems.Where(item => !expectedItems.Contains(item)).Cast<Mitarbeiter__Implementation__>();
            foreach (var removed in removedItems)
            {
                Assert.That(removed.Projekte, Has.No.Member(parent));
            }
        }
    }
}
