
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations.with_persistent_order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    public abstract class when_adding_multiple_items
        : OrderedOneNFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();

            // add in inverse creation oder to try(!) to provoke IDs != _pos order
            oneSide1.NEnds.Add(nSide2);
            oneSide1.NEnds.Add(nSide1);
        }

        [Test]
        public void should_persist_order()
        {
            Assert.That(oneSide1.NEnds, Is.EqualTo(new[] { nSide2, nSide1 }));
        }
    }
}
