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

namespace Zetbox.Generator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public static class ContextExtensions
    {
        public static IQueryable<ObjectClass> GetBaseClasses(this IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass == null);
        }

        public static IQueryable<ObjectClass> GetDerivedClasses(this IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass != null);
        }

        public static IEnumerable<Relation> GetRelationsWithSeparateStorage(this IZetboxContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => r.Storage == StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }

        public static IEnumerable<Relation> GetRelationsWithoutSeparateStorage(this IZetboxContext ctx)
        {
            return ctx.GetQuery<Relation>()
                .Where(r => r.Storage != StorageType.Separate)
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }
    }
}
