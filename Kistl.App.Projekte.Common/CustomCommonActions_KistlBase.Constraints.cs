using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using System.Diagnostics;

namespace Kistl.App.Base
{
    public static partial class CustomCommonActions_KistlBase
    {
        #region ToString
        public static void OnToString_NotNullableConstraint(NotNullableConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (obj.ConstrainedProperty == null)
            {
                e.Result = String.Format("The ConstrainedProperty should not be NULL");
            }
            else
            {
                e.Result = String.Format("{0} should not be NULL", obj.ConstrainedProperty.Name);
            }
        }

        public static void OnToString_IntegerRangeConstraint(IntegerRangeConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} <= {1} <= {2}", obj.Min, obj.ConstrainedProperty == null ? "(no property)" : obj.ConstrainedProperty.Name, obj.Max);
        }
        
        public static void OnToString_StringRangeConstraint(StringRangeConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            string maxLength = obj.MaxLength != null ? obj.MaxLength.ToString() : "unlimited";
            // Only display min if there is an actual restriction.
            if (obj.MinLength > 0)
            {
                e.Result = String.Format("{0} should have at least {1} and at most {2} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.Name,
                    obj.MinLength,
                    maxLength);
            }
            else
            {
                e.Result = String.Format("{0} should have at most {1} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.Name,
                    maxLength);
            }
        }

        public static void OnToString_MethodInvocationConstraint(MethodInvocationConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "MethodIvocation's Method.ObjectClass should be assignable from InvokeOnObjectClass";
        }

        public static void OnToString_IsValidIdentifierConstraint(IsValidIdentifierConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "Method names, property names, enum names etc. must be valid names.";
        }

        public static void OnToString_ConsistentNavigatorConstraint(ConsistentNavigatorConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "The navigator should be consistent with the defining Relation.";
        }
        #endregion
    }
}
