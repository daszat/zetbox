namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class NotNullableConstraintActions
    {
        [Invocation]
        public static void ToString(NotNullableConstraint obj, Zetbox.API.MethodReturnEventArgs<string> e)
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

        [Invocation]
        public static void IsValid(
            NotNullableConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            if (constrainedObjectParam is IDataObject)
            {
                var dataObj = (IDataObject)constrainedObjectParam;
                if (!dataObj.IsInitialized(obj.ConstrainedProperty.Name))
                {
                    e.Result = false;
                    return;
                }
            }
            e.Result = constrainedValueParam != null;
        }

        [Invocation]
        public static void GetErrorText(
            NotNullableConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = String.IsNullOrEmpty(obj.Reason) ? "Wert muss gesetzt sein" : String.Format("Wert muss gesetzt sein: {0}", obj.Reason);
        }
    }
}
