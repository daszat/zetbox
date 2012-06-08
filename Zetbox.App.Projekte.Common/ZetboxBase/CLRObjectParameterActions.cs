namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class CLRObjectParameterActions
    {
        [Invocation]
        public static void GetParameterType(CLRObjectParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = obj.Type.AsType(true);
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }

        [Invocation]
        public static void GetParameterTypeString(CLRObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Type != null ? obj.Type.FullName : "<no type set>";
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }
    }
}
