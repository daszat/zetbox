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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API
{
    public abstract class BaseRelationshipFilterCollection<T> : ICollection<T>
        where T : IRelationEntry
    {
        protected IZetboxContext ctx { get; private set; }
        protected IDataObject Parent { get; private set; }

        public BaseRelationshipFilterCollection(IZetboxContext ctx, IDataObject parent)
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
        public RelationshipFilterASideCollection(IZetboxContext ctx, IDataObject parent)
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
        public RelationshipFilterBSideCollection(IZetboxContext ctx, IDataObject parent)
            : base(ctx, parent)
        {
        }

        protected override IEnumerable<T> GetFilteredList()
        {
            return ctx.AttachedObjects.OfType<T>().Where(e => e.ObjectState != DataObjectState.Deleted && e.BObject == Parent);
        }
    }
}
