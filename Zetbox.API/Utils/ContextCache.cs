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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Store IPersistenceObjects ordered by (root-)Type and ID for fast access by type or Export Guid.
    /// </summary>
    public class ContextCache<TKey>
        : ICollection<IPersistenceObject>
    {
        private Dictionary<InterfaceType, Dictionary<TKey, IPersistenceObject>> _objects = new Dictionary<InterfaceType, Dictionary<TKey, IPersistenceObject>>();
        private Dictionary<Guid, IPersistenceObject> _exportableobjects = new Dictionary<Guid, IPersistenceObject>();
        private readonly IZetboxContext ctx;
        private readonly Func<IPersistenceObject, TKey> keyFromItem;

        public ContextCache(IZetboxContext parent, Func<IPersistenceObject, TKey> keyFromItem)
        {
            this.ctx = parent;
            this.keyFromItem = keyFromItem;
        }

        public IPersistenceObject Lookup(InterfaceType t, TKey id)
        {
            // Interface types are structs and can't be null
            //if (t == null) { throw new ArgumentNullException("t"); }
            var rootT = t.GetRootType();

            if (!_objects.ContainsKey(rootT))
                return null;

            IDictionary<TKey, IPersistenceObject> typeList = _objects[rootT];
            if (!typeList.ContainsKey(id))
                return null;

            return typeList[id];
        }

        public IPersistenceObject Lookup(Guid exportGuid)
        {
            if (!_exportableobjects.ContainsKey(exportGuid))
                return null;
            return _exportableobjects[exportGuid];
        }

        public IEnumerable this[InterfaceType t]
        {
            get
            {
                // Interface types are structs and can't be null
                //if (t == null) { throw new ArgumentNullException("t"); }
                var rootT = t.GetRootType();

                if (!_objects.ContainsKey(rootT))
                    return null;

                Dictionary<TKey, IPersistenceObject> typeList = _objects[rootT];
                return typeList.Values;
            }
        }

        #region ICollection<IPersistenceObject> Members

        public void Add(IPersistenceObject item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }
            var rootT = ctx.GetInterfaceType(item).GetRootType();

            // create per-Type dictionary on-demand
            if (!_objects.ContainsKey(rootT))
                _objects[rootT] = new Dictionary<TKey, IPersistenceObject>();

            _objects[rootT][keyFromItem(item)] = item;

            if (item is IExportableInternal)
            {
                var guid = ((IExportableInternal)item).ExportGuid;
                if (!_exportableobjects.ContainsKey(guid))
                    _exportableobjects[guid] = item;
            }
        }

        public void Clear()
        {
            _objects.Clear();
            _exportableobjects.Clear();
        }

        public bool Contains(IPersistenceObject item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }
            var rootT = ctx.GetInterfaceType(item).GetRootType();
            return _objects.ContainsKey(rootT) && _objects[rootT].ContainsKey(keyFromItem(item));
        }

        public void CopyTo(IPersistenceObject[] array, int arrayIndex)
        {
            foreach (IPersistenceObject item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        public int Count
        {
            get { return _objects.Values.Sum(list => list.Count); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IPersistenceObject item)
        {
            if (item == null) { throw new ArgumentNullException("item"); }
            var rootT = ctx.GetInterfaceType(item).GetRootType();

            if (Contains(item))
                // should always return true
                return _objects[rootT].Remove(keyFromItem(item));
            else
                return false;
        }

        #endregion

        #region IEnumerable<IPersistenceObject> Members

        public IEnumerator<IPersistenceObject> GetEnumerator()
        {
            // use ToList() to avoid concurrent modification exceptions
            foreach (var typeList in _objects.Values.ToList())
            {
                foreach (IPersistenceObject obj in typeList.Values.ToList())
                {
                    yield return obj;
                }
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            // reuse strongly typed enumerator
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Rebuilds the internal caches without changing the set of contained objects.
        /// This is necessary after a SubmitChanges, if the IDs have changed.
        /// </summary>
        public void Rebuild()
        {
            foreach (var dict in _objects.Values)
            {
                foreach (var kv in dict.ToList())
                {
                    var newKey = keyFromItem(kv.Value);
                    if (Object.Equals(kv.Key, newKey))
                    {
                        dict.Remove(kv.Key);
                        dict[newKey] = kv.Value;
                    }
                }
            }
        }
    }
}
