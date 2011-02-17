using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Parties
{
    [Implementor]
    public static class OrganizationActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Parties.Organization obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;
        }
    }
}
