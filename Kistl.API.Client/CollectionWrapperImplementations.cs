using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.API.Client
{

    public sealed class ClientRelationASideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationASideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE, ICollection<ENTRYTYPE>>/*, IList<ATYPE>*/, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationCollectionEntry<ATYPE, BTYPE>, new()
    {
        public ClientRelationASideCollectionWrapper(BTYPE parentObject, ICollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
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

        //#region IList<ATYPE> Members

        //public int IndexOf(ATYPE item)
        //{
        //    return Collection.IndexOf(GetEntryOrDefault(item));
        //}

        //public void Insert(int index, ATYPE item)
        //{
        //    Collection.Insert(index, InitialiseEntry(CreateEntry(item), item));
        //}

        //public void RemoveAt(int index)
        //{
        //    Collection.RemoveAt(index);
        //}

        //public ATYPE this[int index]
        //{
        //    get
        //    {
        //        return ItemFromEntry(Collection[index]);
        //    }
        //    set
        //    {
        //        Collection[index].A = value;
        //    }
        //}

        //#endregion

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

    public sealed class ClientRelationASideListWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationASideListWrapper<ATYPE, BTYPE, ENTRYTYPE, ICollection<ENTRYTYPE>>, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationListEntry<ATYPE, BTYPE>, new()
    {
        public ClientRelationASideListWrapper(BTYPE parentObject, ICollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
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

    public sealed class ClientRelationBSideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationBSideCollectionWrapper<ATYPE, BTYPE, ENTRYTYPE, ICollection<ENTRYTYPE>>/*, IList<BTYPE>*/, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationCollectionEntry<ATYPE, BTYPE>, new()
    {
        public ClientRelationBSideCollectionWrapper(ATYPE parentObject, ICollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
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

        //#region IList<BTYPE> Members

        //public int IndexOf(BTYPE item)
        //{
        //    return Collection.IndexOf(GetEntryOrDefault(item));
        //}

        //public void Insert(int index, BTYPE item)
        //{
        //    Collection.Insert(index, InitialiseEntry(CreateEntry(item), item));
        //}

        //public void RemoveAt(int index)
        //{
        //    Collection.RemoveAt(index);
        //}

        //public BTYPE this[int index]
        //{
        //    get
        //    {
        //        return ItemFromEntry(Collection[index]);
        //    }
        //    set
        //    {
        //        Collection[index].B = value;
        //    }
        //}

        //#endregion

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

    public sealed class ClientRelationBSideListWrapper<ATYPE, BTYPE, ENTRYTYPE>
        : RelationBSideListWrapper<ATYPE, BTYPE, ENTRYTYPE, ICollection<ENTRYTYPE>>, INotifyCollectionChanged
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationListEntry<ATYPE, BTYPE>, new()
    {
        public ClientRelationBSideListWrapper(ATYPE parentObject, ICollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        protected override ENTRYTYPE CreateEntry(object item)
        {
            return (ENTRYTYPE)ParentObject.Context.CreateRelationCollectionEntry(new ImplementationType(typeof(ENTRYTYPE)).ToInterfaceType());
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
