namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class IndexConstraintActions
    {
        [Invocation]
        public static void GetErrorText(Zetbox.App.Base.IndexConstraint obj, MethodReturnEventArgs<string> e, IDataObject constrainedObject)
        {
            e.Result = "";
        }

        [Invocation]
        public static void IsValid(Zetbox.App.Base.IndexConstraint obj, MethodReturnEventArgs<bool> e, Zetbox.API.IDataObject constrainedObject)
        {
            e.Result = true; // enforced by database
        }
    }
}
