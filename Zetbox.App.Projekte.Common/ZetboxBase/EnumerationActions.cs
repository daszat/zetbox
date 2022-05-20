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
    public static class EnumerationActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task GetEntryByName(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry> e, System.String name)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Name == name);

            return System.Threading.Tasks.Task.CompletedTask;
        }
        [Invocation]
        public static System.Threading.Tasks.Task GetEntryByValue(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry> e, System.Int32 val)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Value == val);

            return System.Threading.Tasks.Task.CompletedTask;
        }
        [Invocation]
        public static System.Threading.Tasks.Task GetLabelByName(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<string> e, System.String name)
        {
            var entry = obj.GetEntryByName(name);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;

            return System.Threading.Tasks.Task.CompletedTask;
        }
        [Invocation]
        public static System.Threading.Tasks.Task GetLabelByValue(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<string> e, System.Int32 val)
        {
            var entry = obj.GetEntryByValue(val);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetName(Enumeration obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("Base.Enumerations.{0}.{1}", obj.Module.Namespace, obj.Name);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Enumeration obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
