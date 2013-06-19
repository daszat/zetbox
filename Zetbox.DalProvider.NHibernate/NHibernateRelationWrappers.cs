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

namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    public class NHibernateASideCollectionWrapper<TA, TB, TEntry>
        : ObservableASideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>, IEnumerable<IRelationEntry>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
    {
        public NHibernateASideCollectionWrapper(TB parentObject, ICollection<TEntry> baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            Collection.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            Collection.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        IEnumerator<IRelationEntry> IEnumerable<IRelationEntry>.GetEnumerator()
        {
            return Collection.Cast<IRelationEntry>().GetEnumerator();
        }
    }

    public class NHibernateBSideCollectionWrapper<TA, TB, TEntry>
        : ObservableBSideCollectionWrapper<TA, TB, TEntry, ICollection<TEntry>>, IEnumerable<IRelationEntry>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
    {
        public NHibernateBSideCollectionWrapper(TA parentObject, ICollection<TEntry> baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            Collection.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            Collection.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        IEnumerator<IRelationEntry> IEnumerable<IRelationEntry>.GetEnumerator()
        {
            return Collection.Cast<IRelationEntry>().GetEnumerator();
        }
    }


    public class NHibernateASideListWrapper<TA, TB, TEntry>
        : ObservableASideListWrapper<TA, TB, TEntry, ICollection<TEntry>>, IEnumerable<IRelationEntry>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
    {
        public NHibernateASideListWrapper(TB parentObject, ICollection<TEntry> baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            Collection.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            Collection.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        IEnumerator<IRelationEntry> IEnumerable<IRelationEntry>.GetEnumerator()
        {
            return Collection.Cast<IRelationEntry>().GetEnumerator();
        }

        public override TA this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                throw new InvalidOperationException("Case 2629: Using an index setter is currently not implemented in NHibernate");
            }
        }
    }

    public class NHibernateBSideListWrapper<TA, TB, TEntry>
        : ObservableBSideListWrapper<TA, TB, TEntry, ICollection<TEntry>>, IEnumerable<IRelationEntry>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
    {
        public NHibernateBSideListWrapper(TA parentObject, ICollection<TEntry> baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        public override void AddWithoutSetParent(TEntry item)
        {
            OnEntryAdding(item);
            Collection.Add(item);
            OnEntryAdded(item);
        }

        public override void RemoveWithoutClearParent(TEntry item)
        {
            OnEntryRemoving(item);
            Collection.Remove(item);
            OnEntryRemoved(item);
        }

        IEnumerator<IRelationEntry> IEnumerable<IRelationEntry>.GetEnumerator()
        {
            return Collection.Cast<IRelationEntry>().GetEnumerator();
        }

        public override TB this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                throw new InvalidOperationException("Case 2629: Using an index setter is currently not implemented in NHibernate");
            }
        }
    }
}
