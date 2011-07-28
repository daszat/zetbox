using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class AdjustmentTypeActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Invoicing.AdjustmentType obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Description;
        }
    }
}
