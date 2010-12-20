
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
    public class ContextCache
        : ICollection<IPersistenceObject>
    {
        private Dictionary<InterfaceType, Dictionary<int, IPersistenceObject>> _objects = new Dictionary<InterfaceType, Dictionary<int, IPersistenceObject>>();
        private Dictionary<Guid, IPersistenceObject> _exportableobjects = new Dictionary<Guid, IPersistenceObject>();
        private readonly IKistlContext ctx;

        public ContextCache(IKistlContext parent)
        {
            this.ctx = parent;
        }

        public IPersistenceObject Lookup(InterfaceType t, int id)
        {
            // Interface types are structs and can't be null
            //if (t == null) { throw new ArgumentNullException("t"); }
            var rootT = t.GetRootType();

            if (!_objects.ContainsKey(rootT))
                return null;

            IDictionary<int, IPersistenceObject> typeList = _objects[rootT];
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

                Dictionary<int, IPersistenceObject> typeList = _objects[rootT];
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
                _objects[rootT] = new Dictionary<int, IPersistenceObject>();

            _objects[rootT][item.ID] = item;

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
            return _objects.ContainsKey(rootT) && _objects[rootT].ContainsKey(item.ID);
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
                return _objects[rootT].Remove(item.ID);
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
