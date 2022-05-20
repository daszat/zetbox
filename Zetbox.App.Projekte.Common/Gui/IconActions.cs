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
    using Zetbox.API;
    using System.Text.RegularExpressions;

    [Implementor]
    public static class IconActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Icon obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.IconFile;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetName(Icon obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("Gui.Icons.{0}.{1}", obj.Module.Name, Regex.Replace(obj.IconFile, @"\W", "_"));

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
