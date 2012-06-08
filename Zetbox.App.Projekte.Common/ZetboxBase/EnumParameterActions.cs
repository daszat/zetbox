namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class EnumParameterActions
    {
        [Invocation]
        public static void GetParameterType(EnumParameter obj, MethodReturnEventArgs<Type> e)
        {
            var cls = obj.Enumeration;
            e.Result = Type.GetType(cls.Module.Namespace + "." + cls.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }

        [Invocation]
        public static void GetParameterTypeString(EnumParameter obj, MethodReturnEventArgs<System.String> e)
        {
            if (obj.Enumeration == null)
            {
                e.Result = "<no enum>";
            }
            else if (obj.Enumeration.Module == null)
            {
                e.Result = "<no namespace>." + obj.Enumeration.Name;
            }
            else
            {
                e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.Name;
            }
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }
    }
}
