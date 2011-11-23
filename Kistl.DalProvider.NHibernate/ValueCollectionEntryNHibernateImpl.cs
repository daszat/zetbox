
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    
    public abstract class ValueCollectionEntryNHibernateImpl<TA, TAImpl, TB>
        : NHibernatePersistenceObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        // where TB : class or struct (string/int)
    {
        protected ValueCollectionEntryNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public virtual void UpdateParent(string propertyName, IDataObject parentObj)
        {
            throw new MemberAccessException(String.Format("No {0} property in {1}", propertyName, GetImplementedInterface().FullName));
        }
    }
}
