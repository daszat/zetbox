namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class InitOnlyConstraintActions
    {
        [Invocation]
        public static void ToString(Kistl.App.Base.InitOnlyConstraint obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = "The item can only be set during initialization";
        }
    }
}
