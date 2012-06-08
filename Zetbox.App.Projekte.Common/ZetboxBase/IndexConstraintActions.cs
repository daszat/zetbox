namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class IndexConstraintActions
    {
        [Invocation]
        public static void GetErrorText(Kistl.App.Base.IndexConstraint obj, MethodReturnEventArgs<string> e, IDataObject constrainedObject)
        {
            e.Result = "";
        }

        [Invocation]
        public static void IsValid(Kistl.App.Base.IndexConstraint obj, MethodReturnEventArgs<bool> e, Kistl.API.IDataObject constrainedObject)
        {
            e.Result = true; // enforced by database
        }
    }
}
