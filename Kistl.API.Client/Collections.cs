using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    #region BackReferenceCollection
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BackReferenceCollection<T> : IList<T>
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

        private void AddToList(T item)
        {
            if (item == null) throw new ArgumentNullException("item", "Cannot add a NULL Object to this collection");
            SetPointerProperty(item);
        }

        private void RemoveFromList(T item)
        {
            if (item == null) throw new ArgumentNullException("item", "Cannot remove a NULL Object to this collection");
            ClearPointerProperty(item);
        }

        private void SetPointerProperty(T item)
        {
            if (typeof(IEnumerable).IsAssignableFrom(item.GetPropertyType(_pointerProperty)))
            {
                item.AddToCollection<IDataObject>(_pointerProperty, _parent, true);
            }
            else
            {
                item.SetPropertyValue<IDataObject>(_pointerProperty, _parent);
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
            }
        }

        public void ApplyChanges(BackReferenceCollection<T> list)
        {
            if (list == null) return;
            list.collection = new List<T>(this.collection);
        }

        public void AttachToContext(IKistlContext ctx)
        {
            collection = new List<T>(collection.Select(i => ctx.Attach(i)).Cast<T>());
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return collection.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            collection.Insert(index, item);
            AddToList(item);
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
                AddToList(value);
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            collection.Add(item);
            AddToList(item);
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

    }
    #endregion

    #region ListPropertyCollection
    public class ListPropertyCollection<T, PARENT, COLLECTIONENTRYTYPE> : IList<T>, INotifyCollectionChanged
        where COLLECTIONENTRYTYPE : BaseClientCollectionEntry, ICollectionEntry<T, PARENT>, INotifyPropertyChanged, new()
        where PARENT : IDataObject
    {
        private PARENT parent;

        private NotifyingObservableCollection<COLLECTIONENTRYTYPE> collection;
        public NotifyingObservableCollection<COLLECTIONENTRYTYPE> UnderlyingCollection
        {
            get { return collection; }
        }

        private List<COLLECTIONENTRYTYPE> deletedCollection;
        public List<COLLECTIONENTRYTYPE> DeletedCollection
        {
            get { return deletedCollection; }
        }

        private void AddToDeletedCollection(COLLECTIONENTRYTYPE e)
        {
            // TODO: Call Helper.IsFloatingObject will also check, if Object is detached.
            // But thats not the point here. We are only interested, if the Object should be send
            // to the Server to be deleted.
            if (e.ID <= Helper.INVALIDID) return;
            if (deletedCollection.FirstOrDefault<COLLECTIONENTRYTYPE>(i => i.ID == e.ID) == null)
            {
                if (parent.Context != null)
                {
                    parent.Context.Delete(e);
                }
                else
                {
                    e.ObjectState = DataObjectState.Deleted;
                }
                deletedCollection.Add(e);
            }
        }

        public ListPropertyCollection(PARENT parent, string propertyName)
        {
            this.parent = parent;
            collection = new NotifyingObservableCollection<COLLECTIONENTRYTYPE>(parent, propertyName);
            deletedCollection = new List<COLLECTIONENTRYTYPE>();
        }

        public void ApplyChanges(ListPropertyCollection<T, PARENT, COLLECTIONENTRYTYPE> other)
        {
            other.collection.BeginUpdate();
            try
            {
                // Reset Collection
                if (other.parent.Context == null) throw new InvalidOperationException("ApplyChanges works only for attached Collections");

                other.collection.ForEach(i => other.parent.Context.Detach(i));
                other.deletedCollection.ForEach(i => other.parent.Context.Detach(i));
                other.collection.Clear();
                other.deletedCollection.Clear();

                foreach (COLLECTIONENTRYTYPE e in collection)
                {
                    COLLECTIONENTRYTYPE n = new COLLECTIONENTRYTYPE();
                    ((BaseClientCollectionEntry)e).ApplyChanges(n);
                    other.collection.Add(n);
                }

                other.parent.NotifyPropertyChanged(other.collection.PropertyName);
            }
            finally
            {
                other.collection.EndUpdate();
            }
        }

        public void AttachToContext(IKistlContext ctx)
        {
            collection.BeginUpdate();
            try
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    collection[i] = (COLLECTIONENTRYTYPE)ctx.Attach(collection[i]);
                }
            }
            finally
            {
                collection.EndUpdate();
            }
        }

        #region INotifyCollectionChanged Member

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { UnderlyingCollection.CollectionChanged += value; }
            remove { UnderlyingCollection.CollectionChanged -= value; }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            COLLECTIONENTRYTYPE i = new COLLECTIONENTRYTYPE();
            if (parent.Context != null) parent.Context.Attach(i);
            collection.Add(i);

            i.Parent = this.parent;
            i.Value = item;
        }

        public void Clear()
        {
            collection.BeginUpdate();
            try
            {
                var collectionTmp = collection.ToArray();
                collection.Clear();
                foreach (COLLECTIONENTRYTYPE e in collectionTmp)
                {
                    e.Value = default(T);
                    AddToDeletedCollection(e);
                }
            }
            finally
            {
                collection.EndUpdate();
                collection.NotifyParent();
            }
        }

        public bool Contains(T item)
        {
            return collection.Count(i => i.Value.Equals(item)) > 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
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
            COLLECTIONENTRYTYPE e = collection.SingleOrDefault(i => i.Value.Equals(item));
            if (e == null) return false;

            bool result = collection.Remove(e);

            e.Value = default(T);
            AddToDeletedCollection(e);
            return result;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return collection.Select(i => i.Value).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return collection.Select(i => i.Value).GetEnumerator();
        }

        #endregion

        #region IList<T> Members

        public int IndexOf(T item)
        {
            COLLECTIONENTRYTYPE e = collection.Single(i => i.Value.Equals(item));
            return collection.IndexOf(e);
        }

        public void Insert(int index, T item)
        {
            COLLECTIONENTRYTYPE i = new COLLECTIONENTRYTYPE();
            if (parent.Context != null) i = (COLLECTIONENTRYTYPE)parent.Context.Attach(i);
            collection.Insert(index, i);
            i.Parent = this.parent;
            i.Value = item;
        }

        public void RemoveAt(int index)
        {
            COLLECTIONENTRYTYPE item = collection[index];
            collection.RemoveAt(index);
            item.Value = default(T);
            AddToDeletedCollection(item);
        }

        public T this[int index]
        {
            get
            {
                return collection[index].Value;
            }
            set
            {
                collection[index].Value = value;
            }
        }

        #endregion

        #region Streaming Methods
        public void ToStream(System.IO.BinaryWriter sw)
        {
            foreach (ICollectionEntry obj in collection)
            {
                BinarySerializer.ToBinary(true, sw);
                obj.ToStream(sw);
            }
            foreach (ICollectionEntry obj in deletedCollection)
            {
                BinarySerializer.ToBinary(true, sw);
                obj.ToStream(sw);
            }

            BinarySerializer.ToBinary(false, sw);
        }

        public void FromStream(System.IO.BinaryReader sr)
        {
            collection.BeginUpdate();
            try
            {
                while (sr.ReadBoolean())
                {
                    COLLECTIONENTRYTYPE obj = new COLLECTIONENTRYTYPE();
                    obj.FromStream(sr);
                    collection.Add(obj);
                }

                // deletedCollection is not needed to be deserialized
                // Server wouldn't send deleted Objects
            }
            finally
            {
                collection.EndUpdate();
            }
        }
        #endregion

    }
    #endregion
}
