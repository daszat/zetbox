namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Kistl.API;

    [Implementor]
    public static class ViewModelDescriptorActions
    {
        [Invocation]
        public static void ToString(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} (default: {1}) [{2}]",
                obj.Description,
                obj.DefaultEditorKind,
                obj.ViewModelRef == null ? "(no type)" : obj.ViewModelRef.ToString());
        }

        [Invocation]
        public static void GetName(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            if (obj.ViewModelRef != null)
            {
                var type = obj.ViewModelRef.AsType(false);
                if (type != null)
                {
                    e.Result = string.Format("Gui.ViewModelDescriptors.{0}", Regex.Replace(type.ToString(), @"\W", "_"));
                }
            }
        }
    }
}
