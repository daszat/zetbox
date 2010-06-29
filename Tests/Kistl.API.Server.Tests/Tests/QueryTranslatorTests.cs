
namespace Kistl.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Kistl.API.Server.Mocks;
    using Kistl.App.Base;

    using NUnit.Framework;

    using Autofac;
    using Kistl.API.Configuration;

    internal class TestQueryTranslatorProvider<T> : QueryTranslatorProvider<T>
    {
        private readonly InterfaceType.Factory _iftFactory;

        internal TestQueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, IKistlContext ctx, InterfaceType.Factory iftFactory)
            : base(metaDataResolver, identity, source, ctx, iftFactory)
        {
            _iftFactory = iftFactory;
        }

        protected override QueryTranslatorProvider<TElement> GetSubProvider<TElement>()
        {
            return new TestQueryTranslatorProvider<TElement>(MetaDataResolver, Identity, Source, Ctx, _iftFactory);
        }
    }

    [TestFixture]
    public class QueryTranslatorTests : AbstractApiServerTestFixture
    {
        IKistlContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = new KistlContextMock(scope.Resolve<IMetaDataResolver>(), null, scope.Resolve<KistlConfig>(), scope.Resolve<InterfaceType.Factory>());
        }

        [Test]
        public void should_keep_Convert_nodes_on_primitive_data()
        {
            var q = ctx.GetQuery<TestObjClass>();
            var subject = new TestQueryTranslatorProvider<TestObjClass>(scope.Resolve<IMetaDataResolver>(), null, q, ctx, scope.Resolve<InterfaceType.Factory>());

            var obj = Expression.MakeBinary(
                ExpressionType.Equal,
                Expression.Convert(Expression.Constant(Kistl.API.Mocks.TestEnum.X), typeof(int)),
                Expression.Constant(23));

            Assert.That(() => { subject.Visit(obj); }, Throws.Nothing);
        }
    }
}
