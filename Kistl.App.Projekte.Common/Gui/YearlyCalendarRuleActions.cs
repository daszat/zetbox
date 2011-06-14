namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    [Implementor]
    public static class YearlyCalendarRuleActions
    {
        [Invocation]
        public static void ToString(YearlyCalendarRule obj, MethodReturnEventArgs<System.String> e)
        {
            // Nothing to do
        }

        [Invocation]
        public static void AppliesTo(YearlyCalendarRule obj, MethodReturnEventArgs<System.Boolean> e, System.DateTime date)
        {
            e.Result = false; // Abstract
        }
    }
}
