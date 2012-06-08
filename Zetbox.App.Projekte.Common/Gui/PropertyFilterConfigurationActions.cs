
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    [Implementor]
    public static class PropertyFilterConfigurationActions
    {
        [Invocation]
        public static void GetLabel(Kistl.App.GUI.PropertyFilterConfiguration obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Property.GetLabel();
        }
    }
}
