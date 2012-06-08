
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
    public class EntityGenericListWrapperTests
        : GenericListTests<EntityListWrapper<Property, PropertyEfImpl>, Property>
    {
        protected EntityCollection<PropertyEfImpl> wrappedList;

        private ObjectClassEfImpl parent;

        public EntityGenericListWrapperTests(int items)
            : base(items) { }

        public override void SetUp()
        {
            base.SetUp();
        }

        protected override Property NewItem()
        {
            var result = ctx.Create<IntProperty>();
            result.Description = "item#" + NewItemNumber();
            return result;
        }

        protected override EntityListWrapper<Property, PropertyEfImpl> CreateCollection(List<Property> items)
        {
            parent = (ObjectClassEfImpl)ctx.Create<ObjectClass>();
            wrappedList = parent.PropertiesImpl;
            foreach (var item in items)
            {
                parent.Properties.Add(item);
            }
            return (EntityListWrapper<Property, PropertyEfImpl>)parent.Properties;
        }

        protected override void AssertInvariants(List<Property> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            // collection must be sorted like the expected items
            Assert.That(collection, Is.EquivalentTo(expectedItems));

            // the wrapped list has to contain all expected items
            Assert.That(wrappedList.OrderBy(o => o.GetHashCode()), Is.EquivalentTo(expectedItems.OrderBy(o => o.GetHashCode())));

            // the parent pointer has to be set right
            foreach (var expected in expectedItems.Cast<PropertyEfImpl>())
            {
                Assert.That(expected.ObjectClass, Is.EqualTo(parent));
                Assert.That(expected.Properties_pos, Is.Not.Null);
            }

            // the removed objects' references must be nulled
            var removedItems = initialItems.Where(item => !expectedItems.Contains(item)).Cast<PropertyEfImpl>();
            foreach (var removed in removedItems)
            {
                Assert.That(removed.ObjectClass, Is.Null);
                Assert.That(removed.Properties_pos, Is.Null);
            }
        }
    }
}
