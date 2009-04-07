using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Utils
{

    public interface IReadOnlyCollection<TValue>
        : IList<TValue>, ICollection<TValue>, IEnumerable<TValue>
    {
    }

    /// <summary>
    /// A living read only projection of a collection through a function
    /// </summary>
    /// <typeparam name="TValue">The type of elements in the underlying collection.</typeparam>
    /// <typeparam name="TResult">The type of projected elements in the collection.</typeparam>
    public class ReadOnlyProjection<TValue, TResult>
        : IReadOnlyCollection<TResult>, IList, ICollection, IEnumerable
    {
        private IList<TValue> _list;
        private Func<TValue, TResult> _select;
        public ReadOnlyProjection(IList<TValue> list, Func<TValue, TResult> select)
        {
            _list = list;
            _select = select;
        }

        protected Func<TValue, TResult> Selector { get { return _select; } }

        #region IList<TResult> Members

        public int IndexOf(TResult item)
        {
            var source = _list.Where(v => Object.Equals(_select(v), item)).FirstOrDefault();

            if (source != null)
                return _list.IndexOf(source);
            else
                return -1;
        }

        void IList<TResult>.Insert(int index, TResult item)
        {
            throw new InvalidOperationException();
        }

        void IList<TResult>.RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }

        public TResult this[int index]
        {
            get
            {
                return _select(_list[index]);
            }
        }

        TResult IList<TResult>.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        #endregion

        #region ICollection<TResult> Members

        void ICollection<TResult>.Add(TResult item)
        {
            throw new InvalidOperationException();
        }

        void ICollection<TResult>.Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(TResult item)
        {
            return _list.Any(v => Object.Equals(_select(v), item));
        }

        public void CopyTo(TResult[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");

            if (arrayIndex >= array.Length
                || (array.Length - arrayIndex) < _list.Count)
                throw new ArgumentOutOfRangeException("arrayIndex");

            for (int i = 0; i < _list.Count; i++)
            {
                array[i + arrayIndex] = _select(_list[i]);
            }
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<TResult>.Remove(TResult item)
        {
            throw new InvalidOperationException();
        }

        #endregion

        #region IEnumerable<TResult> Members

        public IEnumerator<TResult> GetEnumerator()
        {
            foreach (var v in _list)
            {
                yield return _select(v);
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IList Members

        int IList.Add(object value)
        {
            throw new InvalidOperationException();
        }

        void IList.Clear()
        {
            throw new InvalidOperationException();
        }

        bool IList.Contains(object value)
        {
            return this.Contains((TResult)value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((TResult)value);
        }

        void IList.Insert(int index, object value)
        {
            throw new InvalidOperationException();
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            throw new InvalidOperationException();
        }

        void IList.RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((TResult[])array, index);
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        #endregion
    }
}