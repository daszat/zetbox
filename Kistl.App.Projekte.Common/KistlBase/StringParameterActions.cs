namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class StringParameterActions
    {
        [Invocation]
        public static void GetParameterTypeString(StringParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

    }
}
