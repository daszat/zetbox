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
    using Zetbox.API.Utils;

    [Implementor]
    public class RecurrenceRuleActions
    {
        [Invocation]
        public static void ToString(RecurrenceRule obj, MethodReturnEventArgs<string> e)
        {
            var interval = obj.Interval ?? 1;
            if (interval <= 0)
            {
                interval = 1;
            }

            var sb = new StringBuilder();
            if (obj.EveryYear) sb.AppendFormat("every {0} year", interval);
            else if (obj.EveryQuater) sb.AppendFormat("every {0} quater", interval);
            else if (obj.EveryMonth) sb.AppendFormat("every {0} month", interval);
            else if (obj.EveryDayOfWeek.HasValue) sb.AppendFormat("every {0} {1}", interval, obj.EveryDayOfWeek.ToString());
            else if (obj.EveryDay) sb.AppendFormat("every {0} day", interval);

            if (obj.MonthsOffset.HasValue || obj.DaysOffset.HasValue || obj.HoursOffset.HasValue || obj.MinutesOffset.HasValue) sb.Append(" + offset:");

            if (obj.MonthsOffset.HasValue) sb.AppendFormat(" {0} months", obj.MonthsOffset.Value);
            if (obj.DaysOffset.HasValue) sb.AppendFormat(" {0} days", obj.DaysOffset.Value);
            if (obj.HoursOffset.HasValue) sb.AppendFormat(" {0} hours", obj.HoursOffset.Value);
            if (obj.MinutesOffset.HasValue) sb.AppendFormat(" {0} minutes", obj.MinutesOffset.Value);

            if (obj.Until.HasValue)
            {
                sb.AppendFormat(" until {0}", obj.Until);
            } 
            else if (obj.Count.HasValue)
            {
                sb.AppendFormat(" {0} times", obj.Count);
            }
                        
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
            var interval = obj.Interval ?? 1;
            if(interval <= 0)
            {
                Logging.Log.WarnFormat("{0} has an invalid interval of {1}", obj, interval);
                interval = 1;
            }

            if (obj.EveryYear)
            {
                dt = dt.FirstYearDay().AddYears(1 * interval);
            }
            else if (obj.EveryQuater)
            {
                dt = dt.FirstQuaterDay().AddMonths(3 * interval);
            }
            else if (obj.EveryMonth)
            {
                dt = dt.FirstMonthDay().AddMonths(1 * interval);
            }
            else if (obj.EveryDayOfWeek.HasValue)
            {
                // Assuming Monday is the first day of week
                dt = dt.FirstWeekDay().AddDays((((int)obj.EveryDayOfWeek - 1) % 7)).AddDays(7 * interval);
            }
            else if (obj.EveryDay)
            {
                dt = dt.Date.AddDays(1 * interval);
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
