namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class NavigationEntryActions
    {
        [Invocation]
        public static void GetDefaultViewModel(Kistl.App.GUI.NavigationEntry obj, MethodReturnEventArgs<object> e, Kistl.API.IKistlContext dataCtx, System.Object parent)
        {
            // do nothing
        }
    }
}
