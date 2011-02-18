namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DoublePropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(DoubleProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }
    }
}
