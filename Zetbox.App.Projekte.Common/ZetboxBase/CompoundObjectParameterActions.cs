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
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    [Implementor]
    public static class CompoundObjectParameterActions
    {
        [Invocation]
        public static void GetParameterType(Zetbox.App.Base.CompoundObjectParameter obj, Zetbox.API.MethodReturnEventArgs<System.Type> e)
        {
            var cls = obj.CompoundObject;
            e.Result = Type.GetType(cls.Module.Namespace + "." + cls.Name + ", " + Zetbox.API.Helper.InterfaceAssembly, true);
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }

        [Invocation]
        public static void GetParameterTypeString(CompoundObjectParameter obj, MethodReturnEventArgs<string> e)
        {
            var cls = obj.CompoundObject;
            if (cls == null)
            {
                e.Result = "<no type>";
            }
            else if (cls.Module == null)
            {
                e.Result = "<no namespace>." + cls.Name;
            }
            else
            {
                e.Result = cls.Module.Namespace + "." + cls.Name;
            }
            BaseParameterActions.DecorateParameterType(obj, e, false);
        }
    }
}
