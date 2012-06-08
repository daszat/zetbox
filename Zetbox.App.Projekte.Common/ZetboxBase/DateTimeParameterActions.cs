namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class DateTimeParameterActions
    {
        [Invocation]
        public static void GetParameterType(DateTimeParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(DateTime);
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }

        [Invocation]
        public static void GetParameterTypeString(DateTimeParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "DateTime";
            BaseParameterActions.DecorateParameterType(obj, e, true);
        }
    }
}
