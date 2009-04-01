using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace Kistl.API.Utils
{

    public static class MagicCollectionFactory
    {

        public static ICollection<TResult> WrapAsCollectionHelper<TFrom, TResult>(ICollection<TFrom> collection)
            where TFrom : TResult
        {
            return new GenericCastingCollectionWrapper<TFrom, TResult>(collection);
        }

        public static object WrapAsCollectionReflectionHelper(Type TFrom, Type TResult, object collection)
        {
            MethodInfo wrapAsCollection = typeof(MagicCollectionFactory).GetMethod("WrapAsCollectionHelper");
            return wrapAsCollection.MakeGenericMethod(TFrom, TResult).Invoke(null, new[] { collection });
        }

        /// <summary>
        /// Wrap "arbitrary" list-like objects into an ICollection&lt;T&gt;. Currently works with ICollection&lt;T&gt;s and ILists
        /// </summary>
        public static ICollection<T> WrapAsCollection<T>(object collection)
        {
            if (collection is ICollection<T>)
            {
                return (ICollection<T>)collection;
            }
            else if (collection is IList<T>)
            {
                return (IList<T>)collection;
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
                    return (ICollection<T>)WrapAsCollectionReflectionHelper(elementTypes.Single(), typeof(T), collection);
                }
                else if (collection is IList)
                {
                    return new CastingCollectionWrapper<T>((IList)collection);
                }
            }
            return null;
        }

        public static IList<TResult> WrapAsListHelper<TFrom, TResult>(IList<TFrom> list)
            where TFrom : TResult
        {
            return new GenericCastingListWrapper<TFrom, TResult>(list);
        }

        public static object WrapAsListReflectionHelper(Type TFrom, Type TResult, object list)
        {
            MethodInfo wrapAsList = typeof(MagicCollectionFactory).GetMethod("WrapAsListHelper");
            return wrapAsList.MakeGenericMethod(TFrom, TResult).Invoke(null, new[] { list });
        }


        /// <summary>
        /// Wrap a list-like objects into an IList&lt;T&gt;. Currently works with IList&lt;t>s, ILists and ICollection&ltT>s
        /// </summary>
        public static IList<T> WrapAsList<T>(object collection)
        {
            if (collection is IList<T>)
            {
                return (IList<T>)collection;
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
                    return (IList<T>)WrapAsListReflectionHelper(elementTypes.Single(), typeof(T), collection);
                }
                else if (collection is IList)
                {
                    return new CastingListWrapper<T>((IList)collection);
                }
            }
            return null;
        }

    }

    /// <summary>
    /// This wrapper takes a collection object and turns it into a ICollection&lt;T&gt;
    /// </summary>
    public class CastingCollectionWrapper<T> : ICollection<T>
    {
        protected IList baseList;

        public CastingCollectionWrapper(IList baseList)
        {
            this.baseList = baseList;
        }

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

    public class CastingListWrapper<T> : CastingCollectionWrapper<T>, IList<T>
    {

        public CastingListWrapper(IList baseList)
            : base(baseList)
        {
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

    }

    public class GenericCastingCollectionWrapper<TFrom, TResult> : ICollection<TResult>
        where TFrom : TResult
    {
        protected ICollection<TFrom> baseCollection;

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
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public TValue this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<TValue> Members

        public void Add(TValue item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TValue item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(TValue item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<TValue> Members

        public IEnumerator<TValue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}