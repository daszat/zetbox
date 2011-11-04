
namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    [Implementor]
    public static class ImportedFileActions
    {
        [Invocation]
        public static void HandleBlobChange(ImportedFile obj, MethodReturnEventArgs<Kistl.App.Base.Blob> e, Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            if (oldBlob != null && newBlob != oldBlob)
            {
                throw new InvalidOperationException("Changing blob on imported files is not allowed");
            }
            e.Result = newBlob;
        }

        private static void MakeInternal(IKistlContext ctx, ImportedFile obj, File doc)
        {
            // Clone blob, so it could be deleted
            doc.Blob = ctx.Find<Blob>(ctx.CreateBlob(ctx.GetFileInfo(obj.Blob.ID), obj.Blob.MimeType));
            doc.Name = obj.Name;
            ctx.Delete(obj);
        }

        [Invocation]
        public static void MakeDocument(ImportedFile obj, MethodReturnEventArgs<Document> e)
        {
            var ctx = obj.Context;
            var doc = ctx.Create<Document>();
            MakeInternal(ctx, obj, doc);
            e.Result = doc;
        }
        [Invocation]
        public static void MakeDynamicFile(ImportedFile obj, MethodReturnEventArgs<DynamicFile> e)
        {
            var ctx = obj.Context;
            var doc = ctx.Create<DynamicFile>();
            MakeInternal(ctx, obj, doc);
            e.Result = doc;
        }
        [Invocation]
        public static void MakeStaticFile(ImportedFile obj, MethodReturnEventArgs<StaticFile> e)
        {
            var ctx = obj.Context;
            var doc = ctx.Create<StaticFile>();
            MakeInternal(ctx, obj, doc);
            e.Result = doc;
        }
    }
}
