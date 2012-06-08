
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    [Implementor]
    public static class ObjectReferenceParameterActions
    {
        [Invocation]
        public static void GetParameterType(ObjectReferenceParameter obj, MethodReturnEventArgs<Type> e)
        {
            var def = obj.ObjectClass;
            e.Result = Type.GetType(def.Module.Namespace + "." + def.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
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
