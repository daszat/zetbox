
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
    public static class CompoundObjectParameterActions
    {
        [Invocation]
        public static void GetParameterType(Kistl.App.Base.CompoundObjectParameter obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            e.Result = Type.GetType(obj.GetParameterTypeString() + ", " + Kistl.API.Helper.InterfaceAssembly, true);
        }

        [Invocation]
        public static void GetParameterTypeString(CompoundObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            if (obj.CompoundObject == null)
            {
                e.Result = "<no type>";
            }
            else if (obj.CompoundObject.Module == null)
            {
                e.Result = "<no namespace>." + obj.CompoundObject.Name;
            }
            else
            {
                e.Result = obj.CompoundObject.Module.Namespace + "." + obj.CompoundObject.Name;
            }
        }
    }
}
