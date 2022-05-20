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

    [Implementor]
    public static class NavigationEntryActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Zetbox.App.GUI.NavigationEntry obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = String.Format("NavEntry: {0}",
                  obj.Title);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }

    [Implementor]
    public static class NavigationScreenActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Zetbox.App.GUI.NavigationScreen obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = String.Format("NavigationScreen: {0}",
                  obj.Title);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }

    [Implementor]
    public static class NavigationSearchScreenActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Zetbox.App.GUI.NavigationSearchScreen obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = String.Format("NavigationSearchScreen ({1}): {0}",
                  obj.Title, obj.Type);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }

    [Implementor]
    public static class NavigationActionActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Zetbox.App.GUI.NavigationAction obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = String.Format("NavigationAction: {0}",
                  obj.Title);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
