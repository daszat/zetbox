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


    }
}
