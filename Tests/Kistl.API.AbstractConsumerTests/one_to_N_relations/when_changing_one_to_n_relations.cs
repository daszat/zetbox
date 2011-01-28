
namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;

    using NUnit.Framework;
    
    public abstract class when_changing_one_to_n_relations
        : AbstractTestFixture
    {
        protected IKistlContext ctx;
        protected One_to_N_relations_N nSide;
        protected One_to_N_relations_One oneSide;

        protected abstract void DoModification();

        protected void SubmitAndReload()
        {
            ctx.SubmitChanges();
            ctx = GetContext();
            nSide = ctx.Find<One_to_N_relations_N>(nSide.ID);
            oneSide = ctx.Find<One_to_N_relations_One>(oneSide.ID);
        }

        [SetUp]
        public void CreateData()
        {
            ctx = GetContext();
            nSide = ctx.Create<One_to_N_relations_N>();
            oneSide = ctx.Create<One_to_N_relations_One>();
            SubmitAndReload();
        }

        [TearDown]
        public void ForgetContext()
        {
            nSide = null;
            oneSide = null;
            ctx = null;
        }

        [Test]
        public void should_notify_OneSide_property()
        {
            TestChangeNotification(nSide, "OneSide",
                DoModification,
                () => { Assert.That(oneSide.NSide, Has.No.Member(nSide), "changing event should be triggered before the value has changed"); },
                () => { Assert.That(oneSide.NSide, Has.Member(nSide), "changed event should be triggered after the value has changed"); }
            );
        }

        [Test]
        public void should_notify_NSide_property()
        {
            TestChangeNotification(oneSide, "NSide",
                DoModification,
                null,
                null
            );
        }

        [Test]
        public void should_set_one_side_modified()
        {
            DoModification();

            Assert.That(oneSide.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        [Test]
        public void should_set_n_side_modified()
        {
            DoModification();

            Assert.That(nSide.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }

        public abstract void should_persist_OneSide_property_value();
        
        public abstract void should_persist_NSide_property_value();
    }
}
