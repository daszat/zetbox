
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    [Implementor]
    public static class ObjectReferenceParameterActions
    {
        [Invocation]
        public static void GetParameterType(ObjectReferenceParameter obj, MethodReturnEventArgs<Type> e)
        {
            var def = obj.ObjectClass;
            e.Result = Type.GetType(def.Module.Namespace + "." + def.Name + ", " + Kistl.API.Helper.InterfaceAssembly, true);
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }

        [Invocation]
        public static void GetParameterTypeString(ObjectReferenceParameter obj, MethodReturnEventArgs<string> e)
        {
            var def = obj.ObjectClass;
            if (def == null)
            {
                e.Result = "<no type>";
            }
            else if (def.Module == null)
            {
                e.Result = "<no namespace>." + def.Name;
            }
            else
            {
                e.Result = def.Module.Namespace + "." + def.Name;
            }
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }
    }
}
