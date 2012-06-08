// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

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
    public class EntityListWrapperTests
        : BasicListTests<EntityListWrapper<Property, PropertyEfImpl>, Property>
    {
        protected EntityCollection<PropertyEfImpl> wrappedList;

        private ObjectClassEfImpl parent;

        public EntityListWrapperTests(int items)
            : base(items) { }

        public override void SetUp()
        {
            base.SetUp();
        }

        protected override Property NewItem()
        {
            var result = ctx.Create<IntProperty>();
            result.Description = "item#" + result.ID;
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
