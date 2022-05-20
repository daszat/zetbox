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
    public static class WorkScheduleSyncProviderActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task PerformSync(WorkScheduleSyncProvider obj)
        {
            if (obj.Calendar == null || obj.WorkSchedule == null)
            {
                Logging.Server.Warn(string.Format("{0}.PerformSync: unable to sync. Either calendar or work schedule is null", obj.Name));
                return System.Threading.Tasks.Task.CompletedTask;
            }

            var ctx = obj.Context;
            int counter = 0;

            // The easy method -> delete, then recreate
            var currentYear = DateTime.Today.FirstYearDay();
            var nextYear = currentYear.AddYears(1).LastYearDay();
            foreach (var evt in ctx.GetQuery<Event>()
                .Where(e => e.Calendar == obj.Calendar)
                .Where(e => e.StartDate >= currentYear))
            {
                ctx.Delete(evt);
                counter++;
            }
            Logging.Server.Info(string.Format("{0}.PerformSync: deleting {1} events", obj.Name, counter));

            counter = 0;
            var rules = obj.WorkSchedule.AndParents(lst => lst.WorkScheduleRules, p => p.BaseWorkSchedule)
                .Where(r => r.IsWorkingDay == false)
                .OfType<YearlyWorkScheduleRule>()
                .ToList();
            var dt = currentYear;
            while (dt <= nextYear)
            {
                foreach(var rule in rules.Where(r => r.AppliesTo(dt)))
                {
                    var evt = ctx.Create<Event>();
                    evt.Calendar = obj.Calendar;
                    evt.Summary = rule.Name;
                    evt.StartDate = dt;
                    evt.EndDate = dt;
                    evt.IsAllDay = true;
                    evt.IsViewReadOnly = true;
                    counter++;
                }
                
                dt = dt.AddDays(1);
            }

            Logging.Server.Info(string.Format("{0}.PerformSync: created {1} events", obj.Name, counter));

            obj.NextSync = DateTime.Today.AddDays(1); // once a day is good enougth

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
