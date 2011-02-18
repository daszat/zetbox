namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class IntParameterActions
    {
        [Invocation]
        public static void GetParameterTypeString(Kistl.App.Base.IntParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

    }
}
