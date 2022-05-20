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
    public static class EnumParameterActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task GetParameterType(EnumParameter obj, MethodReturnEventArgs<Type> e)
        {
            var cls = obj.Enumeration;
            e.Result = Type.GetType(cls.Module.Namespace + "." + cls.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
            BaseParameterActions.DecorateParameterType(obj, e, true);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetParameterTypeString(EnumParameter obj, MethodReturnEventArgs<System.String> e)
        {
            if (obj.Enumeration == null)
            {
                e.Result = "<no enum>";
            }
            else if (obj.Enumeration.Module == null)
            {
                e.Result = "<no namespace>." + obj.Enumeration.Name;
            }
            else
            {
                e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.Name;
            }
            BaseParameterActions.DecorateParameterType(obj, e, true);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
