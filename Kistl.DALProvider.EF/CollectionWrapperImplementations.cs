using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DALProvider.EF
{

    public sealed class EntityRelationASideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationASideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationCollectionEntry<ATYPE, BTYPE>, new()
    {
        public EntityRelationASideCollectionWrapper(BTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }

    }

    public sealed class EntityRelationASideListWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationASideListWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationListEntry<ATYPE, BTYPE>, new()
    {
        public EntityRelationASideListWrapper(BTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class EntityRelationBSideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationBSideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationCollectionEntry<ATYPE, BTYPE>, new()
    {
        public EntityRelationBSideCollectionWrapper(ATYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class EntityRelationBSideListWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationBSideListWrapper<ATYPE, BTYPE, ENTRYTYPE, EntityCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationListEntry<ATYPE, BTYPE>, new()
    {
        public EntityRelationBSideListWrapper(ATYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

}
