
namespace Kistl.API
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class RelationASideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseRelationCollectionWrapper<TA, TB, TB, TA, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationCollectionEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        protected RelationASideCollectionWrapper(TB parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TA> GetItems()
        {
            Collection.ForEach(e => e.AttachToContext(ParentObject.Context));
            return Collection.Select(e => e.A);
        }

        protected override TA ItemFromEntry(TEntry entry)
        {
            entry.AttachToContext(ParentObject.Context);
            return entry.A;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TA item)
        {
            entry.B = ParentObject;
            entry.A = item;
            return entry;
        }

    }

    public abstract class RelationASideListWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseRelationListWrapper<TA, TB, TB, TA, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        protected RelationASideListWrapper(TB parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TA> GetItems()
        {
            Collection.ForEach(e => e.AttachToContext(ParentObject.Context));
            return Collection.Select(e => e.A);
        }

        protected override IEnumerable<TEntry> GetSortedEntries()
        {
            return Collection.OrderBy(e => e.AIndex);
        }

        protected override TA ItemFromEntry(TEntry entry)
        {
            entry.AttachToContext(ParentObject.Context);
            return entry.A;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TA item)
        {
            entry.B = ParentObject;
            entry.A = item;
            entry.AIndex = Kistl.API.Helper.LASTINDEXPOSITION;
            entry.BIndex = Kistl.API.Helper.LASTINDEXPOSITION;
            return entry;
        }

        /// <summary>
        /// Overriden to set the index on the incoming entry
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            if (!entry.AIndex.HasValue || entry.AIndex == Kistl.API.Helper.LASTINDEXPOSITION)
            {
                Kistl.API.Helper.FixIndices(GetSortedEntries().ToList(), IndexFromEntry, SetIndex);
            }
        }

        protected override int? IndexFromEntry(TEntry entry)
        {
            return entry.AIndex;
        }

        protected override void SetIndex(TEntry entry, int idx)
        {
            entry.AIndex = idx;
        }

        protected override void SetItem(TEntry entry, TA item)
        {
            entry.A = item;
        }
    }

    public abstract class RelationBSideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseRelationCollectionWrapper<TA, TB, TA, TB, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationCollectionEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        protected RelationBSideCollectionWrapper(TA parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TB> GetItems()
        {
            Collection.ForEach(e => e.AttachToContext(ParentObject.Context));
            return Collection.Select(e => e.B);
        }

        protected override TB ItemFromEntry(TEntry entry)
        {
            entry.AttachToContext(ParentObject.Context);
            return entry.B;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TB item)
        {
            entry.A = ParentObject;
            entry.B = item;
            return entry;
        }
    }

    public abstract class RelationBSideListWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseRelationListWrapper<TA, TB, TA, TB, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        protected RelationBSideListWrapper(TA parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TB> GetItems()
        {
            Collection.ForEach(e => e.AttachToContext(ParentObject.Context));
            return Collection.Select(e => e.B);
        }

        protected override IEnumerable<TEntry> GetSortedEntries()
        {
            return Collection.OrderBy(e => e.BIndex);
        }

        protected override TB ItemFromEntry(TEntry entry)
        {
            entry.AttachToContext(ParentObject.Context);
            return entry.B;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TB item)
        {
            entry.A = ParentObject;
            entry.B = item;
            entry.AIndex = Kistl.API.Helper.LASTINDEXPOSITION;
            entry.BIndex = Kistl.API.Helper.LASTINDEXPOSITION;
            return entry;
        }

        /// <summary>
        /// Overriden to set the index on the incoming entry
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            if (!entry.BIndex.HasValue || entry.BIndex == Kistl.API.Helper.LASTINDEXPOSITION)
            {
                Kistl.API.Helper.FixIndices(GetSortedEntries().ToList(), IndexFromEntry, SetIndex);
            }
        }

        protected override int? IndexFromEntry(TEntry entry)
        {
            return entry.BIndex;
        }

        protected override void SetIndex(TEntry entry, int idx)
        {
            entry.BIndex = idx;
        }

        protected override void SetItem(TEntry entry, TB item)
        {
            entry.B = item;
        }
    }

}
