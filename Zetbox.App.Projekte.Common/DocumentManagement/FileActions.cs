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

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class FileActions
    {
        [Invocation]
        public static void preSet_Blob(at.dasz.DocumentManagement.File obj, PropertyPreSetterEventArgs<Zetbox.App.Base.Blob> e)
        {
            e.Result = obj.HandleBlobChange(e.OldValue, e.NewValue);
        }

        [Invocation]
        public static void ToString(at.dasz.DocumentManagement.File obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void NotifyDeleting(at.dasz.DocumentManagement.File obj)
        {
            if (obj.Blob != null)
            {
                obj.Context.Delete(obj.Blob);
            }
        }
    }
}
