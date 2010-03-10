using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace at.dasz.DocumentManagement
{
    public static class CustomClientActions_DocumentManagement
    {
        public static void OnOpen_File(at.dasz.DocumentManagement.File obj)
        {
            if (obj.Blob != null)
            {
                obj.Blob.Open();
            }
        }

        public static void OnUpload_File(at.dasz.DocumentManagement.File obj)
        {
            string path = Kistl.Client.GuiApplicationContext.Current.Factory.GetSourceFileNameFromUser();
            if (!string.IsNullOrEmpty(path))
            {
                var fi = new System.IO.FileInfo(path);
                int id = obj.Context.CreateBlob(fi, fi.GetMimeType());
                obj.Blob = obj.Context.Find<Kistl.App.Base.Blob>(id);
            }
        }
    }
}
