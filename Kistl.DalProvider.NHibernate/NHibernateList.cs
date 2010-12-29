
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public class NHibernateASideCollectionWrapper<TA, TB, TEntry>
        : RelationASideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>, new()
    {
        public NHibernateASideCollectionWrapper(TB parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }
        protected override TEntry CreateEntry(object item)
        {
            throw new NotImplementedException();
        }
    }
    public class NHibernateBSideCollectionWrapper<TA, TB, TEntry>
        : RelationBSideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>, new()
    {
        public NHibernateBSideCollectionWrapper(TA parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }
        protected override TEntry CreateEntry(object item)
        {
            throw new NotImplementedException();
        }
    }


    public class NHibernateASideListWrapper<TA, TB, TEntry>
        : RelationASideListWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>, new()
    {
        public NHibernateASideListWrapper(TB parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }
        protected override TEntry CreateEntry(object item)
        {
            throw new NotImplementedException();
        }
    }
    public class NHibernateBSideListWrapper<TA, TB, TEntry>
        : RelationBSideListWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>, new()
    {
        public NHibernateBSideListWrapper(TA parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }
        protected override TEntry CreateEntry(object item)
        {
            throw new NotImplementedException();
        }
    }
}
