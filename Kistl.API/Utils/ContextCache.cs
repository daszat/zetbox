
namespace Kistl.API.Utils
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
        private readonly IKistlContext ctx;
        private readonly Func<IPersistenceObject, TKey> keyFromItem;

        public ContextCache(IKistlContext parent, Func<IPersistenceObject, TKey> keyFromItem)
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

    }
}
