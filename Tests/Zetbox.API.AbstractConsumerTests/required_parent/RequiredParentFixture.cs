
namespace Kistl.API.AbstractConsumerTests.required_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class RequiredParentFixture
        : AbstractTestFixture
    {
        protected IKistlContext ctx;
        protected RequiredParent oneSide1;
        protected RequiredParent oneSide2;
        protected RequiredParent oneSide3;
        protected RequiredParentChild nSide1;
        protected RequiredParentChild nSide2;
        protected bool hasSubmitted = false;

        [SetUp]
        public virtual void InitTestObjects()
        {
            ctx = GetContext();
            oneSide1 = ctx.Create<RequiredParent>();
            oneSide2 = ctx.Create<RequiredParent>();
            oneSide3 = ctx.Create<RequiredParent>();
            nSide1 = ctx.Create<RequiredParentChild>();
            nSide2 = ctx.Create<RequiredParentChild>();
        }

        [TearDown]
        public void ForgetTestObjects()
        {
            ctx = null;
            oneSide1 = oneSide2 = oneSide3 = null;
            nSide1 = nSide2 = null;
            hasSubmitted = false;
        }

        protected void SubmitAndReload()
        {
            ctx.SubmitChanges();
            hasSubmitted = true;
            ctx = GetContext();
            oneSide1 = ctx.Find<RequiredParent>(oneSide1.ID);
            oneSide2 = ctx.Find<RequiredParent>(oneSide2.ID);
            oneSide3 = ctx.Find<RequiredParent>(oneSide3.ID);
            nSide1 = ctx.Find<RequiredParentChild>(nSide1.ID);
            nSide2 = ctx.Find<RequiredParentChild>(nSide2.ID);
        }
    }
}
