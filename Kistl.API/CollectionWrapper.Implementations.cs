using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{

    public abstract class CollectionASideWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : CollectionEntriesWrapper<ATYPE, BTYPE, BTYPE, ATYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, INewCollectionEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        public CollectionASideWrapper(BTYPE parentObject, BASECOLLECTIONTYPE baseCollection)
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
            entry.A = item;
            entry.B = ParentObject;
            return entry;
        }

    }

    public abstract class ListASideWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : ListEntriesWrapper<ATYPE, BTYPE, BTYPE, ATYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, INewListEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        public ListASideWrapper(BTYPE parentObject, BASECOLLECTIONTYPE baseCollection)
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
            entry.A = item;
            entry.B = ParentObject;
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

    public abstract class CollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : CollectionEntriesWrapper<ATYPE, BTYPE, ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where ENTRYTYPE : class, INewCollectionEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        public CollectionBSideWrapper(ATYPE parentObject, BASECOLLECTIONTYPE baseCollection)
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

    public abstract class ListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : ListEntriesWrapper<ATYPE, BTYPE, ATYPE, BTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        where ATYPE : class, IDataObject
        where ENTRYTYPE : class, INewListEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        public ListBSideWrapper(ATYPE parentObject, BASECOLLECTIONTYPE baseCollection)
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
