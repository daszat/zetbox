namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ReadOnlyConstraintActions
    {
        [Invocation]
        public static void IsValid(Kistl.App.Base.ReadOnlyConstraint obj, MethodReturnEventArgs<System.Boolean> e, object constrainedObject, object constrainedValue)
        {
            e.Result = true; // always valid
        }
        [Invocation]
        public static void GetErrorText(Kistl.App.Base.ReadOnlyConstraint obj, MethodReturnEventArgs<System.String> e, object constrainedObject, object constrainedValue)
        {
            e.Result = string.Empty; // always valid
        }
    }
}
