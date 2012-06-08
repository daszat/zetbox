
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
    using Zetbox.App.Base;
    
    using NUnit.Framework;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public class EntityGenericCollectionWrapperTests
        : GenericCollectionTests<EntityCollectionWrapper<ObjectClass, ObjectClassEfImpl>, ObjectClass>
    {
        protected EntityCollection<ObjectClassEfImpl> wrappedCollection;

        private ObjectClassEfImpl parent;

        public EntityGenericCollectionWrapperTests(int items)
            : base(items) { }

        public override void SetUp()
        {
            base.SetUp();
        }


        protected override ObjectClass NewItem()
        {
            var result = ctx.Create<ObjectClass>();
            result.Description = "item#" + NewItemNumber();
            return result;
        }

        protected override EntityCollectionWrapper<ObjectClass, ObjectClassEfImpl> CreateCollection(List<ObjectClass> items)
        {
            parent = (ObjectClassEfImpl)ctx.Create<ObjectClass>();
            wrappedCollection = parent.SubClassesImpl;
            foreach (var item in items)
            {
                parent.SubClasses.Add(item);
            }
            return (EntityCollectionWrapper<ObjectClass, ObjectClassEfImpl>)parent.SubClasses;
        }

        protected override void AssertInvariants(List<ObjectClass> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            Assert.That(wrappedCollection.OrderBy(o => o.GetHashCode()), Is.EquivalentTo(expectedItems.OrderBy(o => o.GetHashCode())));

            foreach (var expected in expectedItems.Cast<ObjectClassEfImpl>())
            {
                Assert.That(expected.BaseObjectClass, Is.EqualTo(parent));
            }

            var removedItems = initialItems.Where(item => !expectedItems.Contains(item)).Cast<ObjectClassEfImpl>();
            foreach (var removed in removedItems)
            {
                Assert.That(removed.BaseObjectClass, Is.Null);
            }
        }
    }
}
