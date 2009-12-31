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
    public class EntityCollectionWrapper<TInterface, TImpl> : ICollection<TInterface>, ICollection
        where TImpl : class, System.Data.Objects.DataClasses.IEntityWithRelationships, TInterface, IDataObject
        where TInterface : class, IDataObject
    {
        protected EntityCollection<TImpl> underlyingCollection;
        protected IKistlContext ctx;

        public EntityCollectionWrapper(IKistlContext ctx, EntityCollection<TImpl> ec)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (ec == null) { throw new ArgumentNullException("ec"); }

            this.ctx = ctx;
            underlyingCollection = ec;
            foreach (IPersistenceObject obj in underlyingCollection)
            {
                obj.AttachToContext(ctx);
            }
        }

        public virtual void Add(TInterface item)
        {
            if (item == null)
            {
                underlyingCollection.Add(null);
                return;
            }
            else
            {
                if (ctx != item.Context) { throw new WrongKistlContextException(); }

                underlyingCollection.Add((TImpl)item);
            }
        }

        public virtual void Clear()
        {
            underlyingCollection.Clear();
        }

        public virtual bool Contains(TInterface item)
        {
            if (item == null)
            {
                return underlyingCollection.Contains(null);
            }

            if (ctx != item.Context) { throw new WrongKistlContextException(); }

            return underlyingCollection.Contains((TImpl)item);
        }

        public virtual void CopyTo(TInterface[] array, int arrayIndex)
        {
            foreach (TInterface i in GetEnumerable())
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

        public virtual bool Remove(TInterface item)
        {
            if (item == null)
            {
                return underlyingCollection.Remove(null);
            }

            if (ctx != item.Context) { throw new WrongKistlContextException(); }

            return underlyingCollection.Remove((TImpl)item);
        }

        public virtual IEnumerator<TInterface> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        protected virtual IEnumerable<TInterface> GetEnumerable()
        {
            return underlyingCollection.Cast<TInterface>();
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

    public class EntityListWrapper<TInterface, TImpl>
        : EntityCollectionWrapper<TInterface, TImpl>, IList<TInterface>
        where TImpl : class, System.Data.Objects.DataClasses.IEntityWithRelationships, TInterface, IDataObject
        where TInterface : class, IDataObject
    {
        private string _pointerProperty = "";

        public EntityListWrapper(IKistlContext ctx, EntityCollection<TImpl> ec, string pointerProperty)
            : base(ctx, ec)
        {
            _pointerProperty = pointerProperty;
        }

        #region Index Management
        protected void UpdateIndex(TInterface item, int? index)
        {
            // Sets the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Sets Parameter.Method__Position__
            item.SetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix, index);
        }

        protected int? GetIndex(TInterface item)
        {
            return item.GetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix);
        }

        protected TInterface GetAt(int index)
        {
            foreach (TInterface i in underlyingCollection)
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
        public override void Add(TInterface item)
        {
            base.Add(item);
            UpdateIndex(item, underlyingCollection.Count - 1);
        }

        public override void Clear()
        {
            foreach (TInterface i in underlyingCollection)
            {
                UpdateIndex(i, null);
            }
            base.Clear();
        }

        public override bool Remove(TInterface item)
        {
            UpdateIndex(item, null);
            return base.Remove(item);
        }

        protected override IEnumerable<TInterface> GetEnumerable()
        {
            return base.GetEnumerable().OrderBy(i => GetIndex(i));
        }

        #endregion

        #region IList<INTERFACE> Members

        public int IndexOf(TInterface item)
        {
            int? result = GetIndex(item);
            if (result == null) throw new InvalidOperationException("Collection is not sorted");
            return result.Value;
        }

        public void Insert(int index, TInterface item)
        {
            UpdateIndex(item, index);
            // TODO: Optimize
            foreach (TInterface i in underlyingCollection)
            {
                int idx = GetIndex(i) ?? Kistl.API.Helper.LASTINDEXPOSITION;
                if (idx >= index)
                {
                    UpdateIndex(i, idx + 1);
                }
            }
            underlyingCollection.Add((TImpl)item);
        }

        public void RemoveAt(int index)
        {
            TInterface item = GetAt(index);
            if (item == null) throw new ArgumentOutOfRangeException(string.Format("Index {0} not found in collection", index));
            base.Remove(item);

            // TODO: Optimize
            foreach (TInterface i in underlyingCollection)
            {
                int idx = GetIndex(i) ?? Kistl.API.Helper.LASTINDEXPOSITION;
                if (idx >= index)
                {
                    UpdateIndex(i, idx - 1);
                }
            }
        }

        public TInterface this[int index]
        {
            get
            {
                TInterface i = GetAt(index);
                if (i == null) throw new ArgumentOutOfRangeException(string.Format("Index {0} not found in collection", index));
                return i;
            }
            set
            {
                TInterface i = GetAt(index);
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
