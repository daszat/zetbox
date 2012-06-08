
namespace Zetbox.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    
    public abstract class RelationEntryMemoryImpl<TA, TAImpl, TB, TBImpl>
        : BaseMemoryPersistenceObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, IDataObject
        where TBImpl : class, IDataObject, TB
    {
        protected RelationEntryMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public abstract Guid RelationID { get; }
    }
}
