using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.DalProvider.EF.Mocks;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests
{
    [TestFixture]
    public class QueryTranslatorTests
    {
        IKistlContext ctx;

        [SetUp]
        public void Init()
        {
            var testCtx = new ServerApiContextMock();
            ctx = KistlContext.GetContext();
        }

        public interface TestInterface
        {
            int ID { get; }
        }

        public class TestClass : TestInterface
        {
            public int ID { get; set; }
        }

        private TestClass tc;

        [Test]
        public void test_against_class()
        {
            tc = new TestClass() { ID = 1 };
            var q = ctx.GetQuery<ObjectReferenceProperty>().Where(orp => orp.ID == tc.ID);
            foreach (var orp in q)
            {
                Assert.That(orp, Is.Not.Null);
            }
        }

        private TestInterface ti;
        [Test]
        public void test_against_interface()
        {
            ti = new TestClass() { ID = 1 };
            var q = ctx.GetQuery<ObjectReferenceProperty>().Where(orp => orp.ID == ti.ID);
            foreach (var orp in q)
            {
                Assert.That(orp, Is.Not.Null);
            }
        }

        private DataType dt;
        [Test]
        public void test_against_kistl_object()
        {
            dt = ctx.GetQuery<DataType>().First();
            var q = ctx.GetQuery<ObjectReferenceProperty>().Where(orp => orp.ID == dt.ID);
            foreach (var orp in q)
            {
                Assert.That(orp, Is.Not.Null);
            }
        }

        [Test]
        public void query_with_enum()
        {
            var q = ctx.GetQuery<Relation>().Where(r => r.Storage == StorageType.Separate).ToList();
            Assert.IsNotEmpty(q);
            foreach (var r in q)
            {
                Assert.That(r, Is.Not.Null);
                Assert.That(r.Storage, Is.EqualTo(StorageType.Separate));
            }
        }

        /// <summary>
        /// </summary>
        [Test]
        public void query_with_objectfilter()
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var module = ctx.GetQuery<Module>().Where(m => m.ModuleName == "KistlBase").First();
                Assert.That(module, Is.Not.Null);
                var result = ctx.GetQuery<ObjectClass>().Where(c => c.Module == module).ToList();
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
            }
        }

    }
}
