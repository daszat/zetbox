
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    /// <summary>
    /// Wraps 1:N Relation, which EF provides via a EntityCollection
    /// </summary>
    public class EntityCollectionWrapper<TInterface, TImpl> : ICollection<TInterface>, ICollection
        where TInterface : class, IDataObject
        where TImpl : class, System.Data.Objects.DataClasses.IEntityWithRelationships, TInterface, IDataObject
    {
        protected ICollection<TImpl> underlyingCollection;
        protected IKistlContext ctx;

        /// <summary>
        /// Initializes a new instance of the EntityCollectionWrapper class using the specified context and <see cref="EntityCollection{TImpl}"/>
        /// </summary>
        /// <param name="ctx">the parent data context (optional)</param>
        /// <param name="ec">the <see cref="EntityCollection{TImpl}"/> that should be wrapped</param>
        public EntityCollectionWrapper(IKistlContext ctx, ICollection<TImpl> ec)
        {
            if (ec == null) { throw new ArgumentNullException("ec"); }

            this.ctx = ctx;
            underlyingCollection = ec;
            if (ctx != null)
            {
                foreach (IPersistenceObject obj in underlyingCollection)
                {
                    obj.AttachToContext(ctx);
                }
            }
        }

        public virtual void Add(TInterface item)
        {
            if (item == null)
            {
                underlyingCollection.Add(null);
                return;
            }

            if (ctx != item.Context) { throw new WrongKistlContextException(); }

            underlyingCollection.Add((TImpl)item);
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
            if (array == null) { throw new ArgumentNullException("array"); }
            if (arrayIndex < 0) { throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex must be non-negative"); }
            if (arrayIndex >= array.Length)
            {
                var msg = String.Format("arrayIndex={0} must be less than array.Length={1}", arrayIndex, array.Length);
                throw new ArgumentOutOfRangeException("arrayIndex", msg);
            }

            if (arrayIndex + underlyingCollection.Count > array.Length)
            {
                var msg = String.Format("items do not fit idx={0} + #item={1} >= len={2}", arrayIndex, underlyingCollection.Count, array.Length);
                throw new ArgumentOutOfRangeException("arrayIndex", msg);
            }

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
        private readonly string _pointerProperty;
        private List<TImpl> _orderedItems;

        public EntityListWrapper(IKistlContext ctx, ICollection<TImpl> ec, string pointerProperty)
            : base(ctx, ec)
        {
            if (String.IsNullOrEmpty(pointerProperty)) { throw new ArgumentOutOfRangeException("pointerProperty"); }

            _pointerProperty = pointerProperty;
            ResetOrderedItems();
        }

        private void ResetOrderedItems()
        {
            _orderedItems = new List<TImpl>(underlyingCollection.OrderBy(item => GetIndexProperty(item) ?? Kistl.API.Helper.LASTINDEXPOSITION));
            FixIndices();
        }

        /// <summary>
        /// Assure that all indices are strictly monotonous rising (i.e. n.Index &lt; (n+1).Index) according to their order in <see cref="_orderedItems"/>.
        /// </summary>
        private void FixIndices()
        {
            int maxIdx = -1;
            for (int i = 0; i < _orderedItems.Count; i++)
            {
                var item = _orderedItems[i];
                int? idx = GetIndexProperty(item);
                if (!idx.HasValue || idx <= maxIdx || idx == Kistl.API.Helper.LASTINDEXPOSITION)
                {
                    // TODO: try to space out items better
                    idx = maxIdx + 1;
                    UpdateIndexProperty(item, idx);
                }
                maxIdx = idx.Value;
            }
        }

        #region Index Management
        protected void UpdateIndexProperty(TInterface item, int? index)
        {
            // Sets the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Sets Parameter.Method_pos
            item.SetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix, index);
        }

        protected int? GetIndexProperty(TInterface item)
        {
            return item.GetPropertyValue<int?>(_pointerProperty + Helper.PositionSuffix);
        }
        #endregion

        #region ICollection Overrides
        public override void Add(TInterface item)
        {
            base.Add(item);
            _orderedItems.Add((TImpl)item);
            UpdateIndexProperty(item, underlyingCollection.Count - 1);
        }

        public override void Clear()
        {
            foreach (TInterface i in underlyingCollection)
            {
                UpdateIndexProperty(i, null);
            }
            base.Clear();
            _orderedItems.Clear();
        }

        public override bool Remove(TInterface item)
        {
            var result = base.Remove(item);
            if (result)
            {
                _orderedItems.Remove((TImpl)item);
                UpdateIndexProperty(item, null);
            }
            return result;
        }

        protected override IEnumerable<TInterface> GetEnumerable()
        {
            return _orderedItems.Cast<TInterface>();
        }

        #endregion

        #region IList<INTERFACE> Members

        public int IndexOf(TInterface item)
        {
            return _orderedItems.IndexOf((TImpl)item);
        }

        public void Insert(int index, TInterface item)
        {
            // insert item without index and rely on FixIndices
            // to set the proper index and propagate changes
            UpdateIndexProperty(item, null);
            _orderedItems.Insert(index, (TImpl)item);
            FixIndices();
            underlyingCollection.Add((TImpl)item);
        }

        public void RemoveAt(int index)
        {
            TInterface item = _orderedItems[index];
            _orderedItems.RemoveAt(index);
            base.Remove(item);
        }

        public TInterface this[int index]
        {
            get
            {
                TInterface i = _orderedItems[index];
                return i;
            }
            set
            {
                TInterface i = _orderedItems[index];

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
