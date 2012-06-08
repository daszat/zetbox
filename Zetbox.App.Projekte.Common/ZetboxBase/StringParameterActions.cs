namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class StringParameterActions
    {
        [Invocation]
        public static void GetParameterType(StringParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(string);
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }

        [Invocation]
        public static void GetParameterTypeString(StringParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "string";
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }
    }
}
