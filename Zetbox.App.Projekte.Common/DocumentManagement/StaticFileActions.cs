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
using Zetbox.App.Base;

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class StaticFileActions
    {
        [Invocation]
        public static void HandleBlobChange(StaticFile obj, MethodReturnEventArgs<Blob> e, Blob oldBlob, Blob newBlob)
        {
            if (oldBlob != null && newBlob != oldBlob)
            {
                throw new InvalidOperationException("Changing blob on static files is not allowed");
            }
            e.Result = newBlob;
        }

        [Invocation]
        public static void UploadCanExec(StaticFile obj, MethodReturnEventArgs<bool> e)
        {
            e.Result = obj.Blob == null;
        }

        [Invocation]
        public static void UploadCanExecReason(StaticFile obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "Changing blob on static files is not allowed";
        }
    }
}
