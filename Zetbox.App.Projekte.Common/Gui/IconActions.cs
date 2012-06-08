namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using System.Text.RegularExpressions;

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

        [Invocation]
        public static void ToString(Icon obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.IconFile;
        }


        [Invocation]
        public static void GetName(Icon obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("Gui.Icons.{0}.{1}", obj.Module.Name, Regex.Replace(obj.IconFile, @"\W", "_"));
        }
    }
}
