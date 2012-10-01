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

namespace Zetbox.DalProvider.Base.RelationWrappers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    public class ASideListWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseListWrapper<TA, TB, TB, TA, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public ASideListWrapper(TB parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TA> GetItems()
        {
            return Collection.Select(e => e.A);
        }

        protected override IEnumerable<TEntry> GetSortedEntries()
        {
            return Collection.OrderBy(e => e.AIndex);
        }

        protected override TA ItemFromEntry(TEntry entry)
        {
            return entry.A;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TA item)
        {
            entry.B = ParentObject;
            entry.A = item;
            entry.AIndex = Zetbox.API.Helper.LASTINDEXPOSITION;
            entry.BIndex = Zetbox.API.Helper.LASTINDEXPOSITION;
            return entry;
        }

        /// <summary>
        /// Overriden to set the index on the incoming entry
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            if (!entry.AIndex.HasValue || entry.AIndex == Zetbox.API.Helper.LASTINDEXPOSITION)
            {
                Zetbox.API.Helper.FixIndices(GetSortedEntries().ToList(), IndexFromEntry, SetIndex);
            }
        }

        protected override int? IndexFromEntry(TEntry entry)
        {
            return entry.AIndex;
        }

        protected override void SetIndex(TEntry entry, int idx)
        {
            entry.AIndex = idx;
        }

        protected override void SetItem(TEntry entry, TA item)
        {
            entry.A = item;
        }
    }

    public class BSideListWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseListWrapper<TA, TB, TA, TB, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationListEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public BSideListWrapper(TA parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TB> GetItems()
        {
            return Collection.Select(e => e.B);
        }

        protected override IEnumerable<TEntry> GetSortedEntries()
        {
            return Collection.OrderBy(e => e.BIndex);
        }

        protected override TB ItemFromEntry(TEntry entry)
        {
            return entry.B;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TB item)
        {
            entry.A = ParentObject;
            entry.B = item;
            entry.AIndex = Zetbox.API.Helper.LASTINDEXPOSITION;
            entry.BIndex = Zetbox.API.Helper.LASTINDEXPOSITION;
            return entry;
        }

        /// <summary>
        /// Overriden to set the index on the incoming entry
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnEntryAdded(TEntry entry)
        {
            base.OnEntryAdded(entry);
            if (!entry.BIndex.HasValue || entry.BIndex == Zetbox.API.Helper.LASTINDEXPOSITION)
            {
                Zetbox.API.Helper.FixIndices(GetSortedEntries().ToList(), IndexFromEntry, SetIndex);
            }
        }

        protected override int? IndexFromEntry(TEntry entry)
        {
            return entry.BIndex;
        }

        protected override void SetIndex(TEntry entry, int idx)
        {
            entry.BIndex = idx;
        }

        protected override void SetItem(TEntry entry, TB item)
        {
            entry.B = item;
        }
    }
}