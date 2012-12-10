// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Test;
    using NUnit.Framework;

    public abstract class AbstractContextTests
        : AbstractTestFixture
    {
        protected int firstId;
        protected int secondId;
        protected InterfaceType.Factory iftFactory;

        protected static Guid sequence = new Guid("5c3d9012-a36d-4910-9e7b-1bf7d8f7d09d");
        protected static Guid continuousSequence = new Guid("57a01b4f-940d-4089-b239-fa5a46dc7d00");

        public override void SetUp()
        {
            base.SetUp();
            iftFactory = scope.Resolve<InterfaceType.Factory>();

            using (IZetboxContext ctx = GetContext())
            {
                ctx.GetQuery<TestObjClass>().ForEach(obj => { obj.ObjectProp = null; ctx.Delete(obj); });
                ProjectDataFixture.DeleteData(ctx);
                ctx.SubmitChanges();
            }

            using (IZetboxContext ctx = GetContext())
            {
                var list = new List<TestObjClass>();
                while (list.Count < 2)
                {
                    var newObj = ctx.Create<TestObjClass>();
                    newObj.ObjectProp = null; // kunde;
                    newObj.StringProp = "blah" + list.Count;
                    list.Add(newObj);
                }

                ctx.SubmitChanges();

                firstId = list[0].ID;
                list[0].StringProp = "First";
                list[0].TestEnumProp = TestEnum.First;

                secondId = list[1].ID;
                list[1].StringProp = "Second";
                list[1].TestEnumProp = TestEnum.Second;

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
        public void should_create_objects_with_valid_new_IDs()
        {
            using (var ctx = GetContext())
            {
                int objCount = 10;
                var objs = new List<TestObjClass>();
                while (objCount-- > 0)
                {
                    var obj = ctx.Create<TestObjClass>();
                    Assert.That(obj.ID, Is.LessThan(Zetbox.API.Helper.INVALIDID));
                    obj.StringProp = "Muh " + objCount; // avoid not null constraint
                    objs.Add(obj);
                }
                ctx.SubmitChanges();

                foreach (var obj in objs)
                {
                    Assert.That(obj.ID, Is.GreaterThan(Zetbox.API.Helper.INVALIDID));
                }
            }
        }

        [Test]
        public void should_create_objects_with_different_IDs()
        {
            using (var ctx = GetContext())
            {
                var objList = new List<TestObjClass>();
                int objCount = 10;
                while (objCount-- > 0)
                {
                    objList.Add(ctx.Create<TestObjClass>());
                }
                Assert.That(objList.Select(o => o.ID).ToList(), Is.Unique);
            }
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
        public void rollback_should_not_fail_on_non_existing_transaction()
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

        private TestObjClass CreateTestObjClass(IZetboxContext ctx)
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
                    Assert.That(() => testCtx.Find<TestObjClass>(obj.ID), Throws.ArgumentException);
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
                var currentNumber = s.Data != null ? s.Data.CurrentNumber : 0;
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
                var currentNumber = s.Data != null ? s.Data.CurrentNumber : 0;
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
                ctx.GetSequenceNumber(continuousSequence);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_fail_on_wrong_continuous_sequence_method()
        {
            using (var ctx = GetContext())
            {
                ctx.GetContinuousSequenceNumber(sequence);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void should_fail_on_increment_continuous_sequence_without_transaction()
        {
            using (var ctx = GetContext())
            {
                ctx.GetContinuousSequenceNumber(sequence);
            }
        }

        [Test]
        public void GetContext_returns_a_context()
        {
            IZetboxContext ctx = GetContext();
            Assert.That(ctx, Is.Not.Null);
        }


        [Test]
        public void GetQuery()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        public void Find_T_returns_right_object()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = ctx.Find<TestObjClass>(firstId);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.ID, Is.EqualTo(firstId));
                Assert.That(obj.TestEnumProp, Is.EqualTo(TestEnum.First));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Find_T_fails_on_invalid_ID()
        {
            using (IZetboxContext ctx = GetContext())
            {
                ctx.Find<TestObjClass>(Zetbox.API.Helper.INVALIDID);
            }
        }

        [Test]
        public void Find_ObjectType_returns_right_object()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = (TestObjClass)ctx.Find(iftFactory(typeof(TestObjClass)), firstId);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.ID, Is.EqualTo(firstId));
                Assert.That(obj.TestEnumProp, Is.EqualTo(TestEnum.First));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Find_ObjectType_fails_on_invalid_ID()
        {
            using (IZetboxContext ctx = GetContext())
            {
                ctx.Find(iftFactory(typeof(TestObjClass)), Zetbox.API.Helper.INVALIDID);
            }
        }

        [Test]
        public void GetListOf_T_SubClasses_returns_a_non_empty_list_on_class_DataType()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var obj = ctx.GetQuery<ObjectClass>().First(o => o.Name == "DataType");
                List<ObjectClass> result = ctx.GetListOf<ObjectClass>(obj, "SubClasses").ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetListOf_T_WrongPropertyName_fails()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var obj = ctx.GetQuery<TestObjClass>().First(o => o.ID == firstId);
                ctx.GetListOf<TestObjClass>(obj, "NotAProperty");
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetListOf_T_WrongItemType_fails()
        {
            using (IZetboxContext ctx = GetContext())
            {
                var obj = ctx.GetQuery<ObjectClass>().First(o => o.Name == "DataType");
                ctx.GetListOf<TestObjClass>(obj, "SubClasses").ToList();
            }
        }

        [Test]
        public void UpdateSomeData_SubmitChanges()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = ctx.Find<TestObjClass>(firstId);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.StringProp, Is.EqualTo("First"));

                obj.StringProp = "Test";
                ctx.SubmitChanges();
            }

            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass obj = ctx.Find<TestObjClass>(firstId);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.StringProp, Is.EqualTo("Test"));
            }
        }

        //[Test]
        //[Ignore("Implement IsSorted on TestObjClass.TestName")]
        //public void UpdateLists_SubmitChanges()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        //obj.TestNames[1] = "MuhBlah";

        //        ctx.SubmitChanges();
        //    }

        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        //Assert.That(obj.TestNames[1], Is.EqualTo("MuhBlah"));
        //    }
        //}



        [Test]
        public void Create_Generic()
        {
            using (IZetboxContext ctx = GetContext())
            {
                bool hasCreated = false;
                GenericEventHandler<IPersistenceObject> createdHandler = new GenericEventHandler<IPersistenceObject>(delegate(object obj, GenericEventArgs<IPersistenceObject> e) { hasCreated = true; });
                ctx.ObjectCreated += createdHandler;

                TestObjClass newObj = ctx.Create<TestObjClass>();
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
                Assert.That(hasCreated, Is.True);

                ctx.ObjectCreated -= createdHandler;
            }
        }

        [Test]
        public void Create_Type()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass newObj = ctx.Create(iftFactory(typeof(TestObjClass))) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }

        [Test]
        public void Create_ObjectType()
        {
            using (IZetboxContext ctx = GetContext())
            {
                TestObjClass newObj = ctx.Create(iftFactory(typeof(TestObjClass))) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }


        [Test]
        public void Delete_triggers_ObjectDeleted()
        {
            using (IZetboxContext ctx = GetContext())
            {
                bool hasDeleted = false;
                GenericEventHandler<IPersistenceObject> deletedHandler = new GenericEventHandler<IPersistenceObject>(
                    delegate(object obj, GenericEventArgs<IPersistenceObject> e)
                    {
                        hasDeleted = true;
                    });
                ctx.ObjectDeleted += deletedHandler;

                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result.ToList().Count, Is.GreaterThan(0));

                result.ForEach<TestObjClass>(
                    o => ctx.Delete(o));

                Assert.That(hasDeleted, Is.True);

                ctx.ObjectDeleted -= deletedHandler;
                ctx.SubmitChanges();
            }
        }

        [Test]
        public void Delete_deletes_objects()
        {
            Delete_triggers_ObjectDeleted();
            using (IZetboxContext ctx = GetContext())
            {
                Assert.That(ctx.GetQuery<TestObjClass>().Count(), Is.EqualTo(0));
            }
        }


        //[Test]
        //public void Delete_ICollectionEntry()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        var result = ctx.GetQuery<TestObjClass>();
        //        Assert.That(result.ToList().Count, Is.EqualTo(4));

        //        foreach (TestObjClass obj in result)
        //        {
        //            obj.TestNames.Clear();
        //            Assert.That(obj.TestNames.Count, Is.EqualTo(0));
        //        }
        //    }
        //}


    }
}
