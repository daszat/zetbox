namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class ViewDescriptorActions
    {
        [Invocation]
        public static void ToString(ViewDescriptor obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}/{1}: {2}",
                obj.Toolkit,
                obj.ControlKind != null ? obj.ControlKind.Name : "(unknown kind)",
                obj.ControlRef == null
                    ? "(none)"
                    : obj.ControlRef.ToString());
        }

    }
}
