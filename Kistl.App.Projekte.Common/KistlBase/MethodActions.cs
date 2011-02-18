
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
        public static void CreateMethodInvocation(Kistl.App.Base.Method obj, MethodReturnEventArgs<Kistl.App.Base.MethodInvocation> e)
        {
            e.Result = obj.Context.Create<MethodInvocation>();
            e.Result.InvokeOnObjectClass = obj.ObjectClass;
            e.Result.Method = obj;
            e.Result.Module = obj.Module;
        }

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

    }
}
