using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Collections;

namespace Kistl.API.Server
{
    public class EntityCollectionWrapper<INTERFACE, IMPL> : ICollection<INTERFACE> 
        where IMPL : class, System.Data.Objects.DataClasses.IEntityWithRelationships, INTERFACE, IDataObject
    {
        private EntityCollection<IMPL> _ec;

        public EntityCollectionWrapper(EntityCollection<IMPL> ec)
        {
            _ec = ec;
        }

        public void Add(INTERFACE item)
        {
            _ec.Add((IMPL)item);
        }

        public void Clear()
        {
            _ec.Clear();
        }

        public bool Contains(INTERFACE item)
        {
            return _ec.Contains((IMPL)item);
        }

        public void CopyTo(INTERFACE[] array, int arrayIndex)
        {
            foreach (INTERFACE i in _ec)
            {
                array[arrayIndex++] = i;
            }
        }

        public int Count
        {
            get { return _ec.Count; }
        }

        public bool IsReadOnly
        {
            get { return _ec.IsReadOnly; }
        }

        public bool Remove(INTERFACE item)
        {
            return _ec.Remove(item as IMPL);
        }


        public IEnumerator<INTERFACE> GetEnumerator()
        {
            return ((IEnumerable)_ec).Cast<INTERFACE>().GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_ec).Cast<INTERFACE>().GetEnumerator();
        }
    }

    public class EntityCollectionEntryValueWrapper<PARENT, VALUE, IMPL> : IList<VALUE>
        where IMPL : class, System.Data.Objects.DataClasses.IEntityWithRelationships, ICollectionEntry<VALUE, PARENT>, new()
        where PARENT : IDataObject
    {
        private EntityCollection<IMPL> _ec;
        private PARENT _parentObject;

        public EntityCollectionEntryValueWrapper(PARENT parentObject, EntityCollection<IMPL> ec)
        {
            _ec = ec;
            _parentObject = parentObject;
        }

        #region IList<VALUE> Members

        public int IndexOf(VALUE item)
        {
            int idx = 0;
            foreach (IMPL v in _ec)
            {
                if (object.Equals(v.Value, item)) return idx;
                idx++;
            }
            return -1;
        }

        public void Insert(int index, VALUE item)
        {
            // TODO: Not ready yet
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            _ec.Remove(_ec.Skip(index).Single());
        }

        public VALUE this[int index]
        {
            // TODO: Not ready yet
            get
            {
                return _ec.Skip(index).Single().Value;
            }
            set
            {
                _ec.Skip(index).Single().Value = value;
            }
        }

        #endregion

        #region ICollection<VALUE> Members

        public void Add(VALUE item)
        {
            IMPL entry = new IMPL();
            entry.Parent = _parentObject;
            entry.Value = item;
            _ec.Add(entry);
        }

        public void Clear()
        {
            _ec.Clear();
        }

        public bool Contains(VALUE item)
        {
            return _ec.Select(e => e.Value).Contains(item);
        }

        public void CopyTo(VALUE[] array, int arrayIndex)
        {
            foreach (var i in _ec.Select(i => i.Value))
            {
                array[arrayIndex++] = i;
            }
        }

        public int Count
        {
            get { return _ec.Count; }
        }

        public bool IsReadOnly
        {
            get { return _ec.IsReadOnly; }
        }

        public bool Remove(VALUE item)
        {
            IMPL e = _ec.Single(i => i.Value.Equals(item));
            return _ec.Remove(e);
        }

        #endregion

        #region IEnumerable<VALUE> Members

        public IEnumerator<VALUE> GetEnumerator()
        {
            return _ec.Select(i => i.Value).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _ec.Select(i => i.Value).GetEnumerator();
        }

        #endregion
    }

    public class EntityCollectionEntryParentWrapper<PARENT, VALUE, IMPL> : IList<PARENT>
        where IMPL : class, System.Data.Objects.DataClasses.IEntityWithRelationships, ICollectionEntry<VALUE, PARENT>, new()
        where PARENT : IDataObject
    {
        private EntityCollection<IMPL> _ec;
        private VALUE _parentObject;

        public EntityCollectionEntryParentWrapper(VALUE parentObject, EntityCollection<IMPL> ec)
        {
            _ec = ec;
            _parentObject = parentObject;
        }

        #region IList<PARENT> Members

        public int IndexOf(PARENT item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, PARENT item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public PARENT this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<PARENT> Members

        public void Add(PARENT item)
        {
            IMPL entry = new IMPL();
            entry.Value = _parentObject;
            entry.Parent = item;
            _ec.Add(entry);
        }

        public void Clear()
        {
            _ec.Clear();
        }

        public bool Contains(PARENT item)
        {
            return _ec.Select(e => e.Parent).Contains(item);
        }

        public void CopyTo(PARENT[] array, int arrayIndex)
        {
            foreach(var i in _ec.Select(i => i.Parent))
            {
                array[arrayIndex++] = i;
            }
        }

        public int Count
        {
            get { return _ec.Count; }
        }

        public bool IsReadOnly
        {
            get { return _ec.IsReadOnly; }
        }

        public bool Remove(PARENT item)
        {
            IMPL e = _ec.Single(i => i.Parent.Equals(item));
            return _ec.Remove(e);
        }

        #endregion

        #region IEnumerable<PARENT> Members

        public IEnumerator<PARENT> GetEnumerator()
        {
            return _ec.Select(i => i.Parent).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _ec.Select(i => i.Parent).GetEnumerator();
        }

        #endregion
    }
}
