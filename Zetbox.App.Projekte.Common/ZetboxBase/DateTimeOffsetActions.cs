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
    public class DateTimeOffsetActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(DateTimeOffset obj, MethodReturnEventArgs<string> e)
        {
            var sb = new StringBuilder();

            if (obj.Years.HasValue) sb.AppendFormat(" {0} years", obj.Years.Value);
            if (obj.Months.HasValue) sb.AppendFormat(" {0} months", obj.Months.Value);
            if (obj.Days.HasValue) sb.AppendFormat(" {0} days", obj.Days.Value);
            if (obj.Hours.HasValue) sb.AppendFormat(" {0} hours", obj.Hours.Value);
            if (obj.Minutes.HasValue) sb.AppendFormat(" {0} minutes", obj.Minutes.Value);
            if (obj.Seconds.HasValue) sb.AppendFormat(" {0} seconds", obj.Seconds.Value);

            e.Result = sb.ToString();

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task AddTo(DateTimeOffset obj, MethodReturnEventArgs<DateTime> e, DateTime dt)
        {
            e.Result = dt
                .AddYears(obj.Years ?? 0)
                .AddMonths(obj.Months ?? 0)
                .AddDays(obj.Days ?? 0.0)
                .AddHours(obj.Hours ?? 0.0)
                .AddMinutes(obj.Minutes ?? 0.0)
                .AddSeconds(obj.Seconds ?? 0.0);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
