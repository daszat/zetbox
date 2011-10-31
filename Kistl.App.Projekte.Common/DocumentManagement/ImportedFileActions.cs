
namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ImportedFileActions
    {
        [Invocation]
        public static void HandleBlobChange(ImportedFile obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            if (oldBlob != null && newBlob != null && newBlob != oldBlob)
            {
                throw new InvalidOperationException("Changing blob on imported files is not allowed");
            }
            e.Result = newBlob;
        }

        [Invocation]
        public static void MakeDocument(ImportedFile obj, MethodReturnEventArgs<at.dasz.DocumentManagement.Document> e)
        {
            var ctx = obj.Context;
            var doc = ctx.Create<Document>();
            doc.Blob = obj.Blob;
            obj.Blob = null;
            doc.Name = obj.Name;
            ctx.Delete(obj);
        }
        [Invocation]
        public static void MakeDynamicFile(ImportedFile obj, MethodReturnEventArgs<at.dasz.DocumentManagement.Document> e)
        {
            var ctx = obj.Context;
            var doc = ctx.Create<DynamicFile>();
            doc.Blob = obj.Blob;
            obj.Blob = null;
            doc.Name = obj.Name;
            ctx.Delete(obj);
        }
        [Invocation]
        public static void MakeStaticFile(ImportedFile obj, MethodReturnEventArgs<at.dasz.DocumentManagement.Document> e)
        {
            var ctx = obj.Context;
            var doc = ctx.Create<StaticFile>();
            doc.Blob = obj.Blob;
            obj.Blob = null;
            doc.Name = obj.Name;
            ctx.Delete(obj);
        }
    }
}
