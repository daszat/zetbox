using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class StaticFileActions
    {
        [Invocation]
        public static void HandleBlobChange(at.dasz.DocumentManagement.StaticFile obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            if (oldBlob != null && newBlob != oldBlob)
            {
                throw new InvalidOperationException("Changing blob on static files is not allowed");
            }
            e.Result = newBlob;
        }
    }
}
