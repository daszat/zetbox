using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Utils
{

    public interface IReadOnlyCollection<TValue>
        : ICollection<TValue>, IEnumerable<TValue>
    {
    }

    public interface IReadOnlyList<TValue>
        : IList<TValue>, IReadOnlyCollection<TValue>
    {
    }

    /// <summary>
    /// A live read only projection of a collection through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying collection.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the collection.</typeparam>
    public class ReadOnlyProjectedCollection<TInput, TOutput>
        : IReadOnlyCollection<TOutput>
    {
        private readonly ICollection<TInput> _collection;
        private readonly Func<TInput, TOutput> _selector;
        private readonly Func<TOutput, TInput> _inverter;

        /// <summary>
        /// </summary>
        /// <param name="collection">the collection to project</param>
        /// <param name="selector">produces TOutput objects for TInput values; MUST always create Equal objects for the same input</param>
        /// <param name="inverter">an optional inversion of the <paramref name="selector"/>; used to accelerate some operations; MAY be null</param>
        public ReadOnlyProjectedCollection(ICollection<TInput> collection, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
        {
            if (collection == null) { throw new ArgumentNullException("collection"); }
            if (selector == null) { throw new ArgumentNullException("selector"); }

            _collection = collection;
            _selector = selector;
            _inverter = inverter;
        }

        #region Utilities

        public ICollection<TInput> Collection
        {
            get { return _collection; }
        }

        protected Func<TInput, TOutput> Selector { get { return _selector; } }

        protected Func<TOutput, TInput> Inverter { get { return _inverter; } }

        /// <summary>
        /// returns an TInput item whose projection matches <paramref name="item"/>. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected TInput Invert(TOutput item)
        {
            if (_inverter != null)
            {
                return _inverter(item);
            }
            else
            {
                return _collection.FirstOrDefault(input => Object.Equals(item, _selector(input)));
            }
        }

        #endregion

        #region ICollection<TOutput> Members

        void ICollection<TOutput>.Add(TOutput item)
        {
            throw new InvalidOperationException();
        }

        void ICollection<TOutput>.Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(TOutput item)
        {
            return _collection.Contains(Invert(item));
        }

        public void CopyTo(TOutput[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");

            if (arrayIndex >= array.Length
                || (array.Length - arrayIndex) < _collection.Count)
                throw new ArgumentOutOfRangeException("arrayIndex");

            foreach (var output in this)
            {
                array[arrayIndex++] = output;
            }
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(TOutput item)
        {
            throw new InvalidOperationException();
        }

        #endregion

        #region IEnumerable<TOutput> Members

        public IEnumerator<TOutput> GetEnumerator()
        {
            return _collection.Select(_selector).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.Select(_selector).GetEnumerator();
        }

        #endregion

    }


    /// <summary>
    /// A live read only projection of a List through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying List.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the List.</typeparam>
    public class ReadOnlyProjectedList<TInput, TOutput>
        : ReadOnlyProjectedCollection<TInput, TOutput>, IReadOnlyList<TOutput>, IList, ICollection, IEnumerable
    {
        private IList<TInput> _list;

        public ReadOnlyProjectedList(IList<TInput> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(list, selector, inverter)
        {
            _list = list;
        }

        #region IList<TResult> Members

        public int IndexOf(TOutput item)
        {
            var source = _list.Where(v => Object.Equals(Selector(v), item)).FirstOrDefault();

            if (source != null)
                return _list.IndexOf(source);
            else
                return -1;
        }

        void IList<TOutput>.Insert(int index, TOutput item)
        {
            throw new InvalidOperationException();
        }

        void IList<TOutput>.RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }

        public TOutput this[int index]
        {
            get
            {
                return Selector(_list[index]);
            }
        }

        // hide the settor when not using the interface
        TOutput IList<TOutput>.this[int index]
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
            return this.Contains((TOutput)value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((TOutput)value);
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
            this.CopyTo((TOutput[])array, index);
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