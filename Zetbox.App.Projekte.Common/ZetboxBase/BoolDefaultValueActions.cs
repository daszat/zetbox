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
    public static class BoolDefaultValueActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task GetDefaultValue(Zetbox.App.Base.BoolDefaultValue obj, MethodReturnEventArgs<object> e)
        {
            e.Result = obj.BoolValue;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task ToString(Zetbox.App.Base.BoolDefaultValue obj, MethodReturnEventArgs<System.String> e)
        {
            if (obj.Property != null)
            {
                e.Result = string.Format("{0} will be initialized with '{1}'",
                    obj.Property.Name,
                    obj.BoolValue);
            }
            else
            {
                e.Result = "Initializes a property with a configured bool value";
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
