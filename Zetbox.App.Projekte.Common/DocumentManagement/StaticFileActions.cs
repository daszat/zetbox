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
