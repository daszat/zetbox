
namespace Kistl.API.AbstractConsumerTests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Test;
    using NUnit.Framework;

    public abstract class when_deleting
        : NMTestFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            aSide1.BSide.Add(bSide1);
            SubmitAndReload();
        }

        [Test]
        public void should_delete_RelationEntry()
        {
            aSide1.BSide.Remove(bSide1);
            Assert.That(ctx.AttachedObjects.OfType<IRelationEntry>().Single().ObjectState, Is.EqualTo(DataObjectState.Deleted));
        }

        [Test]
        public void should_remove_from_Collection()
        {
            ctx.Delete(bSide1);

            // TODO: Remove the manual Remove() here.
            aSide1.BSide.Remove(bSide1);
            Assert.That(aSide1.BSide, Has.No.Member(bSide1));

            ctx.SubmitChanges();
        }
    }
}
