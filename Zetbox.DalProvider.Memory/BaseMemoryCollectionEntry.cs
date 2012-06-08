
namespace Zetbox.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    
    // obsolete
    public abstract class BaseMemoryCollectionEntry
        : BaseMemoryPersistenceObject
    {
        protected BaseMemoryCollectionEntry(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }
    }
}
