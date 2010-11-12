
namespace Kistl.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class RelationEntryEfImpl<TA, TAImpl, TB, TBImpl>
        : BaseServerCollectionEntry_EntityFramework
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, IDataObject
        where TBImpl : class, IDataObject, TB
    {
        protected RelationEntryEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public abstract Guid RelationID { get; }
    }
}
