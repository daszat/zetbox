using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Kistl.API.Client
{
    internal class CacheController<T> where T : ICloneable
    {
        private static CacheController<T> _current = new CacheController<T>();
        public static CacheController<T> Current
        {
            get 
            {
                return _current;
            }
        }

        #region Key
        private class Key
        {
            public Key()
            {
            }

            public Key(ObjectType t, int id)
            {
                Type = t;
                ID = id;
            }

            public ObjectType Type { get; set; }
            public int ID { get; set; }

            public override int GetHashCode()
            {
                if (Type == null) return 0;
                return Type.GetHashCode() * 1024 + ID;
            }

            public override bool Equals(object obj)
            {
                Key b = obj as Key;
                if(b == null) return false;
                if (Type == null) return false;
                return this.Type.Equals(b.Type) && this.ID == b.ID;
            }
        }
        #endregion

        private Dictionary<Key, T> _cache = new Dictionary<Key, T>();

        public void Set(ObjectType type, int ID, T obj)
        {
            _cache[new Key(type, ID)] = obj;
        }

        public T Get(ObjectType type, int ID)
        {
            Key k = new Key(type, ID);
            if (_cache.ContainsKey(k))
            {
                Trace.WriteLineIf(TraceClient.TraceHelper.TraceLevel.TraceVerbose, string.Format("Cachehit {0} [{1}]", type, ID));
                return _cache[k];
            }
            else
            {
                return default(T);
            }
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
