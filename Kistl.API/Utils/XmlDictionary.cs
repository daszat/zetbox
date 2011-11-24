
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

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

    // non-generic interface to avoid implementing IEnumerable on XmlDictonary, which would break XmlSerializer
    public interface IXmlDictionaryDtoData
    {
        IEnumerable<KeyValuePair<object, object>> DtoData { get; }
    }

    [Serializable]
    // non-generic interface to avoid implementing IEnumerable on XmlDictonary, which would break XmlSerializer
    public class XmlDictionary<TKey, TValue> : IXmlDictionaryDtoData
    {
        /// <summary>
        /// This value factory is used to create new value objects when a unknown key is set.
        /// </summary>
        [NonSerialized]
        private readonly Func<TKey, TValue> _valueFactory;

        private Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        public XmlDictionary()
            : this((Func<TKey, TValue>)null)
        {
        }

        public XmlDictionary(IEnumerable<XmlKeyValuePair<TKey, TValue>> data)
            : this((Func<TKey, TValue>)null)
        {
            if (data != null)
            {
                foreach (var d in data)
                {
                    this.Add(d.Key, d.Value);
                }
            }
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

        IEnumerable<KeyValuePair<object, object>> IXmlDictionaryDtoData.DtoData
        {
            get { return Data.Select(xkvp => new KeyValuePair<object, object>(xkvp.Key, xkvp.Value)); }
        }
    }
}
