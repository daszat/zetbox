namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class NavigationEntryActions
    {
        [Invocation]
        public static void GetDefaultViewModel(Zetbox.App.GUI.NavigationEntry obj, MethodReturnEventArgs<object> e, Zetbox.API.IZetboxContext dataCtx, System.Object parent)
        {
            // do nothing
        }
    }
}
