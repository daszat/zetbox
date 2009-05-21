
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
    /// <typeparam name="TValue">the type of stored objects</typeparam>
    public sealed class LookupDictionary<TKey, TValue>
            : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// The backing store of this ListDictionary. This list is searched when looking up stuff.
        /// </summary>
        private IList<TValue> _list;

        /// <summary>
        /// The function to get a key from an item.
        /// </summary>
        private Func<TValue, TKey> _key;

        /// <summary>
        /// Initialises a new instance of the ListDictionary class.
        /// </summary>
        /// <param name="list">the list to use as underlying storage</param>
        /// <param name="key">the function to create keys for lookups</param>
        public LookupDictionary(IList<TValue> list, Func<TValue, TKey> key)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            _list = list;
            _key = key;
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
        /// Adds an object to the underlying list, if the key matches.
        /// </summary>
        /// <param name="key">the key value</param>
        /// <param name="value">the object to store.</param>
        /// <exception cref="ArgumentOutOfRangeException">if the key doesn't match the value</exception>
        public void Add(TKey key, TValue value)
        {
            if (!Object.Equals(key, _key(value)))
            {
                throw new ArgumentOutOfRangeException("key");
            }
            _list.Add(value);
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

        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            int idx = IndexOf(key);
            if (idx == -1)
            {
                return false;
            }

            _list.RemoveAt(idx);
            return true;
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

            value = _list[idx];
            return true;
        }

        /// <summary>
        /// the underlying list of values
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _list; }
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
                Add(key, value);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _list.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _list.Contains(item.Value);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<TKey, TValue>(_key(_list[i]), _list[i]);
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
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _list.Select(item => new KeyValuePair<TKey, TValue>(_key(item), item)).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <inheritdoc/>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
