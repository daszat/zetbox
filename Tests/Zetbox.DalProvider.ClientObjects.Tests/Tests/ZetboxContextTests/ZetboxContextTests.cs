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

namespace Zetbox.DalProvider.Client.Tests.ZetboxContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Test;
    using NUnit.Framework;
    using Zetbox.API.Client;
    using System.Threading.Tasks;

    [TestFixture]
    public class ZetboxContextTests : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
    {
        private IZetboxContext ctx;
        private InterfaceType.Factory _iftFactory;

        public override void SetUp()
        {
            base.SetUp();

            Logging.Log.Info("ZetboxContextTests.SetUp() is called");

            this._iftFactory = scope.Resolve<InterfaceType.Factory>();
            ctx = GetContext();
            //CacheController<Zetbox.API.IDataObject>.Current.Clear();
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
            TestObjClass obj = (TestObjClass)ctx.Find(_iftFactory(typeof(TestObjClass)), targetId);
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
        public void GetObjects()
        {
            List<TestObjClass> list = ctx.GetQuery<TestObjClass>().ToList();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));
            list.ForEach(obj => Assert.That(obj, Is.Not.Null));
            list.ForEach(obj => Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified)));
            list.ForEach(obj => Assert.That(obj.Context, Is.EqualTo(ctx)));
        }

        [Test]
        public void GetObjects_Twice()
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
            TestObjClass o = (TestObjClass)ctx.Find(_iftFactory(typeof(TestObjClass)), 1);
            List<TestObjClass> list = ctx.GetListOf<TestObjClass>(o, "Children");
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));
            list.ForEach(obj => Assert.That(obj, Is.Not.Null));
            list.ForEach(obj => Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified)));
            list.ForEach(obj => Assert.That(obj.Context, Is.EqualTo(ctx)));
        }

        [Test]
        public void GetListOf_Twice()
        {
            TestObjClass o = (TestObjClass)ctx.Find(_iftFactory(typeof(TestObjClass)), 1);
            List<TestObjClass> list1 = ctx.GetListOf<TestObjClass>(o, "Children");
            Assert.That(list1, Is.Not.Null);
            Assert.That(list1.Count, Is.AtLeast(2));
            list1.ForEach(obj => Assert.That(o, Is.Not.Null));

            List<TestObjClass> list2 = ctx.GetListOf<TestObjClass>(o, "Children");
            Assert.That(list2, Is.Not.Null);
            Assert.That(list2.Count, Is.EqualTo(list1.Count));
            list2.ForEach(obj => Assert.That(o, Is.Not.Null));

            for (int i = 0; i < list1.Count; i++)
            {
                Assert.That(list1[i], Is.EqualTo(list2[i]));
            }
        }

        [Test]
        public void GetObject_GetObjects()
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
            TestObjClass obj1 = ctx.Find<TestObjClass>(1);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.ID, Is.EqualTo(3));

            List<TestObjClass> list = ctx.GetListOf<TestObjClass>(obj1, "Children");
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));

            TestObjClass obj_list = list.Single(o => o.ID == 3);
            Assert.That(obj_list, Is.Not.Null);
            Assert.That(obj_list.ID, Is.EqualTo(3));

            Assert.That(object.ReferenceEquals(obj, obj_list), "obj & obj_list are different Objects");
        }

        [Test]
        public void GetObjects_GetListOf()
        {
            List<TestObjClass> list = ctx.GetQuery<TestObjClass>().ToList();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.AtLeast(2));

            TestObjClass obj = ctx.Find<TestObjClass>(1);
            List<TestObjClass> listOf = ctx.GetListOf<TestObjClass>(obj, "Children");
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
            TestObjClass obj = ctx.Create(_iftFactory(typeof(TestObjClass))) as TestObjClass;
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.ID, Is.LessThan(Helper.INVALIDID));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public void Create_ObjectType()
        {
            TestObjClass obj = ctx.Create(_iftFactory(typeof(TestObjClass))) as TestObjClass;
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

        // TODO: cannot reference Zetbox.Objects.ClientImpl, but must!
        //[Test]
        //public void Attach_IDataObject_New()
        //{
        //    TestObjClass obj = new TestObjClassImpl(null);
        //    ctx.Attach(obj);
        //    Assert.That(obj.Context, Is.EqualTo(ctx));
        //    Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));
        //}

        //[Test]
        //public void Attach_IDataObject_Twice()
        //{
        //    TestObjClass obj = new TestObjClassImpl(null);
        //    ctx.Attach(obj);
        //    Assert.That(obj.Context, Is.EqualTo(ctx));

        //    ctx.Attach(obj);
        //    Assert.That(obj.Context, Is.EqualTo(ctx));
        //}

        //[Test]
        //public void Attach_IDataObject_Twice_Modified()
        //{
        //    TestObjClass obj = new TestObjClassImpl(null);
        //    obj.SetPrivatePropertyValue<int>("ID", 10);
        //    ctx.Attach(obj);
        //    Assert.That(obj.Context, Is.EqualTo(ctx));

        //    obj.StringProp = "Test";
        //    Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));

        //    ctx.Attach(obj);
        //    Assert.That(obj.Context, Is.EqualTo(ctx));
        //    Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));
        //}


        //[Test]
        //public void Attach_IDataObject_Existing_Twice_But_Different()
        //{
        //    TestObjClass obj1 = new TestObjClassImpl(null);
        //    obj1.SetPrivatePropertyValue<int>("ID", 1);
        //    ctx.Attach(obj1);
        //    Assert.That(obj1.Context, Is.EqualTo(ctx));

        //    TestObjClass obj2 = new TestObjClassImpl(null);
        //    obj2.SetPrivatePropertyValue<int>("ID", 1);
        //    TestObjClass obj3 = (TestObjClass)ctx.Attach(obj2);
        //    Assert.That(object.ReferenceEquals(obj1, obj3), "obj1 & obj3 are different Objects");
        //    Assert.That(!object.ReferenceEquals(obj2, obj3), "obj1 & obj3 are the same Objects");
        //}

        [Test]
        public void Attach_IDataObject_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { ctx.Attach((IDataObject)null); });
        }

        //[Test]
        //public void Detach()
        //{
        //    TestObjClass obj = new TestObjClassImpl(null);
        //    obj.SetPrivatePropertyValue<int>("ID", 1);
        //    ctx.Attach(obj);
        //    Assert.That(obj.Context, Is.EqualTo(ctx));

        //    ctx.Detach(obj);
        //    Assert.That(obj.Context, Is.Null);
        //}

        //[Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void Detach_NotAttached()
        //{
        //    TestObjClass obj = new TestObjClassImpl(null);
        //    obj.SetPrivatePropertyValue<int>("ID", 1);

        //    ctx.Detach(obj);
        //    Assert.That(obj.Context, Is.Null);
        //}

        [Test]
        public void Detach_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { ctx.Detach((IDataObject)null); });
        }

        //[Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void Detach_Twice()
        //{
        //    TestObjClass obj = new TestObjClassImpl(null);
        //    obj.SetPrivatePropertyValue<int>("ID", 1);
        //    ctx.Attach(obj);
        //    Assert.That(obj.Context, Is.EqualTo(ctx));

        //    ctx.Detach(obj);
        //    Assert.That(obj.Context, Is.Null);

        //    ctx.Detach(obj);
        //}

        [Test]
        public void Delete()
        {
            TestObjClass obj = ctx.GetQuery<TestObjClass>().First();
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            ctx.Delete(obj);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
        }

        [Test]
        public void Delete_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { ctx.Delete((IDataObject)null); });
        }

        //[Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void Delete_Not_Attached()
        //{
        //    TestObjClass obj = new TestObjClassImpl(null);
        //    ctx.Delete(obj);
        //    Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
        //}

        [Test]
        public void Delete_Wrong_Context()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                TestObjClass obj = GetContext().Find<TestObjClass>(1);
                ctx.Delete(obj);
                Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            });
        }

        [Test]
        public async Task SubmitChanges()
        {
            int testId = 1;
            string testString = "Test";

            TestObjClass obj = ctx.Find<TestObjClass>(testId);
            obj.StringProp = testString;
            Assert.That(obj.ID, Is.EqualTo(testId));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified));

            int result = await ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
            Assert.That(obj.StringProp, Is.EqualTo(testString));
        }

        [Test]
        public async Task SubmitChanges_New()
        {
            TestObjClass obj = ctx.Create<TestObjClass>();
            obj.StringProp = "Test";
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.New));

            int result = await ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public async Task SubmitChanges_Nothing()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(1);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));

            int result = await ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(0));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            Assert.That(obj.Context, Is.EqualTo(ctx));
        }

        [Test]
        public async Task SubmitChanges_Delete()
        {
            TestObjClass obj = ctx.Find<TestObjClass>(1);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Unmodified));
            ctx.Delete(obj);
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));

            int result = await ctx.SubmitChanges();
            Assert.That(result, Is.EqualTo(1));
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Deleted));
            Assert.That(obj.Context, Is.Null);
        }
    }
}