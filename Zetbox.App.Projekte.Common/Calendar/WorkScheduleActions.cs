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
        public static void ToString(WorkSchedule obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void GetWorkingHours(WorkSchedule obj, MethodReturnEventArgs<System.Decimal> e, System.DateTime from, System.DateTime until)
        {
            e.Result = Calc(obj, from, until, CalcModes.WorkingHours);
        }

        [Invocation]
        public static void GetWorkingDays(WorkSchedule obj, MethodReturnEventArgs<System.Int32> e, System.DateTime from, System.DateTime until)
        {
            e.Result = (int)Calc(obj, from, until, CalcModes.WorkingDays);
        }

        [Invocation]
        public static void GetOffDays(WorkSchedule obj, MethodReturnEventArgs<System.Int32> e, System.DateTime from, System.DateTime until)
        {
            e.Result = (int)Calc(obj, from, until, CalcModes.OffDays);
        }

        private enum CalcModes
        {
            WorkingHours,
            WorkingDays,
            OffDays
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
                foundRule = rules.OfType<YearlyWorkScheduleRule>().FirstOrDefault(r => r.AppliesTo(from));
                // Find DayOfWeekRule
                if (foundRule == null)
                {
                    foundRule = rules.OfType<DayOfWeekWorkScheduleRule>().FirstOrDefault(r => r.AppliesTo(from));
                }
                // Find CommonRule
                if (foundRule == null)
                {
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