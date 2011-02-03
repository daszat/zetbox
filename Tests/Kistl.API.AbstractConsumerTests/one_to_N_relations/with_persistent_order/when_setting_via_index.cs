
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations.with_persistent_order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    public abstract class when_setting_via_index
        : OrderedOneNFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();

            oneSide1.NEnds.Add(nSide1);
            oneSide1.NEnds.Add(nSide2);
        }

        [Test]
        public void should_replace_first_item()
        {
            oneSide1.NEnds[0] = nSide3;
            Assert.That(oneSide1.NEnds, Is.EqualTo(new[] { nSide3, nSide2 }));
        }

        [Test]
        public void should_replace_second_item()
        {
            oneSide1.NEnds[1] = nSide3;
            Assert.That(oneSide1.NEnds, Is.EqualTo(new[] { nSide1, nSide3 }));
        }
    }
}
