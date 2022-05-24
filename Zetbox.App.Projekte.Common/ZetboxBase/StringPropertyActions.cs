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
    public static class StringPropertyActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ObjectIsValid(StringProperty obj, ObjectIsValidEventArgs e)
        {
            if (obj.GetLengthConstraint() == null)
            {
                e.Errors.Add(string.Format("String property {0}.{1} must have a string range constraint", obj.ObjectClass.Name, obj.Name));
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetPropertyType(StringProperty obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = typeof(string);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static async System.Threading.Tasks.Task GetElementTypeString(StringProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "string";
            await PropertyActions.DecorateElementType(obj, e, false);
        }

        [Invocation]
        public static async System.Threading.Tasks.Task GetPropertyTypeString(StringProperty obj, MethodReturnEventArgs<string> e)
        {
            await GetElementTypeString(obj, e);
            PropertyActions.DecorateParameterType(obj, e, false, obj.IsList, obj.HasPersistentOrder);
        }
    }
}
