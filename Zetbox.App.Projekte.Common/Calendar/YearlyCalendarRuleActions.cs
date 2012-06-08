namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;

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
            // Abstract
        }
    }
}
