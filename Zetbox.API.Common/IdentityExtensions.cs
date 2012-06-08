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

using Zetbox.API;
using Zetbox.App.Base;

namespace Zetbox.App.Extensions
{
    public static class IdentityExtensions
    {
        public static bool IsAdmininistrator(this Identity id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return id.Groups.Any(g => g.ExportGuid == Zetbox.NamedObjects.Base.Groups.Administrator.Guid);
        }

        public static bool IsInGroup(this Identity id, Group grp)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (grp == null) throw new ArgumentNullException("grp");
            return id.Groups.Any(g => g.ExportGuid == grp.ExportGuid);
        }
    }
}
