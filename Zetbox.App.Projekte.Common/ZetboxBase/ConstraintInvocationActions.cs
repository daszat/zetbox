namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class ConstraintInvocationActions
    {
        [Invocation]
        public static void GetMemberName(ConstraintInvocation obj, MethodReturnEventArgs<string> e)
        {
            var constr_IsValid = obj.Context.GetQuery<InvokingConstraint>().FirstOrDefault(i => i.IsValidInvocation == obj);
            var constr_GetErrorText = obj.Context.GetQuery<InvokingConstraint>().FirstOrDefault(i => i.GetErrorTextInvocation == obj);

            if (constr_IsValid != null)
            {
                e.Result = string.Format("OnIsValid_{0}_{1}",
                    constr_IsValid.ConstrainedProperty != null && constr_IsValid.ConstrainedProperty.ObjectClass != null ? constr_IsValid.ConstrainedProperty.ObjectClass.Name : string.Empty,
                    constr_IsValid.ConstrainedProperty != null ? constr_IsValid.ConstrainedProperty.Name : string.Empty);
            }
            else if (constr_GetErrorText != null)
            {
                e.Result = string.Format("OnGetErrorText_{0}_{1}",
                    constr_GetErrorText.ConstrainedProperty != null && constr_GetErrorText.ConstrainedProperty.ObjectClass != null ? constr_GetErrorText.ConstrainedProperty.ObjectClass.Name : string.Empty,
                    constr_GetErrorText.ConstrainedProperty != null ? constr_GetErrorText.ConstrainedProperty.Name : string.Empty);
            }
            else
            {
                e.Result = string.Empty;
            }
        }

        [Invocation]
        public static void GetCodeTemplate(ConstraintInvocation obj, MethodReturnEventArgs<string> e)
        {
            var constr_IsValid = obj.Context.GetQuery<InvokingConstraint>().FirstOrDefault(i => i.IsValidInvocation == obj);
            var constr_GetErrorText = obj.Context.GetQuery<InvokingConstraint>().FirstOrDefault(i => i.GetErrorTextInvocation == obj);

            if (constr_IsValid != null)
            {
                e.Result = string.Format("public static bool {0}(object constrainedObject, object constrainedValue)\n{{\n\treturn true;\n}}", obj.GetMemberName());

            }
            else if (constr_GetErrorText != null)
            {
                e.Result = string.Format("public static string {0}(object constrainedObject, object constrainedValue)\n{{\n\treturn string.Empty;\n}}", obj.GetMemberName());
            }
            else
            {
                e.Result = string.Empty;
            }
        }
    }
}
