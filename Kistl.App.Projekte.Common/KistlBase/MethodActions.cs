
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
    public static class MethodActions
    {
        [Invocation]
        public static void GetLabel(Kistl.App.Base.Method obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;
        }

        [Invocation]
        public static void ToString(Method obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (obj.ObjectClass != null && obj.ObjectClass.Module != null)
            {
                e.Result = obj.ObjectClass.Module.Namespace + "." +
                                obj.ObjectClass.Name + "." + obj.Name;

                ToStringHelper.FixupFloatingObjectsToString(obj, e);
            }
            else
            {
                e.Result = String.Format("new Method #{0}: {1}", obj.ID, obj.Name);
            }
        }

        [Invocation]
        public static void GetReturnParameter(Method obj, MethodReturnEventArgs<BaseParameter> e)
        {
            e.Result = obj.Parameter.SingleOrDefault(param => param.IsReturnParameter);
        }

        [Invocation]
        public static void postSet_Name(Method obj, PropertyPostSetterEventArgs<string> e)
        {
            obj.NotifyPropertyChanged("CodeTemplate", string.Empty, string.Empty);
        }

        // TODO: Not supported yet
        //[Invocation]
        //public static void postSet_Parameter(Method obj, PropertyPostSetterEventArgs<IList<BaseParameter>> e)
        //{
        //    obj.NotifyPropertyChanged("CodeTemplate", string.Empty, string.Empty);
        //}

        [Invocation]
        public static void get_CodeTemplate(Method obj, PropertyGetterEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public static void {0}(", obj.Name);

            if (obj.ObjectClass != null)
            {
                sb.AppendFormat("{0} obj", obj.ObjectClass.Name);
            }
            else
            {
                sb.Append("<<TYPE>> obj");
            }

            var returnParam = obj.GetReturnParameter();
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

            foreach (var param in obj.Parameter.Where(p => !p.IsReturnParameter))
            {
                sb.AppendFormat(", {0} {1}",
                    param.IsList ? string.Format("IList<{0}>", param.GetParameterTypeString()) : param.GetParameterTypeString(),
                    param.Name);
            }

            sb.AppendLine(")");
            sb.AppendLine("{");
            sb.AppendLine("}");

            e.Result = sb.ToString();
        }
    }
}
