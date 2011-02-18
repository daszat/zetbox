namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Extensions;

    [Implementor]
    public static class MethodInvocationConstraintActions
    {
        [Invocation]
        public static void ToString(MethodInvocationConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "MethodIvocation's Method.ObjectClass should be assignable from InvokeOnObjectClass";
        }

        [Invocation]
        public static void IsValid(
            MethodInvocationConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            var constrainedObject = (MethodInvocation)constrainedObjectParam;
            var method = (Method)constrainedValueParam;
            e.Result = (method != null) && method.ObjectClass.IsAssignableFrom(constrainedObject.InvokeOnObjectClass);
        }


        [Invocation]
        public static void GetErrorText(
            MethodInvocationConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            var constrainedObject = (MethodInvocation)constrainedObjectParam;
            var method = (Method)constrainedValueParam;
            e.Result = String.Format("This Invocation's  InvokeOnObjectClass ('{1}') should be a descendent of (or equal to) the Method's class ('{0}'), but isn't",
                (method != null) ? method.ObjectClass.ToString() : "(no Method yet)",
                constrainedObject.InvokeOnObjectClass != null ? constrainedObject.InvokeOnObjectClass.ToString() : "(no ObjectClass yet)");
        }
    }
}
