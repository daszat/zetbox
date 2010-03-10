
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
    using Kistl.App.Base;
    
    using NUnit.Framework;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public class EntityListWrapperTests
        : BasicListTests<EntityListWrapper<Property, Property__Implementation__>, Property>
    {
        protected EntityCollection<Property__Implementation__> wrappedList;

        private IContainer innerContainer;
        private IKistlContext ctx;
        private ObjectClass__Implementation__ parent;

        public EntityListWrapperTests(int items)
            : base(items) { }

        protected void EnsureContainer()
        {
            if (innerContainer == null)
            {
                innerContainer = KistlContext.Container.CreateInnerContainer();
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

        protected override Property NewItem()
        {
            EnsureContainer();
            var result = ctx.Create<IntProperty>();
            result.Description = "item#" + result.ID;
            return result;
        }

        protected override EntityListWrapper<Property, Property__Implementation__> CreateCollection(List<Property> items)
        {
            EnsureContainer();
            parent = (ObjectClass__Implementation__)ctx.Create<ObjectClass>();
            wrappedList = parent.Properties__Implementation__;
            foreach (var item in items)
            {
                parent.Properties.Add(item);
            }
            return (EntityListWrapper<Property, Property__Implementation__>)parent.Properties;
        }

        protected override void AssertInvariants(List<Property> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            // collection must be sorted like the expected items
            Assert.That(collection, Is.EquivalentTo(expectedItems));

            // the wrapped list has to contain all expected items
            Assert.That(wrappedList.OrderBy(o => o.GetHashCode()), Is.EquivalentTo(expectedItems.OrderBy(o => o.GetHashCode())));

            // the parent pointer has to be set right
            foreach (var expected in expectedItems.Cast<Property__Implementation__>())
            {
                Assert.That(expected.ObjectClass, Is.EqualTo(parent));
                Assert.That(expected.ObjectClass_pos, Is.Not.Null);
            }

            // the removed objects' references must be nulled
            var removedItems = initialItems.Where(item => !expectedItems.Contains(item)).Cast<Property__Implementation__>();
            foreach (var removed in removedItems)
            {
                Assert.That(removed.ObjectClass, Is.Null);
                Assert.That(removed.ObjectClass_pos, Is.Null);
            }
        }
    }
}
