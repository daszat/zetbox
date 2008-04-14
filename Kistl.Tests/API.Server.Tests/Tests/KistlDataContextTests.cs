using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace API.Server.Tests.Tests
{
    [TestFixture]
    public class KistlDataContextTests
    {
        [SetUp]
        public void SetUp()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                var list = result.ToList();

                list[0].StringProp = "First";
                list[0].TestEnumProp = 1;

                list[1].StringProp = "Second";
                list[1].TestEnumProp = 2;

                ctx.SubmitChanges();
            }
        }

        [Test]
        public void GetContext()
        {
            IKistlContext ctx = KistlDataContext.GetContext();
            Assert.That(ctx, Is.Not.Null);
        }

        [Test]
        public void InitSession()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                Assert.That(ctx, Is.Not.Null);
                Assert.That(KistlDataContext.Current, Is.Not.Null);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitSessionTwice()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                Assert.That(ctx, Is.Not.Null);
                Assert.That(KistlDataContext.Current, Is.Not.Null);
                using (IKistlContext ctx2 = KistlDataContext.InitSession())
                {
                    Assert.That(ctx2, Is.Not.Null);
                    Assert.That(KistlDataContext.Current, Is.Not.Null);
                }
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoSession()
        {
            Assert.That(KistlDataContext.Current, Is.Null);
        }

        [Test]
        public void GetQuery()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        public void GetQuery_ObjType()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetQuery(new ObjectType(typeof(TestObjClass)));
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        public void GetListOf()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var obj = ctx.GetQuery<TestObjClass>().First(o => o.ID == 1);
                var result = ctx.GetListOf<TestObjClass_TestNameCollectionEntry>(obj, "TestNames");
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void GetListOf_ObjType()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetListOf<TestObjClass_TestNameCollectionEntry>(new ObjectType(typeof(TestObjClass)), 1, "TestNames");
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(2));
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetListOf_WrongPropertyName()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var obj = ctx.GetQuery<TestObjClass>().First(o => o.ID == 1);
                var result = ctx.GetListOf<TestObjClass_TestNameCollectionEntry>(obj, "NotAProperty");
            }
        }

        [Test]
        public void SelectSomeData()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void SelectSomeData_Collection()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(2));

                Assert.That(result.ToList()[0].TestNames.Count, Is.EqualTo(2));
                Assert.That(result.ToList()[1].TestNames.Count, Is.EqualTo(2));
            }
        }

        private void ChangeData(IKistlContext ctx)
        {
            TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.StringProp, Is.EqualTo("First"));

            obj.StringProp = "Test";
        }

        private void CheckData(IKistlContext ctx)
        {
            TestObjClass obj = ctx.GetQuery<TestObjClass>().Where(o => o.ID == 1).First();
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.StringProp, Is.EqualTo("Test"));
        }

        [Test]
        public void UpdateSomeData_SubmitChanges()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                ChangeData(ctx);
                ctx.SubmitChanges();
            }

            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                CheckData(ctx);
            }
        }

        [Test]
        public void UpdateSomeData_SaveChanges()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                ChangeData(ctx);
                ctx.SubmitChanges();
            }

            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                CheckData(ctx);
            }
        }

        [Test]
        public void Attach_IDataObject_New()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass obj = new TestObjClass();
                ctx.Attach(obj);
            }
        }

        [Test]
        public void Attach_IDataObject_Existing()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass obj = new TestObjClass() { ID = 3 };
                ctx.Attach(obj);
            }
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Attach_ICollectionEntry()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass_TestNameCollectionEntry obj = new TestObjClass_TestNameCollectionEntry();
                ctx.Attach(obj);
            }
        }

        [Test]
        public void Create_Generic()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass newObj = ctx.Create<TestObjClass>();
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }

        [Test]
        public void Create_Type()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass newObj = ctx.Create(typeof(TestObjClass)) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }

        [Test]
        public void Create_ObjectType()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass newObj = ctx.Create(new ObjectType(typeof(TestObjClass))) as TestObjClass;
                Assert.That(newObj, Is.Not.Null);
                Assert.That(newObj.Context, Is.Not.Null);
            }
        }


        [Test]
        public void Delete_IDataObject()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(2));

                result.ForEach<TestObjClass>(
                    o => ctx.Delete(o));
            }
        }

        [Test]
        public void Delete_ICollectionEntry()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetQuery<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(2));

                foreach (TestObjClass obj in result)
                {
                    obj.TestNames.ToList().ForEach<TestObjClass_TestNameCollectionEntry>(c => ctx.Delete(c));
                    Assert.That(obj.TestNames.Count, Is.EqualTo(0));
                }
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Detach_IDataObject_Failed()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                ctx.Detach(new TestObjClass());
            }
        }

        [Test]
        public void Detach_IDataObject()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass obj = ctx.GetQuery<TestObjClass>().First();
                ctx.Detach(obj);
                Assert.That(obj.Context, Is.Null);
            }
        }


        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Detach_ICollectionEntry()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestObjClass_TestNameCollectionEntry obj = new TestObjClass_TestNameCollectionEntry();
                ctx.Detach(obj);
            }
        }

    }
}
