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
