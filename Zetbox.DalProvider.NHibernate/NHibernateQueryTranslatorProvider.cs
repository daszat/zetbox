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

namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.API.Server.PerfCounter;
    using global::NHibernate.Linq;

    internal sealed class NHibernateQueryTranslatorProvider<T>
        : QueryTranslatorProvider<T>
    {
        private readonly NHibernateContext _ctx;
        private readonly INHibernateImplementationTypeChecker _implChecker;

        internal NHibernateQueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, NHibernateContext ctx, InterfaceType.Factory iftFactory, INHibernateImplementationTypeChecker implChecker, IPerfCounter perfCounter)
            : base(metaDataResolver, identity, source, ctx, iftFactory, perfCounter)
        {
            _ctx = ctx;
            _implChecker = implChecker;
        }

        protected override QueryTranslatorProvider<TElement> GetSubProvider<TElement>()
        {
            return new NHibernateQueryTranslatorProvider<TElement>(MetaDataResolver, Identity, Source, _ctx, IftFactory, _implChecker, perfCounter);
        }

        protected override object WrapResult(object item)
        {
            item = base.WrapResult(item);

            var proxy = item as IProxyObject;
            if (proxy == null)
                return item;
            else
                return _ctx.AttachAndWrap(proxy);
        }

        protected override string ImplementationSuffix
        {
            get { return "NHibernate" + Zetbox.API.Helper.ImplementationSuffix; }
        }

        protected override Type TranslateType(Type type)
        {
            var result = base.TranslateType(type);
            if (_implChecker.IsImplementationType(result) && !type.IsICompoundObject())
                result = _ctx.ToProxyType(_ctx.GetImplementationType(result));
            return result;
        }

        protected override System.Linq.Expressions.Expression VisitMethodCall(System.Linq.Expressions.MethodCallExpression m)
        {
            if (m.IsMethodCallExpression("TextContains", typeof(ZetboxContextQueryableExtensions)))
            {
                var prop = Visit(m.Arguments[0]);
                var value = Visit(m.Arguments[1]);
                var mi = typeof(NHibernateFullTextMethods).FindMethod("zb_fulltext_search", new[] { typeof(string), typeof(string) });
                return System.Linq.Expressions.Expression.Equal( // avoid nhibernate bug?
                    System.Linq.Expressions.Expression.Call(null, mi, new[] { prop, value }),
                    System.Linq.Expressions.Expression.Constant(true));
            }
            else
            {
                return base.VisitMethodCall(m);
            }
        }
    }

    public static class NHibernateFullTextMethods
    {
        [LinqExtensionMethod]
        public static bool zb_fulltext_search(this String input, String search)
        {
            throw new NotImplementedException();
        }
    }
}
