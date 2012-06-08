using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public abstract class BaseRelationshipFilterCollection<T> : ICollection<T>
        where T : IRelationEntry
    {
        protected IKistlContext ctx { get; private set; }
        protected IDataObject Parent { get; private set; }

        public BaseRelationshipFilterCollection(IKistlContext ctx, IDataObject parent)
        {
            this.ctx = ctx;
            this.Parent = parent;
        }

        protected abstract IEnumerable<T> GetFilteredList();

        #region ICollection<T> Members

        public void Add(T item)
        {
            // Do nothing
        }

        public void Clear()
        {
            // Do nothing
        }

        public bool Contains(T item)
        {
            return GetFilteredList().Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var i in GetFilteredList())
            {
                array[arrayIndex++] = i;
            }
        }

        public int Count { get { return GetFilteredList().Count(); } }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            // Do Nothing
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return GetFilteredList().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetFilteredList().GetEnumerator();
        }

        #endregion
    }

    public class RelationshipFilterASideCollection<T> : BaseRelationshipFilterCollection<T>
        where T : IRelationEntry
    {
        public RelationshipFilterASideCollection(IKistlContext ctx, IDataObject parent)
            : base(ctx, parent)
        {
        }

        protected override IEnumerable<T> GetFilteredList()
        {
            return ctx.AttachedObjects.OfType<T>().Where(e => e.ObjectState != DataObjectState.Deleted && e.AObject == Parent);
        }
    }

    public class RelationshipFilterBSideCollection<T> : BaseRelationshipFilterCollection<T>
        where T : IRelationEntry
    {
        public RelationshipFilterBSideCollection(IKistlContext ctx, IDataObject parent)
            : base(ctx, parent)
        {
        }

        protected override IEnumerable<T> GetFilteredList()
        {
            return ctx.AttachedObjects.OfType<T>().Where(e => e.ObjectState != DataObjectState.Deleted && e.BObject == Parent);
        }
    }
}
