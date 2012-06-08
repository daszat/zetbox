namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class BoolDefaultValueActions
    {
        [Invocation]
        public static void GetDefaultValue(Zetbox.App.Base.BoolDefaultValue obj, MethodReturnEventArgs<object> e)
        {
            e.Result = obj.BoolValue;
        }

        [Invocation]
        public static void ToString(Zetbox.App.Base.BoolDefaultValue obj, MethodReturnEventArgs<System.String> e)
        {
            if (obj.Property != null)
            {
                e.Result = string.Format("{0} will be initialized with '{1}'",
                    obj.Property.Name,
                    obj.BoolValue);
            }
            else
            {
                e.Result = "Initializes a property with a configured bool value";
            }
        }
    }
}
