namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ConstraintActions
    {
        [Invocation]
        public static void IsValid(
            Constraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            // the base constraint accepts all values
            e.Result = true;
        }
    }
}
