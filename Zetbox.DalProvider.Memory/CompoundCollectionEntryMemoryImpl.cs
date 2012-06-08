
namespace Zetbox.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;

    public abstract class CompoundCollectionEntryMemoryImpl<TA, TAImpl, TB, TBImpl>
        : BaseMemoryPersistenceObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, ICompoundObject
        where TBImpl : class, ICompoundObject, TB
    {
        protected CompoundCollectionEntryMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public virtual void UpdateParent(string propertyName, IDataObject parentObj)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }

        public abstract Guid PropertyID { get; }
    }
}
