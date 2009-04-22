using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.DALProvider.EF;

namespace Kistl.DALProvider.EF
{
    /// <summary>
    /// Wraps 1:N Relation, which EF provides via a EntityCollection
    /// </summary>
    /// <typeparam name="INTERFACE"></typeparam>
    /// <typeparam name="IMPL"></typeparam>
    public class EntityCollectionWrapper<INTERFACE, IMPL> : ICollection<INTERFACE>, ICollection
        where IMPL : class, System.Data.Objects.DataClasses.IEntityWithRelationships, INTERFACE, IDataObject
        where INTERFACE : class, IDataObject
    {
        protected EntityCollection<IMPL> underlyingCollection;

        public EntityCollectionWrapper(IKistlContext ctx, EntityCollection<IMPL> ec)
        {
            underlyingCollection = ec;
            foreach (IPersistenceObject obj in underlyingCollection)
            {
                obj.AttachToContext(ctx);
            }
        }

        public virtual void Add(INTERFACE item)
        {
            underlyingCollection.Add((IMPL)item);
        }

        public virtual void Clear()
        {
            underlyingCollection.Clear();
        }

        public virtual bool Contains(INTERFACE item)
        {
            return underlyingCollection.Contains((IMPL)item);
        }

        public virtual void CopyTo(INTERFACE[] array, int arrayIndex)
        {
            foreach (INTERFACE i in GetEnumerable())
            {
                array[arrayIndex++] = i;
            }
        }

        public virtual int Count
        {
            get { return underlyingCollection.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return underlyingCollection.IsReadOnly; }
        }

        public virtual bool Remove(INTERFACE item)
        {
            return underlyingCollection.Remove(item as IMPL);
        }

        public virtual IEnumerator<INTERFACE> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        protected virtual IEnumerable<INTERFACE> GetEnumerable()
        {
            return underlyingCollection.Cast<INTERFACE>();
        }

        #region ICollection Members

        public void CopyTo(Array array, int arrayIndex)
        {
            foreach (var i in GetEnumerable())
            {
                array.SetValue(i, arrayIndex++);
            }
        }

        public bool IsSynchronized { get { return false; } }

        public object SyncRoot { get { return this; } }

        #endregion
    }

    public class EntityListWrapper<INTERFACE, IMPL>
        : EntityCollectionWrapper<INTERFACE, IMPL>, IList<INTERFACE>
        where IMPL : class, System.Data.Objects.DataClasses.IEntityWithRelationships, INTERFACE, IDataObject
        where INTERFACE : class, IDataObject
    {
        private string _pointerProperty = "";

        public EntityListWrapper(IKistlContext ctx, EntityCollection<IMPL> ec, string pointerProperty)
            : base(ctx, ec)
        {
            _pointerProperty = pointerProperty;
        }

        #region Index Management
        protected void UpdateIndex(INTERFACE item, int? index)
        {
            // Sets the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Sets Parameter.Method__Position__
            item.SetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix, index);
        }

        protected int? GetIndex(INTERFACE item)
        {
            return item.GetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix);
        }

        protected INTERFACE GetAt(int index)
        {
            foreach (INTERFACE i in underlyingCollection)
            {
                int? idx = GetIndex(i);
                if (idx == null) continue;
                if (idx.Value == index)
                {
                    return i;
                }
            }
            return null;
        }
        #endregion

        #region ICollection Overrides
        public override void Add(INTERFACE item)
        {
            base.Add(item);
            UpdateIndex(item, underlyingCollection.Count - 1);
        }

        public override void Clear()
        {
            foreach (INTERFACE i in underlyingCollection)
            {
                UpdateIndex(i, null);
            }
            base.Clear();
        }

        public override bool Remove(INTERFACE item)
        {
            UpdateIndex(item, null);
            return base.Remove(item);
        }

        protected override IEnumerable<INTERFACE> GetEnumerable()
        {
            return base.GetEnumerable().OrderBy(i => GetIndex(i));
        }

        #endregion

        #region IList<INTERFACE> Members

        public int IndexOf(INTERFACE item)
        {
            int? result = GetIndex(item);
            if (result == null) throw new InvalidOperationException("Collection is not sorted");
            return result.Value;
        }

        public void Insert(int index, INTERFACE item)
        {
            UpdateIndex(item, index);
            // TODO: Optimize
            foreach (INTERFACE i in underlyingCollection)
            {
                int idx = GetIndex(i) ?? Kistl.API.Helper.LASTINDEXPOSITION;
                if (idx >= index)
                {
                    UpdateIndex(i, idx + 1);
                }
            }
            underlyingCollection.Add((IMPL)item);
        }

        public void RemoveAt(int index)
        {
            INTERFACE item = GetAt(index);
            if (item == null) throw new ArgumentOutOfRangeException(string.Format("Index {0} not found in collection", index));
            base.Remove(item);

            // TODO: Optimize
            foreach (INTERFACE i in underlyingCollection)
            {
                int idx = GetIndex(i) ?? Kistl.API.Helper.LASTINDEXPOSITION;
                if (idx >= index)
                {
                    UpdateIndex(i, idx - 1);
                }
            }
        }

        public INTERFACE this[int index]
        {
            get
            {
                INTERFACE i = GetAt(index);
                if (i == null) throw new ArgumentOutOfRangeException(string.Format("Index {0} not found in collection", index));
                return i;
            }
            set
            {
                INTERFACE i = GetAt(index);
                if (i == null) throw new ArgumentOutOfRangeException(string.Format("Index {0} not found in collection", index));

                if (i != value)
                {
                    RemoveAt(index);
                    Insert(index, value);
                }
            }
        }

        #endregion
    }

}
