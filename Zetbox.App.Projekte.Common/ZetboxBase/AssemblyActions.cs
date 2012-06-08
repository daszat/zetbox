namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class AssemblyActions
    {
        [Invocation]
        public static void ToString(Assembly obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

    }
}
