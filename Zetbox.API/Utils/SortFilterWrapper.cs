// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Wraps a collection to enable sorting and filtering
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SortFilterWrapper<T> : INotifyCollectionChanged, IList<T>
    {
        private List<T> _sortedFilteredList;
        private IEnumerable _collection;
        private InterfaceType _elementType;
        private INotifyCollectionChanged _notifier;

        private string _sortProp;
        private ListSortDirection _direction = ListSortDirection.Ascending;
        private string _filterText;

        public SortFilterWrapper(IEnumerable collection, InterfaceType elementType, INotifyCollectionChanged notifier, string defaultSortProperty)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (notifier == null) throw new ArgumentNullException("notifier");
            if (elementType == null) throw new ArgumentNullException("elementType");

            _collection = collection;
            _elementType = elementType;
            _notifier = notifier;
            _notifier.CollectionChanged += new NotifyCollectionChangedEventHandler(notifier_CollectionChanged);
            _sortProp = defaultSortProperty;
            Sort(_sortProp, _direction, null);
        }

        public void Sort(string p, ListSortDirection direction, NotifyCollectionChangedEventArgs eventArgs = null)
        {
            _sortProp = p;
            _direction = direction;
            ApplySortAndFilter(eventArgs);
        }

        /// <summary>
        /// Filters all string properties
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="eventArgs"></param>
        public void Filter(string txt, NotifyCollectionChangedEventArgs eventArgs = null)
        {
            _filterText = txt;
            ApplySortAndFilter(eventArgs);
        }

        private void ApplySortAndFilter(NotifyCollectionChangedEventArgs eventArgs)
        {
            var qry = _collection.AsQueryable()
                    .AddCast(_elementType.Type);

            if (!string.IsNullOrEmpty(_sortProp))
            { 
                qry = qry.OrderBy(string.Format("{0} {1}", _sortProp, _direction == ListSortDirection.Descending ? "desc" : string.Empty));
            }

            if (!string.IsNullOrEmpty(_filterText))
            {
                var filter = _elementType.Type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                    .Where(i => i.PropertyType == typeof(string))
                    .Select(i => string.Format("({0} != null && {0}.ToLower().Contains(@0))", i.Name))
                    .ToArray();
                qry = qry.Where(string.Join(" || ", filter), _filterText.ToLower());
            }

            _sortedFilteredList = qry.Cast<T>().ToList();

            if (eventArgs != null)
                OnCollectionChanged(eventArgs);
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs eventArgs)
        {
            CollectionChanged?.Invoke(this, eventArgs);
        }

        void notifier_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems == null && e.NewItems == null)
            {
                // we would have to map here, ignore
                Sort(_sortProp, _direction);
                Filter(_filterText);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                return;
            }

            switch (e.Action)
            {
                // we can pass the event on with the specified NewItems
                // but we must loose the indices, as they'll change on sort
                case NotifyCollectionChangedAction.Add:
                    Sort(_sortProp, _direction);
                    Filter(_filterText);
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(e.Action, e.NewItems));
                    break;
                // we can pass the event on with the specified OldItems
                // but we must loose the indices, as they'll change on sort
                case NotifyCollectionChangedAction.Remove:
                    Sort(_sortProp, _direction);
                    Filter(_filterText);
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(e.Action, e.OldItems));
                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    // too complex to map (could result in multiple new events), just notify as reset
                    Sort(_sortProp, _direction);
                    Filter(_filterText);
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    break;
                default:
                    break;
            }
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return _sortedFilteredList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {

        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                return _sortedFilteredList[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<DataObjectViewModel> Members

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            return _sortedFilteredList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _sortedFilteredList.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _sortedFilteredList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sortedFilteredList.GetEnumerator();
        }

        #endregion
    }
}
