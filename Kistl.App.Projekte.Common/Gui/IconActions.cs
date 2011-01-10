using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.App.Projekte.Common.Gui
{
    public static class IconActions
    {
        public static void OnBlob_PreSetter_Icon(Kistl.App.GUI.Icon obj, PropertyPreSetterEventArgs<Kistl.App.Base.Blob> e)
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
