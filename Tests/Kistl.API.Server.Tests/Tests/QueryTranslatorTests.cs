
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

    internal class TestQueryTranslatorProvider<T> : QueryTranslatorProvider<T>
    {
        private readonly Func<Type, Type> _translator;

        internal TestQueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, IKistlContext ctx, Func<Type, Type> translator)
            : base(metaDataResolver, identity, source, ctx)
        {
            _translator = translator;
        }

        protected override Type ToProviderType(Type t)
        {
            return _translator(t);
        }

        protected override QueryTranslatorProvider<TElement> GetSubProvider<TElement>()
        {
            return new TestQueryTranslatorProvider<TElement>(MetaDataResolver, Identity, Source, Ctx, _translator);
        }
    }

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
            var subject = new TestQueryTranslatorProvider<TestObjClass>(new MetaDataResolverMock(), null, q, ctx, (t) => { Assert.Fail("Should not try to translate anything"); return null; });

            var obj = Expression.MakeBinary(
                ExpressionType.Equal,
                Expression.Convert(Expression.Constant(Kistl.API.Mocks.TestEnum.X), typeof(int)),
                Expression.Constant(23));

            Assert.That(() => { subject.Visit(obj); }, Throws.Nothing);
        }
    }
}
