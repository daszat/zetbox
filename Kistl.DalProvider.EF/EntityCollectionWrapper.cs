
namespace Kistl.DalProvider.Ef
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
        where TImpl : class, IEntityWithRelationships, TInterface, IDataObject
    {
        protected readonly EntityCollection<TImpl> underlyingCollection;
        protected readonly IKistlContext ctx;
        private readonly Action _changingNotifier;
        private readonly Action _changedNotifier;

        private readonly Action<TImpl> _itemChangingNotifier;
        private readonly Action<TImpl> _itemChangedNotifier;

        public EntityCollectionWrapper(IKistlContext ctx, EntityCollection<TImpl> ec, Action changingNotifier, Action changedNotifier)
            : this(ctx, ec, changingNotifier, changedNotifier, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EntityCollectionWrapper class using the specified context and <see cref="EntityCollection{TImpl}"/>
        /// </summary>
        /// <param name="ctx">the parent data context (optional)</param>
        /// <param name="ec">the <see cref="EntityCollection{TImpl}"/> that should be wrapped</param>
        /// <param name="changingNotifier">an action called to notify the owner of this collection before a change happens</param>
        /// <param name="changedNotifier">an action called to notify the owner of this collection after a change has happened</param>
        /// <param name="itemChangingNotifier">an action called to notify the modified item before a change happens</param>
        /// <param name="itemChangedNotifier">an action called to notify the modified item after a change has happened</param>
        public EntityCollectionWrapper(IKistlContext ctx, EntityCollection<TImpl> ec, Action changingNotifier, Action changedNotifier, Action<TImpl> itemChangingNotifier, Action<TImpl> itemChangedNotifier)
        {
            // TODO: when can the ctx be legally null? 
            // if (ctx == null) { throw new ArgumentNullException("ctx"); }

            if (ec == null) { throw new ArgumentNullException("ec"); }

            this.ctx = ctx;
            underlyingCollection = ec;
            _changingNotifier = changingNotifier;
            _changedNotifier = changedNotifier;
            _itemChangingNotifier = itemChangingNotifier;
            _itemChangedNotifier = itemChangedNotifier;

            // TODO: remove "if", see above
            if (ctx != null)
            {
                foreach (IPersistenceObject obj in underlyingCollection)
                {
                    obj.AttachToContext(ctx);
                }
            }
        }

        protected void NotifyOwnerChanging()
        {
            if (_changingNotifier != null)
                _changingNotifier();
        }

        protected void NotifyOwnerChanged()
        {
            if (_changedNotifier != null)
                _changedNotifier();
        }

        protected void NotifyItemChanging(TImpl item)
        {
            if (_itemChangingNotifier != null)
                _itemChangingNotifier(item);
        }

        protected void NotifyItemChanged(TImpl item)
        {
            if (_itemChangedNotifier != null)
                _itemChangedNotifier(item);
        }

        public virtual void Add(TInterface item)
        {
            if (item == null)
            {
                underlyingCollection.Add(null);
                return;
            }

            if (ctx != item.Context) { throw new WrongKistlContextException(); }

            var impl = item as TImpl;

            if (impl == null) { throw new ArgumentOutOfRangeException("item", String.Format("item is of wrong type: [{0}] instead of [{1}]", item.GetType().AssemblyQualifiedName, typeof(TImpl).AssemblyQualifiedName)); }

            NotifyOwnerChanging();
            NotifyItemChanging(impl);
            underlyingCollection.Add(impl);
            NotifyItemChanged(impl);
            NotifyOwnerChanged();
        }

        public virtual void Clear()
        {
            // copy list to keep reference to items after Clear()
            var items = underlyingCollection.ToArray();
            NotifyOwnerChanging();
            items.ForEach(i => NotifyItemChanging(i));
            underlyingCollection.Clear();
            items.ForEach(i => NotifyItemChanged(i));
            NotifyOwnerChanged();
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

            if (underlyingCollection.Count > 0 && arrayIndex >= array.Length)
            {
                var msg = String.Format("arrayIndex={0} must be less than array.Length={1}", arrayIndex, array.Length);
                throw new ArgumentException(msg, "arrayIndex");
            }

            if (arrayIndex + underlyingCollection.Count > array.Length)
            {
                var msg = String.Format("items do not fit idx={0} + #item={1} > len={2}", arrayIndex, underlyingCollection.Count, array.Length);
                throw new ArgumentException(msg, "arrayIndex");
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
                NotifyOwnerChanging();
                var nullResult = underlyingCollection.Remove(null);
                NotifyOwnerChanged();
                return nullResult;
            }

            if (ctx != item.Context) { throw new WrongKistlContextException(); }

            var impl = item as TImpl;
            if (impl == null) { throw new ArgumentOutOfRangeException("item", String.Format("item is of wrong type: [{0}] instead of [{1}]", item.GetType().AssemblyQualifiedName, typeof(TImpl).AssemblyQualifiedName)); }

            NotifyOwnerChanging();
            NotifyItemChanging(impl);
            var result = underlyingCollection.Remove(impl);
            NotifyItemChanged(impl);
            NotifyOwnerChanged();
            return result;
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

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null) { throw new ArgumentNullException("array"); }
            if (!array.GetType().GetElementType().IsAssignableFrom(typeof(TImpl)))
            {
                var msg = String.Format("Mismatch between source and destination type: [{0}] not assignable from [{1}]", array.GetType().GetElementType(), typeof(TImpl));
                throw new ArgumentException(msg, "array");
            }

            var items = GetEnumerable().ToList();
            ((ICollection)items).CopyTo(array, arrayIndex);
        }

        public bool IsSynchronized { get { return false; } }

        public object SyncRoot { get { return this; } }

        #endregion
    }

    public class EntityListWrapper<TInterface, TImpl>
        : EntityCollectionWrapper<TInterface, TImpl>, IList<TInterface>, IList
        where TImpl : class, IEntityWithRelationships, TInterface, IDataObject
        where TInterface : class, IDataObject
    {
        private readonly string _pointerProperty;
        private readonly string _positionProperty;
        private List<TImpl> _orderedItems;

        public EntityListWrapper(IKistlContext ctx, EntityCollection<TImpl> ec, Action changingNotifier, Action changedNotifier, string pointerProperty, string positionProperty)
            : this(ctx, ec, changingNotifier, changedNotifier, null, null, pointerProperty, positionProperty)
        {
        }

        public EntityListWrapper(IKistlContext ctx, EntityCollection<TImpl> ec, Action changingNotifier, Action changedNotifier, Action<TImpl> itemChangingNotifier, Action<TImpl> itemChangedNotifier, string pointerProperty, string positionProperty)
            : base(ctx, ec, changingNotifier, changedNotifier, itemChangingNotifier, itemChangedNotifier)
        {
            if (String.IsNullOrEmpty(pointerProperty)) { throw new ArgumentOutOfRangeException("pointerProperty"); }
            if (String.IsNullOrEmpty(positionProperty)) { throw new ArgumentOutOfRangeException("positionProperty"); }

            _pointerProperty = pointerProperty;
            _positionProperty = positionProperty;
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
            Kistl.API.Helper.FixIndices<TImpl>(_orderedItems, GetIndexProperty, (item, i) => UpdateIndexProperty(item, i));
        }

        #region Index Management

        protected void UpdateIndexProperty(TInterface item, int? index)
        {
            // Sets the position Property for a 1:n Relation
            // eg. Method 1-n Parameter
            // Sets Parameter.Method_pos
            item.SetPropertyValue<int?>(_positionProperty, index);
        }

        protected int? GetIndexProperty(TInterface item)
        {
            return item.GetPropertyValue<int?>(_positionProperty);
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
            if (item == null)
            {
                NotifyOwnerChanging();
                _orderedItems.Insert(index, null);
                FixIndices();
                underlyingCollection.Add(null);
                NotifyOwnerChanged();
                return;
            }

            var impl = item as TImpl;
            if (impl == null) { throw new ArgumentOutOfRangeException("item", String.Format("item is of wrong type: [{0}] instead of [{1}]", item.GetType().AssemblyQualifiedName, typeof(TImpl).AssemblyQualifiedName)); }

            NotifyOwnerChanging();
            NotifyItemChanging(impl);

            // insert item without index and rely on FixIndices
            // to set the proper index and propagate changes
            UpdateIndexProperty(item, null);
            _orderedItems.Insert(index, impl);
            FixIndices();
            underlyingCollection.Add(impl);

            NotifyItemChanged(impl);
            NotifyOwnerChanged();
        }

        public void RemoveAt(int index)
        {
            TInterface item = _orderedItems[index];
            _orderedItems.RemoveAt(index);
            UpdateIndexProperty(item, null);
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

        #region IList Members

        int IList.Add(object value)
        {
            Add((TImpl)value);
            return this.Count - 1;
        }

        bool IList.Contains(object value)
        {
            var t = value as TImpl;
            if (t == null)
            {
                return false;
            }
            else
            {
                return this.Contains(t);
            }
        }

        int IList.IndexOf(object value)
        {
            var t = value as TImpl;
            if (t == null)
            {
                return -1;
            }
            else
            {
                return this.IndexOf(t);
            }
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (TImpl)value);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            var t = value as TImpl;
            if (t != null)
            {
                this.Remove(t);
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (TImpl)value;
            }
        }

        #endregion
    }
}
