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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Extensions;

    [Implementor]
    public class RecurrenceRuleActions
    {
        [Invocation]
        public static void ToString(RecurrenceRule obj, MethodReturnEventArgs<string> e)
        {
            var sb = new StringBuilder();
            if (obj.EveryYear) sb.Append("every year");
            else if (obj.EveryQuater) sb.Append("every quater");
            else if (obj.EveryMonth) sb.Append("every month");
            else if (obj.EveryDayOfWeek.HasValue) sb.Append("every " + obj.EveryDayOfWeek.ToString());
            else if (obj.EveryDay) sb.Append("every day");

            if (obj.MonthsOffset.HasValue || obj.DaysOffset.HasValue || obj.HoursOffset.HasValue || obj.MinutesOffset.HasValue) sb.Append(" + offset: ");

            if (obj.MonthsOffset.HasValue) sb.Append(obj.MonthsOffset.Value.ToString() + " months ");
            if (obj.DaysOffset.HasValue) sb.Append(obj.DaysOffset.Value.ToString() + " days ");
            if (obj.HoursOffset.HasValue) sb.Append(obj.HoursOffset.Value.ToString() + " hours ");
            if (obj.MinutesOffset.HasValue) sb.Append(obj.MinutesOffset.Value.ToString() + " minutes ");
            if (sb.Length == 0)
            {
                sb.Append("not defined");
            }
            e.Result = sb.ToString();
        }

        [Invocation]
        public static void GetNext(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e)
        {
            e.Result = obj.GetNext(DateTime.Now);
        }

        [Invocation]
        public static void GetNext(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e, DateTime dt)
        {
            if (obj.EveryYear)
            {
                dt = dt.FirstYearDay().AddYears(1);
            }
            else if (obj.EveryQuater)
            {
                dt = dt.FirstQuaterDay().AddMonths(3);
            }
            else if (obj.EveryMonth)
            {
                dt = dt.FirstMonthDay().AddMonths(1);
            }
            else if (obj.EveryDayOfWeek.HasValue)
            {
                // Assuming Monday is the first day of week
                dt = dt.FirstWeekDay().AddDays((((int)obj.EveryDayOfWeek - 1) % 7)).AddDays(7);
            }
            else if (obj.EveryDay)
            {
                dt = dt.Date.AddDays(1);
            }

            e.Result = dt
                .AddMonths(obj.MonthsOffset ?? 0)
                .AddDays(obj.DaysOffset ?? 0)
                .AddHours(obj.HoursOffset ?? 0)
                .AddMinutes(obj.MinutesOffset ?? 0);
        }

        [Invocation]
        public static void GetCurrent(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e)
        {
            e.Result = obj.GetCurrent(DateTime.Now);
        }

        [Invocation]
        public static void GetCurrent(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e, DateTime dt)
        {
            if (obj.EveryYear)
            {
                dt = dt.FirstYearDay();
            }
            else if (obj.EveryQuater)
            {
                dt = dt.FirstQuaterDay();
            }
            else if (obj.EveryMonth)
            {
                dt = dt.FirstMonthDay();
            }
            else if (obj.EveryDayOfWeek.HasValue)
            {
                // Assuming Monday is the first day of week
                dt = dt.FirstWeekDay().AddDays((((int)obj.EveryDayOfWeek - 1) % 7));
            }
            else if (obj.EveryDay)
            {
                dt = dt.Date;
            }

            e.Result = dt
                .AddMonths(obj.MonthsOffset ?? 0)
                .AddDays(obj.DaysOffset ?? 0)
                .AddHours(obj.HoursOffset ?? 0)
                .AddMinutes(obj.MinutesOffset ?? 0);
        }
    }
}
