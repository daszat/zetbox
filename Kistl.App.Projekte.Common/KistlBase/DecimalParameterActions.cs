namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DecimalParameterActions
    {
        [Invocation]
        public static void GetParameterTypeString(Kistl.App.Base.DecimalParameter obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = "System.Decimal";
        }

    }
}
