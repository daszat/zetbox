
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
    public class EntityCollectionWrapperTests
        : BasicCollectionTests<EntityCollectionWrapper<ObjectClass, ObjectClass__Implementation__>, ObjectClass>
    {
        protected EntityCollection<ObjectClass__Implementation__> wrappedCollection;

        private IKistlContext ctx;
        private ObjectClass__Implementation__ parent;

        public EntityCollectionWrapperTests(int items)
            : base(items) { }

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
        }

        protected override ObjectClass NewItem()
        {
            var result = ctx.Create<ObjectClass>();
            result.Description = "item#" + result.ID;
            return result;
        }

        protected override EntityCollectionWrapper<ObjectClass, ObjectClass__Implementation__> CreateCollection(List<ObjectClass> items)
        {
            parent = (ObjectClass__Implementation__)ctx.Create<ObjectClass>();
            wrappedCollection = parent.SubClasses__Implementation__;
            foreach (var item in items)
            {
                parent.SubClasses.Add(item);
            }
            return (EntityCollectionWrapper<ObjectClass, ObjectClass__Implementation__>)parent.SubClasses;
        }

        protected override void AssertInvariants(List<ObjectClass> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            Assert.That(wrappedCollection.OrderBy(o => o.GetHashCode()), Is.EquivalentTo(expectedItems.OrderBy(o => o.GetHashCode())));
            
            foreach (var expected in expectedItems.Cast<ObjectClass__Implementation__>())
            {
                Assert.That(expected.BaseObjectClass, Is.EqualTo(parent));
            }

            var removedItems = initialItems.Where(item => !expectedItems.Contains(item)).Cast<ObjectClass__Implementation__>();
            foreach (var removed in removedItems)
            {
                Assert.That(removed.BaseObjectClass, Is.Null);
            }
        }
    }
}
