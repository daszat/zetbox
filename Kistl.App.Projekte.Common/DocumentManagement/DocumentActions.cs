using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class DocumentActions
    {
        [Invocation]
        public static void HandleBlobChange(at.dasz.DocumentManagement.Document obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            if (oldBlob != null && !obj.Revisions.Contains(oldBlob))
            {
                obj.Revisions.Add(oldBlob);
            }
            e.Result = newBlob;
        }
    }
}
