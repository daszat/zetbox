using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client.Mocks;

using NUnit.Framework;

namespace Kistl.API.Client.Tests
{
    [TestFixture]
    public class KistlContextTests
    {
        private IKistlContext ctx;
        private CustomActionsManagerAPITest currentCustomActionsManager;

        [SetUp]
        public void SetUp()
        {
            System.Diagnostics.Trace.WriteLine("KistlContextTests.SetUp() is called");

            var testCtx = new ClientApplicationContextMock();

            currentCustomActionsManager = (CustomActionsManagerAPITest)testCtx.CustomActionsManager;
            currentCustomActionsManager.Reset();

            ctx = KistlContext.GetContext();
            //CacheController<Kistl.API.IDataObject>.Current.Clear();
        }

        [Test]
        public void GetQuery_ObjectType_should_create_query_with_proper_ElementType()
        {
            IQueryable<IDataObject> query = ctx.GetQuery(new InterfaceType(typeof(TestObjClass)));
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ElementType, Is.EqualTo(typeof(IDataObject)));
        }

        [Test]
        public void GetQuery_T_should_create_query_with_proper_ElementType()
        {
            IQueryable<TestObjClass> query = ctx.GetQuery<TestObjClass>();
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ElementType, Is.EqualTo(typeof(TestObjClass)));
        }

        [Test]
        public void Find_T_should_return_correct_item()
        {
            int targetId = 1;
            TestObjClass obj = ctx.Find<TestObjClass>(targetId);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.InstanceOf(typeof(TestObjClass)));
            Assert.That(obj.ID, Is.EqualTo(targetId));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void Find_ObjectType_should_return_correct_item()
        {
            int targetId = 1;
            TestObjClass obj = (TestObjClass)ctx.Find(new InterfaceType(typeof(TestObjClass)), targetId);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.InstanceOf(typeof(TestObjClass)));
            Assert.That(obj.ID, Is.EqualTo(targetId));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void Find_should_return_the_same_object_on_second_call()
        {
            int targetId = 1;
            var obj1 = ctx.Find<TestObjClass>(targetId);
            var obj2 = ctx.Find<TestObjClass>(targetId);
            Assert.That(object.ReferenceEquals(obj1, obj2), "Obj1 & Obj2 are different Objects");
        }

        [Test]
        public void GetList()
        {
            List<TestObjClass> list = ctx.GetQuery<TestObjClass>().ToList();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));
            list.ForEach(obj => Assert.That(obj, Is.Not.Null));
            list.ForEach(obj => Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified)));
            list.ForEach(obj => Assert.That(obj.Context, Is.EqualTo(ctx)));
        }

        [Test]
        public void GetList_Twice()
        {
            List<TestObjClass> list1 = ctx.GetQuery<TestObjClass>().ToList();
            Assert.That(list1, Is.Not.Null);
            Assert.That(list1.Count, Is.AtLeast(2));
            list1.ForEach(obj => Assert.That(obj, Is.Not.Null));

            List<TestObjClass> list2 = ctx.GetQuery<TestObjClass>().ToList();
            Assert.That(list2, Is.Not.Null);
            Assert.That(list2.Count, Is.EqualTo(list1.Count));
            list2.ForEach(obj => Assert.That(obj, Is.Not.Null));

            for (int i = 0; i < list1.Count; i++)
            {
                Assert.That(object.ReferenceEquals(list1[i], list2[i]), "list1[i] & list2[i] are different Objects");
            }
        }

        [Test]
        public void GetListOf()
        {
            List<TestObjClass> list = ctx.GetListOf<TestObjClass>(new InterfaceType(typeof(TestObjClass)), 1, "Children");
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));
            list.ForEach(obj => Assert.That(obj, Is.Not.Null));
            list.ForEach(obj => Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified)));
            list.ForEach(obj => Assert.That(obj.Context, Is.EqualTo(ctx)));
        }

        [Test]
        public void GetListOf_Twice()
        {
            List<TestObjClass> list1 = ctx.GetListOf<TestObjClass>(new InterfaceType(typeof(TestObjClass)), 1, "Children");
            Assert.That(list1, Is.Not.Null);
            Assert.That(list1.Count, Is.AtLeast(2));
            list1.ForEach(obj => Assert.That(obj, Is.Not.Null));

            List<TestObjClass> list2 = ctx.GetListOf<TestObjClass>(new InterfaceType(typeof(TestObjClass)), 1, "Children");
            Assert.That(list2, Is.Not.Null);
            Assert.That(list2.Count, Is.EqualTo(list1.Count));
            list2.ForEach(obj => Assert.That(obj, Is.Not.Null));

            for (int i = 0; i < list1.Count; i++)
            {
                Assert.That(list1[i], Is.EqualTo(list2[i]));
            }
        }

        [Test]
        public void GetObject_GetList()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(1);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.ID, Is.EqualTo(1));

            List<TestObjClass> list = ctx.GetQuery<TestObjClass>().ToList();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));

            TestObjClass obj_list = list.Single(o => o.ID == 1);
            Assert.That(obj_list, Is.Not.Null);
            Assert.That(obj_list.ID, Is.EqualTo(1));

            Assert.That(object.ReferenceEquals(obj, obj_list), "obj & obj_list are different Objects");
        }

        [Test]
        public void GetObject_GetListOf()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(3);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.ID, Is.EqualTo(3));

            List<TestObjClass> list = ctx.GetListOf<TestObjClass>(new InterfaceType(typeof(TestObjClass)), 1, "Children");
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));

            TestObjClass obj_list = list.Single(o => o.ID == 3);
            Assert.That(obj_list, Is.Not.Null);
            Assert.That(obj_list.ID, Is.EqualTo(3));

            Assert.That(object.ReferenceEquals(obj, obj_list), "obj & obj_list are different Objects");
        }

        [Test]
        public void GetList_GetListOf()
        {
            List<TestObjClass> list = ctx.GetQuery<TestObjClass>().ToList();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));

            List<TestObjClass> listOf = ctx.GetListOf<TestObjClass>(new InterfaceType(typeof(TestObjClass)), 1, "Children");
            Assert.That(listOf, Is.Not.Null);
            Assert.That(listOf.Count, Is.AtLeast(2));

            TestObjClass obj_list = list.Single(o => o.ID == 3);
            Assert.That(obj_list, Is.Not.Null);
            Assert.That(obj_list.ID, Is.EqualTo(3));

            TestObjClass obj_listOf = listOf.Single(o => o.ID == 3);
            Assert.That(obj_listOf, Is.Not.Null);
            Assert.That(obj_listOf.ID, Is.EqualTo(3));

            Assert.That(object.ReferenceEquals(obj_list, obj_listOf), "obj_list & obj_listOf are different Objects");
        }


        [Test]
        public void Create_T()
        {
            TestObjClass obj = ctx.Create<TestObjClass>();
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.ID, Is.LessThan(Helper.INVALIDID));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void Create_Type()
        {
            TestObjClass obj = ctx.Create(new InterfaceType(typeof(TestObjClass))) as TestObjClass;
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.ID, Is.LessThan(Helper.INVALIDID));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void Create_ObjectType()
        {
            TestObjClass obj = ctx.Create(new InterfaceType(typeof(TestObjClass))) as TestObjClass;
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.ID, Is.LessThan(Helper.INVALIDID));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void Create_T_Twice()
        {
            TestObjClass obj1 = ctx.Create<TestObjClass>();
            Assert.That(obj1, Is.Not.Null);
            Assert.That(obj1.ID, Is.LessThan(Helper.INVALIDID));
            Assert.That(obj1.Context, Is.EqualTo(ctx));

            TestObjClass obj2 = ctx.Create<TestObjClass>();
            Assert.That(obj2, Is.Not.Null);
            Assert.That(obj2.ID, Is.LessThan(Helper.INVALIDID));
            Assert.That(obj2.Context, Is.EqualTo(ctx));

            Assert.That(obj1, Is.Not.EqualTo(obj2));
        }

        [Test]
        public void Attach_IDataObject_New()
        {
            TestObjClass obj = new TestObjClass__Implementation__();
            ctx.Attach(obj);
            Assert.That(obj.Context, Is.EqualTo(ctx));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        }

        [Test]
        public void Attach_IDataObject_Twice()
        {
            TestObjClass obj = new TestObjClass__Implementation__();
            ctx.Attach(obj);
            Assert.That(obj.Context, Is.EqualTo(ctx));

            ctx.Attach(obj);
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void Attach_IDataObject_Twice_Modified()
        {
            TestObjClass obj = new TestObjClass__Implementation__();
            obj.SetPrivatePropertyValue<int>("ID", 10);
            ctx.Attach(obj);
            Assert.That(obj.Context, Is.EqualTo(ctx));

            obj.StringProp = "Test";
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));

            ctx.Attach(obj);
            Assert.That(obj.Context, Is.EqualTo(ctx));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        }


        [Test]
        public void Attach_IDataObject_Existing_Twice_But_Different()
        {
            TestObjClass obj1 = new TestObjClass__Implementation__();
            obj1.SetPrivatePropertyValue<int>("ID", 1);
            ctx.Attach(obj1);
            Assert.That(obj1.Context, Is.EqualTo(ctx));

            TestObjClass obj2 = new TestObjClass__Implementation__();
            obj2.SetPrivatePropertyValue<int>("ID", 1);
            TestObjClass obj3 = (TestObjClass)ctx.Attach(obj2);
            Assert.That(object.ReferenceEquals(obj1, obj3), "obj1 & obj3 are different Objects");
            Assert.That(!object.ReferenceEquals(obj2, obj3), "obj1 & obj3 are the same Objects");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Attach_IDataObject_Null()
        {
            ctx.Attach((IDataObject)null);
        }

        [Test]
        public void Detach()
        {
            TestObjClass obj = new TestObjClass__Implementation__();
            obj.SetPrivatePropertyValue<int>("ID", 1);
            ctx.Attach(obj);
            Assert.That(obj.Context, Is.EqualTo(ctx));

            ctx.Detach(obj);
            Assert.That(obj.Context, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Detach_NotAttached()
        {
            TestObjClass obj = new TestObjClass__Implementation__();
            obj.SetPrivatePropertyValue<int>("ID", 1);

            ctx.Detach(obj);
            Assert.That(obj.Context, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Detach_Null()
        {
            ctx.Detach((IDataObject)null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Detach_Twice()
        {
            TestObjClass obj = new TestObjClass__Implementation__();
            obj.SetPrivatePropertyValue<int>("ID", 1);
            ctx.Attach(obj);
            Assert.That(obj.Context, Is.EqualTo(ctx));

            ctx.Detach(obj);
            Assert.That(obj.Context, Is.Null);

            ctx.Detach(obj);
        }

        [Test]
        public void Delete()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(1);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            ctx.Delete(obj);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_Null()
        {
            ctx.Delete((IDataObject)null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_Not_Attached()
        {
            TestObjClass obj = new TestObjClass__Implementation__();
            ctx.Delete(obj);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_Wrong_Context()
        {
            TestObjClass obj = KistlContext.GetContext().Find<TestObjClass>(1);
            ctx.Delete(obj);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
        }

        [Test]
        public void SubmitChanges()
        {
            int testId = 1;
            string testString = "Test";

            TestObjClass obj = ctx.Find<TestObjClass>(testId);
            obj.StringProp = testString;
            Assert.That(obj.ID, Is.EqualTo(testId));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));

            int result = ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
            Assert.That(obj.StringProp, Is.EqualTo(testString));
        }

        [Test]
        public void SubmitChanges_New()
        {
            TestObjClass obj = ctx.Create<TestObjClass>();
            obj.StringProp = "Test";
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));

            int result = ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void SubmitChanges_Nothing()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(1);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            int result = ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(0));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void SubmitChanges_Delete()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(1);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            ctx.Delete(obj);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            int result = ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(obj.Context, Is.Null);
        }
    }
}