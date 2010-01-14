using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DalProvider.EF
{

    public sealed class EntityRelationASideCollectionWrapper<TA, TB, TEntry>
        : RelationASideCollectionWrapper<TA, TB, TEntry, EntityCollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationCollectionEntry<TA, TB>, new()
    {
        public EntityRelationASideCollectionWrapper(TB parentObject, EntityCollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }

    }

    public sealed class EntityRelationASideListWrapper<TA, TB, TEntry>
        : RelationASideListWrapper<TA, TB, TEntry, EntityCollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationListEntry<TA, TB>, new()
    {
        public EntityRelationASideListWrapper(TB parentObject, EntityCollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class EntityRelationBSideCollectionWrapper<TA, TB, TEntry>
        : RelationBSideCollectionWrapper<TA, TB, TEntry, EntityCollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationCollectionEntry<TA, TB>, new()
    {
        public EntityRelationBSideCollectionWrapper(TA parentObject, EntityCollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class EntityRelationBSideListWrapper<TA, TB, TEntry>
        : RelationBSideListWrapper<TA, TB, TEntry, EntityCollection<TEntry>>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, IRelationListEntry<TA, TB>, new()
    {
        public EntityRelationBSideListWrapper(TA parentObject, EntityCollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            ParentObject.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

}
