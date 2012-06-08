namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class AccessControlActions
    {
        [Invocation]
        public static void ToString(Zetbox.App.Base.AccessControl obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} ({1}) {2}",
                obj.Name ?? string.Empty,
                obj.Rights != null ? obj.Rights.ToString() :  "None",
                obj.Description ?? string.Empty);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

    }
}
