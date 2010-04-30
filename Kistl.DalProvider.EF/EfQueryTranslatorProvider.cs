
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    internal sealed class EfQueryTranslatorProvider<T>
        : QueryTranslatorProvider<T>
    {
        internal EfQueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, IKistlContext ctx, ITypeTransformations typeTrans)
            : base(metaDataResolver, identity, source, ctx, typeTrans)
        {
        }

        protected override Type ToProviderType(Type t)
        {
            return Ctx.ToImplementationType(typeTrans.AsInterfaceType(t)).Type;
        }

        protected override QueryTranslatorProvider<TElement> GetSubProvider<TElement>()
        {
            return new EfQueryTranslatorProvider<TElement>(MetaDataResolver, Identity, Source, Ctx, typeTrans);
        }
    }
}
