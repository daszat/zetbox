
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.DalProvider.Base.RelationWrappers;

    public class NHibernateASideCollectionWrapper<TA, TB, TEntry>
        : ASideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
    {
        private readonly Func<TEntry, IRelationListSync<TEntry>> _getSyncer;

        public NHibernateASideCollectionWrapper(TB parentObject, ICollection<TEntry> baseCollection, Func<TEntry, IRelationListSync<TEntry>> getSyncer)
            : base(parentObject, baseCollection)
        {
            _getSyncer = getSyncer;
        }

        protected override void OnEntryAdding(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryAdding(entry);
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            var bside = _getSyncer(entry);
            if (bside != null)
            {
                bside.AddWithoutSetParent(entry);
            }
            base.OnEntryAdded(entry);
            // notify BSide
            // notify ASide
        }

        protected override void OnEntryRemoving(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryRemoving(entry);
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            var bside = _getSyncer(entry);
            if (bside != null)
            {
                bside.RemoveWithoutClearParent(entry);
            }
            base.OnEntryRemoved(entry);
            // notify BSide
            // notify ASide
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            Collection.Add(item);
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            Collection.Remove(item);
        }
    }

    public class NHibernateBSideCollectionWrapper<TA, TB, TEntry>
        : BSideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
    {
        private readonly Func<TEntry, IRelationListSync<TEntry>> _getSyncer;

        public NHibernateBSideCollectionWrapper(TA parentObject, ICollection<TEntry> baseCollection, Func<TEntry, IRelationListSync<TEntry>> getSyncer)
            : base(parentObject, baseCollection)
        {
            _getSyncer = getSyncer;
        }

        protected override void OnEntryAdding(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryAdding(entry);
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            var aside = _getSyncer(entry);
            if (aside != null)
            {
                aside.AddWithoutSetParent(entry);
            }
            base.OnEntryAdded(entry);
            // notify BSide
            // notify ASide
        }

        protected override void OnEntryRemoving(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryRemoving(entry);
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            var aside = _getSyncer(entry);
            if (aside != null)
            {
                aside.RemoveWithoutClearParent(entry);
            }
            base.OnEntryRemoved(entry);
            // notify BSide
            // notify ASide
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            Collection.Add(item);
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            Collection.Remove(item);
        }
    }


    public class NHibernateASideListWrapper<TA, TB, TEntry>
        : ASideListWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
    {
        private readonly Func<TEntry, IRelationListSync<TEntry>> _getSyncer;

        public NHibernateASideListWrapper(TB parentObject, ICollection<TEntry> baseCollection, Func<TEntry, IRelationListSync<TEntry>> getSyncer)
            : base(parentObject, baseCollection)
        {
            _getSyncer = getSyncer;
        }

        protected override void OnEntryAdding(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryAdding(entry);
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            var bside = _getSyncer(entry);
            if (bside != null)
            {
                bside.AddWithoutSetParent(entry);
            }
            base.OnEntryAdded(entry);
            // notify BSide
            // notify ASide
        }

        protected override void OnEntryRemoving(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryRemoving(entry);
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            var bside = _getSyncer(entry);
            if (bside != null)
            {
                bside.RemoveWithoutClearParent(entry);
            }
            base.OnEntryRemoved(entry);
            // notify BSide
            // notify ASide
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            Collection.Add(item);
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            Collection.Remove(item);
        }
    }

    public class NHibernateBSideListWrapper<TA, TB, TEntry>
        : BSideListWrapper<TA, TB, TEntry, ICollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
    {
        private readonly Func<TEntry, IRelationListSync<TEntry>> _getSyncer;

        public NHibernateBSideListWrapper(TA parentObject, ICollection<TEntry> baseCollection, Func<TEntry, IRelationListSync<TEntry>> getSyncer)
            : base(parentObject, baseCollection)
        {
            _getSyncer = getSyncer;
        }

        protected override void OnEntryAdding(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryAdding(entry);
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            var aside = _getSyncer(entry);
            if (aside != null)
            {
                aside.AddWithoutSetParent(entry);
            }
            base.OnEntryAdded(entry);
            // notify BSide
            // notify ASide
        }

        protected override void OnEntryRemoving(TEntry entry)
        {
            // notify ASide
            // notify BSide
            base.OnEntryRemoving(entry);
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            var aside = _getSyncer(entry);
            if (aside != null)
            {
                aside.RemoveWithoutClearParent(entry);
            }
            base.OnEntryRemoved(entry);
            // notify BSide
            // notify ASide
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            Collection.Add(item);
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            Collection.Remove(item);
        }
    }
}
