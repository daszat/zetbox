
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    
    public abstract class RelationEntryNHibernateImpl<TA, TAImpl, TB, TBImpl>
        : BaseNhPersistenceObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, IDataObject
        where TBImpl : class, IDataObject, TB
    {
        protected RelationEntryNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public abstract Guid RelationID { get; }
    }
}
