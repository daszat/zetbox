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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.App.Base
{
    static class ToStringHelper
    {
        /// <summary>
        /// Since floating objects might have no valid/useful ToString() result yet, prefix them with the typename and id.
        /// </summary>
        /// <param name="obj">The current object</param>
        /// <param name="e">The ToString MethodReturnEventArgs.</param>
        internal static void FixupFloatingObjectsToString(IDataObject obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Context != null && obj.Context.IsReadonly) return;
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}(#{1}): {2}", obj.GetType().Name, obj.ID, e.Result);
            }
        }
    }
}
