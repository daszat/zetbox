using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace at.dasz.DocumentManagement
{
    public static class CustomCommonActions_DocumentManagement
    {
        public static void OnBlob_PreSetter_DynamicFile(at.dasz.DocumentManagement.File obj, PropertyPreSetterEventArgs<Kistl.App.Base.Blob> e)
        {
            // Delete old blob
            if (obj.Blob != null)
            {
                obj.Context.Delete(obj.Blob);
            }
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
