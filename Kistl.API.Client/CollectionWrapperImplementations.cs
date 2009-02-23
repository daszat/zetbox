using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.API.Client
{

    public sealed class ClientCollectionASideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : CollectionASideWrapper<ATYPE, BTYPE, ENTRYTYPE, NotifyingObservableCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public ClientCollectionASideWrapper(BTYPE parentObject, NotifyingObservableCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }

    }

    public sealed class ClientListASideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : ListASideWrapper<ATYPE, BTYPE, ENTRYTYPE, NotifyingObservableCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry,  INewListEntry<ATYPE, BTYPE>, new()
    {
        public ClientListASideWrapper(BTYPE parentObject, NotifyingObservableCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class ClientCollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : CollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, NotifyingObservableCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry,  INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public ClientCollectionBSideWrapper(ATYPE parentObject, NotifyingObservableCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

    public sealed class ClientListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : ListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, NotifyingObservableCollection<ENTRYTYPE>>
        where ATYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry,  INewListEntry<ATYPE, BTYPE>, new()
    {
        public ClientListBSideWrapper(ATYPE parentObject, NotifyingObservableCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return new ENTRYTYPE();
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
        }
    }

}
