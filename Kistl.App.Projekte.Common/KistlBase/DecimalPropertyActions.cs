namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DecimalPropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(DecimalProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Decimal";
        }
    }
}
