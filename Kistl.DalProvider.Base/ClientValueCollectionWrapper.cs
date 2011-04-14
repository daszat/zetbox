
// TODO: move to Kistl.DalProvider.Base.RelationWrappers namespace
namespace Kistl.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Common;
    using Kistl.DalProvider.Base.RelationWrappers;

    public class ClientValueCollectionWrapper<TParent, TValue, TEntry, TEntryImpl, TEntryCollection>
        : ValueCollectionWrapper<TParent, TValue, TEntryImpl, TEntryCollection>, IRelationListSync<TEntry>, INotifyCollectionChanged
        where TParent : IDataObject
        where TEntry : IValueCollectionEntry<TParent, TValue>
        where TEntryImpl : class, TEntry
        where TEntryCollection : ICollection<TEntryImpl>
    {
        public ClientValueCollectionWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            var notifier = collection as INotifyCollectionChanged;
            if (notifier != null)
                notifier.CollectionChanged += (sender, e) => this.NotifyOwner();
        }

        public ClientValueCollectionWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : this(ctx, parent, null, collection)
        {
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void OnEntryAdded(TEntryImpl entry)
        {
            base.OnEntryAdded(entry);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, entry));
            }
        }

        protected override void OnEntryRemoved(TEntryImpl entry)
        {
            base.OnEntryRemoved(entry);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, entry));
            }
        }
        #endregion

        #region IRelationListSync<TEntry> Members

        public virtual void AddWithoutSetParent(TEntry entry)
        {
            if (entry == null) { throw new ArgumentNullException("entry"); }
            var entryImpl = entry as TEntryImpl;
            if (entryImpl == null) { throw new ArgumentOutOfRangeException("entry"); }

            OnEntryAdding(entryImpl);
            collection.Add(entryImpl);
            OnEntryAdded(entryImpl);
        }

        public virtual void RemoveWithoutClearParent(TEntry entry)
        {
            if (entry == null) { throw new ArgumentNullException("entry"); }
            var entryImpl = entry as TEntryImpl;
            if (entryImpl == null) { throw new ArgumentOutOfRangeException("entry"); }

            OnEntryRemoving(entryImpl);
            collection.Remove(entryImpl);
            OnEntryRemoved(entryImpl);
        }

        #endregion
    }

    public class ClientValueListWrapper<TParent, TValue, TEntry, TEntryImpl, TEntryCollection> 
        : ValueListWrapper<TParent, TValue, TEntryImpl, TEntryCollection>
        where TParent : IDataObject
        where TEntry : IValueListEntry<TParent, TValue>
        where TEntryImpl : class, TEntry
        where TEntryCollection : IList<TEntryImpl>
    {
        public ClientValueListWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }

        public ClientValueListWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }

        #region IRelationListSync<TEntry> Members

        public virtual void AddWithoutSetParent(TEntry entry)
        {
            if (entry == null) { throw new ArgumentNullException("entry"); }
            var entryImpl = entry as TEntryImpl;
            if (entryImpl == null) { throw new ArgumentOutOfRangeException("entry"); }

            OnEntryAdding(entryImpl);
            collection.Add(entryImpl);
            OnEntryAdded(entryImpl);
        }

        public virtual void RemoveWithoutClearParent(TEntry entry)
        {
            if (entry == null) { throw new ArgumentNullException("entry"); }
            var entryImpl = entry as TEntryImpl;
            if (entryImpl == null) { throw new ArgumentOutOfRangeException("entry"); }

            OnEntryRemoving(entryImpl);
            collection.Remove(entryImpl);
            OnEntryRemoved(entryImpl);
        }

        #endregion
    }
}
