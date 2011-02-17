using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace at.dasz.DocumentManagement
{
    [Implementor]
    public static class FileActions
    {
        [Invocation]
        public static void preSet_Blob(at.dasz.DocumentManagement.File obj, PropertyPreSetterEventArgs<Kistl.App.Base.Blob> e)
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
