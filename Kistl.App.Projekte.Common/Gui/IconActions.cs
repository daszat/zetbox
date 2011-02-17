namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class IconActions
    {
        [Invocation]
        public static void preSet_Blob(Kistl.App.GUI.Icon obj, PropertyPreSetterEventArgs<Kistl.App.Base.Blob> e)
        {
            // Delete old blob
            if (e.OldValue != null && e.OldValue != e.NewValue)
            {
                obj.Context.Delete(e.OldValue);
            }

            e.Result = e.NewValue;
        }
    }
}
