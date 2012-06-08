
namespace Kistl.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public class ObservableASideListWrapper<TA, TB, TEntry, TBaseCollection>
        : ASideListWrapper<TA, TB, TEntry, TBaseCollection>, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public ObservableASideListWrapper(TB parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
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

    public class ObservableBSideListWrapper<TA, TB, TEntry, TBaseCollection>
        : BSideListWrapper<TA, TB, TEntry, TBaseCollection>, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public ObservableBSideListWrapper(TA parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
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