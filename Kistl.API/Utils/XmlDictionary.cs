
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.ObjectModel;

    // These two classes can be used to substitute for a Dictionary, when XmlSerialization is needed

    [Serializable]
    public sealed class XmlKeyValuePair<TKey, TValue>
    {
        public XmlKeyValuePair() { }
        public XmlKeyValuePair(KeyValuePair<TKey, TValue> kvp) : this(kvp.Key, kvp.Value) { }
        public XmlKeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }

    [Serializable]
    public class XmlDictionary<TKey, TValue> : IEnumerable<XmlKeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// This value factory is used to create new value objects when a unknown key is set.
        /// </summary>
        [NonSerialized]
        private readonly Func<TKey, TValue> _valueFactory;

        private Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        public XmlDictionary()
            : this(null)
        {
        }

        public XmlDictionary(Func<TKey, TValue> valueFactory)
        {
            _valueFactory = valueFactory;
        }

        /// <summary>
        /// This list can only be used by the XmlSerializer
        /// </summary>
        [XmlArray("Data")]
        public XmlKeyValuePair<TKey, TValue>[] Data
        {
            get
            {
                return _dict.Select(kvp => new XmlKeyValuePair<TKey, TValue>(kvp.Key, kvp.Value)).ToArray();
            }
            set
            {
                _dict = value.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        [XmlIgnore]
        public TValue this[TKey key]
        {
            get
            {
                if (_valueFactory != null && !_dict.ContainsKey(key))
                {
                    _dict[key] = _valueFactory(key);
                }
                return _dict[key];
            }
            set
            {
                _dict[key] = value;
            }
        }

        [XmlIgnore]
        public ICollection<TKey> Keys
        {
            get
            {
                return _dict.Keys;
            }
        }

        [XmlIgnore]
        public ICollection<TValue> Values
        {
            get
            {
                return _dict.Values;
            }
        }

        public void Add(TKey key, TValue value)
        {
            _dict[key] = value;
        }

        public void Add(object obj)
        {
            var kvp = (XmlKeyValuePair<TKey, TValue>)obj;
            _dict[kvp.Key] = kvp.Value;
        }

        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }

        public void Clear()
        {
            _dict.Clear();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_dict.Select(kvp => new XmlKeyValuePair<TKey, TValue>(kvp))).GetEnumerator();
        }

        IEnumerator<XmlKeyValuePair<TKey, TValue>> IEnumerable<XmlKeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _dict.Select(kvp => new XmlKeyValuePair<TKey, TValue>(kvp)).GetEnumerator();
        }
    }
}
