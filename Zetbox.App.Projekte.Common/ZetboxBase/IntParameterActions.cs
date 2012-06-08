namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class IntParameterActions
    {
        [Invocation]
        public static void GetParameterType(IntParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(int);
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }

        [Invocation]
        public static void GetParameterTypeString(IntParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "int";
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }
    }
}
