namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class IntPropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(IntProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }
    }
}
