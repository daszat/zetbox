
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
    public static class ObjectReferenceParameterActions
    {
        [Invocation]
        public static void GetParameterType(Kistl.App.Base.ObjectReferenceParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString() + ", " + Kistl.API.Helper.InterfaceAssembly, true);
        }

        [Invocation]
        public static void GetParameterTypeString(ObjectReferenceParameter obj, MethodReturnEventArgs<string> e)
        {
            if (obj.ObjectClass == null)
            {
                e.Result = "<no type>";
            }
            else if (obj.ObjectClass.Module == null)
            {
                e.Result = "<no namespace>." + obj.ObjectClass.Name;
            }
            else
            {
                e.Result = obj.ObjectClass.Module.Namespace + "." + obj.ObjectClass.Name;
            }
        }
    }
}
