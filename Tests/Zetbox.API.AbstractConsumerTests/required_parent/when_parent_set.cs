
namespace Zetbox.API.AbstractConsumerTests.required_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public class when_parent_set
        : RequiredParentFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            nSide1.Parent = oneSide1;
            nSide2.Parent = oneSide1;
        }

        [Test]
        public void should_submit()
        {
            ctx.SubmitChanges();
        }

        [Test]
        public void should_delete_saved_objects()
        {
            SubmitAndReload();

            ctx.Delete(oneSide1);
            ctx.Delete(oneSide2);
            ctx.Delete(oneSide3);
            ctx.Delete(nSide1);
            ctx.Delete(nSide2);

            ctx.SubmitChanges();
        }
    }
}
