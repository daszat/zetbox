namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class BlobActions
    {
        private static IFileOpener _fileOpener;
        public BlobActions(IFileOpener fileOpener)
        {
            _fileOpener = fileOpener;
        }

        [Invocation]
        public static void Open(Zetbox.App.Base.Blob obj)
        {
            _fileOpener.ShellExecute(obj.Context.GetFileInfo(obj.ID));
        }
    }
}
