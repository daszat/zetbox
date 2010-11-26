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
        private static Guid guidTestModule = new Guid("81e8ce31-65eb-46fe-ba86-de7452692d5b");
        protected static Guid sequence = new Guid("5c3d9012-a36d-4910-9e7b-1bf7d8f7d09d");
        protected static Guid continuousSequence = new Guid("57a01b4f-940d-4089-b239-fa5a46dc7d00");

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

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_fail_on_existing_transaction()
        {
            using (var ctx = GetContext())
            {
                ctx.BeginTransaction();
                ctx.BeginTransaction();
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void commit_should_fail_on_non_existing_transaction()
        {
            using (var ctx = GetContext())
            {
                ctx.CommitTransaction();
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void rollback_should_fail_on_non_existing_transaction()
        {
            using (var ctx = GetContext())
            {
                ctx.RollbackTransaction();
            }
        }

        [Test]
        public void should_commit_empty_transaction()
        {
            using (var ctx = GetContext())
            {
                ctx.BeginTransaction();
                ctx.CommitTransaction();
            }
        }

        [Test]
        public void should_rollback_empty_transaction()
        {
            using (var ctx = GetContext())
            {
                ctx.BeginTransaction();
                ctx.RollbackTransaction();
            }
        }

        private TestObjClass CreateTestObjClass(IKistlContext ctx)
        {
            var result = ctx.Create<TestObjClass>();
            result.MyIntProperty = 1;
            result.StringProp = "test";
            result.TestEnumProp = TestEnum.First;
            return result;
        }

        [Test]
        [Ignore("SQL Server locks the table")]
        public void uncommited_data_should_not_be_visible()
        {
            using (var ctx = GetContext())
            using (var testCtx = GetContext())
            {
                List<TestObjClass> objects = new List<TestObjClass>();
                ctx.BeginTransaction();

                objects.Add(CreateTestObjClass(ctx));
                objects.Add(CreateTestObjClass(ctx));
                ctx.SubmitChanges();

                objects.Add(CreateTestObjClass(ctx));
                objects.Add(CreateTestObjClass(ctx));
                ctx.SubmitChanges();

                // SQL Server locks the table
                //foreach (var obj in objects)
                //{
                //    Assert.That(obj.ID, Is.GreaterThan(Helper.INVALIDID));
                //    var testObj = testCtx.Find<TestObjClass>(obj.ID);
                //    Assert.That(testObj, Is.Null);
                //}

                ctx.CommitTransaction();

                foreach (var obj in objects)
                {
                    Assert.That(obj.ID, Is.GreaterThan(Helper.INVALIDID));
                    var testObj = testCtx.Find<TestObjClass>(obj.ID);
                    Assert.That(testObj, Is.Not.Null);
                }
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void should_rollbacktransaction()
        {
            using (var ctx = GetContext())
            using (var testCtx = GetContext())
            {
                List<TestObjClass> objects = new List<TestObjClass>();
                ctx.BeginTransaction();

                objects.Add(CreateTestObjClass(ctx));
                objects.Add(CreateTestObjClass(ctx));
                ctx.SubmitChanges();

                objects.Add(CreateTestObjClass(ctx));
                objects.Add(CreateTestObjClass(ctx));
                ctx.SubmitChanges();

                // SQL Server locks the table
                //foreach (var obj in objects)
                //{
                //    Assert.That(obj.ID, Is.GreaterThan(Helper.INVALIDID));
                //    var testObj = testCtx.Find<TestObjClass>(obj.ID);
                //    Assert.That(testObj, Is.Null);
                //}

                ctx.RollbackTransaction();

                foreach (var obj in objects)
                {
                    Assert.That(obj.ID, Is.GreaterThan(Helper.INVALIDID));
                    var testObj = testCtx.Find<TestObjClass>(obj.ID);
                    Assert.That(testObj, Is.Null);
                }
            }
        }

        [Test]
        public void should_increment_sequence()
        {
            using (var ctx = GetContext())
            {
                var s = ctx.FindPersistenceObject<Sequence>(sequence);
                Assert.That(s.IsContinuous, Is.False);
                var currentNumber = s.CurrentNumber;
                var next = ctx.GetSequenceNumber(sequence);
                Assert.That(next, Is.EqualTo(currentNumber + 1));
            }
        }

        [Test]
        public void should_increment_continuous_sequence()
        {
            using (var ctx = GetContext())
            {
                ctx.BeginTransaction();
                var s = ctx.FindPersistenceObject<Sequence>(continuousSequence);
                Assert.That(s.IsContinuous, Is.True);
                var currentNumber = s.CurrentNumber;
                var next = ctx.GetContinuousSequenceNumber(continuousSequence);
                Assert.That(next, Is.EqualTo(currentNumber + 1));
                ctx.CommitTransaction();
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_fail_on_wrong_sequence_method()
        {
            using (var ctx = GetContext())
            {
                var next = ctx.GetSequenceNumber(continuousSequence);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_fail_on_wrong_continuous_sequence_method()
        {
            using (var ctx = GetContext())
            {
                var next = ctx.GetContinuousSequenceNumber(sequence);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_fail_on_increment_continuous_sequence_without_transaction()
        {
            using (var ctx = GetContext())
            {
                var next = ctx.GetContinuousSequenceNumber(sequence);
            }
        }
    }
}
