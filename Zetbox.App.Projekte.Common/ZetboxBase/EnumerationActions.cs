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
        public static void GetEntryByName(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry> e, System.String name)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Name == name);
        }
        [Invocation]
        public static void GetEntryByValue(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<Zetbox.App.Base.EnumerationEntry> e, System.Int32 val)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Value == val);
        }
        [Invocation]
        public static void GetLabelByName(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<string> e, System.String name)
        {
            var entry = obj.GetEntryByName(name);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;
        }
        [Invocation]
        public static void GetLabelByValue(Zetbox.App.Base.Enumeration obj, MethodReturnEventArgs<string> e, System.Int32 val)
        {
            var entry = obj.GetEntryByValue(val);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        [Invocation]
        public static void ToString(Enumeration obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }
    }
}
