using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kistl.API.Utils
{
    /// <summary>
    /// Store IPersistenceObjects ordered by (root-)Type and ID for fast access within the KistlContextImpl
    /// </summary>
    public class ContextCache : ICollection<IPersistenceObject>
    {

        private IDictionary<Type, IDictionary<int, IPersistenceObject>> _objects = new Dictionary<Type, IDictionary<int, IPersistenceObject>>();

        /// <summary>
        /// Returns the root implementation Type of a given IPersistenceObject.
        /// This corresponds to the ID namespace of the object
        /// </summary>
        /// <param name="obj">IPersistenceObject to inspect</param>
        /// <returns>Root Type of the given Type</returns>
        private static Type GetRootImplType(IPersistenceObject obj)
        {
            return GetRootImplType(obj.GetInterfaceType().ToImplementationType());
        }

        /// <summary>
        /// Returns the root implementation Type of a given System.Type.
        /// This corresponds to the ID namespace of the object
        /// </summary>
        /// <param name="t">Type to inspect</param>
        /// <returns>Root Type of the given Type</returns>
        private static Type GetRootImplType(ImplementationType t)
        {
            Type result = t.Type;
            while (result != null && result.BaseType != null && !result.BaseType.IsAbstract)
            {
                result = result.BaseType;
            }

            return result;
        }

        public IPersistenceObject Lookup(InterfaceType t, int id)
        {
            Type rootT = GetRootImplType(t.ToImplementationType());

            if (!_objects.ContainsKey(rootT))
                return null;

            IDictionary<int, IPersistenceObject> typeList = _objects[rootT];
            if (!typeList.ContainsKey(id))
                return null;

            return typeList[id];
        }

        public IEnumerable this[InterfaceType t]
        {
            get
            {
                Type rootT = GetRootImplType(t.ToImplementationType());

                if (!_objects.ContainsKey(rootT))
                    return null;

                IDictionary<int, IPersistenceObject> typeList = _objects[rootT];
                return typeList.Values;
            }
        }

        #region ICollection<IPersistenceObject> Members

        public void Add(IPersistenceObject item)
        {
            Type rootT = GetRootImplType(item);

            // create per-Type dictionary on-demand
            if (!_objects.ContainsKey(rootT))
                _objects[rootT] = new Dictionary<int, IPersistenceObject>();

            _objects[rootT][item.ID] = item;
        }

        public void Clear()
        {
            _objects.Clear();
        }

        public bool Contains(IPersistenceObject item)
        {
            Type rootT = GetRootImplType(item);
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
            if (Contains(item))
                // should always return true
                return _objects[GetRootImplType(item)].Remove(item.ID);
            else
                return false;
        }

        #endregion

        #region IEnumerable<IPersistenceObject> Members

        public IEnumerator<IPersistenceObject> GetEnumerator()
        {
            foreach (var typeList in _objects.Values)
            {
                foreach (IPersistenceObject obj in typeList.Values)
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
