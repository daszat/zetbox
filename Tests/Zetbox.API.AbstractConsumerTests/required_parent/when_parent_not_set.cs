
namespace Zetbox.API.AbstractConsumerTests.required_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public class when_parent_not_set
        : RequiredParentFixture
    {
        [Test]
        public void should_not_submit()
        {
            Assert.That(() => ctx.SubmitChanges(), Throws.Exception);
        }

        [Test]
        public void should_delete_unsaved_objects()
        {
            ctx.Delete(oneSide1);
            ctx.Delete(oneSide2);
            ctx.Delete(oneSide3);
            ctx.Delete(nSide1);
            ctx.Delete(nSide2);

            ctx.SubmitChanges();
        }
    }
}
