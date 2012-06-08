namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class ModuleActions
    {
        [Invocation]
        public static void ToString(Module obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetName(Module obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "Base.Modules." + obj.Name;
        }
    }
}
