
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public sealed class ClientRelationASideCollectionWrapper<TA, TB, TEntry>
        : RelationASideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>/*, IList<ATYPE>*/, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationCollectionEntry<TA, TB>, new()
    {
        public ClientRelationASideCollectionWrapper(TB parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(ParentObject.Context.GetImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(TEntry entry)
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

    public sealed class ClientRelationASideListWrapper<TA, TB, TEntry>
        : RelationASideListWrapper<TA, TB, TEntry, ICollection<TEntry>>, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>, new()
    {
        public ClientRelationASideListWrapper(TB parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(ParentObject.Context.GetImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(TEntry entry)
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

    public sealed class ClientRelationBSideCollectionWrapper<TA, TB, TEntry>
        : RelationBSideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>/*, IList<BTYPE>*/, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationCollectionEntry<TA, TB>, new()
    {
        public ClientRelationBSideCollectionWrapper(TA parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(ParentObject.Context.GetImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(TEntry entry)
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

    public sealed class ClientRelationBSideListWrapper<TA, TB, TEntry>
        : RelationBSideListWrapper<TA, TB, TEntry, ICollection<TEntry>>, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>, new()
    {
        public ClientRelationBSideListWrapper(TA parentObject, ICollection<TEntry> ec)
            : base(parentObject, ec)
        {
        }

        protected override TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.CreateRelationCollectionEntry(ParentObject.Context.GetImplementationType(typeof(TEntry)).ToInterfaceType());
        }

        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ItemFromEntry(entry)));
        }

        protected override void OnEntryRemoved(TEntry entry)
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
