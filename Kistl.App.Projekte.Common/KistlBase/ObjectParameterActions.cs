
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
    public static class ObjectParameterActions
    {
        [Invocation]
        public static void GetParameterType(Kistl.App.Base.ObjectParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString() + ", " + Kistl.API.Helper.InterfaceAssembly, true);
        }

        [Invocation]
        public static void GetParameterTypeString(ObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            if (obj.DataType == null)
            {
                e.Result = "<no type>";
            }
            else if (obj.DataType.Module == null)
            {
                e.Result = "<no namespace>." + obj.DataType.Name;
            }
            else
            {
                e.Result = obj.DataType.Module.Namespace + "." + obj.DataType.Name;
            }
        }
    }
}
