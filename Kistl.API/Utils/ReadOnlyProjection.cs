using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Utils
{

    public interface IReadOnlyList<TValue>
        : IList<TValue>, ICollection<TValue>, IEnumerable<TValue>
    {
    }

    public abstract class AbstractProjectedList<TInput, TOutput> : IList<TOutput>, ICollection<TOutput>, IEnumerable<TOutput>,  IList, ICollection, IEnumerable
    {
        private readonly IList<TInput> _list;
        private readonly Func<TInput, TOutput> _selector;
        private readonly Func<TOutput, TInput> _inverter;
        private readonly Dictionary<TInput, TOutput> _selectorCache = new Dictionary<TInput, TOutput>();
        private readonly Dictionary<TOutput, TInput> _inverterCache = new Dictionary<TOutput, TInput>();
        private readonly bool _isReadonly;

        /// <summary>
        /// </summary>
        /// <param name="list">the collection to project</param>
        /// <param name="selector">produces TOutput objects for TInput values; MUST always create Equal objects for the same input</param>
        /// <param name="inverter">an optional inversion of the <paramref name="selector"/>; used to accelerate some operations; MAY be null</param>
        /// <param name="isReadonly"></param>
        protected AbstractProjectedList(IList<TInput> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter, bool isReadonly)
        {
            if (list == null) { throw new ArgumentNullException("list"); }
            if (selector == null) { throw new ArgumentNullException("selector"); }

            if (list.IsReadOnly != isReadonly) throw new ArgumentException(string.Format("list.IsReadOnly ({0}) != isReadonly ({1})", list.IsReadOnly, isReadonly));

            _list = list;
            _selector = input => { if (_selectorCache.ContainsKey(input)) { return _selectorCache[input]; } else { return _selectorCache[input] = selector(input); } };
            if(inverter != null) _inverter = output => { if (_inverterCache.ContainsKey(output)) { return _inverterCache[output]; } else { return _inverterCache[output] = inverter(output); } };
            _isReadonly = isReadonly;
        }

        #region Utilities

        public ICollection<TInput> Collection
        {
            get { return _list; }
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
                return _list.FirstOrDefault(input => Object.Equals(item, _selector(input)));
            }
        }

        #endregion

        #region ICollection<TOutput> Members

        void ICollection<TOutput>.Add(TOutput item)
        {
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.Add(Invert(item));
        }

        void ICollection<TOutput>.Clear()
        {
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.Clear();
            _selectorCache.Clear();
            _inverterCache.Clear();
        }

        public bool Contains(TOutput item)
        {
            return _list.Contains(Invert(item));
        }

        public void CopyTo(TOutput[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");

            if (arrayIndex >= array.Length
                || (array.Length - arrayIndex) < _list.Count)
                throw new ArgumentOutOfRangeException("arrayIndex");

            foreach (var output in this)
            {
                array[arrayIndex++] = output;
            }
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return _isReadonly; }
        }

        public bool Remove(TOutput item)
        {
            if(IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            return _list.Remove(Invert(item));
        }

        #endregion

        #region IEnumerable<TOutput> Members

        public IEnumerator<TOutput> GetEnumerator()
        {
            return _list.Select(_selector).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.Select(_selector).GetEnumerator();
        }

        #endregion

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
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.Insert(index, Invert(item));
        }

        void IList<TOutput>.RemoveAt(int index)
        {
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.RemoveAt(index);
        }

        public TOutput this[int index]
        {
            get
            {
                return Selector(_list[index]);
            }
            set
            {
                _list[index] = Invert(value);
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
                this[index] = value;
            }
        }

        #endregion

        #region IList Members

        int IList.Add(object value)
        {
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.Add(Invert((TOutput)value));
            return _list.Count - 1;
        }

        void IList.Clear()
        {
            ((ICollection<TOutput>)this).Clear();
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
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.Insert(index, Invert((TOutput)value));
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.Remove(Invert((TOutput)value));
        }

        void IList.RemoveAt(int index)
        {
            if (IsReadOnly) throw new InvalidOperationException("This enumeration is readonly");
            _list.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (TOutput)value;
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

    /// <summary>
    /// A live read only projection of a List through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying List.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the List.</typeparam>
    public class ReadOnlyProjectedList<TInput, TOutput>
        : AbstractProjectedList<TInput, TOutput>, IReadOnlyList<TOutput>
    {
        public ReadOnlyProjectedList(IList<TInput> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(list, selector, inverter, true)
        {
        }
    }

    /// <summary>
    /// A live projection of a List through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying List.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the List.</typeparam>
    public class ProjectedList<TInput, TOutput>
        : AbstractProjectedList<TInput, TOutput>
    {
        public ProjectedList(IList<TInput> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(list, selector, inverter, false)
        {
        }
    }
}