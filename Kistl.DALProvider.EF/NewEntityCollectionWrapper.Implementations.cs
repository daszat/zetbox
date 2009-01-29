using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DALProvider.EF
{

    public sealed class EntityCollectionASideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : EntityCollectionEntriesWrapper<ATYPE, BTYPE, BTYPE, ATYPE, ENTRYTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public EntityCollectionASideWrapper(BTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override IEnumerable<ATYPE> GetItems()
        {
            return Collection.Select(e => e.A);
        }

        protected override ENTRYTYPE GetEntryOrDefault(ATYPE item)
        {
            return Collection.SingleOrDefault(i => i.B.Equals(item));
        }

        protected override ENTRYTYPE CreateEntry(ATYPE item)
        {
            ENTRYTYPE entry = new ENTRYTYPE()
            {
                A = item,
                B = ParentObject
            };
            return entry;
        }
    }

    public sealed class EntityListASideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : EntityListEntriesWrapper<ATYPE, BTYPE, BTYPE, ATYPE, ENTRYTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewListEntry<ATYPE, BTYPE>, new()
    {
        public EntityListASideWrapper(BTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override IEnumerable<ATYPE> GetItems()
        {
            return Collection.Select(e => e.A);
        }

        protected override IEnumerable<ATYPE> GetList()
        {
            return Collection.OrderBy(e => e.AIndex).Select(e => e.A);
        }

        protected override ENTRYTYPE GetEntryOrDefault(ATYPE item)
        {
            return Collection.SingleOrDefault(i => i.B.Equals(item));
        }

        protected override ENTRYTYPE CreateEntry(ATYPE item)
        {
            ENTRYTYPE entry = new ENTRYTYPE()
            {
                A = item,
                B = ParentObject,
                BIndex = Kistl.API.Helper.LASTINDEXPOSITION
            };
            return entry;
        }

        protected override ATYPE ItemFromEntry(ENTRYTYPE entry)
        {
            return entry.A;
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

    public sealed class EntityCollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
    : EntityCollectionEntriesWrapper<ATYPE, BTYPE, ATYPE, BTYPE, ENTRYTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public EntityCollectionBSideWrapper(ATYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override IEnumerable<BTYPE> GetItems()
        {
            return Collection.Select(e => e.B);
        }

        protected override ENTRYTYPE GetEntryOrDefault(BTYPE item)
        {
            return Collection.SingleOrDefault(i => i.A.Equals(item));
        }

        protected override ENTRYTYPE CreateEntry(BTYPE item)
        {
            ENTRYTYPE entry = new ENTRYTYPE()
            {
                A = ParentObject,
                B = item
            };
            return entry;
        }
    }

    public sealed class EntityListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : EntityListEntriesWrapper<ATYPE, BTYPE, ATYPE, BTYPE, ENTRYTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewListEntry<ATYPE, BTYPE>, new()
    {
        public EntityListBSideWrapper(ATYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
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

        protected override ENTRYTYPE GetEntryOrDefault(BTYPE item)
        {
            return Collection.SingleOrDefault(i => i.A.Equals(item));
        }

        protected override ENTRYTYPE CreateEntry(BTYPE item)
        {
            ENTRYTYPE entry = new ENTRYTYPE()
            {
                A = ParentObject,
                AIndex = Kistl.API.Helper.LASTINDEXPOSITION,
                B = item
            };
            return entry;
        }

        protected override BTYPE ItemFromEntry(ENTRYTYPE entry)
        {
            return entry.B;
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
