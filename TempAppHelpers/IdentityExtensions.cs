using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.Extensions
{
    public static class IdentityExtensions
    {
        public static IEnumerable<Group> GetGroups(this Identity id)
        {
            if (id == null) throw new ArgumentNullException("id");
            Dictionary<Group, Group> result = new Dictionary<Group, Group>();

            foreach (var g in id.Groups)
            {
                if (!result.ContainsKey(g))
                {
                    result[g] = g;
                    GetParentGroupsInternal(g, result);
                }
            }

            return result.Values;
        }

        // TODO: Replace this when NamedInstances are introduced 
        public static readonly Guid Groups_Adminstrator = new Guid("9C46F2B1-09D9-46B8-A7BF-812850921030");

        public static bool IsAdmininistrator(this Identity id)
        {
            if (id == null) throw new ArgumentNullException("id");
            return GetGroups(id).Count(g => g.ExportGuid == Groups_Adminstrator) > 0;
        }

        public static IEnumerable<Group> GetParentGroups(this Group g)
        {
            if (g == null) throw new ArgumentNullException("g");
            Dictionary<Group, Group> result = new Dictionary<Group, Group>();
            GetParentGroupsInternal(g, result);
            return result.Values;
        }

        private static void GetParentGroupsInternal(Group g, Dictionary<Group, Group> result)
        {
            if (g == null) throw new ArgumentNullException("g");
            if (result == null) throw new ArgumentNullException("result");
            foreach (var pg in g.ParentGroups)
            {
                if (!result.ContainsKey(pg))
                {
                    result[pg] = pg;
                    GetParentGroupsInternal(pg, result);
                }
            }
        }
    }
}
