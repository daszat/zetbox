using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Parties
{
    [Implementor]
    public static class CustomerRelationshipActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Parties.CustomerRelationship obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} from {1:d} thru {2:d}", obj.Customer, obj.From, obj.Thru);
        }
    }
}
