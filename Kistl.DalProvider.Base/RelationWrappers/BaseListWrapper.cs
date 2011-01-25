
namespace Kistl.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    /// <summary>
    /// A wrapper around a List of CollectionEntrys to present one "side" as normal list.
    /// </summary>
    /// <typeparam name="TA">the type of the A side</typeparam>
    /// <typeparam name="TB">the type of the B side</typeparam>
    /// <typeparam name="TParent">which type contains the list, one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TItem">which type is contained in the list, the other one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TEntry">the wrapped CollectionEntry type</typeparam>
    /// <typeparam name="TBaseCollection">the provider's collection type</typeparam>
    public abstract class BaseListWrapper<TA, TB, TParent, TItem, TEntry, TBaseCollection>
        : BaseCollectionWrapper<TA, TB, TParent, TItem, TEntry, TBaseCollection>, IList<TItem>, IList
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TParent : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        protected BaseListWrapper(TParent parentObject, TBaseCollection baseCollection)
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
            if (index < 0 || index >= Collection.Count)
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
