namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;

    [Implementor]
    public static class CalendarRuleActions
    {
        [Invocation]
        public static void ToString(CalendarRule obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0}; {1} {2}", obj.Name, obj.WorkingHours, obj.WorkingHours == 1 ? "hour" : "hours");
        }

        [Invocation]
        public static void AppliesTo(CalendarRule obj, MethodReturnEventArgs<System.Boolean> e, System.DateTime date)
        {
            // Abstract
        }
    }

    internal static class CalendarRuleActionsExtensions
    {
        public static bool CheckValidDate(this CalendarRule obj, DateTime date)
        {
            return (obj.ValidFrom == null || obj.ValidFrom <= date) && (obj.ValidUntil == null || obj.ValidUntil >= date);
        }
    }
}
