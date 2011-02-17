namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class CurrentDateTimeDefaultValueActions
    {
        [Invocation]
        public static void GetDefaultValue(Kistl.App.Base.CurrentDateTimeDefaultValue obj, MethodReturnEventArgs<System.Object> e)
        {
            e.Result = DateTime.Now;
        }

        [Invocation]
        public static void ToString(Kistl.App.Base.CurrentDateTimeDefaultValue obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Property != null)
            {
                e.Result = string.Format("{0} will be initialized with the current date and time", obj.Property.Name);
            }
            else
            {
                e.Result = "Initializes a property with the current date and time";
            }
        }
    }
}
