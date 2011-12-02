namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class GroupActions
    {
        [Invocation]
        public static void ToString(Group obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetName(Group kind, MethodReturnEventArgs<string> e)
        {
            e.Result = "Base.Groups." + kind.Name;
        }
    }
}
