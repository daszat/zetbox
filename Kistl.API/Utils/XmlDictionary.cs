
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
    public sealed class XmlDictionary<TKey, TValue> : IEnumerable<XmlKeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

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
