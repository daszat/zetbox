using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Parties
{
    [Implementor]
    public static class EmploymentActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Parties.Employment obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("Employment of {0} from {1:d} thru {2:d}", obj.Employee.Party, obj.From, obj.Thru);
        }
    }
}
