namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    [Implementor]
    public static class DayOfWeekCalendarRuleActions
    {
        [Invocation]
        public static void ToString(DayOfWeekCalendarRule obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format(e.Result + "; every {0}", obj.DayOfWeek.ToString());
        }

        [Invocation]
        public static void AppliesTo(DayOfWeekCalendarRule obj, MethodReturnEventArgs<System.Boolean> e, System.DateTime date)
        {
            e.Result = (int)date.DayOfWeek == (int)obj.DayOfWeek;
        }
    }
}
