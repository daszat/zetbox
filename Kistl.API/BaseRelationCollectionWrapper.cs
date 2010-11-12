
namespace Kistl.API
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A wrapper around a Collection of CollectionEntrys to present one "side" as normal collection.
    /// </summary>
    /// <typeparam name="TA">the type of the A side</typeparam>
    /// <typeparam name="TB">the type of the B side</typeparam>
    /// <typeparam name="TParent">which type contains the list, one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TItem">which type is contained in the list, the other one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TEntry">the wrapped CollectionEntry type</typeparam>
    /// <typeparam name="TBaseCollection">the provider's collection type</typeparam>
    public abstract class BaseRelationCollectionWrapper<TA, TB, TParent, TItem, TEntry, TBaseCollection>
        : ICollection<TItem>, ICollection, IEnumerable
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TParent : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        protected TBaseCollection Collection { get; private set; }
        protected TParent ParentObject { get; private set; }

        protected BaseRelationCollectionWrapper(TParent parentObject, TBaseCollection baseCollection)
        {
            Collection = baseCollection;
            ParentObject = parentObject;
        }

        #region provider- and end-specific extension points

        /// <summary>
        /// returns all items of this collection without a specific order
        /// </summary>
        protected abstract IEnumerable<TItem> GetItems();

        /// <summary>
        /// returns all entries of this collection in the "right" order.
        /// Inheritors may override this to implement ordering semantics, 
        /// since by default, it uses the "natural" ordering of <see cref="Collection"/>.
        /// </summary>
        protected virtual IEnumerable<TEntry> GetSortedEntries()
        {
            return Collection;
        }

        /// <summary>
        /// maps an item to an entry, will return a default value if the item is not associated to an entry of this collection
        /// </summary>
        protected virtual TEntry GetEntryOrDefault(TItem item)
        {
            var result = Collection.SingleOrDefault(e => Object.Equals(ItemFromEntry(e), item));
            if (result != null && ParentObject.Context != null)
            {
                result.AttachToContext(ParentObject.Context);
            }
            return result;
        }

        /// <summary>
        /// Returns the item referenced by a given entry
        /// </summary>
        protected abstract TItem ItemFromEntry(TEntry entry);

        /// <summary>
        /// Creates a new entry
        /// </summary>
        /// <returns></returns>
        protected abstract TEntry CreateEntry(object item);

        /// <summary>
        /// Initialises an entry for the given item
        /// </summary>
        protected abstract TEntry InitialiseEntry(TEntry entry, TItem item);

        /// <summary>
        /// called before an entry is added to the list
        /// </summary>
        /// <param name="entry">the new entry</param>
        protected virtual void OnEntryAdding(TEntry entry)
        {
            entry.AttachToContext(ParentObject.Context);
        }

        /// <summary>
        /// called after an entry is added to the list
        /// </summary>
        /// <param name="entry">the new entry</param>
        protected virtual void OnEntryAdded(TEntry entry) { }

        /// <summary>
        /// called before an entry is removed from the list
        /// </summary>
        /// <param name="entry">the removed entry</param>
        protected virtual void OnEntryRemoving(TEntry entry) { }

        /// <summary>
        /// called after an entry is removed from the list
        /// </summary>
        /// <param name="entry">the removed entry</param>
        protected virtual void OnEntryRemoved(TEntry entry) { }

        #endregion

        #region ICollection<TItem> Members

        public virtual void Add(TItem item)
        {
            TEntry entry = InitialiseEntry(CreateEntry(item), item);
            OnEntryAdding(entry);
            Collection.Add(entry);
            OnEntryAdded(entry);
        }

        public virtual void Clear()
        {
            // need a clone here to avoid collection modification while iterating
            var entries = Collection.ToList();
            foreach (TEntry entry in entries)
            {
                OnEntryRemoving(entry);
            }
            Collection.Clear();
            foreach (TEntry entry in entries)
            {
                OnEntryRemoved(entry);
            }
        }

        public bool Contains(TItem item)
        {
            return GetItems().Contains(item);
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            if (array == null) { throw new ArgumentNullException("array"); }
            if (!array.GetType().GetElementType().IsAssignableFrom(typeof(TItem)))
            {
                var msg = String.Format("Mismatch between source and destination type: [{0}] not assignable from [{1}]", array.GetType().GetElementType(), typeof(TItem));
                throw new ArgumentException(msg, "array");
            }

            foreach (var e in GetSortedEntries())
            {
                array[arrayIndex++] = ItemFromEntry(e);
            }
        }

        public int Count
        {
            get { return Collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return Collection.IsReadOnly; }
        }

        public virtual bool Remove(TItem item)
        {
            TEntry e = GetEntryOrDefault(item);
            if (e != null)
            {
                OnEntryRemoving(e);
                Collection.Remove(e);
                OnEntryRemoved(e);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable<TItem> Members

        public IEnumerator<TItem> GetEnumerator()
        {
            return GetSortedEntries().Select(e => ItemFromEntry(e)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetSortedEntries().Select(e => ItemFromEntry(e)).GetEnumerator();
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null) { throw new ArgumentNullException("array"); }
            if (!array.GetType().GetElementType().IsAssignableFrom(typeof(TItem)))
            {
                var msg = String.Format("Mismatch between source and destination type: [{0}] not assignable from [{1}]", array.GetType().GetElementType(), typeof(TItem));
                throw new ArgumentException(msg, "array");
            }

            var items = GetSortedEntries().Select(e => ItemFromEntry(e)).ToList();
            ((ICollection)items).CopyTo(array, index);
        }

        public bool IsSynchronized { get { return false; } }

        public object SyncRoot { get { return Collection; } }

        #endregion
    }

    /// <summary>
    /// A wrapper around a List of CollectionEntrys to present one "side" as normal list.
    /// </summary>
    /// <typeparam name="TA">the type of the A side</typeparam>
    /// <typeparam name="TB">the type of the B side</typeparam>
    /// <typeparam name="TParent">which type contains the list, one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TItem">which type is contained in the list, the other one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TEntry">the wrapped CollectionEntry type</typeparam>
    /// <typeparam name="TBaseCollection">the provider's collection type</typeparam>
    public abstract class BaseRelationListWrapper<TA, TB, TParent, TItem, TEntry, TBaseCollection>
        : BaseRelationCollectionWrapper<TA, TB, TParent, TItem, TEntry, TBaseCollection>, IList<TItem>, IList
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TParent : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        protected BaseRelationListWrapper(TParent parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        #region provider- and end-specific extension points

        /// <summary>
        /// Returns the index of a given entry
        /// </summary>
        protected abstract int? IndexFromEntry(TEntry entry);

        /// <summary>
        /// Sets the index on a given entry
        /// </summary>
        protected abstract void SetIndex(TEntry entry, int idx);

        /// <summary>
        /// Sets the item on a given entry
        /// </summary>
        protected abstract void SetItem(TEntry entry, TItem item);

        #endregion

        #region Index Management

        protected TEntry GetAt(int index)
        {
            if (index < 0 || index >= Collection.Count) { return null; }

            var list = GetSortedEntries().ToList();
            return list[index];
        }

        #endregion

        #region IList<TItem> Members

        public int IndexOf(TItem item)
        {
            TEntry entry = GetEntryOrDefault(item);
            if (entry == null)
                return -1;

            var list = GetSortedEntries().ToList();
            return list.IndexOf(entry);
        }

        public void Insert(int index, TItem item)
        {
            if (index < 0 || index > Collection.Count)
                throw new ArgumentOutOfRangeException("index", "is not a valid index");

            if (IsReadOnly)
                throw new NotSupportedException("List is ReadOnly");

            TEntry newEntry = InitialiseEntry(CreateEntry(item), item);

            var list = GetSortedEntries().ToList();
            // some providers may automatically insert
            // entries on initialisation. To avoid double entries
            // and wrong order, remove this first.
            list.Remove(newEntry);
            list.Insert(index, newEntry);

            Kistl.API.Helper.FixIndices(list, IndexFromEntry, SetIndex);

            OnEntryAdding(newEntry);
            Collection.Add(newEntry);
            OnEntryAdded(newEntry);
        }

        public void RemoveAt(int index)
        {
            if (0 < index || index >= Collection.Count)
                throw new ArgumentOutOfRangeException("index", "is not a valid index");

            if (IsReadOnly)
                throw new NotSupportedException("List is ReadOnly");

            TEntry oldEntry = GetAt(index);
            base.Remove(ItemFromEntry(oldEntry));
        }

        public TItem this[int index]
        {
            get
            {
                TEntry entry = GetAt(index);
                if (entry == null) throw new ArgumentOutOfRangeException("index", String.Format("Index {0} not found in collection", index));
                return ItemFromEntry(entry);
            }
            set
            {
                TEntry entry = GetAt(index);
                if (entry == null) throw new ArgumentOutOfRangeException("index", String.Format("Index {0} not found in collection", index));

                if (!Object.Equals(ItemFromEntry(entry), value))
                {
                    SetItem(entry, value);
                }
            }
        }

        #endregion

        #region IList Members

        int IList.Add(object value)
        {
            this.Add((TItem)value);
            return this.Count - 1;
        }

        bool IList.Contains(object value)
        {
            if (value is TItem)
                return this.Contains((TItem)value);
            else
                return false;
        }

        int IList.IndexOf(object value)
        {
            if (value is TItem)
                return this.IndexOf((TItem)value);
            else
                return -1;
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (TItem)value);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            if (value is TItem)
                this.Remove((TItem)value);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (TItem)value;
            }
        }

        #endregion

    }
}
