namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class BlobActions
    {
        [Invocation]
        public static void ToString(Zetbox.App.Base.Blob obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.OriginalName;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetStream(Zetbox.App.Base.Blob obj, MethodReturnEventArgs<System.IO.Stream> e)
        {
            e.Result = obj.Context.GetStream(obj.ID);
        }
    }
}
