namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class BaseParameterActions
    {
        internal static void DecorateParameterType(BaseParameter obj, MethodReturnEventArgs<string> e, bool isStruct)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (e == null) throw new ArgumentNullException("e");

            if (isStruct && obj.IsNullable)
            {
                e.Result += "?";
            }

            if (obj.IsList)
            {
                e.Result = string.Format("IEnumerable<{0}>", e.Result);
            }
        }

        internal static void DecorateParameterType(BaseParameter obj, MethodReturnEventArgs<Type> e, bool isStruct)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (e == null) throw new ArgumentNullException("e");

            if (isStruct && obj.IsNullable)
            {
                e.Result = typeof(Nullable<>).MakeGenericType(e.Result);
            }

            if (obj.IsList)
            {
                e.Result = typeof(IEnumerable<>).MakeGenericType(e.Result);
            }
        }

        [Invocation]
        public static void GetLabel(BaseParameter obj, MethodReturnEventArgs<System.String> e)
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
    }
}
