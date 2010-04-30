
namespace Kistl.API.AbstractConsumerTests.PersistenceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public abstract class ObjectLoadFixture : AbstractTestFixture
    {

        public override void SetUp()
        {
            base.SetUp();
            CreateObject();
            ctx = GetContext();
            obj = GetObject();
        }

        public override void TearDown()
        {
            base.TearDown();
            if (ctx != null)
                ctx.Dispose();
            DestroyObjects();
        }

        protected virtual void CreateObject()
        {
            using (var ctx = GetContext())
            {
                var newObject = ctx.Create<TestCustomObject>();
                newObject.Birthday = new DateTime(1960, 12, 24);
                newObject.PersonName = "Testname";
                ctx.SubmitChanges();
            }
        }

        protected virtual void DestroyObjects()
        {
            using (var ctx = GetContext())
            {
                ctx.GetQuery<TestCustomObject>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        protected virtual TestCustomObject GetObject()
        {
            return ctx.GetQuery<TestCustomObject>().First();
        }

        protected IKistlContext ctx { get; private set; }
        protected TestCustomObject obj { get; private set; }

    }

}
