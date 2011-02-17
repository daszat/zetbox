namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class UniqueConstraintActions
    {
        [Invocation]
        public static void GetErrorText(Kistl.App.Base.UniqueConstraint obj, MethodReturnEventArgs<string> e, IDataObject constrainedObject)
        {
            e.Result = "";
        }

        [Invocation]
        public static void IsValid(Kistl.App.Base.UniqueConstraint obj, MethodReturnEventArgs<bool> e, Kistl.API.IDataObject constrainedObject)
        {
            e.Result = true; // enforced by database
        }
    }
}
