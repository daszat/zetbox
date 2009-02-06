using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Kistl.API.Client
{
    public class NewListPropertyCollection<PARENT, T, COLLECTIONENTRYTYPE> : IList<T>, INotifyCollectionChanged
        where COLLECTIONENTRYTYPE : BaseClientCollectionEntry, INewCollectionEntry<PARENT, T>, INotifyPropertyChanged, new()
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

        public NewListPropertyCollection(PARENT parent, string propertyName)
        {
            this.parent = parent;
            collection = new NotifyingObservableCollection<COLLECTIONENTRYTYPE>(parent, propertyName);
            deletedCollection = new List<COLLECTIONENTRYTYPE>();
        }

        public void ApplyChanges(NewListPropertyCollection<PARENT, T, COLLECTIONENTRYTYPE> other)
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

            i.A = this.parent;
            i.B = item;
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
                    e.B = default(T);
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
            return collection.Count(i => i.B.Equals(item)) > 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.Select(i => i.B).ToArray().CopyTo(array, arrayIndex);
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
            COLLECTIONENTRYTYPE e = collection.SingleOrDefault(i => i.B.Equals(item));
            if (e == null) return false;

            bool result = collection.Remove(e);

            e.B = default(T);
            AddToDeletedCollection(e);
            return result;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return collection.Select(i => i.B).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return collection.Select(i => i.B).GetEnumerator();
        }

        #endregion

        #region IList<T> Members

        public int IndexOf(T item)
        {
            COLLECTIONENTRYTYPE e = collection.Single(i => i.B.Equals(item));
            return collection.IndexOf(e);
        }

        public void Insert(int index, T item)
        {
            COLLECTIONENTRYTYPE i = new COLLECTIONENTRYTYPE();
            if (parent.Context != null) i = (COLLECTIONENTRYTYPE)parent.Context.Attach(i);
            collection.Insert(index, i);
            i.A = this.parent;
            i.B = item;
        }

        public void RemoveAt(int index)
        {
            COLLECTIONENTRYTYPE item = collection[index];
            collection.RemoveAt(index);
            item.B = default(T);
            AddToDeletedCollection(item);
        }

        public T this[int index]
        {
            get
            {
                return collection[index].B;
            }
            set
            {
                collection[index].B = value;
            }
        }

        #endregion

        #region Streaming Methods
        public void ToStream(System.IO.BinaryWriter sw)
        {
            int index = 0;
            foreach (ICollectionEntry obj in collection)
            {
                if (obj is ICollectionEntrySorted)
                {

                    ICollectionEntrySorted se = (ICollectionEntrySorted)obj;
                    se.ValueIndex = index++;
                    if (se.ParentIndex == null)
                        se.ParentIndex = Kistl.API.Helper.LASTINDEXPOSITION;
                }

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
}
