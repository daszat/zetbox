namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class ClientReadOnlyConstraintActions
    {
        [Invocation]
        public static void ToString(Zetbox.App.Base.ClientReadOnlyConstraint obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = "Item is readonly on client but changable on the server";
        }
    }
}
