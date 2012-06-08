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
    using System.Text.RegularExpressions;

    [Implementor]
    public static class SequenceActions
    {
        [Invocation]
        public static void ToString(Sequence obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void GetName(Sequence obj, MethodReturnEventArgs<string> e)
        {
            if (!string.IsNullOrEmpty(obj.Name) && obj.Module != null && !string.IsNullOrEmpty(obj.Module.Name))
            {
                e.Result = "Base.Sequences." + obj.Module.Name + "." + Regex.Replace(obj.Name, "\\W", "_");
            }
        }

        [Invocation]
        public static void preSet_Data(Sequence obj, PropertyPreSetterEventArgs<SequenceData> e)
        {
            // TODO: Workaroud: No! Changing Sequence Data is not allowed
            if (e.OldValue != null)
            {
                e.Result = e.OldValue;
            }
        }
    }
}
