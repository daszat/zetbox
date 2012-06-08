namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class EnumDefaultValueActions
    {
        [Invocation]
        public static void GetDefaultValue(Zetbox.App.Base.EnumDefaultValue obj, MethodReturnEventArgs<object> e)
        {
            e.Result = obj.EnumValue.Value;
        }

        [Invocation]
        public static void ToString(Zetbox.App.Base.EnumDefaultValue obj, MethodReturnEventArgs<System.String> e)
        {
            if (obj.Property != null)
            {
                e.Result = string.Format("{0} will be initialized with '{1}.{2}'",
                    obj.Property.Name,
                    obj.EnumValue != null && obj.EnumValue.Enumeration != null ? obj.EnumValue.Enumeration.Name : "<unknown>",
                    obj.EnumValue != null ? obj.EnumValue.Name : "<unknown>");
            }
            else
            {
                e.Result = "Initializes a property with a configured enum value";
            }
        }
    }
}
