using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Projekte;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests
{

    public abstract class AbstractReadonlyContextTests : AbstractTestFixture
    {
        [Test]
        public void query_should_execute_twice()
        {
            using (var ctx = GetContext())
            {
                var query = ctx.GetQuery<TestObjClass>();

                // call linq2objects OrderBy to avoid touching other parts of the provider
                var results1 = query.ToList().OrderBy(t => t.ID);
                var results2 = query.ToList().OrderBy(t => t.ID);

                Assert.That(results2, Is.EquivalentTo(results1), "Query.ToList() didn't return the same collection on the second execution");
            }
        }

        [Test]
        public void query_should_be_reusable()
        {
            using (var ctx = GetContext())
            {
                var query = ctx.GetQuery<TestObjClass>();

                // call providers' OrderBy to create two different queries
                var results1 = query.OrderBy(t => t.ID).ToList();
                var results2 = query.OrderBy(t => t.ID).ToList();

                Assert.That(results2, Is.EquivalentTo(results1), "Query.ToList() didn't return the same collection on the second execution");
            }
        }

        [Test]
        public void query_should_be_reusable_nesting()
        {
            using (var ctx = GetContext())
            {
                var query = ctx.GetQuery<TestObjClass>().Where(t => t.ID > 2).OrderBy(t => t.ID);

                var results1 = query.ToList();
                // reuse queried query again, nested in another query
                var results2 = query.Where(t => t.ID > 2).ToList();

                Assert.That(results2, Is.EquivalentTo(results1), "Query.ToList() didn't return the same collection on the second execution");
            }
        }
    }

    public abstract class AbstractContextTests : AbstractReadonlyContextTests
    {
        public override void SetUp()
        {
            base.SetUp();
            using (var ctx = GetContext())
            {
                ProjectDataFixture.DeleteData(ctx);
                ctx.GetQuery<TestObjClass>().ForEach(obj => { obj.ObjectProp = null; ctx.Delete(obj); });
                ProjectDataFixture.CreateTestData(ctx);
                ctx.SubmitChanges();
            }
        }

        public override void TearDown()
        {
            using (var ctx = GetContext())
            {
                ProjectDataFixture.DeleteData(ctx);
                ctx.GetQuery<TestObjClass>().ForEach(obj => { obj.ObjectProp = null; ctx.Delete(obj); });
                ctx.SubmitChanges();
            }
            base.TearDown();
        }

        [Test]
        public void should_find_new_objects()
        {
            using (var ctx = GetContext())
            {
                var obj = ctx.Create<TestObjClass>();
                Assert.That(ctx.Find<TestObjClass>(obj.ID), Is.SameAs(obj));
            }

        }

        [Test]
        [Ignore("Discuss")]
        public void should_create_objects_with_valid_IDs()
        {
            using (var ctx = GetContext())
            {
                int objCount = 10;
                while (objCount-- > 0)
                {
                    var obj = ctx.Create<TestObjClass>();
                    Assert.That(obj.ID, Is.Not.EqualTo(Kistl.API.Helper.INVALIDID));
                }
            }
        }

        [Test]
        [Ignore("Discuss")]
        public void should_create_objects_with_different_IDs()
        {
            var objList = new List<TestObjClass>();
            using (var ctx = GetContext())
            {
                int objCount = 10;
                while (objCount-- > 0)
                {
                    objList.Add(ctx.Create<TestObjClass>());
                }
            }

            Assert.DoesNotThrow(delegate()
            {
                // throws exception on duplicate keys
                objList.ToDictionary(o => o.ID);
            });
        }


    }
}
