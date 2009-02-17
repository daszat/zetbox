using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.API.Server.Mocks;
using Kistl.App.Test;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class KistlDataContextTests
    {
        [SetUp]
        public void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var result = ctx.GetQuery<TestObjClass>();
                var list = result.ToList();

                list[0].StringProp = "First";
                list[0].TestEnumProp = TestEnum.First;

                //list[1].StringProp = "Second";
                //list[1].TestEnumProp = TestEnum.Second;

                ctx.SubmitChanges();
            }
        }

        [Test]
        public void GetContext()
        {
            IKistlContext ctx = KistlContext.GetContext();
            Assert.That(ctx, Is.Not.Null);
        }

        [Test]
        public void InitSession()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                Assert.That(ctx, Is.Not.Null);
                Assert.That(KistlContext.Current, Is.Not.Null);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitSessionTwice()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                Assert.That(ctx, Is.Not.Null);
                Assert.That(KistlContext.Current, Is.Not.Null);
                using (IKistlContext ctx2 = KistlContext.InitSession())
                {
                    Assert.That(ctx2, Is.Not.Null);
                    Assert.That(KistlContext.Current, Is.Not.Null);
                }
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoSession()
        {
            Assert.That(KistlContext.Current, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CleanUpSession()
        {
            Assert.That(KistlContext.Current, Is.Null);
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                Assert.That(ctx, Is.Not.Null);
                Assert.That(KistlContext.Current, Is.Not.Null);
            }
            Assert.That(KistlContext.Current, Is.Null);
        }

        [Test]
        public void GetQuery()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        [Ignore]
        public void GetQuery_ObjType()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                var result = ctx.GetQuery(typeof(TestObjClass));
                Assert.That(result, Is.Not.Null);
                var testObj = result.First(o => o.ID == 1);
                Assert.That(testObj, Is.Not.Null);
            }
        }

        [Test]
        public void Find_T()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = ctx.Find<TestObjClass>(1);
                Assert.That(obj, Is.Not.Null);
            }
        }

        [Test]
        public void Find_ObjectType()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = (TestObjClass)ctx.Find(typeof(TestObjClass), 1);
                Assert.That(obj, Is.Not.Null);
            }
        }

        [Test]
        public void GetListOf()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                var obj = ctx.GetQuery<TestObjClass>().First(o => o.ID == 1);
                var result = ctx.GetListOf<TestObjClass>(obj, "SubClasses").ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void GetListOf_ObjType()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                var result = ctx.GetListOf<TestObjClass>(typeof(TestObjClass), 1, "SubClasses").ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(2));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetListOf_WrongPropertyName()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                var obj = ctx.GetQuery<TestObjClass>().First(o => o.ID == 1);
                var result = ctx.GetListOf<TestObjClass>(obj, "NotAProperty");
            }
        }

        [Test]
        public void SelectSomeData()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(4));
            }
        }

        [Test]
        public void UpdateSomeData_SubmitChanges()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.StringProp, Is.EqualTo("First"));

                obj.StringProp = "Test";
                ctx.SubmitChanges();
            }

            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.StringProp, Is.EqualTo("Test"));
            }
        }

        //[Test]
        //[Ignore("Implement IsSorted on TestObjClass.TestName")]
        //public void UpdateLists_SubmitChanges()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        //obj.TestNames[1] = "MuhBlah";

        //        ctx.SubmitChanges();
        //    }

        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        //Assert.That(obj.TestNames[1], Is.EqualTo("MuhBlah"));
        //    }
        //}

        [Test]
        public void Attach_IDataObject_New()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = new TestObjClass__Implementation__();
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Attach(obj);
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Added));
            }
        }

        //[Test]
        //public void Attach_IDataObject_New_WithGraph()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        TestObjClass obj = new TestObjClass__Implementation__();
        //        obj.TestNames.Add("Test");
        //        obj.TestNames.Add("Test2");
        //        Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Added));
        //        Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(3));
        //    }
        //}

        [Test]
        public void Attach_IDataObject_Existing()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = new TestObjClass__Implementation__() { ID = 3 };
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Attach(obj);
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Unchanged));
            }
        }

        [Test]
        public void Attach_IDataObject_Existing_Twice()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = new TestObjClass__Implementation__() { ID = 3 };
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Attach(obj);
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Unchanged));
                ctx.Attach(obj);
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Unchanged));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Attach_IDataObject_Existing_Twice_But_Different()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj1 = new TestObjClass__Implementation__() { ID = 3 };
                Assert.That(((TestObjClass__Implementation__)obj1).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Attach(obj1);
                Assert.That(((TestObjClass__Implementation__)obj1).EntityState, Is.EqualTo(EntityState.Unchanged));

                TestObjClass obj2 = new TestObjClass__Implementation__() { ID = 3 };
                Assert.That(((TestObjClass__Implementation__)obj2).EntityState, Is.EqualTo(EntityState.Detached));
                ctx.Attach(obj2);
                Assert.That(((TestObjClass__Implementation__)obj2).EntityState, Is.EqualTo(EntityState.Unchanged));
            }
        }

        //[Test]
        //public void Attach_ICollectionEntry_New()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        TestObjClass_TestNameCollectionEntry__Implementation__ obj = new TestObjClass_TestNameCollectionEntry__Implementation__();
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Added));
        //    }
        //}

        //[Test]
        //public void Attach_ICollectionEntry_Existing()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        TestObjClass_TestNameCollectionEntry__Implementation__ obj = new TestObjClass_TestNameCollectionEntry__Implementation__() { ID = 15 };
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Unchanged));
        //    }
        //}

        //[Test]
        //public void Attach_ICollectionEntry_Existing_Twice()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        TestObjClass_TestNameCollectionEntry__Implementation__ obj = new TestObjClass_TestNameCollectionEntry__Implementation__() { ID = 3 };
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Unchanged));
        //        ctx.Attach(obj);
        //        Assert.That(obj.EntityState, Is.EqualTo(EntityState.Unchanged));
        //    }
        //}

        //[Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void Attach_ICollectionEntry_Existing_Twice_But_Different()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        TestObjClass_TestNameCollectionEntry__Implementation__ obj1 = new TestObjClass_TestNameCollectionEntry__Implementation__() { ID = 3 };
        //        Assert.That(obj1.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj1);
        //        Assert.That(obj1.EntityState, Is.EqualTo(EntityState.Unchanged));

        //        TestObjClass_TestNameCollectionEntry__Implementation__ obj2 = new TestObjClass_TestNameCollectionEntry__Implementation__() { ID = 3 };
        //        Assert.That(obj2.EntityState, Is.EqualTo(EntityState.Detached));
        //        ctx.Attach(obj2);
        //        Assert.That(obj2.EntityState, Is.EqualTo(EntityState.Unchanged));
        //    }
        //}

        [Test]
        public void AttachedObjects()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = new TestObjClass__Implementation__();
                ctx.Attach(obj);
                ctx.Create<TestObjClass>();

                Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(2));
            }
        }

        [Test]
        public void ContainsObject()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = new TestObjClass__Implementation__() { ID = 10 };
                ctx.Attach(obj);
                ctx.Create<TestObjClass>();
                Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(2));

                Assert.That(ctx.ContainsObject(obj.GetType(), obj.ID), Is.EqualTo(obj));
            }
        }

        [Test]
        public void ContainsObject_Not()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = new TestObjClass__Implementation__() { ID = 10 };
                ctx.Create<TestObjClass>();
                Assert.That(ctx.AttachedObjects.Count(), Is.EqualTo(1));

                Assert.That(ctx.ContainsObject(obj.GetType(), obj.ID), Is.Null);
            }
        }

        [Test]
        public void Create_Generic()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
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
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass newObj = ctx.Create(typeof(TestObjClass)) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }

        [Test]
        public void Create_ObjectType()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass newObj = ctx.Create(typeof(TestObjClass)) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }


        [Test]
        public void Delete_IDataObject()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                bool hasDeleted = false;
                GenericEventHandler<IPersistenceObject> deletedHandler = new GenericEventHandler<IPersistenceObject>(delegate(object obj, GenericEventArgs<IPersistenceObject> e) { hasDeleted = true; });
                ctx.ObjectDeleted += deletedHandler;

                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(4));

                result.ForEach<TestObjClass>(
                    o => ctx.Delete(o));

                Assert.That(hasDeleted, Is.True);

                ctx.ObjectDeleted -= deletedHandler;
            }
        }

        //[Test]
        //public void Delete_ICollectionEntry()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
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

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Detach_IDataObject_Failed()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                ctx.Detach(new TestObjClass__Implementation__());
            }
        }

        [Test]
        public void Detach_IDataObject()
        {
            using (IKistlContext ctx = KistlContext.InitSession())
            {
                TestObjClass obj = ctx.GetQuery<TestObjClass>().First();
                ctx.Detach(obj);
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Detached));
            }
        }


        //[Test]
        //public void Detach_ICollectionEntry()
        //{
        //    using (IKistlContext ctx = KistlContext.InitSession())
        //    {
        //        var obj = ctx.Find<TestObjClass>(1);
        //        Assert.That(obj, Is.Not.Null);
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        TestObjClass_TestNameCollectionEntry__Implementation__ c = ((TestObjClass__Implementation__)obj).TestNames__Implementation__.First();
        //        ctx.Detach(c);
        //        Assert.That(c.EntityState, Is.EqualTo(EntityState.Detached));
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(1));
        //    }
        //}

    }
}
