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
namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Zetbox.API;

    [Implementor]
    public static class ViewModelDescriptorActions
    {
        [Invocation]
        public static void ToString(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} (default: {1}) [{2}]",
                obj.Description,
                obj.DefaultEditorKind,
                obj.ViewModelRef == null ? "(no type)" : obj.ViewModelRef.ToString());
        }

        [Invocation]
        public static void GetName(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            if (obj.ViewModelRef != null)
            {
                e.Result = string.Format("Gui.ViewModelDescriptors.{0}", Regex.Replace(obj.ViewModelRef.ToTypeName(), @"\W", "_"));
            }
        }
    }
}
