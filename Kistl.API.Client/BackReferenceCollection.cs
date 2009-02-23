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
        private string _pointerProperty;
        private IDataObject _parent;
        List<T> collection;

        public BackReferenceCollection(string pointerProperty, IDataObject parent)
        {
            _pointerProperty = pointerProperty;
            _parent = parent;
            collection = new List<T>();
        }

        public BackReferenceCollection(string pointerProperty, IDataObject parent, IEnumerable<T> collection)
        {
            _pointerProperty = pointerProperty;
            _parent = parent;
            this.collection = new List<T>(collection);
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
            if (typeof(IEnumerable).IsAssignableFrom(item.GetPropertyType(_pointerProperty)))
            {
                item.AddToCollection<IDataObject>(_pointerProperty, _parent, true);
            }
            else
            {
                item.SetPropertyValue<IDataObject>(_pointerProperty, _parent);
                // TODO: Optimize in Generator
                // Sets the position Property for a 1:n Relation
                // eg. Method 1-n Parameter
                // Sets Parameter.Method__Position__
                if (item.HasProperty(_pointerProperty + Helper.PositionSuffix))
                {
                    item.SetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix, index);
                }
            }
        }

        private void ClearPointerProperty(T item)
        {
            if (typeof(IEnumerable).IsAssignableFrom(item.GetPropertyType(_pointerProperty)))
            {
                item.RemoveFromCollection<IDataObject>(_pointerProperty, _parent);
            }
            else
            {
                item.SetPropertyValue<IDataObject>(_pointerProperty, null);
                // TODO: Optimize in Generator
                // Clears the position Property for a 1:n Relation
                // eg. Method 1-n Parameter
                // Clears Parameter.Method__Position__
                if (item.HasProperty(_pointerProperty + Helper.PositionSuffix))
                {
                    item.SetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix, null);
                }
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
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new ArrayList(1) { newItem }));
        }

        protected virtual void OnItemRemoved(T removedItem)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new ArrayList(1) { removedItem }));
        }

        protected virtual void OnCollectionReset()
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        #endregion
    }
}
