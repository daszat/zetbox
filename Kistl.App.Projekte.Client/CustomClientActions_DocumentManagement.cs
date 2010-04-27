using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Autofac;

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
            // No UI Code in Custom Actions
            // This is the ViewModels Job
            // They know in combination with their associatied Views how to render such tasks
            // ASP.NET would have a big Problem with that function
            throw new NotImplementedException("Add a FileInfo parameter to this method and detect that fact in the DataObjectViewModel while rendering invokable Methods. -> Open FileDialog");
            //string path = Kistl.Client.GuiApplicationContext.Current.ModelFactory.GetSourceFileNameFromUser();
            //if (!string.IsNullOrEmpty(path))
            //{
            //    var fi = new System.IO.FileInfo(path);
            //    int id = obj.Context.CreateBlob(fi, fi.GetMimeType());
            //    obj.Blob = obj.Context.Find<Kistl.App.Base.Blob>(id);
            //    obj.Name = obj.Blob.OriginalName;
            //}
        }
    }
}
