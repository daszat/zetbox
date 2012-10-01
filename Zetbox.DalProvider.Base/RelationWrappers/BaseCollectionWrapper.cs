// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    /// <summary>
    /// A wrapper around a Collection of CollectionEntrys to present one "side" as normal collection.
    /// </summary>
    /// <typeparam name="TA">the type of the A side</typeparam>
    /// <typeparam name="TB">the type of the B side</typeparam>
    /// <typeparam name="TParent">which type contains the list, one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TItem">which type is contained in the list, the other one of ATYPE or BTYPE</typeparam>
    /// <typeparam name="TEntry">the wrapped CollectionEntry type</typeparam>
    /// <typeparam name="TBaseCollection">the provider's collection type</typeparam>
    public abstract class BaseCollectionWrapper<TA, TB, TParent, TItem, TEntry, TBaseCollection>
        : ICollection<TItem>, IRelationListSync<TEntry>, ICollection, IEnumerable
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TParent : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : ICollection<TEntry>
    {
        protected TBaseCollection Collection { get; private set; }
        protected TParent ParentObject { get; private set; }

        protected BaseCollectionWrapper(TParent parentObject, TBaseCollection baseCollection)
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
            var result = Collection.FirstOrDefault(e => Object.Equals(ItemFromEntry(e), item));
            if (result != null && ParentObject.Context != null)
            {
                // TODO: Revomve this
                result.AttachToContext(ParentObject.Context, null);
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
        protected virtual TEntry CreateEntry(object item)
        {
            return (TEntry)ParentObject.Context.Internals().CreateRelationCollectionEntry(ParentObject.Context.GetImplementationType(typeof(TEntry)).ToInterfaceType());
        }

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
            // TODO: Remove this
            entry.AttachToContext(ParentObject.Context, null);
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
        protected virtual void OnEntryRemoved(TEntry entry)
        {
            ParentObject.Context.Delete(entry);
        }

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

        #region IRelationListSync<TEntry> Members

        public virtual void AddWithoutSetParent(TEntry item)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveWithoutClearParent(TEntry item)
        {
            throw new NotImplementedException();
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
}
