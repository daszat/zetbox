
namespace Zetbox.API.AbstractConsumerTests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public abstract class NMTestFixture
        : AbstractTestFixture
    {
        protected IZetboxContext ctx;
        protected N_to_M_relations_A aSide1;
        protected N_to_M_relations_A aSide2;
        protected N_to_M_relations_B bSide1;
        protected N_to_M_relations_B bSide2;

        [SetUp]
        public virtual void InitTestObjects()
        {
            ctx = GetContext();
            aSide1 = ctx.Create<N_to_M_relations_A>();
            aSide2 = ctx.Create<N_to_M_relations_A>();
            bSide1 = ctx.Create<N_to_M_relations_B>();
            bSide2 = ctx.Create<N_to_M_relations_B>();
        }

        [TearDown]
        public virtual void ForgetTestObjects()
        {
            aSide1 = aSide2 = null;
            bSide1 = bSide2 = null;
            ctx = null;
        }

        protected virtual void SubmitAndReload()
        {
            ctx.SubmitChanges();
            ctx = GetContext();
            aSide1 = ctx.Find<N_to_M_relations_A>(aSide1.ID);
            aSide2 = ctx.Find<N_to_M_relations_A>(aSide2.ID);
            bSide1 = ctx.Find<N_to_M_relations_B>(bSide1.ID);
            bSide2 = ctx.Find<N_to_M_relations_B>(bSide2.ID);
        }
    }
}
