using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class DocumentActions
    {
        [Invocation]
        public static void HandleBlobChange(at.dasz.DocumentManagement.Document obj, MethodReturnEventArgs<Zetbox.App.Base.Blob> e, Zetbox.App.Base.Blob oldBlob, Zetbox.App.Base.Blob newBlob)
        {
            if (oldBlob != null && !obj.Revisions.Contains(oldBlob))
            {
                obj.Revisions.Add(oldBlob);
            }
            e.Result = newBlob;
        }

        [Invocation]
        public static void NotifyDeleting(at.dasz.DocumentManagement.Document obj)
        {
            foreach(var blob in obj.Revisions)
            {
                obj.Context.Delete(blob);
            }
        }
    }
}
