
namespace Kistl.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class CompoundCollectionEntryEfImpl<TA, TAImpl, TB, TBImpl>
        : BaseServerCollectionEntry_EntityFramework
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, ICompoundObject
        where TBImpl : class, ICompoundObject, TB
    {
        protected CompoundCollectionEntryEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }
    }
}
