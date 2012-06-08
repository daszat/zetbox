
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
    public static class CompoundObjectParameterActions
    {
        [Invocation]
        public static void GetParameterType(Zetbox.App.Base.CompoundObjectParameter obj, Zetbox.API.MethodReturnEventArgs<System.Type> e)
        {
            var cls = obj.CompoundObject;
            e.Result = Type.GetType(cls.Module.Namespace + "." + cls.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }

        [Invocation]
        public static void GetParameterTypeString(CompoundObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            var cls = obj.CompoundObject;
            if (cls == null)
            {
                e.Result = "<no type>";
            }
            else if (cls.Module == null)
            {
                e.Result = "<no namespace>." + cls.Name;
            }
            else
            {
                e.Result = cls.Module.Namespace + "." + cls.Name;
            }
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }
    }
}
