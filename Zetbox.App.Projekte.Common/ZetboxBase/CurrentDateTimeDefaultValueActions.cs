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
    public static class CurrentDateTimeDefaultValueActions
    {
        [Invocation]
        public static void GetDefaultValue(CurrentDateTimeDefaultValue obj, MethodReturnEventArgs<System.Object> e)
        {
            var dtProp = (DateTimeProperty)obj.Property;
            switch (dtProp.DateTimeStyle)
            {
                case DateTimeStyles.Date:
                    e.Result = DateTime.Today;
                    break;
                case DateTimeStyles.Time:
                    e.Result = DateTime.Now; // TODO: what to do here?
                    break;
                case DateTimeStyles.DateTime:
                default:
                    e.Result = DateTime.Now;
                    break;
            }
        }

        [Invocation]
        public static void ToString(Zetbox.App.Base.CurrentDateTimeDefaultValue obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Property != null)
            {
                var dtProp = (DateTimeProperty)obj.Property;
                switch (dtProp.DateTimeStyle)
                {
                    case DateTimeStyles.Date:
                        e.Result = string.Format("{0} will be initialized with the current date", obj.Property.Name);
                        break;
                    case DateTimeStyles.Time:
                        e.Result = string.Format("{0} will be initialized with the current date and time", obj.Property.Name);  // TODO: what to do here?
                        break;
                    case DateTimeStyles.DateTime:
                    default:
                        e.Result = string.Format("{0} will be initialized with the current date and time", obj.Property.Name);
                        break;
                }
            }
            else
            {
                e.Result = "Initializes a property with the current date and time";
            }
        }
    }
}
