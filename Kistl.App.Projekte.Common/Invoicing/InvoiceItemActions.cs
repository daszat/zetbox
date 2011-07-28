using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace ZBox.Basic.Invoicing
{
    [Implementor]
    public static class InvoiceItemActions
    {
        [Invocation]
        public static void ToString(ZBox.Basic.Invoicing.InvoiceItem obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} {1}, {2}", obj.Quantity, obj.Amount, obj.Description);
        }
    }
}
