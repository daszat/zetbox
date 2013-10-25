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
    using Zetbox.API.Utils;

    [Implementor]
    public static class ViewModelDescriptorActions
    {
        [Invocation]
        public static void ToString(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} (default: {1}) [{2}]",
                obj.Description,
                obj.DefaultEditorKind,
                string.IsNullOrWhiteSpace(obj.ViewModelTypeRef) ? "(no type)" : obj.ViewModelTypeRef);
        }

        [Invocation]
        public static void GetName(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            if (!string.IsNullOrWhiteSpace(obj.ViewModelTypeRef) && obj.ViewModelTypeRef != "ERROR")
            {
                var spec = TypeSpec.Parse(obj.ViewModelTypeRef);

                e.Result = string.Format("Gui.ViewModelDescriptors.{0}", Regex.Replace(spec.GetSimpleName(addAssemblyNames: false), @"\W+", "_"));
            }
        }
    }
}
