
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    internal sealed class NHibernateQueryTranslatorProvider<T>
        : QueryTranslatorProvider<T>
    {
        private readonly NHibernateContext _ctx;
        internal NHibernateQueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, NHibernateContext ctx, InterfaceType.Factory iftFactory)
            : base(metaDataResolver, identity, source, ctx, iftFactory)
        {
            _ctx = ctx;
        }

        protected override QueryTranslatorProvider<TElement> GetSubProvider<TElement>()
        {
            return new NHibernateQueryTranslatorProvider<TElement>(MetaDataResolver, Identity, Source, _ctx, IftFactory);
        }

        protected override object WrapResult(object item)
        {
            return _ctx.AttachAndWrap((IProxyObject)base.WrapResult(item));
        }

        protected override string ImplementationSuffix
        {
            get { return "NHibernate" + Kistl.API.Helper.ImplementationSuffix; }
        }
    }
}
