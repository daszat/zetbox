using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Parties
{
    [Implementor]
    public static class CustomerActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Parties.Customer obj, MethodReturnEventArgs<System.String> e)
        {
            // Base is good enougth
            // e.Result = string.Format("Employee of {0} from {1:d} thru {2:d}", obj.Party, obj.From, obj.Thru);
        }
    }
}
