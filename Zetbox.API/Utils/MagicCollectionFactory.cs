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

namespace Zetbox.API.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;

    public static class MagicCollectionFactory
    {
        public static ICollection<TResult> WrapAsCollectionHelper<TFrom, TResult>(ICollection<TFrom> collection)
            where TFrom : TResult
        {
            if (collection == null) { throw new ArgumentNullException("collection"); }

            return new GenericCastingCollectionWrapper<TFrom, TResult>(collection);
        }

        public static ICollection<TResult> WrapListAsCollectionHelper<TFrom, TResult>(IList<TFrom> list)
            where TFrom : TResult
        {
            if (list == null) { throw new ArgumentNullException("list"); }

            return new GenericCastingListWrapper<TFrom, TResult>(list);
        }

        public static object WrapAsCollectionReflectionHelper(Type TFrom, Type TResult, object collection)
        {
            if (collection == null) { throw new ArgumentNullException("collection"); }

            MethodInfo wrapAsCollection = typeof(MagicCollectionFactory).GetMethod("WrapAsCollectionHelper");
            return wrapAsCollection.MakeGenericMethod(TFrom, TResult).Invoke(null, new[] { collection });
        }

        public static object WrapListAsCollectionReflectionHelper(Type TFrom, Type TResult, object list)
        {
            if (list == null) { throw new ArgumentNullException("list"); }

            MethodInfo wrapListAsCollection = typeof(MagicCollectionFactory).GetMethod("WrapListAsCollectionHelper");
            return wrapListAsCollection.MakeGenericMethod(TFrom, TResult).Invoke(null, new[] { list });
        }

        /// <summary>
        /// Wrap "arbitrary" list-like objects into an ICollection&lt;T&gt;. Currently works with ICollection&lt;T&gt;s and ILists
        /// </summary>
        public static ICollection<T> WrapAsCollection<T>(object collection)
        {
            if (collection == null) { throw new ArgumentNullException("collection"); }

            var collectionType = collection.GetType();
            var implementedInterfaces = collectionType.GetInterfaces();

            if (implementedInterfaces.Contains(typeof(ICollection<T>)))
            {
                return (ICollection<T>)collection;
            }
            else if (implementedInterfaces.Contains(typeof(IList<T>)))
            {
                return (IList<T>)collection;
            }
            else if (implementedInterfaces.Contains(typeof(IEnumerable<T>)))
            {
                // TODO: implement a non-synchronized version for here
                return new List<T>((IEnumerable<T>)collection);
            }
            else
            {
                var elementTypes = collection.GetType().FindElementTypes().Where(t => typeof(T).IsAssignableFrom(t)).ToArray();

                if (elementTypes.Contains(typeof(T)))
                {
                    elementTypes = elementTypes.Where(t => t != typeof(T)).ToArray();
                }

                if (elementTypes.Count() > 1)
                {
                    throw new AmbiguousMatchException("Ambiguous Element Types found");
                }
                else if (elementTypes.Count() == 1)
                {
                    var elementType = elementTypes.Single();
                    var sourceListType = typeof(IList<>).MakeGenericType(elementType);
                    if (implementedInterfaces.Contains(sourceListType))
                    {
                        return (ICollection<T>)WrapListAsCollectionReflectionHelper(elementType, typeof(T), collection);
                    }
                    else
                    {
                        var sourceCollectionType = typeof(ICollection<>).MakeGenericType(elementType);
                        if (implementedInterfaces.Contains(sourceCollectionType))
                        {
                            return (ICollection<T>)WrapAsCollectionReflectionHelper(elementType, typeof(T), collection);
                        }
                    }
                }
                else if (implementedInterfaces.Contains(typeof(IList)))
                {
                    return new CastingListWrapper<T>((IList)collection);
                }
            }

            throw new ArgumentException(String.Format("Unable to determine CollectionWrapper for {0}", collectionType.FullName), "collection");
        }

        public static IList<TResult> WrapAsListHelper<TFrom, TResult>(IList<TFrom> list)
            where TFrom : TResult
        {
            if (list == null) { throw new ArgumentNullException("list"); }

            return new GenericCastingListWrapper<TFrom, TResult>(list);
        }

        public static object WrapAsListReflectionHelper(Type TFrom, Type TResult, object list)
        {
            if (TFrom == null) { throw new ArgumentNullException("TFrom"); }
            if (TResult == null) { throw new ArgumentNullException("TResult"); }
            if (list == null) { throw new ArgumentNullException("list"); }

            MethodInfo wrapAsList = typeof(MagicCollectionFactory).GetMethod("WrapAsListHelper");
            return wrapAsList.MakeGenericMethod(TFrom, TResult).Invoke(null, new[] { list });
        }


        /// <summary>
        /// Wrap a list-like objects into an IList&lt;T&gt;. Currently works with IList&lt;T&gt;s, ILists and ICollection&lt;T&gt;s
        /// </summary>
        public static IList<T> WrapAsList<T>(object potentialList)
        {
            if (potentialList == null) { throw new ArgumentNullException("potentialList"); }

            var listType = potentialList.GetType();
            var implementedInterfaces = listType.GetInterfaces();

            if (implementedInterfaces.Contains(typeof(IList<T>)))
            {
                return (IList<T>)potentialList;
            }
            else if (implementedInterfaces.Contains(typeof(IList)))
            {
                var elementTypes = listType.FindElementTypes().Where(t => typeof(T).IsAssignableFrom(t)).ToArray();

                if (elementTypes.Contains(typeof(T)))
                {
                    elementTypes = elementTypes.Where(t => t != typeof(T)).ToArray();
                }

                if (elementTypes.Count() > 1)
                {
                    throw new AmbiguousMatchException("Ambiguous Element Types found");
                }
                else if (elementTypes.Count() == 1)
                {
                    return (IList<T>)WrapAsListReflectionHelper(elementTypes.Single(), typeof(T), potentialList);
                }
                else
                {
                    return new CastingListWrapper<T>((IList)potentialList);
                }
            }

            if (typeof(T).GetInterfaces().Contains(typeof(IPersistenceObject)))
            {
                try
                {
                    var potentialCollection = WrapAsCollection<T>(potentialList);
                    var sortedList = new SortListFromCollection<int, T>(item => ((IPersistenceObject)item).ID, potentialCollection);
                    return sortedList;
                }
                catch (ArgumentException)
                {
                    // also failed to wrap as collection, ignore this exception and fall through to the final exception
                }
            }

            if (typeof(T).GetInterfaces().Contains(typeof(ISortKey<int>)))
            {
                try
                {
                    var potentialCollection = WrapAsCollection<T>(potentialList);
                    var sortedList = new SortListFromCollection<int, T>(item => ((ISortKey<int>)item).InternalSortKey, potentialCollection);
                    return sortedList;
                }
                catch (ArgumentException)
                {
                    // also failed to wrap as collection, ignore this exception and fall through to the final exception
                }
            }

            throw new ArgumentException(String.Format("Unable to determine ListWrapper for {0}", listType.FullName), "potentialList");
        }

        /// <summary>
        /// Helper method to avoid explicit generic parameter when possible.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="potentialList"></param>
        /// <returns></returns>
        public static IList<T> WrapAsList<T>(ICollection<T> potentialList)
        {
            return WrapAsList<T>((object)potentialList);
        }
    }

    /// <summary>
    /// This wrapper takes a collection object and turns it into an ICollection&lt;T&gt; and IList&lt;T&gt;
    /// </summary>
    public class CastingListWrapper<T> : ICollection<T>, IList<T>
    {
        protected IList baseList;

        public CastingListWrapper(IList baseList)
        {
            this.baseList = baseList;
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return baseList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            baseList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            baseList.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return (T)baseList[index];
            }
            set
            {
                baseList[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            baseList.Add(item);
        }

        public void Clear()
        {
            baseList.Clear();
        }

        public bool Contains(T item)
        {
            return baseList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in baseList)
            {
                array[arrayIndex++] = (T)item;
            }
        }

        public int Count
        {
            get { return baseList.Count; }
        }

        public bool IsReadOnly
        {
            get { return baseList.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            if (baseList.Contains(item))
            {
                baseList.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return baseList.Cast<T>().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseList.GetEnumerator();
        }

        #endregion

    }

    public class GenericCastingCollectionWrapper<TFrom, TResult> : ICollection<TResult>
        where TFrom : TResult
    {
        private ICollection<TFrom> baseCollection;

        public GenericCastingCollectionWrapper(ICollection<TFrom> baseList)
        {
            this.baseCollection = baseList;
        }

        #region ICollection<T> Members

        public void Add(TResult item)
        {
            baseCollection.Add((TFrom)item);
        }

        public void Clear()
        {
            baseCollection.Clear();
        }

        public bool Contains(TResult item)
        {
            if (item is TFrom)
                return baseCollection.Contains((TFrom)item);
            else
                return false;
        }

        public void CopyTo(TResult[] array, int arrayIndex)
        {
            foreach (var item in baseCollection)
            {
                array[arrayIndex++] = item;
            }
        }

        public int Count
        {
            get { return baseCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return baseCollection.IsReadOnly; }
        }

        public bool Remove(TResult item)
        {
            if (Contains(item))
            {
                baseCollection.Remove((TFrom)item);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<TResult> GetEnumerator()
        {
            return baseCollection.Cast<TResult>().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseCollection.GetEnumerator();
        }

        #endregion
    }

    public class GenericCastingListWrapper<TFrom, TResult> : GenericCastingCollectionWrapper<TFrom, TResult>, IList<TResult>
        where TFrom : TResult
    {
        protected IList<TFrom> baseList;

        public GenericCastingListWrapper(IList<TFrom> baseList)
            : base(baseList)
        {
            this.baseList = baseList;
        }

        #region IList<T> Members

        public int IndexOf(TResult item)
        {
            if (item is TFrom)
                return baseList.IndexOf((TFrom)item);
            else
                return -1;
        }

        public void Insert(int index, TResult item)
        {
            baseList.Insert(index, (TFrom)item);
        }

        public void RemoveAt(int index)
        {
            baseList.RemoveAt(index);
        }

        public TResult this[int index]
        {
            get
            {
                return (TResult)baseList[index];
            }
            set
            {
                baseList[index] = (TFrom)value;
            }
        }

        #endregion
    }

    public class SortListFromCollection<TKey, TValue> : IList<TValue>
    {
        private ICollection<TValue> _baseCollection;
        private SortedList<TKey, TValue> _sortedList;
        private Func<TValue, TKey> _keyFromItem;

        public SortListFromCollection(Func<TValue, TKey> keyFromItem, ICollection<TValue> collection)
        {
            _baseCollection = collection;
            _keyFromItem = keyFromItem;
            // allocate a bit more space to avoid immediate re-allocation at first Add
            _sortedList = new SortedList<TKey, TValue>(_baseCollection.Count + 10);
            foreach (var i in _baseCollection)
            {
                _sortedList.Add(_keyFromItem(i), i);
            }
        }

        #region IList<TValue> Members

        public int IndexOf(TValue item)
        {
            return _sortedList.IndexOfKey(_keyFromItem(item));
        }

        public void Insert(int index, TValue item)
        {
            Add(item);
        }

        public void RemoveAt(int index)
        {
            var key = _sortedList.Keys[index];
            var item = _sortedList[key];
            _baseCollection.Remove(item);
            _sortedList.Remove(key);
        }

        public TValue this[int index]
        {
            get
            {
                var key = _sortedList.Keys[index];
                var item = _sortedList[key];
                return item;
            }
            set
            {
                RemoveAt(index);
                Add(value);
            }
        }

        #endregion

        #region ICollection<TValue> Members

        public void Add(TValue item)
        {
            // Order of inserting is important
            // _sortedList does not fire Events, _baseCollection does
            // if _baseCollection fires an update event, _sortedList must be prepeard already
            _sortedList.Add(_keyFromItem(item), item);
            _baseCollection.Add(item);
        }

        public void Clear()
        {
            _baseCollection.Clear();
            _sortedList.Clear();
        }

        public bool Contains(TValue item)
        {
            return _baseCollection.Contains(item);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            _sortedList.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _baseCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return _baseCollection.IsReadOnly; }
        }

        public bool Remove(TValue item)
        {
            var result = _baseCollection.Remove(item);

            // only remove item if it actually exists
            if (result)
            {
                _sortedList.Remove(_keyFromItem(item));
            }

            return result;
        }

        #endregion

        #region IEnumerable<TValue> Members

        public IEnumerator<TValue> GetEnumerator()
        {
            return _sortedList.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sortedList.Values.GetEnumerator();
        }

        #endregion
    }

    public interface IKeyed<T>
    {
        T Key { get; }
    }
}