namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class CLRObjectParameterActions
    {
        [Invocation]
        public static void GetParameterTypeString(Kistl.App.Base.CLRObjectParameter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Type != null ? obj.Type.FullName : string.Empty;
        }
    }
}
