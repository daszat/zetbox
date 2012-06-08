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
    public class EntityCollectionWrapperTests
        : BasicCollectionTests<EntityCollectionWrapper<ObjectClass, ObjectClassEfImpl>, ObjectClass>
    {
        protected EntityCollection<ObjectClassEfImpl> wrappedCollection;

        private ObjectClassEfImpl parent;

        public EntityCollectionWrapperTests(int items)
            : base(items) { }

        public override void SetUp()
        {
            base.SetUp();
        }

        protected override ObjectClass NewItem()
        {
            var result = ctx.Create<ObjectClass>();
            result.Description = "item#" + result.ID;
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
