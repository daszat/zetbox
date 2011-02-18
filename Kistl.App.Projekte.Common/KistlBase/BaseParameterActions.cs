namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class BaseParameterActions
    {
        [Invocation]
        public static void GetLabel(Kistl.App.Base.BaseParameter obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;
        }

        [Invocation]
        public static void ToString(BaseParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0}{1} {2}",
                obj.IsReturnParameter
                    ? "[Return] "
                    : String.Empty,
                obj.GetParameterTypeString(),
                obj.Name);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetParameterType(Kistl.App.Base.BaseParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString(), true);
        }

    }
}
