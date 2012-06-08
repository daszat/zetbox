
namespace Zetbox.DalProvider.Ef.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Text;

    using Autofac;

    using Zetbox.API;
    using Zetbox.API.Tests;
    using Zetbox.App.Projekte;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using NUnit.Framework;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public class RelationListWrapperTests
        : BasicListTests<BSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryEfImpl, EntityCollection<Projekt_haben_Mitarbeiter_RelationEntryEfImpl>>, Mitarbeiter>
    {
        protected EntityCollection<Projekt_haben_Mitarbeiter_RelationEntryEfImpl> wrappedCollection;

        private ProjektEfImpl parent;

        public RelationListWrapperTests(int items)
            : base(items) { }

        public override void SetUp()
        {
            base.SetUp();
        }

        protected override Mitarbeiter NewItem()
        {
            var result = ctx.Create<Mitarbeiter>();
            result.Name = "item#" + result.ID;
            return result;
        }

        protected override BSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryEfImpl, EntityCollection<Projekt_haben_Mitarbeiter_RelationEntryEfImpl>> CreateCollection(List<Mitarbeiter> items)
        {
            parent = (ProjektEfImpl)ctx.Create<Projekt>();
            parent.Name = "proj#" + parent.ID;
            wrappedCollection = parent.MitarbeiterImpl;
            foreach (var item in items)
            {
                parent.Mitarbeiter.Add(item);
            }
            return (BSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryEfImpl, EntityCollection<Projekt_haben_Mitarbeiter_RelationEntryEfImpl>>)parent.Mitarbeiter;
        }

        protected override void AssertInvariants(List<Mitarbeiter> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            Assert.That(wrappedCollection.Select(entry => entry.B).OrderBy(o => o.GetHashCode()), Is.EquivalentTo(expectedItems.OrderBy(o => o.GetHashCode())));

            foreach (var expected in expectedItems.Cast<MitarbeiterEfImpl>())
            {
                Assert.That(expected.Projekte, Has.Member(parent));
            }

            var removedItems = initialItems.Where(item => !expectedItems.Contains(item)).Cast<MitarbeiterEfImpl>();
            foreach (var removed in removedItems)
            {
                Assert.That(removed.Projekte, Has.No.Member(parent));
            }
        }
    }
}
