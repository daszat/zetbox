using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{

    // TODO: use delegate instead of SetPropertyValue. May be up to 300x faster.
    // TODO: rename to ...List
    // TODO: take care of SELECT N+1 problem when modifying collection entries

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BackReferenceCollection<T> : IList<T>, INotifyCollectionChanged
        where T : class, IDataObject
    {
        private string _fkProperty;
        private string _posProperty;
        private IDataObject _parent;
        List<T> collection;

        public BackReferenceCollection(string propertyName, IDataObject parent)
            : this("fk_" + propertyName, propertyName + Helper.PositionSuffix, parent)
        {

        }

        public BackReferenceCollection(string propertyName, IDataObject parent, IEnumerable<T> collection)
            : this("fk_" + propertyName, propertyName + Helper.PositionSuffix, parent, collection)
        {

        }

        /// <param name="fkProperty">the name of the fk_Property which does notification, but not collection fixing</param>
        public BackReferenceCollection(string fkProperty, string posProperty, IDataObject parent)
        {
            _fkProperty = fkProperty;
            _posProperty = posProperty;
            _parent = parent;
            collection = new List<T>();
        }

        /// <param name="fkProperty">the name of the fk_Property which does notification, but not collection fixing</param>
        public BackReferenceCollection(string fkProperty, string posProperty, IDataObject parent, IEnumerable<T> collection)
            : this(fkProperty, posProperty, parent)
        {
            this.collection = new List<T>(collection);
        }

        public void AddWithoutSetParent(T item)
        {
            collection.Add(item);
            OnItemAdded(item);
        }

        public void RemoveWithoutClearParent(T item)
        {
            collection.Remove(item);
            OnItemRemoved(item);
        }

        private void AddToList(T item, int index)
        {
            if (item == null) throw new ArgumentNullException("item", "Cannot add a NULL Object to this collection");
            SetPointerProperty(item, index);
            OnItemAdded(item);
        }

        private void RemoveFromList(T item)
        {
            if (item == null) throw new ArgumentNullException("item", "Cannot remove a NULL Object to this collection");
            ClearPointerProperty(item);
            OnItemRemoved(item);
        }

        private void SetPointerProperty(T item, int index)
        {
            if (item.GetPropertyValue<int?>(_fkProperty) != _parent.ID)
            {
                item.SetPrivatePropertyValue<int?>(_fkProperty, _parent.ID);
            }
            // TODO: Optimize in Generator
            // Sets the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Sets Parameter.Method__Position__
            if (item.HasProperty(_posProperty))
            {
                item.SetPropertyValue<int?>(_posProperty, index);
            }
        }

        private void ClearPointerProperty(T item)
        {
            if (item.GetPropertyValue<int?>(_fkProperty) != null)
            {
                item.SetPrivatePropertyValue<int?>(_fkProperty, null);
            }
            // TODO: Optimize in Generator
            // Clears the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Clears Parameter.Method__Position__
            if (item.HasProperty(_posProperty))
            {
                item.SetPropertyValue<int?>(_posProperty, null);
            }
        }

        public void ApplyChanges(BackReferenceCollection<T> list)
        {
            if (list == null) return;
            list.collection = new List<T>(this.collection);
            list.OnCollectionReset();
        }

        public void AttachToContext(IKistlContext ctx)
        {
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
            collection.Insert(index, item);
            AddToList(item, index);
        }

        public void RemoveAt(int index)
        {
            T item = collection[index];
            collection.RemoveAt(index);
            RemoveFromList(item);
        }

        public T this[int index]
        {
            get
            {
                return collection[index];
            }
            set
            {
                if (collection[index].Equals(value)) return;

                T item = collection[index];
                collection[index] = value;

                RemoveFromList(item);
                AddToList(value, index);
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            collection.Add(item);
            AddToList(item, collection.Count - 1);
        }

        public void Clear()
        {
            var tmpCollection = collection.ToArray();
            collection.Clear();
            tmpCollection.ForEach(i => RemoveFromList(i));
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
            var result = collection.Remove(item);
            RemoveFromList(item);
            return result;
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnItemAdded(T newItem)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem));
        }

        protected virtual void OnItemRemoved(T removedItem)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem));
        }

        protected virtual void OnCollectionReset()
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        #endregion
    }
}
