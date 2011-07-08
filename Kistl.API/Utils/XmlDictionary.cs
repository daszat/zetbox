
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    // These two classes can be used to substitute for a Dictionary, when XmlSerialization is needed

    [Serializable]
    public sealed class XmlKeyValuePair<TKey, TValue>
    {
        public XmlKeyValuePair() { }
        public XmlKeyValuePair(KeyValuePair<TKey, TValue> kvp)
        {
            this.Key = kvp.Key;
            this.Value = kvp.Value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }

    [Serializable]
    public sealed class XmlDictionary<TKey, TValue> : IEnumerable<XmlKeyValuePair<TKey, TValue>>
    {
        private readonly Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        public Dictionary<TKey, TValue> Dict { get { return _dict; } }

        IEnumerator<XmlKeyValuePair<TKey, TValue>> IEnumerable<XmlKeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return Dict.Select(kvp => new XmlKeyValuePair<TKey, TValue>(kvp)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Dict.Select(kvp => new XmlKeyValuePair<TKey, TValue>(kvp)).GetEnumerator();
        }

        [Obsolete("Only used for XmlSerializer")]
        public void Add(object item)
        {
            var kvp = item as XmlKeyValuePair<TKey, TValue>;
            if (kvp != null)
                Dict.Add(kvp.Key, kvp.Value);
        }

        public TValue this[TKey key]
        {
            get
            {
                return Dict[key];
            }
            set
            {
                Dict[key] = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            Dict.Add(key, value);
        }

        public ICollection<TKey> Keys
        {
            get { return Dict.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return Dict.Values; }
        }
    }
}
