namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ObjectReferencePlaceholderPropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(ObjectReferencePlaceholderProperty obj, MethodReturnEventArgs<string> e)
        {
            if (obj.ReferencedObjectClass == null)
            {
                e.Result = "Empty ObjectReferencePlaceholderProperty";
                return;
            }

            if (obj.ReferencedObjectClass.Module == null)
            {
                e.Result = "Invalid ReferencedObjectClass (no module)";
                return;
            }

            e.Result = obj.ReferencedObjectClass.Module.Namespace + "." + obj.ReferencedObjectClass.Name;
        }
    }
}
