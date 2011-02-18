namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class IconActions
    {
        private static IViewModelFactory _factory;

        public IconActions(IViewModelFactory factory)
        {
            _factory = factory;
        }

        [Invocation]
        public static void Upload(Kistl.App.GUI.Icon obj)
        {
            // UI Code in Custom Actions!
            // ASP.NET would have a big Problem with that function
            string path = _factory.GetSourceFileNameFromUser();
            if (!string.IsNullOrEmpty(path))
            {
                var fi = new System.IO.FileInfo(path);
                int id = obj.Context.CreateBlob(fi, fi.GetMimeType());
                obj.Blob = obj.Context.Find<Kistl.App.Base.Blob>(id);
                obj.IconFile = obj.Blob.OriginalName;
            }
        }

        [Invocation]
        public static void Open(Kistl.App.GUI.Icon obj)
        {
            if (obj.Blob != null)
            {
                obj.Blob.Open();
            }
        }
    }
}
