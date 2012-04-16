namespace Kistl.API.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IReadOnlyCollection<TValue>
        : ICollection<TValue>, IEnumerable<TValue>
    {
    }

    public interface IReadOnlyList<TValue>
        : IList<TValue>, IReadOnlyCollection<TValue>
    {
    }

    public abstract class AbstractProjectedCollection<TInput, TOutput>
        : ICollection<TOutput>, IEnumerable<TOutput>, ICollection, IEnumerable
    {
        private readonly object _lock = new object();
        private readonly Func<ICollection<TInput>> _collection;
        private readonly Func<TInput, TOutput> _selector;
        private readonly Func<TOutput, TInput> _inverter;
        // TODO: these caches should be purged when items are removed or overwritten. As is, these create a veritable memory leak.
        private readonly Dictionary<TInput, TOutput> _selectorCache = new Dictionary<TInput, TOutput>();
        private readonly Dictionary<TOutput, TInput> _inverterCache = new Dictionary<TOutput, TInput>();
        private readonly bool _isReadOnly;

        /// <summary>
        /// </summary>
        /// <param name="collection">the collection to project</param>
        /// <param name="selector">produces TOutput objects for TInput values; MUST always create Equal objects for the same input</param>
        /// <param name="inverter">an optional inversion of the <paramref name="selector"/>; used to accelerate some operations; MAY be null</param>
        /// <param name="isReadOnly">Whether or not this list should allow modifications through the public interface. Regardless of the value specified here, the contents can change if the underlying collection changes.</param>
        protected AbstractProjectedCollection(ICollection<TInput> collection, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter, bool isReadOnly)
        {
            if (collection == null) { throw new ArgumentNullException("collection"); }
            if (selector == null) { throw new ArgumentNullException("selector"); }

            if (!isReadOnly && collection.IsReadOnly)
                throw new ArgumentException("Writable projected collection was requested, but the underlying collection is read only.");

            _collection = () => collection;
            _selector = CreateSelector(selector);
            _inverter = CreateInverter(inverter);
            _isReadOnly = isReadOnly;
        }

        /// <summary>
        /// </summary>
        /// <param name="collection">the collection to project, wrapped in an accessor to be transparent to underlying reference changes</param>
        /// <param name="selector">produces TOutput objects for TInput values; MUST always create Equal objects for the same input</param>
        /// <param name="inverter">an optional inversion of the <paramref name="selector"/>; used to accelerate some operations; MAY be null</param>
        /// <param name="isReadOnly">Whether or not this list should allow modifications through the public interface. Regardless of the value specified here, the contents can change if the underlying collection changes.</param>
        protected AbstractProjectedCollection(Func<ICollection<TInput>> collection, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter, bool isReadOnly)
        {
            if (collection == null) { throw new ArgumentNullException("collection"); }
            if (selector == null) { throw new ArgumentNullException("selector"); }

            if (!isReadOnly && collection().IsReadOnly)
                throw new ArgumentException("Writable projected collection was requested, but the underlying collection is read only.");

            _collection = collection;
            _selector = CreateSelector(selector);
            _inverter = CreateInverter(inverter);
            _isReadOnly = isReadOnly;
        }

        private Func<TOutput, TInput> CreateInverter(Func<TOutput, TInput> inverter)
        {
            if (inverter == null)
                return null;

            return output =>
            {
                if (_inverterCache.ContainsKey(output))
                {
                    return _inverterCache[output];
                }
                else
                {
                    var input = inverter(output);
                    _selectorCache[input] = output;
                    _inverterCache[output] = input;
                    return input;
                }
            };
        }

        private Func<TInput, TOutput> CreateSelector(Func<TInput, TOutput> selector)
        {
            return input =>
            {
                if (_selectorCache.ContainsKey(input))
                {
                    return _selectorCache[input];
                }
                else
                {
                    var output = selector(input);
                    _selectorCache[input] = output;
                    _inverterCache[output] = input;
                    return output;
                }
            };
        }

        #region Utilities

        /// <summary>
        /// This reference may change without notice.
        /// </summary>
        public ICollection<TInput> Collection
        {
            get { return _collection(); }
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
                return _collection().FirstOrDefault(input => Object.Equals(item, _selector(input)));
            }
        }

        #endregion

        #region ICollection<TOutput> Members

        void ICollection<TOutput>.Add(TOutput item)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _collection().Add(Invert(item));
        }

        void ICollection<TOutput>.Clear()
        {
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _collection().Clear();
            _selectorCache.Clear();
            _inverterCache.Clear();
        }

        public bool Contains(TOutput item)
        {
            return _collection().Contains(Invert(item));
        }

        public void CopyTo(TOutput[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex", String.Format("arrayIndex={0} < 0", arrayIndex));

            if (arrayIndex >= array.Length && _collection().Count > 0)
                throw new ArgumentOutOfRangeException("arrayIndex", String.Format("arrayIndex={0} >= array.Length={1} && _collection.Count={2} > 0", arrayIndex, array.Length, _collection().Count));

            if ((array.Length - arrayIndex) < _collection().Count)
                throw new ArgumentOutOfRangeException("arrayIndex", String.Format("(array.Length={0} - arrayIndex={1})={2} < _collection.Count={3}", array.Length, arrayIndex, array.Length - arrayIndex, _collection().Count));

            foreach (var output in this)
            {
                array[arrayIndex++] = output;
            }
        }

        public int Count
        {
            get { return _collection().Count; }
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        public bool Remove(TOutput item)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            return _collection().Remove(Invert(item));
        }

        #endregion

        #region IEnumerable<TOutput> Members

        public IEnumerator<TOutput> GetEnumerator()
        {
            return _collection().Select(_selector).GetEnumerator();
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((TOutput[])array, index);
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                var underlyingCollection = _collection as ICollection;
                if (underlyingCollection != null)
                    return underlyingCollection.IsSynchronized;
                else
                    return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                var underlyingCollection = _collection as ICollection;
                if (underlyingCollection != null)
                    return underlyingCollection.SyncRoot;
                else
                    return _lock;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection().Select(_selector).GetEnumerator();
        }

        #endregion
    }

    public abstract class AbstractProjectedList<TInput, TOutput>
        : AbstractProjectedCollection<TInput, TOutput>, IList<TOutput>, IList
    {
        private readonly Func<IList<TInput>> _list;

        /// <summary>
        /// </summary>
        /// <param name="list">the collection to project</param>
        /// <param name="selector">produces TOutput objects for TInput values; MUST always create Equal objects for the same input</param>
        /// <param name="inverter">an optional inversion of the <paramref name="selector"/>; used to accelerate some operations; MAY be null</param>
        /// <param name="isReadOnly">Whether or not this list should allow modifications through the public interface. Regardless of the value specified here, the contents can change if the underlying collection changes.</param>
        protected AbstractProjectedList(IList<TInput> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter, bool isReadOnly)
            : base(list, selector, inverter, isReadOnly)
        {
            _list = () => list;
        }

        public AbstractProjectedList(Func<IList<TInput>> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter, bool isReadOnly)
            : base(() => (ICollection<TInput>)list(), selector, inverter, isReadOnly)
        {
            _list = list;
        }

        #region IList<TResult> Members

        public int IndexOf(TOutput item)
        {
            return _list().IndexOf(Invert(item));
        }

        void IList<TOutput>.Insert(int index, TOutput item)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _list().Insert(index, Invert(item));
        }

        void IList<TOutput>.RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _list().RemoveAt(index);
        }

        public TOutput this[int index]
        {
            get
            {
                return Selector(_list()[index]);
            }
            set
            {
                _list()[index] = Invert(value);
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
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _list().Add(Invert((TOutput)value));
            return _list().Count - 1;
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
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _list().Insert(index, Invert((TOutput)value));
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _list().Remove(Invert((TOutput)value));
        }

        void IList.RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new InvalidOperationException("This enumeration is readonly");
            _list().RemoveAt(index);
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
    }

    /// <summary>
    /// A live read only projection of a Collection through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying Collection.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the Collection.</typeparam>
    public sealed class ReadOnlyProjectedCollection<TInput, TOutput>
        : AbstractProjectedCollection<TInput, TOutput>, IReadOnlyCollection<TOutput>
    {
        public ReadOnlyProjectedCollection(ICollection<TInput> Collection, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(Collection, selector, inverter, true)
        {
        }

        public ReadOnlyProjectedCollection(Func<ICollection<TInput>> collection, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(collection, selector, inverter, false)
        {
        }
    }

    /// <summary>
    /// A live projection of a Collection through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying Collection.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the Collection.</typeparam>
    public sealed class ProjectedCollection<TInput, TOutput>
        : AbstractProjectedCollection<TInput, TOutput>
    {
        public ProjectedCollection(ICollection<TInput> collection, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(collection, selector, inverter, false)
        {
        }

        public ProjectedCollection(Func<ICollection<TInput>> collection, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(collection, selector, inverter, false)
        {
        }
    }

    /// <summary>
    /// A live read only projection of a List through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying List.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the List.</typeparam>
    public sealed class ReadOnlyProjectedList<TInput, TOutput>
        : AbstractProjectedList<TInput, TOutput>, IReadOnlyList<TOutput>
    {
        public ReadOnlyProjectedList(IList<TInput> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(list, selector, inverter, true)
        {
        }

        public ReadOnlyProjectedList(Func<IList<TInput>> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(list, selector, inverter, true)
        {
        }
    }

    /// <summary>
    /// A live projection of a List through a function
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the underlying List.</typeparam>
    /// <typeparam name="TOutput">The type of projected elements in the List.</typeparam>
    public sealed class ProjectedList<TInput, TOutput>
        : AbstractProjectedList<TInput, TOutput>
    {
        public ProjectedList(IList<TInput> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(list, selector, inverter, false)
        {
        }

        public ProjectedList(Func<IList<TInput>> list, Func<TInput, TOutput> selector, Func<TOutput, TInput> inverter)
            : base(list, selector, inverter, false)
        {
        }
    }
}