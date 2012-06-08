namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class DoubleParameterActions
    {
        [Invocation]
        public static void GetParameterType(DoubleParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(double);
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }

        [Invocation]
        public static void GetParameterTypeString(DoubleParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "double";
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }
    }
}
