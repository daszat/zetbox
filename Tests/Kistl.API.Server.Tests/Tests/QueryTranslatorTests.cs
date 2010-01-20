using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Kistl.API.Server.Mocks;

using NUnit.Framework;

namespace Kistl.API.Server.Tests
{
    [TestFixture]
    public class QueryTranslatorTests
    {
        IKistlContext ctx;

        [SetUp]
        public void SetUp()
        {
            ctx = new KistlContextMock();
        }

        [Test]
        public void should_keep_Convert_nodes_on_primitive_data()
        {
            var q = ctx.GetQuery<TestObjClass>();
            var subject = new QueryTranslatorProvider<TestObjClass>(new MetaDataResolverMock(), null, q, ctx);

            var obj = Expression.MakeBinary(
                ExpressionType.Equal,
                Expression.Convert(Expression.Constant(Kistl.API.Mocks.TestEnum.X), typeof(int)),
                Expression.Constant(23));

            Assert.DoesNotThrow(delegate() { subject.Visit(obj); });
        }

    }
}
