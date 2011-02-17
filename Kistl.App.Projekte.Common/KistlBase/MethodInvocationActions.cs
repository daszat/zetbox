
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;

    [Implementor]
    public static class MethodInvocationActions
    {
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        [Invocation]
        public static void ToString(MethodInvocation obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} {1}.{2}",
                (obj.Implementor != null && obj.Implementor.Assembly != null)
                    ? obj.Implementor.Assembly.DeploymentRestrictions.ToString()
                    : "unknown",
                obj.InvokeOnObjectClass == null
                    ? "unattached"
                    : obj.InvokeOnObjectClass.Name,
                obj.Method == null
                    ? "unattached"
                    : obj.Method.Name);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }
        [Invocation]
        public static void GetCodeTemplate(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public static void {0}(", mi.GetMemberName());

            if (mi.InvokeOnObjectClass != null)
            {
                sb.AppendFormat("{0}.{1} obj", mi.InvokeOnObjectClass.Module != null ? mi.InvokeOnObjectClass.Module.Namespace : String.Empty, mi.InvokeOnObjectClass.Name);
            }
            else
            {
                sb.Append("<<TYPE>> obj");
            }

            if (mi.Method != null)
            {
                var returnParam = mi.Method.GetReturnParameter();
                if (returnParam != null)
                {
                    if (returnParam.IsList)
                    {
                        sb.AppendFormat(", MethodReturnEventArgs<IList<{0}>> e", returnParam.GetParameterTypeString());
                    }
                    else
                    {
                        sb.AppendFormat(", MethodReturnEventArgs<{0}> e", returnParam.GetParameterTypeString());
                    }
                }

                foreach (var param in mi.Method.Parameter.Where(p => !p.IsReturnParameter))
                {
                    sb.AppendFormat(", {0} {1}",
                        param.IsList ? string.Format("IList<{0}>", param.GetParameterTypeString()) : param.GetParameterTypeString(),
                        param.Name);
                }
            }

            sb.AppendLine(")");
            sb.AppendLine("{");
            sb.AppendLine("}");

            e.Result = sb.ToString();
        }

        [Invocation]
        public static void GetMemberName(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("On");
            sb.Append(mi.Method != null ? mi.Method.Name : "<<METHODNAME>>");
            sb.Append("_");
            sb.Append(mi.InvokeOnObjectClass != null ? mi.InvokeOnObjectClass.Name : "<<OBJECTCLASSNAME>>");

            e.Result = sb.ToString();
        }
    }
}
