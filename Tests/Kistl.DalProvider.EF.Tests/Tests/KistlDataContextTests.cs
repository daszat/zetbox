using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Projekte;
using Kistl.App.Test;
using Kistl.DalProvider.EF.Mocks;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class KistlDataContextTests
    {

        int firstId;
        int secondId;

        [SetUp]
        public void SetUp()
        {
            var testCtx = new ServerApiContextMock();

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var result = ctx.GetQuery<TestObjClass>();
                var list = result.ToList();

                while (list.Count < 2)
                {
                    var newObj = ctx.Create<TestObjClass>();
                    newObj.ObjectProp = ctx.GetQuery<Kunde>().First();
                    list.Add(newObj);
                }

                firstId = list[0].ID;
                list[0].StringProp = "First";
                list[0].TestEnumProp = TestEnum.First;

                secondId = list[1].ID;
                list[1].StringProp = "Second";
                list[1].TestEnumProp = TestEnum.Second;

                ctx.SubmitChanges();
            }
        }

        [Test]
        public void GetContext_returns_a_context()
        {
            IKistlContext ctx = KistlContext.GetContext();
            Assert.That(ctx, Is.Not.Null);
        }


        [Test]
        public void GetQuery()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        public void GetQuery_ObjType()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var result = ctx.GetQuery(new InterfaceType(typeof(TestObjClass)));
                Assert.That(result, Is.Not.Null);
                var testObj = result.First(o => o.ID == firstId);
                Assert.That(testObj, Is.Not.Null);
                Assert.That(testObj, Is.InstanceOf(typeof(TestObjClass)));
            }
        }

        [Test]
        public void Find_T_returns_right_object()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass obj = ctx.Find<TestObjClass>(Kistl.API.Helper.INVALIDID);
            }
        }

        [Test]
        public void Find_ObjectType_returns_right_object()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass obj = (TestObjClass)ctx.Find(new InterfaceType(typeof(TestObjClass)), firstId);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.ID, Is.EqualTo(firstId));
                Assert.That(obj.TestEnumProp, Is.EqualTo(TestEnum.First));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Find_ObjectType_fails_on_invalid_ID()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass obj = (TestObjClass)ctx.Find(new InterfaceType(typeof(TestObjClass)), Kistl.API.Helper.INVALIDID);
            }
        }

        [Test]
        public void GetListOf_T_SubClasses_returns_a_non_empty_list_on_class_DataType()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<ObjectClass>().First(o => o.ClassName == "DataType");
                List<ObjectClass> result = ctx.GetListOf<ObjectClass>(obj, "SubClasses").ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        public void GetListOf_ObjType_returns_a_non_empty_list_on_class_DataType()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<ObjectClass>().First(o => o.ClassName == "DataType");
                List<ObjectClass> result = ctx.GetListOf<ObjectClass>(new InterfaceType(typeof(ObjectClass)), obj.ID, "SubClasses").ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetListOf_T_WrongPropertyName_fails()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<TestObjClass>().First(o => o.ID == firstId);
                var result = ctx.GetListOf<TestObjClass>(obj, "NotAProperty");
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetListOf_ObjType_WrongPropertyName_fails()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var result = ctx.GetListOf<TestObjClass>(new InterfaceType(typeof(TestObjClass)), firstId, "NotAProperty");
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetListOf_T_WrongItemType_fails()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<ObjectClass>().First(o => o.ClassName == "DataType");
                var result = ctx.GetListOf<TestObjClass>(obj, "SubClasses").ToList();
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetListOf_ObjType_WrongItemType_fails()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var obj = ctx.GetQuery<ObjectClass>().First(o => o.ClassName == "DataType");
                var result = ctx.GetListOf<TestObjClass>(new InterfaceType(typeof(ObjectClass)), obj.ID, "SubClasses").ToList();
            }
        }

        [Test]
        public void UpdateSomeData_SubmitChanges()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass obj = ctx.Find<TestObjClass>(firstId);
                Assert.That(obj, Is.Not.Null);
                Assert.That(obj.StringProp, Is.EqualTo("First"));

                obj.StringProp = "Test";
                ctx.SubmitChanges();
            }

            using (IKistlContext ctx = KistlContext.GetContext())
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
        //    using (IKistlContext ctx = KistlContext.GetContext())
        //    {
        //        TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        //obj.TestNames[1] = "MuhBlah";

        //        ctx.SubmitChanges();
        //    }

        //    using (IKistlContext ctx = KistlContext.GetContext())
        //    {
        //        TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
        //        Assert.That(obj.TestNames.Count, Is.EqualTo(2));

        //        //Assert.That(obj.TestNames[1], Is.EqualTo("MuhBlah"));
        //    }
        //}

        [Test]
        public void Attach_IDataObject_New()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
        //    using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
        //    using (IKistlContext ctx = KistlContext.GetContext())
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
        //    using (IKistlContext ctx = KistlContext.GetContext())
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
        //    using (IKistlContext ctx = KistlContext.GetContext())
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
        //    using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass newObj = ctx.Create(new InterfaceType(typeof(TestObjClass))) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }

        [Test]
        public void Create_ObjectType()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass newObj = ctx.Create(new InterfaceType(typeof(TestObjClass))) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }


        [Test]
        public void Delete_triggers_ObjectDeleted()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                Assert.That(ctx.GetQuery<TestObjClass>().Count(), Is.EqualTo(0));
            }
        }


        //[Test]
        //public void Delete_ICollectionEntry()
        //{
        //    using (IKistlContext ctx = KistlContext.GetContext())
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
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                ctx.Detach(new TestObjClass__Implementation__());
            }
        }

        [Test]
        public void Detach_IDataObject()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                TestObjClass obj = ctx.GetQuery<TestObjClass>().First();
                ctx.Detach(obj);
                Assert.That(((TestObjClass__Implementation__)obj).EntityState, Is.EqualTo(EntityState.Detached));
            }
        }


        //[Test]
        //public void Detach_ICollectionEntry()
        //{
        //    using (IKistlContext ctx = KistlContext.GetContext())
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



        [Test]
        public void should_find_new_objects()
        {
            using (var ctx = KistlContext.GetContext())
            {
                var obj = ctx.Create<TestObjClass>();
                Assert.That(ctx.Find<TestObjClass>(obj.ID), Is.SameAs(obj));
            }

        }

        [Test]
        [Ignore("Discuss")]
        public void should_create_objects_with_valid_IDs()
        {
            using (var ctx = KistlContext.GetContext())
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
            using (var ctx = KistlContext.GetContext())
            {
                int objCount = 10;
                while (objCount-- > 0)
                {
                    objList.Add(ctx.Create<TestObjClass>());
                }
            }

            Assert.DoesNotThrow(delegate() { 
                // throws exception on duplicate keys
                objList.ToDictionary(o => o.ID); 
            });
        }


    }
}
