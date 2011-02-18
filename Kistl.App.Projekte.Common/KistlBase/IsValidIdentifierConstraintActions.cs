namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class IsValidIdentifierConstraintActions
    {
        [Invocation]
        public static void ToString(IsValidIdentifierConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "Method names, property names, enum names etc. must be valid names.";
        }

        [Invocation]
        public static void IsValid(
                   IsValidIdentifierConstraint obj,
                   MethodReturnEventArgs<bool> e,
                   object constrainedObjectParam,
                   object constrainedValueParam)
        {
            e.Result = (constrainedValueParam != null) &&
                System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier((string)constrainedValueParam);
        }

        [Invocation]
        public static void GetErrorText(
            IsValidIdentifierConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = string.Format("'{0}' is not a valid identifier", constrainedValueParam);
        }
    }
}
