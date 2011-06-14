namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    [Implementor]
    public static class CalendarActions
    {
        [Invocation]
        public static void ToString(Calendar obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void GetWorkingHours(Calendar obj, MethodReturnEventArgs<System.Decimal> e, System.DateTime from, System.DateTime until)
        {
            e.Result = Calc(obj, from, until, CalcModes.WorkingHours);
        }

        [Invocation]
        public static void GetWorkingDays(Calendar obj, MethodReturnEventArgs<System.Int32> e, System.DateTime from, System.DateTime until)
        {
            e.Result = (int)Calc(obj, from, until, CalcModes.WorkingDays);
        }

        [Invocation]
        public static void GetOffDays(Calendar obj, MethodReturnEventArgs<System.Int32> e, System.DateTime from, System.DateTime until)
        {
            e.Result = (int)Calc(obj, from, until, CalcModes.OffDays);
        }

        private enum CalcModes
        {
            WorkingHours,
            WorkingDays,
            OffDays
        }

        private static decimal Calc(Calendar obj, DateTime from, DateTime until, CalcModes what)
        {
            decimal result = 0;

            while (from <= until)
            {
                CalendarRule foundRule = null;
                // Find YearlyRule
                foundRule = obj.CalendarRules.OfType<YearlyCalendarRule>().FirstOrDefault(r => r.AppliesTo(from));
                // Find DayOfWeekRule
                if (foundRule == null)
                {
                    foundRule = obj.CalendarRules.OfType<DayOfWeekCalendarRule>().FirstOrDefault(r => r.AppliesTo(from));
                }
                // Find CommonRule
                if (foundRule == null)
                {
                    foundRule = obj.CalendarRules.OfType<CommonCalendarRule>().FirstOrDefault(r => r.AppliesTo(from));
                }

                if (foundRule != null)
                {
                    switch(what)
                    {
                        case CalcModes.WorkingHours:
                            result += foundRule.WorkingHours;
                            break;
                        case CalcModes.WorkingDays:
                            if(foundRule.WorkingHours != 0)
                                result++;
                            break;
                        case CalcModes.OffDays:
                            if (foundRule.WorkingHours == 0)
                                result++;
                            break;
                    }
                }
                else if (obj.BaseCalendar != null)
                {
                    result += Calc(obj.BaseCalendar, from, from, what);
                }

                from = from.AddDays(1);
            }

            return result;
        }
    }
}
