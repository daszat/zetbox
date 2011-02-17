namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class AccessControlActions
    {
        [Invocation]
        public static void ToString(Kistl.App.Base.AccessControl obj, MethodReturnEventArgs<string> e)
        {
            e.Result = (obj.Name ?? string.Empty) + " (" + (obj.Rights ?? AccessRights.None) + ") " + (obj.Description ?? string.Empty);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

    }
}
