namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class IntegerRangeConstraintActions
    {
        [Invocation]
        public static void ToString(IntegerRangeConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} <= {1} <= {2}", obj.Min, obj.ConstrainedProperty == null ? "(no property)" : obj.ConstrainedProperty.Name, obj.Max);
        }

        [Invocation]
        public static void IsValid(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            if (constrainedValueParam != null)
            {
                int v = (int)constrainedValueParam;
                e.Result = (obj.Min <= v) && (v <= obj.Max);
            }
            else
            {
                // only accept null values if no lower bound is set
                e.Result = obj.Min == 0;
            }
        }

        [Invocation]
        public static void GetErrorText(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            int v = (int)constrainedValueParam;
            if (obj.IsValid(constrainedObjectParam, constrainedValueParam))
            {
                e.Result = null;
            }
            else
            {
                StringBuilder result = new StringBuilder();
                if (v < obj.Min)
                    result.AppendFormat("{0} should be equal or greater than {1}", obj.ConstrainedProperty.Name, obj.Min);
                if (v > obj.Max)
                    result.AppendFormat("{0} should be equal or less than {1}", obj.ConstrainedProperty.Name, obj.Max);

                if (!String.IsNullOrEmpty(obj.Reason))
                {
                    result.Append(": ");
                    result.Append(obj.Reason);
                }

                e.Result = result.ToString();
            }
        }
    }
}
