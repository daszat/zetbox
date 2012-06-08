namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Utils;

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

        private static List<CalendarRule> RulesAndParents(this Calendar cal)
        {
            var result = new List<CalendarRule>();
            result.AddRange(cal.CalendarRules);
            var parent = cal.BaseCalendar;
            while (parent != null)
            {
                result.AddRange(parent.CalendarRules);
                parent = parent.BaseCalendar;
            }
            return result;
        }

        private static decimal Calc(Calendar obj, DateTime from, DateTime until, CalcModes what)
        {
            decimal result = 0;
            var rules = obj.RulesAndParents();

            while (from <= until)
            {
                CalendarRule foundRule = null;
                // Find YearlyRule
                foundRule = rules.OfType<YearlyCalendarRule>().FirstOrDefault(r => r.AppliesTo(from));
                // Find DayOfWeekRule
                if (foundRule == null)
                {
                    foundRule = rules.OfType<DayOfWeekCalendarRule>().FirstOrDefault(r => r.AppliesTo(from));
                }
                // Find CommonRule
                if (foundRule == null)
                {
                    foundRule = rules.OfType<CommonCalendarRule>().FirstOrDefault(r => r.AppliesTo(from));
                }

                if (foundRule != null)
                {
                    switch (what)
                    {
                        case CalcModes.WorkingHours:
                            result += foundRule.WorkingHours;
                            break;
                        case CalcModes.WorkingDays:
                            if (foundRule.IsWorkingDay)
                                result++;
                            break;
                        case CalcModes.OffDays:
                            if (!foundRule.IsWorkingDay)
                                result++;
                            break;
                    }
                }
                else
                {
                    Logging.Log.WarnFormat("No Calendar rule found in {0} for {1}", obj.Name, from);
                }

                from = from.AddDays(1);
            }

            return result;
        }
    }
}
