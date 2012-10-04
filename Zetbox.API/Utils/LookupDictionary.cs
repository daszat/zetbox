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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class transforms a list into a dictionary. The specified lambdas are used to create the keys and values.
    /// On initialisation all keys are created and stored. The values are created lazily on first access and cached.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Contrary to Linq's Lookup operator, this class has reduced up-front costs in CPU and RAM.
    /// </para>
    /// <para>
    /// Changes to the source list are ignored.
    /// </para>
    /// </remarks>
    /// <typeparam name="TKey">the type of values used as lookup key</typeparam>
    /// <typeparam name="TUnderlyingValue">the type of stored objects</typeparam>
    /// <typeparam name="TValue">the type of presented objects</typeparam>
    public sealed class LookupDictionary<TKey, TUnderlyingValue, TValue>
            : IDictionary<TKey, TValue>, System.Collections.IDictionary
    {
        /// <summary>
        /// The underlying items of this LookupDictionary. Stores items by their generated key.
        /// </summary>
        private readonly Dictionary<TKey, TUnderlyingValue> _items;

        /// <summary>
        /// The backing store of this LookupDictionary. Stores already generated values by key.
        /// </summary>
        private readonly Dictionary<TKey, TValue> _values;

        /// <summary>
        /// The function to get a value from an item.
        /// </summary>
        private readonly Func<TUnderlyingValue, TValue> _valueFunc;

        /// <summary>
        /// Initialises a new instance of the LookupDictionary class.
        /// </summary>
        /// <param name="list">the list to use as underlying storage</param>
        /// <param name="key">the function to create keys for lookups</param>
        /// <param name="value">the function to create values from items</param>
        public LookupDictionary(IList<TUnderlyingValue> list, Func<TUnderlyingValue, TKey> key, Func<TUnderlyingValue, TValue> value)
        {
            if (list == null) { throw new ArgumentNullException("list"); }
            if (key == null) { throw new ArgumentNullException("key"); }
            if (value == null) { throw new ArgumentNullException("value"); }
            _items = list.ToDictionary(key);
            _values = new Dictionary<TKey, TValue>();
            _valueFunc = value;
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
            return _items.ContainsKey(key);
        }

        /// <inheritdoc/>
        public ICollection<TKey> Keys
        {
            get { return _items.Keys; }
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
            if (!_items.ContainsKey(key))
            {
                value = default(TValue);
                return false;
            }

            if (_values.ContainsKey(key))
            {
                value = _values[key];
            }
            else
            {
                _values[key] = value = _valueFunc(_items[key]);
            }

            return true;
        }

        /// <summary>
        /// the underlying list of values
        /// </summary>
        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                foreach (var k in _items.Keys)
                {
                    if (!_values.ContainsKey(k))
                    {
                        _values[k] = _valueFunc(_items[k]);
                    }
                }
                return _values.Values;
            }
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
                    throw new ArgumentOutOfRangeException("key", key, "No such key found");
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
            return _items.ContainsKey(item.Key) && this[item.Key].Equals(item.Value);
        }

        /// <inheritdoc/>
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null) { throw new ArgumentNullException("array"); }

            foreach (var key in _items.Keys)
            {
                array[arrayIndex++] = new KeyValuePair<TKey, TValue>(key, this[key]);
            }
        }

        /// <inheritdoc/>
        public int Count
        {
            get { return _items.Count; }
        }

        /// <inheritdoc/>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <inheritdoc/>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException("LookupDictionary cannot be modified");
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <inheritdoc/>
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _items.Keys.Select(key => new KeyValuePair<TKey, TValue>(key, this[key])).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <inheritdoc/>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
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
            get { return ((IDictionary<TKey, TValue>)this).Values.ToList(); }
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
            if (array == null) { throw new ArgumentNullException("array"); }

            foreach (var key in _items.Keys)
            {
                array.SetValue(new KeyValuePair<TKey, TValue>(key, this[key]), index++);
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
