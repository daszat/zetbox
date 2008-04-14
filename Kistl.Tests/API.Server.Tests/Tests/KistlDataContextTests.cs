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
                var result = ctx.GetTable<TestObjClass>();
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
        public void GetTable()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetTable<TestObjClass>();
                Assert.That(result, Is.Not.Null);
            }
        }

        [Test]
        public void SelectSomeData()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetTable<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(2));
            }
        }

        private void ChangeData(IKistlContext ctx)
        {
            TestObjClass obj = ctx.GetTable<TestObjClass>().Where(o => o.ID == 1).First();
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.StringProp, Is.EqualTo("First"));

            obj.StringProp = "Test";
        }

        private void CheckData(IKistlContext ctx)
        {
            TestObjClass obj = ctx.GetTable<TestObjClass>().Where(o => o.ID == 1).First();
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
                TestCollectionEntry obj = new TestCollectionEntry();
                ctx.Attach(obj);
            }
        }

        [Test]
        public void Delete_IDataObject()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                var result = ctx.GetTable<TestObjClass>();
                Assert.That(result.ToList().Count, Is.EqualTo(2));

                result.ForEach<TestObjClass>(
                    o => ctx.Delete(o));
            }
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Detach_IDataObject()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                ctx.Detach(new TestObjClass());
            }
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Detach_ICollectionEntry()
        {
            using (IKistlContext ctx = KistlDataContext.InitSession())
            {
                TestCollectionEntry obj = new TestCollectionEntry();
                ctx.Detach(obj);
            }
        }

    }
}
