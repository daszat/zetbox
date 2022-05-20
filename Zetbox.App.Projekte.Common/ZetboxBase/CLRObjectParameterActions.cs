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
    public static class CLRObjectParameterActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task GetParameterType(CLRObjectParameter obj, MethodReturnEventArgs<Type> e)
        {
            e.Result = Type.GetType(obj.TypeRef, throwOnError: true);
            BaseParameterActions.DecorateParameterType(obj, e, false);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetParameterTypeString(CLRObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            if(obj == null || string.IsNullOrWhiteSpace(obj.TypeRef))
            {
                e.Result = "<no type set>";
                return System.Threading.Tasks.Task.CompletedTask;
            }

            var type = Type.GetType(obj.TypeRef, throwOnError: false);
            if (type == null)
            {
                e.Result = "<no type set>";
            }
            else
            {
                e.Result = type.ToCSharpTypeRef();
                BaseParameterActions.DecorateParameterType(obj, e, false);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
