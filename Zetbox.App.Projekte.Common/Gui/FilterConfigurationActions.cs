
namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;

    [Implementor]
    public static class FilterConfigurationActions
    {
        
        [Invocation]
        public static void GetLabel(Zetbox.App.GUI.FilterConfiguration obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : string.Empty;
        }
    }
}
