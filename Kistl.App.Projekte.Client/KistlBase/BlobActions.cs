namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public static class BlobActions
    {
        [Invocation]
        public static void Open(Kistl.App.Base.Blob obj)
        {
            obj.Context.GetFileInfo(obj.ID).ShellExecute();
        }
    }
}
