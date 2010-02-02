using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client.Mocks.OneNLists;
    using Kistl.API.Tests;

    using NUnit.Framework;

    [TestFixture(0)]
    [TestFixture(1)]
    [TestFixture(10)]
    [TestFixture(50)]
    public class OneNRelationTests
        : IListTests<OneNRelationList<INSide>, INSide>
    {
        public OneNRelationTests(int items)
            : base(items) { }

        internal OneSide obj;
        internal OneNRelationList<INSide> wrapper { get { return obj.List; } }

        protected override INSide NewItem()
        {
            var id = NewItemNumber();
            return new NSide() { ID = id, Description = "item#" + id };
        }

        protected override OneNRelationList<INSide> CreateCollection(List<INSide> items)
        {
            obj = new OneSide(items.Cast<INSide>().ToList());
            for (int i = 0; i < items.Count; i++)
            {
                var item = (NSide)items[i];
                item.OneSide = obj;
                item.OneSide_pos = i * 10;
            }
            return wrapper;
        }

        protected override void AssertInvariants(List<INSide> expectedItems)
        {
            base.AssertInvariants(expectedItems);

            // we also expect the collection to be stable, i.e. not change the order of the items
            Assert.That(collection, Is.EquivalentTo(expectedItems));

            foreach (var expected in expectedItems.Cast<NSide>())
            {
                //Assert.That(expected.OneSide, Is.SameAs(obj));
                Assert.That(expected.LastParentId, Is.EqualTo(obj.ID));
                Assert.That(expected.OneSide_pos, Is.Not.Null);
            }

            Assert.That(collection.OfType<NSide>().Select(ns => ns.OneSide_pos).ToArray(), Is.Ordered);
        }
    }
}
