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
        public static void ToString(Zetbox.App.GUI.NavigationEntry obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = String.Format("NavEntry: {0}",
                  obj.Title);
        }
    }
}
