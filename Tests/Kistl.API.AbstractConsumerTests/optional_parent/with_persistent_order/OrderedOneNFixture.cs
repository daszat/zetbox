
namespace Kistl.API.AbstractConsumerTests.optional_parent.with_persistent_order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class OrderedOneNFixture
        : AbstractTestFixture
    {
        protected IKistlContext ctx;
        protected OrderedOneEnd oneSide1;
        protected OrderedOneEnd oneSide2;
        protected OrderedOneEnd oneSide3;
        protected OrderedNEnd nSide1;
        protected OrderedNEnd nSide2;
        protected OrderedNEnd nSide3;

        [SetUp]
        public virtual void InitTestObjects()
        {
            ctx = GetContext();
            oneSide1 = ctx.Create<OrderedOneEnd>();
            oneSide2 = ctx.Create<OrderedOneEnd>();
            oneSide3 = ctx.Create<OrderedOneEnd>();
            nSide1 = ctx.Create<OrderedNEnd>();
            nSide2 = ctx.Create<OrderedNEnd>();
            nSide3 = ctx.Create<OrderedNEnd>();
        }

        [TearDown]
        public void ForgetTestObjects()
        {
            ctx = null;
            oneSide1 = oneSide2 = oneSide3 = null;
            nSide1 = nSide2 = nSide3 = null;
        }

        protected void SubmitAndReload()
        {
            ctx.SubmitChanges();
            ctx = GetContext();
            oneSide1 = ctx.Find<OrderedOneEnd>(oneSide1.ID);
            oneSide2 = ctx.Find<OrderedOneEnd>(oneSide2.ID);
            oneSide3 = ctx.Find<OrderedOneEnd>(oneSide3.ID);
            nSide1 = ctx.Find<OrderedNEnd>(nSide1.ID);
            nSide2 = ctx.Find<OrderedNEnd>(nSide2.ID);
            nSide3 = ctx.Find<OrderedNEnd>(nSide3.ID);
        }
    }
}
