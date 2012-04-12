
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public abstract class CompoundCollectionEntryNHibernateImpl<TA, TAImpl, TB, TBImpl>
        : NHibernatePersistenceObject
        where TA : class, IDataObject
        where TAImpl : class, IDataObject, TA
        where TB : class, ICompoundObject
        where TBImpl : class, ICompoundObject, TB
    {
        protected CompoundCollectionEntryNHibernateImpl(Func<IFrozenContext> lazyCtx)
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
