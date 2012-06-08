
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
            if (_implChecker.IsImplementationType(result))
                result = _ctx.ToProxyType(_ctx.GetImplementationType(result));
            return result;
        }
    }
}
