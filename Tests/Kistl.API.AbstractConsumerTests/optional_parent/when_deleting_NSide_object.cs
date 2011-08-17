
namespace Kistl.API.AbstractConsumerTests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class when_deleting_NSide_object
        : OneNFixture
    {
        public override void InitTestObjects()
        {
            base.InitTestObjects();
            // prepare
            oneSide1.NSide.Add(nSide1);
            oneSide1.NSide.Add(nSide2);
            SubmitAndReload();
        }

        public void should_not_fail()
        {
            ctx.Delete(nSide1);
            ctx.SubmitChanges();
        }
    }
}
