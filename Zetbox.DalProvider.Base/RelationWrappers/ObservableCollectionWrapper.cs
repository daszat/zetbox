
namespace Zetbox.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    public class ObservableASideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : ASideCollectionWrapper<TA, TB, TEntry, TBaseCollection>, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public ObservableASideCollectionWrapper(TB parentObject, TBaseCollection baseCollection)
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

    public class ObservableBSideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : BSideCollectionWrapper<TA, TB, TEntry, TBaseCollection>, INotifyCollectionChanged
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public ObservableBSideCollectionWrapper(TA parentObject, TBaseCollection baseCollection)
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