namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class DecimalParameterActions
    {
        [Invocation]
        public static void GetParameterType(DecimalParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(decimal);
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }

        [Invocation]
        public static void GetParameterTypeString(DecimalParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "decimal";
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }
    }
}
