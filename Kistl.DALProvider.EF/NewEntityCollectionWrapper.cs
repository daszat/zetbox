using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.DALProvider.EF
{

    /// <summary>
    /// A wrapper around a EntityCollection to present one "side" as normal collection.
    /// </summary>
    /// <typeparam name="ATYPE">the type of the A side</typeparam>
    /// <typeparam name="BTYPE">the type of the B side</typeparam>
    /// <typeparam name="PARENTTYPE">which type contains the list, one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="ITEMTYPE">which type is contained in the list, the other one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="ENTRYTYPE">the wrapped CollectionEntry type</typeparam>
    public abstract class EntityCollectionWrapper<ATYPE, BTYPE, PARENTTYPE, ITEMTYPE, ENTRYTYPE>
        : ICollection<ITEMTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where PARENTTYPE : class, IDataObject
        where ITEMTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewCollectionEntry<ATYPE, BTYPE>, new()
    {
        protected EntityCollection<ENTRYTYPE> Collection { get; private set; }
        protected PARENTTYPE ParentObject { get; private set; }

        public EntityCollectionWrapper(PARENTTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
        {
            Collection = ec;
            ParentObject = parentObject;
        }

        #region end-specific extension points

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
        /// maps an item to an entry
        /// </summary>
        protected abstract ENTRYTYPE GetEntryOrDefault(ITEMTYPE item);

        /// <summary>
        /// creates a new entry for the given item
        /// </summary>
        protected abstract ENTRYTYPE CreateEntry(ITEMTYPE item);

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

        #endregion

        /// <summary>
        /// removes the given entry from the data store
        /// </summary>
        protected void Clear(ENTRYTYPE entry)
        {
            entry.A = null;
            entry.B = null;
            // Case: 668
            entry.GetEFContext().DeleteObject(entry);
        }

        #region ICollection<VALUE> Members

        public virtual void Add(ITEMTYPE item)
        {
            ENTRYTYPE entry = CreateEntry(item);
            Collection.Add(entry);
            // Case: 668
            entry.AttachToContext(ParentObject.Context);

            OnEntryAdded(entry);
        }

        public virtual void Clear()
        {
            // Must be cleared by hand
            // EF will drop the other side on the CollectionEntry - that's right and OK
            // But we want the CollectionEntry to be deleted completely in that case
            foreach (ENTRYTYPE entry in Collection.ToList())
            {
                OnEntryRemoving(entry);
                Clear(entry);
            }
            Collection.Clear();
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
                Clear(e);
                Collection.Remove(e);
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
    }

    /// <summary>
    /// List
    /// </summary>
    public abstract class EntityListWrapper<ATYPE, BTYPE, PARENTTYPE, ITEMTYPE, ENTRYTYPE>
        : EntityCollectionWrapper<ATYPE, BTYPE, PARENTTYPE, ITEMTYPE, ENTRYTYPE>, IList<ITEMTYPE>
        where ATYPE : class, IDataObject
        where BTYPE : class, IDataObject
        where PARENTTYPE : class, IDataObject
        where ITEMTYPE : class, IDataObject
        where ENTRYTYPE : BaseServerCollectionEntry_EntityFramework, IEntityWithRelationships, INewListEntry<ATYPE, BTYPE>, new()
    {
        public EntityListWrapper(PARENTTYPE parentObject, EntityCollection<ENTRYTYPE> ec)
            : base(parentObject, ec)
        {
        }

        #region end-specific extension points

        /// <summary>
        /// Returns the item referenced by a given entry
        /// </summary>
        protected abstract ITEMTYPE ItemFromEntry(ENTRYTYPE entry);

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
            foreach (ENTRYTYPE entry in Collection)
            {
                int? idxEntry = IndexFromEntry(entry);
                if (idxEntry.HasValue && idxEntry.Value == index)
                {
                    return entry;
                }
            }
            return null;
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

            ENTRYTYPE newEntry = CreateEntry(item);
            SetIndex(newEntry, index);
            Collection.Add(newEntry);
            newEntry.AttachToContext(ParentObject.Context);

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

            // TODO: Optimize
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
    }
}
