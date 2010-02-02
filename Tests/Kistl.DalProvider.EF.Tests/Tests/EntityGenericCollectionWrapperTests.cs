
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
    public class EntityGenericCollectionWrapperTests
        : GenericCollectionTests<EntityCollectionWrapper<ObjectClass, ObjectClass__Implementation__>, ObjectClass>
    {
        protected EntityCollection<ObjectClass__Implementation__> wrappedCollection;

        private IContainer innerContainer;
        private IKistlContext ctx;
        private ObjectClass__Implementation__ parent;

        public EntityGenericCollectionWrapperTests(int items)
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

        protected override ObjectClass NewItem()
        {
            EnsureContainer();
            var result = ctx.Create<ObjectClass>();
            result.Description = "item#" + NewItemNumber();
            return result;
        }

        protected override EntityCollectionWrapper<ObjectClass, ObjectClass__Implementation__> CreateCollection(List<ObjectClass> items)
        {
            EnsureContainer();
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
