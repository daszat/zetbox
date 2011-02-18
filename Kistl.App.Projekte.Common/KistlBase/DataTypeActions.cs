namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class DataTypeActions
    {
        [Invocation]
        public static void ToString(DataType obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}.{1}",
                obj.Module == null
                    ? "[no module]"
                    : obj.Module.Namespace,
                obj.Name);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetDataType(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetDataTypeString() + ", " + Kistl.API.Helper.InterfaceAssembly, true);
        }

        [Invocation]
        public static void GetDataTypeString(Kistl.App.Base.DataType obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (obj.Module == null)
            {
                e.Result = obj.Name;
            }
            else
            {
                e.Result = obj.Module.Namespace + "." + obj.Name;
            }
        }

    }
}
