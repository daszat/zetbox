using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.API.Client
{

    public sealed class ClientCollectionASideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : CollectionASideWrapper<ATYPE, BTYPE, ENTRYTYPE, IList<ENTRYTYPE>>, IList<ATYPE>, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public ClientCollectionASideWrapper(BTYPE parentObject, IList<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return (ENTRYTYPE)ParentObject.Context.CreateCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryAdded(ENTRYTYPE entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, ItemFromEntry(entry)));
        }

        #region IList<ATYPE> Members

        public int IndexOf(ATYPE item)
        {
            return Collection.IndexOf(GetEntryOrDefault(item));
        }

        public void Insert(int index, ATYPE item)
        {
            Collection.Insert(index, InitialiseEntry(CreateEntry(), item));
        }

        public void RemoveAt(int index)
        {
            Collection.RemoveAt(index);
        }

        public ATYPE this[int index]
        {
            get
            {
                return ItemFromEntry(Collection[index]);
            }
            set
            {
                Collection[index].A = value;
            }
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        #endregion

    }

    public sealed class ClientListASideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : ListASideWrapper<ATYPE, BTYPE, ENTRYTYPE, ICollection<ENTRYTYPE>>, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry, INewListEntry<ATYPE, BTYPE>, new()
    {
        public ClientListASideWrapper(BTYPE parentObject, ICollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return (ENTRYTYPE)ParentObject.Context.CreateCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryAdded(ENTRYTYPE entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, ItemFromEntry(entry)));
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        #endregion
    }

    public sealed class ClientCollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : CollectionBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, IList<ENTRYTYPE>>, IList<BTYPE>, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        public ClientCollectionBSideWrapper(ATYPE parentObject, IList<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return (ENTRYTYPE)ParentObject.Context.CreateCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryAdded(ENTRYTYPE entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, ItemFromEntry(entry)));
        }

        #region IList<BTYPE> Members

        public int IndexOf(BTYPE item)
        {
            return Collection.IndexOf(GetEntryOrDefault(item));
        }

        public void Insert(int index, BTYPE item)
        {
            Collection.Insert(index, InitialiseEntry(CreateEntry(), item));
        }

        public void RemoveAt(int index)
        {
            Collection.RemoveAt(index);
        }

        public BTYPE this[int index]
        {
            get
            {
                return ItemFromEntry(Collection[index]);
            }
            set
            {
                Collection[index].B = value;
            }
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        #endregion

    }

    public sealed class ClientListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : ListBSideWrapper<ATYPE, BTYPE, ENTRYTYPE, ICollection<ENTRYTYPE>>, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where ENTRYTYPE : BaseClientCollectionEntry, INewListEntry<ATYPE, BTYPE>, new()
    {
        public ClientListBSideWrapper(ATYPE parentObject, ICollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry()
        {
            return (ENTRYTYPE)ParentObject.Context.CreateCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
        }

        protected override void OnEntryAdded(ENTRYTYPE entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(ENTRYTYPE entry)
        {
            entry.Context.Delete(entry);
            base.OnEntryRemoved(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, ItemFromEntry(entry)));
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        #endregion
    }

}
