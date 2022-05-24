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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.App.Test
{
    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class RecurrenceRuleTesterActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(RecurrenceRuleTester obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "a RecurrenceRuleTester";

            return System.Threading.Tasks.Task.CompletedTask;
        }
        [Invocation]
        public static async System.Threading.Tasks.Task GetCurrent(RecurrenceRuleTester obj, MethodReturnEventArgs<DateTime> e, DateTime? dt)
        {
            if (dt.HasValue)
            {
                e.Result = await obj.Rule.GetCurrent(DateTime.Today, dt.Value);
            }
            else
            {
                e.Result = await obj.Rule.GetCurrent(DateTime.Today);
            }
        }

        [Invocation]
        public static async System.Threading.Tasks.Task GetNext(RecurrenceRuleTester obj, MethodReturnEventArgs<DateTime> e, DateTime? dt)
        {
            if (dt.HasValue)
            {
                e.Result = await obj.Rule.GetNext(DateTime.Today, dt.Value);
            }
            else
            {
                e.Result = await obj.Rule.GetNext(DateTime.Today);
            }
        }
    }
}
