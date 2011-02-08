
namespace Kistl.Client.Presentables.ValueViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;

    /// <summary>
    /// </summary>
    public class ObjectCollectionViewModel
        : BaseObjectCollectionViewModel<IReadOnlyObservableList<DataObjectViewModel>, ICollection<IDataObject>>, IValueCollectionViewModel<DataObjectViewModel, IReadOnlyObservableList<DataObjectViewModel>>
    {
        public new delegate ObjectCollectionViewModel Factory(IKistlContext dataCtx, IValueModel mdl);

        public ObjectCollectionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IObjectCollectionValueModel<ICollection<IDataObject>> mdl)
            : base(appCtx, dataCtx, mdl)
        {
        }

        #region Public interface and IReadOnlyValueModel<IReadOnlyObservableCollection<DataObjectViewModel>> Members

        private ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel> _valueCache;
        public override IReadOnlyObservableList<DataObjectViewModel> Value
        {
            get
            {
                EnsureValueCache();
                return _valueCache;
            }
            set
            {
                throw new NotSupportedException();
            }

        }

        private SortedWrapper _wrapper = null;
        private class SortedWrapper : INotifyCollectionChanged, IList<IDataObject>
        {
            private List<IDataObject> _sortedList;
            private IEnumerable _collection;
            private INotifyCollectionChanged _notifier;

            private string _sortProp = "ID";
            private ListSortDirection _direction = ListSortDirection.Ascending;

            public SortedWrapper(IEnumerable collection, INotifyCollectionChanged notifier)
            {
                _collection = collection;
                _notifier = notifier;
                _notifier.CollectionChanged += new NotifyCollectionChangedEventHandler(notifier_CollectionChanged);
                Sort(_sortProp, _direction);
            }

            public void Sort(string p, ListSortDirection direction)
            {
                _sortProp = p;
                _direction = direction;
                _sortedList = _collection.AsQueryable()
                    .OrderBy(string.Format("{0} {1}", _sortProp, _direction == ListSortDirection.Descending ? "desc" : string.Empty))
                    .Cast<IDataObject>()
                    .ToList();
                OnCollectionChanged();
            }

            private void OnCollectionChanged()
            {
                var temp = CollectionChanged;
                if (temp != null)
                {
                    temp(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }

            void notifier_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                Sort(_sortProp, _direction);
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
                throw new NotImplementedException();
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

        protected override void EnsureValueCache()
        {
            if (_wrapper == null)
            {
                _wrapper = new SortedWrapper(ObjectCollectionModel.UnderlyingCollection, ObjectCollectionModel);
                _valueCache = new ReadOnlyObservableProjectedList<IDataObject, DataObjectViewModel>(
                    _wrapper,
                    obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, obj),
                    mdl => mdl.Object);
                
                _valueCache.CollectionChanged += _valueCache_CollectionChanged;
            }
        }

        void _valueCache_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyValueChanged();
        }

        public bool HasPersistentOrder
        {
            get
            {
                return false;
            }
        }

        #endregion

        public void Sort(string propName, ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentNullException("propName");
            EnsureValueCache();
            _wrapper.Sort(propName, direction);
        }
    }
}
