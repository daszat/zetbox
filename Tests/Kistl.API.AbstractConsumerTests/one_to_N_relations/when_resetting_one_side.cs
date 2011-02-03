
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class when_resetting_one_side
        : OneNFixture
    {
        protected virtual DataObjectState GetExpectedModifiedState()
        {
            return hasSubmitted ? DataObjectState.Modified : DataObjectState.New;
        }

        protected virtual DataObjectState GetExpectedUnmodifiedState()
        {
            return hasSubmitted ? DataObjectState.Unmodified : DataObjectState.New;
        }

        [Test]
        [Category("legacy tests")]
        public void should_work()
        {
            nSide1.OneSide = oneSide1;

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide1), "Setting the first property destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.EquivalentTo(new[] { nSide1 }), "first NSide list not correct");
            Assert.That(oneSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()));
            Assert.That(oneSide2.ObjectState, Is.EqualTo(GetExpectedUnmodifiedState()));
            Assert.That(nSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()));

            nSide1.OneSide = oneSide2;

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide2), "Setting the second property destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.Empty, "first NSide list was not cleared");
            Assert.That(oneSide2.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }), "second NSide list not correct");

            Assert.That(oneSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()));
            Assert.That(oneSide2.ObjectState, Is.EqualTo(GetExpectedModifiedState()));
            Assert.That(nSide1.ObjectState, Is.EqualTo(GetExpectedModifiedState()));

            SubmitAndReload();

            Assert.That(nSide1.OneSide, Is.SameAs(oneSide2), "reloading destroyed the object reference");
            Assert.That(oneSide1.NSide, Is.Empty, "reloading destroyed the first NSide list");
            Assert.That(oneSide2.NSide.ToArray(), Is.EquivalentTo(new[] { nSide1 }), "reloading destroyed the second NSide list");
            Assert.That(oneSide1.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(oneSide2.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(nSide1.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
        }
    }
}
