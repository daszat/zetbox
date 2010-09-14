
namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
using Kistl.Client.Presentables;
    
    public class CustomClientActions_DocumentManagement
    {
        private static IModelFactory _factory;

        public CustomClientActions_DocumentManagement(IModelFactory factory)
        {
            _factory = factory;
        }

        public static void OnOpen_File(File obj)
        {
            if (obj.Blob != null)
            {
                obj.Blob.Open();
            }
        }

        public static void OnUpload_File(File obj)
        {
            // UI Code in Custom Actions!
            // ASP.NET would have a big Problem with that function
            string path = _factory.GetSourceFileNameFromUser();
            if (!string.IsNullOrEmpty(path))
            {
                var fi = new System.IO.FileInfo(path);
                int id = obj.Context.CreateBlob(fi, fi.GetMimeType());
                obj.Blob = obj.Context.Find<Kistl.App.Base.Blob>(id);
                obj.Name = obj.Blob.OriginalName;
            }
        }
    }
}
