
namespace Kistl.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    // TODO: use delegate instead of SetPropertyValue. May be up to 300x faster.
    // TODO: take care of SELECT N+1 problem when modifying collection entries

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OneNRelationList<T> : IList<T>, IRelationListSync<T>, IList, INotifyCollectionChanged
        where T : class, INotifyingObject, IDataObject
    {
        private readonly string _propertyName;
        private readonly string _posProperty;
        private readonly IDataObject _owner;
        private readonly Action _ownerNotifier;
        private List<T> collection; // can change

        ///// <param name="fkProperty">the name of the fk_Property which does notification, but not collection fixing</param>
        public OneNRelationList(string fkProperty, string posProperty, IDataObject owner, Action ownerNotifier)
            : this(fkProperty, posProperty, owner, ownerNotifier, new List<T>()) { }

        ///// <param name="fkProperty">the name of the fk_Property which does notification, but not collection fixing</param>
        public OneNRelationList(string fkProperty, string posProperty, IDataObject owner, Action ownerNotifier, IEnumerable<T> collection)
        {
            _propertyName = fkProperty;
            _posProperty = posProperty;
            _owner = owner;
            _ownerNotifier = ownerNotifier;
            this.collection = collection != null ? new List<T>(collection.OrderBy(i => GetPosition(i))) : new List<T>();

            //foreach (var item in this.collection)
            //{
            //    item.PropertyChanged += item_PropertyChanged;
            //}
        }

        void IRelationListSync<T>.AddWithoutSetParent(T item)
        {
            // on ReloadReference, the _posProperty has a valid value
            // we need to insert correctly

            if (!String.IsNullOrEmpty(_posProperty))
            {
                int? pos = GetPosition(item);
                if (pos.HasValue)
                {
                    int idx = collection.FindIndex(i => GetPosition(i) > pos.Value);
                    if (idx >= 0)
                    {
                        collection.Insert(idx, item);
                        OnItemAdded(item, idx);

                        return;
                    }
                }
            }

            collection.Add(item);
            OnItemAdded(item, collection.Count - 1);
        }

        void IRelationListSync<T>.RemoveWithoutClearParent(T item)
        {
            int index = collection.IndexOf(item);
            collection.Remove(item);
            OnItemRemoved(item, index);
        }

        private void NotifyOwner()
        {
            if (_ownerNotifier != null)
                _ownerNotifier();
        }

        private void DoInsert(T item, int index)
        {
            if (item == null)
                throw new ArgumentNullException("item", "Cannot add a NULL Object to this collection");
            if (_owner.Context != item.Context)
                throw new WrongKistlContextException();
            collection.Insert(index, item);
            SetPointerProperty(item);
            if (!String.IsNullOrEmpty(_posProperty))
            {
                Kistl.API.Helper.FixIndices(collection, GetPosition, SetPosition);
            }
            OnItemAdded(item, index);
        }

        private void DoRemoveAt(T item, int index)
        {
            collection.RemoveAt(index);
            ClearPointerProperty(item);
            OnItemRemoved(item, index);
        }

        private bool DoRemove(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "Cannot remove a NULL Object from this collection");

            int index = collection.IndexOf(item);
            if (index < 0)
                return false;
            DoRemoveAt(item, index);
            return true;
        }

        private void DoClear()
        {
            // do nothing if collection is empty
            // this runs contrary to what ObservableCollection<> does,
            // but it's stupid to notify if nothing has changed
            if (collection.Count == 0) { return; }

            OnCollectionResetting();
            foreach (var item in collection)
            {
                ClearPointerProperty(item);
            }
            collection.Clear();
            OnCollectionReset();
        }

        private int? GetPosition(T item)
        {
            if (String.IsNullOrEmpty(_posProperty))
                return -1;
            else
                return item.GetPropertyValue<int?>(_posProperty);
        }

        private void SetPointerProperty(T item)
        {
            (item as IDataObject).UpdateParent(_propertyName, _owner.ID);
        }

        private void SetPosition(T item, int index)
        {
            // TODO: Optimize in Generator
            // Sets the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Sets Parameter.Method__Position__
            item.SetPropertyValue<int?>(_posProperty, index);
        }

        private void ClearPointerProperty(T item)
        {
            (item as IDataObject).UpdateParent(_propertyName, null);
            // TODO: Optimize in Generator
            // Clears the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Clears Parameter.Method__Position__
            if (!String.IsNullOrEmpty(_posProperty))
            {
                item.SetPropertyValue<int?>(_posProperty, null);
            }
        }

        public void ApplyChanges(OneNRelationList<T> list)
        {
            if (list == null)
                return;
            list.OnCollectionResetting();
            list.collection = new List<T>(this.collection);
            list.OnCollectionReset();
        }

        public void AttachToContext(IKistlContext ctx)
        {
            OnCollectionResetting();
            collection = new List<T>(collection.Select(i => ctx.Attach(i)).Cast<T>());
            OnCollectionReset();
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return collection.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            DoInsert(item, index);
        }

        public void RemoveAt(int index)
        {
            DoRemoveAt(collection[index], index);
        }

        public T this[int index]
        {
            get
            {
                return collection[index];
            }
            set
            {
                if (Object.Equals(collection[index], value))
                    return;

                DoRemove(collection[index]);
                DoInsert(value, index);
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            DoInsert(item, collection.Count);
        }

        public void Clear()
        {
            DoClear();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return DoRemove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        private event NotifyCollectionChangedEventHandler _CollectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                _CollectionChanged += value;
            }
            remove
            {
                _CollectionChanged -= value;
            }
        }

        protected virtual void OnItemAdded(T newItem, int index)
        {
            NotifyOwner();

            if (_CollectionChanged != null)
                _CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem, index));

            // newItem.PropertyChanged += item_PropertyChanged;
        }

        protected virtual void OnItemRemoved(T removedItem, int index)
        {
            NotifyOwner();

            if (_CollectionChanged != null)
                _CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));

            //removedItem.PropertyChanged -= item_PropertyChanged;
        }

        protected virtual void OnCollectionReset()
        {
            NotifyOwner();

            if (_CollectionChanged != null)
                _CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected virtual void OnCollectionResetting()
        {
            //foreach (var item in collection)
            //{
            //    item.PropertyChanged -= item_PropertyChanged;
            //}
        }

        #endregion

        #region IList Members

        int IList.Add(object value)
        {
            Add((T)value);
            return this.Count - 1;
        }

        bool IList.Contains(object value)
        {
            var t = value as T;
            if (t == null)
            {
                return false;
            }
            else
            {
                return this.Contains(t);
            }
        }

        int IList.IndexOf(object value)
        {
            var t = value as T;
            if (t == null)
            {
                return -1;
            }
            else
            {
                return this.IndexOf(t);
            }
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (T)value);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            var t = value as T;
            if (t != null)
            {
                this.Remove(t);
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T)value;
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null) { throw new ArgumentNullException("array"); }
            if (!array.GetType().GetElementType().IsAssignableFrom(typeof(T)))
            {
                var msg = String.Format("Mismatch between source and destination type: [{0}] not assignable from [{1}]", array.GetType().GetElementType(), typeof(T));
                throw new ArgumentException(msg, "array");
            }

            ((ICollection)collection).CopyTo(array, index);
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        #endregion
    }
}
