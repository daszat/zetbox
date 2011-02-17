using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class DynamicFileActions
    {
        [Invocation]
        public static void HandleBlobChange(at.dasz.DocumentManagement.DynamicFile obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
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
