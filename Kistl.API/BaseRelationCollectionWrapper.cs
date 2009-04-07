using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{

    /// <summary>
    /// A wrapper around a Collection of CollectionEntrys to present one "side" as normal collection.
    /// </summary>
    /// <typeparam name="ATYPE">the type of the A side</typeparam>
    /// <typeparam name="BTYPE">the type of the B side</typeparam>
    /// <typeparam name="PARENTTYPE">which type contains the list, one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="ITEMTYPE">which type is contained in the list, the other one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="ENTRYTYPE">the wrapped CollectionEntry type</typeparam>
    /// <typeparam name="BASECOLLECTIONTYPE">the provider's collection type</typeparam>
    public abstract class BaseRelationCollectionWrapper<ATYPE, BTYPE, PARENTTYPE, ITEMTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : ICollection<ITEMTYPE>, ICollection, IEnumerable
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where PARENTTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationCollectionEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        protected BASECOLLECTIONTYPE Collection { get; private set; }
        protected PARENTTYPE ParentObject { get; private set; }

        public BaseRelationCollectionWrapper(PARENTTYPE parentObject, BASECOLLECTIONTYPE baseCollection)
        {
            Collection = baseCollection;
            ParentObject = parentObject;

            foreach (IPersistenceObject obj in Collection)
            {
                obj.AttachToContext(parentObject.Context);
            }
        }

        #region provider- and end-specific extension points

        /// <summary>
        /// returns all items of this collection without a specific order
        /// </summary>
        protected abstract IEnumerable<ITEMTYPE> GetItems();

        /// <summary>
        /// returns all items of this collection in the "right" order.
        /// Inheritors may override this to implement ordering semantics, 
        /// since by default, it uses the "natural" ordering of GetItems().
        /// </summary>
        protected virtual IEnumerable<ITEMTYPE> GetList()
        {
            return GetItems();
        }

        /// <summary>
        /// maps an item to an entry, will return a default value if the item is not associated to an entry of this collection
        /// </summary>
        protected virtual ENTRYTYPE GetEntryOrDefault(ITEMTYPE item)
        {
            return Collection.SingleOrDefault(e => Object.Equals(ItemFromEntry(e), item));
        }

        /// <summary>
        /// Returns the item referenced by a given entry
        /// </summary>
        protected abstract ITEMTYPE ItemFromEntry(ENTRYTYPE entry);

        /// <summary>
        /// Creates a new entry
        /// </summary>
        /// <returns></returns>
        protected abstract ENTRYTYPE CreateEntry(object item);

        /// <summary>
        /// Initialises an entry for the given item
        /// </summary>
        protected abstract ENTRYTYPE InitialiseEntry(ENTRYTYPE entry, ITEMTYPE item);

        /// <summary>
        /// called before an entry is added to the list
        /// </summary>
        /// <param name="entry">the new entry</param>
        protected virtual void OnEntryAdding(ENTRYTYPE entry)
        {
            entry.AttachToContext(ParentObject.Context);
        }

        /// <summary>
        /// called after an entry is added to the list
        /// </summary>
        /// <param name="entry">the new entry</param>
        protected virtual void OnEntryAdded(ENTRYTYPE entry) { }

        /// <summary>
        /// called before an entry is removed from the list
        /// </summary>
        /// <param name="entry">the removed entry</param>
        protected virtual void OnEntryRemoving(ENTRYTYPE entry) { }

        /// <summary>
        /// called after an entry is removed from the list
        /// </summary>
        /// <param name="entry">the removed entry</param>
        protected virtual void OnEntryRemoved(ENTRYTYPE entry) { }

        #endregion

        #region ICollection<VALUE> Members

        public virtual void Add(ITEMTYPE item)
        {
            ENTRYTYPE entry = InitialiseEntry(CreateEntry(item), item);
            OnEntryAdding(entry);
            Collection.Add(entry);
            OnEntryAdded(entry);
        }

        public virtual void Clear()
        {
            // need a clone here
            var entries = Collection.ToList();
            foreach (ENTRYTYPE entry in entries)
            {
                OnEntryRemoving(entry);
            }
            Collection.Clear();
            foreach (ENTRYTYPE entry in entries)
            {
                OnEntryRemoved(entry);
            }
        }

        public bool Contains(ITEMTYPE item)
        {
            return GetItems().Contains(item);
        }

        public void CopyTo(ITEMTYPE[] array, int arrayIndex)
        {
            foreach (var i in GetItems())
            {
                array[arrayIndex++] = i;
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

        public virtual bool Remove(ITEMTYPE item)
        {
            ENTRYTYPE e = GetEntryOrDefault(item);
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

        #region IEnumerable<ITEMTYPE> Members

        public IEnumerator<ITEMTYPE> GetEnumerator()
        {
            return GetList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetList().GetEnumerator();
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            foreach (var i in GetItems())
            {
                array.SetValue(i, index++);
            }
        }

        public bool IsSynchronized { get { return false; } }

        public object SyncRoot { get { return Collection; } }

        #endregion
    }

    /// <summary>
    /// A wrapper around a List of CollectionEntrys to present one "side" as normal list.
    /// </summary>
    /// <typeparam name="ATYPE">the type of the A side</typeparam>
    /// <typeparam name="BTYPE">the type of the B side</typeparam>
    /// <typeparam name="PARENTTYPE">which type contains the list, one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="ITEMTYPE">which type is contained in the list, the other one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="ENTRYTYPE">the wrapped CollectionEntry type</typeparam>
    /// <typeparam name="BASECOLLECTIONTYPE">the provider's collection type</typeparam>
    public abstract class BaseRelationListWrapper<ATYPE, BTYPE, PARENTTYPE, ITEMTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>
        : BaseRelationCollectionWrapper<ATYPE, BTYPE, PARENTTYPE, ITEMTYPE, ENTRYTYPE, BASECOLLECTIONTYPE>, IList<ITEMTYPE>, IList
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where PARENTTYPE : class, IDataObject
        where ENTRYTYPE : class, IRelationListEntry<ATYPE, BTYPE>
        where BASECOLLECTIONTYPE : class, ICollection<ENTRYTYPE>
    {
        public BaseRelationListWrapper(PARENTTYPE parentObject, BASECOLLECTIONTYPE baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        #region provider- and end-specific extension points

        /// <summary>
        /// Returns the index of a given entry
        /// </summary>
        protected abstract int? IndexFromEntry(ENTRYTYPE entry);

        /// <summary>
        /// Sets the index on a given entry
        /// </summary>
        protected abstract void SetIndex(ENTRYTYPE entry, int idx);

        /// <summary>
        /// Sets the item on a given entry
        /// </summary>
        protected abstract void SetItem(ENTRYTYPE entry, ITEMTYPE item);

        #endregion

        #region Index Management

        protected ENTRYTYPE GetAt(int index)
        {
            RepairIndexes();
            return Collection.SingleOrDefault(e => { var idx = IndexFromEntry(e); return idx.HasValue && idx.Value == index; });
        }

        /// <summary>
        /// Repairs all index values on the entries. This is currently needed sometimes, 
        /// because entries could have been added/modified/removed by the "other" side, without us knowing.
        /// Then the IndexFromEntry() is set to LASTINDEXPOSITION or holes exist which confuse the rest.
        /// </summary>
        protected void RepairIndexes()
        {
            int i = 0;
            foreach (var entry in Collection.OrderBy(e => IndexFromEntry(e) ?? Kistl.API.Helper.LASTINDEXPOSITION))
            {
                SetIndex(entry, i++);
            }
        }

        #endregion

        #region IList<ITEMTYPE> Members

        public int IndexOf(ITEMTYPE item)
        {
            ENTRYTYPE entry = GetEntryOrDefault(item);
            if (entry == null)
                return -1;

            int? result = IndexFromEntry(entry);
            if (result == null || result.Value == Kistl.API.Helper.LASTINDEXPOSITION)
                throw new InvalidOperationException("Collection is not sorted");

            return result.Value;
        }

        public void Insert(int index, ITEMTYPE item)
        {
            // allow index==-1 and index==Collection.Count for prepending or appending to list
            if (-1 < index || index > Collection.Count)
                throw new ArgumentOutOfRangeException("index", "is not a valid index");

            if (IsReadOnly)
                throw new NotSupportedException("List is ReadOnly");

            // TODO: Optimize
            foreach (ENTRYTYPE entry in Collection)
            {
                int idx = IndexFromEntry(entry) ?? Kistl.API.Helper.LASTINDEXPOSITION;
                if (idx >= index)
                {
                    SetIndex(entry, idx + 1);
                }
            }

            ENTRYTYPE newEntry = InitialiseEntry(CreateEntry(item), item);
            SetIndex(newEntry, index);

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

            ENTRYTYPE oldEntry = GetAt(index);
            base.Remove(ItemFromEntry(oldEntry));

            // not really needed when removing items.
            // TODO: Optimize, check whether other parts can live with holes 
            // in the Position; when inserting exploit holes to shortcut 
            // Position updating
            foreach (ENTRYTYPE entry in Collection)
            {
                int idx = IndexFromEntry(entry) ?? Kistl.API.Helper.LASTINDEXPOSITION;
                if (idx >= index)
                {
                    SetIndex(entry, idx - 1);
                }
            }
        }

        public ITEMTYPE this[int index]
        {
            get
            {
                ENTRYTYPE entry = GetAt(index);
                if (entry == null) throw new ArgumentOutOfRangeException(string.Format("Index {0} not found in collection", index));
                return ItemFromEntry(entry);
            }
            set
            {
                ENTRYTYPE entry = GetAt(index);
                if (entry == null) throw new ArgumentOutOfRangeException(string.Format("Index {0} not found in collection", index));

                if (!Object.Equals(ItemFromEntry(entry), value))
                {
                    SetItem(entry, value);
                }
            }
        }

        #endregion

        #region IList Members

        public int Add(object value)
        {
            return this.Add((ITEMTYPE)value);
        }

        public bool Contains(object value)
        {
            if (value is ITEMTYPE)
                return this.Contains((ITEMTYPE)value);
            else
                return false;
        }

        public int IndexOf(object value)
        {
            if (value is ITEMTYPE)
                return this.IndexOf((ITEMTYPE)value);
            else
                return -1;
        }

        public void Insert(int index, object value)
        {
            this.Insert(index, (ITEMTYPE)value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public void Remove(object value)
        {
            if (value is ITEMTYPE)
                this.Remove((ITEMTYPE)value);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (ITEMTYPE)value;
            }
        }

        #endregion

    }
}
