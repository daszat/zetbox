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

namespace Zetbox.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Autofac;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server.Mocks;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.App.Base;
    using NUnit.Framework;

    internal class TestQueryTranslatorProvider<T> : QueryTranslatorProvider<T>
    {
        private readonly InterfaceType.Factory _iftFactory;

        internal TestQueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, IZetboxContext ctx, InterfaceType.Factory iftFactory, IPerfCounter perfCounter)
            : base(metaDataResolver, identity, source, ctx, iftFactory, perfCounter)
        {
            _iftFactory = iftFactory;
        }

        protected override QueryTranslatorProvider<TElement> GetSubProvider<TElement>()
        {
            return new TestQueryTranslatorProvider<TElement>(MetaDataResolver, Identity, Source, Ctx, _iftFactory, perfCounter);
        }

        protected override string ImplementationSuffix
        {
            get { return Zetbox.API.Helper.ImplementationSuffix; }
        }
    }

    [TestFixture]
    public class QueryTranslatorTests : AbstractApiServerTestFixture
    {
        IZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = new ZetboxContextMock(scope.Resolve<IMetaDataResolver>(), null, scope.Resolve<ZetboxConfig>(), scope.Resolve<Func<IFrozenContext>>(), scope.Resolve<InterfaceType.Factory>());
        }

        [Test]
        public void should_keep_Convert_nodes_on_primitive_data()
        {
            var q = ctx.GetQuery<TestObjClass>();
            var subject = new TestQueryTranslatorProvider<TestObjClass>(scope.Resolve<IMetaDataResolver>(), null, q, ctx, scope.Resolve<InterfaceType.Factory>(), scope.Resolve<IPerfCounter>());

            var obj = Expression.MakeBinary(
                ExpressionType.Equal,
                Expression.Convert(Expression.Constant(Zetbox.API.Mocks.TestEnum.X), typeof(int)),
                Expression.Constant(23));

            Assert.That(() => { subject.Visit(obj); }, Throws.Nothing);
        }
    }
}
