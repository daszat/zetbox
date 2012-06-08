namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ViewReadOnlyConstraintActions
    {
        [Invocation]
        public static void ToString(ViewReadOnlyConstraint obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = "Item is readonly in view but changable on the server/client";
        }
    }
}
