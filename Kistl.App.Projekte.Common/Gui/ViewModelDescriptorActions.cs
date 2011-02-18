namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ViewModelDescriptorActions
    {
        [Invocation]
        public static void ToString(ViewModelDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} (default: {1}) [{2}]",
                obj.Description,
                obj.DefaultEditorKind,
                obj.ViewModelRef == null ? "(no type)" : obj.ViewModelRef.ToString());
        }

    }
}
