namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class IsValidNamespaceConstraintActions
    {
        [Invocation]
        public static void IsValid(
                   IsValidIdentifierConstraint obj,
                   MethodReturnEventArgs<bool> e,
                   object constrainedObjectParam,
                   object constrainedValueParam)
        {
            if (constrainedValueParam != null)
            {
                string @namespace = (string)constrainedValueParam;

                e.Result = true;
                foreach (string ns in @namespace.Split('.'))
                {
                    e.Result &= System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(ns);
                }
            }
        }
    }
}
