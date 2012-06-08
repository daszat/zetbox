// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    [Implementor]
    public static class ImportedFileActions
    {
        [Invocation]
        public static void HandleBlobChange(ImportedFile obj, MethodReturnEventArgs<Zetbox.App.Base.Blob> e, Zetbox.App.Base.Blob oldBlob, Zetbox.App.Base.Blob newBlob)
        {
            if (oldBlob != null && newBlob != oldBlob)
            {
                throw new InvalidOperationException("Changing blob on imported files is not allowed");
            }
            e.Result = newBlob;
        }

        private static void MakeInternal(IZetboxContext ctx, ImportedFile obj, File doc)
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
