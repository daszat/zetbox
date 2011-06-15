namespace Kistl.App.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    [Implementor]
    public static class FixedYearlyCalendarRuleActions
    {
        [Invocation]
        public static void ToString(FixedYearlyCalendarRule obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format(e.Result + "; Yearly on {0}.{1}", obj.Day, obj.Month);
        }

        [Invocation]
        public static void AppliesTo(FixedYearlyCalendarRule obj, MethodReturnEventArgs<System.Boolean> e, System.DateTime date)
        {
            e.Result = date.Day == obj.Day && date.Month == obj.Month;
        }
    }
}
