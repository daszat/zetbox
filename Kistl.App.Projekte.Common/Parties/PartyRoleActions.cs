using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Parties
{
    [Implementor]
    public static class PartyRoleActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Parties.PartyRole obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} of {1} from {2:d} thru {3:d}", obj.Context.GetInterfaceType(obj).Type.Name, obj.Party, obj.From, obj.Thru);
        }
    }
}
