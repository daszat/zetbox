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
        : ICollectionTests<OneNRelationList<INSide>, INSide>
    {
        public OneNRelationTests(int items)
            : base(items) { }

        internal OneSide obj;
        internal OneNRelationList<INSide> wrapper { get { return obj.List; } }

        protected override INSide NewItem()
        {
            return new NSide() { Description = "item#" + NewItemNumber() };
        }

        protected override OneNRelationList<INSide> CreateCollection(List<INSide> items)
        {
            obj = new OneSide(items);
            return wrapper;
        }

        protected override void AssertInvariants(List<INSide> expectedItems)
        {
            base.AssertInvariants(expectedItems);
        }
    }
}
