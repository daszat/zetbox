namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DateTimePropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(DateTimeProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }
    }
}
