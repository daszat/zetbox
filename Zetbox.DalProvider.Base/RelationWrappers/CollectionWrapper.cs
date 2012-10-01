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

    public class ASideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseCollectionWrapper<TA, TB, TB, TA, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public ASideCollectionWrapper(TB parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TA> GetItems()
        {
            return Collection.Select(e => e.A);
        }

        protected override TA ItemFromEntry(TEntry entry)
        {
            return entry.A;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TA item)
        {
            entry.B = ParentObject;
            entry.A = item;
            return entry;
        }
    }

    public class BSideCollectionWrapper<TA, TB, TEntry, TBaseCollection>
        : BaseCollectionWrapper<TA, TB, TA, TB, TEntry, TBaseCollection>
        where TA : class, IDataObject
        where TB : class, IDataObject
        where TEntry : class, IRelationEntry<TA, TB>
        where TBaseCollection : class, ICollection<TEntry>
    {
        public BSideCollectionWrapper(TA parentObject, TBaseCollection baseCollection)
            : base(parentObject, baseCollection)
        {
        }

        protected override IEnumerable<TB> GetItems()
        {
            return Collection.Select(e => e.B);
        }

        protected override TB ItemFromEntry(TEntry entry)
        {
            return entry.B;
        }

        protected override TEntry InitialiseEntry(TEntry entry, TB item)
        {
            entry.A = ParentObject;
            entry.B = item;
            return entry;
        }
    }
}