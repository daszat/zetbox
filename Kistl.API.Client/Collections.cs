using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BackReferenceCollection<T> : List<T>
    {
        public BackReferenceCollection()
        {
        }

        public BackReferenceCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }
    }

    public class ListPropertyCollection<T, PARENT, COLLECTIONENTRYTYPE> : IList<T>, INotifyCollectionChanged
        where COLLECTIONENTRYTYPE : class, ICollectionEntry<T, PARENT>, INotifyPropertyChanged, new()
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

        public void CopyTo(ListPropertyCollection<T, PARENT, COLLECTIONENTRYTYPE> other)
        {
            other.collection.BeginUpdate();
            try
            {
                other.ResetInternal();
                foreach (COLLECTIONENTRYTYPE e in collection)
                {
                    COLLECTIONENTRYTYPE n = new COLLECTIONENTRYTYPE();
                    e.CopyTo(n);

                    if (other.parent.Context != null) 
                        n = (COLLECTIONENTRYTYPE)other.parent.Context.Attach(n);
                    other.collection.Add(n);
                }
            }
            finally
            {
                other.collection.EndUpdate();
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
            COLLECTIONENTRYTYPE i = new COLLECTIONENTRYTYPE() { Value = item, Parent = this.parent };
            if (parent.Context != null) parent.Context.Attach(i);
            collection.Add(i);
        }

        /// <summary>
        /// TODO: Das macht mich noch nicht glücklich!
        /// </summary>
        public void Clear()
        {
            collection.BeginUpdate();
            try
            {
                collection.ForEach(e => AddToDeletedCollection(e));
                collection.Clear();
            }
            finally
            {
                collection.EndUpdate();
                collection.NotifyParent();
            }
        }

        private void ResetInternal()
        {
            collection.BeginUpdate();
            try
            {
                if (parent.Context != null)
                {
                    collection.ForEach(i => parent.Context.Detach(i));
                    deletedCollection.ForEach(i => parent.Context.Detach(i));
                }
                collection.Clear();
                deletedCollection.Clear();
            }
            finally
            {
                collection.EndUpdate();
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
            COLLECTIONENTRYTYPE e = collection.Single(i => i.Value.Equals(item));
            AddToDeletedCollection(e);
            return collection.Remove(e);
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
            COLLECTIONENTRYTYPE i = new COLLECTIONENTRYTYPE() { Value = item, Parent = this.parent };
            if (parent.Context != null)
                i = (COLLECTIONENTRYTYPE)parent.Context.Attach(i);
            collection.Insert(index, i);
        }

        public void RemoveAt(int index)
        {
            AddToDeletedCollection(collection[index]);
            collection.RemoveAt(index);
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
                ResetInternal();

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
}
