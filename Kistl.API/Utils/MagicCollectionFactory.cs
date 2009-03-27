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

        public static ICollection<T> WrapAsCollection<T, X>(ICollection<X> collection)
            where X : T
        {
            return new GenericCastingCollectionWrapper<T, X>(collection);
        }

        /// <summary>
        /// Wrap "arbitrary" list-like objects into an ICollection&lt;T&gt;. Currently works with ICollection&lt;T&gt;s and ILists
        /// </summary>
        public static ICollection<T> WrapAsCollection<T>(object collection)
        {
            if (typeof(ICollection<T>).IsAssignableFrom(collection.GetType()))
            {
                return (ICollection<T>)collection;
            }
            else if (typeof(IList).IsAssignableFrom(collection.GetType()))
            {
                return new CastingCollectionWrapper<T>((IList)collection);
            }
            return null;
        }

        public static IList<T> WrapAsListHelper<T, X>(IList<X> collection)
            where X : T
        {
            return new GenericCastingListWrapper<T, X>(collection);
        }

        /// <summary>
        /// Wrap a list-like objects into an IList&lt;T&gt;. Currently works with ILists
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

                if (elementTypes.Count() > 1)
                {
                    throw new AmbiguousMatchException("Ambiguous Element Types found");
                }
                else if (elementTypes.Count() == 1)
                {
                    MethodInfo wrapAsList = typeof(MagicCollectionFactory).GetMethod("WrapAsListHelper");
                    return (IList<T>)wrapAsList.MakeGenericMethod(typeof(T), elementTypes.Single()).Invoke(null, new object[] { collection });
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

    public class GenericCastingCollectionWrapper<T, X> : ICollection<T>
        where X : T
    {
        protected ICollection<X> baseCollection;

        public GenericCastingCollectionWrapper(ICollection<X> baseList)
        {
            this.baseCollection = baseList;
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            baseCollection.Add((X)item);
        }

        public void Clear()
        {
            baseCollection.Clear();
        }

        public bool Contains(T item)
        {
            if (item is X)
                return baseCollection.Contains((X)item);
            else
                return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
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

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                baseCollection.Remove((X)item);
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
            return baseCollection.Cast<T>().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseCollection.GetEnumerator();
        }

        #endregion
    }

    public class GenericCastingListWrapper<T, X> : GenericCastingCollectionWrapper<T, X>, IList<T>
        where X : T
    {
        protected IList<X> baseList;

        public GenericCastingListWrapper(IList<X> baseList)
            : base(baseList)
        {
            this.baseList = baseList;
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            if (item is X)
                return baseList.IndexOf((X)item);
            else
                return -1;
        }

        public void Insert(int index, T item)
        {
            baseList.Insert(index, (X)item);
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
                baseList[index] = (X)value;
            }
        }

        #endregion
    }


}
