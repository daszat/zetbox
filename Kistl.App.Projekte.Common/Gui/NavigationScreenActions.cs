namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class NavigationScreenActions
    {
        [Invocation]
        public static void ToString(Kistl.App.GUI.NavigationScreen obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = String.Format("Screen: {0}",
                  obj.Title);
        }
    }
}
