namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    [Implementor]
    public static class EasterCalendarRuleActions
    {
        [Invocation]
        public static void ToString(EasterCalendarRule obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format(e.Result + "; Easter, offset {0} days", obj.Offset);
        }

        [Invocation]
        public static void AppliesTo(EasterCalendarRule obj, MethodReturnEventArgs<System.Boolean> e, System.DateTime date)
        {
            e.Result = false;
        }
    }
}
