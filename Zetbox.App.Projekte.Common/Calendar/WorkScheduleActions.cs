// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Utils;

    [Implementor]
    public static class WorkScheduleActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(WorkSchedule obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetWorkingHours(WorkSchedule obj, MethodReturnEventArgs<System.Decimal> e, System.DateTime from, System.DateTime until)
        {
            e.Result = Calc(obj, from, until, CalcModes.WorkingHours);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetWorkingDays(WorkSchedule obj, MethodReturnEventArgs<System.Int32> e, System.DateTime from, System.DateTime until)
        {
            e.Result = (int)Calc(obj, from, until, CalcModes.WorkingDays);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetOffDays(WorkSchedule obj, MethodReturnEventArgs<System.Int32> e, System.DateTime from, System.DateTime until)
        {
            e.Result = (int)Calc(obj, from, until, CalcModes.OffDays);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetHolidays(WorkSchedule obj, MethodReturnEventArgs<int> e, DateTime from, DateTime until)
        {
            e.Result = (int)Calc(obj, from, until, CalcModes.Holidays);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        private enum CalcModes
        {
            WorkingHours,
            WorkingDays,
            OffDays,
            Holidays
        }

        private static List<WorkScheduleRule> RulesAndParents(this WorkSchedule cal)
        {
            var result = new List<WorkScheduleRule>();
            result.AddRange(cal.WorkScheduleRules);
            var parent = cal.BaseWorkSchedule;
            while (parent != null)
            {
                result.AddRange(parent.WorkScheduleRules);
                parent = parent.BaseWorkSchedule;
            }
            return result;
        }

        private static decimal Calc(WorkSchedule obj, DateTime from, DateTime until, CalcModes what)
        {
            decimal result = 0;
            var rules = obj.RulesAndParents();

            while (from <= until)
            {
                WorkScheduleRule foundRule = null;
                // Find YearlyRule
                var yearlyRule = rules.OfType<YearlyWorkScheduleRule>().FirstOrDefault(r => r.AppliesTo(from));
                // Find DayOfWeekRule
                var dayOfWeekRule = rules.OfType<DayOfWeekWorkScheduleRule>().FirstOrDefault(r => r.AppliesTo(from));

                if (yearlyRule == null)
                {
                    foundRule = dayOfWeekRule;
                }
                else if(dayOfWeekRule != null)
                {
                    // Use the most "free" rule
                    if (dayOfWeekRule.IsWorkingDay == false || dayOfWeekRule.WorkingHours < yearlyRule.WorkingHours)
                    {
                        foundRule = dayOfWeekRule;
                    }
                    else
                    {
                        // use the more specific rule
                        foundRule = yearlyRule;
                    }
                }
                else
                {
                    foundRule = yearlyRule;
                }

                if (foundRule == null)
                {
                    // Find CommonRule
                    foundRule = rules.OfType<CommonWorkScheduleRule>().FirstOrDefault(r => r.AppliesTo(from));
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
                        case CalcModes.Holidays:
                            if (foundRule is YearlyWorkScheduleRule && !foundRule.IsWorkingDay)
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

        [Invocation]
        public static System.Threading.Tasks.Task Duplicate(WorkSchedule obj, MethodReturnEventArgs<Zetbox.App.Calendar.WorkSchedule> e)
        {
            var ctx = obj.Context;
            var result = ctx.Create<WorkSchedule>();

            result.Name = obj.Name + " Copy";
            result.BaseWorkSchedule = obj.BaseWorkSchedule;
            result.Module = obj.Module;

            foreach(var rule in obj.WorkScheduleRules)
            {
                var newRule = (WorkScheduleRule)ctx.Create(ctx.GetInterfaceType(rule));
                result.WorkScheduleRules.Add(rule);
                newRule.WorkSchedule = result;

                newRule.Module = rule.Module;
                newRule.Name = rule.Name;
                newRule.IsWorkingDay = rule.IsWorkingDay;
                newRule.WorkingHours = rule.WorkingHours;
                newRule.ValidFrom = rule.ValidFrom;
                newRule.ValidUntil = rule.ValidUntil;

                if (rule is DayOfWeekWorkScheduleRule)
                {
                    ((DayOfWeekWorkScheduleRule)newRule).DayOfWeek = ((DayOfWeekWorkScheduleRule)rule).DayOfWeek;
                }
                else if (rule is FixedYearlyWorkScheduleRule)
                {
                    ((FixedYearlyWorkScheduleRule)newRule).Day = ((FixedYearlyWorkScheduleRule)rule).Day;
                    ((FixedYearlyWorkScheduleRule)newRule).Month = ((FixedYearlyWorkScheduleRule)rule).Month;
                }
                else if (rule is EasterWorkScheduleRule)
                {
                    ((EasterWorkScheduleRule)newRule).Offset = ((EasterWorkScheduleRule)rule).Offset;
                }
            }

            e.Result = result;

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
