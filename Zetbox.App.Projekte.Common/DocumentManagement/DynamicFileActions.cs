using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class DynamicFileActions
    {
        [Invocation]
        public static void HandleBlobChange(at.dasz.DocumentManagement.DynamicFile obj, MethodReturnEventArgs<Zetbox.App.Base.Blob> e, Zetbox.App.Base.Blob oldBlob, Zetbox.App.Base.Blob newBlob)
        {
            // Delete old blob
            if (oldBlob != null && newBlob != oldBlob)
            {
                obj.Context.Delete(oldBlob);
            }

            e.Result = newBlob;
        }
    }
}
