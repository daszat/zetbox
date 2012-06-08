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

    [Implementor]
    public static class DateTimeRangeActions
    {
        [Invocation]
        public static void ToString(DateTimeRange obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0:d} - {1:d}", obj.From, obj.Thru);
        }

        [Invocation]
        public static void get_TotalDays(DateTimeRange obj, PropertyGetterEventArgs<int?> e)
        {
            e.Result = obj.From.HasValue && obj.Thru.HasValue ? (int?)((obj.Thru.Value - obj.From.Value).TotalDays) : null;
        }
    }
}
