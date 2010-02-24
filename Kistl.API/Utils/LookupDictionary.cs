
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class wraps a list into a dictionary and uses specified lambdas for looking up values in the list.
    /// Contrary to Linq's Lookup operator, this class has no up-front costs in CPU or RAM but has to search 
    /// through the underlying list on access. This is needed when the underlying list is mutable.
    /// </summary>
    /// <typeparam name="TKey">the type of values used as lookup key</typeparam>
    /// <typeparam name="TUnderlyingValue">the type of stored objects</typeparam>
    /// <typeparam name="TValue">the type of presented objects</typeparam>
    public sealed class LookupDictionary<TKey, TUnderlyingValue, TValue>
            : IDictionary<TKey, TValue>, System.Collections.IDictionary
    {
        /// <summary>
        /// The backing store of this ListDictionary. This list is searched when looking up stuff.
        /// </summary>
        private readonly IList<TUnderlyingValue> _list;

        /// <summary>
        /// The function to get a key from an item.
        /// </summary>
        private readonly Func<TUnderlyingValue, TKey> _key;

        /// <summary>
        /// The function to get a value from an item.
        /// </summary>
        private readonly Func<TUnderlyingValue, TValue> _value;

        /// <summary>
        /// Initialises a new instance of the ListDictionary class.
        /// </summary>
        /// <param name="list">the list to use as underlying storage</param>
        /// <param name="key">the function to create keys for lookups</param>
        /// <param name="value">the function to create values from items</param>
        public LookupDictionary(IList<TUnderlyingValue> list, Func<TUnderlyingValue, TKey> key, Func<TUnderlyingValue, TValue> value)
        {
            if (list == null) { throw new ArgumentNullException("list"); }
            if (key == null) { throw new ArgumentNullException("key"); }
            if (value == null) { throw new ArgumentNullException("value"); }
            _list = list;
            _key = key;
            _value = value;
        }

        /// <summary>
        /// An enumerable list of Keys. May not have to evaluate keys for all items.
        /// </summary>
        public IEnumerable<TKey> KeysEnumerable
        {
            get { return _list.Select(item => _key(item)); }
        }

        /// <summary>
        /// Returns the index of the first item matching the specified key.
        /// </summary>
        /// <param name="key">the key to search for</param>
        /// <returns>the index of the found item or -1 if there is no matching item found</returns>
        public int IndexOf(TKey key)
        {
            int idx = 0;
            foreach (var item in _list)
            {
                if (Object.Equals(key, _key(item)))
                {
                    return idx;
                }
                idx += 1;
            }
            return -1;
        }

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// The LookupDictionary cannot be modified.
        /// </summary>
        /// <param name="key">the key value</param>
        /// <param name="value">the object to store.</param>
        /// <exception cref="InvalidOperationException">always</exception>
        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        /// <inheritdoc/>
        public bool ContainsKey(TKey key)
        {
            return KeysEnumerable.Contains(key);
        }

        /// <inheritdoc/>
        public ICollection<TKey> Keys
        {
            get { return KeysEnumerable.ToList(); }
        }

        /// <summary>
        /// The LookupDictionary cannot be modified.
        /// </summary>
        /// <param name="key">the key value</param>
        /// <exception cref="InvalidOperationException">always</exception>
        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        /// <summary>
        /// Tries to get the value matching the specified key. Returns false otherwise.
        /// </summary>
        /// <param name="key">the key to search for</param>
        /// <param name="value">the found item is passed in this parameter</param>
        /// <returns>a value indicating whether or not an item was found</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            int idx = IndexOf(key);
            if (idx == -1)
            {
                value = default(TValue);
                return false;
            }

            value = _value(_list[idx]);
            return true;
        }

        /// <summary>
        /// the underlying list of values
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _list.Select(i => _value(i)).ToList(); }
        }

        /// <summary>
        /// Gets or sets the item at the specified key. If there is no matching item 
        /// or the key doesn't match the specified item on setting, an exception is thrown.
        /// </summary>
        /// <param name="key">the key to use</param>
        /// <returns>the found item</returns>
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (TryGetValue(key, out value))
                {
                    return value;
                }
                else
                {
                    return default(TValue); // throw new ArgumentOutOfRangeException("key");
                }
            }
            set
            {
                throw new InvalidOperationException("LookupDictionary cannot be modified");
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <inheritdoc/>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        /// <inheritdoc/>
        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _list.Any(i => _value(i).Equals(item.Value));
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null) { throw new ArgumentNullException("array"); }

            for (int i = 0; i < _list.Count; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<TKey, TValue>(_key(_list[i]), _value(_list[i]));
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <inheritdoc/>
        public bool IsReadOnly
        {
            get { return _list.IsReadOnly; }
        }

        /// <inheritdoc/>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _list.Select(item => new KeyValuePair<TKey, TValue>(_key(item), _value(item))).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <inheritdoc/>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IDictionary Members

        void System.Collections.IDictionary.Add(object key, object value)
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        void System.Collections.IDictionary.Clear()
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        bool System.Collections.IDictionary.Contains(object key)
        {
            return this.ContainsKey((TKey)key);
        }

        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        bool System.Collections.IDictionary.IsFixedSize
        {
            get { return false; }
        }

        bool System.Collections.IDictionary.IsReadOnly
        {
            get { return true; }
        }

        System.Collections.ICollection System.Collections.IDictionary.Keys
        {
            get { return this.Keys.ToList(); }
        }

        void System.Collections.IDictionary.Remove(object key)
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        System.Collections.ICollection System.Collections.IDictionary.Values
        {
            get { return this.Values.ToList(); }
        }

        object System.Collections.IDictionary.this[object key]
        {
            get
            {
                return this[(TKey)key];
            }
            set
            {
                this[(TKey)key] = (TValue)value;
            }
        }

        #endregion

        #region ICollection Members

        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                array.SetValue(new KeyValuePair<TKey, TValue>(_key(_list[i]), _value(_list[i])), i + index);
            }
        }

        int System.Collections.ICollection.Count
        {
            get { return this.Count; }
        }

        bool System.Collections.ICollection.IsSynchronized
        {
            get { return false; }
        }

        object System.Collections.ICollection.SyncRoot
        {
            get { return null; }
        }

        #endregion
    }
}
