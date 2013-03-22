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
            if (obj.Frequency.HasValue) sb.AppendFormat("every {0} {1}", interval, ToString(obj.Frequency.Value));
            if (!string.IsNullOrWhiteSpace(obj.ByMonth)) sb.AppendFormat(" by months {0}", obj.ByMonth);
            if (!string.IsNullOrWhiteSpace(obj.ByWeekNumber)) sb.AppendFormat(" by week numbers {0}", obj.ByWeekNumber);
            if (!string.IsNullOrWhiteSpace(obj.ByYearDay)) sb.AppendFormat(" by year days {0}", obj.ByYearDay);
            if (!string.IsNullOrWhiteSpace(obj.ByMonthDay)) sb.AppendFormat(" by month days {0}", obj.ByMonthDay);
            if (!string.IsNullOrWhiteSpace(obj.ByDay)) sb.AppendFormat(" by days {0}", obj.ByDay);
            if (!string.IsNullOrWhiteSpace(obj.ByHour)) sb.AppendFormat(" by hours {0}", obj.ByHour);
            if (!string.IsNullOrWhiteSpace(obj.ByMinute)) sb.AppendFormat(" by minutes {0}", obj.ByMinute);
            if (!string.IsNullOrWhiteSpace(obj.BySecond)) sb.AppendFormat(" by seconds {0}", obj.BySecond);

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

        private static string ToString(Frequency f)
        {
            switch (f)
            {
                case Frequency.Yearly:
                    return "year";
                case Frequency.Monthly:
                    return "month";
                case Frequency.Weekly:
                    return "week";
                case Frequency.Daily:
                    return "day";
                case Frequency.Hourly:
                    return "hour";
                case Frequency.Minutely:
                    return "minue";
                case Frequency.Secondly:
                    return "second";
                default:
                    return string.Empty;
            }
        }

        [Invocation]
        public static void GetNext(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e, DateTime start)
        {
            e.Result = obj.GetNext(DateTime.Now, start);
        }

        [Invocation]
        public static void GetNext(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e, DateTime dt, DateTime start)
        {
            var interval = obj.Interval ?? 1;
            if(interval <= 0)
            {
                Logging.Log.WarnFormat("{0} has an invalid interval of {1}", obj, interval);
                interval = 1;
            }

            e.Result = dt;
        }

        [Invocation]
        public static void GetCurrent(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e, DateTime start)
        {
            e.Result = obj.GetCurrent(DateTime.Now, start);
        }

        [Invocation]
        public static void GetCurrent(RecurrenceRule obj, MethodReturnEventArgs<DateTime> e, DateTime dt, DateTime start)
        {
            e.Result = dt;
        }

        [Invocation]
        public static void GetWithinInterval(RecurrenceRule obj, MethodReturnEventArgs<IEnumerable<DateTime>> e, DateTime start, DateTime from, DateTime until)
        {
        }
    }
}
