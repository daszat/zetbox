using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.Extensions
{
    public static class IdentityExtensions
    {
        // TODO: Replace this when NamedInstances are introduced 
        public static readonly Guid Groups_Adminstrator = new Guid("9C46F2B1-09D9-46B8-A7BF-812850921030");

        public static bool IsAdmininistrator(this Identity id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return id.Groups.Count(g => g.ExportGuid == Groups_Adminstrator) > 0;
        }
    }
}
