using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{

    public abstract class RelationASideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : BaseRelationCollectionWrapper<ATYPE, BTYPE, BTYPE, ATYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationCollectionEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        protected RelationASideCollectionWrapper(BTYPE parentObject, BASECOLLECTIONTYPE baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<ATYPE> GetItems()
        {
            return Collection.Select(e => e.A);
        }

        protected override ATYPE ItemFromEntry(ENTRYTYPE entry)
        {
            return entry.A;
        }

        protected override ENTRYTYPE InitialiseEntry(ENTRYTYPE entry, ATYPE item)
        {
            entry.B = ParentObject;
            entry.A = item;
            return entry;
        }

    }

    public abstract class RelationASideListWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : BaseRelationListWrapper<ATYPE, BTYPE, BTYPE, ATYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationListEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        protected RelationASideListWrapper(BTYPE parentObject, BASECOLLECTIONTYPE baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<ATYPE> GetItems()
        {
            return Collection.Select(e => e.A);
        }

        protected override IEnumerable<ATYPE> GetList()
        {
            return Collection.OrderBy(e => e.AIndex).Select(e => ItemFromEntry(e));
        }

        protected override ATYPE ItemFromEntry(ENTRYTYPE entry)
        {
            return entry.A;
        }

        protected override ENTRYTYPE InitialiseEntry(ENTRYTYPE entry, ATYPE item)
        {
            entry.B = ParentObject;
            entry.A = item;
            entry.BIndex = Kistl.API.Helper.LASTINDEXPOSITION;
            return entry;
        }


        /// <summary>
        /// Overriden to set the index on the incoming entry
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnEntryAdded(ENTRYTYPE entry)
        {
            base.OnEntryAdded(entry);
            entry.AIndex = Collection.Count - 1;
        }

        protected override int? IndexFromEntry(ENTRYTYPE entry)
        {
            return entry.AIndex;
        }

        protected override void SetIndex(ENTRYTYPE entry, int idx)
        {
            entry.AIndex = idx;
        }

        protected override void SetItem(ENTRYTYPE entry, ATYPE item)
        {
            entry.A = item;
        }
    }

    public abstract class RelationBSideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : BaseRelationCollectionWrapper<ATYPE, BTYPE, ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationCollectionEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        protected RelationBSideCollectionWrapper(ATYPE parentObject, BASECOLLECTIONTYPE baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<BTYPE> GetItems()
        {
            return Collection.Select(e => e.B);
        }

        protected override BTYPE ItemFromEntry(ENTRYTYPE entry)
        {
            return entry.B;
        }

        protected override ENTRYTYPE InitialiseEntry(ENTRYTYPE entry, BTYPE item)
        {
            entry.A = ParentObject;
            entry.B = item;
            return entry;
        }


    }

    public abstract class RelationBSideListWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : BaseRelationListWrapper<ATYPE, BTYPE, ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationListEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        protected RelationBSideListWrapper(ATYPE parentObject, BASECOLLECTIONTYPE baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<BTYPE> GetItems()
        {
            return Collection.Select(e => e.B);
        }

        protected override IEnumerable<BTYPE> GetList()
        {
            return Collection.OrderBy(e => e.BIndex).Select(e => e.B);
        }

        protected override BTYPE ItemFromEntry(ENTRYTYPE entry)
        {
            return entry.B;
        }

        protected override ENTRYTYPE InitialiseEntry(ENTRYTYPE entry, BTYPE item)
        {
            entry.A = ParentObject;
            entry.B = item;
            entry.AIndex = Kistl.API.Helper.LASTINDEXPOSITION;
            return entry;
        }

        /// <summary>
        /// Overriden to set the index on the incoming entry
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnEntryAdded(ENTRYTYPE entry)
        {
            base.OnEntryAdded(entry);
            entry.BIndex = Collection.Count - 1;
        }

        protected override int? IndexFromEntry(ENTRYTYPE entry)
        {
            return entry.BIndex;
        }

        protected override void SetIndex(ENTRYTYPE entry, int idx)
        {
            entry.BIndex = idx;
        }

        protected override void SetItem(ENTRYTYPE entry, BTYPE item)
        {
            entry.B = item;
        }
    }

}
