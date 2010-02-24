
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public class ClientValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>
        : ValueCollectionWrapper<TParent, TValue, TEntry, TEntryCollection>, INotifyCollectionChanged
        where TParent : IDataObject
        where TEntry : class, IValueCollectionEntry<TParent, TValue>
        where TEntryCollection : ICollection<TEntry>
    {
        public ClientValueCollectionWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }

        public ClientValueCollectionWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, entry));
            }
        }

        protected override void OnEntryRemoved(TEntry entry)
        {
            base.OnEntryRemoved(entry);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, entry));
            }
        }
        #endregion
    }

    public class ClientValueListWrapper<TParent, TValue, TEntry, TEntryCollection> : ValueListWrapper<TParent, TValue, TEntry, TEntryCollection>
        where TParent : IDataObject
        where TEntry : class, IValueListEntry<TParent, TValue>
        where TEntryCollection : IList<TEntry>
    {
        public ClientValueListWrapper(IKistlContext ctx, TParent parent, Action parentNotifier, TEntryCollection collection)
            : base(ctx, parent, parentNotifier, collection)
        {
        }

        public ClientValueListWrapper(IKistlContext ctx, TParent parent, TEntryCollection collection)
            : base(ctx, parent, null, collection)
        {
        }
    }
}
