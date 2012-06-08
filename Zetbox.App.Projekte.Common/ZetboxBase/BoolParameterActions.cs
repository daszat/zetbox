namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class BoolParameterActions
    {
        [Invocation]
        public static void GetParameterType(BoolParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(bool);
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }

        [Invocation]
        public static void GetParameterTypeString(BoolParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "bool";
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }
    }
}
