
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    
    public abstract class ValueCollectionEntryMemoryImpl<TA, TAImpl, TB>
        : BaseMemoryPersistenceObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        // where TB : class or struct (string/int)
    {
        protected ValueCollectionEntryMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public virtual void UpdateParent(string propertyName, IDataObject parentObj)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }
    }
}
