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
    public class OneNRelationCollection<T> : IList<T>, INotifyCollectionChanged
        where T : class, IDataObject
    {
        private string _propertyName;
        private string _posProperty;
        private IDataObject _parent;
        List<T> collection;

        public OneNRelationCollection(string propertyName, IDataObject parent)
            : this(propertyName, propertyName + Helper.PositionSuffix, parent)
        {

        }

        public OneNRelationCollection(string propertyName, IDataObject parent, IEnumerable<T> collection)
            : this(propertyName, propertyName + Helper.PositionSuffix, parent, collection)
        {

        }

        ///// <param name="fkProperty">the name of the fk_Property which does notification, but not collection fixing</param>
        public OneNRelationCollection(string fkProperty, string posProperty, IDataObject parent)
        {
            _propertyName = fkProperty;
            _posProperty = posProperty;
            _parent = parent;
            collection = new List<T>();
        }

        ///// <param name="fkProperty">the name of the fk_Property which does notification, but not collection fixing</param>
        public OneNRelationCollection(string fkProperty, string posProperty, IDataObject parent, IEnumerable<T> collection)
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
            int index = collection.IndexOf(item);
            collection.Remove(item);
            OnItemRemoved(item, index);
        }

        private void DoInsert(T item, int index)
        {
            if (item == null) throw new ArgumentNullException("item", "Cannot add a NULL Object to this collection");
            if (_parent.Context != item.Context) throw new WrongKistlContextException();
            collection.Insert(index, item);
            SetPointerProperty(item, index);
            OnItemAdded(item);
        }

        private void DoRemoveAt(T item, int index)
        {
            collection.RemoveAt(index);
            ClearPointerProperty(item);
            OnItemRemoved(item, index);
        }

        private bool DoRemove(T item)
        {
            if (item == null) throw new ArgumentNullException("item", "Cannot remove a NULL Object from this collection");

            int index = collection.IndexOf(item);
            if (index < 0)
                return false;
            DoRemoveAt(item, index);
            return true;
        }

        private void DoClear()
        {
            foreach (var item in collection)
            {
                ClearPointerProperty(item);
            }
            collection.Clear();
            OnCollectionReset();
        }

        private void SetPointerProperty(T item, int index)
        {
            if (item.GetPrivateFieldValue<int?>("_fk_" + _propertyName) != _parent.ID)
            {
                (item as BaseClientDataObject).UpdateParent(_propertyName, _parent.ID);
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
            if (item.GetPrivateFieldValue<int?>("_fk_" + _propertyName) != null)
            {
                (item as BaseClientDataObject).UpdateParent(_propertyName, null);
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

        public void ApplyChanges(OneNRelationCollection<T> list)
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
                if (Object.Equals(collection[index], value)) return;

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

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnItemAdded(T newItem)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem));
        }

        protected virtual void OnItemRemoved(T removedItem, int index)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));
        }

        protected virtual void OnCollectionReset()
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        #endregion
    }
}
