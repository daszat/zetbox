
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
        public static void MakeDocument(ImportedFile obj, MethodReturnEventArgs<at.dasz.DocumentManagement.Document> e)
        {
        }
    }
}
