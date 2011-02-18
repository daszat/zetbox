namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class CalculatedObjectReferencePropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(CalculatedObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferencedClass;
            if (objClass == null)
            {
                e.Result = "<no class>";
            }
            else if (objClass.Module == null)
            {
                e.Result = "<no namespace>." + objClass.Name;
            }
            else
            {
                e.Result = objClass.Module.Namespace + "." + objClass.Name;
            }
        }
    }
}
