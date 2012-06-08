
namespace Zetbox.API.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;

    public sealed class SortedWrapper : INotifyCollectionChanged, IList<IDataObject>
    {
        private List<IDataObject> _sortedList;
        private IEnumerable _collection;
        private INotifyCollectionChanged _notifier;

        private string _sortProp;
        private ListSortDirection _direction = ListSortDirection.Ascending;

        public SortedWrapper(IEnumerable collection, INotifyCollectionChanged notifier, string defaultSortProperty)
        {
            _collection = collection;
            _notifier = notifier;
            _notifier.CollectionChanged += new NotifyCollectionChangedEventHandler(notifier_CollectionChanged);
            _sortProp = defaultSortProperty;
            Sort(_sortProp, _direction, null);
        }

        public void Sort(string p, ListSortDirection direction, NotifyCollectionChangedEventArgs eventArgs)
        {
            _sortProp = p;
            _direction = direction;
            if (string.IsNullOrEmpty(_sortProp))
            {
                _sortedList = _collection.Cast<IDataObject>().ToList();
            }
            else
            {
                _sortedList = _collection.AsQueryable()
                    .OrderBy(string.Format("{0} {1}", _sortProp, _direction == ListSortDirection.Descending ? "desc" : string.Empty))
                    .Cast<IDataObject>()
                    .ToList();
            }
            if (eventArgs != null)
                OnCollectionChanged(eventArgs);
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs eventArgs)
        {
            var temp = CollectionChanged;
            if (temp != null)
            {
                temp(this, eventArgs);
            }
        }

        void notifier_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems == null && e.NewItems == null)
            {
                // we would have to map here, ignore
                Sort(_sortProp, _direction, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                return;
            }

            switch (e.Action)
            {
                // we can pass the event on with the specified NewItems
                // but we must loose the indices, as they'll change on sort
                case NotifyCollectionChangedAction.Add:
                    Sort(_sortProp, _direction, new NotifyCollectionChangedEventArgs(e.Action, e.NewItems));
                    break;
                // we can pass the event on with the specified OldItems
                // but we must loose the indices, as they'll change on sort
                case NotifyCollectionChangedAction.Remove:
                    Sort(_sortProp, _direction, new NotifyCollectionChangedEventArgs(e.Action, e.OldItems));
                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    // too complex to map (could result in multiple new events), just notify as reset
                    Sort(_sortProp, _direction, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    break;
                default:
                    break;
            }
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region IList<IDataObject> Members

        public int IndexOf(IDataObject item)
        {
            return _sortedList.IndexOf(item);
        }

        public void Insert(int index, IDataObject item)
        {
            
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IDataObject this[int index]
        {
            get
            {
                return _sortedList[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<DataObjectViewModel> Members

        public void Add(IDataObject item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IDataObject item)
        {
            return _sortedList.Contains(item);
        }

        public void CopyTo(IDataObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _sortedList.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(IDataObject item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<IDataObject> Members

        public IEnumerator<IDataObject> GetEnumerator()
        {
            return _sortedList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sortedList.GetEnumerator();
        }

        #endregion
    }
}
