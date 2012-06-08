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

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.API.Utils;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public static class RelationActions
    {
        [Invocation]
        public static void NotifyCreated(Relation obj)
        {
            obj.A = obj.Context.Create<RelationEnd>();
            obj.B = obj.Context.Create<RelationEnd>();
        }

        /// <summary>
        /// Workaround delete cascade troubles
        /// </summary>
        /// <param name="obj"></param>
        [Invocation]
        public static void NotifyDeleting(Relation obj)
        {
            var ctx = obj.Context;
            if (obj.A != null)
            {
                obj.A.Type = null;
                if (obj.A.Navigator != null) ctx.Delete(obj.A.Navigator);
                ctx.Delete(obj.A);
            }
            if (obj.B != null)
            {
                obj.B.Type = null;
                if (obj.B.Navigator != null) ctx.Delete(obj.B.Navigator);
                ctx.Delete(obj.B);
            }
        }
    }
}
