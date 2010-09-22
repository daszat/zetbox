using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace at.dasz.DocumentManagement
{
    public static class CustomCommonActions_DocumentManagement
    {
        public static void OnBlob_PreSetter_File(at.dasz.DocumentManagement.File obj, PropertyPreSetterEventArgs<Kistl.App.Base.Blob> e)
        {
            e.Result = obj.HandleBlobChange(e.OldValue, e.NewValue);
        }

        public static void OnHandleBlobChange_DynamicFile(at.dasz.DocumentManagement.DynamicFile obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            // Delete old blob
            if (oldBlob != null)
            {
                obj.Context.Delete(oldBlob);
            }

            e.Result = newBlob;
        }

        public static void OnHandleBlobChange_StaticFile(at.dasz.DocumentManagement.StaticFile obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            if (oldBlob != null && newBlob != oldBlob)
            {
                throw new InvalidOperationException("Changing blob on static files is not allowed");
            }
            e.Result = newBlob;
        }

        public static void OnHandleBlobChange_Document(at.dasz.DocumentManagement.Document obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            if (oldBlob != null && !obj.Revisions.Contains(oldBlob))
            {
                obj.Revisions.Add(oldBlob);
            }
            e.Result = newBlob;
        }

        public static void OnToString_File(at.dasz.DocumentManagement.File obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Name;
        }

        public static void OnNotifyDeleting_File(at.dasz.DocumentManagement.File obj)
        {
            if (obj.Blob != null)
            {
                obj.Context.Delete(obj.Blob);
            }
        }
    }
}
