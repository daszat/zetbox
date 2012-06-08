
namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    public abstract class ValueCollectionEntryEfImpl<TA, TAImpl, TB>
        : BaseServerCollectionEntry_EntityFramework
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
    {
        protected ValueCollectionEntryEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public abstract Guid PropertyID { get; }
    }
}
