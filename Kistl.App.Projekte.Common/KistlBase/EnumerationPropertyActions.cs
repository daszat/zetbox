namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class EnumerationPropertyActions
    {
        [Invocation]
        public static void GetPropertyTypeString(EnumerationProperty obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Enumeration == null)
            {
                e.Result = "<no enum>";
            }
            else if (obj.Enumeration.Module == null)
            {
                e.Result = "<no namespace>." + obj.Enumeration.Name;
            }
            else
            {
                e.Result = obj.Enumeration.Module.Namespace + "." + obj.Enumeration.Name;
            }
        }
    }
}
