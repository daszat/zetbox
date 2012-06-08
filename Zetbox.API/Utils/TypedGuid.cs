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

namespace Zetbox.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public struct TypedGuid<T>
        where T : class, IPersistenceObject
    {
        public TypedGuid(string guidString)
            : this(new Guid(guidString))
        {
        }

        public TypedGuid(Guid guid)
        {
            this._guid = guid;
        }

        private readonly Guid _guid;
        public Guid Guid { get { return _guid; } }

        public T Find(IReadOnlyZetboxContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            return ctx.FindPersistenceObject<T>(_guid);
        }
    }


    public struct TypedGuidList<T>
        where T : class, IPersistenceObject
    {
        public TypedGuidList(params string[] guidStrings)
            : this(guidStrings.Select(s => new Guid(s)).ToArray())
        {
        }

        public TypedGuidList(params Guid[] guid)
        {
            this._guid = guid;
        }

        private readonly Guid[] _guid;
        public IEnumerable<Guid> Guid { get { return _guid; } }

        public IEnumerable<T> Find(IReadOnlyZetboxContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            return ctx.FindPersistenceObjects<T>(_guid);
        }
    }
}
