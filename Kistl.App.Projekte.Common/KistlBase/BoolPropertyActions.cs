namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class BoolPropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(BoolProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }
    }
}
