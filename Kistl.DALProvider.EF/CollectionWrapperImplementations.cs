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
        : CollectionASideWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public EntityCollectionASideWrapper(BTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.GetEFContext().DeleteObject(entry);
            base.OnEntryRemoved(entry);
        }

    }

    public sealed class EntityListASideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : ListASideWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewListEntry<ATYPE, BTYPE>, new()
    {
        public EntityListASideWrapper(BTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.GetEFContext().DeleteObject(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class EntityCollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : CollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public EntityCollectionBSideWrapper(ATYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.GetEFContext().DeleteObject(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class EntityListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : ListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewListEntry<ATYPE, BTYPE>, new()
    {
        public EntityListBSideWrapper(ATYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.GetEFContext().DeleteObject(entry);
            base.OnEntryRemoved(entry);
        }
    }

}
