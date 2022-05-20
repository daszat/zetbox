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

    [Implementor]
    public static class EventActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Event obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = string.Format("{0} - {1}: {2} ({3})",
                obj.IsAllDay ? obj.StartDate.ToShortDateString() : obj.StartDate.ToShortDateString() + " " + obj.StartDate.ToShortTimeString(),
                obj.IsAllDay ? obj.EndDate.ToShortDateString() : obj.EndDate.ToShortDateString() + " " + obj.EndDate.ToShortTimeString(),
                obj.Summary,
                obj.Location);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task NotifyPreSave(Event obj)
        {
            if (obj.IsAllDay)
            {
                obj.StartDate = obj.StartDate.Date;
                obj.EndDate = obj.EndDate.Date;
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
