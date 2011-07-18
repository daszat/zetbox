
namespace Kistl.API.AbstractConsumerTests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Test;

    using NUnit.Framework;
    using Kistl.App.Projekte;

    public abstract class when_concurrency
        : AbstractTestFixture
    {
        public override void SetUp()
        {
            base.SetUp();

            using (IKistlContext ctx = GetContext())
            {
                ctx.GetQuery<TestObjClass>().ForEach(obj => { obj.ObjectProp = null; ctx.Delete(obj); });
                ProjectDataFixture.DeleteData(ctx);
                ctx.SubmitChanges();
            }

            using (IKistlContext ctx = GetContext())
            {
                ProjectDataFixture.CreateTestData(ctx);
                var newObj = ctx.Create<TestObjClass>();
                newObj.StringProp = "The one and only";
                newObj.TestEnumProp = TestEnum.First;
                ctx.SubmitChanges();
            }
        }

        public override void TearDown()
        {
            using (var ctx = GetContext())
            {
                ctx.GetQuery<TestObjClass>().ForEach(obj => { obj.ObjectProp = null; ctx.Delete(obj); });
                ProjectDataFixture.DeleteData(ctx);
                ctx.SubmitChanges();
            }
            base.TearDown();
        }

        [Test]
        [ExpectedException(typeof(ConcurrencyException))]
        public void should_fail_when_parallel_changes()
        {
            var ctx1 = GetContext();
            var ctx2 = GetContext();

            var obj1 = ctx1.GetQuery<Projekt>().First();
            var obj2 = ctx2.FindPersistenceObject<Projekt>(obj1.ID);

            Assert.That(obj1.ID, Is.EqualTo(obj2.ID));
            Assert.That(obj1.Name, Is.EqualTo(obj2.Name));

            obj1.Name = "Test1";
            ctx1.SubmitChanges();

            System.Threading.Thread.Sleep(1000); // Wait, wait. Concurrency relies on a timestamp

            obj2.Name = "Test2";
            ctx2.SubmitChanges();
        }

        [Test]
        public void should_allow_serial_changes()
        {
            var ctx1 = GetContext();
            var ctx2 = GetContext();

            var obj1 = ctx1.GetQuery<Projekt>().First();

            obj1.Name = "Test1";
            ctx1.SubmitChanges();

            System.Threading.Thread.Sleep(1000); // Wait, wait. Concurrency relies on a timestamp

            var obj2 = ctx2.FindPersistenceObject<Projekt>(obj1.ID);

            Assert.That(obj1.ID, Is.EqualTo(obj2.ID));
            Assert.That(obj1.Name, Is.EqualTo(obj2.Name));

            obj2.Name = "Test2";
            ctx2.SubmitChanges();
        }

        [Test]
        public void should_allow_parallel_changes_on_non_IChangedBy()
        {
            var ctx1 = GetContext();
            var ctx2 = GetContext();

            var obj1 = ctx1.GetQuery<TestObjClass>().First();
            var obj2 = ctx2.FindPersistenceObject<TestObjClass>(obj1.ID);

            Assert.That(obj1.ID, Is.EqualTo(obj2.ID));
            Assert.That(obj1.StringProp, Is.EqualTo(obj2.StringProp));

            obj1.StringProp = "Test1";
            ctx1.SubmitChanges();

            System.Threading.Thread.Sleep(1000); // Wait, wait. Concurrency relies on a timestamp

            obj2.StringProp = "Test2";
            ctx2.SubmitChanges();
        }
    }
}
